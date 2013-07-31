using System;
using System.Text;
using System.Web.Script.Serialization;
using CoolApp.Common.Extensions;
using CoolApp.Core.Interfaces.External;
using CoolApp.Core.Models.Mobile;
using CoolApp.Infrastructure.Configuration;
using CoolApp.Infrastructure.Configuration.Notifications;
using RestSharp;

namespace CoolApp.Infrastructure.Helpers
{
    public class MobileRestAPI : IMobileRestAPI
    {
        private readonly NotificationProviderElement _providerConfig;
        private readonly NotificationErrorCollection _errorConfig;

        public MobileRestAPI()
        {
            _providerConfig = AppConfig.Instance.Notifications.Providers[AppConfig.Instance.Notifications.DefaultProvider];
            _errorConfig = AppConfig.Instance.Notifications.Errors;
        }

        #region Private Methods

        /// <summary>
        /// Queries the string call. (Only HTTPS calls)
        /// </summary>
        /// <param name="urlAction">The URL action.</param>
        /// <param name="bodyObject">The param dictionary.</param>
        /// <param name="method">The method. (Only GET or DELETE calls)</param>
        /// <returns></returns>
        private object QueryStringCall(string urlAction, object bodyObject, Method method)
        {
            if(bodyObject == null)
            {
                return new { error = _errorConfig["MissingParameter"] };
            }

            if(method != Method.GET && method != Method.DELETE)
            {
                return new { error = _errorConfig["GetDeleteAllowedOnly"] };
            }            

            var requestURL = new StringBuilder(_providerConfig.BaseURL);

            requestURL.Append(string.Format("/{0}{1}", _providerConfig.Version, urlAction)).Append(bodyObject.GetQueryString());

            var client = new RestClient(_providerConfig.BaseURL) { CookieContainer = new System.Net.CookieContainer() };

            LoginMobileServer(client);

            var request = new RestRequest(method) { RequestFormat = DataFormat.Json };

            request.AddUrlSegment("appkey", _providerConfig.AppKey);

            var result = client.Execute(request);

            if (result.ErrorException != null)
            {
                return new { error = result.ErrorMessage, exception = result.ErrorException };
            }

            return result.Content;
        }

        /// <summary>
        /// Perform a forms data call.
        /// </summary>
        /// <param name="urlAction">The URL action.</param>
        /// <param name="bodyObject">The body object.</param>
        /// <param name="method">The method. (Only PUT and POST methods)</param>
        /// <returns></returns>
        private object FormDataCall(string urlAction, object bodyObject, Method method)
        {
            if (bodyObject == null)
            {
                return new { error = _errorConfig["MissingParameter"] };
            }

            if (method != Method.PUT && method != Method.POST)
            {
                return new { error = _errorConfig["PutPostAllowedOnly"] };
            }

            var client = new RestClient(_providerConfig.BaseURL) { CookieContainer = new System.Net.CookieContainer() };

            LoginMobileServer(client);

            var request = new RestRequest(String.Format("/{0}{1}",_providerConfig.Version, urlAction), method) { RequestFormat = DataFormat.Json };

            request.AddUrlSegment("appkey", _providerConfig.AppKey);

            request.AddBody(bodyObject);

            var result = client.Execute(request);

            if (result.ErrorException != null)
            {
                return new { error = result.ErrorMessage, exception = result.ErrorException };
            }

            return result.Content;
        }

        /// <summary>
        /// Logins on the mobile server.
        /// </summary>
        /// <param name="client">The client of request.</param>
        /// <returns></returns>
        private RestResponse LoginMobileServer(IRestClient client)
        {
            var requestLogin = new RestRequest(_providerConfig.Version + _providerConfig.LoginMethodUrl, Method.POST)
            {
                RequestFormat = DataFormat.Json,
            };

            requestLogin.AddUrlSegment("appkey", _providerConfig.AppKey);

            requestLogin.AddBody(new
            {
                login = _providerConfig.AuthUser,
                password = _providerConfig.AuthPassword
            });

            return client.Execute(requestLogin) as RestResponse;
        }

        /// <summary>
        /// Mobiles the rest call.
        /// </summary>
        /// <param name="urlAction">The URL action.</param>
        /// <param name="bodyObject">The body object.</param>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        private object MobileRestCall(string urlAction, object bodyObject, string method)
        {
            switch (method.ToUpper())
            {
                case "POST":
                    return FormDataCall(urlAction, bodyObject, Method.POST);
                case "PUT":
                    return FormDataCall(urlAction, bodyObject, Method.PUT);
                case "GET":
                    return QueryStringCall(urlAction, bodyObject, Method.GET);
                case "DELETE":
                    return QueryStringCall(urlAction, bodyObject, Method.DELETE);
                default:
                    return FormDataCall(urlAction, bodyObject, Method.POST);
            }
        }

        #endregion

        #region Public Methods

        public object SendNotification(MobileNotification notification)
        {
            var o = new
                {
                    channel = notification.Channel,
                    to_ids = notification.UserIds,
                    payload = new {
                        title = notification.Title,
                        badge = notification.Badge,
                        alert = notification.Text,
                        sound = notification.Sound ?? _providerConfig.DefaultSound,
                        vibrate = notification.Vibrate,
                        icon = notification.Icon
                    },
                };

            var result = MobileRestCall(_providerConfig.PushNotificationUrl, o, "POST");

            var serializer1 = new JavaScriptSerializer();
            var json = serializer1.Deserialize<object>(result.ToString());

            return json;
        }

        #endregion
    }
}

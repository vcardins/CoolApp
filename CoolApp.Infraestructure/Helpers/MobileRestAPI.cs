using System;
using System.Text;
using CoolApp.Common.Extensions;
using CoolApp.Core.Interfaces.External;
using CoolApp.Core.Models.Mobile;
using CoolApp.Infrastructure.Configuration.Notifications;
using CoolApp.Infrastructure.Notifications;
using RestSharp;

namespace CoolApp.Infrastructure.Helpers
{
    public class MobileRestAPI : IMobileRestAPI
    {
        private readonly NotificationProviderElement _config = NotificationManager.Default;

        private object Errors = new
            {
                ObjectParamNull = "",
                MethodOnlyAllowedToGETAndDELETECalls = "",
                MethodOnlyAllowedToPUTAndPOSTCalls = ""
            };

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
                return new { error = "The param dictionary cannot be null." };
            }

            if(method != Method.GET && method != Method.DELETE)
            {
                return new { error = "The query string call allow only GET and DELETE calls." };
            }            

            var requestURL = new StringBuilder(_config.BaseURL);

            requestURL.Append(string.Format("/{0}{1}", _config.Version, urlAction)).Append(bodyObject.GetQueryString());

            var client = new RestClient(_config.BaseURL) { CookieContainer = new System.Net.CookieContainer() };

            LoginMobileServer(client);

            var request = new RestRequest(method) { RequestFormat = DataFormat.Json };

            request.AddUrlSegment("appkey", _config.AppKey);

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
                return new { error = "The param dictionary cannot be null." };
            }

            if (method != Method.PUT && method != Method.POST)
            {
                return new { error = "The form data call allow only PUT and POST calls." };
            }

            var client = new RestClient(_config.BaseURL) { CookieContainer = new System.Net.CookieContainer() };

            LoginMobileServer(client);

            var request = new RestRequest(String.Format("/{0}{1}",_config.Version, urlAction), method) { RequestFormat = DataFormat.Json };

            request.AddUrlSegment("appkey", _config.AppKey);

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
            var requestLogin = new RestRequest(_config.Version + _config.LoginMethodUrl, Method.POST)
            {
                RequestFormat = DataFormat.Json,
            };

            requestLogin.AddUrlSegment("appkey", _config.AppKey);

            requestLogin.AddBody(new
            {
                login = _config.AuthUser,
                password = _config.AuthPassword
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

        public void SendNotification(MobileNotification mobileNotification)
        {
            const string notificationURLAction = "/push_notification/notify.json?key={appkey}";
            var notification = new
                {
                    channel = mobileNotification.Channel,
                    to_ids = mobileNotification.UserIds,
                    payload = new {
                        title = mobileNotification.Title,
                        badge = mobileNotification.Badge,
                        alert = mobileNotification.Text,
                        sound = (String.IsNullOrEmpty(mobileNotification.Sound)?"default":mobileNotification.Sound),
                        vibrate = mobileNotification.Vibrate,
                        icon = mobileNotification.Icon
                    },
                };

            MobileRestCall(notificationURLAction, notification, "POST");
        }

        #endregion
    }
}

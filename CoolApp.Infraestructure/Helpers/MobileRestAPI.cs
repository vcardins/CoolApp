using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolApp.Core.Interfaces.External;
using RestSharp;
using RestSharp.Authenticators;

namespace CoolApp.Infraestructure.Helpers
{
    public class MobileRestAPI : IMobileRestAPI
    {
        const string AppKey = "aB5BBr6oKB5mJYUKjqskLeTQjAWxKbK1";
        const string BaseURL = "https://api.cloud.appcelerator.com";
        private const string Version = "/v1";
        private const string User = "";
        private const string Pass = "";
        private const string Sound = "";

        private object Errors = new
            {
                ObjectParamNull = "",
                MethodOnlyAllowedToGETAndDELETECalls = "",
                MethodOnlyAllowedToPUTAndPOSTCalls = ""
            };

        private object URLs = new
            {
                LoginMethodURL = ""
            };

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

            var requestURL = new StringBuilder(urlAction);

            requestURL.Append("?key=").Append(AppKey);

            //foreach (var param in bodyObject)
            //{
            //    requestURL.Append("&").Append(param.Key).Append("=").Append(param.Value);
            //}

            var client = new RestClient(requestURL.ToString());

            var request = new RestRequest(method) { RequestFormat = DataFormat.Json };

            var result = client.Execute(request);

            if (result.ErrorException != null)
            {
                return new { error = result.ErrorMessage, exception = result.ErrorException };
            }

            return result.Content;
        }

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

            var client = new RestClient(BaseURL);

            client.CookieContainer = new System.Net.CookieContainer();

            var requestLogin = new RestRequest("/v1/users/login.json?key={appkey}", Method.POST)
            {
                RequestFormat = DataFormat.Json,
            };

            requestLogin.AddUrlSegment("appkey", AppKey);

            requestLogin.AddBody(new
            {
                login = "dsmoreira",
                password = "75915346"
            });

            var response = client.Execute(requestLogin);

            var request = new RestRequest(urlAction, method) { RequestFormat = DataFormat.Json };

            request.AddUrlSegment("appkey", AppKey);

            request.AddBody(bodyObject);

            var result = client.Execute(request);

            if (result.ErrorException != null)
            {
                return new { error = result.ErrorMessage, exception = result.ErrorException };
            }

            return result.Content;
        }

        private void LoginMobileServer()
        {
            var client = new RestClient(" https://api.cloud.appcelerator.com");
            var request = new RestRequest("/v1/users/login.json?key={appkey}", Method.POST)
            {
                RequestFormat = DataFormat.Json,
            };
            request.AddUrlSegment("appkey", "mykey");
            request.AddBody(new
            {
                login = "user",
                password = "password"
            });
            var response = client.Execute(request); //makes the request but doesn't get a response
            var content = response.Content;
        }

        public object MobileRestCall(string urlAction, object bodyObject, string method)
        {
            switch (method.ToUpper())
            {
                    case "POST":
                    return FormDataCall(urlAction, bodyObject, Method.POST);
                        break;
                    case "PUT":
                        return FormDataCall(urlAction, bodyObject, Method.PUT);
                        break;
                    case "GET":
                        return QueryStringCall(urlAction, bodyObject, Method.GET);
                        break;
                    case "DELETE":
                        return QueryStringCall(urlAction, bodyObject, Method.DELETE);
                        break;
                    default:
                        return FormDataCall(urlAction, bodyObject, Method.POST);
                        break;
            }
        }

        public void SendNotification(string title, string text, string channel, string clientId, string devideToken)
        {
            const string notificationURLAction = "/v1/push_notification/notify.json?key={appkey}";
            var notification = new
                {
                    channel = channel,
                    payload = new {
                        title = title,
                        alert = text,
                        sound = "default"
                    }
                };

            MobileRestCall(notificationURLAction, notification, "POST");
        }
    }
}

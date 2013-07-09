#region credits
// ***********************************************************************
// Assembly	: TaskForceManager
// Author	: Rod Johnson
// Created	: 03-09-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion
#region

using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using CoolApp.Application.Startup;

#endregion

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppStartup), "WebApi")]

namespace CoolApp.Application.Startup
{
    #region

    

    #endregion

    public partial  class AppStartup
	{
        public static void WebApi()
        {
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"\d+" }
            );

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                name: "DefaultApiWithActions",
                routeTemplate: "api/{controller}/{action}"
            );

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "DefaultApiGet", "api/{controller}", 
                new { action = "Get" }, 
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );
            
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "DefaultApiPost", "api/{controller}", 
                new { action = "Post" }, 
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );
            
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "DefaultApiPut", "api/{controller}", 
                new { action = "Put" }, 
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) }
            );
            
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "DefaultApiDelete", "api/{controller}", 
                new { action = "Delete" }, 
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) }
            );
        }
	}
}
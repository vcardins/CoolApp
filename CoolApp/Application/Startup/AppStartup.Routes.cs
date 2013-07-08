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

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoolApp.Application.Startup
{
    #region

    

    #endregion

    public partial class AppStartup
    {
        public static void Routes()
        {
            RouteTable.Routes.MapHubs();

            // Allows to execute "/notfound" when requesting things like /bin or /App_Data.
            RouteTable.Routes.MapRoute(
                name: "NotFound",
                url: "notfound",
                defaults: new RouteValueDictionary(new { controller = "NotFound", action = "NotFound" }),
                constraints: new RouteValueDictionary(new { incoming = new IncomingRequestRouteConstraint() })
            );

            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RouteTable.Routes.LowercaseUrls = true;

            RouteTable.Routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        class IncomingRequestRouteConstraint : IRouteConstraint
        {
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                return routeDirection == RouteDirection.IncomingRequest;
            }
        }
    }
}

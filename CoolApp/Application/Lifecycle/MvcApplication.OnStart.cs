using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using CoolApp.Application.Startup;

namespace CoolApp.Application.Lifecycle
{
    public partial class MvcApplication
	{

        protected void Application_Start()
        {            
            AreaRegistration.RegisterAllAreas();
            AppStartup.Routes();

          
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;

        }
	}
}
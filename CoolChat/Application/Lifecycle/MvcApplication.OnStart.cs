using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using CoolChat.Application.Startup;
using Newtonsoft.Json.Serialization;

namespace CoolChat.Application.Lifecycle
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
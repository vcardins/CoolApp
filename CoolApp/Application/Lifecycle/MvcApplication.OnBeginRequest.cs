using CoolApp.Infraestructure.Tracing;

namespace CoolApp.Application.Lifecycle
{
    public partial class MvcApplication
    {
        /// <summary>
        /// Fired when an application request is received. It's the first event fired for a request, which is often a page request (URL) that a user enters.
        /// </summary>
        protected void Application_BeginRequest()
        {
            Tracing.Verbose("Begin Request: " + System.Web.HttpContext.Current.Request.RawUrl);
        }
    }
}
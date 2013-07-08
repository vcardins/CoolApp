namespace CoolApp.Application.Lifecycle
{
    public partial class MvcApplication
    {
        /// <summary>
        /// Fired when the ASP.NET page framework completes an authorization request. It allows caching modules to serve the request from the cache, thus bypassing handler execution.
        /// </summary>
        protected void Application_ResolveRequestCache()
        {            

        }
    }
}
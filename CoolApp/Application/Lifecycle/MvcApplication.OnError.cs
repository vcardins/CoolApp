namespace CoolApp.Application.Lifecycle
{
    public partial class MvcApplication
	{
        /// <summary>
        /// Fired when an unhandled exception is encountered within the application.
        /// </summary>
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //new WebErrorEventEx(ex, this).Raise();
        }
	}
}
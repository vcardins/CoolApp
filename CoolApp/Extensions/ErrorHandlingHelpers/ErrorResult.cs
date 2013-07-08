#region credits
// ***********************************************************************
// Assembly	: Deten
// Author	: Marko Ilievski
// Created	: 04-12-2013
// 
// Last Modified By : Marko Ilievski
// Last Modified On : 04-12-2013
// ***********************************************************************
#endregion

using System.Net;
using System.Web.Mvc;

namespace TaskForceManager.Extensions.ErrorHandlingHelpers
{
    #region

    

    #endregion

    public class ErrorResult : ActionResult
    {
        private const string ErrorView = "~/Views/Error/Error.cshtml";
        private const string NotFoundView = "~/Views/Error/NotFound.cshtml";
        private const string AccessDeniedView = "~/Views/Error/AccessDenied.cshtml";

        /// <summary>
        /// The status code of the error. Defaults to "NotFound".
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The view data passed to the NotFound view.
        /// </summary>
        public ViewDataDictionary ViewData { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            var request = context.HttpContext.Request;

            string viewName = ErrorView;
            // TODO: Add logic for not authorized requests
            switch (StatusCode)
            {
                case HttpStatusCode.NotFound :
                    viewName = NotFoundView;
                    break;
                case HttpStatusCode.Unauthorized:
                    viewName = AccessDeniedView;
                    break;
            }

            // We need to clear both headers and content
            // because Clear() doesn't clear the headers
            // see: http://msdn.microsoft.com/en-us/library/system.web.httpresponse.clear.aspx
            response.ClearHeaders();
            response.ClearContent();
            response.StatusCode = (int)StatusCode;

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            response.TrySkipIisCustomErrors = true;

            if (request.IsAjaxRequest())
            {
                // TODO: Add logic for AJAX requests
            }

            var viewResult = new ViewResult
            {
                ViewName = viewName,
                ViewData = ViewData ?? new ViewDataDictionary()
            };

            viewResult.ExecuteResult(context);
        }
    }
}
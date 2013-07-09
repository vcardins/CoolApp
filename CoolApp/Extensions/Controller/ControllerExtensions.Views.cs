using System;
using System.IO;
using System.Web.Mvc;

namespace CoolApp.Extensions.Controller
{
    public static partial class ControllerExtensions
    {
        public static bool ViewExists(this System.Web.Mvc.Controller controller, string name)
        {
            ViewEngineResult result = ViewEngines.Engines.FindView(controller.ControllerContext, name, null);
            return (result.View != null);
        }

        /// <summary>
        ///     Renders a (partial) view to string.
        /// </summary>
        /// <param name="controller">Controller to extend</param>
        /// <param name="viewName">(Partial) view to render</param>
        /// <returns>Rendered (partial) view as string</returns>
        public static string RenderPartialViewToString(this System.Web.Mvc.Controller controller, string viewName)
        {
            return controller.RenderViewToString(viewName, null);
        }

        public static string RenderViewToString(this System.Web.Mvc.Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            try
            {
                using (var sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null);
                    var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
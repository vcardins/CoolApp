using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CoolApp.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class MultiResponseFormatAttribute : ActionFilterAttribute
    {

        public static Func<ControllerContext, string> AjaxViewNameConvention = context => "_" + context.RouteData.GetRequiredString("action");
        private PropertyCase _propertyCase;
        private bool _convertDate;

        /// <summary>
        /// The view name for non-Ajax requests
        /// </summary>
        public string NonAjaxViewName { get; set; }

        /// <summary>
        /// The view name for Ajax requests
        /// </summary>
        public string AjaxViewName { get; set; }

        /// <summary>Gets or sets the serialiser settings</summary> 
        public JsonSerializerSettings Settings { get; set; }

        /// <summary>Gets or sets the encoding of the response</summary> 
        public Encoding ContentEncoding { get; set; }

        /// <summary>Gets or sets the content type for the response</summary> 
        public string ContentType { get; set; }

        /// <summary>Gets the formatting types depending on whether we are in debug mode</summary> 
        private Formatting Formatting
        {
            get
            {
                return Debugger.IsAttached ? Formatting.Indented : Formatting.None;
            }
        }

      
        public override void OnActionExecuted(ActionExecutedContext context)
        {

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
         
            var viewResult = context.Result as ViewResult;
            if (viewResult == null)
                return;

            
            var tempData = context.Controller.TempData;
            var viewData = context.Controller.ViewData;

            viewData.Model = viewResult.Model;

            var req = context.HttpContext.Request;
            var modelState = viewData.ModelState;
            var errs = modelState.Keys.Where(key => modelState[key].Errors.Count > 0).ToDictionary<string, string, IList<string>>(key => key, key => modelState[key].Errors.Select(e => e.ErrorMessage).ToList());

            if (!req.IsAjaxRequest()){
                context.Result = new ViewResult
                {
                    ViewName = viewResult.ViewName ?? GetViewName(context),
                    TempData = tempData,
                    ViewData = viewData
                };
            } 
            else 
            {
                HttpResponseBase response = context.HttpContext.Response;

                // set content type 
                //response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

                //var isJson = req.ContentType.Split(';').Any(t => t.Equals("application/json", StringComparison.OrdinalIgnoreCase));
                var isJson = req.Headers["Accept"].Split(',').Any(t => t.Equals("application/json", StringComparison.OrdinalIgnoreCase));
                if (isJson)
                {
                    _propertyCase = PropertyCase.CamelCase;
                    _convertDate = true;

                    var view = new JsonResult();
                    if (tempData.ContainsKey("ActionResult"))
                    {
                        view.Data = tempData["ActionResult"];
                    }
                    else
                    {
                        var msg = tempData.ContainsKey("Message") ? tempData["Message"] : String.Empty;
                        if (errs.Any())
                        {
                            view.Data = new { errors = errs, success = false, message = msg };
                        }
                        else
                        {
                            view.Data = new { data = viewData.Model, success = true, message = msg };
                        }
                    }

                    // set content encoding 
                    if (ContentEncoding != null)
                    {
                        response.ContentEncoding = ContentEncoding;
                    }

                    if (Settings == null)
                    {

                        Settings = new JsonSerializerSettings();
                        if (_propertyCase == PropertyCase.CamelCase)
                        {
                            Settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        }

                        if (_convertDate)
                        {
                            Settings.Converters = new List<JsonConverter>
                        {
                            new JavaScriptDateTimeConverter()
                        };
                        }
                    } 

                    Settings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        
                    };

                    var x = JsonConvert.SerializeObject(view.Data, Formatting, Settings); 
                    context.Result = new JsonResult
                    {
                        Data = view.Data
                    };

                }
                else
                {
                    var viewName = GetAjaxViewName(context) ?? viewResult.ViewName;
                    var vResult = context.Result as ViewResult;
                    context.Result = new PartialViewResult
                    {
                        TempData = vResult.TempData,
                        ViewData = vResult.ViewData,
                        ViewName = viewName
                    };

                }
            }
        }

        private string GetViewName(ControllerContext context)
        {
            return !string.IsNullOrEmpty(NonAjaxViewName) ? NonAjaxViewName : context.RouteData.GetRequiredString("action");
        }

        private string GetAjaxViewName(ControllerContext context)
        {
            var ajaxViewName = AjaxViewNameConvention(context);
            var result = ViewEngines.Engines.FindView(context, ajaxViewName, null);

            ajaxViewName = (result.View != null) ? ajaxViewName : GetViewName(context);
            return !string.IsNullOrEmpty(AjaxViewName) ? AjaxViewName : ajaxViewName;
        }
    }

    public enum PropertyCase
    {
        PascalCase,
        CamelCase
    };
}
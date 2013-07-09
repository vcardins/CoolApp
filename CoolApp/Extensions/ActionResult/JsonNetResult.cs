#region credits
// ***********************************************************************
// Assembly	: Deten
// Author	: Momentum
// Created	: 03-17-2013
// 
// Last Modified By : Momentum
// Last Modified On : 03-21-2013
// ***********************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CoolApp.Extensions.TempData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CoolApp.Extensions.ActionResult
{
    #region

    

    #endregion

    /// <summary> 
    /// Simple Json Result that implements the Json.NET serialiser offering more versatile serialisation 
    /// </summary> 
    /// 
    public class JsonNetResult : System.Web.Mvc.ActionResult
    {

        private readonly PropertyCase _propertyCase;
        private readonly bool _convertDate;

        public JsonNetResult()
            : this(null)
        {
        }

        public JsonNetResult(object responseBody, PropertyCase propertyCase = PropertyCase.CamelCase, bool convertDate = true)
        {
            ResponseBody = responseBody;
            _propertyCase = propertyCase;
            _convertDate = convertDate;
        }

        public JsonNetResult(object responseBody, JsonSerializerSettings settings)
        {
            Settings = settings;
        }

        /// <summary>Gets or sets the serialiser settings</summary> 
        public JsonSerializerSettings Settings { get; set; }

        /// <summary>Gets or sets the encoding of the response</summary> 
        public Encoding ContentEncoding { get; set; }

        /// <summary>Gets or sets the content type for the response</summary> 
        public string ContentType { get; set; }

        /// <summary>Gets or sets the body of the response</summary> 
        public object ResponseBody { get; set; }

        /// <summary>Gets the formatting types depending on whether we are in debug mode</summary> 
        private Formatting Formatting
        {
            get
            {
                return Debugger.IsAttached ? Formatting.Indented : Formatting.None;
            }
        }

        /// <summary> 
        /// Serialises the response and writes it out to the response object 
        /// </summary> 
        /// <param name="context">The execution context</param> 
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            // set content type 
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? 
                                    ContentType : 
                                    "application/json";

            if (ResponseBody == null)
            {
                var tempData = context.Controller.TempData;
                var success = !tempData.HasError();
                var status = success ?                             
                            "Success" : tempData.ContainsKey("Info") ?
                            "Error" : (tempData.ContainsKey("Success") ? 
                            "Info" : "");

                if (!tempData.ContainsKey("Data"))
                {
                    ResponseBody = new { success = success, message = tempData[status] };
                }
                else
                {
                    ResponseBody = new { success = success, message = tempData[status], data = tempData["data"] };
                }
                
            }

            // set content encoding 
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Settings == null) { 
                
                Settings = new JsonSerializerSettings();
                if (_propertyCase == PropertyCase.CamelCase) { 
                    Settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }

                if (_convertDate) { 
                    Settings.Converters = new List<JsonConverter>
                        {
                            new JavaScriptDateTimeConverter()
                        };
                }
            } 

            if (ResponseBody != null)
            {
                response.Write(JsonConvert.SerializeObject(ResponseBody, Formatting, Settings));
            }
        }
    }

    public enum PropertyCase
    {
        PascalCase,
        CamelCase
    };
}
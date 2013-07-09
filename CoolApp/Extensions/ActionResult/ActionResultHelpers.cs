#region credits
// ***********************************************************************
// Assembly	: EEJ
// Author	: Rod Johnson
// Created	: 03-17-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-21-2013
// ***********************************************************************
#endregion

using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CoolApp.Extensions.ModelState;
using CoolApp.Extensions.TempData;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CoolApp.Extensions.ActionResult
{
    #region

    

    #endregion

    public class ExtjsResult : System.Web.Mvc.ActionResult
    {
        public object Data { private get; set; }
        public bool Success { private get; set; }
        public string Message { private get; set; }

        public ExtjsResult()
        {
            Success = true;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var tempData = context.Controller.TempData;
            var modelState = context.Controller.ViewData.ModelState;
            IDictionary result = new Dictionary<string,object>();

            Success = !tempData.HasError() && modelState.Errors().Count.Equals(0) && Success;
            var msgList = new List<string>();

            var info = tempData[
                            tempData.HasError() ? 
                            "Error" : (tempData.ContainsKey("Success") ? 
                            "Success" : tempData.ContainsKey("Info") ? 
                            "Info" : "")
                            ];

            if (!string.IsNullOrEmpty(Message))
            {
                msgList.Add(Message);
            }
            else
            {
                if (info != null)
                    msgList.Add(info.ToString());

                //Dictionary<string, IList<string>> modelErrors = modelState.Errors();
                //if (modelErrors.Any())
                //{
                //    msgList = msgList.Union<string>(modelErrors).ToList();
                //}                
            }
                

            result.Add("success", Success);

            if ( msgList.Count>0 )
            {
                result.Add("message", string.Join(",", msgList.ToArray()));
            }

            if (Data != null)
            {   
                result.Add("data", Data);
            }

            HttpContextBase httpContextBase = context.HttpContext;
            httpContextBase.Response.Buffer = true;
            httpContextBase.Response.Clear();
            httpContextBase.Response.ContentType = "application/json";
            
            string json =
              JsonConvert.SerializeObject(
                result,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
              );

            httpContextBase.Response.Write(json);
        }

    }
}
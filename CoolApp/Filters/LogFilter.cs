#region credits
// ***********************************************************************
// Assembly	: TaskForceManager
// Author	: Rod Johnson
// Created	: 03-20-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Web.Mvc;

namespace CoolApp.Filters
{
    #region

    

    #endregion

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class LogAttribute : ActionFilterAttribute
    {
        //private readonly ILog _logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            //_logger.InfoFormat(CultureInfo.InvariantCulture, 
            //    "Executing action {0}.{1}", 
            //    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
            //    filterContext.ActionDescriptor.ActionName);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //_logger.InfoFormat(CultureInfo.InvariantCulture,
            //    "Executed action {0}.{1}",
            //    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
            //    filterContext.ActionDescriptor.ActionName);
        }
    }
}
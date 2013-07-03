#region credits
// ***********************************************************************
// Assembly	: TaskForceManager
// Author	: Rod Johnson
// Created	: 02-24-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Web.Mvc;

namespace TaskForceManager.Filters
{
    #region

    

    #endregion

    public class ShowAlerts : ActionFilterAttribute
    {
        private readonly bool _show;

        public ShowAlerts(bool show = true)
        {
            _show = show;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.ShowAlerts = _show;
            base.OnActionExecuted(filterContext);
        }
    }
}
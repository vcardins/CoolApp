#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Security
// Author	: Rod Johnson
// Created	: 03-27-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Web.Mvc;
using System.Web.Security;

namespace CoolApp.Filters
{
    #region

    

    #endregion

    public class OnlyAnonymousAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult(FormsAuthentication.DefaultUrl);
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}

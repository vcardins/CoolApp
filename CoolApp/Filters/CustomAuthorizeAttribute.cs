#region credits
// ***********************************************************************
// Assembly	: TaskForceManager
// Author	: Rod Johnson
// Created	: 03-27-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Web.Mvc;
using CoolApp.Extensions.ActionDescriptors;

namespace CoolApp.Filters
{
    #region

    

    #endregion

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;

            // If the action or controller has AllowAnonymous or OnlyAnonymous Attributes should let them pass
            if (actionDescriptor.HasAttribute<AllowAnonymousAttribute>()) return;

            if (actionDescriptor.HasAttribute<OnlyAnonymousAttribute>() &&
                !filterContext.RequestContext.HttpContext.Request.IsAuthenticated)
                return;

            base.OnAuthorization(filterContext);
        }
    }
}
#region credits
// ***********************************************************************
// Assembly	: TaskForceManager
// Author	: Rod Johnson
// Created	: 03-17-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Web.Mvc;

namespace TaskForceManager.Extensions.UrlHelpers
{
    #region

    

    #endregion

    public static partial class UrlExtensions
    {
        public static string AbsoluteAction(this UrlHelper url, string actionName, string controllerName, object routeValues = null)
        {
            if (url.RequestContext.HttpContext.Request.Url != null)
            {
                string scheme = url.RequestContext.HttpContext.Request.Url.Scheme;
                return url.Action(actionName, controllerName, routeValues, scheme);
            }
            return null;
        }
       
    }
}
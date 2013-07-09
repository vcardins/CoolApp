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

namespace TaskForceManager.Extensions.HtmlHelpers
{
    #region

    

    #endregion

    public static partial class HtmlExtensions
    {
        public static string ViewName(this HtmlHelper htmlHelper)
        {

            var context = htmlHelper.ViewContext.HttpContext;
            var routeData = htmlHelper.RouteCollection.GetRouteData(context);
            return routeData != null ? routeData.Values["Action"].ToString() : string.Empty;
        }

    }
}
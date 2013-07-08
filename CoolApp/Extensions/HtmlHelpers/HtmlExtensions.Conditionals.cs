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

    public static partial class BootstrapHelpers
    {
        /// <summary>
        /// Actives the when.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="extraClasses">The extra classes.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString ActiveWhen(this HtmlHelper helper, string actionName, string controllerName, string extraClasses = "")
        {
            var currentActionName = helper.ViewContext.RouteData.Values["action"].ToString();
            var currentControllerName = helper.ViewContext.RouteData.Values["controller"].ToString();

            if ((controllerName.ToLower() == currentControllerName.ToLower()) &&
                (actionName.ToLower() == currentActionName.ToLower()))
                return new MvcHtmlString("class = 'active " + extraClasses + "'");

            return new MvcHtmlString("");
        }
    }
}
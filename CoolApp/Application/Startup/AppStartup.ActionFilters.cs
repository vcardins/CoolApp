#region credits
// ***********************************************************************
// Assembly	: TaskForceManager
// Author	: Rod Johnson
// Created	: 03-09-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion
#region

using System.Web.Mvc;
using CoolApp.Application.Startup;
using CoolApp.Filters;

#endregion

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppStartup), "ActionFilters")]

namespace CoolApp.Application.Startup
{
    #region

    

    #endregion

    public partial class AppStartup
    {
        /// <summary>
        /// Actions the filters.
        /// </summary>
        public static void ActionFilters()
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            GlobalFilters.Filters.Add(new CustomAuthorizeAttribute());
        }
    }
}
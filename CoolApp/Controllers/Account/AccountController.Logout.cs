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

namespace CoolApp.Controllers.Account
{
    #region

    

    #endregion

    /// <summary>
    /// Class AccountController
    /// </summary>
    public partial class AccountController
    {
        /// <summary>
        /// Logout the existing user.
        /// </summary>
        /// <returns>
        /// ActionResult.
        /// </returns>
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            if (Request.IsAuthenticated)
            {
                _authService.SignOut();  
                Session.Abandon();
            }

            return RedirectPermanent("~/");
                      
        }       
    }
}
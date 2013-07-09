#region credits
// ***********************************************************************
// Assembly	: TaskForceManager
// Author	: Rod Johnson
// Created	: 03-15-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Web.Mvc;
using CoolApp.Extensions.ModelState;
using CoolApp.Extensions.TempData;
using CoolApp.Filters;
using CoolApp.Models.Account;

namespace CoolApp.Controllers.Account
{

    #region

    #endregion

    /// <summary>
    /// Class AccountController
    /// </summary>
    /// 
    [MultiResponseFormat]
    public partial class AccountController
    {
        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var loginModel = new LoginModel {ReturnUrl = returnUrl};
            return View(loginModel);
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var result = _authService.Authenticate(model.UserName, model.Password);
                if (ModelState.Process(result))
                {
                    _authService.SignIn(model.UserName, model.RememberMe, model.OffsetTimeZone);
                    
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                TempData.AddErrorMessage("Invalid Username or Password");
            }
            return View(model);
        }
    }
}
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
using CoolApp.Core.Interfaces.Service;

namespace CoolApp.Controllers.Account
{
    #region

    

    #endregion

    public partial class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="authService">The auth service.</param>
        public AccountController(IUserService userService, IAuthenticationService authService)
        {
            _userService = userService;
            _authService = authService;
        }

    }
}
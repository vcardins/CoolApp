using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CoolApp.Common.Crypto;
using CoolApp.Core.Extensions;
using CoolApp.Core.Interfaces.Service;
using CoolApp.Core.Interfaces.Validation;
using CoolApp.Core.Models;

namespace CoolApp.Infraestructure.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _loginUrl = FormsAuthentication.LoginUrl;

        private readonly string _authCookieName = FormsAuthentication.FormsCookieName;

        public bool IsAuthenticated()
        {
            return HttpContext.Current.Request.Cookies.AllKeys.Contains(_authCookieName);
        }

        public virtual IValidationContainer<User> Authenticate(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username)) throw new ArgumentException("username");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentException("password");

            var userService = DependencyResolver.Current.GetService<IUserService>();

            var account = userService.GetByUsername(username);
            var container = new User().GetValidationContainer();
            if (account == null)
            {
                container.ValidationErrors.Add("", new List<string> { "User not found" });
                return container;
            }

            container = account.GetValidationContainer();
            if (!container.IsValid)
                return container;

            var isValid = VerifyHashedPassword(password, account.HashedPassword);

            if (!isValid)
            {
                container.ValidationErrors.Add("", new List<string> {"Unable to authenticate user"});
            }
            else
            {
                account.LastLogin = DateTime.UtcNow;
                account.FailedLoginCount = 0;
                userService.SaveOrUpdate(account);
            }

            return container;

        }

        internal bool VerifyHashedPassword(string password, string hashedPassword)
        {
            return CryptoHelper.VerifyHashedPassword(hashedPassword, password);
        }

        public void SignIn(string username, bool isPersistant, double? offsetTimeZone)
        {
            FormsAuthentication.SetAuthCookie(username, isPersistant);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public void RedirectToLoginPage()
        {
            if (HttpContext.Current.Request.Path == _loginUrl)
            {
                return;
            }

            HttpContext.Current.Response.Redirect(_loginUrl);
        }

        public string GetRedirectUrl(string username, bool createPersistentCookie)
        {
            return FormsAuthentication.GetRedirectUrl(username, createPersistentCookie);
        }
    }
}
#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Infrastructure
// Author	: Rod Johnson
// Created	: 03-19-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Threading;
using System.Web;
using System.Web.Mvc;
using CoolApp.Core.Interfaces.Service;
using CoolApp.Core.Models;

namespace CoolApp.Infraestructure.Profiles
{
    #region

    #endregion

    public class UserProfile
    {
        public static User Current
        {
            get
            {                
                var user = HttpContext.Current.Session["UserProfile"] as User;
                if (user == null)
                {
                    HttpContext.Current.Session["UserProfile"] = user =
                            DependencyResolver.Current.GetService<IUserService>()
                                              .GetByUsername(Thread.CurrentPrincipal.Identity.Name);
                }
                return user;
            }
        }

        public static double TimeZoneOffset
        {
            get
            {
                return Helpers.Common.TimeZoneOffset != null ? Helpers.Common.TimeZoneOffset.Value : 0;
            }
            set
            {
                Helpers.Common.TimeZoneOffset = value;
            }
        }
    }
}
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

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using TaskForceManager.Core.Interfaces.Site;

namespace TaskForceManager.Extensions.HtmlHelpers
{
    #region

    

    #endregion

    public static partial class HtmlExtensions
    {
        /// <summary>
        /// Gets the page heading text.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="title">The title.</param>
        /// <returns>System.String.</returns>
        public static string GetPageHeadingText(this HtmlHelper helper, string title = null)
        {
            var rules = new List<Func<string>>
                {
                    () => title,
                    () => helper.ViewBag.Title,
                    () =>
                        {
                          if (SiteMap.CurrentNode != null)
                          {
                              return SiteMap.CurrentNode.Title;
                          }
                          return null;
                        }
                };

            foreach (var rule in rules)
            {
                title = rule();
                if (!string.IsNullOrEmpty(title))
                    break;
            }

            return title;
        }

        /// <summary>
        /// Gets the website title.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns>System.String.</returns>
        public static string GetWebsiteTitle(this HtmlHelper helper)
        {
            var settings = DependencyResolver.Current.GetService<ISiteSettings>();
            return settings.WebsiteName;
        }

        /// <summary>
        /// Gets the page title.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns>System.String.</returns>
        public static string GetPageTitle(this HtmlHelper helper)
        {            
            return GetWebsiteTitle(helper) + " - " + GetPageHeadingText(helper);
        }
    }
}
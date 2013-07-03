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
using TaskForceManager.Infrastructure.Photos;

namespace TaskForceManager.Extensions.UrlHelpers
{
    #region

    

    #endregion

    public static partial class UrlExtensions
    {
        public static string GetPhotoUrl(this UrlHelper helper, string imageId, string size, string defaultUrl = null)
        {
            var photo = PhotoManager.Provider.GetPhotoResize(imageId, size);
            if (photo != null)
            {
                return helper.Content(photo.Url);
            }
            return defaultUrl != null ? helper.Content(defaultUrl) : "";
        }
    }
}
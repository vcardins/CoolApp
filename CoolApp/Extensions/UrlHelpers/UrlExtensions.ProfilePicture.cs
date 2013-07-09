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
using TaskForceManager.Core.Enums;
using TaskForceManager.Core.Model;

namespace TaskForceManager.Extensions.UrlHelpers
{
    #region

    

    #endregion

    public static partial class UrlExtensions
    {
        public static string ProfilePicture(this UrlHelper url, string photoId, string resize, Gender gender)
        {            
            string defaultUrl = string.Format("~/Content/images/{0}/{1}.jpg", resize, gender.ToString());

            var photoUrl = GetPhotoUrl(url, photoId, resize, defaultUrl);
            var img = new TagBuilder("img");
            img.MergeAttribute("src", photoUrl);
            img.MergeAttribute("class", "img-polaroid");
            return img.ToString(TagRenderMode.SelfClosing);
        }
      
    }
}
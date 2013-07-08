#region credits
// ***********************************************************************
// Assembly	: Deten
// Author	: Momentum
// Created	: 03-17-2013
// 
// Last Modified By : Momentum
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Web.Mvc;

namespace Deten.Web.Extensions.StringHelpers
{
    #region

    

    #endregion

    public static class StringExtensions
    {
        public static MvcHtmlString ToMvcHtmlString(this string value)
        {
            return new MvcHtmlString(value);
        }
    }
}
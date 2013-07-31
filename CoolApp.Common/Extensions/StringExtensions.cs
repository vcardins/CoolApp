#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Common
// Author	: Rod Johnson
// Created	: 03-23-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion
#region



#endregion

using System;
using System.Linq;
using System.Web;

namespace CoolApp.Common.Extensions
{
    #region

    

    #endregion

    public static class StringExtensions
    {
        public static string GetQueryString(this object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
    }
}


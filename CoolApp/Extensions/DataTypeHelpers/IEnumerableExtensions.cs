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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace TaskForceManager.Extensions.DataTypeHelpers
{
    #region

    

    #endregion

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Fors the enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the T enum.</typeparam>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns>SelectList.</returns>
        public static SelectList ForEnum<TEnum>(TEnum enumType)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = value.ToString(),
                                                    Value = value.ToString()
                                                };
            return new SelectList(items);
        }

        public static IEnumerable<SelectListItem> ForEnum(Type enumType)
        {
            return ForEnum(enumType, string.Empty);
        }

        public static IEnumerable<SelectListItem> ForEnum(Type enumType, string initialText)
        {
            return ForEnum(enumType, initialText, string.Empty);
        }

        public static IEnumerable<SelectListItem> ForEnum(Type enumType, string initialText, string selectedItem)
        {
            ICollection<SelectListItem> items = new List<SelectListItem>();


            foreach (var value in Enum.GetValues(enumType))
            {
                var memInfo = enumType.GetMember(value.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = attributes.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : value.ToString();

                items.Add(new SelectListItem
                {
                    Text = description ?? value.ToString(),
                    Value = ((int)value).ToString(CultureInfo.InvariantCulture),
                    Selected = value.ToString().Equals(selectedItem)
                });
            }

            return items;
        }
    }
}
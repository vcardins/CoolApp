#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Core
// Author	: Rod Johnson
// Created	: 03-23-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CoolApp.Core.Common.Validation;
using CoolApp.Core.Interfaces.Validation;
using CoolApp.Core.Models;

namespace CoolApp.Core.Extensions
{
    #region

    

    #endregion

    public static class ValidationEngine
    {
        /// <summary>
        /// Will validate an entity that implements IValidatableObject and DataAnnotations
        /// </summary>
        /// <typeparam name="T">The type that inherits the abstract basetype DomainObject</typeparam>
        /// <param name="entity">The Entity to validate</param>
        /// <returns></returns>
        public static IValidationContainer<T> GetValidationContainer<T>(this T entity) where T : DomainObject
        {
            var brokenrules = new Dictionary<string, IList<string>>();

            // IValidatableObject
            var customErrors = entity.Validate(new ValidationContext(entity, null, null));
            if (customErrors != null)
            {
                foreach (var customError in customErrors)
                {
                    if (customError == null)
                    {
                        continue;
                    }

                    foreach (var memberName in customError.MemberNames)
                    {
                        if (!brokenrules.ContainsKey(memberName))
                        {
                            brokenrules.Add(memberName, new List<string>());
                        }

                        brokenrules[memberName].Add(customError.ErrorMessage);
                    }
                }
            }

            // DataAnnotations
            foreach (var pi in entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                foreach (
                    var attribute in (ValidationAttribute[])pi.GetCustomAttributes(typeof(ValidationAttribute), false))
                {
                    if (attribute.IsValid(pi.GetValue(entity, null)))
                    {
                        continue;
                    }

                    if (!brokenrules.ContainsKey(pi.Name))
                    {
                        brokenrules.Add(pi.Name, new List<string>());
                    }

                    brokenrules[pi.Name].Add(attribute.FormatErrorMessage(pi.Name));
                }
            }

            return new ValidationContainer<T>(brokenrules, entity);
        }
    }
}
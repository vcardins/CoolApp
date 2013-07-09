#region credits
// ***********************************************************************
// Assembly	: Deten
// Author	: Momentum
// Created	: 03-17-2013
// 
// Last Modified By : Momentum
// Last Modified On : 03-21-2013
// ***********************************************************************
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CoolApp.Core.Interfaces.Validation;

namespace CoolApp.Extensions.ModelState
{
    #region

    

    #endregion

    public static class ModelStateExtensions
    {
        /// <summary>
        /// Processes the specified model state.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool Process(this ModelStateDictionary modelState, IValidationContainer result)
        {
            foreach (var r in result.ValidationErrors)
                foreach (var e in r.Value)
                    modelState.AddModelError(r.Key, e);
            return modelState.IsValid;
        }

        public static Dictionary<string, IList<string>> Errors(this ModelStateDictionary modelState)
        {
            var errors = new Dictionary<string, IList<string>>();
            foreach (var key in modelState.Keys)
            {
                if (modelState[key].Errors.Count > 0)
                {
                    errors.Add(key, modelState[key].Errors.Select(e => e.ErrorMessage).ToList());
                }
            }
            return errors;
        }
            

        /// <summary>
        /// Processes the specified model state.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool Process(this System.Web.Http.ModelBinding.ModelStateDictionary modelState,
            IValidationContainer result)
        {
            foreach (var r in result.ValidationErrors)
                foreach (var e in r.Value)
                    modelState.AddModelError(r.Key, e);
            return modelState.IsValid;
        }
    }
}
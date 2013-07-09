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

namespace TaskForceManager.Extensions.ModelMetadataHelpers
{
    #region

    

    #endregion

    public static partial class ModelMetadataExtensions
    {
        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns>BootstrapInputOptions.</returns>
        public static HtmlHelpers.BootstrapInputOptions GetOptions(this ModelMetadata metadata)
        {
            object options;

            metadata.AdditionalValues.TryGetValue("InputOptions", out options);

            return (HtmlHelpers.BootstrapInputOptions)options;
        }
    }
}
#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Core
// Author	: Rod Johnson
// Created	: 03-16-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Configuration;

namespace CoolApp.Core.Interfaces.Notifications
{
    #region

    

    #endregion
    
    public interface INotificationProvider
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty("name")]
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [ConfigurationProperty("version")]
        string Version { get; set; }

        /// <summary>
        /// Gets or sets the appKey.
        /// </summary>
        [ConfigurationProperty("appKey")]
        string AppKey { get; set; }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        [ConfigurationProperty("baseURL")]
        string BaseURL { get; set; }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        [ConfigurationProperty("loginMethodUrl")]
        string LoginMethodUrl { get; set; }

        /// <summary>
        /// Gets or sets the auth User.
        /// </summary>
        [ConfigurationProperty("authUser")]
        string AuthUser { get; set; }

        /// <summary>
        /// Gets or sets the auth Password.
        /// </summary>
        [ConfigurationProperty("authPassword")]
        string AuthPassword { get; set; }

        /// <summary>
        /// Gets or sets the authUser.
        /// </summary>
        [ConfigurationProperty("defaultSound")]
        string DefaultSound { get; set; }
    }
}



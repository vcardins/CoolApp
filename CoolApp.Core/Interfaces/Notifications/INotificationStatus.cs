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
    
    public interface INotificationStatus
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty("name")]
        string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty("id")]
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [ConfigurationProperty("sound")]
        string Sound { get; set; }

        /// <summary>
        /// Gets or sets the appKey.
        /// </summary>
        [ConfigurationProperty("icon")]
        string Icon { get; set; }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        [ConfigurationProperty("vibrate")]
        bool Vibrate { get; set; }

    }
}



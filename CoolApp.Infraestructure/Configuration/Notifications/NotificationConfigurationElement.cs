using System.Configuration;
using CoolApp.Core.Interfaces.Notifications;

namespace CoolApp.Infrastructure.Configuration.Notifications
{
    /// <summary>
    /// The photo section.
    /// </summary>
    public class NotificationConfigurationElement : ConfigurationElement, INotificationSettings
    {
       
        /// <summary>
        /// Gets or sets the default provider.
        /// </summary>
        //[StringValidator(MinLength = 1)]
        [ConfigurationProperty("defaultProvider")]
        public string DefaultProvider
        {
            get
            {
                return (string)base["defaultProvider"];
            }

            set
            {
                base["defaultProvider"] = value;
            }
        }
        
        /// <summary>
        /// Gets the Providers.
        /// </summary>
        [ConfigurationProperty("Provider")]
        public NotificationProviderCollection Providers
        {
            get
            {
                return (NotificationProviderCollection)base["Provider"];
            }
        }

        /// <summary>
        /// Gets the Providers.
        /// </summary>
        [ConfigurationProperty("Status")]
        public NotificationStatusCollection Statuses
        {
            get
            {
                return (NotificationStatusCollection)base["Status"];
            }
        }

        /// <summary>
        /// Gets the Providers.
        /// </summary>
        [ConfigurationProperty("Error")]
        public NotificationErrorCollection Errors
        {
            get
            {
                return (NotificationErrorCollection)base["Error"];
            }
        }
    }
}
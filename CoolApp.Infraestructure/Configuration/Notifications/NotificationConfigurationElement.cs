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
        [StringValidator(MinLength = 1)]
        [ConfigurationProperty("defaultProvider", DefaultValue = "appcelerator")]
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
    }
}
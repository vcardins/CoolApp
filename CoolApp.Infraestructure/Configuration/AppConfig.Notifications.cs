using System.Configuration;
using CoolApp.Infrastructure.Configuration.Notifications;

namespace CoolApp.Infrastructure.Configuration
{
    public partial class AppConfig
    {
        [ConfigurationProperty("notifications", IsRequired = true)]
        public NotificationConfigurationElement Notifications
        {
            get
            {
                return (NotificationConfigurationElement)base["notifications"];
            }
        }
    }
}

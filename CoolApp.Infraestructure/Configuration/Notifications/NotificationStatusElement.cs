using System.Configuration;
using CoolApp.Core.Interfaces.Notifications;

namespace CoolApp.Infrastructure.Configuration.Notifications
{
    /// <summary>
    /// The photo resize element.
    /// </summary>
    public class NotificationStatusElement : ConfigurationElement, INotificationStatus
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }

            set
            {
                base["name"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty("id")]
        public int Id
        {
            get
            {
                return (int)base["id"];
            }

            set
            {
                base["id"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("sound")]
        public string Sound
        {
            get
            {
                return (string)base["sound"];
            }

            set
            {
                base["sound"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("icon")]
        public string Icon
        {
            get
            {
                return (string)base["icon"];
            }

            set
            {
                base["icon"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("vibrate")]
        public bool Vibrate
        {
            get
            {
                return (bool)base["vibrate"];
            }

            set
            {
                base["vibrate"] = value;
            }
        }

       
    }
}
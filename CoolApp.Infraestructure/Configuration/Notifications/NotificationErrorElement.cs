using System.Configuration;
using CoolApp.Core.Interfaces.Notifications;

namespace CoolApp.Infrastructure.Configuration.Notifications
{
    /// <summary>
    /// The notification error element.
    /// </summary>
    public class NotificationErrorElement : ConfigurationElement, INotificationError
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
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
        /// Gets or sets the error value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [ConfigurationProperty("message")]
        public string Message
        {
            get
            {
                return (string)base["message"];
            }

            set
            {
                base["message"] = value;
            }
        }
       
    }
}
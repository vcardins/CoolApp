using System.Configuration;
using CoolApp.Core.Interfaces.Notifications;

namespace CoolApp.Infrastructure.Configuration.Notifications
{
    /// <summary>
    /// The photo resize element.
    /// </summary>
    public class NotificationProviderElement : ConfigurationElement, INotificationProvider
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
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("version")]
        public string Version
        {
            get
            {
                return (string)base["version"];
            }

            set
            {
                base["version"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("appKey")]
        public string AppKey
        {
            get
            {
                return (string)base["appKey"];
            }

            set
            {
                base["appKey"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("baseURL")]
        public string BaseURL
        {
            get
            {
                return (string)base["baseURL"];
            }

            set
            {
                base["baseURL"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("loginMethodUrl")]
        public string LoginMethodUrl
        {
            get
            {
                return (string)base["loginMethodUrl"];
            }

            set
            {
                base["loginMethodUrl"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("authUser")]
        public string AuthUser
        {
            get
            {
                return (string)base["authUser"];
            }

            set
            {
                base["authUser"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("authPassword")]
        public string AuthPassword
        {
            get
            {
                return (string)base["authPassword"];
            }

            set
            {
                base["authPassword"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [ConfigurationProperty("defaultSound")]
        public string DefaultSound
        {
            get
            {
                return (string)base["defaultSound"];
            }

            set
            {
                base["defaultSound"] = value;
            }
        }

       
    }
}
using System;
using System.Configuration;

namespace CoolApp.Infrastructure.Configuration.Notifications
{
    /// <summary>
    /// The photo resize collection.
    /// </summary>
    [ConfigurationCollection(typeof(NotificationProviderElement))]
    public class NotificationProviderCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// The property name.
        /// </summary>
        internal const string PropertyName = "add";

        /// <summary>
        /// The is element name.
        /// </summary>
        /// <param name="elementName">
        /// The element name.
        /// </param>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets the element name.
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        public void Add(ProviderSettings provider)
        {
            if (provider != null)
            {
                BaseAdd(provider);
            }
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// The create new element.
        /// </summary>
        /// <returns>
        /// The System.Configuration.ConfigurationElement.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new NotificationProviderElement();
        }

        /// <summary>
        /// The get element key.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The System.Object.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (NotificationProviderElement)element;
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The JamesRocks.Photos.NotificationProviderElement.
        /// </returns>
        public new NotificationProviderElement this[string key]
        {
            get
            {
                return (NotificationProviderElement)BaseGet(key);
            }
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The JamesRocks.Photos.NotificationProviderElement.
        /// </returns>
        public NotificationProviderElement this[int index]
        {
            get
            {
                return (NotificationProviderElement)BaseGet(index);
            }

            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }
    }
}
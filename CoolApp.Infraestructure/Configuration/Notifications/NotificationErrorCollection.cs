using System;
using System.Configuration;

namespace CoolApp.Infrastructure.Configuration.Notifications
{
    /// <summary>
    /// The notification Message collection.
    /// </summary>
    [ConfigurationCollection(typeof(NotificationErrorElement))]
    public class NotificationErrorCollection : ConfigurationElementCollection
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
        /// <param name="Message">
        /// The Message.
        /// </param>
        public void Add(ProviderSettings Message)
        {
            if (Message != null)
            {
                BaseAdd(Message);
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
            return new NotificationErrorElement();
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
            var notificationErrorElement = element as NotificationErrorElement;
            return notificationErrorElement.Name;
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The JamesRocks.Photos.NotificationErrorElement.
        /// </returns>
        public new NotificationErrorElement this[string key]
        {
            get { return BaseGet(key) as NotificationErrorElement; }
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The JamesRocks.Photos.NotificationErrorElement.
        /// </returns>
        public NotificationErrorElement this[int index]
        {
            get { return BaseGet(index) as NotificationErrorElement; }

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
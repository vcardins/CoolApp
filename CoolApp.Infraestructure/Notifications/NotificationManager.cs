#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Infrastructure
// Author	: Rod Johnson
// Created	: 03-16-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using CoolApp.Infrastructure.Configuration;
using CoolApp.Infrastructure.Configuration.Notifications;

namespace CoolApp.Infrastructure.Notifications
{
    #region

    

    #endregion

    /// <summary>
    /// The photo manager.
    /// </summary>
    public class NotificationManager
    {
        #region Provider-specific code

        /// <summary>
        /// The lock.
        /// </summary>
        private static readonly object Lock = new object();

        /// <summary>
        /// The _provider.
        /// </summary>
        private static NotificationProviderElement _provider;

        /// <summary>
        /// The _providers.
        /// </summary>
        private static NotificationProviderCollection _providers;

        /// <summary>
        /// The _photo resize.
        /// </summary>
        private static IDictionary<string, NotificationProviderElement> _notificationProvider;

        /// <summary>
        /// Gets the provider.
        /// </summary>
        public static NotificationProviderElement Default
        {
            get
            {
                var section = AppConfig.Instance.Notifications;

                foreach (NotificationProviderElement p in from NotificationProviderElement p in section.Providers where p.Name == section.DefaultProvider select p)
                {
                    _provider = p;
                }
                return _provider;
            }
        }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        public static NotificationProviderElement Provider
        {
            get
            {
                LoadProviders();
                return _provider;
            }
        }

        /// <summary>
        /// Gets the providers.
        /// </summary>
        public static NotificationProviderCollection Providers
        {
            get
            {
                LoadProviders();
                return _providers;
            }
        }

        /// <summary>
        /// Gets the photo resizes.
        /// </summary>
        public static IDictionary<string, NotificationProviderElement> NotificationProviders
        {
            get
            {
                LoadProviders();
                return _notificationProvider;
            }
        }

        /// <summary>
        /// The load providers.
        /// </summary>
        /// <exception cref="ProviderException">
        /// </exception>
        private static void LoadProviders()
        {
            if (_provider == null)
            {
                lock (Lock)
                {
                    var section = AppConfig.Instance.Notifications;

                    _providers = new NotificationProviderCollection();
                    _provider = _providers[section.DefaultProvider];

                    _notificationProvider = new Dictionary<string, NotificationProviderElement>();

                    foreach (NotificationProviderElement p in section.Providers)
                    {
                        _notificationProvider.Add(p.Name, p);
                    }

                    if (_provider == null)
                    {
                        throw new ProviderException("Unable to load default FileSystemProvider");
                    }
                }
            }
        }

        #endregion

    }
}
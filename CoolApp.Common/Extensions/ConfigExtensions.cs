using System;
using System.Collections.Specialized;
using System.Configuration;

// ReSharper disable CheckNamespace
namespace CoolApp.Common.Extensions
{
    public static class NameValueCollectionEx
// ReSharper restore CheckNamespace
    {
        public static T GetAndRemove<T>(this NameValueCollection config, string propertyName, bool required) where T : IConvertible
        {
            string value = config.Get(propertyName);
            if (required && string.IsNullOrEmpty(value))
                throw new ConfigurationErrorsException(string.Format("Expected attribute {0}, which is missing or empty.", propertyName));
            config.Remove(propertyName);
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
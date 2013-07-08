using System;
using System.Web.Caching;

namespace CoolApp.Infraestructure.Helpers
{
	public static class CacheHelper
	{
		static readonly object Sync = new object();

		/// <summary>
		/// Insert value into the cache using
		/// appropriate name/value pairs (no sliding expiration)
		/// </summary>
		/// <typeparam name="T">Type of cached item</typeparam>
		/// <param name="cache"></param>
		/// <param name="o">Item to be cached</param>
		/// <param name="key">Name of item</param>
		public static void Add<T>(this Cache cache, T o, string key)
		{
			Add<T>(cache, o, key, 60);
		}

		/// <summary>
		/// Insert value into the cache using
		/// appropriate name/value pairs (no sliding expiration)
		/// </summary>
		/// <typeparam name="T">Type of cached item</typeparam>
		/// <param name="cache"></param>
		/// <param name="o">Item to be cached</param>
		/// <param name="key">Name of item</param>
		/// <param name="timeOutMinutes">Cache timout in minutes</param>
		public static void Add<T>(this Cache cache, T o, string key, int timeOutMinutes)
		{
			cache.Insert(
				key,
				o,
				null,
				DateTime.Now.AddMinutes(timeOutMinutes),
				Cache.NoSlidingExpiration);
		}

		/// <summary>
		/// Remove item from cache
		/// </summary>
		/// <param name="cache"></param>
		/// <param name="key">Name of cached item</param>
		public static void Clear(this Cache cache, string key)
		{
			cache.Remove(key);
		}

		/// <summary>
		/// Check for item in cache
		/// </summary>
		/// <param name="cache"></param>
		/// <param name="key">Name of cached item</param>
		/// <returns></returns>
		public static bool Exists(this Cache cache, string key)
		{
			return cache[key] != null;
		}

		/// <summary>
		/// Retrieve cached item
		/// </summary>
		/// <typeparam name="T">Type of cached item</typeparam>
		/// <param name="cache"></param>
		/// <param name="key">Name of cached item</param>
		/// <param name="value">Cached value. Default(T) if
		/// item doesn't exist.</param>
		/// <returns>Cached item as type</returns>
		public static bool Get<T>(this Cache cache, string key, out T value)
		{
			try
			{
				if(!Exists(cache, key))
				{
					value = default(T);
					return false;
				}

				value = (T)cache[key];
			}
			catch
			{
				value = default(T);
				return false;
			}

			return true;
		}

		/// <summary>
		/// Executes a method and stores the result in cache using the given cache key. If the data already exists in cache, it returns the data
		/// and doesn't execute the method.  Thread safe, although the method parameter isn't guaranteed to be thread safe.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cache"></param>
		/// <param name="cacheKey">Each method has it's own isolated set of cache items, so cacheKeys won't overlap accross methods.</param>
		/// <param name="method"></param>
		/// <param name="expirationMinutes">Lifetime of cache items, in minutes</param>
		/// <returns></returns>
		public static T Data<T>(this Cache cache, string cacheKey,
			int expirationMinutes, Func<T> method) where T : class
		{
			//var hash = method.GetHashCode().ToString();
			var data = (T)cache[cacheKey]; //[hash + cacheKey]
			if(data == null)
			{
				data = method();

				if(expirationMinutes > 0 && data != null)
					lock(Sync)
					{
						//hash + cacheKey
						cache.Insert(cacheKey, data, null, DateTime.Now.AddMinutes
							(expirationMinutes), Cache.NoSlidingExpiration);
					}
			}

			return data;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace GMKT.GMobile.Biz
{
	public static class CacheHelper
	{
		private static string NullCacheValueString = "F33EBD9C-B20C-4975-9D21-63EA71ED9843";
		private static readonly Object _locker = new object();

		public static bool IsExist(string methodName, string keyName)
		{
			if (String.IsNullOrWhiteSpace(methodName)) throw new ArgumentException("Invalid cache methodName");
			if (String.IsNullOrWhiteSpace(keyName)) throw new ArgumentException("Invalid cache keyName");

			string key = methodName + keyName;

			return MemoryCache.Default.Contains(key);
		}

		public static object GetCacheItem(string methodName, string keyName)
		{
			if (String.IsNullOrWhiteSpace(methodName)) throw new ArgumentException("Invalid cache methodName");
			if (String.IsNullOrWhiteSpace(keyName)) throw new ArgumentException("Invalid cache keyName");

			string key = methodName + keyName;

			if (MemoryCache.Default[key] != null)
			{
				if (MemoryCache.Default[key].GetType() == typeof(string))
				{
					string cacheVal = MemoryCache.Default[key].ToString();
					if (cacheVal == NullCacheValueString)
						return null;
				}

				return MemoryCache.Default[key];
			}
			return null;
		}

		public static void SetCacheItem(string methodName, string keyName, object cacheItem, int cacheSeconds)
		{
			if (String.IsNullOrWhiteSpace(methodName)) throw new ArgumentException("Invalid cache methodName");
			if (String.IsNullOrWhiteSpace(keyName)) throw new ArgumentException("Invalid cache keyName");

			string key = methodName + keyName;

			if (MemoryCache.Default[key] == null)
			{
				lock (_locker)
				{
					if (MemoryCache.Default[key] == null)
					{
						CacheItem item = new CacheItem(key, cacheItem);
						CacheItemPolicy policy = CreatePolicy(cacheSeconds);

						if (cacheItem == null)
						{
							// null 인 경우 NullCacheValueString 을 넣는다. 그렇지 않으면 오류남
							item.Value = NullCacheValueString;
						}
						MemoryCache.Default.Add(item, policy);

					}
				}
			}
		}

		private static CacheItemPolicy CreatePolicy(int cacheSecond)
		{
			CacheItemPolicy policy = new CacheItemPolicy();

			policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(cacheSecond);
			policy.Priority = CacheItemPriority.Default;

			return policy;
		}
	}
}

using System;
using Common.Enums;
using Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using Utils.ExtensionMethods;

namespace Common.Implementation
{
    /// <summary>
    /// Distributed Memory Cache Provider
    /// </summary>
    public class MemoryCacheProvider : ICacheProvider
    {
        private const int ExpirationTime = 10;

        private IMemoryCache Cache;

        private static object _lock = new object();
        private const string CacheControllKey = "_cacheControllKey";
        private ICacheService Service;
        private Dictionary<string, DateTime> ItemsLastModified;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="service"></param>
        public MemoryCacheProvider(IMemoryCache cache, ICacheService service)
        {

            Cache = cache;
            Service = service;
            ItemsLastModified = Service.Get()
                .ToDictionary(k => k.Name, v => DateTime.Now);

            SetCacheControll();
        }

        /// <summary>
        /// Gets value from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Get<T>(CacheItems item, Func<T> predicate)
            where T : class
        {
            Verify();
            var key = item.ToString();
            var result = Cache.Get(key) as T;
            
            if(result == null)
            {
                lock (_lock)
                {
                    result = predicate();
                    Cache.Set(key, result);
                    ItemsLastModified[key] = DateTime.Now;
                }
            }
            return result;
        }
        /// <summary>
        /// Invalidate cache item
        /// </summary>
        /// <param name="key"></param>
        public void Invalidate(CacheItems key)
        {
            Cache.Remove(key.ToString());
        }
        /// <summary>
        /// Invalidate all cache items
        /// </summary>
        public void Invalidate()
        {
            Enum<CacheItems>.Names.ToList().ForEach(key => Invalidate(key));
        }

        private void SetCacheControll()
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(ExpirationTime))
                .RegisterPostEvictionCallback(EvictionCallback);

            Cache.Set(CacheControllKey, new { }, cacheEntryOptions);
        }
        
        private void EvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            System.Diagnostics.Debug.WriteLine("exp time: " + DateTime.Now);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(ExpirationTime))
                .RegisterPostEvictionCallback(EvictionCallback);

            Verify();

            Cache.Set(CacheControllKey, new { }, cacheEntryOptions);
        }

        private void Verify()
        {
            var result = Cache.Get(CacheControllKey) as IEnumerable<ICacheItem>;
            if (result == null)
            {
                lock (_lock)
                {
                    Invalidate(Service.Get());
                }
            }
        }
        private void Invalidate(IEnumerable<ICacheItem> items)
        {
            items.Where(i => i.LastModifiedDate > ItemsLastModified[i.Name])
                .ToList()
                .ForEach(i => Cache.Remove(i.Name));
        }
        private void Invalidate(string key)
        {
            Cache.Remove(key);
        }
        private byte[] Serialize<T>(T value) 
        {
            var jsonValue = JsonConvert.SerializeObject(value);
            return Encoding.UTF8.GetBytes(jsonValue);
        }
        private T Deserialize<T>(byte[] value)
        {
            var jsonValue = Encoding.UTF8.GetString(value);
            return JsonConvert.DeserializeObject<T>(jsonValue);
        }
    }
}

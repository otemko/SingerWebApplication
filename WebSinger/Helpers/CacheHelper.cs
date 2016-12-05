using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace WebSinger.Helpers
{
    public static class CacheHelper
    {
        public static object Get(string cacheKey) 
        {
            object item = MemoryCache.Default.Get(cacheKey);
            return item == null ? null : item;
        }

        public static void Set<T>(string cacheKey, T value)
        {
            MemoryCache.Default.Add(cacheKey, value, DateTime.Now.AddMinutes(10));
        }

        public static void Change<T>(string cacheKey, T newValue)
        {
            MemoryCache.Default.Remove(cacheKey);
            MemoryCache.Default.Add(cacheKey, newValue, DateTime.Now.AddMinutes(10));
        }
    }
}
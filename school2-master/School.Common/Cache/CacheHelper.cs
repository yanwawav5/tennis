using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace school.Common.Cache
{
    public class CacheHelper
    {
        private static ICache _McCache = new WebCache();//暂时用本地缓存
        /// <summary>
        /// Mc缓存
        /// </summary>
        public static ICache Memcached
        {
            get
            {
                return _McCache;
            }
        }

        private static ICache _WebCache = new WebCache();
        /// <summary>
        /// IIS缓存
        /// </summary>
        public static ICache Cache
        {
            get
            {
                return _WebCache;
            }
        }
        //public static Task SetCache(string key, Object obj, int m)
        //{
        //    return Task.Factory.StartNew(() =>
        //    {
        //        HttpRuntime.Cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(m), CacheItemPriority.High, null);
        //    });
        //}
        //public static void InsertCache(String key, Object obj)
        //{
        //    HttpRuntime.Cache.Insert(key, obj);
        //}
        //public static Object GetCache(String key)
        //{
        //    return HttpRuntime.Cache.Get(key);
        //}
        //public static void RemoveCache(String key)
        //{
        //    HttpRuntime.Cache.Remove(key);
        //}
        //public static void UpdateCache(String key, Object o)
        //{
        //    HttpRuntime.Cache.Insert(key, o);
        //}
    }
}

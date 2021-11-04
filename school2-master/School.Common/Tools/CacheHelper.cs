using school.Common.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Common.Tools
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
    }
}

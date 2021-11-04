using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace school.Common
{
    /// <summary>
    /// web.config的配置信息
    /// </summary>
    public class Config
    {
        #region 旧
      

        #region 数据缓存时常

        private static int? _CacheExpires;
        /// <summary>
        /// 缓存时常
        /// </summary>
        public static int CacheExpires
        {
            get
            {
                try
                {
                    _CacheExpires = _CacheExpires ?? Convert.ToInt32(WebConfigurationManager.AppSettings["CacheExpires"].ToString());
                    return (int)_CacheExpires;
                }
                catch
                {
                    return 20;
                }

            }
        }

        #endregion

        #region 数据库连接

        private static String _ConnectionString;
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static String ConnectionString
        {
            get
            {
                try
                {
                    _ConnectionString = _ConnectionString ?? WebConfigurationManager.AppSettings["ConnectionString"].ToString();
                    return _ConnectionString;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region 数据分片

        private static int? _ShardingCount;
        /// <summary>
        /// 分片
        /// </summary>
        public static int ShardingCount
        {
            get
            {
                try
                {
                    _ShardingCount = _ShardingCount ?? Convert.ToInt32(WebConfigurationManager.AppSettings["ShardingCount"]);
                    return (int)_ShardingCount;
                }
                catch
                {
                    return 100;
                }
            }
        }

        #endregion

     

        #region 数据隔离级别
        private static IsolationLevel? _IsolationLevel = null;
        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public static IsolationLevel IsolationLevel
        {
            get
            {
                try
                {
                    _IsolationLevel = _IsolationLevel ?? (IsolationLevel)Convert.ToInt32(WebConfigurationManager.AppSettings["IsolationLevel"]);
                    return (IsolationLevel)_IsolationLevel;
                }
                catch
                {
                    return IsolationLevel.ReadCommitted;
                }
            }
        }
        #endregion



        #endregion

        #region
        private static String _WxAppKey;
        /// <summary>
        /// AppKey
        /// </summary>
        public static String WxAppKey
        {
            get
            {
                try
                {
                    _WxAppKey = _WxAppKey ?? WebConfigurationManager.AppSettings["WxAppKey"].ToString();

                    return _WxAppKey;
                }
                catch
                {
                    return null;
                }
            }
        }


        private static String _WxAppSecret;
        /// <summary>
        /// AppKey
        /// </summary>
        public static String WxAppSecret
        {
            get
            {
                try
                {
                    _WxAppSecret = _WxAppSecret ?? WebConfigurationManager.AppSettings["WxAppSecret"].ToString();

                    return _WxAppSecret;
                }
                catch
                {
                    return null;
                }
            }
        }






        private static String _WxSmallAppKey;
        /// <summary>
        /// AppKey
        /// </summary>
        public static String WxSmallAppKey
        {
            get
            {
                try
                {
                    _WxSmallAppKey = _WxSmallAppKey ?? WebConfigurationManager.AppSettings["WxSmallAppKey"].ToString();

                    return _WxSmallAppKey;
                }
                catch
                {
                    return null;
                }
            }
        }


        private static String _WxSmallAppSecret;
        /// <summary>
        /// AppKey
        /// </summary>
        public static String WxSmallAppSecret
        {
            get
            {
                try
                {
                    _WxSmallAppSecret = _WxSmallAppSecret ?? WebConfigurationManager.AppSettings["WxSmallAppSecret"].ToString();

                    return _WxSmallAppSecret;
                }
                catch
                {
                    return null;
                }
            }
        }





        private static String _WxSmallApiURL;
        /// <summary>
        /// AppKey
        /// </summary>
        public static String WxSmallApiURL
        {
            get
            {
                try
                {
                    _WxSmallApiURL = _WxSmallApiURL ?? WebConfigurationManager.AppSettings["WxSmallApiURL"].ToString();

                    return _WxSmallApiURL;
                }
                catch
                {
                    return null;
                }
            }
        }

        private static String _DomainName;
        /// <summary>
        /// 域名
        /// </summary>
        public static String DomainName
        {
            get
            {
                try
                {
                    _DomainName = _DomainName ?? WebConfigurationManager.AppSettings["DomainName"].ToString();

                    return _DomainName;
                }
                catch
                {
                    return null;
                }
            }
        }

        private static String _Token;
        /// <summary>
        /// Token
        /// </summary>
        public static String Token
        {
            get
            {
                try
                {
                    _Token = _Token ?? WebConfigurationManager.AppSettings["Token"].ToString();

                    return _Token;
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion
    }
}

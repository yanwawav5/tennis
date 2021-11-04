using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace school.Common.Tools
{
  public  class WebUtils
    {
        #region 获得当前绝对路径

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        #endregion

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.QueryString[strName];
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Form[strName];
        }

        /// <summary>
        /// XXX
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
            {
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }

        #region 获取并转换

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName);
            }
            else
            {
                return GetQueryString(strName);
            }
        }


        ///// <summary>
        ///// 获得指定Url参数的int类型值
        ///// </summary>
        ///// <param name="strName">Url参数</param>
        ///// <param name="defValue">缺省值</param>
        ///// <returns>Url参数的int类型值</returns>
        //public static int GetQueryInt(string strName, int defValue)
        //{
        //    return ConvertHelper.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        //}


        ///// <summary>
        ///// 获得指定表单参数的int类型值
        ///// </summary>
        ///// <param name="strName">表单参数</param>
        ///// <param name="defValue">缺省值</param>
        ///// <returns>表单参数的int类型值</returns>
        //public static int GetFormInt(string strName, int defValue)
        //{
        //    return ConvertHelper.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        //}

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        //public static int GetInt(string strName, int defValue)
        //{
        //    if (GetQueryInt(strName, defValue) == defValue)
        //    {
        //        return GetFormInt(strName, defValue);
        //    }
        //    else
        //    {
        //        return GetQueryInt(strName, defValue);
        //    }
        //}

        ///// <summary>
        ///// 获得指定Url参数的float类型值
        ///// </summary>
        ///// <param name="strName">Url参数</param>
        ///// <param name="defValue">缺省值</param>
        ///// <returns>Url参数的int类型值</returns>
        //public static float GetQueryFloat(string strName, float defValue)
        //{
        //    return ConvertHelper.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        //}


        ///// <summary>
        ///// 获得指定表单参数的float类型值
        ///// </summary>
        ///// <param name="strName">表单参数</param>
        ///// <param name="defValue">缺省值</param>
        ///// <returns>表单参数的float类型值</returns>
        //public static float GetFormFloat(string strName, float defValue)
        //{
        //    return ConvertHelper.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        //}

        /// <summary>
        /// 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        //public static float GetFloat(string strName, float defValue)
        //{
        //    if (GetQueryFloat(strName, defValue) == defValue)
        //    {
        //        return GetFormFloat(strName, defValue);
        //    }
        //    else
        //    {
        //        return GetQueryFloat(strName, defValue);
        //    }
        //}

        public static string HtmlDecode(object o)
        {
            if (o == null)
            {
                return null;
            }
            return HtmlDecode(o.ToString());
        }


        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 返回  字符串的HTML编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string HtmlEncode(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace(" ", "&nbsp;");
                str = str.Replace("'", "&#39;");
                str = str.Replace("\"", "&quot;");
                str = str.Replace("\r\n", "<br>");
                str = str.Replace("\n", "<br>");
            }
            return str;
        }

        #endregion

        //#region 获得当前页面客户端的IP

        ///// <summary>
        ///// 获得当前页面客户端的IP
        ///// </summary>
        ///// <returns>当前页面客户端的IP</returns>
        //public static string GetIp()
        //{
        //    string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (string.IsNullOrEmpty(result))
        //    {
        //        result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }

        //    if (string.IsNullOrEmpty(result))
        //    {
        //        result = HttpContext.Current.Request.UserHostAddress;
        //    }

        //    if (string.IsNullOrEmpty(result) || !ValidationHelper.IsIp(result))
        //    {
        //        return "0.0.0.0";
        //    }

        //    return result;
        //}

        //#endregion

        #region 获得当前页面的名称

        /// <summary>
        /// 获得当前页面的名称
        /// </summary>
        /// <returns>当前页面的名称</returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
            return urlArr[urlArr.Length - 1].ToLower();
        }

        #endregion

        #region cookie操作

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName] ?? new HttpCookie(strName);
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }


        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires"></param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName] ?? new HttpCookie(strName);
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddDays(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value;
            }

            return "";
        }

        /// <summary>
        /// 清除cookie
        /// </summary>
        /// <param name="name">name of cookie</param>
        public static void RemoveCookie(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie != null)
            {
                HttpContext.Current.Request.Cookies.Remove(name);
            }
        }

        #endregion

        #region Session操作

        /// <summary>
        /// 读Session值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>Session值</returns>
        public static string GetSession(string strName)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[strName] != null)
            {
                string value = HttpContext.Current.Session[strName].ToString();
                return value;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 写入Session值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>Session值</returns>
        public static void WriteSession(string strName, string value)
        {
            HttpContext.Current.Session[strName] = value;
        }

        #endregion

        #region 日期

        /// <summary>
        /// 返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
            {
                return replacestr;
            }

            if (datetimestr.Equals(""))
            {
                return replacestr;
            }

            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }


        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// 返回标准时间
        /// </summary>
        /// <param name="fDateTime"></param>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
            {
                return fDateTime;
            }
            DateTime s = Convert.ToDateTime(fDateTime);
            return s.ToString(formatStr);
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="fDateTime"></param>
        /// <returns></returns>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 验证是否是时间格式
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        /// <summary>
        /// 任意以数字,A-Z,_且3位数的字符
        /// </summary>
        /// <returns></returns>
        public static bool IsLetterOrNum(string str)
        {
            return Regex.IsMatch(str, @"^(\w{3,18})$");
        }

        #endregion

    }
}

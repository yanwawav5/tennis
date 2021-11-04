using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Yidu.Common.Tools
{
    public class SHA1Helper
    {

        /// <summary>
        /// 验证微信签名
        /// </summary>
        public static bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };

            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            var data = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(tmpStr));
            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }
            tmpStr = sb.ToString();
            tmpStr = tmpStr.ToLower();

            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 获取由SHA1加密的字符串
        public static string EncryptToSHA1(string str)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(str);
            byte[] str2 = sha1.ComputeHash(str1);
            sha1.Clear();
            (sha1 as IDisposable).Dispose();
            return Convert.ToBase64String(str2);
        }
        #endregion

        #region 获取由MD5加密的字符串
        public static string EncryptToMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(str);
            byte[] str2 = md5.ComputeHash(str1, 0, str1.Length);
            md5.Clear();
            (md5 as IDisposable).Dispose();
            return Convert.ToBase64String(str2);
        }
        #endregion

        #region [ 生成hmacsha1的散列 ]
        public static string HmacSha1(string word)
        {
            return BitConverter.ToString(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(word))).Replace("-", string.Empty);
        }
        #endregion

        #region  [ 字符串的字典序排序,区分大小写 ]
        public static string GetOrder(string[] ss)
        {
            var list = ss.OrderBy(x => x, StringComparer.Ordinal).ToArray();
            return string.Join("", list);
        }
        #endregion



        #region
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="jsapi_ticket">微信公众号调用微信JS临时票据</param>
        /// <param name="nonceStr">随机串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="url">当前网页URL</param>
        /// <returns></returns>
        public static string GetSignature(string jsapi_ticket, string nonceStr, long timestamp, string url)
        {

            var string1Builder = new StringBuilder();
            //注意这里参数名必须全部小写，且必须有序
            string1Builder.Append("jsapi_ticket=").Append(jsapi_ticket).Append("&")
                          .Append("noncestr=").Append(nonceStr).Append("&")
                          .Append("timestamp=").Append(timestamp).Append("&")
                          .Append("url=").Append(url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url);

            return Sha1(string1Builder.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 签名加密，使用SHA加密所得
        /// </summary>
        /// <param name="content">签名加密参数</param>
        /// <param name="encode">编码UTF-8</param>
        /// <returns></returns>
        public static string Sha1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytesIn = encode.GetBytes(content);
                byte[] bytesOut = sha1.ComputeHash(bytesIn);
                sha1.Dispose();
                string result = BitConverter.ToString(bytesOut);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }

        #endregion
    }
}

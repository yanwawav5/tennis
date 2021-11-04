using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net; 
using System.Text;
using System.Web;

namespace school.Api.WePay
{
    public class utility
    {
        /// <summary>
        /// 获得post过来的数据 
        /// </summary>
        /// <returns></returns>
        public static string getpoststr()
        { 
            Int32 intlen =Convert.ToInt32(HttpContext.Current.Request.InputStream.Length);
            byte[] b = new byte[intlen];
            HttpContext.Current.Request.InputStream.Read(b, 0, intlen);

            bool result = false;
            try
            {
                string fileName = @"F:\end\My.txt";
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(b, 0, b.Length);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }  

            return Encoding.UTF8.GetString(b);
        }

        /// <summary> 
        /// 模拟post提交 
        /// </summary> 
        /// <param name="url">请求地址</param> 
        /// <param name="xmlparam">xml参数</param> 
        /// <returns>返回结果</returns> 
        public static string posthttpresponse(string url, string xmlparam)
        {
            HttpWebRequest myhttpwebrequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myhttpwebrequest.Method = "post";
            myhttpwebrequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            // encode the data 
            byte[] encodedbytes = Encoding.UTF8.GetBytes(xmlparam);
            myhttpwebrequest.ContentLength = encodedbytes.Length;

            // write encoded data into request stream 
            Stream requeststream = myhttpwebrequest.GetRequestStream();
            requeststream.Write(encodedbytes, 0, encodedbytes.Length);
            requeststream.Close();

            HttpWebResponse result;

            try
            {
                result = (HttpWebResponse)myhttpwebrequest.GetResponse();
            }
            catch
            {
                return string.Empty;
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                using (Stream mystream = result.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(mystream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            return null;
        }
    }
}
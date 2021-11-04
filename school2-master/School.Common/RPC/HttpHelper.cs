using school.Model.TO;
using school.Model.TO.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace school.Common.RPC
{
    public class HttpHelper
    {
        #region Post Get(HttpClient)

        public static string Get(string url, string sessionKey, string language, string ip)
        {
            var client = GetHttpClient(sessionKey, language, ip);
            var response = client.GetAsync(new Uri(url)).Result;
            var task = response.Content.ReadAsStringAsync();
            response.Dispose();
            client.Close();
            return task.Result;
        }

        public static string Post(string url, string data, string sessionKey, string language, string ip)
        {
            var client = GetHttpClient(sessionKey, language, ip);
            HttpContent stringContent = new StringContent(data);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync(new Uri(url), stringContent).Result;
            var task = response.Content.ReadAsStringAsync();
            response.Dispose();
            client.Close();
            return task.Result;
        }

        #endregion


        #region 创建HttpClient

        /// <summary>
        /// 获取一个用于请求的HttpClient
        /// </summary>
        public static HttpClient GetHttpClient(string sessionKey, string language, string ip)
        {
            var apiHeader = new ApiHeader
            {
                UserKey = sessionKey,
                LangKey = language,
                AppKey = "DDD",
                Ip = ip
            };
            var client = HttpClientPool.Instance.Open(apiHeader);
            return client;
        }


        #endregion

        
    }
}

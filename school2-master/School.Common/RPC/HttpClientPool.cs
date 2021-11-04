using school.Model.TO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace school.Common.RPC
{
    public class HttpClientPool
    {
        private static readonly HttpClientPool instance = new HttpClientPool();
        private ConcurrentBag<HttpClient> httpClients = null;
        private readonly string apiHeaderKey = Const.APIHEADER;

        private HttpClientPool()
        {
            httpClients = new ConcurrentBag<HttpClient>();
        }
        public static HttpClientPool Instance
        {
            get { return instance; }
        }

        public ConcurrentBag<HttpClient> HttpClients
        {
            get { return httpClients; }
        }

        public HttpClient Open(ApiHeader header)
        {
            HttpClient httpClient = null;
            if (httpClients.Count > 0)
            {
                httpClients.TryTake(out httpClient);
            }
            else
            {
                var handler = new HttpClientHandler()
                {
                  //  AutomaticDecompression = DecompressionMethods.GZip,
                };
                httpClient = new HttpClient(handler);
                httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            }
            if (httpClient.DefaultRequestHeaders.Contains(apiHeaderKey))
            {
                httpClient.DefaultRequestHeaders.Remove(apiHeaderKey);
            }
            httpClient.DefaultRequestHeaders.Add(apiHeaderKey, header.ToJson());
            return httpClient;
        }
    }
}

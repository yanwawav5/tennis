using school.Model.VO;
using school.Common;
using school.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using school.BLL.Factory;
using school.Model.BO.User;
using school.Model.Enum;

namespace school.API.Filter
{
    /// <summary>
    /// WebApiDelegatingHandler
    /// </summary>
    public class schoolDelegatingHandler : DelegatingHandler
    {
        /// <summary>
        /// 重新请求信息到内部API，从而进行处理
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
           var logItem = new LogItem();
            var userId = string.Empty;
            var projectId = string.Empty;
            string userBody = string.Empty;
            var powers = string.Empty;
            if (request.Content != null)
            {
                try
                {
                    IEnumerable<string> keys = null;
                    logItem.URL = request.RequestUri.AbsoluteUri;
                    logItem.Name = request.RequestUri.LocalPath;
                    logItem.Header = request.Headers.TryGetValues("MyHeader",out keys)?keys.FirstOrDefault():"";
                    logItem.RequestTime = DateTime.Now;  
                    logItem.Body = request.Content.ReadAsStringAsync().Result; 
                }
                catch (Exception exp)
                {
                    Log.Error(MethodBase.GetCurrentMethod().Name, exp);
                }
            }
            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>
            ((task) =>
            {
                if (task.Result.StatusCode == HttpStatusCode.BadRequest)
                {
                    //由于异常处理，人工将请求码设置成BadRequest，此处重新设置成OK，以便Client处理
                    task.Result.StatusCode = HttpStatusCode.OK;
                    //logItem.Result = Gzip.DeCompressStr(task.Result.Content.ReadAsByteArrayAsync().Result);
                    logItem.Result = task.Result.Content.ReadAsStringAsync().Result;
                }
                logItem.ResponseTime = DateTime.Now;
                logItem.ExcuteTimes = (logItem.ResponseTime - logItem.RequestTime).TotalMilliseconds;
              
                Log.Info(new object[] { "API", logItem });
                return task.Result;
            }
            );
        } 
    }
}
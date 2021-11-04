using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Net;
using school.Model.TO.Response;
using school.Common.Tools;
using school.Common;
using school.Api;
using Newtonsoft.Json.Linq;
using school.Model.Enum;

namespace school.API.Filter
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class schoolExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {

            var exp = actionExecutedContext.Exception;
            var baseController = (BaseController)actionExecutedContext.ActionContext.ControllerContext.Controller;
            var data = baseController.CallException<CommonResponse>(exp);
            var subMsg = data.Message; 
            if (exp != null)
            { 
                var typeName = exp.GetType().Name;
                if (typeName == "schoolException")
                {
                    if (((schoolException) exp).ErrorCode == SubCode.UnifiedError.GetHashCode())
                    { 
                        data.SubMessage = subMsg;
                        data.Message = "操作失败"; 
                    }
                    else if (((schoolException)exp).ErrorCode == SubCode.ParError.GetHashCode())
                    {
                        data.SubMessage = subMsg;
                        data.Message = "非法输入";
                    }
                  

                }
                else
                {
                    data.SubMessage = subMsg;
                    data.Message = "操作失败";
                }
            }

           
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, data);
            return base.OnExceptionAsync(actionExecutedContext, cancellationToken);
        }
    }
}
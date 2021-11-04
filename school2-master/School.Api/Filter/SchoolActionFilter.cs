using school.Model.Enum;
using school.Model.TO.Response;
using school.Common;
using school.Common.Tools;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using school.Api;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace school.API.Filter
{
    /// <summary>
    /// 自定义动作过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class schoolActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 是否验证登录
        /// </summary>
        public bool IsNeedLogin = true;
        public bool IsCk = true;
        /// <summary>
        /// OnActionExecutingAsync
        /// </summary>
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (!IsCk)
            {
                return base.OnActionExecutingAsync(actionContext, cancellationToken);
            }
            var baseController = (BaseController)actionContext.ControllerContext.Controller;
            if (IsNeedLogin && !baseController.IsLogin())
            {
                var data = baseController.CallException<CommonResponse>(new schoolException(SubCode.RequireLogin.GetHashCode(), Message.Instance.R("v1")));
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, data);
            } 
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
        
        /// <summary>
        /// OnActionExecutedAsync
        /// </summary>
        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}
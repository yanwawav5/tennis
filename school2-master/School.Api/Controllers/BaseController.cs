using school.Model.Enum;
using school.Model.TO;
using school.Model.TO.Response;
using school.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using school.IBLL.Session;
using System.Web;
using System.Reflection;
using school.Common.Tools;
using school.BLL.Factory;
using school.Model.BO.User;
using school.Model.BO;

namespace school.Api
{
    public class BaseController : ApiController
    {
        #region 属性
        /// <summary>
        /// 通用实例
        /// </summary>
        protected SessionService mSession { get; set; }
        /// <summary>
        /// API请求Head携带信息
        /// </summary>
        protected ApiHeader mHeader { get; set; }
        /// <summary>
        /// 上下文
        /// </summary>
        protected ActionContext mActionContext { get; set; }
        /// <summary>
        /// HTTP上下文
        /// </summary>
        protected HttpContext mHttpContext { get; set; }
        #endregion

        #region 构造方法
        public BaseController()
        {
            mHeader=GetHeaderParams();
            mSession = SessionFactory.SessionService;
            mHttpContext = HttpContext.Current;
            mActionContext = new ActionContext { LangKey = mHeader?.LangKey,ActionTime = DateParse.GetDateTime() ,LoginUser=new UserBO()};
        }
        #endregion

        #region 请求头解析
        /// <summary>
        /// 获取请求头参数
        /// </summary>
        private ApiHeader GetHeaderParams()
        {
            try
            {
                var value = HttpContext.Current.Request.Headers[Const.APIHEADER];
                return value.ToObject<ApiHeader>();
            }
            catch (Exception exp)
            {
                Log.Error(MethodBase.GetCurrentMethod().Name, exp);
                return null;
            }
        }

        #endregion

        #region 验证
        /// <summary>
        /// 验证相关安全与权限
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public Boolean IsLogin()
        {
            var isLogin = false;
            //验证
            if (IsLogin(mHeader.UserKey, mHeader.EquipmentCode))
            {
                isLogin = true;
            }
            return isLogin;
            //return true;
        }
        /// <summary>
        /// 验证SessionKey是否有效
        /// </summary>
        private bool IsLogin(string sessionKey, string equipmentCode)
        {
            var userBO = CacheHelper.Memcached.Get(sessionKey); 
            if (userBO!= null)
            {  
                mActionContext.WxLoginUser = (WxUserBO)userBO;
                if (mActionContext.WxLoginUser.Id == null)
                    return false;
                return true;
            }
            else
            {
                var model = mSession.mWloginKeyBLL.Get(mHeader.UserKey);
                if (model != null)
                {
                    var stude = mSession.mstudentBLL.GetOneByUionId(model.Unionid);
                    userBO = new WxUserBO { Id = stude.Id, OpenId = model.OpenId, UserType = model.UserType,UnionId=model.Unionid,SmOpenId=stude.SmOpenId};
                    Common.Cache.CacheHelper.Memcached.Set(mHeader.UserKey, userBO);
                    mActionContext.WxLoginUser = (WxUserBO)userBO;
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 通用返回

        #region Callback
        
        /// <summary>
        /// Api请求后的返回数据结构
        /// </summary>
        protected ApiResult<T> CallbackData<T>(SubCode code, string message, T body) where T :AbsResponse, new()
        {
            return new ApiResult<T>() { SubCode = code, Message = message, Body = body };
        }
        /// <summary>
        ///  Api请求后的返回数据结构
        /// </summary>
        protected ApiResult<T> CallbackData<T>(SubCode code, string message = "") where T : AbsResponse, new()
        {
            return new ApiResult<T>() { SubCode = code, Message = message };
        }
        /// <summary>
        ///  Api请求后的返回数据结构
        /// </summary>
        protected ApiResult<T> CallbackData<T>(T body) where T : AbsResponse, new()
        {
            return CallbackData(0, null, body);
        }
        /// <summary>
        /// Api请求后的返回数据结构
        /// </summary>
        protected ApiResult<CommonResponse> CallbackData(int rowCount)
        {
            if (rowCount == -1)
                return CallbackData<CommonResponse>(SubCode.Failure, "");

            if (rowCount > 0)
            {
                return ApiResultHelper.Success();
            }
            else
            {
                return new ApiResult<CommonResponse>() { SubCode= SubCode.Success, SubMessage="" };
            }
        }
        /// <summary>
        /// 返回异常信息
        /// </summary>
        [NonAction]
        public ApiResult<T> CallException<T>(Exception ex) where T : AbsResponse, new()
        {
            var result = new ApiResult<T>() { Code = ApiCode.Failure, SubCode = SubCode.Failure, Message = ex.Message };

            if (ex.GetType() == typeof(schoolException))
            {
                result.Code = ApiCode.Success;
                result.SubCode= (SubCode)((schoolException)ex).ErrorCode;
            }
            
            return result;
        }

        #endregion

       
        #endregion
         
        /// <summary>
        /// 创建对象
        /// </summary>
        protected T CreateObject<T>() where T : AbsResponse, new()
        {
            T t = new T();
            return t;
        } 
      
        protected string R(string key)
        {
            return Message.Instance.R(key, mHeader.LangKey);
        } 
    }
}

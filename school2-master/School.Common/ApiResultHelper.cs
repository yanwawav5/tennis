using school.Model.Enum;
using school.Model.TO;
using school.Model.TO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Common
{

    public class ApiResultHelper
    {
        #region 成功

        /// <summary>
        /// 快速返回一个表示操作成功的ApiResult
        /// </summary>
        public static ApiResult<T> Success<T>(T body) where T : AbsResponse, new()
        {
            var res = new ApiResult<T>();
            res.Code = ApiCode.Success;
            res.SubCode = SubCode.Success;
            res.Body = body;
            return res;
        }
        /// <summary>
        /// 快速返回一个表示操作成功的ApiResult
        /// </summary>
        public static ApiResult<CommonResponse> Success()
        {
            var res = new ApiResult<CommonResponse>();
            res.Code = ApiCode.Success;
            res.SubCode = SubCode.Success;
            return res;
        }

        #endregion

      

        #region 业务错误

        /// <summary>
        /// 快速返回一个表示业务错误的ApiResult
        /// </summary>
        public static ApiResult<T> SubError<T>(string Message) where T : AbsResponse, new()
        {
            var res = new ApiResult<T>();
            res.Code = ApiCode.Success;
            res.SubCode = SubCode.Failure;
            res.Message = Message;
            return res;
        }

        /// <summary>
        /// 快速返回一个表示业务错误的ApiResult
        /// </summary>
        public static ApiResult<CommonResponse> SubError(string Message)
        {
            var res = new ApiResult<CommonResponse>();
            res.Code = ApiCode.Success;
            res.SubCode = SubCode.Failure;
            res.Message = Message;
            return res;
        }
        
        #endregion
    }
}

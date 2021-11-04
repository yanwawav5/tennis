using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.Enum
{
    public enum SubCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Failure = 1,
        /// <summary>
        /// 操作失败
        /// </summary>
        UnifiedError = 5,
        /// <summary>
        /// 参数错误
        /// </summary>
        ParError = 6,
        /// <summary>
        /// 请输入用户名
        /// </summary>
        NeedUsername = 10,
        /// <summary>
        /// 
        /// </summary>
        NeedPassword = 11,
        /// <summary>
        /// 
        /// </summary>
        WrongUsernameOrPassword = 12,

        /// <summary>
        /// 
        /// </summary>
        RequireLogin = 1000,
        /// <summary>
        /// 
        /// </summary>
        NoOperate = 1001,
        /// <summary>
        /// 
        /// </summary>
        OvertimeSessionKey = 500,
        /// <summary>
        /// 
        /// </summary>
        InvalidSessionKey = 503,
        /// <summary>
        /// 
        /// </summary>
        RequireVerifyCode = 510,
        /// <summary>
        /// 
        /// </summary>
        InvalidVerifyCode = 511,
        /// <summary>
        /// 
        /// </summary>
        WrongVerifyCode = 512,
        /// <summary>
        /// 
        /// </summary>
        NoAccessControl = 513, 
       
    }
}

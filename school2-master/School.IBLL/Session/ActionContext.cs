using school.Model.BO;
using school.Model.BO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.IBLL.Session
{
    public class ActionContext
    {
        #region 当前请求语言

        /// <summary>
        /// 当前语言关键字
        /// </summary>
        public string LangKey { get; set; }

        #endregion

        #region 提交时间

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ActionTime { get; set; }
        /// <summary>
        /// 当前登录者
        /// </summary>
        public UserBO LoginUser { get; set; }

        public WxUserBO WxLoginUser { get; set; }
        #endregion
    }
}

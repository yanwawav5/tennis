using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model
{
    public class WxSmallTokenVO : WxBaseVO
    {
        public string access_token { get; set; }

        public int expires_in { get; set; }
    }

    public class WxJsVO : WxBaseVO
    {
        public string ticket { get; set; }

        public int expires_in { get; set; }
    }
    public class wxre
    {
        /// <summary>
        /// code
        /// </summary>
        public string errcode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }
         
    }
}

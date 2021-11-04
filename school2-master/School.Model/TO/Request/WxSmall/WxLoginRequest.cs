using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class WxLoginRequest : AbsRequest
    {
        /// <summary>
        /// 登录CODE
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string EncryptedData { get; set; }
        /// <summary>
        /// IV 
        /// </summary>
        public string Iv { get; set; }
    }
}

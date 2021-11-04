using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class WxUserBO
    {
        public string Id { get; set; }
        public string OpenId { get; set; }
        public string SmOpenId { get; set; }
        public string NickName { get; set; }
        public string UserKey { get; set; }
        public int UserType { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// openId
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// openId
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// openId
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// openId
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// openId
        /// </summary>
        public string UnionId { get; set; }

    }
}

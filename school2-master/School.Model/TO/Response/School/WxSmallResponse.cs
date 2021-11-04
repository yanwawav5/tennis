using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class WxSmallResponse : AbsResponse
    {
        //public WxSmallInfoVO Model { get; set; }
        //public WUserInfoVO Model { get; set; }
        public M_Student Model { get; set; }
    }

    public class WxSmallLgResponse : AbsResponse
    {
        public StuBO Model { get; set; }
    }
    public class StuBO
    {
        /// <summary>
        /// SessionKey
        /// </summary>
        public string SessionKey { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Enabled { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
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
        /// openId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string NickName { get; set; }
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
        /// <summary>
        /// 小程序OpeniD
        /// </summary>
        public string SmOpenId { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public string UserKey { get; set; }
        /// <summary>
        /// 是否登录
        /// </summary>

        public bool IsLogin { get; set; }
    }
}

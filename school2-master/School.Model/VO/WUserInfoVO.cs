using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class WUserInfoVO
    {
        public string NickName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string AvatarUrl { get; set; } 
        public string Id { get; set; } 
        public int Status { get; set; }
        public string Tel { get; set; }

        public string SessionKey { get; set; }
        /// <summary>
        /// 角色信息
        /// </summary>
        public int UserType { get; set; }
    }
}

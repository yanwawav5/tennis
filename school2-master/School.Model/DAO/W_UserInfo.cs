using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_UserInfo : BaseEntity
    {
        public string NickName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string AvatarUrl { get; set; }
        public string UnionId { get; set; }
        public string OpenId { get; set; }
        public int Status { get; set; }
        public int UserType { get; set; }
    }
}

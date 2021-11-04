using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_UserAddress : BaseEntity
    {
        public string OpenId { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Tel { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        public int IsDefault { get; set; }
        public string UserInfoId { get; set; }

        public string Latitude { get; set; }
        public string Logitude { get; set; }
        public string Name { get; set; }
        public string UserAddress { get; set; }
    }
}

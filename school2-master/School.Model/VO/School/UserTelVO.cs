using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class UserTelVO
    {
        /// <summary>
        /// 电话
        /// </summary>
        public string phoneNumber { get; set; }
        /// <summary>
        /// 区号电话
        /// </summary>
        public string purePhoneNumber { get; set; }
        /// <summary>
        /// 区号
        /// </summary>
        public string countryCode { get; set; }
    }
}

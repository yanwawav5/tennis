using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
   public class PayRequest:AbsRequest
    {
        public string Code { get; set; }
        public string ServerId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 0为课程支付，1为场地支付
        /// </summary>
        public int Category { get; set; }
    }
}

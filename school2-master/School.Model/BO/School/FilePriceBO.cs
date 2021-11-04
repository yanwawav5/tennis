using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
   public class FilePriceBO
    {
        public string Id { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        public string PriceSec { get; set; }
        /// <summary>
        /// 上下午时间点
        /// </summary>
        public int sorttime { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }
    }
}

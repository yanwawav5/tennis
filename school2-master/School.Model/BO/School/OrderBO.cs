using school.Model.TO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class OrderBO
    {
        public string StudentId { get; set; }
        public string FieldId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 预订时间
        /// </summary>
        public List<int> HourList { get; set; }
        public string UserId { get; set; }
        public int Category { get; set; }


    }
}

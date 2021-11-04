using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
   public class ClassNumInfoVO
    {
        /// <summary>
        /// 总共
        /// </summary>
        public string AllNum { get; set; }
       /// <summary>
       /// 已使用
       /// </summary>
        public string UsedNum { get; set; }
        /// <summary>
        /// 剩余
        /// </summary>
        public string SurplusNum { get; set; }
    }
}

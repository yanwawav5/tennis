using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
  
    public  class M_TransferCourse : BaseModel
    {
        /// <summary>
        /// 学员
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 原课程
        /// </summary>
        public string OldCourseId { get; set; }
        /// <summary>
        /// 新课程
        /// </summary>
        public string NewCourseId { get; set; }
        /// <summary>
        /// 原来剩余课时
        /// </summary>
        public int OldSurplusClassTimes { get; set; }
        /// <summary>
        /// 现在课时
        /// </summary>
        public int NewSurplusClassTimes { get; set; }
        /// <summary>
        /// 原来单价
        /// </summary>
        public string UnitPrice { get; set; } 
        
    }
}

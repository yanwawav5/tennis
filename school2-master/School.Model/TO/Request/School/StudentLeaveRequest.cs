using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class StudentLeaveRequest : AbsRequest
    {
        /// <summary>
        /// 老师编号
        /// </summary>
      //  public string TeacherId { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary>
        public string StudentCourseClassId { set; get; }
        /// <summary>
        /// 留言
        /// </summary>
        public string Main { get; set; }
    }

    public class StudentLeaveListRequest : PageRequest
    {
        /// <summary>
        /// 学生
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 老师
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 课程
        /// </summary>
        public string StudentCourseClassId { get; set; }
    }
}

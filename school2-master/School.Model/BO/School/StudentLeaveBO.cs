using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class StudentLeaveBO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 学生编号
        /// </summary>
        public string StudentId { get; set; }  
        /// <summary>
        /// 课程编号
        /// </summary>
        public string StudentCourseClassId { get; set; }
        /// <summary>
        /// 老师编号(student表Id）
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// 请假留言
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        public string UserId { get; set; }
    }


    public class StudentLeaveListBO:PageInfo
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

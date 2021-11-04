using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class StudentCourseClassQueryBORequest
    {
        /// <summary>
        /// 课程表
        /// </summary>
        public string TeacherCourseId { get; set; }
        /// <summary>
        /// 课程状态(0待使用，1完成，2取消)
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 老师编号
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 0查看所有，1查看作业,2布置作业,3作业已经上传，4.作业已经评价
        /// </summary>
        public int Flg { get; set; }
    }


    public class StudentCourseClassQueryStudentRequest
    { 
        /// <summary>
        /// 课程状态(0待使用，1完成，2取消)
        /// </summary>
        public string Status { get; set; } 
        /// <summary>
        /// 0查看所有，1查看作业,2布置作业,3作业已经上传，4.作业已经评价
        /// </summary>
        public int Flg { get; set; }
    }
}

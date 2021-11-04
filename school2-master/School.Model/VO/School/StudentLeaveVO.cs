using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class StudentLeaveVO
    {
        public DateTime? CreateTime { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// 留言人编号
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

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string StudentName { get; set; } 
        /// <summary>
        /// 学生电话
        /// </summary>
        public string StudentTel { get; set; }
        /// <summary>
        /// 老师姓名
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 老师电话
        /// </summary>
        public string TeacherTel { get; set; }

        /// <summary>
        /// day
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string TeacherCourseTime { get; set; }
        /// <summary>
        /// 场地名称
        /// </summary>
        public string SchoolName { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string ClassName { get; set; }
    }
}

using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class M_StudentLeave : BaseModel
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// 留言人编号
        /// </summary>
        [References(typeof(M_Student))]
        public string StudentId { get; set; }
        [Reference]
        public virtual M_Student M_Student { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary>
        
        [References(typeof(M_StudentCourseClass))]
        public string StudentCourseClassId { get; set; } 
        [Reference]
        public virtual M_StudentCourseClass M_StudentCourseClass { get; set; }
      

        /// <summary>
        /// 老师编号(student表Id）
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// 请假留言
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 状态(0,正常，1请假，2申请成功，3，申请失败,4取消）
        /// </summary>
        public int Status { get; set; }
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

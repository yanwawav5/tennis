using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
  
    public  class M_TeacherCourseEvaluate : BaseModel
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 学生
        /// </summary>
        [References(typeof(M_Student))]
        public string StudentId { get; set; }
        [Reference]
        public virtual M_Student M_Student { get; set; }


        /// <summary>
        /// 老师
        /// </summary>
        [References(typeof(M_Student))]
        public string TeacherId { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        [References(typeof(M_TeacherCourse))]
        public string TeacherCourseId { get; set; }
        [Reference]
        public virtual M_TeacherCourse M_TeacherCourse { get; set; }

        /// <summary>
        /// 学生报名
        /// </summary>
        [References(typeof(M_StudentCourseClass))]
        public string StudentCourseClassId { get; set; }
        [Reference]
        public virtual M_StudentCourseClass M_StudentCourseClass { get; set; } 

        /// <summary>
        /// 老师评价
        /// </summary> 
        public string TeacherEvaluate { get; set; }
        /// <summary>
        /// 学生评价
        /// </summary> 
        public string StudentEvaluate { get; set; }

        /// <summary>
        /// 学生星星
        /// </summary> 
        public int StudentStars { get; set; }
        /// <summary>
        /// 作业评价
        /// </summary> 
        public string JobEvaluate { get; set; }

    }
}

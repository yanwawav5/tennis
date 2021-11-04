using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{

    public class M_TeacherCourse : BaseModel
    {
        ///<summary>
        /// 老师编号
        /// </summary>
        [References(typeof(M_Student))]
        public string StudentId { get; set; }
        [Reference]
        public virtual M_Student M_Student { get; set; }
        ///<summary>
        /// 场地编号
        /// </summary>
        [References(typeof(M_Field))]
        public string FieldId { get; set; }
        [Reference]
        public virtual M_Field M_Field { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Book { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
        ///// <summary>
        ///// 详情
        ///// </summary>
        //public string Main { get; set; }
        /// <summary>
        /// 课程名分类(小班课程，双人课程)
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 年龄分类（精灵）
        /// </summary>
        public int AgeCategoryId { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 课程时间
        /// </summary>
        public string TeacherCourseTime { get; set; }

        /// <summary>
        /// 0全部1，正常，2取消
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 课程表课程名称
        /// </summary>
       // public string ClassName { get; set; }
        /// <summary>
        /// 教案
        /// </summary> 

        [References(typeof(M_TeachingPlan))]
        public string TeachingPlanId { get; set; }
        [Reference]
        public virtual M_TeachingPlan M_TeachingPlan { get; set; }


        [References(typeof(M_School))]
        public string SchoolId { get; set; }
        [Reference]
        public virtual M_School M_School { get; set; }
    }
}

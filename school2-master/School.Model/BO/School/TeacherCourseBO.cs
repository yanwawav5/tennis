using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class TeacherCourseBO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        ///<summary>
        /// 老师编号
        /// </summary> 
        public string StudentId { get; set; }  
        ///<summary>
        /// 场地编号
        /// </summary> 
        public string FieldId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 教案编号
        /// </summary>
        public string TeachingPlanId { get; set; }
        ///// <summary>
        ///// 标签
        ///// </summary>
        //public string Book { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 课程时间
        /// </summary>
        public string TeacherCourseTime { get; set; }
        ///// <summary>
        ///// 详情
        ///// </summary>
        //public string Main { get; set; }
        public string UserId { get; set; }

        ///// <summary>
        ///// 课程名分类(小班课程，双人课程)
        ///// </summary>
        //public int CategoryId { get; set; }
        ///// <summary>
        ///// 年龄分类（精灵）
        ///// </summary>
        //public int AgeCategoryId { get; set; }
        /// <summary>
        /// 插入订场数据
        /// </summary>
        public List<SubscribeBO> List { get; set; }
    }
}

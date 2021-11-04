using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class StudentCourseBO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 课程名分类
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 年龄分类
        /// </summary>
        public string AgeCategoryId { get; set; }
        /// <summary>
        /// 课时（如2h-节）
        /// </summary>
        public string HourInfo { get; set; }
        /// <summary>
        /// 价格（元）
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 课时（如10节）
        /// </summary>
        public int ClassTimes { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 用户年龄
        /// </summary>
        public int UserCategoryId { get; set; }
        /// <summary>
        /// 网球水平
        /// </summary>
        public int TennisId { get; set; }

        /// <summary>
        /// 卡卷
        /// </summary> 
        public string StudentKjId { get; set; } 
        /// <summary>
        /// 学生编号
        /// </summary> 
        public string StudentId { get; set; } 

        /// <summary>
        /// 课程编号
        /// </summary> 
        public string CourseId { get; set; }

        /// <summary>
        /// 添加课时数量
        /// </summary>
        public int AddClassTimes { get; set; }
    }
}

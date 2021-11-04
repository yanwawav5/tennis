using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
   public class TeacherCourseFieldVO
    {
        public string Id { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 场地编号
        /// </summary>
        public string FieldId { get; set; }
        /// <summary>
        /// 场地名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary>
        public string TeacherCourseId { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string TeacherTime { get; set; }
        /// <summary>
        /// 报名人数
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 剩余人数
        /// </summary>
        public int SurplusNum { get; set; }
    }
}

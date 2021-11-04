using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{

    public class M_Subscribe : BaseModel
    {
        /// <summary>
        /// 学生编号
        /// </summary>
        [References(typeof(M_Student))]
        /// <summary>
        /// 教师课程
        /// </summary>
        public string TeacherCourseId { get; set; } 
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        [References(typeof(M_Student))]
        public string StudentId { get; set; }
        [Reference]
        public virtual M_Student M_Student { get; set; }
        /// <summary>
        /// 场地编号
        /// </summary>
        [References(typeof(M_Field))]
        public string FieldId { get; set; }
        [Reference]
        public virtual M_Field M_Field { get; set; }
        /// <summary>
        /// 时间（10开始）
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// 状态0默认1取消
        /// </summary>
        public int Status { get; set; }


        /// <summary>
        /// 日期如20200228
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 活动编号
        /// </summary>
        public string ActivityId { get; set; }

    }
}

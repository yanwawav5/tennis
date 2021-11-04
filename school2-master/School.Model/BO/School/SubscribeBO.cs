using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class SubscribeBO
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
        /// 教师课程
        /// </summary>
        public string TeacherCourseId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 时间（10开始）
        /// </summary>
       // public int Hour { get; set; }

        /// <summary>
        /// 状态0默认1取消
        /// </summary>
        public int Status { get; set; } 
        /// <summary>
        /// 日期如20200228
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 场地编号
        /// </summary>
        public string FieldId { get; set; }
        /// <summary>
        /// 活动编号
        /// </summary>
        public string ActivityId { get; set; }
        /// <summary>
        /// 订购场地时间
        /// </summary>

        public List<int> HourList { get; set; }
    }

    public class SubscribeAddBO
    {
        /// <summary>
        /// 创建者
        /// </summary>
        public string UserId { get; set; }
        public List<SubscribeBO> List { get; set; }
        ///// <summary>
        ///// 学生编号
        ///// </summary>
        //public string StudentId { get; set; }
        ///// <summary>
        ///// 教师课程
        ///// </summary>
        //public string TeacherCourseId { get; set; }
        ///// <summary>
        ///// 订单编号
        ///// </summary>
        //public string OrderId { get; set; }
        ///// <summary>
        ///// 日期如20200228
        ///// </summary>
        //public int Day { get; set; }

        ///// <summary>
        ///// 场地编号
        ///// </summary>
        //public string FieldId { get; set; }
        ///// <summary>
        ///// 订购场地时间
        ///// </summary>

        //public List<int> HourList { get; set; }

    }


    public class SubscribeESC
    {
        public string UserId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public List<string> OrderId { get; set;}
        /// <summary>
        /// 老师定课编号
        /// </summary>
        public string TeacherCourseId { get; set; }
    }
}

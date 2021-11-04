using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class ActivityStudentBO
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
        /// 活动编号
        /// </summary>
        public string ActivityId { get; set; }
       
        /// <summary>
        /// 价格
        /// </summary>
        public string Price
        {
            get; set;
        }
        /// <summary>
        /// 状态0默认不报名，1报名成功，2待取消，3取消
        /// </summary>
        public int Status { get; set; }
        public string StudentName { get; set; }
        public string StudentTel { get; set; }
        public string UserId { get; set; }
    }
}

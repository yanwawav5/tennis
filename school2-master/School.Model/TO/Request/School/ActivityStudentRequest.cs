using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class ActivityStudentRequest : AbsRequest
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
        /// <summary>
        /// 报名姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 报名电话
        /// </summary>
        public string StudentTel { get; set; }
    }
}

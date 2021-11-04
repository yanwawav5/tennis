using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{

    public class M_ActivityStudent : BaseModel
    {
        /// <summary>
        /// 学生编号
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 活动编号
        /// </summary>
        [References(typeof(M_Activity))]
        public string ActivityId { get; set; }
        [Reference]
        public virtual M_Activity M_Activity { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 状态0默认不报名，1报名成功，2待取消,3取消成功，4退款完成,5完成（虚拟状态）
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 报名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 报名
        /// </summary>
        public string StudentTel { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl { get; set; }
    }
}

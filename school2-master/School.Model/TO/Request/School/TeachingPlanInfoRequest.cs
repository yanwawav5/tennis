using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class TeachingPlanInfoRequest : AbsRequest
    {
        public string Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 教案编号
        /// </summary> 
        public string TeachingPlanId { get; set; }
        /// <summary>
        /// 时间段
        /// </summary>
        public string TimeSlot { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public string TimeLength { get; set; }

        /// <summary>
        /// 组织安全
        /// </summary>
        public string Org { get; set; }

        /// <summary>
        /// 视频编号
        /// </summary>
        public string VideoId { get; set; }

        /// <summary>
        /// 技术要点
        /// </summary>
        public string Points { get; set; }

    }

}

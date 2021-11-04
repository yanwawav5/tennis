using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class TeachingPlanInfoVO
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
        /// 分类
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 子分类
        /// </summary>
        public string CategoryItemId { get; set; }

        public string CategoryName { get; set; }
        public string CategoryItemName { get; set; }
        /// <summary>
        /// 视频名称
        /// </summary>
        public string VideoName { get; set; }
        /// <summary>
        /// 技术要点
        /// </summary>
        public string Points { get; set; }

    }
}

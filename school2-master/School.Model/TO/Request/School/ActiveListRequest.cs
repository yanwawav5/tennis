using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class ActiveListRequest : PageRequest
    {
        /// <summary>
        /// 空全部，1正常，2取消，3完成
        /// </summary>
        public string Status { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 按日
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        ///  0忽略，1 报名中活动），2报名或者未开始3不能报名活动
        /// </summary>
        public string StatusInfo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class ShareInfoRequest : AbsRequest
    {
        public string UserKey { get; set; }

        /// <summary>
        /// 年月日（20200115）
        /// </summary>
        public string ShareDay { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 课程作业
        /// </summary>
        public string StudentCourseClassId { get; set; }
        /// <summary>
        /// 体验课
        /// </summary>
        public string StudentCourseId { get; set; }
        /// <summary>
        /// 类型（0,约课 1，作业，2体验）
        /// </summary>
        public int Types { get; set; }
    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class ShareInfoBO
    {

        public string Id { get; set; }
        /// <summary>
        /// 学生Id
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 点击
        /// </summary>
        public int Click { get; set; }

        /// <summary>
        /// 年月日（20200115）
        /// </summary>
        public string ShareDay { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// UnionId
        /// </summary>
        public string UnionId { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string NickName { get; set; } 

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

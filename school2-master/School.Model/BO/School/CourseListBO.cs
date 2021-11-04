using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class CourseListBO : PageInfo
    {
        /// <summary>
        /// 课程名分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 年龄分类
        /// </summary>
        public int AgeCategoryId { get; set; }
    }

    public class StudentCourseListBO : PageInfo
    {
        /// <summary>
        /// 课程名分类
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 年龄分类
        /// </summary>
        public string AgeCategoryId { get; set; }
        /// <summary>
        /// 状态（0未使用，1已使用，2，申请退款，3退款成功）
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 0普通课程1体验课(空全部)
        /// </summary>
        public string Normal { get; set; }
        /// <summary>
        /// 是否学生
        /// </summary>
        public bool IsStudent { get; set; }
    }
}

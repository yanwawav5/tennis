using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class TeacherMessageBO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 留言人编号
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 老师编号
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// 用户ID
        /// 
        /// </summary>
        public string UserId { get; set; }
    }
}

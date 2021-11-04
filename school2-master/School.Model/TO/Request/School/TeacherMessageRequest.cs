using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class TeacherMessageRequest: AbsRequest
    {
        
        public string Id { get; set; }
        /// <summary>
        /// 1学生给老师，2老师给学生
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set;}
        /// <summary>
        /// 内容
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 留言人编号
        /// </summary>
        public string ToUserId { get; set; }
    }
}

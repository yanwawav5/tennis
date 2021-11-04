using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class M_TeacherMessage : BaseModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        [References(typeof(M_Student))]
        public string StudentId { get; set; } 
        [Reference]
        public virtual M_Student M_Student { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 老师编号
        /// </summary>
        public string TeacherId { get; set; }
    }
}

using school.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class TeacherCourseRequest
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        ///<summary>
        /// 老师编号
        /// </summary> 
        public string StudentId { get; set; }
        ///<summary>
        /// 场地编号
        /// </summary> 
        public string FieldId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 教案编号
        /// </summary>
        public string TeachingPlanId { get; set; } 
        public string UserId { get; set; }
         
        /// <summary>
        /// 插入订场数据
        /// </summary> 
        public List<SubscribeBO> List { get; set; }
    }
}

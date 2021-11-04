using school.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class IdListKjRequest : PageInfo
    {
        public string Id { get; set; }
        public string Status { get; set; }

        public string Key { get; set; }
        public string CategoryId { get; set; }
    }

    public class IdListRequest : PageInfo
    {
        public string Id { get; set; }
        public string Status { get; set; }

        public string Key { get; set; } 
    }

   


    public class IdCouRequest
    {
        /// <summary>
        /// 老师课程
        /// </summary>
        public string TeacherStudentCourseId { get; set; }
        /// <summary>
        /// 学生报名
        /// </summary>
        public string StudentCourseId { get; set; }
    }
    public class IdRequest 
    {
        public string Id { get; set; }
        public string Status { get; set; }
    }
    public class IdCategoryListRequest : PageInfo
    {
        public string Id { get; set; } 
        public int Category { get; set; }
    }
    public class IdStatusCategoryListRequest : PageInfo
    {
        public string Id { get; set; }
        /// <summary>
        /// 0学生订场，1内部订场
        /// </summary>
        public string Category { get; set; }
        public string Status { get; set; }
    }
}

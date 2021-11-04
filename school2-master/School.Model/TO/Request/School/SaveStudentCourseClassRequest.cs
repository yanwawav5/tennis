using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class SaveStudentCourseClassRequest
    {
        public string TeacherCourseId { get; set; }
        public string StudentId { get; set; }
    }
}

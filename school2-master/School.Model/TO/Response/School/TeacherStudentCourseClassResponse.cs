using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class TeacherStudentCourseClassResponse:AbsResponse
    {
        public List<StudentCourseClassVO> List { get; set; }
        public int TotNum { get; set; }
    }

    public class TeacherStudentListVOResponse : AbsResponse
    {
        public List<TeacherStudentVO> List { get; set; }
    }
}

using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class TeacherSelListResponse : AbsResponse
    {
        public List<TeacherVO> List { get; set; }
    }

    public class TeacherCourseFieldListResponse:AbsResponse
    {
        public List<TeacherCourseFieldVO> List { get; set; }
    }
}

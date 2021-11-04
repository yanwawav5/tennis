using school.Model.TO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO.School
{
    public class DkListVO
    {
        public List<StudentCourseClassVO> StudentClassList { get; set; }


        public List<StudentCourseVO> StudentCourseList { get; set; }
    }
}

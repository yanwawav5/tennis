using school.Model.DAO;
using System;
using school.Model.VO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class StudentCourseClassRsponese : AbsResponse
    {
        public StudentCourseClassVO StudentCourseClass { get; set; }

    }
}

using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class TeackerActivityStudentVOResponse:AbsResponse
    { 
        public ActivityVO Model { get; set; }
        public List<ActivityStudentVO> List { get; set; }
    }
}

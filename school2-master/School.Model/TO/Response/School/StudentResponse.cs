using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class StudentResponse:AbsResponse
    {
    }


    public class StudentLeaveListResponse : AbsResponse
    {
        public List<StudentLeaveVO> List { get; set; }
        public int TotNum { get; set; }
    }

    public class StudentLeaveResponse : AbsResponse
    {
        public StudentLeaveVO Model { get; set; }
    }
}

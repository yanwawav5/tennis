using school.Model.BO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class ActvityResponse:AbsResponse
    {
         
    }


    public class ActivityAllVOResponse : AbsResponse
    {
        public ActivityAllVO Model { get; set; }
    }
    public class ActivityListVOResponse : AbsResponse
    {
        public List<ActivityVO>List { get; set; }
        public int TotNum { get; set; }
    }

    public class  ActivityStudentListByStudentResponse : AbsResponse
    {
        public List<StudentActivityStudentVO> List { get; set; }
        public int TotNum { get; set; }
    }
}

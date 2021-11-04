using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class TeachingPlanResponse : AbsResponse
    {
        public TeachingPlanVO Model { get; set; }
        public List<TeachingPlanInfoVO> List { get; set; }
    }
    public class TeachingPlanListResponse : AbsResponse
    {
        public List<TeachingPlanVO> List { get; set; }
        public int TotNum { get; set; }
    }


    public class TeachingPlanInfoResponse : AbsResponse
    {
        public TeachingPlanInfoVO Model { get; set; }
    }
    public class TeachingPlanInfoListResponse : AbsResponse
    {
        public List<TeachingPlanInfoVO> List { get; set; }
        public int TotNum { get; set; }
    }
}



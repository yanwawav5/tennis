using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class TeacherResponse :AbsResponse
    {
        public TeacherInfoVO Model { get; set; }
    }

    public class TeacherListResponse : AbsResponse
    {
        public List<TeacherInfoVO> List { get; set; }
        public int TotNum { get; set; }
    } 
}

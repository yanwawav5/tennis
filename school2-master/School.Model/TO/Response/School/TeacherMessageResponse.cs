using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class TeacherMessageResponse : AbsResponse
    {
        public TeacherMessageVO Model { get; set; }
    }
}

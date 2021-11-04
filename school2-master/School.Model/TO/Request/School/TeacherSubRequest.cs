using school.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class TeacherSubRequest : AbsRequest
    {
        public List<SubscribeBO> List { get; set; }
    }
}

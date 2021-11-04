using school.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class SendMessageRequest 
    {
        public string StudentId { get; set; }
        public string StudentClassId { get; set; }
    }


    public class MenuRequest
    {
        public string menuMain { get; set; }
    }
}

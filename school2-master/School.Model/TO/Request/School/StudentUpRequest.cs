using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class StudentUpRequest
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Tel { get; set; }
        public int Types { get; set; }
        public string UserId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class EquListRequest : PageRequest
    {
        public string SchoolId { get; set; }
        public DateTime? Etime { get; set; }
        public DateTime? Btime { get; set; }
    }
}

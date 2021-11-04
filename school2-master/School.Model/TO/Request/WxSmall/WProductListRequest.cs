using school.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO
{
    public class WProductListRequest : PageInfo
    {
        public int Sort { get; set; }
    }
}

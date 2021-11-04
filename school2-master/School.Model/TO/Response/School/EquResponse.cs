using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class EquResponse:AbsResponse
    {
        public EquVO Model { get; set; }
    }

    public class EquListResponse : AbsResponse
    {
        public ListEquVO Model { get; set; }
    }
}

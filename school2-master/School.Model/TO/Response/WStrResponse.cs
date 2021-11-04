using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class WStrResponse : AbsResponse
    {
        public VauleVO Model { get; set; }
    }
    public class UpPicVOResponse : AbsResponse
    {
        public UpPicVO Model { get; set; }
    }

}

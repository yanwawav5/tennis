using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class IdListResponse : AbsResponse
    {
        public List<string> List { get; set; }
    }

    public class IdResponse : AbsResponse
    {
        public string Model { get; set; }
    }

    public class KjInfoResponse : AbsResponse
    {
        public int Num { get; set; }
        public string Price { get; set; }
        public string Id { get; set; }
    }

}

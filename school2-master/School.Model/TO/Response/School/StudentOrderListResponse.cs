using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class StudentOrderListResponse : AbsResponse
    {
        public List<OrderVO> List { get; set; }
        public int TotNum { get; set; }
    }
    public class StudentOrderResponse : AbsResponse
    {
        public OrderVO Model { get; set; }
    }
}

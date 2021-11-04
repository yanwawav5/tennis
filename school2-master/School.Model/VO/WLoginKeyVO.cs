using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class WLoginKeyVO
    {
        public string Id { get; set; }
        public string OpenId { get; set; }
        public int Status { get; set; }
        public string UserId { get; set; }
        public int UserType { get; set; }

        public string Unionid { get; set; }
    }
}

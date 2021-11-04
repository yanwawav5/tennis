using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class WServerMainAddBO:WBaseBO
    {
        public DateTime? ApplyTime { get; set; } 
        public string UserAddressId { get; set; }
        public string OtherAddress { get; set; }
        public string OtherTel { get; set; }
        public string ProductId { get; set; }
        public string UserInfoId { get; set; } 

        public int ServerNum { get; set; }
        public List<WServerItemBO> List { get; set; }
    }
}

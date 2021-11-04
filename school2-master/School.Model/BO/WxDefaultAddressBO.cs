using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
   public class WxDefaultAddressBO: WBaseBO
    {
        public int IsDefault { get; set; }
        public string UserInfoId { get; set; }
    }
}

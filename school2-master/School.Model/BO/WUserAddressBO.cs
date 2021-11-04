using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class WUserAddressBO : WBaseBO
    {
        public string OpenId { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Tel { get; set; } 
        public string Latitude { get; set; }
        public string Logitude { get; set; }
        public string Name { get; set; }
        public string UserAddress { get; set; }
        public string UserInfoId { get; set; }
    }
}

using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
   public class W_TopPic : BaseEntity
    { 
        public string Name { get; set; }
        public string Url { get; set; }
        public string IsTop { get; set; }
        public string Pic { get; set; }
    }
}

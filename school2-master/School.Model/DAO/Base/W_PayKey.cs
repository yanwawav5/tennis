using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO.Base
{
   public class W_PayKey
    {
        public int Id { get; set; }
        public string ServerId { get; set; } 
        public DateTime CreateTime { get; set; }
        public int Category { get; set; }
    }
}

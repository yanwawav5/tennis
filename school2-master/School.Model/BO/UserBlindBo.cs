using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
   public class UserBlindBo
    { 
        /// <summary>
        /// 试验方案
        /// </summary>
        public int BlindMethodType { get; set; }
        /// <summary>
        /// 用户是否盲态
        /// </summary>
        public int BlindStatus { get; set; }
    }
}

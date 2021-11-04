using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class MathBO
    {
        public string ProductItemId { get; set; }
        public int MathNum { get; set; }
        /// <summary>
        /// 0 正常*  1,特殊固定
        /// </summary>
        public int TypeInfo { get; set; }
        public int ServerNum { get; set; }
    }
     
}

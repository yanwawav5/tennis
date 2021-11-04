using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
   public class M_FilePrice : BaseModel
    {
        /// <summary>
        /// 分界时间
        /// </summary>
        public int sorttime { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 晚上价格
        /// </summary>
        public string PriceSec { get; set; }
    }
}

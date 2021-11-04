using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{

    public class M_PrizeType : BaseModel
    {
        /// <summary>
        /// 类别
        /// </summary>
        public int PrizeType { get; set; }

        /// <summary>
        /// 中奖概率
        /// </summary>
        public string PrizeInfo { get; set; }
    }
}

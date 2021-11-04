using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class M_StudentPrize : BaseModel
    {
        /// <summary>
        /// 分享编号
        /// </summary>
        public string ShareInfoId { get; set; }
        /// <summary>
        /// UnionId
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 1代表中奖
        /// </summary>
        public int Prize { get; set; }
        /// <summary>
        /// 0,1,2,3,4,5代表中奖
        /// </summary>
        public int PrizeNum { get; set; }

        /// <summary>
        /// 0未抽奖，1抽奖
        /// </summary>
        public int Status { get; set; }

    }
}

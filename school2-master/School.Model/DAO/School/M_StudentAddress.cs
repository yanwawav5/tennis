using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class M_StudentAddress : BaseModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string StudentId { get; set; }
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class WServerItemBO:WBaseBO
    {
        public string UserInfoId { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public int Areas { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        public int Sort { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 子产品Id
        /// </summary>
        public string ProductItemId { get; set; }
        /// <summary>
        /// 购物车Id
        /// </summary>
        public string ServerMainId { get; set; }
    }
}

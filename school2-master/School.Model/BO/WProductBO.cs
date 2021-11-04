using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class WProductBO :WBaseBO
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 好评
        /// </summary>

        public int Praise { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string SubMain { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int MatchFormula { get; set; }

        /// <summary>
        /// 服务价格图片
        /// </summary>
        public string ServerPic { get; set; }

    }
}

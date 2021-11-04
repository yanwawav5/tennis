using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class FieldVO
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        public string Pic { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; } 
        public string X { get; set; }
        public string Y { get; set; }
    }
}

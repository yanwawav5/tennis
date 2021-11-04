using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class ProductItemVO
    {
        /// <summary>
        /// 子产品Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 子产品单位
        /// </summary>
        public string ProductUnit { get; set; }
        /// <summary>
        /// 子产品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public List<ProductItemPriceVO> ProductPriceList { get; set; }

    }
}

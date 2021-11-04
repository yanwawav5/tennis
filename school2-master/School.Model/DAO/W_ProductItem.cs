using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_ProductItem : BaseEntity
    {
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
        public string ProductId { get; set; }
    }
}

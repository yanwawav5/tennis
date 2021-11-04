using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_ProductItemPrice : BaseEntity
    {
        public string ProductItemId { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// 1为表格第一种计算，2为表格第二中计算访视
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 1为普通*
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 最高价格
        /// </summary>
        public decimal TopPrice { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string ProductUnit { get; set; }
    }
}

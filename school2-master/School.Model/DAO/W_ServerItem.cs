using school.Model.DAO.Base;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_ServerItem : BaseEntity
    {
        [References(typeof(W_Product))]
        public string ProductId { get; set; }
        [References(typeof(W_ServerMain))]
        public string ServerMainId { get; set; }
        public string UserInfoId { get; set; }
        public int Areas { get; set; }
        public int Num { get; set; }
        public int Sort { get; set; }
        [References(typeof(W_ProductItem))]
        public string ProductItemId { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }
        [Reference]
        public virtual W_ProductItem W_ProductItem { get; set; }
        [Reference]
        public virtual W_Product W_Product { get; set; }
       
     

        [Reference]
        public virtual W_ServerMain W_ServerMain { get; set; }
    }
}

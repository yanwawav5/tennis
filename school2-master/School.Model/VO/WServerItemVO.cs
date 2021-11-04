using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model
{
    public class WServerItemVO
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        /// <summary>
        /// 子产品名称
        /// </summary>
        public string ProductItemId { get; set; }
        public string ServerMainId { get; set; }
        public string UserInfoId { get; set; }
        public int Areas { get; set; }
        public int Num { get; set; }
        public int Sort { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }
        public W_ProductItem ProductItemModel { get; set; }
    }
}

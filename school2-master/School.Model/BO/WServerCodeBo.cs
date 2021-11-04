using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class WServerCodeBo
    {
        public string Code { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; } 
        /// <summary>
        /// 1。元/H    2.元/平   3 ，元/台
        /// </summary>
        public int ServerType { get; set; }
    } 

}

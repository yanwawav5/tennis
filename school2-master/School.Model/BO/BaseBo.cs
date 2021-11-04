using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class BaseBo
    {

        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Deleted { get; set; }
        /// <summary>
        /// 起始页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
    }
}

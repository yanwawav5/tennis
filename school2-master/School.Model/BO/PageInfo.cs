using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
   public class PageInfo
    {
        /// <summary>
        /// 起始页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int TotNum { get; set; }
    }
    public class PageInfoKey:PageInfo
    { 
        public string Key { get; set; }
    }
}

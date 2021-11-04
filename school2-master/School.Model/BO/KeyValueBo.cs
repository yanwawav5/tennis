using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class KeyValueBo
    {
        /// <summary>
        /// key
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// value
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  
        /// </summary>
        public int flag { get; set; }
    }

    public class IdNum
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
    }
}

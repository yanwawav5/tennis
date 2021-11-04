using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO
{
    public class ApiHeader
    {
        /// <summary>
        /// SessionKey
        /// </summary>
        public string UserKey { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string LangKey { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string EquipmentCode { get; set; }
        /// <summary>
        /// API之间调用的密钥
        /// </summary>
        public string AppKey { get; set; }
    }
}

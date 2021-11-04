using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class LogItem
    {
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; }
        /// <summary>
        /// 相应时间
        /// </summary>
        public DateTime ResponseTime { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public double ExcuteTimes { get; set; }
        /// <summary>
        /// 客户端Ip
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 请求URL
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// API名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ApiHeader
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 请求内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 相应内容
        /// </summary>
        public string Result { get; set; }
    }
}

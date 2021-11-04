using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseLog
    {
        /// <summary>
        /// 日志类型
        /// </summary>
        public string LogType { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public object Name { get; set; }
        /// <summary>
        /// 相应内容
        /// </summary>
        public object Result { get; set; }

    }
}

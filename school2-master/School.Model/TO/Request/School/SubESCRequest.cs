using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class SubESCRequest : AbsRequest
    {
        public List<string> Id { get; set; }
        /// <summary>
        /// 0学生1老师
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 3.取消成功,4是退款成功
        /// </summary>
        public int Status { get; set; }
    }
}

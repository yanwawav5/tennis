using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class SubRequest : AbsRequest
    {
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolId { get; set; }
        /// <summary>
        /// 场地编号
        /// </summary>
        public string FieldId { get; set; }
        /// <summary>
        /// 0教学，1订场和活动
        /// </summary>
        public int SelType { get; set; }
       /// <summary>
       /// 日期
       /// </summary>
        public int Day { get; set; }

        public int FutureDay { get; set; }
    }
}

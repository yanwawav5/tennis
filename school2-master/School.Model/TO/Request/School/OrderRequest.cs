using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class OrderRequest:AbsRequest
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 代金卷
        /// </summary>
        public string StudentKjId { get; set; }
        /// <summary>
        /// 0学生1内部
        /// </summary>
        public int Category { get; set; }
        public List<OrederInfo> List { get; set; }
    }
    public class OrederInfo
    {
        public string StudentId { get; set; }
        public string FieldId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
       // public string Price { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 预订时间
        /// </summary>
        public List<int> HourList { get; set; }
    }
}

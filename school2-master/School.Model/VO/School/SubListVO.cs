using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class SubListVO
    {
        public List<SubVO> HourList { get; set; }
    }

    public class SubVO
    {
        public string SchoolName { get; set; }
        /// <summary>
        /// 场地编号
        /// </summary>
        public string FieldId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 夜场价格
        /// </summary>
        public string Pricesec { get; set; }

        /// <summary>
        /// 时间点
        /// </summary>
        public int sorttime { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 预定时间
        /// </summary>
        public List<int> HourList { get; set; }
    }

     
}

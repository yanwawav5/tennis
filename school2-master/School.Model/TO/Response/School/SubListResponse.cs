using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class SubListResponse : AbsResponse
    {
        public List<SubVO> List { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public int Hour { get; set; }
        /// <summary>
        /// 白天价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 晚上价格
        /// </summary>
        public string PriceSec { get; set; }
    }
}

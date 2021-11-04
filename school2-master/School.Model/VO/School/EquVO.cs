using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class ListEquVO
    {
        public List<EquVO> List { get; set; }
        public int TotNum { get; set; }
        public string AllTot { get; set; }
    }
    public class EquVO
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UserTime { get; set; }
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolId { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }
    }
}

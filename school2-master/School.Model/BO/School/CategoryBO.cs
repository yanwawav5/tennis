using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class CategoryBO
    {
        public string UserId { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }
    }
}

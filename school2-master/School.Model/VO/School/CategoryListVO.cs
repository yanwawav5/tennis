using school.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class CategoryListVO
    {
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
        public string Category { get; set; }
        public List<CategoryList1> OneList { get; set; }
    }
    public class CategoryList1
    {
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
        public string Category { get; set; }
        public List<CategoryList2> Twolist { get; set; }
    }
    public class CategoryList2
    {
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
        public string Category { get; set; }
    }

    public class CategoryListAllVO
    {
        public List<M_Category> OneList { get; set; }

        public List<M_Category> TwoList { get; set; }

        public List<M_Category> ThrList { get; set; }
    }
}

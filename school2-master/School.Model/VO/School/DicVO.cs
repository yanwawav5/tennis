using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class DicVO
    {
        public string Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 课程名分类（1，课程内容分类 ）
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public string PaterId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
  
    public  class M_Dic : BaseModel
    {
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
        public string VideoUrl { get; set; }

        /// <summary>
        /// 技术要点
        /// </summary>
        public string Points { get; set; }

        /// <summary>
        /// 第一个条件
        /// </summary>
        public string OneCategory { get; set; }
        /// <summary>
        /// 第二个条件
        /// </summary>
        public string TwoCategory { get; set; }
        /// <summary>
        /// 第三个条件
        /// </summary>
        public string ThrCategory { get; set; }

    }
}

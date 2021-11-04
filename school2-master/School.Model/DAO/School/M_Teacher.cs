using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class M_Teacher : BaseModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }


        [References(typeof(M_Student))]
        public string StudentId { get; set; }
        [Reference]
        public virtual M_Student M_Student { get; set; } 
        /// <summary>
        /// 评价
        /// </summary>
        public int Valuation { get; set; }
        /// <summary>
        /// 教龄
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string SubMain { get; set; }

        public string Tel { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }

    }
}

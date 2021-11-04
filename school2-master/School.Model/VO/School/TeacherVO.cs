using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class TeacherVO
    {
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; } 
        /// <summary>
        /// 老师编号
        /// </summary>
        public string StudentId { get; set; } 
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
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
    }
}

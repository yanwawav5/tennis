using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{

    public class M_Course : BaseModel
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 课程名分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 年龄分类
        /// </summary>
        public int AgeCategoryId { get; set; }
        /// <summary>
        /// 课时（如2h-节）
        /// </summary>
        public string HourInfo { get; set; }
        /// <summary>
        /// 价格（元）
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 课时（如10）
        /// </summary>
        public string ClassTimes { get; set; } 
        /// <summary>
        /// 人数
        /// </summary>
        public string PeoperNum { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Book { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string SubMain { get; set; }
    }
}

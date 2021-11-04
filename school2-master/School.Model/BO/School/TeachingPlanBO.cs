using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class TeachingPlanBO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
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
        /// 目标
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        ///  装备
        /// </summary>
        public string Equip { get; set; }
        /// <summary>
        /// 下节课
        /// </summary>
        public string NextTitle { get; set; }
        /// <summary>
        /// 视频编号
        /// </summary>
        public string VideoId { get; set; }
        /// <summary>
        /// 下节课视频
        /// </summary>
        public string NextVideoId { get; set; }
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
        public string UserId { get; set; }
        /// <summary>
                                           /// 技术要点
                                           /// </summary>
        public string NextPoints { get; set; }
        /// <summary>
        /// 作业要点
        /// </summary>
        public string JobPoints { get; set; }
        /// <summary>
        /// 作业视频
        /// </summary>
        public string JobVideoId { get; set; }

        public List<TeachingPlanInfoBO> List { get; set; }
    }
}

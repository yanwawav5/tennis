using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class TeachingPlanVO
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
        /// 本节课视频
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
        /// <summary>
        /// 视频名称
        /// </summary>
        public string VideoName { get; set; }
        /// <summary>
        /// 视频名称
        /// </summary>
        public string NextVideoName { get; set; }
        /// <summary>
        /// 下一个视频图片
        /// </summary>
        public string NextPic { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public string VideoUrl { get; set; }
        /// <summary>
        /// 下一个视频地址
        /// </summary>
        public string NextVideoUrl { get; set; }

        /// <summary>
        /// 技术要点
        /// </summary>
        public string NextPoints { get; set; }
        /// <summary>
        /// 作业要点
        /// </summary>
        public string JobPoints { get; set; }
        /// <summary>
        /// 作业视频编号
        /// </summary>
        public string JobVideoId { get; set; }

        /// <summary>
        /// 作业视频URL
        /// </summary>
        public string JobVideoUrl { get; set; }

        /// <summary>
        /// 作业视频名称
        /// </summary>
        public string JobVideoName { get; set; }
    }



    public class TeachingPlanShowMVCVO
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
        /// 本节课视频
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
        /// <summary>
        /// 视频名称
        /// </summary>
        public string VideoName { get; set; }
        /// <summary>
        /// 视频名称
        /// </summary>
        public string NextVideoName { get; set; }
        /// <summary>
        /// 下一个视频图片
        /// </summary>
        public string NextPic { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public string VideoUrl { get; set; }
        /// <summary>
        /// 下一个视频地址
        /// </summary>
        public string NextVideoUrl { get; set; }

        /// <summary>
        /// 技术要点
        /// </summary>
        public string NextPoints { get; set; }
        /// <summary>
        /// 作业要点
        /// </summary>
        public string JobPoints { get; set; }
        /// <summary>
        /// 作业视频编号
        /// </summary>
        public string JobVideoId { get; set; }

        /// <summary>
        /// 作业视频URL
        /// </summary>
        public string JobVideoUrl { get; set; }

        /// <summary>
        /// 作业视频名称
        /// </summary>
        public string JobVideoName { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string ClassName { get; set; }
       /// <summary>
       /// 日期
       /// </summary>
        public int Day { get; set; } 
        /// <summary>
        /// 时间
        /// </summary>
        public string TeacherCourseTime { get; set; }
        public List<planItemMVCVO> ItemList { get; set; }
    }

    public class planItemMVCVO
    {
        public string Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 教案编号
        /// </summary> 
        public string TeachingPlanId { get; set; }
        /// <summary>
        /// 时间段
        /// </summary>
        public string TimeSlot { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public string TimeLength { get; set; }

        /// <summary>
        /// 组织安全
        /// </summary>
        public string Org { get; set; }

        /// <summary>
        /// 视频编号
        /// </summary>
        public string VideoId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 子分类
        /// </summary>
        public string CategoryItemId { get; set; }

        public string CategoryName { get; set; }
        public string CategoryItemName { get; set; }
        /// <summary>
        /// 视频名称
        /// </summary>
        public string VideoName { get; set; }
        /// <summary>
        /// 技术要点
        /// </summary>
        public string Points { get; set; }

    }
}

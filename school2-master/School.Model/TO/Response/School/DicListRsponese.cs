using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class DicListRsponese:AbsResponse
    {
        public List<DicMainVO> List { get; set; }
    }

    public class DicItemRsponese : AbsResponse
    {
        public DicItemVO Model { get; set; }
    }

    public class DicMainVO
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
        /// 视频
        /// </summary>
        public string VideoUrl { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public string PaterId { get; set; }
        /// <summary>
        /// 技术要点
        /// </summary>

        public string Points { get; set; }
        public List<DicItemVO> ItemList { get; set; }
    }
    public class DicItemVO
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
        /// 视频
        /// </summary>
        public string VideoUrl { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 技术要点
        /// </summary>

        public string Points { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public string PaterId { get; set; } 
        public DateTime? CreateTime { get; set; }


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


    public class MDicListRsponese : AbsResponse
    {
        public List<M_Dic> List { get; set; }
    }


    public class DicItemVO2
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
        /// 视频
        /// </summary>
        public string VideoUrl { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 技术要点
        /// </summary>

        public string Points { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public string PaterId { get; set; }
        public DateTime? CreateTime { get; set; }


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

        public List<M_Category> OneList { get; set; }

        public List<M_Category> TwoList { get; set; }

        public List<M_Category> ThrList { get; set; }
        /// <summary>
        /// 视频列表
        /// </summary>
        public List<VideoVO> VideoList { get; set; }
    }
}

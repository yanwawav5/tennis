using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{

    public class M_StudentCourse : BaseModel
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 状态(0待支付，1支付成功,2，申请退款，3退款成功)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 价格（元）
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 课时（如10节）
        /// </summary>
        public int ClassTimes { get; set; }
        /// <summary>
        /// 课程名分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 年龄分类
        /// </summary>
        public int AgeCategoryId { get; set; }
        /// <summary>
        /// 学生姓名(购买)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 学生电话(购买)
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 年龄类别
        /// </summary>
        public int UserCategoryId { get; set; }
        /// <summary>
        /// 技能水平
        /// </summary>
        public int TennisId { get; set; }
        /// <summary>
        /// 剩余课时
        /// </summary>
        public int SurplusClassTimes { get; set; }

        /// <summary>
        /// 秀一秀
        /// </summary>
        public string CourseVideoUrl { get; set; }

        /// <summary>
        /// 添加课时数量
        /// </summary>
        public int AddClassTimes { get; set; }
        /// <summary>
        /// 卡卷
        /// </summary>
        [References(typeof(W_StudentKj))]
        public string StudentKjId { get; set; }
        [Reference]
        public virtual W_StudentKj W_StudentKj { get; set; }



        /// <summary>
        /// 学生编号
        /// </summary>
        [References(typeof(M_Student))]
        public string StudentId { get; set; }
        [Reference]
        public virtual M_Student M_Student { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary>
        [References(typeof(M_Course))]
        public string CourseId { get; set; }
        [Reference]
        public virtual M_Course M_Course { get; set; }

        /// <summary>
        /// 0未支付，1已付款
        /// </summary>
        public int ispay { get; set; }

    } 
}

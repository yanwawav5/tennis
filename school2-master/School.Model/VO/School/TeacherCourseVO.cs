using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class TeacherCourseVO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        ///<summary>
        /// 老师编号
        /// </summary> 
        public string StudentId { get; set; }
        ///<summary>
        /// 场地编号
        /// </summary> 
        public string FieldId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Book { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 详情
        /// </summary>
        public string Main { get; set; }

        /// <summary>
        /// 课程名分类(小班课程，双人课程)
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 年龄分类（精灵）
        /// </summary>
        public int AgeCategoryId { get; set; }
    }

    public class TeacherInfoVO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Main { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

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
        /// 头像
        /// </summary>
        public string HeadImgUrl { get; set; }

        public string Tel { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
    }


    public class TeacherCourseShowVO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Book { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 场地名称
        /// </summary>
        public string FieldId { get; set; }
        /// <summary>
        /// 场地名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string TeacherCourseTime { get; set; }
        /// <summary>
        /// 课程名分类(小班课程，双人课程)
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 年龄分类（精灵）
        /// </summary>
        public int AgeCategoryId { get; set; }
        /// <summary>
        /// 课程状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        ///  学校电话
        /// </summary>
        public string SchoolTel { get; set;}
        /// <summary>
        /// 学校地址
        /// </summary>
        public string SchoolAddress { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }

        /// <summary>
        /// 教练名称
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string TeacherTel { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string TeachingPlanId { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateUserId { get; set; }
    }

    public class SubTeacherShowVO
    {
        public string Id { get; set; }
        public List<string> Hour { get; set; }
    }
}

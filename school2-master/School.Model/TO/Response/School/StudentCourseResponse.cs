using school.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class StudentCourseResponse : AbsResponse
    {
        public StudentCourseOneVO2 Model { get; set; }
    }
    public class StudentCourseOneVO2
    {
        public string Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 价格（元）
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 课时（如10节）
        /// </summary>
        public string ClassTimes { get; set; }
        /// <summary>
        /// 课程名分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 年龄分类
        /// </summary>
        public int AgeCategoryId { get; set; }

        public string Name { get; set; }
        public string Tel { get; set; }
        public int UserCategoryId { get; set; }
        public int TennisId { get; set; }
        /// <summary>
        /// 剩余课时
        /// </summary>
        public string SurplusClassTimes { get; set; }
        /// <summary>
        /// 卡卷
        /// </summary> 
        public string StudentKjId { get; set; }

        /// <summary>
        /// 学生编号
        /// </summary> 
        public string StudentId { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary> 
        public string CourseId { get; set; }

        public string CourseName { get; set; }
        /// <summary>
        /// 卡卷金额
        /// </summary>
        public string KjPrice { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 学生手机
        /// </summary>
        public string StudentTel { get; set; }
        /// <summary>
        /// 添加课时数量
        /// </summary>
        public int AddClassTimes { get; set; }
    }

    public class StudentCourseOneVO
    {
        public string Id { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 状态
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

        public string Name { get; set; }
        public string Tel { get; set; }
        public int UserCategoryId { get; set; }
        public int TennisId { get; set; }
        /// <summary>
        /// 剩余课时
        /// </summary>
        public int SurplusClassTimes { get; set; }
        /// <summary>
        /// 卡卷
        /// </summary> 
        public string StudentKjId { get; set; }

        /// <summary>
        /// 学生编号
        /// </summary> 
        public string StudentId { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary> 
        public string CourseId { get; set; }

        public string CourseName { get; set; }
        /// <summary>
        /// 卡卷金额
        /// </summary>
        public string KjPrice { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 学生手机
        /// </summary>
        public string StudentTel { get; set; }
        /// <summary>
        /// 添加课时数量
        /// </summary>
        public int AddClassTimes { get; set; }
    }

    public class StudentCourseListResponse : AbsResponse
    {
        public List<StudentCourseVO> List { get; set; }


        public int TotNum { get; set; }
    }



    public class StudentCourseVO
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
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 学生编号
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary> 
        public string CourseId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 价格（元）
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 课时（如10节）
        /// </summary>
        public string ClassTimes { get; set; }
        /// <summary>
        /// 课程名分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 年龄分类
        /// </summary>
        public int AgeCategoryId { get; set; }
        /// <summary>
        /// 剩余课时
        /// </summary>
        public string SurplusClassTimes { get; set; }
        /// <summary>
        /// 购买日期
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 学生手机
        /// </summary>
        public string StudentTel { get; set; }
        /// <summary>
        /// 体验课秀一秀
        /// </summary>
        public string CourseVideoUrl { get; set; }
        /// <summary>
        /// 添加课时数量
        /// </summary>
        public int AddClassTimes { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserName { get; set; }
    }

}

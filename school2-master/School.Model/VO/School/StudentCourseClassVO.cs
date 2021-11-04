using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class StudentCourseClassVO
    {
        public string TeacherCourseId { get; set; }
        public string StudentId { get; set; }
        public string TeacherId { get; set; }
        public string Id { get; set; }
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 老师姓名
        /// </summary>
        public string TeacherName { get; set; }
        /// <summary>
        /// 学生电话
        /// </summary>
        public string StudentTel { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 学生状态
        /// </summary>
        public int StudentStars { get; set; }
        /// <summary>
        /// 老师评价
        /// </summary>
        public string TeacherEvaluate { get; set; }
        /// <summary>
        /// 学生给老师拼接
        /// </summary>
        public string StudentEvaluate { get; set; }
        /// <summary>
        /// 作业评价
        /// </summary>
        public string JobEvaluate { get; set; }
        /// <summary>
        /// 作业名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 学生上传作业
        /// </summary>
        public string StudentJobUrl { get; set; }
        /// <summary>
        /// 作业视频编号
        /// </summary>
        public string JobTempVideoId { get; set; }
        /// <summary>
        /// 作业视频
        /// </summary>
        public string JobTempVideoUrl { get; set; }
        /// <summary>
        /// 作业状态
        /// </summary>
        public string JobStatus { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string TeacherCourseName { get; set; }
        /// <summary>
        /// 课程时间
        /// </summary>
        public  string TeacherCourseTime { get; set; } 
        /// <summary>
        /// 课程日期
        /// </summary>
        public int Day { get; set; } 
        /// <summary>
        /// 老师电话
        /// </summary>
        public string TeacherTel { get; set; }
        /// <summary>
        /// 教练图片
        /// </summary>
        public string TeacherPic { get; set; }
        
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }
        /// <summary>
        /// 场地名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StudentLeave { get; set; }
        
        /// <summary>
        /// 作业要点
        /// </summary>
        public string JobPoints { get; set; }
        /// <summary>
        /// 秀一秀
        /// </summary>
        public string CourseVideoUrl { get; set; }

        /// <summary>
        /// 训练时长
        /// </summary>
        public string TrainTime { get; set; }
        /// <summary>
        /// 训练组数
        /// </summary>
        public string TrainNum { get; set; }
        // public string SchoolId { get; set; }
        // /// <summary>
        // /// 场地姓名
        // /// </summary>
        // public string SchoolName { get; set; }
        // public string Id { get; set; }
        // /// <summary>
        // /// 学生编号
        // /// </summary>
        // public string StudentId { get; set; }
        // /// <summary>
        // /// 场地编号
        // /// </summary>
        // public string FieldId { get; set; }
        // /// <summary>
        // /// 学生
        // /// </summary>
        // public string StudentName { get; set; } 
        // /// <summary>
        // /// 学校电话
        // /// </summary>
        // public string SchoolTel { get; set; }
        // /// <summary>
        // /// 场地图片
        // /// </summary>
        // public string Pic { get; set; }
        // /// <summary>
        // /// 创建时间
        // /// </summary>
        // public string CreateTime { get; set; }
        ///// <summary>
        ///// 场地
        ///// </summary>
        // public string FieldName { get; set; }

        // /// <summary>
        // /// 价格
        // /// </summary>
        // public string Price { get; set; }
        // /// <summary>
        // /// 时间
        // /// </summary>
        // public int Day { get; set; }
        // /// <summary>
        // /// 状态
        // /// </summary>
        // public int Status { get; set; }
        // /// <summary>
        // /// 小时列表
        // /// </summary>
        // public List<int> HourList { get; set; }
        // /// <summary>
        // ///姓名
        // /// </summary>
        // public string Name { get; set; }
        // /// <summary>
        // /// 电话
        // /// </summary>
        // public string Tel { get; set; }

        // /// <summary>
        // /// 用户手机
        // /// </summary>
        // public string StudentTel { get; set; }

        // public string X { get; set; }
        // public string Y { get; set; }

    }
}

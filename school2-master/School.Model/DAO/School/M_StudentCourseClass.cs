using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{

    public class M_StudentCourseClass : BaseModel
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        [References(typeof(M_TeacherCourse))]
        public string TeacherCourseId { get; set; }
        [Reference]
        public virtual M_TeacherCourse M_TeacherCourse { get; set; } 

        [References(typeof(M_Student))]
        public string StudentId { get; set; }
        [Reference]
        public virtual M_Student M_Student { get; set; }
        
        public string TeacherId { get; set; }

        /// <summary>
        /// 课程名分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 年龄分类
        /// </summary>
        public int AgeCategoryId { get; set; }

        /// <summary>
        /// 学校编号
        /// </summary>
        public string SChoolId { get; set; }
        /// <summary>
        /// 场地编号
        /// </summary>
        public string FieldId { get; set; }
        /// <summary>
        /// 课程状态（0默认1已完成，2取消（老师取消，学生请假成功，学生自己取消）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 学生评价星星(老师给学生)
        /// </summary>
        public int StudentStars { get; set; }
        /// <summary>
        /// 老师评价( 学生给老师)
        /// </summary>
        public string TeacherEvaluate { get; set; }
        /// <summary>
        /// 学生评价（老师给学生）
        /// </summary>
        public string StudentEvaluate { get; set; }
        /// <summary>
        /// 作业评价
        /// </summary>
        public string JobEvaluate { get; set; }
        /// <summary>
        /// 作业名称(老师布置作业名称)
        /// </summary>
        public string JobName { get; set; }  
        /// <summary>
        /// 作业视频(学生上传)
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
        /// 作业图片
        /// </summary>
        public string JobTempPic { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 作业要点
        /// </summary>
        public string JobPoints { get; set; }

        /// <summary>
        /// 学生付费课程编号
        /// </summary>
        public string StudentCourseId { get; set; }
        /// <summary>
        /// 作业状态
        /// </summary>
        public string JobStatus { get; set; }
         
        /// <summary>
        /// 日期
        /// </summary>
        public int Day { get; set; }
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
    }
}

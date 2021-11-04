using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    /// <summary>
    /// 老师评价
    /// </summary>
    public class JobStudentTeacherEvaluateRequest : AbsRequest
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 学生评价给老师星星
        /// </summary>
        public string StudentEvaluate { get; set; }
    }


    /// <summary>
    /// 老师评价
    /// </summary>
    public class CourseVideoUrlRequest : AbsRequest
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 秀一秀
        /// </summary>
        public string CourseVideoUrl { get; set; }
    }
    /// <summary>
    /// 作业拼接
    /// </summary>
    public class JobEvaluateRequest : AbsRequest
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 学生评价给老师星星
        /// </summary>
        public string JobEvaluate { get; set; }
    }
    /// <summary>
    /// 学生评价
    /// </summary>
    public class JobStudentStudentEvaluateRequest : AbsRequest
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 学生星星
        /// </summary>
        public int StudentStars { get; set; }
        /// <summary>
        /// 学生评价
        /// </summary>
        public string TeacherEvaluate { get; set; }
    }

    /// <summary>
    /// 完成作业
    /// </summary>
    public class JobStudentStudentAddJobRequest : AbsRequest
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public string VideoUrl { get; set; }
    }
    /// <summary>
    /// 布置作业
    /// </summary>
    public class JobStudentTeacherAddJobRequest : AbsRequest
    {
        /// <summary>
        /// 课程编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 作业名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 视频编号
        /// </summary>
        public string VideoId { get; set; }
        /// <summary>
        /// 作业要点
        /// </summary>
        public string JobPoints { get; set; }

        /// <summary>
        /// 训练组数
        /// </summary>
        public string trainNum { get; set; }

        /// <summary>
        /// 训练时长
        /// </summary>
        public string trainTime { get; set; }
    }
}


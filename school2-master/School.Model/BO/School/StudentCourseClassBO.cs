using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class StudentCourseClassBO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        public string UserId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
         
        public string TeacherCourseId { get; set; }   
        public string StudentId { get; set; } 
        public string TeacherId { get; set; }

        public int Status { get; set; }

        ///// <summary>
        ///// 学生状态
        ///// </summary>
        //public int StudentStars { get; set; }
        ///// <summary>
        ///// 老师评价
        ///// </summary>
        //public string TeacherEvaluate { get; set; }
        ///// <summary>
        ///// 学生给老师拼接
        ///// </summary>
        //public string StudentEvaluate { get; set; }
        ///// <summary>
        ///// 作业评价
        ///// </summary>
        //public string JobEvaluate { get; set; }
        ///// <summary>
        ///// 作业名称
        ///// </summary>
        //public string JobName { get; set; }
        ///// <summary>
        ///// 作业视频编号
        ///// </summary>
        //public string JobTempVideoId { get; set; }
        ///// <summary>
        ///// 作业视频
        ///// </summary>
        //public string JobTempVideoUrl { get; set; }


        /// <summary>
        /// 学生评价星星(作业)
        /// </summary>
        public int StudentStars { get; set; }
        /// <summary>
        /// 老师评价( 学生给老师)
        /// </summary>
        public string TeacherEvaluate { get; set; }
        /// <summary>
        /// 学生给老师评价（老师给学生）
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
        /// 作业视频图片
        /// </summary>
        public string JobTempPic { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }


    }

    public class StudentCourseClassQueryBO:PageInfo
    {
        public string TeacherCourseId { get; set; }
        /// <summary>
        /// 0待完成，1已完成，2已取消
        /// </summary>
        public string Status { get; set; }
        public string StudentId { get; set; }
        /// <summary>
        /// 0查看所有，1查看作业,2布置作业,3作业已经上传，4.作业已经评价
        /// </summary>
        public int Flg { get; set; }
    }
}

using school.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class StudentCourseRequest : PageInfo
    {
        /// <summary>
        /// 课程名分类
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 状态0正常1体验
        /// </summary>
        public int Normal { get; set; }
        /// <summary>
        /// 年龄分类
        /// </summary>
        public string AgeCategoryId { get; set; }
        /// <summary>
        /// 状态（0未使用，1已使用，2申请退款，3退款成功）
        /// </summary>
        public string Status { get; set; } 
        /// <summary>
        /// 是否学生（true学生)
        /// </summary>
        public bool IsStudent { get; set; }
    }


    public class StudentCourseAddRequest
    {
        /// <summary>
        /// 课程名分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 年龄分类
        /// </summary>
        public int AgeCategoryId { get; set; }
        /// <summary>
        /// 状态（0未付款，1付款）
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 用户年龄
        /// </summary>
        public int UserCategoryId { get; set; }
        /// <summary>
        /// 网球水平
        /// </summary>
        public int TennisId { get; set; }

        /// <summary>
        /// 卡卷
        /// </summary> 
        public string StudentKjId { get; set; }
        /// <summary>
        /// 课程编号
        /// </summary> 
        public string CourseId { get; set; }
        /// <summary>
        ///  价格
        /// </summary>
        public string Price { get; set; }


    }


    public class StudentCourse2Request
    { 
        /// <summary>
        /// 状态（0未付款，1付款）
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; } 
        /// <summary>
        /// 课程编号
        /// </summary> 
        public string CourseId { get; set; }
        /// <summary>
        ///  价格
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// 课时（如10节）
        /// </summary>
        public int ClassTimes { get; set; }
        /// <summary>
        /// 添加课时数量
        /// </summary>
        public int AddClassTimes { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
    }


    public class StudentCour3Request
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 修改的课程
        /// </summary> 
        public string CourseId { get; set; }
        /// <summary>
        ///  分类
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        ///  年龄分类
        /// </summary>
        public int AgeCategoryId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 剩余课时
        /// </summary>
        public string SurplusClassTimes { get; set; }
    }
}

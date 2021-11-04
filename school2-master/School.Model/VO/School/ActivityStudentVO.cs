using school.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class ActivityStudentVO
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 学生编号
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 活动编号
        /// </summary>
        public string ActivityId { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public string Price
        {
            get; set;
        }
        /// <summary>
        /// 状态0默认不报名，1报名成功，2取消,3教練同意，4退款成功,5完成
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 报名学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 报名学生电话
        /// </summary>
        public string StudentTel { get; set; }
        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        ///  头像
        /// </summary>
        public string HeadImgUrl { get; set; } 

    }
    public class StudentActivityStudentVO
    {

        public M_Activity ActivityModel { get; set; }
        public ActivityStudentVO Model { get; set; }

        public int EnrollNum { get; set; }
        public int SurplusNum { get; set; }
    }


    public class TeackerActivityStudentVO
    {
        public ActivityVO Model { get; set; }
        public List<ActivityStudentVO> List { get; set; }
    }
}

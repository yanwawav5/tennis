using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.Enum
{
    #region 请假状态
    public enum LeaveStatusEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 请假
        /// </summary>
        Ask = 1,
        /// <summary>
        /// 成功
        /// </summary>
        Suc = 2,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 3,
        /// <summary>
        /// 取消
        /// </summary>
        Esc = 4
    }
    #endregion

    #region 留言
    public enum MessageToEnum
    {
        /// <summary>
        /// 给老师
        /// </summary>
        ToTeacher = 1,
        /// <summary>
        /// 给学生
        /// </summary>
        ToStudent = 2
    }
    #endregion

    #region 课程
    public enum TeacherCourseEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 取消
        /// </summary>
        Esc = 2
    }
    #endregion


    #region 付款
    public enum PayStatusEnum
    {
        /// <summary>
        /// 待支付
        /// </summary>
        UnPay=0,
        /// <summary>
        /// 已支付
        /// </summary>
        Normal = 1,
        
    }
    #endregion
}

using school.IBLL.Base;
using school.Model;
using school.Model.BO;
using school.Model.DAO;
using school.Model.TO.Request;
using school.Model.TO.Response;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace school.IBLL
{
    public interface IStudentCourseBLL : IBaseBLL
    {
        /// <summary>
        /// 保存或更新
        /// </summary>
        string CreateOrUpdateCourse(StudentCourseBO modelBo);
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        StudentCourseOneVO GetOneCourse(string Id);
        StudentCourseOneVO2 GetOneCourse2(string Id);
        /// <summary>
        /// 用于订单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        M_StudentCourse GetOneStudentCourseByIdSi(string Id);
        /// <summary>
        /// 获取多个
        /// </summary>
        /// <returns></returns>
        List<StudentCourseVO> GetListCourse(StudentCourseListBO bo);
        /// <summary>
        /// 修改学生课程
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo UpdateStudentCourse(StudentCourseBO modelBo);
        /// <summary>
        /// 体验课秀一秀
        /// </summary>
        /// <param name="id"></param>
        /// <param name="videoUrl"></param>
        /// <param name="uerId"></param>
        /// <returns></returns>
        AmountBo SaveTyCouserVideoUrl(string id, string videoUrl, string uerId);
        StudentLeaveVO GetOneLeave(string Id);
        AmountBo CreateOrUpdateLeave(StudentLeaveBO modelBo);
        List<StudentLeaveVO> GetListLeave(StudentLeaveListBO bo);

        /// <summary>
        /// 请假状态修改
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo UpdateLeaveStauts(string id, int status, string userId);
        List<StudentCourseClassVO> GetListStudentCourseClass(StudentCourseClassQueryBO bo);
        AmountBo CreateOrUpdateStudentClass(string studentId, string teachercourseId, string courseId);
        StudentCourseClassVO GetOneStudentCourseClass(string Id);

        /// <summary>
        /// 取消课程后获取课程+1
        /// </summary>
        /// <param name="teachercourseId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        List<SqlItem> GetEscTeacherCourse(string teachercourseId, int status);
        /// <summary>
        /// 删除订购课程
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteStudentCourse(string Id);
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateStudentCourseStatus(string id, string status);


        #region 我的课程-约课
        /// <summary>
        /// 根据老师学校获取课程
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="schoolId"></param>
        /// <param name="addDay"></param>
        /// <returns></returns>
        List<TeacherCourseFieldVO> GetTeacherCourseByTeacherSchool(string teacherId, string schoolId, string studentCourseId, int addDay);
        /// <summary>
        /// 获取学校根据老师
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<KeyValueBo> GetSchoolByTeacherId(string Id);
        /// <summary>
        /// 获取老师
        /// </summary>
        /// <returns></returns>
        List<TeacherVO> GetTeacherList();
        #endregion

        #region  作业评价
        /// <summary>
        /// 学生给老师本课程评价
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        AmountBo SaveStudentEvaluate(string id, string teacherEvaluate, int studentStars, string uerId);


        /// <summary>
        /// 老师给学生本课程评价
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        AmountBo SaveTeacherEvaluate(string id, string studentEvaluate, string uerId);
        /// <summary>
        /// 秀一秀
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseVideoUrl"></param>
        /// <param name="uerId"></param>
        /// <returns></returns>
        AmountBo SaveCourseVideoUrl(string id, string courseVideoUrl, string uerId);

        AmountBo SaveJobEvaluate(string id, string JobEvaluate, string uerId);
        /// <summary>
        /// 学生提交作业
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        AmountBo SaveStudentAddJob(string id, string videoUrl, string uerId);


        /// <summary>
        /// 老师布置作业
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        AmountBo SaveTeacherAddJob(string id, string jobName, string videoId, string uerId, string jobPoints, string trainNum, string trainTime);
        #endregion

        #region 学生获取课表
        List<StudentCourseClassVO> GetListStudentCourseClassByStudent(StudentCourseClassQueryBO bo);
        /// <summary>
        /// 微信打卡页面
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<StudentCourseClassVO> GetListStudentCourseClassByStudentWX(StudentCourseClassQueryBO bo, int type = 1);
        #endregion

        #region 课程剩余
        ClassNumInfoVO GetClassNumInfo(string id);
        #endregion



        #region

        bool UpdateStudentIsPay(string id);
        AmountBo TransferStudentCourse(StudentCour3Request request);


        #endregion
    }
}

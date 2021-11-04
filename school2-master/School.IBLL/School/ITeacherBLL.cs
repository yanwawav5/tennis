using school.IBLL.Base;
using school.Model;
using school.Model.BO;
using school.Model.DAO;
using school.Model.TO.Response;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace school.IBLL
{
    public interface ITeacherBLL : IBaseBLL
    {
        /// <summary>
        /// 保存或更新
        /// </summary>
        AmountBo CreateOrUpdate(TeacherBO modelBo);
        /// <summary>
        /// 自己
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo UpdateTeacherInfoByZJ(TeacherBO2 modelBo);
        AmountBo UpdateTeacherInfo(TeacherBO2 modelBo);
        string login(string user, string pwd);
        /// <summary>
        /// 计算课程时间
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        List<string> GetTime(List<int> l);
        TeacherInfoVO GetOne(string id);
        List<TeacherInfoVO> GetList(TeacherListBO bo);
        /// <summary>
        /// 获取留言详细列表
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<TeacherMessageVO> GetTeacherMessageList(IdCategoryListBO bo);
        List<TeacherMessageMainVO> GetTeacherMessageMainList(IdCategoryListBO bo);
        /// <summary>
        /// 添加留言
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo CreateOrUpdateMessage(TeacherMessageBO modelBo);
       
        /// <summary>
        /// 获取字典项目
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        List<DicMainVO> GetDicList(int category,bool showAll=false);
        List<M_Dic> GetDicParentList(string parentId = "");
        List<M_Dic> GetDicAllList();
        /// <summary>
        /// 根据3个条件获取数据
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<M_Dic> GetListByCategorId(DicCategoryBO bo);
        AmountBo DeleteDic(string id);

        DicItemVO GetDicOne(string id);

        AmountBo CreateOrUpdateDic(DicBO modelBo);

        #region 教案
        /// <summary>
        /// 创建教案
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo CreateOrUpdateTeachingPlan(TeachingPlanBO modelBo);

        AmountBo DeleteTeachingPlan(string id);
        TeachingPlanVO GetTeachingPlan(string id);
        List<TeachingPlanVO> GetTeachingPlanList(IdListBO bo);

        AmountBo CreateOrUpdateTeachingPlanInfo(TeachingPlanInfoBO modelBo);
        List<TeachingPlanInfoVO> GetTeachingPlanInfoList(IdListBO bo);
        TeachingPlanInfoVO GetTeachingPlanInfo(string id);


        #region 视频
        VideoVO GetVideo(string id);
        List<VideoVO> GetVideoList(IdListBO bo);
        AmountBo CreateOrUpdateVideo(VideoBO modelBo);
        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        AmountBo DeleteVideo(string id, string userId);
        #endregion

        #region 课程表
        List<TeacherCourseShowVO> GetTeacherShowList(int day, string fieldId);


        List<TeacherCourseShowVO> GetTeacherShowListAll();
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TeacherCourseShowVO GetTeacherShow(string id);
        M_TeacherCourse GetTeacherCourseOne(string id);

        List<M_TeacherCourse> GetTeacherCourseList();
        #endregion

        #region 课程
        AmountBo CreateOrUpdateTeacherCourse(TeacherCourseBO modelBo);
        /// <summary>
        /// 取消课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AmountBo EscTeacherCourse(string id,string userId);
        #endregion

        #endregion

        #region 学员管理-》学员列表
        /// <summary>
        /// 获取老师下学生
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<TeacherStudentVO> GetStudentCourseListByTeacher(IdListBO bo);
        /// <summary>
        /// 获取学员作业列表
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<StudentCourseClassVO> GetListStudentCourseClassByTeacher(StudentCourseClassQueryBO bo);
        #endregion

        #region 活动
        /// <summary>
        /// 添加修改
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo CreateOrUpdateActivity(ActivityBO modelBo);
        /// <summary>
        /// 取消活动
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        AmountBo EscActivity(string id, string userId);
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActivityAllVO GetOneActivityVO(string id,bool isTeacher=false);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<ActivityVO> GetActivityList(IdActiStatusListBO bo);

        AmountBo UpdateActivity(string id, string userId, string status);


        string CreateOrUpdateActivityStudent(ActivityStudentBO modelBo);

        AmountBo UpdateActivityStudentStatus(string id, int status, string userId);
        M_ActivityStudent getOneOnlyActStu(string id);
       List<ActivityStudentVO> GetActivityStudentList(IdStatusListBO bo);

        ActivityStudentVO GetActivityStudentOne(string id);

        SubListReVO GetSubListById(string id, int category);

        List<StudentActivityStudentVO> GetActivityStudentListByStudent(IdStudentStatusListBO bo);
        TeackerActivityStudentVO GetActivityStudentByTeacher(string activityId);
        #endregion
    }
}

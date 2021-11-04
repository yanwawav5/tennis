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
        /// ��������
        /// </summary>
        AmountBo CreateOrUpdate(TeacherBO modelBo);
        /// <summary>
        /// �Լ�
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo UpdateTeacherInfoByZJ(TeacherBO2 modelBo);
        AmountBo UpdateTeacherInfo(TeacherBO2 modelBo);
        string login(string user, string pwd);
        /// <summary>
        /// ����γ�ʱ��
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        List<string> GetTime(List<int> l);
        TeacherInfoVO GetOne(string id);
        List<TeacherInfoVO> GetList(TeacherListBO bo);
        /// <summary>
        /// ��ȡ������ϸ�б�
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<TeacherMessageVO> GetTeacherMessageList(IdCategoryListBO bo);
        List<TeacherMessageMainVO> GetTeacherMessageMainList(IdCategoryListBO bo);
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo CreateOrUpdateMessage(TeacherMessageBO modelBo);
       
        /// <summary>
        /// ��ȡ�ֵ���Ŀ
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        List<DicMainVO> GetDicList(int category,bool showAll=false);
        List<M_Dic> GetDicParentList(string parentId = "");
        List<M_Dic> GetDicAllList();
        /// <summary>
        /// ����3��������ȡ����
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<M_Dic> GetListByCategorId(DicCategoryBO bo);
        AmountBo DeleteDic(string id);

        DicItemVO GetDicOne(string id);

        AmountBo CreateOrUpdateDic(DicBO modelBo);

        #region �̰�
        /// <summary>
        /// �����̰�
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


        #region ��Ƶ
        VideoVO GetVideo(string id);
        List<VideoVO> GetVideoList(IdListBO bo);
        AmountBo CreateOrUpdateVideo(VideoBO modelBo);
        /// <summary>
        /// ɾ����Ƶ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        AmountBo DeleteVideo(string id, string userId);
        #endregion

        #region �γ̱�
        List<TeacherCourseShowVO> GetTeacherShowList(int day, string fieldId);


        List<TeacherCourseShowVO> GetTeacherShowListAll();
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TeacherCourseShowVO GetTeacherShow(string id);
        M_TeacherCourse GetTeacherCourseOne(string id);

        List<M_TeacherCourse> GetTeacherCourseList();
        #endregion

        #region �γ�
        AmountBo CreateOrUpdateTeacherCourse(TeacherCourseBO modelBo);
        /// <summary>
        /// ȡ���γ�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AmountBo EscTeacherCourse(string id,string userId);
        #endregion

        #endregion

        #region ѧԱ����-��ѧԱ�б�
        /// <summary>
        /// ��ȡ��ʦ��ѧ��
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<TeacherStudentVO> GetStudentCourseListByTeacher(IdListBO bo);
        /// <summary>
        /// ��ȡѧԱ��ҵ�б�
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<StudentCourseClassVO> GetListStudentCourseClassByTeacher(StudentCourseClassQueryBO bo);
        #endregion

        #region �
        /// <summary>
        /// ����޸�
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo CreateOrUpdateActivity(ActivityBO modelBo);
        /// <summary>
        /// ȡ���
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        AmountBo EscActivity(string id, string userId);
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActivityAllVO GetOneActivityVO(string id,bool isTeacher=false);
        /// <summary>
        /// ��ȡ�б�
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

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
        /// ��������
        /// </summary>
        string CreateOrUpdateCourse(StudentCourseBO modelBo);
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        StudentCourseOneVO GetOneCourse(string Id);
        StudentCourseOneVO2 GetOneCourse2(string Id);
        /// <summary>
        /// ���ڶ���
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        M_StudentCourse GetOneStudentCourseByIdSi(string Id);
        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <returns></returns>
        List<StudentCourseVO> GetListCourse(StudentCourseListBO bo);
        /// <summary>
        /// �޸�ѧ���γ�
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo UpdateStudentCourse(StudentCourseBO modelBo);
        /// <summary>
        /// �������һ��
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
        /// ���״̬�޸�
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo UpdateLeaveStauts(string id, int status, string userId);
        List<StudentCourseClassVO> GetListStudentCourseClass(StudentCourseClassQueryBO bo);
        AmountBo CreateOrUpdateStudentClass(string studentId, string teachercourseId, string courseId);
        StudentCourseClassVO GetOneStudentCourseClass(string Id);

        /// <summary>
        /// ȡ���γ̺��ȡ�γ�+1
        /// </summary>
        /// <param name="teachercourseId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        List<SqlItem> GetEscTeacherCourse(string teachercourseId, int status);
        /// <summary>
        /// ɾ�������γ�
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteStudentCourse(string Id);
        /// <summary>
        /// �޸�״̬
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        bool UpdateStudentCourseStatus(string id, string status);


        #region �ҵĿγ�-Լ��
        /// <summary>
        /// ������ʦѧУ��ȡ�γ�
        /// </summary>
        /// <param name="teacherId"></param>
        /// <param name="schoolId"></param>
        /// <param name="addDay"></param>
        /// <returns></returns>
        List<TeacherCourseFieldVO> GetTeacherCourseByTeacherSchool(string teacherId, string schoolId, string studentCourseId, int addDay);
        /// <summary>
        /// ��ȡѧУ������ʦ
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<KeyValueBo> GetSchoolByTeacherId(string Id);
        /// <summary>
        /// ��ȡ��ʦ
        /// </summary>
        /// <returns></returns>
        List<TeacherVO> GetTeacherList();
        #endregion

        #region  ��ҵ����
        /// <summary>
        /// ѧ������ʦ���γ�����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        AmountBo SaveStudentEvaluate(string id, string teacherEvaluate, int studentStars, string uerId);


        /// <summary>
        /// ��ʦ��ѧ�����γ�����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        AmountBo SaveTeacherEvaluate(string id, string studentEvaluate, string uerId);
        /// <summary>
        /// ��һ��
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseVideoUrl"></param>
        /// <param name="uerId"></param>
        /// <returns></returns>
        AmountBo SaveCourseVideoUrl(string id, string courseVideoUrl, string uerId);

        AmountBo SaveJobEvaluate(string id, string JobEvaluate, string uerId);
        /// <summary>
        /// ѧ���ύ��ҵ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        AmountBo SaveStudentAddJob(string id, string videoUrl, string uerId);


        /// <summary>
        /// ��ʦ������ҵ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        AmountBo SaveTeacherAddJob(string id, string jobName, string videoId, string uerId, string jobPoints, string trainNum, string trainTime);
        #endregion

        #region ѧ����ȡ�α�
        List<StudentCourseClassVO> GetListStudentCourseClassByStudent(StudentCourseClassQueryBO bo);
        /// <summary>
        /// ΢�Ŵ�ҳ��
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<StudentCourseClassVO> GetListStudentCourseClassByStudentWX(StudentCourseClassQueryBO bo, int type = 1);
        #endregion

        #region �γ�ʣ��
        ClassNumInfoVO GetClassNumInfo(string id);
        #endregion



        #region

        bool UpdateStudentIsPay(string id);
        AmountBo TransferStudentCourse(StudentCour3Request request);


        #endregion
    }
}

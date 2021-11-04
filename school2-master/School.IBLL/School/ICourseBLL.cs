using school.IBLL.Base;
using school.Model;
using school.Model.BO;
using school.Model.DAO;
using school.Model.TO.Request;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace school.IBLL
{
    public interface ICourseBLL : IBaseBLL
    {
        /// <summary>
        /// ��������
        /// </summary>
        AmountBo CreateOrUpdate(CourseBO modelBo);
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        M_Course GetOne(string Id);
        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <returns></returns>
        List<M_Course> GetList(CourseListBO bo);
        AmountBo DeleteCourse(string id, string userId);

        #region ���ض���ʱ��

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo CreateOrUpdateSub(SubscribeAddBO bo);
        AmountBo UpdateOrder(string id);
        M_Order OrderOnlyPay(string id);
        /// <summary>
        /// sqlitem
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<SqlItem> AddSubscribeSqlItem(SubscribeAddBO bo);
        List<string> CreateOrUpdateOrder(List<OrderBO> bo, string UserId, string name, string tel, string kj, int category);
        /// <summary>
        /// ��ȡ�б����ʱ�䳡��
        /// </summary>
        /// <param name="fieldId"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        List<SubVO> GetListSub(string fieldId, int day, int futureDay);
        /// <summary>
        /// ȡ��Ԥ��
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        AmountBo EscSub(SubscribeESC bo, string status);

        AmountBo ApplyEscSub(SubscribeESC bo);
        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        AmountBo ApplyEscSubEsc(SubscribeESC bo);
        /// <summary>
        /// �@ȡ�����A��
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        List<SubVO> GetListSubBySchoolId(string schoolId, int day, int dctype = 0);
        #endregion


        #region ��е����
        #region ��е
        AmountBo CreateOrUpdateEqu(EquBO modelBo);

        EquVO GetEquOne(string id);

        ListEquVO GetEquList(EquListBO bo);
        M_Equ GetEquOneUserAdmin(string id);
        List<M_Equ> GetEquListUserAdmin(string fielId);
        #endregion
        #endregion


        #region �ֵ��ѯ
        AmountBo CreateOrUpdateCategory(CategoryBO modelBo);
        AmountBo DeleteCategory(string id);
        M_Category GetCatergoryOne(string id);
        List<M_Category> GetCatergoryParList();
        List<M_Category> GetCatergoryList(string parentId);
        //List<M_Category> GetCatergoryListByCategory(string category);
        List<CategoryListVO> GetCatergoryListByCategory(string category);
        List<M_Category> GetCatergoryListByCategoryOne(string category);
        CategoryListAllVO GetCatergoryAll(string category);
        #endregion
        AmountBo CreateOrUpdateMenu(string menu, string userId);
        M_Menu GetOneMenu();
        /// <summary>
        /// �γ��б�
        /// </summary>
        /// <returns></returns>
        List<M_Course> GetCourseList(CourseRequest request);
        M_Course GetSurplusClassTimes(StudentCour3Request request);
    }
}

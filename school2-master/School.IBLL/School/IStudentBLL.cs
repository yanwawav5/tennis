using school.IBLL.Base;
using school.Model;
using school.Model.BO;
using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace school.IBLL
{
    public interface IStudentBLL : IBaseBLL
    {
        /// <summary>
        /// 保存或更新
        /// </summary>
        string CreateOrUpdate(StudentBO modelBo);
        AmountBo UpdateStudentSmOpenId(string unionId, string openId);
        M_Student GetOne(string Id,bool show=false);

        M_Student GetOneByWechat(string Id);

        M_Student GetOneBySmOpenId(string smopenId);

        List<M_Student> GetList(PageInfo bo);

        AmountBo CreateTableLog();
        M_Student GetOneByUionId(string UnionId);

        bool UpdateTel(string unionId, string id, string tel, string studentId);
        AmountBo CreateOrUpdateStudent(string id, string fullName, string tel, int types, string userId);
        #region
        /// <summary>
        /// 获取用户场地订购信息
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        List<OrderVO> GetOrderList(IdStatusListBO bo);
        OrderVO GetOrder(string id);

        List<StudentKjVO> GetKjList(IdStatusListBO bo);
        int GetKjCount(string id,string category);

        KeyValueBo StudengKjFirst(string id, string categoryId);

        #endregion
    }
}

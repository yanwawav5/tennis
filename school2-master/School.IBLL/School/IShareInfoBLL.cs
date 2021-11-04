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
    public interface IShareInfoBLL : IBaseBLL
    {
        /// <summary>
        /// ��������
        /// </summary>
        string CreateOrUpdate(ShareInfoBO modelBo);
        string CreateOrUpdate2(ShareInfoBO modelBo);
        M_ShareInfo GetOne(string Id); 
        List<ShareInfoVO> GetList(PageInfo bo);
        /// <summary>
        /// ��վ
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        bool CreateOrUpdateClick(string Id, string uid);
        /// <summary>
        /// ��ȡ�ܵ�������
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int GetNum(string Id);
        /// <summary>
        /// ��ȡѵ������
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        int GetXLNum(string Id,int types);
    }
}

using school.IBLL.Base;
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
    public interface IWTokenBLL : IBaseBLL
    {
        /// <summary>
        /// 保存或更新，资源管理链接
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo CreateOrUpdate(string tokenId);

        /// <summary>
        /// 删除资源管理链接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AmountBo Delete(string id);

        /// <summary>
        /// 查询单条
        /// </summary>
        string Get();

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="modelBo"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<WProductVO> GetList(WProdcutListBO bo);

        SqlItem CreateOrUpdateSjs(W_StudentKj bo);
        #region Js
        AmountBo CreateOrUpdateJs(string ticket);

        string GetTicket();
        #endregion

        AmountBo CreateOrUpdateMS(W_Code bo);
        bool UpdateCode(string phone, string code);
    }
}

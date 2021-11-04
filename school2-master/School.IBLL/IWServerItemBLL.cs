using school.IBLL.Base;
using school.Model;
using school.Model.BO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.IBLL
{
    public interface IWServerItemBLL : IBaseBLL
    {
        /// <summary>
        /// 保存或更新，资源管理链接
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        AmountBo CreateOrUpdate(WServerItemBO modelBo);

        /// <summary>
        /// 删除资源管理链接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AmountBo Delete(string id);

        /// <summary>
        /// 查询单条
        /// </summary>
        WServerItemVO Get(string id);

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="modelBo"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<WServerItemVO> GetList(WIdListBO bo);

        void GetSqlItem(List<WServerItemBO> list, string productId, string serverMainId);

        List<WServerItemVO> GetListNew(WIdListBO bo);
        PriceVO GetEmdPrice(string id,int jisuanfangfa);

        string GetBasePrice(string id,out string name);
    }
}

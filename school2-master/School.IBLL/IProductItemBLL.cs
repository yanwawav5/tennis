using school.IBLL.Base;
using school.Model.BO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.IBLL
{
    public interface IProductItemBLL : IBaseBLL
    {

        /// <summary>
        /// 查询单条
        /// </summary>
        ProductItemVO Get(string id); 
        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="modelBo"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<ProductItemVO> GetList(WIdListBO bo);
    }
}

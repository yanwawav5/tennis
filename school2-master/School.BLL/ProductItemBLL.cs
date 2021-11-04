using school.BLL.Base;
using school.BLL.Factory;
using school.Common.Tools;
using school.IBLL;
using school.IDAL;
using school.Model.BO;
using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.BLL
{
    public class ProductItemBLL : BaseBLL<IProductItemDAL>, IProductItemBLL
    {
        public ProductItemVO Get(string id)
        {
            var modelVo = new ProductItemVO();
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_ProductItem>(p => p.Id == id);
            ObjectHelper.AutoMapping(queryModel, modelVo);
            return modelVo;
        }

        public List<ProductItemVO> GetList(WIdListBO bo)
        {
            int totNum = 0;
            var list = new List<ProductItemVO>();

            var whereQl = BuildExpression<W_ProductItem>(p => p.ProductId ==bo.Id);
            IEnumerable<W_ProductItem> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<W_ProductItem>(whereQl);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;

            list = noumena.OrderByDescending(x => x.CreateTime).ThenByDescending(x => x.Id).Select(
                p => new ProductItemVO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Pic = p.Pic,
                    Price = p.Price,
                    ProductUnit = p.ProductUnit,
                    ProductPriceList = SessionFactory.SessionService.mWProductItemPriceBLL.GetList(new WIdListBO { Id = p.Id, PageIndex = 0, PageSize = 0 })
                }).ToList();
            return list;
        }
    }
}

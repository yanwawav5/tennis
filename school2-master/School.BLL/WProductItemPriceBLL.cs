using school.BLL.Base;
using school.Common.Tools;
using school.IBLL;
using school.IDAL;
using school.Model.BO;
using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace school.BLL
{
    public class WProductItemPriceBLL : BaseBLL<IWProductItemPriceDAL>, IWProductItemPriceBLL
    {
        /// <summary>
        /// 保存或更新，资源管理链接
        /// </summary>
        public string CreateOrUpdate(WLoginKeyBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_LoginKey>(p => p.Id == modelBo.Id);
            ObjectHelper.AutoMapping(modelBo, queryModel);
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateParse.GetDateTime();
                queryModel.CreateUserId = modelBo.UserId;
                amount = mServiceDAL.Create(queryModel);
            }
            else
            {
                queryModel.UpdateTime = DateParse.GetDateTime();
                queryModel.UpdateUserId = modelBo.UserId;
                amount = mServiceDAL.Update(queryModel);
            }
            return queryModel.Id;
        }

        /// <summary>
        /// 删除资源管理链接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AmountBo Delete(string id)
        {
            var amount = 0;

            return base.ReturnBo(amount);
        }

        /// <summary>
        /// 查询单条
        /// </summary>
        public ProductItemPriceVO Get(string id)
        {
            var modelVo = new ProductItemPriceVO();
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_ProductItemPrice>((Expression<Func<W_ProductItemPrice, bool>>)(p => p.Id == id));
            ObjectHelper.AutoMapping(queryModel, modelVo);
            return modelVo;
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="modelBo"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<ProductItemPriceVO> GetList(WIdListBO bo)
        {
            int totNum = 0;
            var list = new List<ProductItemPriceVO>();
            var whereQl = BuildExpression<W_ProductItemPrice>(p => p.ProductItemId == bo.Id);
            IEnumerable<W_ProductItemPrice> noumena = null;
            noumena = mServiceDAL.LoadEntities<W_ProductItemPrice>(whereQl);
            totNum = noumena.Count();
            if (!noumena.Any()) return list;

            list = noumena.OrderByDescending(x => x.Num).ThenByDescending(x => x.CreateTime).Select(
                p => new ProductItemPriceVO
                {
                    Id = p.Id,
                    Num = p.Num,
                    ProductItemId = p.ProductItemId,
                    ProductUnit = p.ProductUnit,
                    TopPrice = p.TopPrice,
                    Type = p.Type,
                    Price = p.Price
                }).ToList();
            return list;
        }
        public WPriceVO GetPrice(MathBO bo)
        {
            var list = base.LoadEntities<W_ProductItemPrice>(p => p.ProductItemId == bo.ProductItemId).OrderBy(p => p.Num).ToList();
            if (list.Count == 0) return new WPriceVO { TotPrice = 0 };
            var type = list[0].Type;
            if (type == 1)
            {
                if (bo.MathNum >= list.Count)
                {
                    return new WPriceVO { TotPrice = bo.MathNum * list[2].Price * bo.ServerNum, Price = (list[2].Price).ToString() + list[2].ProductUnit };
                } 
                return new WPriceVO { TotPrice = bo.MathNum * list[bo.MathNum - 1].Price * bo.ServerNum, Price = (list[bo.MathNum - 1].Price).ToString() + list[bo.MathNum - 1].ProductUnit };
            }
            else if (type == 2)
            {
                var pri = base.LoadFirstOrDefault<W_ProductItemPrice>(p => p.ProductItemId == bo.ProductItemId && p.Num == bo.TypeInfo);
                return new WPriceVO { TotPrice = pri.Price * bo.ServerNum * bo.MathNum, Price = (pri.Price).ToString() + pri.ProductUnit };
            }
            
               
            return new WPriceVO { TotPrice = 0 };
        }
        public WPriceVO GetEndPrice(MathBO bo)
        {
            var list = base.LoadEntities<W_ProductItemPrice>(p => p.ProductItemId == bo.ProductItemId).OrderBy(p => p.Num).ToList();
            if (list.Count == 0) return new WPriceVO { TotPrice = 0 };
            var type = list[0].Type;
            if (type == 1)
            {
                if (bo.MathNum >= list.Count)
                {
                    return new WPriceVO { TotPrice = bo.MathNum * list[2].Price, Price = list[2].Price.ToString() + list[2].ProductUnit };
                }

                return new WPriceVO { TotPrice = bo.MathNum * list[bo.MathNum - 1].Price, Price = list[bo.MathNum - 1].Price.ToString() + list[bo.MathNum - 1].ProductUnit };
            }
            else if (type == 2)
            {
                var pri = base.LoadFirstOrDefault<W_ProductItemPrice>(p => p.ProductItemId == bo.ProductItemId && p.Num == bo.TypeInfo);
                return new WPriceVO { TotPrice = pri.Price * bo.MathNum, Price = pri.Price.ToString() + pri.ProductUnit };
            }
            return new WPriceVO { TotPrice = 0 };
        }
    }
}

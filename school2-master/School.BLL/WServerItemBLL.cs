using school.BLL.Base;
using school.IBLL;
using school.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using school.Model;
using school.Model.BO;
using school.Model.DAO;
using school.Common.Tools;
using school.Model.VO;
using school.Common;
using school.Model.Enum;
using school.BLL.Factory;

namespace school.BLL
{
    public class WServerItemBLL : BaseBLL<IWServerItemDAL>, IWServerItemBLL
    {
        public AmountBo CreateOrUpdate(WServerItemBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerItem>(p => p.Id == modelBo.Id);
            ObjectHelper.AutoMapping(modelBo, queryModel);
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateTime.Now;
                queryModel.CreateUserId = modelBo.UserId;
                queryModel.ServerMainId = modelBo.ServerMainId;
                amount = mServiceDAL.Create(queryModel);
            }
            else
            {
                queryModel.UpdateTime = DateTime.Now;
                queryModel.UpdateUserId = modelBo.UserId;
                amount = mServiceDAL.Update(queryModel);
            }
            return base.ReturnBo(amount);
        }

        public AmountBo Delete(string id)
        {
            throw new NotImplementedException();
        }

        public WServerItemVO Get(string id)
        {
            var modelVo = new WServerItemVO();
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerItem>(p => p.Id == id);
            ObjectHelper.AutoMapping(queryModel, modelVo);
            return modelVo;
        }

        public void GetSqlItem(List<WServerItemBO> list, string productId, string serverMainId)
        {
            var bo = new WServerItemBO();
            foreach (var item in list)
            {
                item.ServerMainId = serverMainId;

                CreateOrUpdate(item);
            }
        }
        public List<WServerItemVO> GetList(WIdListBO bo)
        {
            int totNum = 0;
            var list = new List<WServerItemVO>();

            var whereQl = BuildExpression<W_ServerItem>(p => p.ProductId == p.ProductId);
            IEnumerable<W_ServerItem> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<W_ServerItem>(whereQl);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime, true, x => new { x.ProductItemId });
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;

            list = noumena.OrderByDescending(x => x.Sort).ThenByDescending(x => x.CreateTime).Select(
                p => new WServerItemVO
                {
                    Id = p.Id,
                    Num = p.Num,
                    Areas = p.Areas,
                    ProductItemModel = p.W_ProductItem
                }).ToList();
            return list;

        }


        public List<WServerItemVO> GetListNew(WIdListBO bo)
        {
            int totNum = 0;
            int psize = 0;
            var list = new List<WServerItemVO>();

            //var whereQl = BuildExpression<W_ServerItem>(p => p.ProductItemId == bo.Id);
            var whereQl = BuildExpression<W_ServerItem>(p => p.ServerMainId == bo.Id);
            IEnumerable<W_ServerItem> noumena = null;
            //if (bo.PageSize == 0)
            //{
            //    noumena = mServiceDAL.LoadEntities<W_ServerItem>(whereQl);
            //    totNum = noumena.Count();
            //}
            //else
            {
                if (bo.PageSize == 0)
                {
                    psize = 100000;
                }
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, psize,
                    out totNum, whereQl, false, p => p.CreateTime, true, x => new { x.W_ProductItem });
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;

            list = noumena.OrderByDescending(x => x.Sort).ThenByDescending(x => x.CreateTime).Select(
                p => new WServerItemVO
                {
                    Id = p.Id,
                    Num = p.Num,
                    Areas = p.Areas,
                    ProductItemModel = p.W_ProductItem
                }).ToList();
            return list;

        }
        public string GetBasePrice(string id, out string name)
        {
            var vo = new PriceVO();
            var model = base.LoadFirstOrDefault<W_ServerMain>(p => p.Id == id);
            var sertime = Convert.ToDateTime(model.BeginTime);
            var dtnow = DateTime.Now;
            TimeSpan ts = dtnow - sertime;
            var vomodel = new W_ProductItem();
            decimal price = 0;
            var payList = base.LoadEntities<W_ServerItem>(p => p.ServerMainId == id);
            var lis = base.LoadEntities<W_ProductItemPrice>(p => p.ProductItemId == payList[0].ProductItemId).OrderByDescending(p => p.Num);
            if (ts.Hours == 0)
            {
                var lvo2 = lis.Where<W_ProductItemPrice>(p => p.Num >= ts.Hours).OrderBy(p => p.Num).FirstOrDefault();
                price += lvo2.Price * ts.Hours;
                name = "≥" + lvo2.Num + "小时";
                return lvo2.Price + lvo2.ProductUnit;
            }
            else
            {
                var lvo = lis.Where<W_ProductItemPrice>(p => p.Num <= ts.Hours).OrderByDescending(p => p.Num).FirstOrDefault();
                price += lvo.Price * ts.Hours;
                name = "≥" + lvo.Num + "小时";
                return lvo.Price + lvo.ProductUnit;
            }
        }

        public PriceVO GetEmdPrice(string id, int jisuanfangfa)
        {
            var vo = new PriceVO();
            var model = base.LoadFirstOrDefault<W_ServerMain>(p => p.Id == id);
            var sertime = Convert.ToDateTime(model.BeginTime);
            var dtnow = DateTime.Now;
            TimeSpan ts = dtnow - sertime;
            var vomodel = new W_ProductItem();
            decimal price = 0;
            var payList = base.LoadEntities<W_ServerItem>(p => p.ServerMainId == id);

            foreach (var item in payList)
            {

                var vomodels = base.LoadFirstOrDefault<W_ProductItemPrice>(p => p.ProductItemId == item.ProductItemId && p.Num == jisuanfangfa);
                if (vomodels.Type == 2)
                {
                    if (vomodels.ProductUnit == "元/h")
                    {
                        price += vomodels.Price * ts.Hours + vomodels.Price * ts.Minutes / 60;
                    }
                    else
                    {
                        if (item.Num > 0)
                        {
                            price += vomodels.Price * item.Num;
                        }
                    }

                }
                else if (vomodels.Type == 1)
                {
                    var lis = base.LoadEntities<W_ProductItemPrice>(p => p.ProductItemId == item.ProductItemId).OrderByDescending(p => p.Num);

                    if (ts.Hours == 0)
                    {
                        var lvo2 = lis.Where<W_ProductItemPrice>(p => p.Num >= ts.Hours).OrderBy(p => p.Num).FirstOrDefault();
                        price += lvo2.Price * ts.Hours;
                    }
                    else
                    {
                        var lvo = lis.Where<W_ProductItemPrice>(p => p.Num <= ts.Hours).OrderByDescending(p => p.Num).FirstOrDefault();
                        price += lvo.Price * ts.Hours;
                    }
                    var hvo = lis.Where<W_ProductItemPrice>(p => p.Num > ts.Hours).OrderBy(p => p.Num).FirstOrDefault();
                    if (hvo != null)
                    {
                        var lvo3 = lis.Where<W_ProductItemPrice>(p => p.Num >= ts.Hours).OrderBy(p => p.Num).FirstOrDefault();
                        if (hvo.Price > 0)
                            price += hvo.Price * ts.Minutes / 60;
                        if (price >= lvo3.TopPrice && hvo.TopPrice > 0)
                            price = lvo3.TopPrice;
                    }
                    else
                    {
                        var hvo2 = lis.Where<W_ProductItemPrice>(p => p.Num <= ts.Hours).OrderByDescending(p => p.Num).FirstOrDefault();
                        price += hvo2.Price * ts.Minutes / 60;
                    }
                }
            }

            if (model.ProductId == "2")
            {
                if (model.ServerNum <= 1)
                    model.ServerNum = 1;
                price = price * model.ServerNum;
            }
            vo.Price = System.Decimal.Round(price, 2);
            vo.BeginTime = model.BeginTime;
            return vo;
        }


        WPriceVO GetPrice(MathBO bo)
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

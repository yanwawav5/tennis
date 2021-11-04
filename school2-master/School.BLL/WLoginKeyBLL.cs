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
    public class WLoginKeyBLL : BaseBLL<IWLoginKeyDAL>, IWLoginKeyBLL
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
        public WLoginKeyVO Get(string id)
        {
            var modelVo = new WLoginKeyVO();
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_LoginKey>(p => p.Id == id);
            ObjectHelper.AutoMapping(queryModel, modelVo); 
            return modelVo;
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="modelBo"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<WLoginKeyVO> GetList(WLoginKeyBO bo)
        {
            return null;
            //int totNum = 0;
            //var list = new List<WProductVO>();
            //var whereQl = BuildExpression<W_Product>(p => p.Sort==bo.Sort);
            //IEnumerable<W_Product> noumena = null; 
            //if (bo.PageSize == 0)
            //{
            //    noumena = mServiceDAL.LoadEntities<W_Product>(whereQl);
            //    totNum = noumena.Count();
            //}
            //else
            //{
            //    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
            //        out totNum, whereQl, false, p => p.CreateTime);
            //}
            //bo.TotNum = totNum;
            //if (!noumena.Any()) return list;

            //list = noumena.OrderByDescending(x => x.Sort).ThenByDescending(x => x.CreateTime).Select(
            //    p => new WProductVO
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Sort = p.Sort,
            //        IsTop=p.IsTop, Main=p.Main, Pic=p.Pic, Praise=p.Praise , Price= p.Price
            //    }).ToList();
            //return list;
        }
 
    }
}

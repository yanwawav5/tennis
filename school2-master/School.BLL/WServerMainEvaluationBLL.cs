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
    public class WServerMainEvaluationBLL : BaseBLL<IWServerMainEvaluationDAL>, IWServerMainEvaluationBLL
    {
        /// <summary>
        /// 保存或更新，资源管理链接
        /// </summary>


        /// <summary>
        /// 删除资源管理链接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AmountBo Delete(string id)
        {
            var amount = 0;
            var model = base.LoadFirstOrDefault<W_ServerMainEvaluation>(p => p.Id == id);
            base.Delete(model);
            return base.ReturnBo(amount);
        }

        /// <summary>
        /// 查询单条
        /// </summary>
        public WServerMainEvaluationVO Get(string id)
        {
            var modelVo = new WServerMainEvaluationVO();
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerMainEvaluation>(p => p.Id == id);
            ObjectHelper.AutoMapping(queryModel, modelVo);
            return modelVo;
        }

        /// <summary>
        /// 查询多条
        /// </summary>
        /// <param name="modelBo"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<WServerMainEvaluationVO> GetList(string id)
        {
            var list = new List<WServerMainEvaluationVO>();
            var whereQl = BuildExpression<W_ServerMainEvaluation>(p => p.ServerMainId == id);
            IEnumerable<W_ServerMainEvaluation> noumena = null;
            noumena = mServiceDAL.LoadEntities<W_ServerMainEvaluation>(whereQl);
            if (!noumena.Any()) return list;
            list = noumena.OrderByDescending(x => x.CreateTime).Select(
                p => new WServerMainEvaluationVO
                {
                    Pic = p.Pic,
                    Id = p.Id
                }).ToList();
            return list;
        }

    }
}

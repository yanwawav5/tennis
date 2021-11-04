using school.BLL.Base;
using school.IBLL;
using school.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using school.Model.BO;
using school.Model.VO;
using school.Model.DAO;
using school.Common.Tools;

namespace school.BLL
{
    public class TopPicBLL : BaseBLL<ITopPicDAL>, ITopPicBLL
    {
        public TopPicVO Get(string id)
        {
            var modelVo = new TopPicVO();
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_TopPic>(p => p.Id == id);
            ObjectHelper.AutoMapping(queryModel, modelVo);
            return modelVo;
        }

        public List<TopPicVO> GetList(WIdListBO bo)
        {
            int totNum = 0;
            var list = new List<TopPicVO>();

            var whereQl = BuildExpression<W_TopPic>(p => p.Id != null);
            IEnumerable<W_TopPic> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<W_TopPic>(whereQl);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.IsTop);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;

            list = noumena.OrderByDescending(x => x.IsTop).ThenByDescending(x => x.CreateTime).Select(
                p => new TopPicVO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Pic = p.Pic,
                    Url = p.Url
                }).ToList();
            return list;
        }
    }
}

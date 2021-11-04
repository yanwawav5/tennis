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
using school.BLL.Factory;

namespace school.BLL
{
    public class WUserInfoBLL : BaseBLL<IWUserInfoDAL>, IWUserInfoBLL
    {
        public string CreateOrUpdate(WUserInfoBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_UserInfo>(p => p.Id == modelBo.Id);
            ObjectHelper.AutoMapping(modelBo, queryModel);
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateParse.GetDateTime();
                queryModel.CreateUserId = queryModel.Id;
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

        public WUserInfoVO GetOne(string Id)
        {
            var modelVo = new WUserInfoVO();
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_UserInfo>(p => p.Id == Id);
            ObjectHelper.AutoMapping(queryModel, modelVo);
            return modelVo;
        }

        public W_UserInfo GetOnes(string Id)
        {
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_UserInfo>(p => p.Id == Id);
            return queryModel;

        }
        /// <summary>
        /// 获取用户信息如果新添加只有Id
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public WUserInfoVO GetOneByOpenId(string openid)
        {
            var modelVo = new WUserInfoVO();
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_UserInfo>(p => p.OpenId == openid);
            if (queryModel.FtimeStemp == null)
            {
                modelVo.Id = CreateOrUpdate(new WUserInfoBO { OpenId = openid });
            }
            else
            {
                ObjectHelper.AutoMapping(queryModel, modelVo);
            }
            return modelVo;
        }


        public List<WUserInfoVO> GetEmployeeList(PageInfo bo)
        {
            int totNum = 0;
            var list = new List<WUserInfoVO>();

            var whereQl = BuildExpression<W_UserInfo>(p => p.UserType == 1);
            IEnumerable<W_UserInfo> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<W_UserInfo>(whereQl);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;

            list = noumena.OrderByDescending(x => x.CreateTime).ThenByDescending(x => x.CreateTime).Select(
                p => new WUserInfoVO
                {
                    Id = p.Id,
                    AvatarUrl = p.AvatarUrl,
                    NickName = p.NickName,
                    UserType=p.UserType
                }).ToList();
            return list;
        }
    }
}

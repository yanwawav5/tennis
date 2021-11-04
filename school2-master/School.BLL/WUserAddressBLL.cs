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
using school.Common;
using school.Model.Enum;
using System.Data.SqlClient;

namespace school.BLL
{
    public class WUserAddressBLL : BaseBLL<IWUserAddressDAL>, IWUserAddressBLL
    {
        public AmountBo CreateOrUpdate(WUserAddressBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_UserAddress>(p => p.Id == modelBo.Id);
            var userInfoId = queryModel?.UserInfoId;
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
                queryModel.UserInfoId = userInfoId;
                amount = mServiceDAL.Update(queryModel);
            }
            return base.ReturnBo(amount);
        }
        public AmountBo UpdateDefault(WxDefaultAddressBO modelBo)
        {
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_UserAddress>(p => p.Id == modelBo.Id);
            queryModel.IsDefault = modelBo.IsDefault;
            queryModel.UpdateTime = DateParse.GetDateTime();
            queryModel.UpdateUserId = modelBo.UserId;

            if (modelBo.IsDefault == 1)
            {
                SqlItem sqlItems = new SqlItem()
                {
                    SqlValue = "update W_UserAddress set IsDefault=0 where userInfoId=@userInfoId",
                    Params = new[] { new SqlParameter("@userInfoId", modelBo.UserInfoId) }
                };
                base.ExecuteSqlTran(sqlItems);
            }
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        public AmountBo Delete(string id, string userId)
        {
            var count = base.Count<W_UserAddress>(p => p.UserInfoId == userId);
            if (count <= 1)
                throw new schoolException(SubCode.Failure.GetHashCode(), "删除失败,不能删除最后一条数据");
            var amount = 0;
            var esse = mServiceDAL.LoadFirstOrDefault<W_UserAddress>(p => p.Id == id);
            if (esse != null)
            {
                amount = mServiceDAL.Delete(esse);
            }
            return base.ReturnBo(amount);
        }

        public List<WUserAddressVO> GetList(string id)
        {
            List<WUserAddressVO> list = new List<WUserAddressVO>();
            var noumena = mServiceDAL.LoadEntities<W_UserAddress>(p => p.UserInfoId == id).OrderBy(p => p.CreateTime);
            if (noumena.Any())
            {
                list = noumena.Select(p => new WUserAddressVO
                {
                    Id = p.Id,
                    Address = p.Address,
                    Tel = p.Tel,
                    UserName = p.UserName,
                    IsDefault = p.IsDefault,
                    Latitude = p.Latitude,
                    Logitude = p.Logitude,
                    Name = p.Name,
                    UserAddress = p.UserAddress
                }).ToList();
            }
            return list;
        }

        public WUserAddressVO GetOne(string Id, string userId)
        {
            var modelVo = new WUserAddressVO();
            var queryModel = new W_UserAddress();
            if (!string.IsNullOrEmpty(Id))
                queryModel = mServiceDAL.LoadFirstOrDefault<W_UserAddress>(p => p.Id == Id && p.UserInfoId == userId);
            else
                queryModel = mServiceDAL.LoadFirstOrDefault<W_UserAddress>(p => p.UserInfoId == userId && p.IsDefault == 1);
            if (queryModel.FtimeStemp == null)
                queryModel = mServiceDAL.LoadFirstOrDefault<W_UserAddress>(p => p.UserInfoId == userId);
            ObjectHelper.AutoMapping(queryModel, modelVo);
            return modelVo;
        }

        public W_UserAddress GetOneById(string Id)
        { 
            var queryModel = new W_UserAddress();
            if (!string.IsNullOrEmpty(Id))
                queryModel = mServiceDAL.LoadFirstOrDefault<W_UserAddress>(p => p.Id == Id); 
            return queryModel;
        }
    }
}

using school.IBLL.Base;
using school.Model.BO;
using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.IBLL
{
    public interface IWUserAddressBLL : IBaseBLL
    {
        AmountBo CreateOrUpdate(WUserAddressBO modelBo);
        WUserAddressVO GetOne(string Id,string userId);
        List<WUserAddressVO> GetList(string openId);

        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AmountBo Delete(string id,string userId);

        AmountBo UpdateDefault(WxDefaultAddressBO modelBo);
        W_UserAddress GetOneById(string Id);
    }
}

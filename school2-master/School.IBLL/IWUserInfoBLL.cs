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
    public interface IWUserInfoBLL : IBaseBLL
    {
        /// <summary>
        /// 保存或更新，资源管理链接
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        string CreateOrUpdate(WUserInfoBO modelBo);
        WUserInfoVO GetOne(string Id);
        WUserInfoVO GetOneByOpenId(string Id);
       //WUserInfoVO GetEmployeeOne(string Id);
        List<WUserInfoVO> GetEmployeeList(PageInfo p);


        W_UserInfo GetOnes(string Id);
    }
}

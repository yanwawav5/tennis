using school.Model;
using school.IBLL;
using school.BLL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using school.IDAL;
using System.Threading.Tasks;
using school.Model.BO;
using school.Model.BO.User;
using school.Model.DAO;
using school.Common.Tools;
using school.Model.VO;
using System.Linq.Expressions;
using school.BLL.Factory;

namespace school.BLL
{
    public class UserBLL : BaseBLL<IUserDAL>, IUserBLL
    {
    }
}

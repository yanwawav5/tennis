using school.IBLL.Base;
using school.Model;
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
    public interface IStudentPrizeBLL : IBaseBLL
    {
        /// <summary>
        /// 保存或更新
        /// </summary>
        PrizeVO CreateOrUpdate(M_StudentPrize modelBo, int type = 1,int status=0);
        
    }
}

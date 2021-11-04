using school.IBLL.Base;
using school.Model;
using school.Model.BO;
using school.Model.DAO;
using school.Model.DAO.Base;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.IBLL
{
    public interface IWServerMainBLL : IBaseBLL
    {
        AmountBo CreateOrUpdate(WServerMainAddBO modelBo);

        AmountBo SaveEmp(WServerEmpBO modelBo);
        VauleVO GetCode(string Id);

        WServerMainVO GetOne(string Id, string userId);
        List<WServerMainVO> GetList(WServerMainListBO bo);
        List<WServerMainVO> GetEmployeeList(WServerMainListBO bo);
        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AmountBo Delete(string id, string userId);


        AmountBo UpdateCode(WServerCodeBo modelBo);

        AmountBo UpdateSecCode(WServerCodeBo modelBo);
        int UpdateStatus(string id);
        AmountBo SaveEva(WServerMainEvaluationBO modelBo);
        int SaveKey(string Id,int category);
        string GetKey(string Id);

        W_PayKey GetKeyByServerId(string serverId);
        ServerEvalVO GetEval(string id);


        AmountBo EscServer(WChangeStatusBO modelBo);
    }
}

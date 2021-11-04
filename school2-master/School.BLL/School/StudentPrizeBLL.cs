using school.IBLL;
using school.BLL.Base;
using System;
using System.Collections.Generic;
using school.IDAL;
using school.Model.BO;
using school.Model.DAO;
using school.Common.Tools;
using school.Model.Enum;
using school.Common;
using school.Model.VO;

namespace school.BLL
{
    public class StudentPrizeBLL : BaseBLL<IStudentPrizeDAL>, IStudentPrizeBLL
    {

        public PrizeVO CreateOrUpdate(M_StudentPrize modelBo, int type = 1, int status = 0)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_ShareInfo>(p => p.Id == modelBo.ShareInfoId);//查询分享的数据
            if (queryModel.FtimeStemp == null && queryModel.UnionId != modelBo.UnionId)
            {
                Log.Error(new String[] { "抽奖Id", modelBo.ShareInfoId });
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            }
            if (status == 1) //抽奖判断
            {
                var pmode = base.LoadFirstOrDefault<M_StudentPrize>(p => p.ShareInfoId == modelBo.ShareInfoId);
                var countclick = base.Count<M_ShareInfoClick>(p => p.ShareInfoId == modelBo.ShareInfoId && p.UnionId != modelBo.UnionId);
                if (countclick == 0)
                    throw new schoolException(SubCode.Failure.GetHashCode(), Message.Instance.R("v_CJ1"));
                var count = base.Count<M_StudentPrize>(p => p.ShareInfoId == modelBo.ShareInfoId&&p.Status==1);
                if (count > 0)
                    throw new schoolException(SubCode.Failure.GetHashCode(), "不能重复抽奖！");
                M_StudentPrize mode = new M_StudentPrize();
                ObjectHelper.AutoMapping(pmode, mode); 
                mode.Id = pmode.Id;
                mode.UpdateTime = DateParse.GetDateTime();
                mode.UpdateUserId = modelBo.UnionId;
                mode.Status = 1;
                amount = mServiceDAL.Update<M_StudentPrize>(mode);
            }
            else
            {
                var pmode = base.LoadFirstOrDefault<M_StudentPrize>(p => p.ShareInfoId == modelBo.ShareInfoId);
                if(pmode.FtimeStemp!=null)
                    return new PrizeVO { Prize = pmode.Prize, PrizeNum = pmode.PrizeNum };
                var prizeList = mServiceDAL.LoadFirstOrDefault<M_PrizeType>(p => p.PrizeType == type);//查询中奖的数据
                Random rnd = new Random();
                int rndNum = rnd.Next(1000);
                var ran = prizeList.PrizeInfo.Split(',');
                int num = 0;
                modelBo.Prize = 0;
                modelBo.PrizeNum = 1;
                int tot = 0;
                foreach (var item in ran)
                {
                    tot += Int32.Parse(item);
                    if (tot >= rndNum)
                    {
                        modelBo.Prize = 1;
                        modelBo.PrizeNum = num;
                        break;
                    }
                    num++;
                }
                M_StudentPrize mode = new M_StudentPrize()
                {
                    Id = Utils.GetGuid(),
                    CreateTime = DateParse.GetDateTime(),
                    CreateUserId = modelBo.UnionId,
                    ShareInfoId = modelBo.ShareInfoId,
                    UnionId = modelBo.UnionId,
                    Prize = modelBo.Prize,
                    PrizeNum = modelBo.PrizeNum,
                    Status = 0
                };
                amount = mServiceDAL.Create<M_StudentPrize>(mode);
            }

            return new PrizeVO { Prize = modelBo.Prize, PrizeNum = modelBo.PrizeNum };
        }

        public AmountBo CreateTableLog()
        {

            return base.ReturnBo(1);
        }
        SqlItem CreateSqlIte()
        {
            string endName = DateTime.Now.ToString("yyMMddHH");
            string tbname = "M_Log" + endName;
            string sql = "select * into " + tbname + " from M_Log";
            var sqlItem = new SqlItem()
            {
                SqlValue = sql,
                Params = null
            };
            return sqlItem;
        }
        SqlItem DeleteSqlItem()
        {
            string sql = "TRUNCATE TABLE  [dbo].[M_Log] ";//删除  
            var sqlItem = new SqlItem()
            {
                SqlValue = sql,
                Params = null
            };
            return sqlItem;
        }
    }
}

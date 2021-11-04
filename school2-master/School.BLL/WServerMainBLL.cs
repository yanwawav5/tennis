using school.BLL.Base;
using school.IBLL;
using school.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using school.Model.BO;
using school.Model.DAO;
using school.Common.Tools;
using school.Model;
using school.Common;
using school.Model.Enum;
using school.Model.VO;
using school.BLL.Factory;
using school.Model.DAO.Base;
using System.Data.SqlClient;

namespace school.BLL
{
    public class WServerMainBLL : BaseBLL<IWServerMainDAL>, IWServerMainBLL
    {
        public AmountBo CreateOrUpdate(WServerMainAddBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerMain>(p => p.Id == modelBo.Id);
            var userInfoId = queryModel?.UserInfoId;
            ObjectHelper.AutoMapping(modelBo, queryModel);
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.ServerCode = ranStr(DateTime.Now.Millisecond);
                queryModel.CreateTime = DateTime.Now;
                queryModel.CreateUserId = modelBo.UserId;
                queryModel.Status = 1;
                queryModel.ApplyTime = modelBo.ApplyTime;
                queryModel.ServerSecCode = ranStr2(DateTime.Now.Millisecond);
                amount = mServiceDAL.Create(queryModel);
            }
            else
            {
                queryModel.UpdateTime = DateTime.Now;
                queryModel.UpdateUserId = modelBo.UserId;
                queryModel.UserInfoId = userInfoId;
                amount = mServiceDAL.Update(queryModel);
            }
            SessionFactory.SessionService.mWserverItemBLL.GetSqlItem(modelBo.List, modelBo.ProductId, queryModel.Id);
            return base.ReturnBo(amount);
        }
        public AmountBo SaveEmp(WServerEmpBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerMain>(p => p.Id == modelBo.Id);
            if (queryModel.FtimeStemp == null) throw new schoolException(SubCode.Failure.GetHashCode(), "参数错误,未查询到您要操作的数据");
            queryModel.ServerUserId = modelBo.ServerUserId;
            amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);

        }

        public int SaveKey(string Id, int category)
        {
            W_PayKey vo = new W_PayKey()
            {
                ServerId = Id,
                CreateTime = DateTime.Now
            };
            //int amount= base.Create<W_PayKey>(vo);

            SqlParameter[] par =
             {
                new SqlParameter("@ServerId",Id),
                new SqlParameter("@Category",category),
                new SqlParameter("@CreateTime",DateTime.Now)
            };
            SqlItem sql2 = new SqlItem()
            {
                SqlValue = "insert into w_paykey (ServerId,category,CreateTime) values (@ServerId,@category,@CreateTime)",
                Params = par
            };
            base.ExecuteSqlTran(sql2);
            SqlItem sql = new SqlItem()
            {
                SqlValue = "select top 1 id from w_paykey where ServerId=@ServerId order by createtime desc",
                Params = par
            };
            var dt = base.ExecuteDataTable(sql);
            var mo = base.DataTableToEntities<W_PayKey>(dt).FirstOrDefault();
            return mo.Id;
        }

        public string GetKey(string Id)
        {
            SqlParameter[] par =
               {
                new SqlParameter("@id",Id),
            };
            SqlItem sql = new SqlItem()
            {
                SqlValue = "select top 1 ServerId from w_paykey where id=@id ",
                Params = par
            };
            var dt = base.ExecuteDataTable(sql);
            var mo = base.DataTableToEntities<W_PayKey>(dt).FirstOrDefault();
            return mo.ServerId;
        }

        public W_PayKey GetKeyByServerId(string serverId)
        {
            var model = base.LoadFirstOrDefault<W_PayKey>(p => p.Id ==Int32.Parse(serverId));
            return model;
        }

        string ranStr2(int tick)
        {
            string str = string.Empty;
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                str += r.Next(1, 9).ToString();
            }
            return str;
        }


        string ranStr(int tick)
        {
            string str = string.Empty;
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                str += r.Next(0, 9).ToString();
            }
            return str;
        }

        public AmountBo Delete(string id, string userId)
        {
            var amount = 0;
            var esse = mServiceDAL.LoadFirstOrDefault<W_ServerMain>(p => p.Id == id);
            if (esse != null)
            {
                amount = mServiceDAL.Delete(esse);
            }
            return base.ReturnBo(amount);
        }

        public VauleVO GetCode(string Id)
        {
            var modelVo = new WServerMainVO();
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerMain>(p => p.Id == Id);
            if (queryModel.FtimeStemp == null) throw new schoolException(SubCode.Failure.GetHashCode(), "参数错误,未查询到您要操作的数据");
            if (string.IsNullOrEmpty(queryModel.BeginTime.ToString()))
                return new VauleVO { Str = queryModel.ServerCode };
            return new VauleVO { Str = queryModel.ServerSecCode };
        }

        public List<WServerMainVO> GetList(WServerMainListBO bo)
        {
            int totNum = 0;
            var list = new List<WServerMainVO>();

            var whereQl = BuildExpression<W_ServerMain>(
                 BuildLambda<W_ServerMain>(bo.UserInfoId, p => p.UserInfoId == bo.UserInfoId),
                 BuildLambda<W_ServerMain>(bo.Status != 0, p => p.Status == bo.Status));
            IEnumerable<W_ServerMain> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<W_ServerMain>(whereQl);
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
                p => new WServerMainVO
                {
                    Id = p.Id,
                    Price = p.Price,
                    ApplyTime = p.ApplyTime,
                    Status = p.Status,
                    BeginTime = p.BeginTime,
                    EndTime = p.EndTime,
                    OtherAddress = p.OtherAddress,
                    OtherTel = p.OtherTel,
                    UserAddressId = p.UserAddressId,
                    ProductId = p.ProductId,
                    ServerUserId = p.ServerUserId,
                    ServerCode = p.ServerCode,
                    ServerSecCode = p.ServerSecCode,
                    ServerType = p.ServerType,
                    Pic = SessionFactory.SessionService.mWproductBLL.Get(p.ProductId)?.Pic,
                    Name = SessionFactory.SessionService.mWproductBLL.Get(p.ProductId)?.Name,
                    UserModel = SessionFactory.SessionService.mWuserInfoBLL.GetOne(p.ServerUserId),
                    AddressModel = SessionFactory.SessionService.mWuserAddressBLL.GetOneById(p.UserAddressId),
                    ServerNum = p.ServerNum,
                    List = SessionFactory.SessionService.mWserverItemBLL.GetListNew(new WIdListBO { Id = p.Id, PageIndex = 0, PageSize = 0 })
                }).ToList();
            return list;
        }



        public List<WServerMainVO> GetEmployeeList(WServerMainListBO bo)
        {
            int totNum = 0;
            var list = new List<WServerMainVO>();

            var whereQl = BuildExpression<W_ServerMain>(
                 BuildLambda<W_ServerMain>(bo.UserInfoId, p => p.ServerUserId == bo.UserInfoId),
                 BuildLambda<W_ServerMain>(bo.Status != 0, p => p.Status == bo.Status));
            IEnumerable<W_ServerMain> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<W_ServerMain>(whereQl);
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
                p => new WServerMainVO
                {
                    Id = p.Id,
                    Price = p.Price,
                    ApplyTime = p.ApplyTime,
                    Status = p.Status,
                    BeginTime = p.BeginTime,
                    EndTime = p.EndTime,
                    OtherAddress = p.OtherAddress,
                    OtherTel = p.OtherTel,
                    UserAddressId = p.UserAddressId,
                    ProductId = p.ProductId,
                    ServerUserId = p.ServerUserId,
                    ServerCode = p.ServerCode,
                    ServerSecCode = p.ServerSecCode,
                    ServerType = p.ServerType,
                    ServerNum = p.ServerNum,
                    Pic = SessionFactory.SessionService.mWproductBLL.Get(p.ProductId)?.Pic,
                    Name = SessionFactory.SessionService.mWproductBLL.Get(p.ProductId)?.Name,
                    UserModel = SessionFactory.SessionService.mWuserInfoBLL.GetOne(p.ServerUserId),
                    AddressModel = SessionFactory.SessionService.mWuserAddressBLL.GetOneById(p.UserAddressId),
                    List = SessionFactory.SessionService.mWserverItemBLL.GetListNew(new WIdListBO { Id = p.Id, PageIndex = 0, PageSize = 0 })
                }).ToList();
            return list;
        }


        public WServerMainVO GetOne(string Id, string userId)
        {
            var modelVo = new WServerMainVO();
            var queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerMain>(p => p.Id == Id);
            ObjectHelper.AutoMapping(queryModel, modelVo);
            return modelVo;
        }

        public AmountBo UpdateCode(WServerCodeBo modelBo)
        {
            var queryModel = new W_ServerMain();
            queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerMain>(p => p.Id == modelBo.Id);
            if (queryModel.FtimeStemp == null) throw new schoolException(SubCode.Failure.GetHashCode(), "参数错误,未查询到您要操作的数据");
            if (string.IsNullOrEmpty(queryModel.BeginTime.ToString()))
            {
                //  if (queryModel.ServerCode != modelBo.Code) throw new schoolException("验证码错误");
                // else
                {
                    queryModel.BeginTime = DateTime.Now;
                    queryModel.Status = 2;
                }
            }
            else
            {
                throw new schoolException("操作失败验证码错误");
                //if (queryModel.ServerSecCode != modelBo.Code) throw new schoolException("验证码错误");
                //else
                //{
                //    queryModel.EndTime = DateTime.Now;
                //    queryModel.Status = 2;
                //}
            }
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        public int UpdateStatus(string id)
        {
            var queryModel = new W_ServerMain();
            queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerMain>(p => p.Id == id);
            if (queryModel.FtimeStemp == null) return 0;
            queryModel.Status = 3;
            return mServiceDAL.Update(queryModel);
        }


        public AmountBo UpdateSecCode(WServerCodeBo modelBo)
        {
            var queryModel = new W_ServerMain();
            queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerMain>(p => p.Id == modelBo.Id);
            if (queryModel.FtimeStemp == null) throw new schoolException(SubCode.Failure.GetHashCode(), "参数错误,未查询到您要操作的数据");
            if (string.IsNullOrEmpty(queryModel.BeginTime.ToString()))
            {
                throw new schoolException("操作失败，请输入第一个服务码");
            }
            else
            {
                //if (queryModel.ServerSecCode != modelBo.Code) throw new schoolException("验证码错误");
                //else
                //{
                queryModel.Price = modelBo.Price;
                queryModel.EndTime = DateTime.Now;
                queryModel.ServerType = modelBo.ServerType;
                queryModel.Status = 2;
                //}
            }
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        public AmountBo SaveEva(WServerMainEvaluationBO modelBo)
        {
            var queryModel = new W_ServerMain();
            queryModel = mServiceDAL.LoadFirstOrDefault<W_ServerMain>(p => p.Id == modelBo.Id);
            if (queryModel.FtimeStemp == null) throw new schoolException(SubCode.Failure.GetHashCode(), "参数错误,未查询到您要操作的数据");
            queryModel.EvaluationMain = modelBo.EvaluationMain;
            queryModel.EvaluationStars = modelBo.EvaluationStars;
            queryModel.Status = 4;
            var amount = mServiceDAL.Update(queryModel);
            foreach (var item in modelBo.List)
            {
                base.Create<W_ServerMainEvaluation>(new W_ServerMainEvaluation { Id = Guid.NewGuid().ToString(), Pic = item.Pic, ServerMainId = modelBo.Id, CreateUserId = queryModel.CreateUserId, CreateTime = DateTime.Now });
            }
            return base.ReturnBo(amount);
        }


        public ServerEvalVO GetEval(string id)
        {
            var model = base.LoadFirstOrDefault<W_ServerMain>(p => p.Id == id);
            var lis = base.LoadEntities<W_ServerMainEvaluation>(p => p.ServerMainId == id).Select(p => p.Pic).ToList();
            var s = new ServerEvalVO()
            {
                EvaluationMain = model?.EvaluationMain,
                EvaluationStars = model.EvaluationStars,
                PicList = SessionFactory.SessionService.mWServerMainEvaluationBLL.GetList(id)
            };
            return s;
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo EscServer(WChangeStatusBO modelBo)
        {
            var model = base.LoadFirstOrDefault<W_ServerMain>(p => p.Id == modelBo.Id);
            CanChange(model.Status, modelBo.Status);
            model.Status = 10;
            var amo = base.Update<W_ServerMain>(model);
            return base.ReturnBo(amo);
        }
        void CanChange(int oStatus, int nStatus)
        {
            if (nStatus == 10)
            {
                if (oStatus != 1)
                    throw new schoolException("操作失败，现在状态不能进行此操作");
            }


        }

    }
}

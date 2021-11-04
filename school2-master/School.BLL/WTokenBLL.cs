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
using System.Data;
using school.Model;
using System.Data.SqlClient;
using school.Common;
using school.Model.Enum;

namespace school.BLL
{
    public class WTokenBLL : BaseBLL<IWTokenDAL>, IWTokenBLL
    {
        public AmountBo CreateOrUpdate(string tokenId)
        {
            W_Token token = new W_Token()
            {
                CreateTime = DateTime.Now,
                CreateUserId = "userId",
                Id = Guid.NewGuid().ToString(),
                TokenId = tokenId
            };
            var amount = mServiceDAL.Create(token);
            return base.ReturnBo(amount);
        }



        public new AmountBo Delete(string id)
        {
            throw new NotImplementedException();
        }

        #region token
        public string Get()
        {
            SqlItem sql = new SqlItem()
            {
                SqlValue = "select top 1 * from W_Token order by createtime desc",
                Params = null
            };
            var dt = base.ExecuteDataTable(sql);
            var vo = base.DataTableToEntities<TokenVO>(dt).FirstOrDefault();
            DateTime dtime = new DateTime();
            dtime = vo == null ? Convert.ToDateTime("2019-1-1") : Convert.ToDateTime(vo.CreateTime);
            if (GetNew(dtime))
            {
                WxSmallTokenVO wxvo = WxUserLibApi.GetToken();
                CreateOrUpdate(wxvo.access_token);
                return wxvo.access_token;
            }
            return vo.TokenId;
        } 

        bool GetNew(DateTime dtime)
        {
            DateTime dtnow = DateTime.Now;
            if (dtime.AddSeconds(6000) >= dtnow)
                return false;
            return true;
        }
        #endregion

        public List<WProductVO> GetList(WProdcutListBO bo)
        {
            throw new NotImplementedException();
        }

        #region Js-sdk

        public AmountBo CreateOrUpdateJs(string ticket)
        {
            W_Js token = new W_Js()
            {
                CreateTime = DateTime.Now,
                CreateUserId = "userId",
                Id = Guid.NewGuid().ToString(),
                Ticket = ticket
            };
            var amount = mServiceDAL.Create(token);
            return base.ReturnBo(amount);
        }

        public string GetTicket()
        {
            SqlItem sql = new SqlItem()
            {
                SqlValue = "select top 1 * from W_Js order by createtime desc",
                Params = null
            };
            var dt = base.ExecuteDataTable(sql);
            var vo = base.DataTableToEntities<JsVO>(dt).FirstOrDefault();
            DateTime dtime = new DateTime();
            dtime = vo == null ? Convert.ToDateTime("2019-1-1") : Convert.ToDateTime(vo.CreateTime);
            if (GetNew(dtime))
            {
                WxJsVO wxvo = WxUserLibApi.GetJs(Get());
                CreateOrUpdateJs(wxvo.ticket);
                return wxvo.ticket;
            }
            return vo.Ticket;
        }
        #endregion

        #region 卡卷
        /// <summary>
        /// 添加卡卷给用户
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public SqlItem CreateOrUpdateSjs(W_StudentKj bo)
        {
            W_StudentKj code = new W_StudentKj()
            {
                CreateTime = DateTime.Now,
                CreateUserId = bo.CreateUserId,
                Id = Guid.NewGuid().ToString(),
                Price = bo.Price,
                Status = 0,
                UnionId = bo.CreateUserId,
                StudentId = bo.CreateUserId,
                UpdateTime = DateTime.Now,
                EndTime = DateTime.Now.AddYears(1)

            };
            return base.InsertSqlItem<W_StudentKj>(code);
        }

        #endregion

        #region dx 发送短信
        public AmountBo CreateOrUpdateMS(W_Code bo)
        {
            if (!string.IsNullOrEmpty(bo.ShareInfoId))
            {
                var count = base.Count<W_Code>(p => p.Phone == bo.Phone && p.ShareInfoId == bo.ShareInfoId && p.Status == 1);
                if (count > 0)
                    throw new schoolException(SubCode.Failure.GetHashCode(), "不能重复领取！");
            }
            Random r = new Random();
            var num = r.Next(10000, 99999).ToString();
            W_Code code = new W_Code()
            {
                CreateTime = DateTime.Now,
                CreateUserId = bo.CreateUserId,
                Id = Guid.NewGuid().ToString(),
                Code = num,
                Status = 0,
                UnionId = bo.UnionId,
                StudentId = bo.StudentId,
                Phone = bo.Phone,
                ShareInfoId = bo.ShareInfoId,
                Price = bo.Price
            };
            var amount = mServiceDAL.Create(code);
            WxUserLibApi.sends(bo.Phone, num);

            return base.ReturnBo(amount);
        }
        bool GetNew(DateTime dtime, int tick)
        {
            DateTime dtnow = DateTime.Now;
            if (dtime.AddSeconds(tick) >= dtnow)
                return false;
            return true;
        }
        public bool UpdateCode(string phone, string code)
        {
            var query = base.LoadFirstOrDefault<W_Code>(p => p.Phone == phone && p.Code == code && p.Status == 0);
            if (query.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "验证码错误！");
            if (GetNew(Convert.ToDateTime(query.CreateTime), 300))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), "验证码超时请重新获取！");
            }
            List<SqlItem> sqls = new List<SqlItem>();
            SqlParameter[] par =
               {
                   new SqlParameter("@phone", phone),
                   new SqlParameter("@code", code),
                   new SqlParameter("@UpdateTime", DateTime.Now),
            };
            SqlItem sql = new SqlItem()
            {
                SqlValue = "Update W_Code set status=1,UpdateTime=@UpdateTime where phone=@phone and code=@code",
                Params = par.ToArray()
            };
            sqls.Add(sql);
            if (!string.IsNullOrEmpty(query.Price) && Convert.ToInt32(query.Price) > 0)
                sqls.Add(CreateOrUpdateSjs(new W_StudentKj { UnionId = query.UnionId, Price = query.Price, CreateUserId = query.CreateUserId, Status = 0, StudentId = query.StudentId ,CreateTime=DateTime.Now}));
            return base.ExecuteSqlTran(sqls.ToArray()) > 0;
        }
        #endregion
    }
}

using school.IBLL;
using school.BLL.Base;
using System;
using System.Collections.Generic;
using school.IDAL;
using school.Model.BO;
using school.Model.DAO;
using school.Common.Tools;
using System.Linq;
using System.Data.SqlClient;
using school.Model.VO;
using System.Linq.Expressions;

namespace school.BLL
{
    public class StudentBLL : BaseBLL<IStudentDAL>, IStudentBLL
    {
        public string CreateOrUpdate(StudentBO modelBo)
        {
            var amount = 0;
            var queryModel = new M_Student();
            if (!string.IsNullOrEmpty(modelBo.Id))
            {
                queryModel = mServiceDAL.LoadFirstOrDefault<M_Student>(p => p.Id == modelBo.Id);
            }

            if (queryModel.FtimeStemp == null && !string.IsNullOrEmpty(modelBo.UnionId))
            {
                queryModel = mServiceDAL.LoadFirstOrDefault<M_Student>(p => p.UnionId == modelBo.UnionId);
                modelBo.Id = queryModel.Id;
            }
            ObjectHelper.AutoMapping(modelBo, queryModel, new string[] { "openId", "UnionId", "FtimeStemp", "CreateTime", "CreateUserId", "SmOpenId", "tel" });
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateParse.GetDateTime();
                queryModel.CreateUserId = queryModel.Id;
                queryModel.UnionId = modelBo.UnionId;
                queryModel.OpenId = modelBo.OpenId;
                queryModel.SmOpenId = modelBo.SmOpenId;
                amount = mServiceDAL.Create(queryModel);
            }
            else
            {
                queryModel.UpdateTime = DateParse.GetDateTime();
                queryModel.UpdateUserId = modelBo.Id;
                amount = mServiceDAL.Update(queryModel);
            }
            return queryModel.Id;
        }
        public AmountBo CreateOrUpdateStudent(string id, string fullName, string tel, int types, string userId)
        {
            var amount = 0;
            var queryModel = new M_Student();
            queryModel = mServiceDAL.LoadFirstOrDefault<M_Student>(p => p.Id == id);
            queryModel.Tel = tel;
            queryModel.FullName = fullName;

            if (queryModel.Type != types)
            {
                var te = base.LoadFirstOrDefault<M_Teacher>(p => p.StudentId == queryModel.Id);
                if (types == 1)
                {
                    if (te.FtimeStemp == null)
                    {
                        te.Id = Utils.GetGuid();
                        te.Name = fullName;
                        te.Tel = tel;
                        te.Pic = queryModel.HeadImgUrl;
                        te.StudentId = queryModel.Id;
                        te.CreateTime = DateTime.Now;
                        te.CreateUserId = userId;
                        base.Create<M_Teacher>(te);
                    }
                }
                else
                {
                    if (te.FtimeStemp != null)
                    {
                        base.Delete(te);
                    }
                }
            }
            queryModel.Type = types;
            queryModel.UpdateTime = DateParse.GetDateTime();
            queryModel.UpdateUserId = userId;
            amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        #region
        public AmountBo UpdateStudentSmOpenId(string unionId, string openId)
        {
            var amount = 0;
            var queryModel = new M_Student();
            queryModel = mServiceDAL.LoadFirstOrDefault<M_Student>(p => p.UnionId == unionId);
            queryModel.SmOpenId = openId;
            amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(1);
        }


        #endregion


        public bool UpdateTel(string unionId, string id, string tel, string studentId)
        {
            SqlParameter[] par =
            {
                new SqlParameter("@unionId", unionId),
                new SqlParameter("@id", id),
                new SqlParameter("@tel", tel),
                new SqlParameter("@studentId",studentId)
            };
            var sqllist = new List<SqlItem>();

            var sql = new SqlItem()
            {
                SqlValue = "update M_student set Tel=@tel where id=@id",
                Params = par.ToArray()
            };
            var sql2 = new SqlItem()
            {
                SqlValue = "update M_teacher set Tel=@tel where studentId=@studentId",
                Params = par.ToArray()
            };
            sqllist.Add(sql);
            sqllist.Add(sql2);
            return base.ExecuteSqlTran(sqllist.ToArray()) > 0;
        }

        public AmountBo CreateTableLog()
        {
            return base.ReturnBo(1);
        }

        public List<M_Student> GetList(PageInfo bo)
        {
            int totNum = 0;
            var list = new List<M_Student>();
            var whereQl = BuildExpression<M_Student>(p => p.Deleted == 0);
            IEnumerable<M_Student> noumena = null;
            noumena = mServiceDAL.LoadEntities(whereQl);
            totNum = noumena.Count();
            if (!noumena.Any()) return list;
            list = noumena.OrderByDescending(x => x.Type).ThenByDescending(x => x.CreateTime).ToList();
            foreach (var item in list)
            {
                item.OpenId = "";
            }
            return list;
        }

        public M_Student GetOne(string Id, bool show = false)
        {
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Student>(p => p.Id == Id);

            if (!show)
            {
                queryModel.OpenId = "";
                queryModel.UnionId = "";
            }
            return queryModel;
        }


        public M_Student GetOneByWechat(string openId)
        {
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Student>(p => p.OpenId == openId);
            //queryModel.OpenId = ""; queryModel.UnionId = ""; queryModel.SmOpenId = "";
            return queryModel;
        }
        public M_Student GetOneBySmOpenId(string smopenId)
        {
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Student>(p => p.SmOpenId == smopenId);
            //  queryModel.OpenId = ""; queryModel.UnionId = "";queryModel.SmOpenId = "";
            return queryModel;
        }



        public M_Student GetOneByUionId(string UnionId)
        {
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Student>(p => p.UnionId == UnionId);
            // queryModel.OpenId = ""; queryModel.UnionId = ""; queryModel.SmOpenId = "";
            return queryModel;
        }



        #region //学生订单
        public List<OrderVO> GetOrderList(IdStatusListBO bo)
        {
            int totNum = 0;
            var list = new List<OrderVO>();
            List<int> sta = new List<int>();
            if (!string.IsNullOrEmpty(bo.Status))
            {
                string[] sp = bo.Status.Split(',');
                foreach (var item in sp)
                {
                    sta.Add(Int32.Parse(item));
                }
            }
            var whereQl = BuildExpression<M_Order>(
                 BuildLambda<M_Order>(bo.Status, p => sta.Contains(p.Status)),
                 BuildLambda<M_Order>(bo.CategoryId, p => p.Category == Int32.Parse(bo.CategoryId)),
                 BuildLambda<M_Order>(bo.Id, p => p.StudentId == bo.Id));
            IEnumerable<M_Order> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_Order>(whereQl);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime);
            }
            bo.TotNum = totNum;
            var fi = base.LoadEntities<M_Field>(p => p.Id != null).ToList();
            var stu = base.LoadEntities<M_Student>(p => p.Id != null).ToList();
            var schlist = base.LoadEntities<M_School>(p => p.Id != null).ToList();
            if (!noumena.Any()) return list;
            List<M_Order> olist = noumena.OrderByDescending(x => x.CreateTime).ToList();
            foreach (var p in olist)
            {
                var fone = fi.FirstOrDefault(x => x.Id == p.FieldId);
                var sch = schlist.FirstOrDefault(x => x.Id == fone.SchoolId);
                var sont = stu.FirstOrDefault(x => x.Id == p.StudentId);
                var ol = new OrderVO()
                {
                    Id = p.Id,
                    Day = p.Day,
                    Status = p.Status,
                    FieldId = p.FieldId,
                    FieldName = fone?.Name,
                    Price = p.Price,
                    StudentName = sont?.NickName,
                    StudentId = p.StudentId,
                    Name = p.Name,
                    Tel = p.Tel,
                    Pic = fone?.Pic,
                    X = fone?.X,
                    Y = fone?.Y,
                    Category=p.Category,
                    StudentTel = sont?.Tel,
                    CreateTime = Convert.ToDateTime(p.CreateTime).ToString("yyyy-MM-dd HH:mm:ss"),
                    SchoolTel = sch?.Tel,
                    SchoolName = sch?.Name
                };
                if (ol.Status == 1)
                {
                    if (ol.Day < Int32.Parse(DateTime.Now.ToString("yyyyMMdd"))&&ol.Status==1)
                    {
                        ol.Status = 5;//已完成(0.未付款，1已付款，2申请退款，3..4，教练同意5.完成
                    }
                }
                List<int> hlist = new List<int>();
                var hl = base.LoadEntities<M_Subscribe>(x => x.OrderId == p.Id).ToList();
                foreach (var l in hl)
                {
                    hlist.Add(l.Hour);
                }
                ol.HourList = hlist;
                list.Add(ol);
            }
            return list;
        }

        public OrderVO GetOrder(string id)
        {
            var model = new OrderVO();
            var mo = base.LoadFirstOrDefault<M_Order>(p => p.Id == id);
            ObjectHelper.AutoMapping(mo, model);

            var fi = base.LoadFirstOrDefault<M_Field>(p => p.Id == mo.FieldId);

            var sch = base.LoadFirstOrDefault<M_School>(p => p.Id == fi.SchoolId);

            List<int> hlist = new List<int>();
            var hl = base.LoadEntities<M_Subscribe>(x => x.OrderId == mo.Id).ToList();
            foreach (var l in hl)
            {
                hlist.Add(l.Hour);
            }

            model.CreateTime = Convert.ToDateTime(mo.CreateTime).ToString("yyyy-MM-dd HH:mm:ss");
            model.X = fi?.X;
            model.Y = fi?.Y;
            model.Pic = fi?.Pic;
            model.HourList = hlist;
            model.Category = mo.Category;
            model.FieldName = fi?.Name;
            model.SchoolName = sch?.Name;
            model.SchoolTel = sch?.Tel;
            model.Address = fi?.Address;
            return model;
        }
        #endregion


        #region 代金卷
        public List<StudentKjVO> GetKjList(IdStatusListBO bo)
        {
            int totNum = 0;
            var list = new List<StudentKjVO>();
            IEnumerable<W_StudentKj> noumena = null;
            if (bo.Status == "3")
            {
                var whereQl = BuildExpression<W_StudentKj>(
                     BuildLambda<W_StudentKj>(bo.Id, p => p.StudentId == bo.Id),
                     BuildLambda<W_StudentKj>(bo.CategoryId, p => p.CategoryId == bo.CategoryId),
                      p => p.Status == 0 && p.EndTime < DateTime.Now);

                if (bo.PageSize == 0)
                {
                    noumena = mServiceDAL.LoadEntities<W_StudentKj>(whereQl);
                    totNum = noumena.Count();
                }
                else
                {
                    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                        out totNum, whereQl, false, p => p.CreateTime);
                }
                bo.TotNum = totNum;
            }

            else
            {
                var whereQl = BuildExpression<W_StudentKj>(
                     BuildLambda<W_StudentKj>(bo.Id, p => p.StudentId == bo.Id),
                     BuildLambda<W_StudentKj>(bo.CategoryId, p => p.CategoryId == bo.CategoryId),
                     BuildLambda<W_StudentKj>(bo.Status, p => p.Status == Int32.Parse(bo.Status)));
                if (bo.PageSize == 0)
                {
                    noumena = mServiceDAL.LoadEntities<W_StudentKj>(whereQl);
                    totNum = noumena.Count();
                }
                else
                {
                    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                        out totNum, whereQl, false, p => p.CreateTime);
                }
                bo.TotNum = totNum;
            }

            if (!noumena.Any()) return list;
            List<W_StudentKj> olist = noumena.OrderByDescending(x => x.CreateTime).ToList();
            foreach (var p in olist)
            {
                var ol = new StudentKjVO();
                ObjectHelper.AutoMapping(p, ol);
                ol.Name = p.Name;
                list.Add(ol);
            }
            return list;
        }
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetKjCount(string id, string categoryId)
        {
            if (!string.IsNullOrEmpty(categoryId))
                return base.Count<W_StudentKj>(p => p.CategoryId == categoryId && p.UnionId == id && p.Status == 0);
            return base.Count<W_StudentKj>(p => p.UnionId == id);
        }

        public KeyValueBo StudengKjFirst(string id, string categoryId)
        {
            var key = new KeyValueBo();
            var mo = base.LoadFirstOrDefault<W_StudentKj>(p => p.CategoryId == categoryId && p.UnionId == id);
            if (mo != null)
            {
                key.Id = mo?.Id;
                key.Name = mo?.Price;
            }
            return key;
        }
        #endregion
    }
}

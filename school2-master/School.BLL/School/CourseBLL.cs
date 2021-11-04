using school.IBLL;
using school.BLL.Base;
using System;
using System.Collections.Generic;
using school.IDAL;
using school.Model.BO;
using school.Model.DAO;
using school.Common.Tools;
using System.Linq;
using school.Common;
using school.Model.Enum;
using System.Data.SqlClient;
using school.Model.TO.Request;
using school.Model.VO;
using System.Data;
using System.Threading.Tasks;

namespace school.BLL
{
    public class CourseBLL : BaseBLL<ICourseDAL>, ICourseBLL
    {
        #region 课程
        public AmountBo CreateOrUpdate(CourseBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Course>(p => p.Id == modelBo.Id);
            ObjectHelper.AutoMapping(modelBo, queryModel);
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateTime.Now;
                queryModel.CreateUserId = modelBo.UserId;
                amount = mServiceDAL.Create(queryModel);
            }
            else
            {
                queryModel.UpdateTime = DateTime.Now;
                queryModel.UpdateUserId = modelBo.UserId;
                amount = mServiceDAL.Update(queryModel);
            }
            return base.ReturnBo(amount);
        }


        public List<M_Course> GetList(CourseListBO bo)
        {
            int totNum = 0;
            var list = new List<M_Course>();

            var whereQl = BuildExpression<M_Course>(
                 BuildLambda<M_Course>(true, p => p.Deleted == 0),
                 BuildLambda<M_Course>(bo.CategoryId != 0, p => p.CategoryId == bo.CategoryId),
                 BuildLambda<M_Course>(bo.AgeCategoryId != 0, p => p.AgeCategoryId == bo.AgeCategoryId));
            IEnumerable<M_Course> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_Course>(whereQl);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.Sort);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            return noumena.ToList();
        }

        public M_Course GetOne(string Id)
        {
            var model = base.LoadFirstOrDefault<M_Course>(p => p.Id == Id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            model.CreateUserId = "";
            return model;
        }


        public AmountBo DeleteCourse(string id, string userId)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Course>(p => p.Id == id);
            queryModel.Deleted = 1;
            queryModel.UpdateTime = DateTime.Now;
            queryModel.UpdateUserId = userId;
            amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        #endregion


        #region 订阅时间
        #region 订单
        List<SqlItem> AddOrder(OrderBO modelBo, string name, string tel, string id, int category)
        {
            List<SqlItem> sqlitems = new List<SqlItem>();
            var m1 = 0;var m2 = 0; 
            var ms = mServiceDAL.LoadFirstOrDefault<M_FilePrice>(p => p.Id == "123");
            foreach (var item in modelBo.HourList)
            {
                if (ms.sorttime >= item)
                {
                    m1++;
                }
                else
                    m2++;
            }
            int tot = m2 * Int32.Parse(ms.PriceSec) + Int32.Parse(ms.Price) * m1;
            var mo = new M_Order()
            {
                CreateTime = DateTime.Now,
                CreateUserId = modelBo.UserId,
                Status = category == 0 ? 0 : 1,
                StudentId = modelBo.StudentId,
                FieldId = modelBo.FieldId,
                //Price = Convert.ToDecimal(Convert.ToDecimal(modelBo.Price) * modelBo.HourList.Count()).ToString(),
                Price=tot.ToString(),
                Day = modelBo.Day,
                Id = id,
                Name = name,
                Tel = tel,
                Category = category
            };

            sqlitems.AddRange(AddSub(new SubscribeBO { Day = modelBo.Day, FieldId = modelBo.FieldId, HourList = modelBo.HourList, Id = mo.Id, Status = 0, StudentId = modelBo.StudentId, UserId = modelBo.UserId, OrderId = mo.Id }));
            sqlitems.Add(base.InsertSqlItem<M_Order>(mo));
            return sqlitems;
        }
        /// <summary>
        /// 订单
        /// </summary>
        /// <param name="bo"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<string> CreateOrUpdateOrder(List<OrderBO> bo, string UserId, string name, string tel, string kj, int category)
        {
            List<SqlItem> sqlitems = new List<SqlItem>();
            var fieldList = base.LoadEntities<M_Field>(p => p.Id != null);
            List<string> list = new List<string>();
            foreach (var item in bo)
            {
                var ids = Utils.GetGuid();
                list.Add(ids);
                item.Price = fieldList.LastOrDefault<M_Field>(p => p.Id == item.FieldId)?.Price;
                item.UserId = UserId;
                sqlitems.AddRange(AddOrder(item, name, tel, ids, category));
            }
            var amount = base.ExecuteSqlTran(sqlitems.ToArray());
            if (amount > 0)
            {
                return list;
            }
            throw new schoolException(SubCode.Failure.GetHashCode(), "操作失败请重试！");

        }


        #region
        public AmountBo UpdateOrder(string id)
        {
            var mo = base.LoadFirstOrDefault<M_Order>(p => p.Id == id);
            int mos = mo.Status;
            mo.Status = 1;
            var amount = mServiceDAL.Update<M_Order>(mo);
            if (mos != 1&&amount==1)
            {
                var fi = base.LoadFirstOrDefault<M_Field>(x => x.Id == mo.FieldId);
                if (fi.FtimeStemp != null)
                {
                    var sc = base.LoadFirstOrDefault<M_School>(x => x.Id == fi.SchoolId);
                    if (sc.FtimeStemp != null)
                    {
                        if (!string.IsNullOrEmpty(sc.Tel))
                        {
                            Task.Factory.StartNew(() =>  WxUserLibApi.sendsSchool(sc.Tel, sc.Name, mo.Day.ToString(),fi.Name,mo.Tel));
                        }
                    } 
                }
            }
            return base.ReturnBo(amount);
        }

        public M_Order OrderOnlyPay(string id)
        {
            return base.LoadFirstOrDefault<M_Order>(p => p.Id == id);
        }
        #endregion


        #endregion


        #region 时间

        List<SqlItem> AddSub(SubscribeBO modelBo)
        {
            List<SqlItem> sqlitems = new List<SqlItem>();
            var mo = new M_Subscribe()
            {
                CreateTime = DateTime.Now,
                CreateUserId = modelBo.UserId,
                Status = 0,
                OrderId = modelBo.OrderId,
                StudentId = modelBo.StudentId,
                FieldId = modelBo.FieldId,
                TeacherCourseId = modelBo.TeacherCourseId,
                ActivityId = modelBo.ActivityId,
                Day = modelBo.Day,

                //Id = Utils.GetGuid()
            };
            foreach (var item in modelBo.HourList)
            {
                mo.Hour = item;
                mo.Id = Utils.GetGuid();
                sqlitems.Add(base.InsertSqlItem<M_Subscribe>(mo));
            }
            return sqlitems;
        }

        public AmountBo CreateOrUpdateSub(SubscribeAddBO bo)
        {
            List<SqlItem> sqlitems = new List<SqlItem>();
            foreach (var item in bo.List)
            {
                item.UserId = bo.UserId;
                sqlitems.AddRange(AddSub(item));
            }
            var amount = base.ExecuteSqlTran(sqlitems.ToArray());
            return base.ReturnBo(amount);

        }
        public List<SqlItem> AddSubscribeSqlItem(SubscribeAddBO bo)
        {
            List<SqlItem> sqlitems = new List<SqlItem>();
            foreach (var item in bo.List)
            {
                item.UserId = bo.UserId;
                sqlitems.AddRange(AddSub(item));
            }
            return sqlitems;
        }

        /// <summary>
        /// 取消成功
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public AmountBo EscSub(SubscribeESC bo, string status)
        {

            List<SqlItem> itemList = new List<SqlItem>();
            if (bo.OrderId != null)
            {
                foreach (var item in bo.OrderId)
                {
                    SqlParameter[] par =
              {
                new SqlParameter("@OrderId",  item),
                new SqlParameter("@TeacherCourseId", bo.TeacherCourseId),
                new SqlParameter("@dtime",DateTime.Now),
                new SqlParameter("@userId",bo.UserId),
                new SqlParameter("@Status",Int32.Parse(status)),
            };
                    SqlItem sql1 = new SqlItem()
                    {
                        SqlValue = "update M_Subscribe set Status=1 , updateUserId=@userId ,UpdateTime=@dtime where OrderId=@OrderId",
                        Params = par.ToArray()
                    };


                    SqlItem sql2 = new SqlItem()
                    {
                        SqlValue = "update M_Order set Status=@Status, updateUserId=@userId,UpdateTime=@dtime where Id=@OrderId",
                        Params = par.ToArray()
                    };
                    itemList.Add(sql1);
                    itemList.Add(sql2);
                }
            }
            if (!string.IsNullOrEmpty(bo.TeacherCourseId))
            {
                SqlParameter[] par =
                {
                new SqlParameter("@OrderId", ""),
                new SqlParameter("@TeacherCourseId", bo.TeacherCourseId),
                new SqlParameter("@dtime",DateTime.Now),
                new SqlParameter("@userId",bo.UserId),
                new SqlParameter("@Status",Int32.Parse(status)),
                };
                var sql1 = new SqlItem()
                {
                    SqlValue = "update M_Subscribe set Status=1,updateUserId=@userId,UpdateTime=@dtime where TeacherCourseId=@TeacherCourseId",
                    Params = par.ToArray()
                };
                var sql2 = new SqlItem()
                {
                    SqlValue = "update M_Teacher set Status=@Status, updateUserId=@userId,UpdateTime=@dtime where Id=@OrderId",
                    Params = par.ToArray()
                };
                itemList.Add(sql1);
                itemList.Add(sql2);
            }
            base.ExecuteSqlTran(itemList.ToArray());
            return base.ReturnBo(1);
        }


        /// <summary>
        /// 申请取消
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public AmountBo ApplyEscSub(SubscribeESC bo)
        {

            List<SqlItem> itemList = new List<SqlItem>();

            if (bo.OrderId != null)
            {
                foreach (var item in bo.OrderId)
                {
                    var models = base.LoadFirstOrDefault<M_Order>(p => p.Id == item);
                    if (models.FtimeStemp == null)
                        throw new schoolException(SubCode.Failure.GetHashCode(), "参数错误非法操作！");
                    if (models.Status == 2)
                        throw new schoolException(SubCode.Failure.GetHashCode(), "您已经申请取消无需再次提交请等待处理！");
                    if (models.Status == 4)
                        throw new schoolException(SubCode.Failure.GetHashCode(), "本状态不能进行取消");
                    SqlParameter[] par =
              {
                new SqlParameter("@OrderId",item),
                new SqlParameter("@TeacherCourseId",bo.TeacherCourseId),
                new SqlParameter("@dtime",DateTime.Now),
                new SqlParameter("@userId",bo.UserId),
            };
                    //par[0].Value = bo.OrderId;
                    //par[1].Value = bo.TeacherCourseId;
                    //par[2].Value = DateTime.Now;
                    //par[3].Value = bo.UserId;
                    SqlItem sql1 = new SqlItem()
                    {
                        SqlValue = "update M_Order set Status=2, updateUserId=@userId,UpdateTime=@dtime where Id=@OrderId",
                        Params = par.ToArray()
                    };
                    itemList.Add(sql1);
                }
            }
            base.ExecuteSqlTran(itemList.ToArray());
            return base.ReturnBo(1);
        }


        public AmountBo ApplyEscSubEsc(SubscribeESC bo)
        {

            List<SqlItem> itemList = new List<SqlItem>();
            if (bo.OrderId != null)
            {
                foreach (var item in bo.OrderId)
                {
                    SqlParameter[] par =
              {
                new SqlParameter("@OrderId", item),
                new SqlParameter("@TeacherCourseId", bo.TeacherCourseId),
                new SqlParameter("@dtime",DateTime.Now),
                new SqlParameter("@userId",bo.UserId),
            };

                    SqlItem sql1 = new SqlItem()
                    {
                        SqlValue = "update M_Order set Status=1, updateUserId=@userId,UpdateTime=@dtime where Id=@OrderId",
                        Params = par.ToArray()
                    };
                    itemList.Add(sql1);
                }
            }
            base.ExecuteSqlTran(itemList.ToArray());
            return base.ReturnBo(1);
        }


        public List<SubVO> GetListSub(string fieldId, int day, int futureDay)
        {
            var list = new List<SubVO>();

            var whereQl = BuildExpression<M_Subscribe>(
                 BuildLambda<M_Subscribe>(fieldId, p => p.FieldId == fieldId),
                 BuildLambda<M_Subscribe>(day != 0, p => p.Day >= day && p.Day <= day + futureDay));
            IEnumerable<M_Subscribe> noumena = null;
            noumena = mServiceDAL.LoadEntities<M_Subscribe>(whereQl);
            if (!noumena.Any()) return list;

            for (int i = 0; i < futureDay; i++)
            {
                SubVO vo = new SubVO();
                vo.Day = day + i;
                List<int> hourl = new List<int>();
                var lis = noumena.ToList().FindAll(p => p.Day == day + i).OrderBy(p => p.Hour).ToList();
                foreach (var item in lis)
                {
                    hourl.Add(item.Hour);
                }
                vo.HourList = hourl;
                list.Add(vo);
            }
            return list;
        }


        public List<SubVO> GetListSubBySchoolId(string schoolId, int day, int dctype = 0)
        {
            var list = new List<SubVO>();

            var schoolModel = base.LoadFirstOrDefault<M_School>(p => p.Id == schoolId);
            var fieldList = base.LoadEntities<M_Field>(p => p.SchoolId == schoolId && p.Deleted == 0, false, p => p.Sort);  
            foreach (var item in fieldList)
            {
                SubVO vo = new SubVO()
                {
                    FieldId = item.Id,
                    Day = day,
                    Name = item.Name,
                    Price = item.Price,
                    Pricesec = item.Pricesec,
                    sorttime = item.sorttime,
                    SchoolName = item.Name
                }; 
                var sublist = base.LoadEntities<M_Subscribe>(p => p.Day == day && p.FieldId == item.Id && p.TeacherCourseId == null&&p.Status==0, true, p => p.Hour).ToList();
                if (dctype == 1)// 非课程订场，过滤所有场地
                {
                    sublist = base.LoadEntities<M_Subscribe>(p => p.Day == day && p.FieldId == item.Id && p.Status == 0, true, p => p.Hour).ToList();
                }
                List<int> hlist = new List<int>();
                foreach (var subitem in sublist)
                {
                    hlist.Add(subitem.Hour);
                }
                vo.HourList = hlist;
                list.Add(vo);
            }
            return list;

        }

        //public M_Subscribe GetOne(string Id)
        //{
        //    var model = base.LoadFirstOrDefault<M_Course>(p => p.Id == Id);
        //    if (model.FtimeStemp == null)
        //        throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
        //    model.CreateUserId = "";
        //    return model;
        //}
        #endregion
        #endregion


        #region 器械
        public AmountBo CreateOrUpdateEqu(EquBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Equ>(p => p.Id == modelBo.Id);
            ObjectHelper.AutoMapping(modelBo, queryModel);
            var sch = base.LoadFirstOrDefault<M_School>(p => p.Id == modelBo.SchoolId);
            queryModel.SchoolName = sch?.Name;
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateTime.Now;
                queryModel.CreateUserId = modelBo.UserId;
                amount = mServiceDAL.Create(queryModel);
            }
            else
            {
                queryModel.UpdateTime = DateTime.Now;
                queryModel.UpdateUserId = modelBo.UserId;
                amount = mServiceDAL.Update(queryModel);
            }
            return base.ReturnBo(amount);
        }

        public EquVO GetEquOne(string id)
        {
            var model = base.LoadFirstOrDefault<M_Equ>(p => p.Id == id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            var vo = new EquVO();
            ObjectHelper.AutoMapping(model, vo);
            vo.UserTime = model.UserTime;
            return vo;
        }

        public ListEquVO GetEquList(EquListBO bo)
        {
            var vo = new ListEquVO();
            var list = new List<EquVO>();

            var whereQl = BuildExpression<M_Equ>(
                 BuildLambda<M_Equ>(bo.SchoolId, p => p.SchoolId == bo.SchoolId),
                   BuildLambda<M_Equ>(true, p => p.Deleted == 0),
                 BuildLambda<M_Equ>(bo.Btime, p => p.UserTime >= Convert.ToDateTime(bo.Btime)),
                 BuildLambda<M_Equ>(bo.Etime, p => p.UserTime <= Convert.ToDateTime(bo.Etime)));
            IEnumerable<M_Equ> noumena = null;
            //noumena = mServiceDAL.LoadEntities<M_Equ>(whereQl).OrderByDescending(p=>p.UserTime).ThenByDescending(p=>p.CreateTime);
            int totNum = 0;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_Equ>(whereQl).OrderByDescending(p => p.UserTime).ThenByDescending(p => p.CreateTime);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize, out totNum, whereQl, false, p => p.UserTime);
            }
            bo.TotNum = totNum;

            vo.List = new List<EquVO>();
            vo.AllTot = "0";
            if (!noumena.Any()) return vo;
            decimal tot = 0;
            //vo.List = noumena.ToList();
            foreach (var item in noumena.ToList())
            {
                EquVO ovo = new EquVO()
                {
                    Name = item.Name,
                    Price = item.Price,
                    SchoolId = item.SchoolId,
                    SchoolName = item.SchoolName,
                    UserTime = item.UserTime
                };
                tot += Convert.ToDecimal(item.Price);
                list.Add(ovo);
            }
            vo.TotNum = bo.TotNum;
            vo.AllTot = tot.ToString();
            vo.List = list;
            return vo;
        }
        public M_Equ GetEquOneUserAdmin(string id)
        {
            var model = base.LoadFirstOrDefault<M_Equ>(p => p.Id == id);
            return model;
        }

        public List<M_Equ> GetEquListUserAdmin(string shcoolId)
        {
            var list = new List<M_Equ>();

            var whereQl = BuildExpression<M_Equ>(
                 BuildLambda<M_Equ>(shcoolId, p => p.SchoolId == shcoolId),
                   BuildLambda<M_Equ>(true, p => p.Deleted == 0));
            IEnumerable<M_Equ> noumena = null;
            int totNum = 0;

            noumena = mServiceDAL.LoadEntities<M_Equ>(whereQl).OrderByDescending(p => p.UserTime).ThenByDescending(p => p.CreateTime);


            if (!noumena.Any()) return list;
            return noumena.ToList();
        }
        #endregion

        #region 字典查询
        public AmountBo CreateOrUpdateCategory(CategoryBO modelBo)
        {

            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Category>(p => p.Id == modelBo.Id);
            ObjectHelper.AutoMapping(modelBo, queryModel);
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateTime.Now;
                queryModel.CreateUserId = modelBo.UserId;
                amount = mServiceDAL.Create(queryModel);
            }
            else
            {
                queryModel.UpdateTime = DateTime.Now;
                queryModel.UpdateUserId = modelBo.UserId;
                amount = mServiceDAL.Update(queryModel);
            }
            return base.ReturnBo(amount);
        }

        public M_Category GetCatergoryOne(string id)
        {

            var model = base.LoadFirstOrDefault<M_Category>(p => p.Id == id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            return model;
        }

        public List<M_Category> GetCatergoryParList()
        {
            var list = new List<M_Category>();

            var whereQl = BuildExpression<M_Category>(
                 BuildLambda<M_Category>(true, p => p.parentId == null),
                   BuildLambda<M_Category>(true, p => p.Deleted == 0));
            IEnumerable<M_Category> noumena = null;

            noumena = mServiceDAL.LoadEntities<M_Category>(whereQl).OrderBy(p => p.Category).OrderByDescending(p => p.Sort).ThenByDescending(p => p.CreateTime);
            if (!noumena.Any()) return list;
            return noumena.ToList();
        }

        public List<M_Category> GetCatergoryList(string parentId)
        {
            var list = new List<M_Category>();

            var whereQl = BuildExpression<M_Category>(
                 BuildLambda<M_Category>(parentId, p => p.parentId == parentId),
                   BuildLambda<M_Category>(true, p => p.Deleted == 0));
            IEnumerable<M_Category> noumena = null;

            noumena = mServiceDAL.LoadEntities<M_Category>(whereQl).OrderByDescending(p => p.Sort).ThenByDescending(p => p.CreateTime);
            if (!noumena.Any()) return list;
            return noumena.ToList();
        }
        public List<M_Category> GetCatergoryListByCategoryOne(string category)
        {
            var list = new List<M_Category>();

            var whereQl = BuildExpression<M_Category>(
                 BuildLambda<M_Category>(category, p => p.Category == category),
                   BuildLambda<M_Category>(true, p => p.Deleted == 0));
            IEnumerable<M_Category> noumena = null;

            noumena = mServiceDAL.LoadEntities<M_Category>(whereQl).OrderByDescending(p => p.Sort).ThenByDescending(p => p.CreateTime);
            if (!noumena.Any()) return list;
            return noumena.ToList();
        }

        public List<CategoryListVO> GetCatergoryListByCategory(string category)
        {
            var list = new List<CategoryListVO>();
            var whereQl = BuildExpression<M_Category>(
                 BuildLambda<M_Category>(category, p => p.Category == category),
                   BuildLambda<M_Category>(true, p => p.Deleted == 0));
            IEnumerable<M_Category> noumena = null;
            noumena = mServiceDAL.LoadEntities<M_Category>(whereQl).OrderByDescending(p => p.Sort).ThenByDescending(p => p.CreateTime);
            if (!noumena.Any()) return list;

            var alllist = base.LoadEntities<M_Category>(p => p.Deleted == 0).OrderBy(p => p.CreateTime).ToList();

            foreach (var item in noumena.ToList())
            {
                CategoryListVO co = new CategoryListVO()
                {
                    Category = item.Category,
                    Id = item.Id,
                    Name = item.Name,
                    parentId = item.parentId,
                    Sort = item.Sort
                };
                co.OneList = new List<CategoryList1>();
                var listo = reCategoryList(alllist, item.Id);

                foreach (var item2 in listo)
                {
                    CategoryList1 co2 = new CategoryList1()
                    {
                        Category = item2.Category,
                        Id = item2.Id,
                        Name = item2.Name,
                        parentId = item2.parentId,
                        Sort = item2.Sort
                    };
                    var list2 = reCategoryList(alllist, item2.Id);
                    co2.Twolist = new List<CategoryList2>();
                    //foreach (var item3 in list2)
                    //{
                    //    CategoryList2 co3 = new CategoryList2()
                    //    {
                    //        Category = item3.Category,
                    //        Id = item3.Id,
                    //        Name = item3.Name,
                    //        parentId = item3.parentId,
                    //        Sort = item3.Sort
                    //    };
                    //    co2.Twolist.Add(co3);
                    //}
                    co.OneList.Add(co2);
                }
                list.Add(co);
            }
            return list;

        }


        public CategoryListAllVO GetCatergoryAll(string category)
        {

            CategoryListAllVO vo = new CategoryListAllVO();
            vo.OneList = new List<M_Category>();
            vo.TwoList = new List<M_Category>();
            vo.ThrList = new List<M_Category>();
            if (category == "1")
            {
                vo.OneList = base.LoadEntities<M_Category>(p => p.Category == "1" && p.Deleted == 0);//年龄
                vo.TwoList = base.LoadEntities<M_Category>(p => p.Category == "2" && p.Deleted == 0);//训练项目
                vo.ThrList = base.LoadEntities<M_Category>(p => p.Category == "3" && p.Deleted == 0);//类型
            }
            if (category == "4")
            {
                vo.OneList = base.LoadEntities<M_Category>(p => p.Category == "4" && p.Deleted == 0);// 技能类别
                vo.TwoList = base.LoadEntities<M_Category>(p => p.Category == "5" && p.Deleted == 0);//训练项目
                vo.ThrList = base.LoadEntities<M_Category>(p => p.Category == "6" && p.Deleted == 0);//类型
            }
            return vo;
        }

        public AmountBo DeleteCategory(string id)
        {
            var mo = base.LoadFirstOrDefault<M_Category>(p => p.Id == id);
            if (mo.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "操作失败数据未查询到！");
            mo.Deleted = 1;
            base.Update(mo);
            return base.ReturnBo(1);
        }
        List<M_Category> reCategoryList(List<M_Category> alllist, string parentId)
        {
            return alllist.FindAll(p => p.parentId == parentId).OrderBy(p => p.CreateTime).ToList();
        }
        #endregion


        #region
        public AmountBo CreateOrUpdateMenu(string menu, string userId)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Menu>(p => p.Id != null);
            //queryModel.Id = Utils.GetGuid();
            queryModel.CreateTime = DateTime.Now;
            queryModel.CreateUserId = userId;
            queryModel.MenuMain = menu;
            amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }



        public M_Menu GetOneMenu()
        {
            var model = base.LoadFirstOrDefault<M_Menu>(p => p.Id != null);
            return model;
        }

        #endregion

        public List<M_Course> GetCourseList(CourseRequest request)
        {
            var list = new List<M_Course>();
            var whereQl = BuildExpression<M_Course>(
                        BuildLambda<M_Course>(true,p => p.Deleted == 0),
                        BuildLambda<M_Course>(request.CategoryId, p => p.CategoryId == request.CategoryId),
                        BuildLambda<M_Course>(request.AgeCategoryId, p => p.AgeCategoryId == request.AgeCategoryId)
                        );
            IEnumerable<M_Course> noumena = null;
            noumena = mServiceDAL.LoadEntities<M_Course>(whereQl);
            if (!noumena.Any()) { return list; }
            else{ return noumena.ToList(); }
 
        }
        public M_Course GetSurplusClassTimes(StudentCour3Request request)
        {
            var momdel = new M_Course();
            var amount = 0;
            //查询原来的订课课程
            var studentCourse = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == request.Id);
            if (studentCourse.CourseId == request.CourseId)
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), "请选择不同的课时！");
            }
            //算出剩余课时应该多少钱，根据原来买的时候的价格进行汇算
            var course = base.LoadFirstOrDefault<M_Course>(p => p.Id == studentCourse.CourseId);
            //旧课时单价，算出旧课时还剩下多少钱
            String unitPrice = (Convert.ToDouble(course.Price) / Convert.ToDouble(course.ClassTimes)).ToString("0.00");
            double totalPrice = Convert.ToDouble(unitPrice) * studentCourse.SurplusClassTimes;
            //算出可以买多少新课时
            var course2 = base.LoadFirstOrDefault<M_Course>(p => p.Id == request.CourseId);
            //现在课程单价
            String newUnitPrice = (Convert.ToDouble(course2.Price) / Convert.ToDouble(course2.ClassTimes)).ToString("0");
            //现在课程剩余整数时间
            int time = (int)Math.Floor(totalPrice / Convert.ToDouble(newUnitPrice));
            double yushu = totalPrice % Convert.ToDouble(newUnitPrice);
            double classtimes = time;
            if (yushu >= (Convert.ToDouble(newUnitPrice) /2))
            {
                classtimes = classtimes + 1;
            }
            momdel.ClassTimes = classtimes.ToString();
            return momdel;
        }
    }
}

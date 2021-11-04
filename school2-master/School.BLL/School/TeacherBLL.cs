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
using school.DAL;
using school.Model.VO;
using System.Data.SqlClient;
using school.Model.TO.Response;
using school.BLL.Factory;

namespace school.BLL
{
    public class TeacherBLL : BaseBLL<ITeatherDAL>, ITeacherBLL
    {
        #region 老师
        public AmountBo CreateOrUpdate(TeacherBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Teacher>(p => p.StudentId == modelBo.StudentId);
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
            var stu = mServiceDAL.LoadFirstOrDefault<M_Student>(p => p.Id == modelBo.StudentId);
            stu.Type = 1;
            mServiceDAL.Update(stu);
            return base.ReturnBo(amount);
        }

        public string login(string user, string pwd)
        {
            var quer = base.LoadFirstOrDefault<M_Teacher>(p => p.Pwd == pwd && p.UserName == user);
            if (quer.FtimeStemp != null)
                return quer.Id;
            return "";
        }

        public AmountBo UpdateTeacherInfo(TeacherBO2 modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Teacher>(p => p.Id == modelBo.Id);
            queryModel.Pic = modelBo.Pic;
            queryModel.Name = modelBo.Name;
            queryModel.Sort = modelBo.Sort;
            queryModel.Tel = modelBo.Tel;
            queryModel.SubMain = modelBo.SubMain;
            queryModel.Year = modelBo.Year;
            queryModel.Valuation = modelBo.Valuation;
            queryModel.Pwd = modelBo.Pwd;

            queryModel.UserName = modelBo.UserName;
            queryModel.UpdateTime = DateTime.Now;
            queryModel.UpdateUserId = modelBo.UserId;
            amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        /// <summary>
        /// 自己修改
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo UpdateTeacherInfoByZJ(TeacherBO2 modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Teacher>(p => p.StudentId == modelBo.StudentId);
            queryModel.Pic = modelBo.Pic;
            queryModel.Name = modelBo.Name;
            queryModel.Tel = modelBo.Tel;
            queryModel.SubMain = modelBo.SubMain;
            queryModel.Year = modelBo.Year;
            queryModel.UpdateTime = DateTime.Now;
            queryModel.UpdateUserId = modelBo.UserId;
            amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }


        public List<TeacherInfoVO> GetList(TeacherListBO bo)
        {
            int totNum = 0;
            var list = new List<TeacherInfoVO>();

            var whereQl = BuildExpression<M_Teacher>();
            //BuildLambda<M_Teacher>(bo.CategoryId!=0, p => p.CategoryId == bo.CategoryId),
            //BuildLambda<M_Teacher>(bo.AgeCategoryId != 0, p => p.AgeCategoryId == bo.AgeCategoryId));
            IEnumerable<M_Teacher> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_Teacher>(whereQl, true, q => new { q.M_Student });
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.Sort, true, q => new { q.M_Student });
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;

            list = noumena.OrderByDescending(x => x.Sort).ThenByDescending(x => x.CreateTime).Select(
               p => new TeacherInfoVO
               {
                   Id = p.Id,
                   Sort = p.Sort,
                   HeadImgUrl = string.IsNullOrEmpty(p.Pic) ? p.M_Student.HeadImgUrl : p.Pic,
                   NickName = string.IsNullOrEmpty(p.Name) ? p.M_Student.NickName : p.Name,
                   SubMain = p.SubMain,
                   Valuation = p.Valuation,
                   Year = p.Year,
                   Main = p.Main,
                   Name = p.Name,
                   StudentId = p.StudentId,
                   Tel = p.Tel
               }).ToList();
            return list;
        }

        public TeacherInfoVO GetOne(string Id)
        {
            var model = base.LoadFirstOrDefault<M_Teacher>(p => p.Id == Id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            model.CreateUserId = "";
            var mo = new TeacherInfoVO();
            ObjectHelper.AutoMapping(model, mo);
            mo.HeadImgUrl = model.Pic;
            mo.NickName = model.Name;
            var te = base.LoadFirstOrDefault<M_Student>(p => p.Id == model.StudentId);
            if (string.IsNullOrEmpty(mo.HeadImgUrl))
                mo.HeadImgUrl = te.HeadImgUrl;
            if (string.IsNullOrEmpty(mo.NickName))
                mo.NickName = te.NickName;
            return mo;
        }



        #endregion

        #region 留言 

        /// <summary>
        /// 添加留言
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo CreateOrUpdateMessage(TeacherMessageBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_TeacherMessage>(p => p.Id == modelBo.Id);
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
        /// <summary>
        /// 获取学生留言
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public List<TeacherMessageVO> GetTeacherMessageList(IdCategoryListBO bo)
        {
            int totNum = 0;
            var list = new List<TeacherMessageVO>();
            IEnumerable<M_TeacherMessage> noumena = null;
            if (bo.Category == MessageToEnum.ToStudent.GetHashCode())//学生使用
            {
                var whereQl = BuildExpression<M_TeacherMessage>(
                BuildLambda<M_TeacherMessage>(true, p => p.StudentId == bo.Id || p.StudentId == bo.ToId),
                BuildLambda<M_TeacherMessage>(true, p => p.TeacherId == bo.ToId || p.TeacherId == bo.Id));
                if (bo.PageSize == 0)
                {
                    noumena = mServiceDAL.LoadEntities<M_TeacherMessage>(whereQl).OrderBy(p => p.CreateTime);
                    totNum = noumena.Count();
                }
                else
                {
                    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                        out totNum, whereQl, false, p => p.CreateTime).OrderBy(p => p.CreateTime);
                }

                bo.TotNum = totNum;
                if (!noumena.Any()) return list;

                var t = base.LoadFirstOrDefault<M_Teacher>(x => x.StudentId == bo.ToId);
                var s = base.LoadFirstOrDefault<M_Student>(x => x.Id == bo.Id);
                foreach (var p in noumena.ToList())
                {
                    bool isyou = (p.CreateUserId == bo.Id);
                    var vo = new TeacherMessageVO
                    {
                        Id = p.Id,
                        CreateTime = p.CreateTime,
                        TeacherId = p.TeacherId,
                        Status = p.Status,
                        Main = p.Main,
                        StudentId = p.StudentId,
                        Title = p.Title,
                        HeadImgUrl = isyou ? s.HeadImgUrl : t.Pic,
                        NickName = isyou ? s.NickName : t.Name,
                        IsYou = isyou
                    };
                    list.Add(vo);
                }

                SqlParameter[] par =
                {
                new SqlParameter("@studentId",bo.Id),
                new SqlParameter("@teacherId",bo.ToId)
                };
                SqlItem sql = new SqlItem()
                {
                    SqlValue = "update M_TeacherMessage set status=1 where StudentId=@studentId and TeacherId=@teacherId",
                    Params = par
                };
                base.ExecuteSqlTran(sql);
            }

            else // (bo.Category == MessageToEnum.ToStudent.GetHashCode())//老师使用
            {
                var whereQl = BuildExpression<M_TeacherMessage>(
                BuildLambda<M_TeacherMessage>(true, p => p.StudentId == bo.ToId || p.StudentId == bo.Id),
                BuildLambda<M_TeacherMessage>(true, p => p.TeacherId == bo.Id || p.TeacherId == bo.ToId));
                if (bo.PageSize == 0)
                {
                    noumena = mServiceDAL.LoadEntities<M_TeacherMessage>(whereQl).OrderBy(p => p.CreateTime);
                    totNum = noumena.Count();
                }
                else
                {
                    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                        out totNum, whereQl, false, p => p.CreateTime).OrderBy(p => p.CreateTime);
                }

                bo.TotNum = totNum;
                if (!noumena.Any()) return list;

                var t = base.LoadFirstOrDefault<M_Teacher>(x => x.StudentId == bo.Id);
                var s = base.LoadFirstOrDefault<M_Student>(x => x.Id == bo.ToId);
                foreach (var p in noumena.ToList())
                {

                    bool isyou = (p.CreateUserId == bo.Id);

                    var vo = new TeacherMessageVO
                    {
                        Id = p.Id,
                        CreateTime = p.CreateTime,
                        TeacherId = p.TeacherId,
                        Status = p.Status,
                        Main = p.Main,
                        StudentId = p.StudentId,
                        Title = p.Title,
                        HeadImgUrl = isyou ? t.Pic : s.HeadImgUrl,
                        NickName = isyou ? t.Name : s.FullName,
                        IsYou = isyou
                    };
                    list.Add(vo);
                }

                SqlParameter[] par =
               {
                new SqlParameter("@studentId",bo.ToId),
                new SqlParameter("@teacherId",bo.Id)
                };
                SqlItem sql = new SqlItem()
                {
                    SqlValue = "update M_TeacherMessage set status=1 where StudentId=@studentId and TeacherId=@teacherId",
                    Params = par
                };
                base.ExecuteSqlTran(sql);
            }


            return list;
        }

        public List<TeacherMessageMainVO> GetTeacherMessageMainList(IdCategoryListBO bo)
        {
            int totNum = 0;
            var list = new List<TeacherMessageMainVO>();
            IEnumerable<M_TeacherMessage> noumena = null;
            if (bo.Category == MessageToEnum.ToStudent.GetHashCode())//学生查看
            {
                var whereQl = BuildExpression<M_TeacherMessage>(
                BuildLambda<M_TeacherMessage>(true, p => p.StudentId == bo.Id));

                if (bo.PageSize == 0)
                {
                    noumena = mServiceDAL.LoadEntities<M_TeacherMessage>(whereQl, true, x => new { x.M_Student }).OrderByDescending(p => p.CreateTime).ToList();
                    totNum = noumena.Count();
                }
                else
                {
                    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                        out totNum, whereQl, false, p => p.CreateTime, true, x => new { x.M_Student }).OrderByDescending(p => p.CreateTime).ToList();
                }
                bo.TotNum = totNum;
                if (!noumena.Any()) return list;
                var llist = noumena.ToList();
                var tealist = base.LoadEntities<M_Teacher>(p => p.Id != null).ToList();
                foreach (var item in llist)
                {
                    if (list.Count(p => p.Id == item.TeacherId) > 0)
                        continue;
                    var tec = tealist.FirstOrDefault<M_Teacher>(p => p.StudentId == item.TeacherId);
                    var vo = new TeacherMessageMainVO()
                    {
                        Id = item.TeacherId,
                        Pic = tec?.Pic,
                        EndTime = Convert.ToDateTime(item.CreateTime).ToString("yyyy-MM-dd HH:mm:ss"),
                        Name = tec?.Name,
                        UnReadNum = llist.Count(p => p.TeacherId == item.TeacherId && p.Status == 0 && p.CreateUserId != item.CreateUserId)
                    };
                    list.Add(vo);
                }
                bo.TotNum = list.Count;
                return list;
            }
            else //老师查询
            {
                var whereQl = BuildExpression<M_TeacherMessage>(
                BuildLambda<M_TeacherMessage>(true, p => p.TeacherId == bo.Id));

                if (bo.PageSize == 0)
                {
                    noumena = mServiceDAL.LoadEntities<M_TeacherMessage>(whereQl, true, x => new { x.M_Student }).OrderByDescending(p => p.CreateTime).ToList();
                    totNum = noumena.Count();
                }
                else
                {
                    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                        out totNum, whereQl, false, p => p.CreateTime, true, x => new { x.M_Student }).OrderByDescending(p => p.CreateTime).ToList();
                }
                bo.TotNum = totNum;
                var llist = noumena.ToList();
                var stuList = base.LoadEntities<M_Student>(p => p.Id != null).ToList();
                foreach (var item in llist)
                {
                    if (list.Count(p => p.Id == item.StudentId) > 0)
                        continue;
                    var vo = new TeacherMessageMainVO()
                    {
                        Id = item.StudentId,
                        Pic = stuList.FirstOrDefault<M_Student>(p => p.Id == item.StudentId)?.HeadImgUrl,
                        EndTime = Convert.ToDateTime(item.CreateTime).ToString("yyyy-MM-dd HH:mm:ss"),
                        Name = stuList.FirstOrDefault<M_Student>(p => p.Id == item.StudentId)?.FullName,
                        UnReadNum = llist.Count(p => p.StudentId == item.StudentId && p.Status == 0 && p.CreateUserId != item.CreateUserId)
                    };
                    list.Add(vo);
                }
                bo.TotNum = list.Count;
                return list;
            }

        }



        #endregion

        #region 老师课程
        /// <summary>
        /// 课程创建
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo CreateOrUpdateTeacherCourse(TeacherCourseBO modelBo)
        {
            if (modelBo.List == null || modelBo.List.Count == 0)
                throw new schoolException(SubCode.Failure.GetHashCode(), Message.Instance.R("v_Nbrq"));
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_TeacherCourse>(p => p.Id == modelBo.Id);
            var hourlist = modelBo.List[0].HourList;
            hourlist.Sort();
            var timestr = "";

            List<SqlItem> sqlItem = new List<SqlItem>();

            if (hourlist.Count > 0)
                timestr = getTiem(hourlist[0]) + "-" + getTiem(hourlist[hourlist.Count - 1] + 1);
            if (queryModel.FtimeStemp == null)
            {
                ObjectHelper.AutoMapping(modelBo, queryModel);
                var sch = base.LoadFirstOrDefault<M_Field>(p => p.Id == modelBo.FieldId);
                var plan = base.LoadFirstOrDefault<M_TeachingPlan>(p => p.Id == modelBo.TeachingPlanId);
                queryModel.CategoryId = plan.CategoryId;
                queryModel.AgeCategoryId = plan.AgeCategoryId;
                queryModel.Day = modelBo.List[0].Day;
                queryModel.SchoolId = sch.SchoolId;
                queryModel.Name = plan.Title;
                queryModel.Book = plan?.CategoryId + "," + plan?.AgeCategoryId;
                queryModel.StudentId = modelBo.UserId;
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateTime.Now;
                queryModel.TeacherCourseTime = timestr;
                queryModel.CreateUserId = modelBo.UserId;
                sqlItem.Add(mServiceDAL.InsertSqlItem<M_TeacherCourse>(queryModel));
                //amount = mServiceDAL.Create(queryModel);
            }

            foreach (var item in modelBo.List)
            {
                item.TeacherCourseId = queryModel.Id;
            }
            var bo = new SubscribeAddBO()
            {
                UserId = modelBo.UserId,
                List = modelBo.List
            };
            sqlItem.AddRange(SessionFactory.SessionService.mCourseBLL.AddSubscribeSqlItem(bo));

            var subCount = base.Count<M_Subscribe>(p => p.Day == modelBo.List[0].Day && hourlist.Contains(p.Hour) && p.FieldId == modelBo.List[0].FieldId && p.TeacherCourseId == null);
            if (subCount > 0)
                throw new schoolException(SubCode.Failure.GetHashCode(), Message.Instance.R("v_Nbdc"));
            try
            {
                //amount = 1;
                amount = base.ExecuteSqlTran(sqlItem.ToArray());
            }

            catch
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), Message.Instance.R("v_Nbdc") + "!");
            }

            return base.ReturnBo(amount);
        }


        public AmountBo EscTeacherCourse(string id, string userId)
        {
            var model = base.LoadFirstOrDefault<M_TeacherCourse>(p => p.Id == id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            if (model.Day < Int32.Parse(DateTime.Now.ToString("yyyyMMdd")))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), "不能取消以前或者当天课程！");
            }

            List<SqlItem> sqlItems = new List<SqlItem>();

            SqlParameter[] par =
                {
                new SqlParameter("@TeacherCourseId", id)
            };
            var sql = new SqlItem();

            sqlItems.Add(new SqlItem()
            {
                SqlValue = "delete M_Subscribe  where TeacherCourseId=@TeacherCourseId",
                Params = par.ToArray()
            });

            SqlParameter[] par2 =
               {
                new SqlParameter("@id", id),
                new SqlParameter("@UpdateTime",DateTime.Now),
                new SqlParameter("@UpdateUserId",userId)
            };
            sqlItems.Add(new SqlItem()
            {
                SqlValue = "update M_TeacherCourse set status=2,UpdateTime=@UpdateTime,UpdateUserId=@UpdateUserId where id=@id",
                Params = par2.ToArray()
            });

            sqlItems.AddRange(SessionFactory.SessionService.mStudentCourseBLL.GetEscTeacherCourse(id, 2));
            var amount = base.ExecuteSqlTran(sqlItems.ToArray());
            return base.ReturnBo(amount);
        }



        //public List<TeacherCourseVO> GetListTeacherCourse(TeacherListBO bo)
        //{
        //    int totNum = 0;
        //    var list = new List<TeacherCourseVO>();

        //    var whereQl = BuildExpression<M_TeacherCourse>();
        //    //BuildLambda<M_Teacher>(bo.CategoryId!=0, p => p.CategoryId == bo.CategoryId),
        //    //BuildLambda<M_Teacher>(bo.AgeCategoryId != 0, p => p.AgeCategoryId == bo.AgeCategoryId));
        //    IEnumerable<M_TeacherCourse> noumena = null;

        //    //var whereQl = BuildExpression<W_ServerMain>(
        //    //    BuildLambda<W_ServerMain>(bo.UserInfoId, p => p.UserInfoId == bo.UserInfoId),
        //    //    BuildLambda<W_ServerMain>(bo.Status != 0, p => p.Status == bo.Status));
        //    //IEnumerable<W_ServerMain> noumena = null;

        //    if (bo.PageSize == 0)
        //    {
        //        noumena = mServiceDAL.LoadEntities<M_TeacherCourse>(whereQl);
        //        totNum = noumena.Count();
        //    }
        //    else
        //    {
        //        noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
        //            out totNum, whereQl, false, p => p.Sort);
        //    }
        //    bo.TotNum = totNum;
        //    if (!noumena.Any()) return list;
        //    list = noumena.OrderByDescending(x => x.Sort).ThenByDescending(x => x.CreateTime).Select(
        //        p => new TeacherCourseVO
        //        {
        //            Id = p.Id,
        //            Sort = p.Sort,
        //            Book = p.Book,
        //            FieldId = p.FieldId,
        //            Main = p.Main,
        //            Name = p.Name,
        //            StudentId = p.StudentId
        //        }).ToList();
        //    return list;
        //}

        public TeacherCourseVO GetOneTeacherCourse(string Id)
        {
            var model = base.LoadFirstOrDefault<M_TeacherCourse>(p => p.Id == Id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            var queryModel = new TeacherCourseVO();
            ObjectHelper.AutoMapping(model, queryModel);
            return queryModel;
        }

        #region
        /// <summary>
        /// 课程表
        /// </summary>
        /// <param name="day"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public List<TeacherCourseShowVO> GetTeacherShowList(int day, string schoolId)
        {
            var list = new List<TeacherCourseShowVO>();
            var whereQl = BuildExpression<M_TeacherCourse>(
                BuildLambda<M_TeacherCourse>(day, p => p.Day == day),
                BuildLambda<M_TeacherCourse>(schoolId, p => p.SchoolId == schoolId),
                BuildLambda<M_TeacherCourse>(true, p => p.Deleted == 0),
                  BuildLambda<M_TeacherCourse>(true, p => p.Status != 2));
            var teacherCourseList = base.LoadEntities<M_TeacherCourse>(whereQl, true, q => new { q.M_Field });
            var planList = base.LoadEntities<M_TeachingPlan>(p => p.Id != null);
            var fieldList = base.LoadEntities<M_Field>(p => p.Id != null);
            var teacherlist = base.LoadEntities<M_Teacher>(p => p.Id != null);
            var schoollist = base.LoadEntities<M_School>(p => p.Id != null);
            foreach (var item in teacherCourseList)
            {
                var sch = schoollist.FirstOrDefault<M_School>(p => p.Id == item.SchoolId);
                var tea = teacherlist.FirstOrDefault<M_Teacher>(p => p.StudentId == item.StudentId);
                var vo = new TeacherCourseShowVO();
                ObjectHelper.AutoMapping(item, vo);
                vo.FieldName = fieldList.FirstOrDefault<M_Field>(p => p.Id == item.FieldId)?.Name;
                vo.TeacherCourseTime = item.TeacherCourseTime;
                vo.Day = item.Day;
                vo.TeacherName = tea?.Name;
                vo.TeacherTel = tea?.Tel;
                vo.SchoolName = sch?.Name;
                vo.Num = base.Count<M_StudentCourseClass>(p => p.TeacherCourseId == item.Id && p.Status != 2);
                list.Add(vo);
            };
            return list;
        }
        public List<TeacherCourseShowVO> GetTeacherShowListAll(string day, string schoolId)
        {
            var list = new List<TeacherCourseShowVO>();
            var whereQl = BuildExpression<M_TeacherCourse>(
                BuildLambda<M_TeacherCourse>(day, p => p.Day == Int32.Parse(day)),
                BuildLambda<M_TeacherCourse>(schoolId, p => p.SchoolId == schoolId),
                BuildLambda<M_TeacherCourse>(true, p => p.Deleted == 0),
                  BuildLambda<M_TeacherCourse>(true, p => p.Status != 2));
            var teacherCourseList = base.LoadEntities<M_TeacherCourse>(whereQl, true, q => new { q.M_Field });
            var planList = base.LoadEntities<M_TeachingPlan>(p => p.Id != null);
            var fieldList = base.LoadEntities<M_Field>(p => p.Id != null);
            var teacherlist = base.LoadEntities<M_Teacher>(p => p.Id != null);
            var schoollist = base.LoadEntities<M_School>(p => p.Id != null);
            foreach (var item in teacherCourseList)
            {
                var sch = schoollist.FirstOrDefault<M_School>(p => p.Id == item.SchoolId);
                var tea = teacherlist.FirstOrDefault<M_Teacher>(p => p.StudentId == item.StudentId);
                var vo = new TeacherCourseShowVO();
                ObjectHelper.AutoMapping(item, vo);
                vo.FieldName = fieldList.FirstOrDefault<M_Field>(p => p.Id == item.FieldId)?.Name;
                vo.TeacherCourseTime = item.TeacherCourseTime;
                vo.Day = item.Day;
                vo.TeacherName = tea?.Name;
                vo.TeacherTel = tea?.Tel;
                vo.SchoolName = sch?.Name;
                vo.Num = base.Count<M_StudentCourseClass>(p => p.TeacherCourseId == item.Id && p.Status != 2);
                list.Add(vo);
            };
            return list;
        }
        public List<string> GetTime(List<int> l)
        {
            var str = string.Empty;
            l.Sort();
            int i = 0;
            int e = 0;
            string s1 = "";
            string s2 = "";
            List<string> ss = new List<string>();
            foreach (var item in l)
            {
                if (i == 0)
                {
                    s1 = getTiem(item);
                    s2 = getTiem(item + 1);
                    e = item + 1;
                    i++;
                    continue;
                }
                i++;
                if (item == e && l.Count() > i)
                {
                    e = item + 1;
                    s2 = getTiem(item + 1);
                }
                else if (item != e)
                {
                    ss.Add(s1 + "-" + getTiem(e));
                    s1 = getTiem(item);
                    s2 = getTiem(item + 1);
                    e = item + 1;
                }
                if (l.Count() == i)
                {
                    ss.Add(s1 + "-" + getTiem(item + 1));
                }
            }
            return ss;
        }

        string getTiem(int item)
        {
            string str = "";
            switch (item)
            {
                case 10:
                    str = "8:00";
                    break;
                case 11:
                    str = "8:30";
                    break;
                case 12:
                    str = "9:00";
                    break;
                case 13:
                    str = "9:30";
                    break;
                case 14:
                    str = "10:00";
                    break;
                case 15:
                    str = "10:30";
                    break;
                case 16:
                    str = "11:00";
                    break;
                case 17:
                    str = "11:30";
                    break;
                case 18:
                    str = "12:00";
                    break;
                case 19:
                    str = "12:30";
                    break;

                case 20:
                    str = "13:00";
                    break;
                case 21:
                    str = "13:30";
                    break;


                case 22:
                    str = "14:00";
                    break;
                case 23:
                    str = "14:30";
                    break;


                case 24:
                    str = "15:00";
                    break;
                case 25:
                    str = "15:30";
                    break;

                case 26:
                    str = "16:00";
                    break;
                case 27:
                    str = "16:30";
                    break;

                case 28:
                    str = "17:00";
                    break;
                case 29:
                    str = "17:30";
                    break;

                case 30:
                    str = "18:00";
                    break;
                case 31:
                    str = "18:30";
                    break;

                case 32:
                    str = "19:00";
                    break;
                case 33:
                    str = "19:30";
                    break;

                case 34:
                    str = "20:00";
                    break;
                case 35:
                    str = "20:30";
                    break;

                case 36:
                    str = "21:00";
                    break;
                case 37:
                    str = "21:30";
                    break;
            }
            return str;
        }

        #region  课程表单
        public M_TeacherCourse GetTeacherCourseOne(string id)
        {
            var mo = base.LoadFirstOrDefault<M_TeacherCourse>(p => p.Id == id);
            return mo;
        }
        public List<M_TeacherCourse> GetTeacherCourseList()
        {
            var mo = base.LoadEntities<M_TeacherCourse>(p => p.Deleted == 0).OrderByDescending(p => p.CreateTime).ToList();
            return mo;
        }
        /// <summary>
        /// 获取单课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TeacherCourseShowVO GetTeacherShow(string id)
        {
            var mo = base.LoadFirstOrDefault<M_TeacherCourse>(p => p.Id == id);

            var fi = base.LoadFirstOrDefault<M_Field>(p => p.Id == mo.FieldId);

            var school = base.LoadFirstOrDefault<M_School>(p => p.Id == fi.SchoolId);
            var su = base.LoadEntities<M_Subscribe>(p => p.TeacherCourseId == mo.Id);
            var vo = new TeacherCourseShowVO();
            ObjectHelper.AutoMapping(mo, vo);
            vo.FieldName = fi?.Name;
            vo.SchoolAddress = school?.Address;
            vo.SchoolTel = school?.Tel;
            vo.SchoolName = school?.Name;
            vo.Day = mo.Day;
            vo.TeacherCourseTime = mo.TeacherCourseTime;
            vo.X = school?.X;
            vo.Y = school?.Y;
            vo.Status = mo.Status.ToString();
            vo.TeachingPlanId = mo.TeachingPlanId;
            vo.CreateUserId = mo.CreateUserId;
            return vo;
        }
        #endregion

        #endregion


        #region

        #endregion


        #endregion


        #region 添加字典
        public AmountBo CreateOrUpdateDic(DicBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Dic>(p => p.Id == modelBo.Id);
            ObjectHelper.AutoMapping(modelBo, queryModel);
            if (queryModel.FtimeStemp == null)
            {

                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateTime.Now;
                queryModel.CreateUserId = "modelBo.UserId";
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
        #endregion

        #region 获取字典项目

        public List<DicMainVO> GetDicList(int category, bool showAll = false)
        {
            List<DicMainVO> li = new List<DicMainVO>();
            var list = new List<M_Dic>();
            if (showAll)
            {
                list = base.LoadEntities<M_Dic>(p => p.Category == category).OrderByDescending(p => p.Sort).ThenByDescending(p => p.CreateTime).ToList();
            }
            else
            {
                list = base.LoadEntities<M_Dic>(p => p.Category == category && p.Deleted == 0).OrderByDescending(p => p.Sort).ThenByDescending(p => p.CreateTime).ToList();
            }
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.PaterId))
                {
                    DicMainVO v = new DicMainVO();
                    ObjectHelper.AutoMapping(item, v);
                    var dics = list.FindAll(p => p.PaterId == item.Id).Select(x => new DicItemVO
                    {
                        Category = x.Category,
                        Id = x.Id,
                        Name = x.Name,
                        PaterId = x.PaterId,
                        Sort = x.Sort,
                        VideoUrl = x.VideoUrl,
                        Points = x.Points,

                    }).OrderBy(p => p.Sort).ToList();
                    v.ItemList = dics;
                    li.Add(v);
                }
            }
            return li;
        }

        /// <summary>
        /// 获取单个字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DicItemVO GetDicOne(string id)
        {
            var model = base.LoadFirstOrDefault<M_Dic>(p => p.Id == id);
            var dic = new DicItemVO()
            {
                Id = model.Id,
                Name = model.Name,
                Category = model.Category,
                PaterId = model.PaterId,
                Sort = model.Sort,
                VideoUrl = model.VideoUrl,
                Points = model.Points,
                CreateTime = model.CreateTime,
                OneCategory = model.OneCategory,
                ThrCategory = model.ThrCategory,
                TwoCategory = model.TwoCategory
            };
            return dic;
        }
        public AmountBo DeleteDic(string id)
        {
            var model = base.LoadFirstOrDefault<M_Dic>(p => p.Id == id);
            model.Deleted = 1;
            if (string.IsNullOrEmpty(model.PaterId))//如果是主节点
            {
                var count = base.Count<M_Dic>(p => p.PaterId == model.Id && p.Deleted == 0);
                if (count > 0)
                    throw new schoolException(SubCode.Failure.GetHashCode(), "有子项不能进行删除！");
            }
            mServiceDAL.Update(model);
            return base.ReturnBo(1);
        }
        public List<M_Dic> GetDicAllList()
        {
            var list = new List<M_Dic>();
            list = base.LoadEntities<M_Dic>(p => p.Id != null).OrderByDescending(p => p.Sort).ThenByDescending(p => p.CreateTime).ToList();
            return list;
        }
        public List<M_Dic> GetDicParentList(string parentId = "")
        {
            var list = new List<M_Dic>();
            if (string.IsNullOrEmpty(parentId))
            {
                list = base.LoadEntities<M_Dic>(p => p.PaterId == null && p.Deleted == 0).OrderByDescending(p => p.Sort).ThenByDescending(p => p.CreateTime).ToList();
                return list;
            }
            list = base.LoadEntities<M_Dic>(p => p.PaterId == parentId && p.Deleted == 0).OrderByDescending(p => p.Sort).ThenByDescending(p => p.CreateTime).ToList();
            return list;
        }


        public List<M_Dic> GetListByCategorId(DicCategoryBO bo)
        {
            int totNum = 0;
            var list = new List<M_Dic>();

            var whereQl = BuildExpression<M_Dic>(
            BuildLambda<M_Dic>(bo.OneCategory, p => p.OneCategory.Contains(bo.OneCategory)),
            BuildLambda<M_Dic>(bo.TwoCategory, p => p.TwoCategory.Contains(bo.TwoCategory)),
            BuildLambda<M_Dic>(bo.ThrCategory, p => p.ThrCategory.Contains(bo.ThrCategory)),
            BuildLambda<M_Dic>(true, p => p.Deleted == 0)
            );
            IEnumerable<M_Dic> noumena = null;

            noumena = mServiceDAL.LoadEntities<M_Dic>(whereQl).OrderBy(p => p.OneCategory).ThenBy(p => p.TwoCategory).ThenBy(p => p.ThrCategory).ThenBy(p => p.CreateTime);
            totNum = noumena.Count();
            if (!noumena.Any()) return list;
            return noumena.ToList();
        }

        #endregion

        #region 教案
        /// <summary>
        /// 删除教案
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo DeleteTeachingPlan(string id)
        {
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_TeachingPlan>(p => p.Id == id);
            if (queryModel.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            queryModel.UpdateTime = DateTime.Now;
            //queryModel.UpdateUserId = modelBo.UserId;
            queryModel.Deleted = 1;
            mServiceDAL.Update(queryModel);

            return base.ReturnBo(1);
        }


        /// <summary>
        /// 课程创建
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo CreateOrUpdateTeachingPlan(TeachingPlanBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_TeachingPlan>(p => p.Id == modelBo.Id);

            ObjectHelper.AutoMapping(modelBo, queryModel);
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateTime.Now;
                queryModel.CreateUserId = modelBo.UserId;
                //sqlItem.Add(base.InsertSqlItem<M_TeachingPlan>(queryModel));
                amount = mServiceDAL.Create(queryModel);
            }
            else
            {
                queryModel.UpdateTime = DateTime.Now;
                queryModel.UpdateUserId = modelBo.UserId;
                amount = mServiceDAL.Update(queryModel);
            }
            List<SqlItem> sqlItem = new List<SqlItem>();
            SqlParameter[] par =
          {
                new SqlParameter("@TeachingPlanId", modelBo.Id)
            };
            var sql = new SqlItem();

            sql = new SqlItem()
            {
                SqlValue = "delete M_TeachingPlanInfo  where TeachingPlanId=@TeachingPlanId",
                Params = par.ToArray()
            };
            sqlItem.Add(sql);
            //return base.ExecuteSqlTran(sql) > 0; 
            foreach (var item in modelBo.List)
            {
                item.UserId = modelBo.UserId;
                item.TeachingPlanId = queryModel.Id;
                sqlItem.Add(AddTeachingItem(item));
            }
            base.ExecuteSqlTran(sqlItem.ToArray());
            return base.ReturnBo(amount);
        }

        SqlItem AddTeachingItem(TeachingPlanInfoBO modelBo)
        {
            var model = new M_TeachingPlanInfo();
            ObjectHelper.AutoMapping(modelBo, model);
            model.CreateTime = DateTime.Now;
            model.CreateUserId = modelBo.UserId;

            model.Id = Utils.GetGuid();
            return base.InsertSqlItem<M_TeachingPlanInfo>(model);
        }


        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TeachingPlanVO GetTeachingPlan(string id)
        {
            var model = base.LoadFirstOrDefault<M_TeachingPlan>(p => p.Id == id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            var queryModel = new TeachingPlanVO();
            ObjectHelper.AutoMapping(model, queryModel);
            var video = base.LoadFirstOrDefault<M_Video>(p => p.Id == model.VideoId);
            var vide2 = base.LoadFirstOrDefault<M_Video>(p => p.Id == model.NextVideoId);
            var Jobvide = base.LoadFirstOrDefault<M_Video>(p => p.Id == model.JobVideoId);
            queryModel.NextVideoName = vide2.Title;
            queryModel.VideoName = video?.Title;
            queryModel.VideoUrl = video?.Url;
            queryModel.NextVideoUrl = vide2?.Url;
            queryModel.NextVideoName = vide2?.Title;
            queryModel.Pic = video?.Pic;
            queryModel.NextPic = vide2?.Pic;
            queryModel.JobVideoName = Jobvide?.Title;
            queryModel.JobVideoUrl = Jobvide?.Url;
            return queryModel;
        }

        /// <summary>
        /// 获取教案列表
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public List<TeachingPlanVO> GetTeachingPlanList(IdListBO bo)
        {
            int totNum = 0;
            var list = new List<TeachingPlanVO>();
            

            var whereQl = BuildExpression<M_TeachingPlan>(
                            BuildLambda<M_TeachingPlan>(bo.Key, p => p.Title.Contains(bo.Key)),
                              BuildLambda<M_TeachingPlan>(true, p => p.Deleted == 0));

            IEnumerable<M_TeachingPlan> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_TeachingPlan>(whereQl);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            var vilist = base.LoadEntities<M_Video>(p => p.Id != null);
            list = noumena.OrderByDescending(x => x.UpdateTime).ThenByDescending(x => x.CreateTime).Select(
                p => new TeachingPlanVO
                {
                    Id = p.Id,
                    Sort = p.Sort,
                    Pic = p.Pic,
                    Title = p.Title,
                    AgeCategoryId = p.AgeCategoryId,
                    Book = p.Book,
                    CategoryId = p.CategoryId,
                    Equip = p.Equip,
                    NextTitle = p.NextTitle,
                    Target = p.Target,
                    VideoId = p.VideoId,
                    NextVideoId = p.NextVideoId,
                    Main = p.Main,
                    JobPoints = p.JobPoints,
                    NextPoints = p.NextPoints,
                    JobVideoId = p.JobVideoId,
                    JobVideoUrl = vilist.FirstOrDefault<M_Video>(x => x.Id == p.JobVideoId)?.Url,
                    JobVideoName = vilist.FirstOrDefault<M_Video>(x => x.Id == p.JobVideoId)?.Title,
                    VideoName = vilist.FirstOrDefault<M_Video>(x => x.Id == p.VideoId)?.Title,
                    NextVideoName = vilist.FirstOrDefault<M_Video>(x => x.Id == p.NextVideoId)?.Title
                }).ToList();
            return list;
        }




        #region 子教案

        /// <summary>
        /// 课程创建
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo CreateOrUpdateTeachingPlanInfo(TeachingPlanInfoBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_TeachingPlanInfo>(p => p.Id == modelBo.Id);
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




        /// <summary>
        /// 获取教案列表
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public List<TeachingPlanInfoVO> GetTeachingPlanInfoList(IdListBO bo)
        {
            int totNum = 0;
            var list = new List<TeachingPlanInfoVO>();

            var whereQl = BuildExpression<M_TeachingPlanInfo>(p => p.TeachingPlanId == bo.Id && p.Deleted == 0);
            IEnumerable<M_TeachingPlanInfo> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_TeachingPlanInfo>(whereQl);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.Sort);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            var vlist = base.LoadEntities<M_Video>(x => x.Id != null);
            list = noumena.OrderByDescending(x => x.Sort).ThenByDescending(x => x.CreateTime).Select(
                p => new TeachingPlanInfoVO
                {
                    Id = p.Id,
                    Sort = p.Sort,
                    Org = p.Org,
                    TeachingPlanId = p.TeachingPlanId,
                    TimeLength = p.TimeLength,
                    TimeSlot = p.TimeSlot,
                    CategoryId = p.CategoryId,
                    CategoryItemId = p.CategoryItemId,
                    CategoryItemName = p.CategoryItemName,
                    CategoryName = p.CategoryName,
                    VideoName = vlist.FirstOrDefault<M_Video>(x => x.Id == p.VideoId)?.Title,
                    Points = p.Points,
                    //Pic = p.Pic,
                    //Title = p.Title,
                    //AgeCategoryId = p.AgeCategoryId,
                    //Book = p.Book,
                    //CategoryId = p.CategoryId,
                    //Equip = p.Equip,
                    //NextTitle = p.NextTitle,
                    //Target = p.Target,
                    VideoId = p.VideoId,
                    //NextVideoId = p.NextVideoId,
                    //Main = p.Main,
                }).ToList();
            return list;
        }


        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TeachingPlanInfoVO GetTeachingPlanInfo(string id)
        {
            var model = base.LoadFirstOrDefault<M_TeachingPlanInfo>(p => p.Id == id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            var queryModel = new TeachingPlanInfoVO();
            ObjectHelper.AutoMapping(model, queryModel);
            return queryModel;
        }
        #endregion



        #region 视频
        /// <summary>
        /// 课程创建
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo CreateOrUpdateVideo(VideoBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Video>(p => p.Id == modelBo.Id);
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

        public AmountBo DeleteVideo(string id, string userId)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Video>(p => p.Id == id);
            {
                queryModel.UpdateTime = DateTime.Now;
                queryModel.UpdateUserId = userId;
                queryModel.Deleted = 1;
                amount = mServiceDAL.Update(queryModel);
                return base.ReturnBo(amount);
            }
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VideoVO GetVideo(string id)
        {
            var model = base.LoadFirstOrDefault<M_Video>(p => p.Id == id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            var queryModel = new VideoVO();
            ObjectHelper.AutoMapping(model, queryModel);
            return queryModel;
        }

        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public List<VideoVO> GetVideoList(IdListBO bo)
        {
            int totNum = 0;
            var list = new List<VideoVO>();
            var whereQl = BuildExpression<M_Video>();
            if (string.IsNullOrEmpty(bo.Key))
            {
                whereQl = BuildExpression<M_Video>(p => p.Id != null && p.Deleted == 0);
            }
            else
            {
                whereQl = BuildExpression<M_Video>(p => p.Title.Contains(bo.Key) && p.Deleted == 0);
            }
            IEnumerable<M_Video> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_Video>(whereQl);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.Sort);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            list = noumena.OrderByDescending(x => x.Sort).ThenByDescending(x => x.CreateTime).Select(
                p => new VideoVO
                {
                    Id = p.Id,
                    Sort = p.Sort,
                    Pic = p.Pic,
                    Title = p.Title,
                    Url = p.Url,
                    Points = p.Points,
                    Main = p.Main
                }).ToList();
            return list;
        }

        #endregion

        #endregion

        #region 学员管理

        #region 学员列表
        /// <summary>
        /// 获取老师下学生
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public List<TeacherStudentVO> GetStudentCourseListByTeacher(IdListBO bo)
        {
            List<TeacherStudentVO> list = new List<TeacherStudentVO>();
            var whereQl = BuildExpression<M_StudentCourseClass>(
             BuildLambda<M_StudentCourseClass>(bo.Key, p => p.StudentName.Contains(bo.Key)),
                 p => p.TeacherId == bo.Id);
            var volist = base.LoadEntities<M_StudentCourseClass>(whereQl, true, x => new { x.M_Student });
            var stulist = base.LoadEntities<M_Student>(p => p.Id != null); 
            foreach (var item in volist)
            {
                if (list.Count(p => p.Id == item.StudentId) > 0)
                    continue;
                var stu = stulist.FirstOrDefault<M_Student>(p => p.Id == item.StudentId); 
                if (string.IsNullOrEmpty(item.StudentName))
                    item.StudentName = stu?.NickName;
                list.Add(new TeacherStudentVO()
                {
                    StudentName = item.StudentName,
                    Id = item.StudentId,
                    Tel = item.M_Student.Tel,
                    HeadImgUrl = stu?.HeadImgUrl
                });
            }
            return list;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取学员作业列表
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public List<StudentCourseClassVO> GetListStudentCourseClassByTeacher(StudentCourseClassQueryBO bo)
        {
            int totNum = 0;
            var list = new List<StudentCourseClassVO>();
            IEnumerable<M_StudentCourseClass> noumena = null;
            if (bo.Flg == 0)
            {
                var whereQl = BuildExpression<M_StudentCourseClass>(
                                    BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                                    BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.TeacherId == bo.StudentId));

                if (bo.PageSize == 0)
                {
                    noumena = mServiceDAL.LoadEntities<M_StudentCourseClass>(whereQl, true, x => new { x.M_Student, x.M_TeacherCourse }).OrderByDescending(p => p.CreateTime);
                    totNum = noumena.Count();
                }
                else
                {
                    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                        out totNum, whereQl, false, p => p.CreateTime, true, x => new { x.M_Student, x.M_TeacherCourse });
                }
            }
            else
            {
                var whereQl = BuildExpression<M_StudentCourseClass>(
                     BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                     BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.TeacherId == bo.StudentId));
                if (!string.IsNullOrEmpty(bo.Flg.ToString()) || bo.Flg != 0)
                {
                    whereQl = BuildExpression<M_StudentCourseClass>(
                     BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                     BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.StudentId == bo.StudentId),
                     BuildLambda<M_StudentCourseClass>(bo.Flg, p => p.JobStatus == bo.Flg.ToString()));
                }
                if (bo.PageSize == 0)
                {
                    noumena = mServiceDAL.LoadEntities<M_StudentCourseClass>(whereQl, true, x => new { x.M_Student, x.M_TeacherCourse }).OrderByDescending(p => p.CreateTime);
                    totNum = noumena.Count();
                }
                else
                {
                    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                        out totNum, whereQl, false, p => p.CreateTime, true, x => new { x.M_Student, x.M_TeacherCourse });
                }
            }
            bo.TotNum = totNum;

            if (!noumena.Any()) return list;
            var teach = base.LoadEntities<M_Student>(p => p.Id != null);
            var tcl = base.LoadEntities<M_TeacherCourse>(p => p.Id != null);
            var schList = base.LoadEntities<M_School>(p => p.Id != null);
            var fiList = base.LoadEntities<M_Field>(p => p.Id != null);
            foreach (var p in noumena)
            {
                var tch = teach.FirstOrDefault<M_Student>(x => x.Id == p.StudentId);
                var tc = tcl.FirstOrDefault<M_TeacherCourse>(x => x.Id == p.TeacherCourseId);
                //var sub = base.LoadEntities<M_Subscribe>(x => x.TeacherCourseId == p.TeacherCourseId);
                var fi = fiList.FirstOrDefault<M_Field>(x => x.Id == p.FieldId);
                var sc = schList.FirstOrDefault<M_School>(x => x.Id == fi.SchoolId);
                Dictionary<string, List<int>> dic = new Dictionary<string, List<int>>();//订购时间
                                                                                        //List<int> l = new List<int>();
                                                                                        //foreach (var item in sub.ToList())
                                                                                        //{
                                                                                        //    l.Add(item.Hour);
                                                                                        //}
                                                                                        //dic.Add(p.TeacherCourseId, l);


                var vo = new StudentCourseClassVO
                {
                    Id = p.Id,
                    CreateTime = p.CreateTime,
                    TeacherId = p.TeacherId,
                    Status = p.Status,
                    StudentName = p.M_Student?.FullName,
                    StudentId = p.StudentId,
                    HeadImgUrl = p.M_Student?.HeadImgUrl,
                    NickName = p.M_Student?.NickName,
                    JobStatus = p.JobStatus,
                    TeacherCourseId = p.TeacherCourseId,
                    JobEvaluate = p.JobEvaluate,
                    JobName = p.JobName,
                    StudentJobUrl = p.StudentJobUrl,
                    TeacherCourseTime = tc?.TeacherCourseTime,
                    TeacherName = tc?.Name,
                    JobTempVideoId = p.JobTempVideoId,
                    JobTempVideoUrl = p.JobTempVideoUrl,
                    StudentEvaluate = p.StudentEvaluate,
                    StudentStars = p.StudentStars,
                    StudentTel = p.M_Student?.Tel,
                    TeacherEvaluate = p.TeacherEvaluate,
                    JobPoints = p.JobPoints,
                    CourseVideoUrl = p.CourseVideoUrl,
                    TrainNum = p.TrainNum,
                    TrainTime = p.TrainTime,
                    FieldName = fi?.Name,
                    SchoolName = sc?.Name,
                    TeacherCourseName = tc?.Name,
                    Day = p.Day,
                };
                list.Add(vo);
            }
            return list;
        }
        #endregion

        #endregion

        #region ///活动相关
        #region 活动
        public AmountBo CreateOrUpdateActivity(ActivityBO modelBo)
        {
            List<SqlItem> sqlItem = new List<SqlItem>();
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_Activity>(p => p.Id == modelBo.Id);
            ObjectHelper.AutoMapping(modelBo, queryModel, new[] { "FtimeStemp" });

            var field = base.LoadFirstOrDefault<M_Field>(p => p.Id == modelBo.FieldId);
            var teac = base.LoadFirstOrDefault<M_Teacher>(p => p.Id == modelBo.StudentId);
            var school = base.LoadFirstOrDefault<M_School>(p => p.Id == field.SchoolId);
            queryModel.ShcoolName = school?.Name;
            queryModel.FieldName = field?.Name;
            queryModel.TeacherTel = teac?.Tel;
            queryModel.TeacherName = teac?.Name;
            if (queryModel.FtimeStemp == null)
            {
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateTime.Now;
                queryModel.CreateUserId = modelBo.UserId;

                var hourlist = modelBo.List;
                hourlist.Sort();


                SubscribeBO d = new SubscribeBO
                {
                    Day = modelBo.Day,
                    FieldId = modelBo.FieldId,
                    Status = 0,
                    StudentId = modelBo.StudentId,
                    ActivityId = queryModel.Id,
                    UserId = modelBo.UserId,
                    HourList = modelBo.List
                };


                var bo = new SubscribeAddBO()
                {
                    UserId = modelBo.UserId,
                    List = new List<SubscribeBO>()
                };
                bo.List.Add(d);
                sqlItem.AddRange(SessionFactory.SessionService.mCourseBLL.AddSubscribeSqlItem(bo));

                var subCount = base.Count<M_Subscribe>(p => p.Day == modelBo.Day && hourlist.Contains(p.Hour) && p.FieldId == modelBo.FieldId);
                if (subCount > 0)
                    throw new schoolException(SubCode.Failure.GetHashCode(), Message.Instance.R("v_Nbdc"));
                try
                {
                    sqlItem.Add(base.InsertSqlItem<M_Activity>(queryModel));
                    amount = base.ExecuteSqlTran(sqlItem.ToArray());
                }

                catch
                {
                    throw new schoolException(SubCode.Failure.GetHashCode(), Message.Instance.R("v_Nbdc") + "!");
                }
                //sqlItem.Add(mServiceDAL.InsertSqlItem<M_Activity>(queryModel));
                //amount = base.ExecuteSqlTran(sqlItem.ToArray());
            }
            else
            {
                queryModel.UpdateTime = DateTime.Now;
                queryModel.UpdateUserId = modelBo.UserId;

                SqlParameter[] par =
               {
                new SqlParameter("@ActivityId", modelBo.Id)
               };
                var sql = new SqlItem();

                sqlItem.Add(new SqlItem()
                {
                    SqlValue = "delete M_Subscribe  where ActivityId=@ActivityId",
                    Params = par.ToArray()
                });
                var hourlist = modelBo.List;
                hourlist.Sort();


                SubscribeBO d = new SubscribeBO
                {
                    Day = modelBo.Day,
                    FieldId = modelBo.FieldId,
                    Status = 0,
                    StudentId = modelBo.StudentId,
                    ActivityId = queryModel.Id,
                    UserId = modelBo.UserId,
                    HourList = modelBo.List
                };

                var bo = new SubscribeAddBO()
                {
                    UserId = modelBo.UserId,
                    List = new List<SubscribeBO>()
                };
                bo.List.Add(d);
                sqlItem.AddRange(SessionFactory.SessionService.mCourseBLL.AddSubscribeSqlItem(bo));

                var subCount = base.Count<M_Subscribe>(p => p.Day == modelBo.Day && hourlist.Contains(p.Hour) && p.FieldId == modelBo.FieldId && p.ActivityId != queryModel.Id);
                if (subCount > 0)
                    throw new schoolException(SubCode.Failure.GetHashCode(), Message.Instance.R("v_Nbdc"));
                try
                {
                    amount = base.ExecuteSqlTran(sqlItem.ToArray());
                }

                catch
                {
                    throw new schoolException(SubCode.Failure.GetHashCode(), Message.Instance.R("v_Nbdc") + "!");
                }
                sqlItem.Add(mServiceDAL.InsertSqlItem<M_Activity>(queryModel));
                amount = base.ExecuteSqlTran(sqlItem.ToArray());
                amount = mServiceDAL.Update(queryModel);
            }
            return base.ReturnBo(amount);
        }


        public AmountBo EscActivity(string id, string userId)
        {
            var model = base.LoadFirstOrDefault<M_Activity>(p => p.Id == id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            if (model.Day < Int32.Parse(DateTime.Now.ToString("yyyyMMdd")))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), "不能取消以前活动！");
            }

            List<SqlItem> sqlItems = new List<SqlItem>();

            SqlParameter[] par =
                {
                new SqlParameter("@ActivityId", id)
            };
            var sql = new SqlItem();

            sqlItems.Add(new SqlItem()
            {
                SqlValue = "delete M_Subscribe  where ActivityId=@ActivityId",
                Params = par.ToArray()
            });

            SqlParameter[] par2 =
               {
                new SqlParameter("@id", id),
                new SqlParameter("@UpdateTime",DateTime.Now),
                new SqlParameter("@UpdateUserId",userId)
            };
            sqlItems.Add(new SqlItem()
            {
                SqlValue = "update M_Activity set status=2,UpdateTime=@UpdateTime,UpdateUserId=@UpdateUserId where id=@id",
                Params = par2.ToArray()
            });

            SqlParameter[] par3 =
             {
                new SqlParameter("@ActivityId", id),
                new SqlParameter("@UpdateTime",DateTime.Now),
                new SqlParameter("@UpdateUserId",userId)
            };
            sqlItems.Add(new SqlItem()
            {
                SqlValue = "update M_ActivityStudent set status=3,UpdateTime=@UpdateTime,UpdateUserId=@UpdateUserId where ActivityId=@ActivityId",
                Params = par3.ToArray()
            });
            var amount = base.ExecuteSqlTran(sqlItems.ToArray());
            return base.ReturnBo(amount);
        }

        public ActivityAllVO GetOneActivityVO(string id, bool isTeacher = false)
        {
            ActivityAllVO vo = new ActivityAllVO();
            var activo = base.LoadFirstOrDefault<M_Activity>(p => p.Id == id);

            var encount = 0;
            if (isTeacher)
                encount = base.Count<M_ActivityStudent>(p => p.ActivityId == activo.Id && (p.Status != 0));
            else
                encount = base.Count<M_ActivityStudent>(p => p.ActivityId == activo.Id && (p.Status == 1 || p.Status == 2));
            ObjectHelper.AutoMapping(activo, vo);
            vo.EnrollNum = encount;
            vo.SurplusNum = vo.Num - vo.EnrollNum;
            vo.List = new List<ActivityStudentVO>();
            if (encount > 0)
            {
                if (!isTeacher)
                    vo.List = GetActivityStudentList(new IdStatusListBO { Id = id, Status = "1,2" });
                else
                    vo.List = GetActivityStudentList(new IdStatusListBO { Id = id });
                if (vo.Day < Int32.Parse(DateTime.Now.ToString("yyyyMMdd")) && vo.Status == 1)
                {
                    vo.Status = 3;
                }
                if (vo.Status == 3)
                {
                    foreach (var item in vo.List)
                    {
                        if (item.Status == 1)
                            item.Status = 5;
                    }
                }
            }
            var sch = base.LoadFirstOrDefault<M_School>(p => p.Id == vo.SchooId);
            vo.Address = sch.Address;
            vo.X = sch.X;
            vo.Y = sch.Y;
            return vo;
        }

        public AmountBo UpdateActivity(string id, string userId, string status)
        {
            var bo = base.LoadFirstOrDefault<M_Activity>(p => p.Id == id);
            bo.Status = Int32.Parse(status);
            bo.UpdateTime = DateTime.Now;
            bo.UpdateUserId = userId;
            return base.ReturnBo(mServiceDAL.Update(bo));

        }
        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public List<ActivityVO> GetActivityList(IdActiStatusListBO bo)
        {
            int totNum = 0;
            var list = new List<ActivityVO>();
            var whereQl = BuildExpression<M_Activity>(
                BuildLambda<M_Activity>(bo.Id, p => p.StudentId == bo.Id),
                BuildLambda<M_Activity>(bo.Day, p => p.Day == Int32.Parse(bo.Day)),
                BuildLambda<M_Activity>(bo.Status, p => p.Status == Int32.Parse(bo.Status)));
            if (!string.IsNullOrEmpty(bo.StatusInfo))
            {
                if (bo.StatusInfo == "1")// 报名中活动
                {
                    whereQl = BuildExpression<M_Activity>(
                    BuildLambda<M_Activity>(bo.Id, p => p.StudentId == bo.Id),
                    BuildLambda<M_Activity>(true, p => p.EnrollEndTime >= DateTime.Now && p.EnrollBegTime <= DateTime.Now),
                    BuildLambda<M_Activity>(bo.StatusInfo, p => p.Status == 1));
                }
                if (bo.StatusInfo == "2")// 报名中活动或者未开始
                {
                    whereQl = BuildExpression<M_Activity>(
                    BuildLambda<M_Activity>(bo.Id, p => p.StudentId == bo.Id),
                    BuildLambda<M_Activity>(true, p => p.EnrollEndTime >= DateTime.Now),
                    BuildLambda<M_Activity>(bo.StatusInfo, p => p.Status == 1));
                }
                else if (bo.StatusInfo == "3") //报名结束活动
                {
                    whereQl = BuildExpression<M_Activity>(
                    BuildLambda<M_Activity>(bo.Id, p => p.StudentId == bo.Id),
                    BuildLambda<M_Activity>(true, p => p.EnrollEndTime < DateTime.Now));
                }
            }
            IEnumerable<M_Activity> noumena = null;

            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_Activity>(whereQl).OrderBy(p => p.EnrollBegTime).ThenBy(p => p.EnrollEndTime);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, true, p => p.EnrollBegTime);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            var keys = getbaomingNum();
            foreach (var item in noumena.ToList())
            {

                ActivityVO v = new ActivityVO();
                ObjectHelper.AutoMapping(item, v);
                if (v.Day < Int32.Parse(DateTime.Now.ToString("yyyyMMdd")) && item.Status == 1)
                {
                    v.Status = 3;//已完成
                }
                var ks = keys.FirstOrDefault<IdNum>(p => p.Id == item.Id);
                if (ks == null)
                {
                    v.EnrollNum = 0;
                    v.SurplusNum = item.Num;
                }
                else
                {
                    v.EnrollNum = ks.Num;
                    v.SurplusNum = item.Num - ks.Num;
                }
                list.Add(v);
            }
            return list;
        }
        #endregion

        #region 活动下学生
        public string CreateOrUpdateActivityStudent(ActivityStudentBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_ActivityStudent>(p => p.Id == modelBo.Id && p.Status != 2);
            ObjectHelper.AutoMapping(modelBo, queryModel);
            if (queryModel.FtimeStemp != null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "不能重复报名！");
            var acmode = base.LoadFirstOrDefault<M_Activity>(p => p.Id == modelBo.ActivityId);
            if (acmode.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "该活动已经取消或者删除不能报名");
            if (DateTime.Now < acmode.EnrollBegTime && DateTime.Now > acmode.EnrollEndTime)
                throw new schoolException(SubCode.Failure.GetHashCode(), "不在报名状态");
            var encount = base.Count<M_ActivityStudent>(p => p.ActivityId == acmode.Id && (p.Status == 1 || p.Status == 2));
            if (encount >= acmode.Num)
                throw new schoolException(SubCode.Failure.GetHashCode(), "不能报名报名人数已经满了");
            queryModel.Id = Utils.GetGuid();
            queryModel.Status = modelBo.Status;
            queryModel.CreateTime = DateParse.GetDateTime();
            queryModel.CreateUserId = modelBo.StudentId;
            queryModel.HeadImgUrl = base.LoadFirstOrDefault<M_Student>(p => p.Id == modelBo.StudentId)?.HeadImgUrl;
            amount = mServiceDAL.Create(queryModel);
            return queryModel.Id;
        }

        public AmountBo UpdateActivityStudentStatus(string id, int status, string userId)
        {
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_ActivityStudent>(p => p.Id == id);
            queryModel.Status = status;
            queryModel.UpdateTime = DateParse.GetDateTime();
            queryModel.UpdateUserId = userId;
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        public M_ActivityStudent getOneOnlyActStu(string id)
        {
            return mServiceDAL.LoadFirstOrDefault<M_ActivityStudent>(p => p.Id == id);
        }
        /// <summary>
        ///  状态0未付款，1报名成功，2待取消,3取消成功 不传递全部
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public List<ActivityStudentVO> GetActivityStudentList(IdStatusListBO bo)
        {

            int totNum = 0;
            var list = new List<ActivityStudentVO>();
            var whereQl = BuildExpression<M_ActivityStudent>(
                    BuildLambda<M_ActivityStudent>(bo.Id, p => p.ActivityId == bo.Id),
                    BuildLambda<M_ActivityStudent>(bo.Status, p => p.Status != 0));
            if (!string.IsNullOrEmpty(bo.Status))
            {
                List<string> stlist = bo.Status.Split(',').ToList();
                whereQl = BuildExpression<M_ActivityStudent>(
                  BuildLambda<M_ActivityStudent>(bo.Id, p => p.ActivityId == bo.Id),
                  BuildLambda<M_ActivityStudent>(bo.Status, p => stlist.Contains(p.Status.ToString())));
            }
            if (string.IsNullOrEmpty(bo.Status))
            {
                whereQl = BuildExpression<M_ActivityStudent>(
                BuildLambda<M_ActivityStudent>(bo.Id, p => p.ActivityId == bo.Id),
                BuildLambda<M_ActivityStudent>(true, p => p.Status != 0));
            }
            IEnumerable<M_ActivityStudent> noumena = null;

            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_ActivityStudent>(whereQl).OrderBy(p => p.CreateTime);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime).OrderBy(p => p.CreateTime);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            foreach (var item in noumena.ToList())
            {
                ActivityStudentVO v = new ActivityStudentVO();
                ObjectHelper.AutoMapping(item, v);
                v.HeadImgUrl = item.HeadImgUrl;
                list.Add(v);
            }
            return list;
        }

        public ActivityStudentVO GetActivityStudentOne(string id)
        {
            return new ActivityStudentVO();
        }

        #region 学生获取报名课程
        public List<StudentActivityStudentVO> GetActivityStudentListByStudent(IdStudentStatusListBO bo)
        {
            int totNum = 0;
            var list = new List<StudentActivityStudentVO>();
            var whereQl = BuildExpression<M_ActivityStudent>(
                BuildLambda<M_ActivityStudent>(bo.StudnetId, p => p.StudentId == bo.StudnetId),
                BuildLambda<M_ActivityStudent>(bo.Status, p => p.Status == Int32.Parse(bo.Status)));
            if (string.IsNullOrEmpty(bo.Status))
            {
                whereQl = BuildExpression<M_ActivityStudent>(
               BuildLambda<M_ActivityStudent>(bo.StudnetId, p => p.StudentId == bo.StudnetId),
               BuildLambda<M_ActivityStudent>(true, p => p.Status != 0));
            }
            IEnumerable<M_ActivityStudent> noumena = null;

            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_ActivityStudent>(whereQl).OrderByDescending(p => p.CreateTime);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime).OrderBy(p => p.CreateTime);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            var aclist = base.LoadEntities<M_Activity>(o => o.Id != null).ToList();

            var keys = getbaomingNum();
            foreach (var item in noumena.ToList())
            {
                StudentActivityStudentVO v = new StudentActivityStudentVO();
                var ac = aclist.FirstOrDefault<M_Activity>(x => x.Id == item.ActivityId);
                ActivityStudentVO vo = new ActivityStudentVO();
                ObjectHelper.AutoMapping(item, vo);
                var ks = keys.FirstOrDefault<IdNum>(p => p.Id == ac.Id);
                v.ActivityModel = ac;
                if (ac.Day < Int32.Parse(DateTime.Now.ToString("yyyyMMdd")) && ac.Status == 1)
                {
                    ac.Status = 3;
                    if (vo.Status == 1)
                        vo.Status = 5;
                }
                v.Model = vo;

                if (ks == null)
                {
                    v.EnrollNum = 0;
                    v.SurplusNum = ac.Num;
                }
                else
                {
                    v.EnrollNum = ks.Num;
                    v.SurplusNum = ac.Num - ks.Num;
                }
                list.Add(v);
            }
            return list;
        }
        #endregion

        #region 老师获取报名下学生
        public TeackerActivityStudentVO GetActivityStudentByTeacher(string activityId)
        {
            TeackerActivityStudentVO vo = new TeackerActivityStudentVO();
            var model = base.LoadFirstOrDefault<M_Activity>(p => p.Id == activityId);
            var mo = new ActivityVO();
            ObjectHelper.AutoMapping(model, mo);
            vo.Model = mo;
            if (mo.Day < Int32.Parse(DateTime.Now.ToString("yyyyMMdd")) && mo.Status == 1)
            {
                mo.Status = 3;
            }
            var list = GetActivityStudentList(new IdStatusListBO { Id = model.Id });
            foreach (var item in list)
            {
                if (mo.Status == 3)
                {
                    if (item.Status == 1)
                        item.Status = 5;
                }
            }
            vo.List = list;
            return vo;
        }
        #endregion

        #endregion

        List<IdNum> getbaomingNum()
        {
            var sqls = @"select count(*) as num,ActivityId AS id from  [M_ActivityStudent] where statUs=1 or statUs=2 group by ActivityId";
            SqlItem sql = new SqlItem
            {
                SqlValue = sqls,
                Params = null
            };
            var d = base.ExecuteDataTable(sql);
            return base.DataTableToEntities<IdNum>(d);
        }


        #region 获取订购时间list
        public SubListReVO GetSubListById(string id, int category)
        {
            SubListReVO vo = new SubListReVO();
            var list = new List<int>();
            var sublist = new List<M_Subscribe>();
            if (category == 0)
            {
                sublist = base.LoadEntities<M_Subscribe>(p => p.TeacherCourseId == id);
            }
            else if (category == 1)
            {
                sublist = base.LoadEntities<M_Subscribe>(p => p.OrderId == id);
            }
            else if (category == 2)
            {
                sublist = base.LoadEntities<M_Subscribe>(p => p.ActivityId == id);
            }
            int day = 0;
            foreach (var item in sublist)
            {
                day = item.Day;
                list.Add(item.Hour);
            }
            vo.List = list;
            vo.Day = day;
            return vo;
        }

        public List<TeacherCourseShowVO> GetTeacherShowListAll()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

    }
}

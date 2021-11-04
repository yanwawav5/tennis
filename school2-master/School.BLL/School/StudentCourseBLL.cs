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
using school.Model.VO;
using school.BLL.Factory;
using school.Model.TO.Response;
using System.Data.SqlClient;
using school.Model.TO.Request;
using System.Threading.Tasks;
using school.Model;

namespace school.BLL
{
    public class StudentCourseBLL : BaseBLL<IStudentCourseDAL>, IStudentCourseBLL
    {
        #region 学生课程

        public string CreateOrUpdateCourse(StudentCourseBO modelBo)
        {
            var amount = 0;
            //var cous = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == modelBo.CourseId);
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == modelBo.Id);
            ObjectHelper.AutoMapping(modelBo, queryModel, new string[] { "ClassTimes", "SurplusClassTimes" });
            if (queryModel.FtimeStemp == null)
            {
                var kj = new W_StudentKj();
                int kjdc = 0;
                if (!string.IsNullOrEmpty(modelBo.StudentKjId))
                {
                    kj = base.LoadFirstOrDefault<W_StudentKj>(p => p.Id == modelBo.StudentKjId);
                    Int32.TryParse(kj.Price, out kjdc);
                }
                var cls = base.LoadFirstOrDefault<M_Course>(p => p.Id == modelBo.CourseId);
                int dc = 0;
                Int32.TryParse(cls.Price, out dc);
                var pri = dc - kjdc;
                if (pri < 0)
                {
                    pri = 0;
                }
                if (cls.CategoryId != 5)
                {
                    cls.ClassTimes = (Convert.ToDouble(cls.ClassTimes) * 2).ToString();//课程*2次数 
                }
                queryModel.Price = pri.ToString();
                queryModel.SurplusClassTimes =Int32.Parse(cls.ClassTimes);
                queryModel.ClassTimes = Int32.Parse(cls.ClassTimes);
                queryModel.Id = Utils.GetGuid();
                queryModel.StudentId = modelBo.UserId;
                queryModel.CreateTime = DateTime.Now;
                queryModel.AgeCategoryId = cls.AgeCategoryId;
                queryModel.CategoryId = cls.CategoryId;
                queryModel.CreateUserId = modelBo.UserId; 
                amount = mServiceDAL.Create(queryModel); 
            }
            return queryModel.Id;
        }




        public AmountBo UpdateStudentCourse(StudentCourseBO modelBo)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == modelBo.Id);
            int adtime = 0;
            adtime = modelBo.AddClassTimes - queryModel.AddClassTimes;
            ObjectHelper.AutoMapping(modelBo, queryModel, new string[] { "studentId", "studentKjId", "HourInfo", "ispay" });
            var courseOne = base.LoadFirstOrDefault<M_Course>(p => p.Id == modelBo.CourseId);
            queryModel.Id = modelBo.Id;
            queryModel.Price = modelBo.Price.ToString();
            queryModel.SurplusClassTimes = modelBo.ClassTimes;
            //queryModel.ClassTimes = modelBo.ClassTimes;
            queryModel.CreateTime = DateTime.Now;
            queryModel.AgeCategoryId = courseOne.AgeCategoryId; //Int32.Parse(modelBo.AgeCategoryId);
            queryModel.CategoryId = courseOne.CategoryId;
            queryModel.CreateUserId = modelBo.UserId;
            queryModel.AddClassTimes = modelBo.AddClassTimes;
            queryModel.ClassTimes = queryModel.ClassTimes + adtime;
            queryModel.SurplusClassTimes = queryModel.SurplusClassTimes + adtime;
            queryModel.Name = modelBo.Name;
            queryModel.Tel = modelBo.Tel;
            amount = base.Update(queryModel);
            return base.ReturnBo(amount);
        }







        /// <summary>
        /// 体验课秀一秀
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public AmountBo SaveTyCouserVideoUrl(string id, string videoUrl, string uerId)
        {
            var queryModel = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == id);
            if (queryModel.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            queryModel.CourseVideoUrl = videoUrl;
            queryModel.UpdateTime = DateTime.Now;
            queryModel.UpdateUserId = uerId;
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        public List<StudentCourseVO> GetListCourse(StudentCourseListBO bo)
        {
            int totNum = 0;
            var list = new List<StudentCourseVO>();
            IEnumerable<M_StudentCourse> noumena = null;
            if (string.IsNullOrEmpty(bo.Normal))
            {
                var whereQl = BuildExpression<M_StudentCourse>(
                      BuildLambda<M_StudentCourse>(1, p => p.ispay == 1),
                     BuildLambda<M_StudentCourse>(bo.CategoryId, p => p.CategoryId == Int32.Parse(bo.CategoryId)),
                     BuildLambda<M_StudentCourse>(bo.Status, p => p.Status == Int32.Parse(bo.Status)),
                     BuildLambda<M_StudentCourse>(bo.UserId, p => p.StudentId == bo.UserId),
                     BuildLambda<M_StudentCourse>(bo.AgeCategoryId, p => p.AgeCategoryId == Int32.Parse(bo.AgeCategoryId)));

                if (bo.PageSize == 0)
                { 
                    noumena = mServiceDAL.LoadEntities<M_StudentCourse>(whereQl);
                    totNum = noumena.Count();
                }
                else
                {

                    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                        out totNum, whereQl, false, p => p.CreateTime); 
                }
                bo.TotNum = totNum;
            }
            else if (bo.Normal == "0")
            {
                var whereQl = BuildExpression<M_StudentCourse>(
                      BuildLambda<M_StudentCourse>(1, p => p.ispay == 1),
                    BuildLambda<M_StudentCourse>(bo.CategoryId, p => p.CategoryId != 5),
                    BuildLambda<M_StudentCourse>(bo.Status, p => p.Status == Int32.Parse(bo.Status)),
                    BuildLambda<M_StudentCourse>(bo.UserId, p => p.StudentId == bo.UserId),
                    BuildLambda<M_StudentCourse>(bo.AgeCategoryId, p => p.AgeCategoryId == Int32.Parse(bo.AgeCategoryId)));

                if (bo.PageSize == 0)
                {
                    //noumena = mServiceDAL.LoadEntities<M_StudentCourse>(whereQl, true, x => new { x.M_Student });
                    noumena = mServiceDAL.LoadEntities<M_StudentCourse>(whereQl);
                    totNum = noumena.Count();
                }
                else
                {

                    noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                        out totNum, whereQl, false, p => p.CreateTime);
                    //noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    //    out totNum, whereQl, false, p => p.CreateTime, true, x => new { x.M_Student });
                }
                bo.TotNum = totNum;
            }
            else if (bo.Normal == "1")
            {
                var whereQl = BuildExpression<M_StudentCourse>(
                      BuildLambda<M_StudentCourse>(1, p => p.ispay == 1),
                    BuildLambda<M_StudentCourse>(bo.CategoryId, p => p.CategoryId == 5),
                    BuildLambda<M_StudentCourse>(bo.Status, p => p.Status == Int32.Parse(bo.Status)),
                    BuildLambda<M_StudentCourse>(bo.UserId, p => p.StudentId == bo.UserId),
                    BuildLambda<M_StudentCourse>(bo.AgeCategoryId, p => p.AgeCategoryId == Int32.Parse(bo.AgeCategoryId)));

                if (bo.PageSize == 0)
                {

                    noumena = mServiceDAL.LoadEntities<M_StudentCourse>(whereQl);
                    //noumena = mServiceDAL.LoadEntities<M_StudentCourse>(whereQl, true, x => new { x.M_Student });
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
            var cou = base.LoadEntities<M_Course>(p => p.Id != null); 
            var studentList = base.LoadEntities<M_Student>(p => p.Id != null);
            M_Student stu = new M_Student();
            foreach (var p in noumena.OrderByDescending(x => x.CreateTime).ToList())
            {
                StudentCourseVO vo = new StudentCourseVO();
                stu = studentList.FirstOrDefault<M_Student>(x => x.Id == p.StudentId);
                vo.Id = p.Id;
                vo.CreateTime = Convert.ToDateTime(p.CreateTime).ToString("yyyy-MM-dd HH:mm:ss");
                vo.AgeCategoryId = p.AgeCategoryId;
                vo.CategoryId = p.CategoryId;
                vo.Title = cou.FirstOrDefault<M_Course>(x => x.Id == p.CourseId)?.Title;
                vo.Status = p.Status;
                vo.StudentId = p.StudentId;
                //vo.ClassTimes = p.ClassTimes;
                vo.Price = p.Price;
                vo.Sort = p.Sort;
                vo.CourseId = p.CourseId;
                if (p.CategoryId == 5)//体验课不/2
                {
                    vo.SurplusClassTimes =p.SurplusClassTimes.ToString();
                    vo.ClassTimes = p.ClassTimes.ToString();
                }
                else
                {
                    var s = p.SurplusClassTimes % 2;
                    if (s == 1)
                        vo.SurplusClassTimes = (p.SurplusClassTimes / 2).ToString() + ".5";
                    else
                        vo.SurplusClassTimes = (p.SurplusClassTimes / 2).ToString();

                    var w = p.ClassTimes % 2;
                    if (w == 1)
                        vo.ClassTimes = (p.ClassTimes / 2).ToString() + ".5";
                    else
                        vo.ClassTimes = (p.ClassTimes / 2).ToString(); 
                }

                vo.StudentName = !string.IsNullOrEmpty(p.Name) ? p.Name : stu?.FullName;
                vo.StudentTel = !string.IsNullOrEmpty(p.Tel) ? p.Tel : stu?.Tel;
                vo.HeadImgUrl = stu?.HeadImgUrl;
                vo.CourseVideoUrl = p.CourseVideoUrl;
                vo.AddClassTimes = p.AddClassTimes;
                list.Add(vo);
            } 
            return list; 
        }

        public StudentCourseOneVO GetOneCourse(string Id)
        {
            var model = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == Id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            var cou = base.LoadFirstOrDefault<M_Course>(p => p.Id == model.CourseId);
            var kj = base.LoadFirstOrDefault<W_StudentKj>(p => p.Id == model.StudentKjId);
            var stu = base.LoadFirstOrDefault<M_Student>(p => p.Id == model.StudentId);
            var mo = new StudentCourseOneVO();
            ObjectHelper.AutoMapping(model, mo);
            mo.Id = model.Id;
            //mo.CreateUserId = "";
            mo.CourseName = cou?.Title; 
            mo.KjPrice = kj?.Price;
            mo.CreateTime = Convert.ToDateTime(model.CreateTime).ToString("yyyy-MM-dd HH:mm:ss");
            mo.StudentTel = !string.IsNullOrEmpty(model.Tel) ? model.Tel : stu?.Tel;
            mo.StudentName = !string.IsNullOrEmpty(model.Name) ? model.Name : stu?.FullName;
            return mo;
        }


        public StudentCourseOneVO2 GetOneCourse2(string Id)
        {
            var model = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == Id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            var cou = base.LoadFirstOrDefault<M_Course>(p => p.Id == model.CourseId);
            var kj = base.LoadFirstOrDefault<W_StudentKj>(p => p.Id == model.StudentKjId);
            var stu = base.LoadFirstOrDefault<M_Student>(p => p.Id == model.StudentId);
            var mo = new StudentCourseOneVO2();
            ObjectHelper.AutoMapping(model, mo);
            mo.Id = model.Id;
            //mo.CreateUserId = "";
            //mo.SurplusClassTimes=
            var s = model.SurplusClassTimes % 2;
            if (s == 1)
                mo.SurplusClassTimes = (model.SurplusClassTimes / 2).ToString() + ".5";
            else
                mo.SurplusClassTimes = (model.SurplusClassTimes / 2).ToString();

            var w = model.ClassTimes % 2;
            if (w == 1)
                mo.ClassTimes = (model.ClassTimes / 2).ToString() + ".5";
            else
                mo.ClassTimes = (model.ClassTimes / 2).ToString();


            mo.CourseName = cou?.Title;
            mo.KjPrice = kj?.Price;
            mo.CreateTime = Convert.ToDateTime(model.CreateTime).ToString("yyyy-MM-dd HH:mm:ss");
            mo.StudentTel = !string.IsNullOrEmpty(model.Tel) ? model.Tel : stu?.Tel;
            mo.StudentName = !string.IsNullOrEmpty(model.Name) ? model.Name : stu?.FullName;
            return mo;
        }


        public M_StudentCourse GetOneStudentCourseByIdSi(string Id)
        {
            return base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == Id);
        }

        public bool UpdateStudentCourseStatus(string id, string status)
        {
            var mo = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == id);
            List<SqlItem> sqlItem = new List<SqlItem>();
            SqlParameter[] par =
          {
                new SqlParameter("@id",id),
                new SqlParameter("@status",status),
                new SqlParameter("@classTimes",0)
            };
            var sql = new SqlItem();

            sql = new SqlItem()
            {
                SqlValue = "update M_StudentCourse set Status=@Status,ClassTimes=@classTimes where id=@id ",
                Params = par.ToArray()
            };
            sqlItem.Add(sql);



            if (!string.IsNullOrEmpty(mo.StudentKjId))
            {
                var kj = base.LoadFirstOrDefault<W_StudentKj>(p => p.Id == mo.StudentKjId);
                kj.Status = 1;
                var sj = mServiceDAL.Update(kj);
            }


            return base.ExecuteSqlTran(sql) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public bool DeleteStudentCourse(string Id)
        {
            List<SqlItem> sqlItem = new List<SqlItem>();
            SqlParameter[] par =
          {
                new SqlParameter("@id",Id)
            };
            var sql = new SqlItem();

            sql = new SqlItem()
            {
                SqlValue = "delete M_StudentCourse  where id=@id",
                Params = par.ToArray()
            };
            sqlItem.Add(sql);
            return base.ExecuteSqlTran(sql) > 0;

        }
        #endregion

        #region 请假 
        /// <summary>
        /// 请假
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo CreateOrUpdateLeave(StudentLeaveBO modelBo)
        {
            var amount = 0;
            //if(base.Count<M_StudentLeave>(p=>p.StudentCourseClassId==modelBo.StudentCourseClassId&&p.StudentId==modelBo.StudentId)>0)
            // {
            //     throw new schoolException(SubCode.Failure.GetHashCode(), "不能重复申请！");
            // }
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_StudentLeave>(p => p.Id == modelBo.Id);
            ObjectHelper.AutoMapping(modelBo, queryModel);

            var st = base.LoadFirstOrDefault<M_StudentLeave>(p => p.StudentCourseClassId == modelBo.StudentCourseClassId && p.StudentId == modelBo.StudentId);
            if (st.FtimeStemp != null)
            {
                ////状态(0, 正常，1请假，2申请成功，3，申请失败, 4取消）
                //if (st.Status == 1)
                //    throw new schoolException(SubCode.Failure.GetHashCode(), "不能重复申请！");
                //var bo = base.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == queryModel.StudentCourseClassId);
                //if (bo.FtimeStemp == null)
                //    throw new schoolException(SubCode.Failure.GetHashCode(), "数据错误，非法操作！");
                //var mo = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == bo.StudentCourseId);
                //if (mo.FtimeStemp == null)
                //    throw new schoolException(SubCode.Failure.GetHashCode(), "数据错误，非法操作！");
                //List<SqlItem> sqlList = new List<SqlItem>();
                //var count = base.Count<M_StudentCourseClass>(p => p.Status == 0 && p.StudentCourseId == mo.Id);
                //if(bo.Status)
                //int num = mo.ClassTimes - count+1;
                //if (num > mo.ClassTimes)
                //    num = mo.ClassTimes;
                //SqlItem sql = UpdateSurplusClassTimes(mo.Id, num);
                //sqlList.Add(sql);
                //sqlList.Add(UpdateStudentCourseClass(queryModel.StudentCourseClassId, 0));
                //base.ExecuteSqlTran(sqlList.ToArray());
                //return UpdateLeaveStauts(modelBo.StudentCourseClassId, modelBo.Status, modelBo.UserId);
            }
            else
            {
                var courclass = base.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == modelBo.StudentCourseClassId);
                var teacherCourse = base.LoadFirstOrDefault<M_TeacherCourse>(p => p.Id == courclass.TeacherCourseId);
                var scholl = base.LoadFirstOrDefault<M_School>(p => p.Id == teacherCourse.SchoolId);
                queryModel.Day = teacherCourse.Day;
                queryModel.TeacherCourseTime = teacherCourse?.TeacherCourseTime;
                queryModel.ClassName = teacherCourse.Name;
                queryModel.SchoolName = scholl?.Name;
                queryModel.TeacherId = teacherCourse.StudentId;
                queryModel.Id = Utils.GetGuid();
                queryModel.CreateTime = DateTime.Now;
                queryModel.CreateUserId = modelBo.UserId;
                amount = mServiceDAL.Create(queryModel);
            }
            return base.ReturnBo(amount);
        }

        /// <summary>
        /// 请假状态修改(按课时）
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo UpdateLeaveStauts(string id, int status, string userId)
        {
            var amount = 0;
            var queryModel = mServiceDAL.LoadFirstOrDefault<M_StudentLeave>(p => p.StudentCourseClassId == id);
            if (queryModel.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            queryModel.UpdateTime = DateTime.Now;
            queryModel.UpdateUserId = userId;
            queryModel.Status = status;

            if (status == LeaveStatusEnum.Suc.GetHashCode())//课程+1 ,课表取消
            {
                var bo = base.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == queryModel.StudentCourseClassId);
                if (bo.FtimeStemp == null)
                    throw new schoolException(SubCode.Failure.GetHashCode(), "数据错误，非法操作！");
                var mo = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == bo.StudentCourseId);
                if (mo.FtimeStemp == null)
                    throw new schoolException(SubCode.Failure.GetHashCode(), "数据错误，非法操作！");
                //查询课程名称
                var course = base.LoadFirstOrDefault<M_Course>(p => p.Id == mo.CourseId);
                List<SqlItem> sqlList = new List<SqlItem>();
                var count = base.Count<M_StudentCourseClass>(p => p.Status == 0 && p.StudentCourseId == mo.Id);
                int num = 0;
                if (mo.CategoryId == 5)//体验课
                {
                    num = mo.SurplusClassTimes + 1;
                    if (num > mo.SurplusClassTimes)
                        num = mo.SurplusClassTimes;
                }
                else //普通课程
                {
                    var listcount = base.Count<M_Subscribe>(x => x.TeacherCourseId == bo.TeacherCourseId);
                    var timenum = mo.SurplusClassTimes + listcount;
                    num = timenum;
                }
                SqlItem sql = UpdateSurplusClassTimes(mo.Id, num);
                sqlList.Add(sql);
                sqlList.Add(UpdateStudentCourseClass(queryModel.StudentCourseClassId, 2));
                base.ExecuteSqlTran(sqlList.ToArray());
                //发送消息给学员
                if (!string.IsNullOrEmpty(mo.Tel))
                {
                    Task.Factory.StartNew(() => WxUserLibApi.sendStudentLeave(mo.Tel, course.Title));
                }
            }


            amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        /// <summary>
        /// 获取学生请假信息
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public List<StudentLeaveVO> GetListLeave(StudentLeaveListBO bo)
        {
            int totNum = 0;
            var list = new List<StudentLeaveVO>();

            var whereQl = BuildExpression<M_StudentLeave>(
                 BuildLambda<M_StudentLeave>(bo.StudentCourseClassId, p => p.StudentCourseClassId == bo.StudentCourseClassId),
                 BuildLambda<M_StudentLeave>(bo.Status, p => p.Status == bo.Status),
                 BuildLambda<M_StudentLeave>(bo.TeacherId, p => p.TeacherId == bo.TeacherId),
                 BuildLambda<M_StudentLeave>(bo.StudentId, p => p.StudentId == bo.StudentId)

                 );
            IEnumerable<M_StudentLeave> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_StudentLeave>(whereQl, true, x => new { x.M_Student });
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime, true, x => new { x.M_Student });
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            var telist = base.LoadEntities<M_Teacher>(p => p.Id != null);
            list = noumena.OrderByDescending(x => x.CreateTime).Select(
            p => new StudentLeaveVO
            {
                Id = p.Id,
                CreateTime = p.CreateTime,
                TeacherId = p.TeacherId,
                Status = p.Status,
                Main = p.Main,
                StudentId = p.StudentId,
                HeadImgUrl = p.M_Student.HeadImgUrl,
                StudentName = p.M_Student.FullName,
                ClassName = p.ClassName,
                Day = p.Day,
                SchoolName = p.SchoolName,
                StudentTel = p.M_Student.Tel,
                TeacherName = telist.FirstOrDefault<M_Teacher>(x => x.StudentId == p.TeacherId)?.Name,
                TeacherTel = telist.FirstOrDefault<M_Teacher>(x => x.StudentId == p.TeacherId)?.Tel,
                TeacherCourseTime = p.TeacherCourseTime,
                StudentCourseClassId = p.StudentCourseClassId
            }).OrderByDescending(p => p.CreateTime).ToList();
            return list;
        }
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public StudentLeaveVO GetOneLeave(string Id)
        {
            var model = base.LoadFirstOrDefault<M_StudentLeave>(p => p.Id == Id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            model.CreateUserId = "";
            var teac = base.LoadFirstOrDefault<M_Teacher>(p => p.Id == model.TeacherId);
            var stu = base.LoadFirstOrDefault<M_Student>(p => p.Id == model.StudentId);
            StudentLeaveVO vo = new StudentLeaveVO();
            ObjectHelper.AutoMapping(model, vo);
            vo.TeacherName = teac?.Name;
            vo.TeacherTel = teac?.Tel;
            vo.StudentName = stu?.FullName;
            vo.StudentTel = stu?.Tel;
            return vo;
        }

        #endregion


        #region  
        #region 学生上课报名 （按次数）
        /// <summary>
        /// 报名选课
        /// </summary>
        /// <param name="modelBo"></param>
        /// <returns></returns>
        public AmountBo CreateOrUpdateStudentClass(string studentId, string teachercourseId, string courseId)
        {
            var addcount = mServiceDAL.Count<M_StudentCourseClass>(p => p.TeacherCourseId == teachercourseId && p.StudentCourseId == courseId && p.Status != LeaveStatusEnum.Suc.GetHashCode());
            if (addcount > 0)
                throw new schoolException(SubCode.Failure.GetHashCode(), "操作失败，本课程你已经报名无需重复报名！");
            var queryModel = new M_StudentCourseClass();// mServiceDAL.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == teachercourseId);
            List<SqlItem> sqlList = new List<SqlItem>();
            var stu = base.LoadFirstOrDefault<M_Student>(p => p.Id == studentId);
            ///用户使用课程报名次数
            var count = base.Count<M_StudentCourseClass>(p => p.Status != LeaveStatusEnum.Suc.GetHashCode() && p.StudentCourseId == courseId);
            var mo = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == courseId);
            int num = 0;
            var tec = base.LoadFirstOrDefault<M_TeacherCourse>(p => p.Id == teachercourseId);
            var pl = base.LoadFirstOrDefault<M_TeachingPlan>(p => p.Id == tec.TeachingPlanId);
            var course = base.LoadFirstOrDefault<M_Course>(p => p.Id == mo.CourseId);
            #region 获取市场
            if (mo.CategoryId != 5) //非体验课
            {
                var listcount = base.Count<M_Subscribe>(x => x.TeacherCourseId == teachercourseId);
                var timenum = mo.SurplusClassTimes - listcount;
                num = timenum;
            }
            else
            {
                num = mo.SurplusClassTimes - 1;
            }
            #endregion

            //int num = mo.ClassTimes - count;
            //int num = timenum;

            if (num < 0)
                throw new schoolException(SubCode.Failure.GetHashCode(), "操作失败，课程剩余数量少于课程需要数不能进行报名！");
            else
            {
                //num = num - 1;
                sqlList.Add(UpdateSurplusClassTimes(courseId, num));
                if (num <= 6 )
                {
                    sendRenewMessage(num,stu,course,tec.StudentId);
                }
            }
    
            queryModel.Id = Utils.GetGuid();
            queryModel.Status = 0;
            queryModel.Sort = 0;
            queryModel.StudentId = mo.StudentId;
            queryModel.SChoolId = tec.SchoolId;
            queryModel.FieldId = tec.FieldId;
            queryModel.TeacherCourseId = teachercourseId;
            queryModel.TeacherId = tec.StudentId;
            queryModel.CategoryId = tec.CategoryId;
            queryModel.AgeCategoryId = tec.AgeCategoryId;
            queryModel.CreateTime = DateTime.Now;
            queryModel.CreateUserId = studentId;
            queryModel.StudentName = stu?.FullName;
            queryModel.StudentCourseId = courseId;
            queryModel.Day = tec.Day;
            queryModel.JobStatus = "0";
            queryModel.JobTempVideoId = pl?.JobVideoId;
            queryModel.JobPoints = pl?.JobPoints;
            queryModel.JobName = pl.FtimeStemp != null ? base.LoadFirstOrDefault<M_Video>(p => p.Id == pl.JobVideoId)?.Title : null;
            queryModel.JobTempVideoUrl = pl.FtimeStemp != null ? base.LoadFirstOrDefault<M_Video>(p => p.Id == pl.JobVideoId)?.Url : null;
            sqlList.Add(base.InsertSqlItem<M_StudentCourseClass>(queryModel));
            var exnum = base.ExecuteSqlTran(sqlList.ToArray());
            return base.ReturnBo(exnum);
        }

        /// <summary>
        /// 推送模板消息给学员和教练
        /// </summary>
        /// <param name="num"></param>
        /// <param name="stu"></param>
        /// <param name="course"></param>
        /// <param name="studentId"></param>
        private void sendRenewMessage(int num, M_Student stu, M_Course course, string teacherId)
        {
            //推送模板给学员
            var dic = new Dictionary<string, MessageContent>();
            dic.Add("thing1", new MessageContent { value = stu.NickName});
            String title = course.Title.Length > 8 ? course.Title.Substring(0, 8) + "..." : course.Title;
            dic.Add("thing2", new MessageContent { value = title });
            dic.Add("number3", new MessageContent { value = num.ToString() });

            dic.Add("thing4", new MessageContent { value = "剩余课时已不足，请及时续费" });

            var bo = new SendMessageSMBO()
            {
                data = dic,
                template_id = "ElmZuJQwNb-7ZjZhA5l0nzzRfG9cShnDmWs2O34uSow",
                touser = stu.SmOpenId
            };
            WxSmallTokenVO wxvo = WxUserLibApi.GetWXSMToken();
            WxUserLibApi.SendMessage(wxvo.access_token, bo.ToJson());

            //推送模板给教练
          /*  var stu1 = base.LoadFirstOrDefault<M_Student>(p => p.Id == teacherId);
            var dic1 = new Dictionary<string, MessageContent>();
            dic1.Add("thing1", new MessageContent { value = stu1.NickName });

            dic1.Add("thing2", new MessageContent { value = course.Title });
            dic1.Add("number3", new MessageContent { value = num.ToString() });

            dic1.Add("thing4", new MessageContent { value = stu.NickName + "课时已不足" });

            var bo1 = new SendMessageSMBO()
            {
                data = dic1,
                template_id = "ElmZuJQwNb-7ZjZhA5l0nzzRfG9cShnDmWs2O34uSow",
                touser = stu1.SmOpenId
            };
            WxUserLibApi.SendMessage(wxvo.access_token, bo1.ToJson());
*/
            //推送模板给教务
            /*   var dic2 = new Dictionary<string, MessageContent>();
               dic2.Add("thing1", new MessageContent { value = stu1.NickName });

               dic2.Add("thing2", new MessageContent { value = course.Title });
               dic2.Add("number3", new MessageContent { value = num.ToString() });

               dic1.Add("thing4", new MessageContent { value = stu.NickName + "课时已不足" });

               var bo2 = new SendMessageSMBO()
               {
                   data = dic1,
                   template_id = "ElmZuJQwNb-7ZjZhA5l0nzzRfG9cShnDmWs2O34uSow",
                   touser = stu1.SmOpenId
               };
               WxUserLibApi.SendMessage(SessionFactory.SessionService.mWtokenBLL.Get(), bo2.ToJson());*/
        }

        #region 作业评价等
        /// <summary>
        /// 老师布置作业
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public AmountBo SaveTeacherAddJob(string id, string jobName, string videoId, string uerId, string jobPoints, string trainNum, string trainTime)
        {
            var queryModel = base.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == id);
            if (queryModel.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            var video = base.LoadFirstOrDefault<M_Video>(p => p.Id == videoId);
            queryModel.JobTempVideoId = videoId;
            queryModel.JobTempVideoUrl = video.Url;
            queryModel.JobTempPic = video.Pic;
            queryModel.UpdateTime = DateTime.Now;
            queryModel.UpdateUserId = uerId;
            queryModel.JobName = jobName;
            queryModel.JobStatus = "0";
            queryModel.JobPoints = jobPoints;
            queryModel.TrainNum = trainNum;
            queryModel.TrainTime = trainTime;
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        /// <summary>
        /// 学生提交作业
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public AmountBo SaveStudentAddJob(string id, string videoUrl, string uerId)
        {
            var queryModel = base.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == id);
            if (queryModel.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            queryModel.StudentJobUrl = videoUrl;
            queryModel.UpdateTime = DateTime.Now;
            queryModel.UpdateUserId = uerId;
            queryModel.JobStatus = "1";
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }


        /// <summary>
        /// 老师给学生本课程评价
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public AmountBo SaveTeacherEvaluate(string id, string studentEvaluate, string uerId)
        {
            var queryModel = base.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == id);
            if (queryModel.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            queryModel.StudentEvaluate = studentEvaluate;
            queryModel.UpdateUserId = uerId;
            queryModel.JobStatus = "2";
            queryModel.UpdateTime = DateTime.Now;
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }


        /// <summary>
        /// 秀一秀
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public AmountBo SaveCourseVideoUrl(string id, string courseVideoUrl, string uerId)
        {
            var queryModel = base.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == id);
            if (queryModel.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            queryModel.CourseVideoUrl = courseVideoUrl;
            queryModel.UpdateUserId = uerId;
            queryModel.UpdateTime = DateTime.Now;
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        /// <summary>
        /// 作业评价
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public AmountBo SaveJobEvaluate(string id, string JobEvaluate, string uerId)
        {
            var queryModel = base.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == id);
            if (queryModel.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            queryModel.JobEvaluate = JobEvaluate;
            queryModel.UpdateUserId = uerId;
            queryModel.UpdateTime = DateTime.Now;
            queryModel.JobStatus = "4";
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }

        /// <summary>
        /// 学生给老师本课程评价
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jobName"></param>
        /// <param name="videoId"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public AmountBo SaveStudentEvaluate(string id, string teacherEvaluate, int studentStars, string uerId)
        {
            var queryModel = base.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == id);
            if (queryModel.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            queryModel.TeacherEvaluate = teacherEvaluate;
            queryModel.StudentStars = studentStars;
            queryModel.UpdateUserId = uerId;
            queryModel.UpdateTime = DateTime.Now;
            var amount = mServiceDAL.Update(queryModel);
            return base.ReturnBo(amount);
        }
        #endregion

        public List<StudentCourseClassVO> GetListStudentCourseClass(StudentCourseClassQueryBO bo)
        {
            int totNum = 0;
            var list = new List<StudentCourseClassVO>();

            var whereQl = BuildExpression<M_StudentCourseClass>(
                 BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                 BuildLambda<M_StudentCourseClass>(bo.Status, p => p.Status == Int32.Parse(bo.Status)),
                 BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.StudentId == bo.StudentId)
                 );
            IEnumerable<M_StudentCourseClass> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_StudentCourseClass>(whereQl, true, x => new { x.M_Student, x.M_TeacherCourse });
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime, true, x => new { x.M_Student, x.M_TeacherCourse });
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            var teaclist = base.LoadEntities<M_Teacher>(p => p.Id != null).ToList();
            var schlist = base.LoadEntities<M_School>(p => p.Id != null);
            var fil = base.LoadEntities<M_Field>(p => p.Id != null);
            foreach (var p in noumena)
            {
                var tec = teaclist.FirstOrDefault<M_Teacher>(x => x.StudentId == p.TeacherId);
                var fi = fil.FirstOrDefault<M_Field>(x => x.Id == p.FieldId);
                var sch = schlist.FirstOrDefault<M_School>(x => x.Id == p.SChoolId);
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
                    JobTempVideoId = p.JobTempVideoId,
                    JobTempVideoUrl = p.JobTempVideoUrl,
                    StudentEvaluate = p.StudentEvaluate,
                    StudentStars = p.StudentStars,
                    StudentTel = p.M_Student?.Tel,
                    TeacherEvaluate = p.TeacherEvaluate,
                    Day = p.M_TeacherCourse.Day,
                    StudentJobUrl = p.StudentJobUrl,
                    TeacherCourseName = p.M_TeacherCourse.Name,
                    TeacherCourseTime = p.M_TeacherCourse.TeacherCourseTime,
                    TeacherName = tec.Name,
                    TeacherTel = tec.Tel,
                    FieldName = fi?.Name,
                    SchoolName = sch?.Name
                };
                list.Add(vo);
            }
            return list;

        }

        public StudentCourseClassVO GetOneStudentCourseClass(string Id)
        {
            var model = base.LoadFirstOrDefault<M_StudentCourseClass>(p => p.Id == Id);
            if (model.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误非法操作！");
            model.CreateUserId = "";
            StudentCourseClassVO vo = new StudentCourseClassVO();
            ObjectHelper.AutoMapping(model, vo);

            var stuli = base.LoadFirstOrDefault<M_StudentLeave>(x => x.StudentId == model.StudentId & x.StudentCourseClassId == Id);
            var stu = base.LoadFirstOrDefault<M_Student>(p => p.Id == model.StudentId);
            var cla = base.LoadFirstOrDefault<M_TeacherCourse>(p => p.Id == model.TeacherCourseId);
            var tel = base.LoadFirstOrDefault<M_Teacher>(p => p.StudentId == model.TeacherId);
            var fi = base.LoadFirstOrDefault<M_Field>(x => x.Id == model.FieldId);
            var sch = base.LoadFirstOrDefault<M_School>(x => x.Id == model.SChoolId);
            vo.TeacherCourseName = cla?.Name;
            vo.StudentName = stu?.FullName;
            vo.HeadImgUrl = stu?.HeadImgUrl;
            vo.NickName = stu?.NickName;
            vo.StudentTel = stu?.Tel;
            vo.Day = cla.Day;
            vo.TeacherCourseTime = cla.TeacherCourseTime;
            vo.TeacherName = tel.Name;
            vo.TeacherTel = tel.Tel;
            vo.TeacherPic = tel.Pic;
            vo.Status = model.Status == 0 ? GetStatus(model.Status, model.Day) : model.Status;
            vo.StudentLeave = stuli?.Status.ToString();
            vo.SchoolName = sch?.Name;
            vo.FieldName = fi?.Name;
            return vo;
        }
        /// <summary>
        /// 更新次数-1天
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SqlItem UpdateSurplusClassTimes(string id, int num)
        {

            SqlParameter[] par =
            {
                new SqlParameter("@id", id),
                new SqlParameter("@num",num)
            };
            var sql = new SqlItem();
            sql = new SqlItem()
            {
                SqlValue = "UPDATE [dbo].[M_StudentCourse] SET [SurplusClassTimes] =@num where id =@id",
                Params = par.ToArray()
            };
            return sql;
        }
        /// <summary>
        ///  取消课表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public SqlItem UpdateStudentCourseClass(string id, int status)
        {

            SqlParameter[] par =
            {
                new SqlParameter("@id", id),
                new SqlParameter("@status",status)
            };
            var sql = new SqlItem();
            sql = new SqlItem()
            {
                SqlValue = "UPDATE [dbo].[M_StudentCourseClass] SET [status] =@status where id =@id",
                Params = par.ToArray()
            };
            return sql;
        }

        public List<SqlItem> GetEscTeacherCourse(string teachercourseId, int status)
        {
            List<SqlItem> sqlItems = new List<SqlItem>();
            var list = base.LoadEntities<M_StudentCourseClass>(p => p.TeacherCourseId == teachercourseId);
            var teachercou = base.LoadFirstOrDefault<M_TeacherCourse>(p => p.Id == teachercourseId);
            if (teachercou.FtimeStemp == null)
                return sqlItems;
            var listcount = base.Count<M_Subscribe>(x => x.TeacherCourseId == teachercourseId);

            #endregion


            foreach (var item in list)
            {
                int num = 1;
                var mo = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == item.StudentCourseId);

                if (mo.CategoryId != 5) //非体验课
                {
                    var timenum = mo.SurplusClassTimes + listcount;
                    num = timenum;
                }
                else
                {
                    num = mo.SurplusClassTimes + 1;
                }


                sqlItems.Add(UpdateStudentCourseClass(item.Id, 2));
                if (item.Status != 2)
                    sqlItems.Add(UpdateSurplusClassTimes(mo.Id, num));
            }
            return sqlItems;
        }

        #endregion

        #region 我的课程 -约课
        /// <summary>
        /// 获取老师列表
        /// </summary>
        /// <returns></returns>
        public List<TeacherVO> GetTeacherList()
        {
            var list = new List<TeacherVO>();
            var Teacherlist = base.LoadEntities<M_Teacher>(p => p.Id != null).ToList().OrderByDescending(p => p.Sort).ThenByDescending(p => p.Id);
            foreach (var item in Teacherlist)
            {
                var vo = new TeacherVO();
                ObjectHelper.AutoMapping(item, vo);
                vo.Id = item.StudentId;
                list.Add(vo);
            }
            return list;
        }
        /// <summary>
        /// 获取所选校区
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<KeyValueBo> GetSchoolByTeacherId(string Id)
        {
            int day = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            var cou = base.LoadEntities<M_TeacherCourse>(p => p.StudentId == Id && p.Day > day, true, x => new { x.M_School })
                .Select(t => new KeyValueBo
                {
                    Id = t.Id,
                    Name = t.Name
                }).OrderByDescending(p => p.Id).ToList();
            return cou;
        }

        public List<TeacherCourseFieldVO> GetTeacherCourseByTeacherSchool(string teacherId, string schoolId, string studentCourseId, int addDay)
        {
            if (addDay < 7)
                addDay = 7;
            var bo = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == studentCourseId);
            List<TeacherCourseFieldVO> list = new List<TeacherCourseFieldVO>();
            int day = Int32.Parse(DateTime.Now.ToString("yyyyMMdd"));

            int nextDay = Int32.Parse(DateTime.Now.AddDays(addDay).ToString("yyyyMMdd"));
            var listCour = base.LoadEntities<M_TeacherCourse>(p => p.StudentId == teacherId && p.SchoolId == schoolId
            && p.CategoryId == bo.CategoryId && p.AgeCategoryId == bo.AgeCategoryId
            && p.Day >= day && p.Day < nextDay && p.Status != 2 && p.Deleted == 0,
            true, x => new { x.M_Field, x.M_TeachingPlan }).ToList();

            foreach (var item in listCour)
            {
                int synum = 0;
                var count = base.Count<M_StudentCourseClass>(p => p.TeacherCourseId == item.Id && p.Status == 0);
                switch (item.CategoryId)
                {
                    case 1:
                        synum = 1 - count;
                        break;
                    case 2:
                        synum = 2 - count;
                        break;
                    case 3:
                        synum = 4 - count;
                        break;
                    case 4:
                        synum = 5 - count;
                        break;

                }
                var vo = new TeacherCourseFieldVO
                {
                    Id = item.Id,
                    FieldId = item.FieldId,
                    FieldName = item.M_Field.Name,
                    Num = count,
                    SurplusNum = synum,
                    Day = item.Day,
                    Name = item.M_TeachingPlan?.Title,
                    TeacherTime = item.TeacherCourseTime,

                };
                list.Add(vo);
            }
            return list;
        }
        #endregion

        #region 我的课表
        public List<StudentCourseClassVO> GetListStudentCourseClassByStudent(StudentCourseClassQueryBO bo)
        {
            int totNum = 0;
            var list = new List<StudentCourseClassVO>();

            var whereQl = BuildExpression<M_StudentCourseClass>();

            whereQl = BuildExpression<M_StudentCourseClass>(
                BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.StudentId == bo.StudentId));

            if (bo.Status == "0")
            {
                whereQl = BuildExpression<M_StudentCourseClass>(
                    BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                    BuildLambda<M_StudentCourseClass>(bo.Status, p => p.Status == Int32.Parse(bo.Status) && p.Day >= Int32.Parse(DateTime.Now.ToString("yyyyMMdd"))),
                    BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.StudentId == bo.StudentId));
            }
            if (bo.Status == "2")
            {
                whereQl = BuildExpression<M_StudentCourseClass>(
                    BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                    BuildLambda<M_StudentCourseClass>(bo.Status, p => p.Status == Int32.Parse(bo.Status)),
                    BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.StudentId == bo.StudentId));
            }
            if (bo.Status == "1")
                whereQl = BuildExpression<M_StudentCourseClass>(
                    BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                    BuildLambda<M_StudentCourseClass>(bo.Status, p => p.Status == 0 && p.Day < Int32.Parse(DateTime.Now.ToString("yyyyMMdd"))),
                    BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.StudentId == bo.StudentId));
            IEnumerable<M_StudentCourseClass> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_StudentCourseClass>(whereQl).OrderByDescending(p => p.CreateTime);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            var teaclist = base.LoadEntities<M_Teacher>(p => p.Id != null).ToList();
            var fields = base.LoadEntities<M_Field>(p => p.Id != null);
            var schools = base.LoadEntities<M_School>(p => p.Id != null);
            var studenlis = base.LoadEntities<M_StudentLeave>(p => p.Id != null);
            var stulist = base.LoadEntities<M_Student>(p => p.Id != null);
            var teachercourlist = base.LoadEntities<M_TeacherCourse>(p => p.Id != null);
            foreach (var p in noumena)
            {

                //string JobStatus = "尚未布置作业";//(1)

                //if (p.JobStatus == "1")
                //{
                //    JobStatus = "尚未布置作业";
                //}
                //else if (p.JobStatus == "2")
                //{
                //    JobStatus = "已布置作业";//(2)
                //}
                //else if (p.JobStatus == "3")
                //{
                //    JobStatus = "作业已上传";//（3）
                //}
                //else if (p.JobStatus == "4")
                //{
                //    JobStatus = "作业已评价";//(4)
                //}
                var tec = teaclist.FirstOrDefault<M_Teacher>(x => x.StudentId == p.TeacherId);
                var fi = fields.FirstOrDefault<M_Field>(o => o.Id == p.FieldId);
                var sc = schools.FirstOrDefault<M_School>(t => t.Id == p.SChoolId);
                var stuli = studenlis.FirstOrDefault<M_StudentLeave>(x => x.StudentId == p.StudentId & x.StudentCourseClassId == p.Id);
                var stu = stulist.FirstOrDefault<M_Student>(x => x.Id == p.StudentId);
                var tou = teachercourlist.FirstOrDefault<M_TeacherCourse>(x => x.Id == p.TeacherCourseId);
                var vo = new StudentCourseClassVO();

                {
                    vo.Id = p.Id;
                    vo.CreateTime = p.CreateTime;
                    vo.TeacherId = p.TeacherId;
                    vo.Status = p.Status == 0 ? GetStatus(p.Status, p.Day) : p.Status;
                    vo.StudentName = stu?.FullName;
                    vo.StudentId = p.StudentId;
                    vo.HeadImgUrl = stu?.HeadImgUrl;
                    vo.NickName = stu?.NickName;
                    vo.JobStatus = p.JobStatus;
                    vo.TeacherCourseId = p.TeacherCourseId;
                    vo.JobEvaluate = p.JobEvaluate;
                    vo.JobName = p.JobName;
                    vo.JobTempVideoId = p.JobTempVideoId;
                    vo.JobTempVideoUrl = p.JobTempVideoUrl;
                    vo.StudentEvaluate = p.StudentEvaluate;
                    vo.StudentStars = p.StudentStars;
                    vo.StudentTel = stu?.Tel;
                    vo.TeacherEvaluate = p.TeacherEvaluate;
                    vo.Day = tou.Day;
                    vo.StudentJobUrl = p.StudentJobUrl;
                    vo.TeacherCourseName = tou?.Name;
                    vo.TeacherCourseTime = tou?.TeacherCourseTime;
                    vo.TeacherName = tec.Name;
                    vo.TeacherTel = tec.Tel;
                    vo.FieldName = fi?.Name;
                    vo.TeacherPic = tec.Pic;
                    vo.SchoolName = sc?.Name;
                    vo.CourseVideoUrl = p.CourseVideoUrl;
                    vo.StudentLeave = stuli?.Status.ToString();
                };
                list.Add(vo);
            }
            return list;
        }

        /// <summary>
        /// 微信打卡页面dklist
        /// </summary>
        /// <param name="bo"></param>
        /// <returns></returns>
        public List<StudentCourseClassVO> GetListStudentCourseClassByStudentWX(StudentCourseClassQueryBO bo, int type = 1)
        {
            int totNum = 0;
            var list = new List<StudentCourseClassVO>();

            var whereQl = BuildExpression<M_StudentCourseClass>();

            whereQl = BuildExpression<M_StudentCourseClass>(
                BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.StudentId == bo.StudentId));

            if (bo.Status == "0")
            {
                whereQl = BuildExpression<M_StudentCourseClass>(
                    BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                    BuildLambda<M_StudentCourseClass>(bo.Status, p => p.Status == Int32.Parse(bo.Status) && p.Day >= Int32.Parse(DateTime.Now.ToString("yyyyMMdd"))),
                    BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.StudentId == bo.StudentId));
            }
            if (bo.Status == "2")
            {
                whereQl = BuildExpression<M_StudentCourseClass>(
                    BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                    BuildLambda<M_StudentCourseClass>(bo.Status, p => p.Status == Int32.Parse(bo.Status)),
                    BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.StudentId == bo.StudentId));
            }
            if (bo.Status == "1")
                whereQl = BuildExpression<M_StudentCourseClass>(
                    BuildLambda<M_StudentCourseClass>(bo.TeacherCourseId, p => p.TeacherCourseId == bo.TeacherCourseId),
                    BuildLambda<M_StudentCourseClass>(bo.Status, p => p.Status == 0 && p.Day < Int32.Parse(DateTime.Now.ToString("yyyyMMdd"))),
                    BuildLambda<M_StudentCourseClass>(bo.StudentId, p => p.StudentId == bo.StudentId));
            IEnumerable<M_StudentCourseClass> noumena = null;
            if (bo.PageSize == 0)
            {
                noumena = mServiceDAL.LoadEntities<M_StudentCourseClass>(whereQl).OrderByDescending(p => p.CreateTime);
                totNum = noumena.Count();
            }
            else
            {
                noumena = mServiceDAL.LoadPageEntities(bo.PageIndex * bo.PageSize, bo.PageSize,
                    out totNum, whereQl, false, p => p.CreateTime);
            }
            bo.TotNum = totNum;
            if (!noumena.Any()) return list;
            var teaclist = base.LoadEntities<M_Teacher>(p => p.Id != null).ToList();
            var fields = base.LoadEntities<M_Field>(p => p.Id != null);
            var schools = base.LoadEntities<M_School>(p => p.Id != null);
            var studenlis = base.LoadEntities<M_StudentLeave>(p => p.Id != null);
            var stulist = base.LoadEntities<M_Student>(p => p.Id != null);
            var teachercourlist = base.LoadEntities<M_TeacherCourse>(p => p.Id != null);
            var culist = base.LoadEntities<M_StudentCourse>(p => p.CategoryId == 5 && p.StudentId == bo.StudentId && p.ispay == 1);
            Log.Error(new String[] { "我特曼个到这里来", type.ToString() });
            foreach (var p in noumena)
            {
                var tec = teaclist.FirstOrDefault<M_Teacher>(x => x.StudentId == p.TeacherId);
                var fi = fields.FirstOrDefault<M_Field>(o => o.Id == p.FieldId);
                var sc = schools.FirstOrDefault<M_School>(t => t.Id == p.SChoolId);
                var stuli = studenlis.FirstOrDefault<M_StudentLeave>(x => x.StudentId == p.StudentId & x.StudentCourseClassId == p.Id);
                var stu = stulist.FirstOrDefault<M_Student>(x => x.Id == p.StudentId);
                var tou = teachercourlist.FirstOrDefault<M_TeacherCourse>(x => x.Id == p.TeacherCourseId);
                var cutype = culist.Any<M_StudentCourse>(x => x.Id == p.StudentCourseId);
                if (type == 2)
                {
                    if (cutype)//体验课
                    {
                        Log.Error(new String[] { "我特曼个到这里来2222", type.ToString() + "-" + cutype });
                        var vo = new StudentCourseClassVO();
                        {
                            vo.Id = p.Id;
                            vo.CreateTime = p.CreateTime;
                            vo.TeacherId = p.TeacherId;
                            vo.Status = p.Status == 0 ? GetStatus(p.Status, p.Day) : p.Status;
                            vo.StudentName = stu?.FullName;
                            vo.StudentId = p.StudentId;
                            vo.HeadImgUrl = stu?.HeadImgUrl;
                            vo.NickName = stu?.NickName;
                            vo.JobStatus = p.JobStatus;
                            vo.TeacherCourseId = p.TeacherCourseId;
                            vo.JobEvaluate = p.JobEvaluate;
                            vo.JobName = p.JobName;
                            vo.JobTempVideoId = p.JobTempVideoId;
                            vo.JobTempVideoUrl = p.JobTempVideoUrl;
                            vo.StudentEvaluate = p.StudentEvaluate;
                            vo.StudentStars = p.StudentStars;
                            vo.StudentTel = stu?.Tel;
                            vo.TeacherEvaluate = p.TeacherEvaluate;
                            vo.Day = tou.Day;
                            vo.StudentJobUrl = p.StudentJobUrl;
                            vo.TeacherCourseName = tou?.Name;
                            vo.TeacherCourseTime = tou?.TeacherCourseTime;
                            vo.TeacherName = tec.Name;
                            vo.TeacherTel = tec.Tel;
                            vo.FieldName = fi?.Name;
                            vo.TeacherPic = tec.Pic;
                            vo.SchoolName = sc?.Name;
                            vo.CourseVideoUrl = p.CourseVideoUrl;
                            vo.StudentLeave = stuli?.Status.ToString();
                        };
                        list.Add(vo);
                    }
                }
                else
                {
                    if (!cutype)//体验课
                    {
                        Log.Error(new String[] { "我特曼个到这里来111", type.ToString() + "-" + cutype });
                        var vo = new StudentCourseClassVO();
                        {
                            vo.Id = p.Id;
                            vo.CreateTime = p.CreateTime;
                            vo.TeacherId = p.TeacherId;
                            vo.Status = p.Status == 0 ? GetStatus(p.Status, p.Day) : p.Status;
                            vo.StudentName = stu?.FullName;
                            vo.StudentId = p.StudentId;
                            vo.HeadImgUrl = stu?.HeadImgUrl;
                            vo.NickName = stu?.NickName;
                            vo.JobStatus = p.JobStatus;
                            vo.TeacherCourseId = p.TeacherCourseId;
                            vo.JobEvaluate = p.JobEvaluate;
                            vo.JobName = p.JobName;
                            vo.JobTempVideoId = p.JobTempVideoId;
                            vo.JobTempVideoUrl = p.JobTempVideoUrl;
                            vo.StudentEvaluate = p.StudentEvaluate;
                            vo.StudentStars = p.StudentStars;
                            vo.StudentTel = stu?.Tel;
                            vo.TeacherEvaluate = p.TeacherEvaluate;
                            vo.Day = tou.Day;
                            vo.StudentJobUrl = p.StudentJobUrl;
                            vo.TeacherCourseName = tou?.Name;
                            vo.TeacherCourseTime = tou?.TeacherCourseTime;
                            vo.TeacherName = tec.Name;
                            vo.TeacherTel = tec.Tel;
                            vo.FieldName = fi?.Name;
                            vo.TeacherPic = tec.Pic;
                            vo.SchoolName = sc?.Name;
                            vo.CourseVideoUrl = p.CourseVideoUrl;
                            vo.StudentLeave = stuli?.Status.ToString();
                        };
                        list.Add(vo);
                    }
                }

            }
            return list;
        }

        public int GetStatus(int status, int day)
        {
            if (status == 0)
            {
                if (Int32.Parse(DateTime.Now.ToString("yyyyMMdd")) > day)
                {
                    status = 1;
                }
                return status;
            }
            return status;
        }
        #endregion


        #region 学生头部
        public ClassNumInfoVO GetClassNumInfo(string id)
        {
            var mo = new ClassNumInfoVO()
            {
                AllNum = "0",
                SurplusNum = "0",
                UsedNum = "0"
            };
            int all = 0;
            int sur = 0;
            var list = base.LoadEntities<M_StudentCourse>(p => p.StudentId == id && p.ispay == 1).ToList();
            foreach (var item in list)
            {
                if (item.Status == 3||item.CategoryId==5) continue;
                all += item.ClassTimes;
                sur += item.SurplusClassTimes;
            }
            int userd = all - sur;
            //mo.AllNum = all.ToString();
            //mo.SurplusNum = sur.ToString();
            int a = all % 2;
            int al = all / 2; //总数非体验课
            int sl = sur / 2; //剩余未+体验课
            int s = sur % 2;
            foreach (var item in list)
            {
                if (item.Status == 3) continue;
                if (item.CategoryId == 5)
                {
                    al += item.ClassTimes;
                    sl += item.SurplusClassTimes;
                }
            }
            if (a == 1)
                mo.AllNum = (al).ToString() + ".5";
            else
                mo.AllNum = (al).ToString(); 
            if (s == 1)
                mo.SurplusNum = (sl).ToString() + ".5";
            else
                mo.SurplusNum = (sl).ToString();

            var ul = al - sl; 
            mo.UsedNum = ul.ToString();
            if (a == 1 && s == 0)
                mo.UsedNum = ul.ToString() + ".5";
            else if (a == 0 && s == 1)
                mo.UsedNum = (ul - 1).ToString() + ".5";
            return mo;
        }
        #endregion

        #region
        /// <summary>
        /// 更新订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateStudentIsPay(string id)
        {
            var model = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == id);
            if (model.ispay == 1) { return true; }
            model.ispay = 1;
            return base.Update(model)>0;

        }
        #endregion


        public AmountBo TransferStudentCourse(StudentCour3Request modelBo)
        {
            modelBo.SurplusClassTimes = (Convert.ToDouble(modelBo.SurplusClassTimes) * 2).ToString("0");
            AmountBo amountBo = new AmountBo();
            var amount = 0;
            //查询原来的订课课程
            var studentCourse = base.LoadFirstOrDefault<M_StudentCourse>(p => p.Id == modelBo.Id);
            if (studentCourse.CourseId == modelBo.CourseId)
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), "请选择不同的课时！");
            }
            //查询旧课程名称
            var old_course = base.LoadFirstOrDefault<M_Course>(p => p.Id == studentCourse.CourseId);
            //查询现在的课程单价，计算出价格
            var course = base.LoadFirstOrDefault<M_Course>(p => p.Id == modelBo.CourseId);
            String unitPrice = (Convert.ToDouble(course.Price) / Convert.ToDouble(course.ClassTimes)).ToString("0.00");
            double totalPrice = Convert.ToDouble(unitPrice) * Convert.ToInt32(modelBo.SurplusClassTimes) / 100;
            //新增订课课程
            M_StudentCourse queryModel = new M_StudentCourse();
            queryModel.Id = Utils.GetGuid();
            queryModel.CreateTime = DateTime.Now;
            queryModel.CreateUserId = studentCourse.CreateUserId;
            queryModel.Deleted = 0;
            queryModel.AgeCategoryId = modelBo.AgeCategoryId;
            queryModel.CategoryId = modelBo.CategoryId;
            queryModel.CourseId = modelBo.CourseId;
            queryModel.StudentId = studentCourse.StudentId;
            queryModel.Price = totalPrice.ToString();
            queryModel.ClassTimes = Convert.ToInt32(modelBo.SurplusClassTimes);
            queryModel.SurplusClassTimes = Convert.ToInt32(modelBo.SurplusClassTimes);
            queryModel.ispay = 1;
            queryModel.Name = studentCourse.Name;
            queryModel.Tel = studentCourse.Tel;
            queryModel.UserCategoryId = studentCourse.UserCategoryId;
            queryModel.TennisId = studentCourse.TennisId;
            queryModel.Status = 0;
            amount = mServiceDAL.Create(queryModel);
            //添加转课记录
            M_TransferCourse transferCourse = new M_TransferCourse();
            transferCourse.Id = Utils.GetGuid();
            transferCourse.StudentId = studentCourse.StudentId;
            transferCourse.OldCourseId = studentCourse.Id;
            transferCourse.NewCourseId = queryModel.Id;
            transferCourse.OldSurplusClassTimes = studentCourse.SurplusClassTimes;
            transferCourse.NewSurplusClassTimes = Convert.ToInt32(modelBo.SurplusClassTimes);
            transferCourse.CreateUserId = modelBo.UserId;
            transferCourse.CreateTime = DateTime.Now;
            mServiceDAL.Create(transferCourse);
            //将原来的订课课程课时变成0
            studentCourse.SurplusClassTimes = 0;
            studentCourse.UpdateTime = DateTime.Now;
            studentCourse.UpdateUserId = modelBo.UserId;
            amount = mServiceDAL.Update(studentCourse);
            //发送消息给学员
            if (!string.IsNullOrEmpty(studentCourse.Tel))
            {
                Task.Factory.StartNew(() => WxUserLibApi.sendTransferStudentCourse(studentCourse.Tel, old_course.Title, course.Title,(Convert.ToDouble(queryModel.ClassTimes)/2).ToString()));
            }
            return amountBo;
        }
    }
}

using Aspose.Words;
using school.BLL.Factory;
using school.Common.Tools;
using school.Model.BO;
using school.Model.DAO;
using school.Model.TO.Response;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace school.Api.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string u, string pwd, string vcode, string redirect)
        {
            var msg = "用户名或者密码错误";
            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(pwd))
                msg = "用户名密码不能为空";
            var user = SessionFactory.SessionService.mTeacherBLL.login(u, pwd);
            if (!string.IsNullOrEmpty(user))
            {
                Session["userKey"] = "ccc";
                Session["userName"] = u;
                return Redirect("/admin/Index");
            }
            ViewBag.msg = msg;
            return View();
        }
        // GET: Home
        public ActionResult Index()
        {
            if (Session["userKey"] == null)
                return Redirect("/admin/Login");
            return View();
        }
        public ActionResult graph_echarts()
        {
            return View();
        }

        public ActionResult Index2()
        {
            var sss = Session["userS"];
            var sdasd = sss;
            return View();
        }

        #region
        public ActionResult studentList()
        {
            List<M_Student> mode = SessionFactory.SessionService.mstudentBLL.GetList(new PageInfo { PageIndex = 0, PageSize = 20 });
            return View(mode);
        }

        public ActionResult student(string id)
        {
            M_Student mode = SessionFactory.SessionService.mstudentBLL.GetOne(id);

            return View(mode);
            //List<M_Student> mode = SessionFactory.SessionService.mstudentBLL.GetList(new PageInfo { PageIndex = 0, PageSize = 20 });
            //return View(mode);
        }
        #endregion
        #region

        public ActionResult Menu()
        {
            M_Menu mode = SessionFactory.SessionService.mCourseBLL.GetOneMenu();
            return View(mode);
        }
        #endregion


        #region
        public ActionResult PlanList()
        {
            List<TeachingPlanVO> mode = SessionFactory.SessionService.mTeacherBLL.GetTeachingPlanList(new IdListBO { });
            return View(mode);
        }

        public ActionResult Plan(string id)
        {
            TeachingPlanVO mode = SessionFactory.SessionService.mTeacherBLL.GetTeachingPlan(id);
            return View(mode);
        }

        public ActionResult PlanPri(string id)
        {

            var teacherCourse = SessionFactory.SessionService.mTeacherBLL.GetTeacherCourseOne(id);
            TeachingPlanVO mode = SessionFactory.SessionService.mTeacherBLL.GetTeachingPlan(teacherCourse.TeachingPlanId);
            var list = SessionFactory.SessionService.mTeacherBLL.GetTeachingPlanInfoList(new IdListBO { Id = teacherCourse.TeachingPlanId });
            TeachingPlanShowMVCVO model = new TeachingPlanShowMVCVO();

            ObjectHelper.AutoMapping(mode, model);
            model.ClassName = teacherCourse.Name;
            model.TeacherCourseTime = teacherCourse.TeacherCourseTime;
            model.Day = teacherCourse.Day;
            List<planItemMVCVO> lists = new List<planItemMVCVO>();
            var dic = SessionFactory.SessionService.mTeacherBLL.GetDicAllList();

            var dict = new Dictionary<string, string>();
            int xnum = 1;int ttnum = 1;
            foreach (var item in list)
            {
                planItemMVCVO vo = new planItemMVCVO();
                ttnum = 1;
                dict.Add("&时间" + xnum + "&", item.TimeLength + "-" + item.TimeSlot);
                ObjectHelper.AutoMapping(item, vo);
                var lis = item.CategoryItemId.Split(',');
                var ss = ""; var point = "";
                foreach (var it in lis)
                {
                    var sx = dic.FirstOrDefault<M_Dic>(x => x.Id == it);

                    if (!string.IsNullOrEmpty(sx?.Name))
                    {
                        ss += ttnum + "." + sx?.Name + " \r\n ";
                    }
                    if(!string.IsNullOrEmpty(sx?.Points))
                    point += ttnum + "." + sx?.Name+ ":" + sx?.Points + " \r\n ";
                    ttnum++;
                }
                dict.Add("&内容" + xnum + "&", GetNoHTMLString(ss));
                dict.Add("&教学" + xnum + "&", GetNoHTMLString(point));
                xnum++;
                vo.CategoryItemName = ss;
                vo.Points = point;
                lists.Add(vo);
            }
            model.ItemList = lists; 

            dict.Add("&名称&", model.Title);
            dict.Add("&日期&", model.Day.ToString());
            dict.Add("&时间时长&", model.TeacherCourseTime);
            dict.Add("&人数&", "");
            dict.Add("&需要装备&", model.Equip);
            dict.Add("&目标&", model.Target);  
            //使用书签操作
            var u = Server.MapPath("/word/c.doc");
            Document doc = new Document(u);
            foreach (var key in dict.Keys)
            {
                var repStr = string.Format("&{0}&", key);
                doc.Range.Replace(repStr, dict[key]);
            }
            var dc2 = "/word/" + DateTime.Now.ToString("yyMMddHHmmss") + ".doc";
            var u2 = Server.MapPath(dc2);
            doc.Save(u2);//也可以保存为1.doc 兼容03-07  
            return Redirect(dc2);
        }
        public static string GetNoHTMLString(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            
            //Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Replace("t;/div&gt;", "");
            Htmlstring = Htmlstring.Replace("<div>", "");
            Htmlstring = Htmlstring.Replace("</div>", "");
            Htmlstring = Htmlstring.Replace("<br>", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring.Trim();
        }

        #endregion

        #region
        public ActionResult VideoList()
        {
            List<VideoVO> mode = SessionFactory.SessionService.mTeacherBLL.GetVideoList(new IdListBO { });
            return View(mode);
        }
        public ActionResult video(string id)
        {
            VideoVO mode = new VideoVO();
            if (string.IsNullOrEmpty(id))
            {
                return View(mode);
            }
            mode = SessionFactory.SessionService.mTeacherBLL.GetVideo(id);
            ViewBag.Id = id;
            return View(mode);
        }
        #endregion
        #region

        public ActionResult ShareList()
        {
            List<ShareInfoVO> mode = SessionFactory.SessionService.mshareInfoBLL.GetList(new PageInfo { });
            return View(mode);
        }
        public ActionResult share(string id)
        {
            M_ShareInfo mode = SessionFactory.SessionService.mshareInfoBLL.GetOne(id);
            return View(mode);
        }
        #endregion
        #region
        public ActionResult TeacherList()
        {
            List<TeacherInfoVO> mode = SessionFactory.SessionService.mTeacherBLL.GetList(new TeacherListBO { });
            return View(mode);
        }
        public ActionResult Teacher(string id)
        {
            TeacherInfoVO mode = SessionFactory.SessionService.mTeacherBLL.GetOne(id);
            ViewBag.Id = id;
            return View(mode);
        }
        #endregion


        #region 学生课程
        public ActionResult StudentCourseList(string id = "", string studentId = "")
        {
            List<StudentCourseVO> mode = SessionFactory.SessionService.mStudentCourseBLL.GetListCourse(new StudentCourseListBO { Status = id, UserId = studentId });
            return View(mode);
        }
        public ActionResult StudentCourse(string id)
        {
            StudentCourseOneVO mode = SessionFactory.SessionService.mStudentCourseBLL.GetOneCourse(id);
            return View(mode);
        }

        public ActionResult TransferCourse(string id)
        {
            StudentCourseOneVO mode = SessionFactory.SessionService.mStudentCourseBLL.GetOneCourse(id);
            return View(mode);
        }
        #endregion

        #region 课程
        public ActionResult CourseList(string id = "")
        {
            List<M_Course> mode = SessionFactory.SessionService.mCourseBLL.GetList(new CourseListBO { });
            return View(mode);
        }
        public ActionResult Course(string id)
        {
            M_Course mode = new M_Course();
            if (!string.IsNullOrEmpty(id))
            {
                mode = SessionFactory.SessionService.mCourseBLL.GetOne(id);
            }
            ViewBag.Id = mode.Id;
            return View(mode);
        }
        #endregion
        #region 课表
        public ActionResult TeacherCourseList(string id = "")
        {
            List<M_TeacherCourse> mode = SessionFactory.SessionService.mTeacherBLL.GetTeacherCourseList();
            return View(mode);
        }
        public ActionResult TeacherCourse(string id)
        {
            M_TeacherCourse mode = new M_TeacherCourse();
            if (!string.IsNullOrEmpty(id))
            {
                mode = SessionFactory.SessionService.mTeacherBLL.GetTeacherCourseOne(id);
            }
            ViewBag.Id = mode.Id;
            return View(mode);
        }
        #endregion

        #region 订场
        public ActionResult OrderList(string id = "")
        {
            List<OrderVO> mode = SessionFactory.SessionService.mstudentBLL.GetOrderList(new IdStatusListBO { Status = "1,2" });
            return View(mode);
        }
        public ActionResult Order(string id)
        {
            OrderVO mode = new OrderVO();
            if (!string.IsNullOrEmpty(id))
            {
                mode = SessionFactory.SessionService.mstudentBLL.GetOrder(id);
            }
            ViewBag.Id = mode.Id;
            return View(mode);
        }
        #endregion

        #region  
        public ActionResult SchoolList(string id = "")
        {
            List<M_School> mode = SessionFactory.SessionService.mFieldBLL.GetListSchool();
            return View(mode);
        }
        public ActionResult School(string id)
        {
            M_School mode = new M_School();
            if (!string.IsNullOrEmpty(id))
            {
                mode = SessionFactory.SessionService.mFieldBLL.GetSchool(id);
            }
            ViewBag.Id = mode.Id;
            return View(mode);
        }
        #endregion
        #region  场地
        public ActionResult FieldList(string id = "")
        {
            List<M_Field> mode = SessionFactory.SessionService.mFieldBLL.GetList(id);
            ViewBag.SchoolId = id;
            return View(mode);

        }
        public ActionResult Field(string id, string schoolId)
        {
            M_Field mode = new M_Field();
            if (!string.IsNullOrEmpty(id))
            {
                mode = SessionFactory.SessionService.mFieldBLL.GetOne(id);
                schoolId = mode.SchoolId;
            }
            ViewBag.Id = mode.Id;
            ViewBag.SchoolId = schoolId;
            return View(mode);
        }
        #endregion

        #region  字典
        public ActionResult DicList(string id = "")
        {
            List<M_Dic> mode = SessionFactory.SessionService.mTeacherBLL.GetDicParentList(id);
            ViewBag.ParId = "";
            return View(mode);

        }
        public ActionResult Dic(string id)
        {
            DicItemVO mode = new DicItemVO();
            if (!string.IsNullOrEmpty(id))
            {
                mode = SessionFactory.SessionService.mTeacherBLL.GetDicOne(id);

            }
            ViewBag.Id = id;
            return View(mode);
        }

        public ActionResult DicItemList(string id = "")
        {
            List<M_Dic> mode = SessionFactory.SessionService.mTeacherBLL.GetDicParentList(id);
            ViewBag.ParId = id;
            return View(mode);

        }
        public ActionResult DicItem(string id, string partId = "")
        {
            DicItemVO2 mode = new DicItemVO2();

            var que = new CategoryListAllVO();

            var vilist = SessionFactory.SessionService.mTeacherBLL.GetVideoList(new IdListBO { });
            mode.VideoList = vilist;

            ViewBag.Id = id;
            ViewBag.ParId = partId; 

            if (!string.IsNullOrEmpty(id))
            {
               var mode2 = SessionFactory.SessionService.mTeacherBLL.GetDicOne(id);
                ObjectHelper.AutoMapping(mode2, mode);
                ViewBag.ParId = mode2.PaterId;
                partId = mode2.PaterId;
            }
            if (!string.IsNullOrEmpty(partId))
            {
                var bmodel = SessionFactory.SessionService.mTeacherBLL.GetDicOne(partId);
                if (bmodel.Sort == 10)
                {
                    que = SessionFactory.SessionService.mCourseBLL.GetCatergoryAll("1");
                }
                if (bmodel.Sort == 9)
                {
                    que = SessionFactory.SessionService.mCourseBLL.GetCatergoryAll("4");
                }
            }
            mode.OneList = que.OneList;
            mode.TwoList = que.TwoList;
            mode.ThrList = que.ThrList; 
            return View(mode);
        }




        #endregion

        #region  学生课程
        public ActionResult StudentClassList()
        {
            List<StudentCourseClassVO> mode = SessionFactory.SessionService.mStudentCourseBLL.GetListStudentCourseClass(new StudentCourseClassQueryBO { });
            return View(mode);

        }
        public ActionResult StudentClass(string id)
        {
            StudentCourseClassVO mode = new StudentCourseClassVO();
            if (!string.IsNullOrEmpty(id))
            {
                mode = SessionFactory.SessionService.mStudentCourseBLL.GetOneStudentCourseClass(id);
            }
            return View(mode);
        }
        #endregion


        #region  学生卡卷
        public ActionResult StudentKjList(string id)
        {
            List<StudentKjVO> mode = SessionFactory.SessionService.mstudentBLL.GetKjList(new IdStatusListBO { Id = id });
            return View(mode);

        }

        #endregion

        #region  学生卡卷
        public ActionResult EquList(string id)
        {
            List<M_Equ> mode = SessionFactory.SessionService.mCourseBLL.GetEquListUserAdmin(id);
            return View(mode);

        }
        public ActionResult EquOne(string id)
        {
            M_Equ mode = SessionFactory.SessionService.mCourseBLL.GetEquOneUserAdmin(id);
            return View(mode);

        }
        #endregion

        #region  分类


        public ActionResult CategoryMain()
        {
            return View();
        }
         

        public ActionResult CategoryList(string id = "")
        {
            List<M_Category> mode = SessionFactory.SessionService.mCourseBLL.GetCatergoryList(id);
            ViewBag.ParId = id;
            return View(mode);

        } 

        public ActionResult CategoryParentList(string category)
        {
            List<M_Category> mode = SessionFactory.SessionService.mCourseBLL.GetCatergoryListByCategoryOne(category);
            return View(mode);

        }
        public ActionResult Category(string id)
        {
            M_Category mode = new M_Category();
            ViewBag.Id = id;
            if (!string.IsNullOrEmpty(id))
            {
                mode = SessionFactory.SessionService.mCourseBLL.GetCatergoryOne(id);  
            } 
            return View(mode);
        }
         



        #endregion

        #region 场地时间价格
        public ActionResult FilePric()
        {

            M_FilePrice mode = SessionFactory.SessionService.mFieldBLL.GetFilePrice();
            return View(mode); 
        }
        #endregion

  
    }
}
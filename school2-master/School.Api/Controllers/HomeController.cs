using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using school.BLL;
using school.BLL.Factory;
using school.Common;
using school.Common.Tools;
using school.Model.BO;
using school.Model.DAO;
using school.Model.TO.Request;
using school.Model.VO.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using Yidu.Common.Tools;


namespace school.Api.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {


            return View();
        }


        public ActionResult Index2()
        {
            var sss = Session["userS"];
            var sdasd = sss;
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Login2(string id)
        {
            Session["userKey"] = "ccc";
            ViewBag.Id = id;
            return View();
        }
        public ActionResult Loginth(string id)
        {
            Session["userKey"] = "ccc";
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Login3(string id)
        {
            var dasd = id;
            return View();
        }

        public ActionResult KJ()
        {
            var noce = CreateNonce_str();
            ViewBag.Str = noce;
            var ti = CreateTimestamp();
            ViewBag.Ct = ti;
            ViewBag.Sd = SHA1Helper.GetSignature(SessionFactory.SessionService.mWtokenBLL.GetTicket(), noce, ti, Config.DomainName + Request.Url.PathAndQuery.ToString());
            return View();
        }

        private string[] strs = new string[]
                                 {
                                  "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                                  "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
                                 };
        /// <summary>
        /// 创建随机字符串
        /// </summary>
        /// <returns></returns>
        public string CreateNonce_str()
        {
            Random r = new Random();
            var sb = new StringBuilder();
            var length = strs.Length;
            for (int i = 0; i < 15; i++)
            {
                sb.Append(strs[r.Next(length - 1)]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 创建时间戳
        /// </summary>
        /// <returns></returns>
        public long CreateTimestamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }



        /// <summary>
        /// 抽奖
        /// </summary>
        /// <returns></returns>
        public ActionResult Luck(string id)
        {
            if (Session["userKey"] == null)
            {
                return RedirectToAction("Login2", "Home", new { id = id });
            }
            WxUserBO v = (WxUserBO)Common.Cache.CacheHelper.Memcached.Get(Session["userKey"].ToString());
            M_StudentPrize mo = new M_StudentPrize()
            {
                ShareInfoId = id,
                CreateUserId = v.UnionId,
                UnionId = v.UnionId
            };
            var bo = SessionFactory.SessionService.mStudentPrizeBLL.CreateOrUpdate(mo, 1, 0);

            ViewBag.Uid = Session["userKey"];
            ViewBag.Id = id;
            ViewBag.Pr = bo.Prize;
            ViewBag.Prn = bo.PrizeNum;
            #region 微信js-sdk
            var noce = CreateNonce_str();
            ViewBag.Str = noce;
            var ti = CreateTimestamp();
            ViewBag.Ct = ti;
            ViewBag.Sd = SHA1Helper.GetSignature(SessionFactory.SessionService.mWtokenBLL.GetTicket(), noce, ti, Config.DomainName + Request.Url.PathAndQuery.ToString());
            #endregion
            //var userBO = (WxUserBO)CacheHelper.Memcached.Get(Session["userKey"].ToString());
            //var amount= SessionFactory.SessionService.mStudentPrizeBLL.CreateOrUpdate(new M_StudentPrize { Status=0, ShareInfoId =id, UnionId = userBO.UnionId, CreateUserId = userBO.UnionId }, 1);
            return View();
        }


        public ActionResult getCode2(string code)
        {
            ViewBag.Code = code;
            //var ouser = WxUserLibApi.GetAccessToken(code);
            //var tuser = WxUserLibApi.GetRefreshAccessToken(ouser.refresh_token);
            //var users = WxUserLibApi.GetRefreshAccessToken(tuser.access_token, ouser.openId);
            //M_Student bo = new M_Student();
            //ObjectHelper.AutoMapping(users, bo);
            //var s = SessionFactory.SessionService.mstudentBLL.CreateOrUpdate(bo);
            return View();
        }


        public ActionResult getCode(string code)
        {
            var ouser = WxUserLibApi.GetAccessToken(code);
            var tuser = WxUserLibApi.GetRefreshAccessToken(ouser.refresh_token);
            var users = WxUserLibApi.GetRefreshAccessToken(tuser.access_token, ouser.openId);
            var stu = SessionFactory.SessionService.mstudentBLL.GetOneByUionId(users.unionid);
            if (!string.IsNullOrEmpty(stu.Id))
            {
                var sessionkey = SessionFactory.SessionService.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = users.unionid, UserId = stu.Id });
                var user = new WxUserBO { Id = stu.Id, OpenId = stu.OpenId, UserKey = stu.Id, UserType = stu.Type, UnionId = users.unionid, HeadImgUrl = users.headimgurl, NickName = users.nickname };
                Common.Cache.CacheHelper.Memcached.Set(sessionkey, user);
                Session["userKey"] = sessionkey;
                return RedirectToAction("upload", "Home");
            }
            StudentBO bo = new StudentBO();
            ObjectHelper.AutoMapping(users, bo);
            var s = SessionFactory.SessionService.mstudentBLL.CreateOrUpdate(bo);

            var sessionkey2 = SessionFactory.SessionService.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = users.unionid, UserId = s });
            var user2 = new WxUserBO { Id = s, OpenId = ouser.openId, UserKey = s, UserType = 1, UnionId = users.unionid, HeadImgUrl = users.headimgurl, NickName = users.nickname };
            Common.Cache.CacheHelper.Memcached.Set(sessionkey2, user2);
            Session["userKey"] = sessionkey2;
            Session["openKey"] = ouser.openId;
            return RedirectToAction("upload", "Home");
        }


        public ActionResult getCodeSec(string state, string code)
        {
            var ouser = WxUserLibApi.GetAccessToken(code);
            var token = SessionFactory.SessionService.mWtokenBLL.Get();
            var users = WxUserLibApi.GetUnionID(token, ouser.openId);
            Log.Error(new String[] { "users", users.ToJson() });
            var stu = SessionFactory.SessionService.mstudentBLL.GetOneByUionId(users.unionid);
            if (!string.IsNullOrEmpty(stu.Id))
            {
                var sessionkey = SessionFactory.SessionService.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = users.unionid, UserId = stu.Id });
                var user = new WxUserBO { Id = stu.Id, OpenId = ouser.openId, UserKey = stu.Id, UserType = stu.Type, UnionId = users.unionid, HeadImgUrl = users.headimgurl, NickName = users.nickname };
        
                Common.Cache.CacheHelper.Memcached.Set(sessionkey, user);
                Session["userKey"] = sessionkey;
                return RedirectToAction("signIn", "Home", new { state = state });
            }
            StudentBO bo = new StudentBO();
            ObjectHelper.AutoMapping(users, bo);
            var s = SessionFactory.SessionService.mstudentBLL.CreateOrUpdate(bo);

            var sessionkey2 = SessionFactory.SessionService.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = users.unionid, UserId = s });
            var user2 = new WxUserBO { Id = s, OpenId = ouser.openId, UserKey = s, UserType = 1, UnionId = users.unionid, HeadImgUrl = users.headimgurl, NickName = users.nickname };
            Common.Cache.CacheHelper.Memcached.Set(sessionkey2, user2);
            Session["userKey"] = sessionkey2;
            return RedirectToAction("signIn", "Home", new { state = state });
        }



        public ActionResult getCodeThr(string state, string code)
        {
            var ouser = WxUserLibApi.GetAccessToken(code);
            var token = SessionFactory.SessionService.mWtokenBLL.Get();
            var users = WxUserLibApi.GetUnionID(token, ouser.openId);
            Log.Error(new String[] { "users", users.ToJson() });
            var stu = SessionFactory.SessionService.mstudentBLL.GetOneByUionId(users.unionid);
            if (!string.IsNullOrEmpty(stu.Id))
            {
                var sessionkey = SessionFactory.SessionService.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = users.unionid, UserId = stu.Id });
                var user = new WxUserBO { Id = stu.Id, OpenId = ouser.openId, UserKey = stu.Id, UserType = stu.Type, UnionId = users.unionid, HeadImgUrl = users.headimgurl, NickName = users.nickname };
                Common.Cache.CacheHelper.Memcached.Set(sessionkey, user);
                Session["userKey"] = sessionkey;
                return RedirectToAction("dkList", "Home");
            }
            StudentBO bo = new StudentBO();
            ObjectHelper.AutoMapping(users, bo);
            var s = SessionFactory.SessionService.mstudentBLL.CreateOrUpdate(bo);

            var sessionkey2 = SessionFactory.SessionService.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = users.unionid, UserId = s });
            var user2 = new WxUserBO { Id = s, OpenId = ouser.openId, UserKey = s, UserType = 1, UnionId = users.unionid, HeadImgUrl = users.headimgurl, NickName = users.nickname };
            Common.Cache.CacheHelper.Memcached.Set(sessionkey2, user2);
            Session["userKey"] = sessionkey2;
            return RedirectToAction("dkList", "Home");
        }

        public ActionResult Upload()
        {
            if (Session["userKey"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var id = Session["userKey"];
            ViewBag.Id = id;
            return View();
        }

        public ActionResult signIn(string state)
        {
            //if (Session["userKey"] == null)
            //{
            //    return RedirectToAction("Login2", "Home", new { id = state });
            //}
            //ViewBag.Uid = Session["userKey"];
            var model = SessionFactory.SessionService.mshareInfoBLL.GetOne(state);
            var usermodel = SessionFactory.SessionService.mstudentBLL.GetOneByUionId(model.UnionId);
            //WxUserBO v = (WxUserBO)Common.Cache.CacheHelper.Memcached.Get(Session["userKey"].ToString());
            //var ok = model.UnionId == v.UnionId ? 1 : 0;
            ViewBag.IsOwn = 1;
            ViewBag.Id = state;
            ViewBag.Url = model.Url;
            ViewBag.NickName = usermodel.NickName;
            ViewBag.Pic = usermodel.HeadImgUrl;
            ViewBag.Ctime = Convert.ToDateTime(model.CreateTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

            var Zcount = SessionFactory.SessionService.mshareInfoBLL.GetNum(state);
            ViewBag.Dz = Zcount;
            var Xlcount = SessionFactory.SessionService.mshareInfoBLL.GetXLNum(model.UnionId,model.Types);
            ViewBag.XLCount = Xlcount;
            ViewBag.Types = model.Types;

            #region 微信js-sdk
            var noce = CreateNonce_str();
            ViewBag.Str = noce;
            var ti = CreateTimestamp();
            ViewBag.Ct = ti;
            ViewBag.Sd = SHA1Helper.GetSignature(SessionFactory.SessionService.mWtokenBLL.GetTicket(), noce, ti, Config.DomainName + Request.Url.PathAndQuery.ToString());
            #endregion


            return View();
        }


        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult Register(string id)
        {
            if (Session["userKey"] == null)
            {
                return RedirectToAction("Login2", "Home", new { id = id });
            }
            var model = SessionFactory.SessionService.mshareInfoBLL.GetOne(id);
            if (model.FtimeStemp == null)
                return RedirectToAction("Login2", "Home", new { id = id });
            var student = SessionFactory.SessionService.mstudentBLL.GetOneByUionId(model.UnionId);
            if (student.FtimeStemp == null)
                return RedirectToAction("Login2", "Home", new { id = id });
            ViewBag.NickName = student.NickName;
            ViewBag.Price = 50;
            ViewBag.Uid = Session["userKey"];
            ViewBag.Id = id;

            #region 微信js-sdk
            var noce = CreateNonce_str();
            ViewBag.Str = noce;
            var ti = CreateTimestamp();
            ViewBag.Ct = ti;
            ViewBag.Sd = SHA1Helper.GetSignature(SessionFactory.SessionService.mWtokenBLL.GetTicket(), noce, ti, Config.DomainName + Request.Url.PathAndQuery.ToString());
            #endregion
            return View();
        }


        public ActionResult dkList(int id = 1, int page = 0)
        {
            if (Session["userKey"] == null)
            {
                return RedirectToAction("Login3", "Home");
            }
            ViewBag.Uid = Session["userKey"];
            WxUserBO v = (WxUserBO)Common.Cache.CacheHelper.Memcached.Get(Session["userKey"].ToString()); 
            var uid = v.Id; 

            var dkListVO = new DkListVO();
            dkListVO.StudentClassList = new List<Model.VO.StudentCourseClassVO>();
            dkListVO.StudentCourseList = new List<Model.TO.Response.StudentCourseVO>();
            int to = 0;
            if (id == 1 || id == 0)
            {
                var p = new StudentCourseClassQueryBO { StudentId = uid, Flg = 0, PageIndex = page, PageSize = 20 };
                var list = SessionFactory.SessionService.mStudentCourseBLL.GetListStudentCourseClassByStudentWX(p);
                dkListVO.StudentClassList = list;
                to = p.TotNum;
            }
            else
            {
                var p = new StudentCourseClassQueryBO { StudentId = uid, Flg = 0, PageIndex = page, PageSize = 20 };
                var list = SessionFactory.SessionService.mStudentCourseBLL.GetListStudentCourseClassByStudentWX(p,2);
                dkListVO.StudentClassList = list;
                to = p.TotNum;
            }
            ViewBag.Tot = to;
            ViewBag.Id = id;
            return View(dkListVO);
        }
        public ActionResult dkListts(int id = 1, int page = 0)
        {
             
            var uid = "ccca4ac8-a7bc-40a6-80a8-9eba901286f9";
            var dkListVO = new DkListVO();
            dkListVO.StudentClassList = new List<Model.VO.StudentCourseClassVO>();
            dkListVO.StudentCourseList = new List<Model.TO.Response.StudentCourseVO>();
            int to = 0;
            if (id == 1 || id == 0)
            {
                var p = new StudentCourseClassQueryBO { StudentId = uid, Flg = 0, PageIndex = page, PageSize = 20 };
                var list = SessionFactory.SessionService.mStudentCourseBLL.GetListStudentCourseClassByStudent(p);
                dkListVO.StudentClassList = list;
                to = p.TotNum;
            }
            else
            {
                var p = new StudentCourseListBO { Normal = "1", IsStudent = true, UserId = uid, PageIndex = page, PageSize = 20 };
                var list = SessionFactory.SessionService.mStudentCourseBLL.GetListCourse(p);
                dkListVO.StudentCourseList = list;
                to = p.TotNum;
            }
            ViewBag.Tot = to;
            ViewBag.Id = id;
            return View(dkListVO);
        }
        public ActionResult UploadJob(string id,string type)
        {
            if (Session["userKey"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var uid = Session["userKey"];
            ViewBag.type = type;
            ViewBag.Uid = uid;
            ViewBag.Id = id;
            return View();
        }
    }
}
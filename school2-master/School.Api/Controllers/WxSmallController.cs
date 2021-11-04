using school.API.Filter;
using school.BLL;
using school.Common;
using school.Common.Tools;
using school.Model;
using school.Model.BO;
using school.Model.BO.User;
using school.Model.Enum;
using school.Model.TO;
using school.Model.TO.Request;
using school.Model.TO.Response;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace school.Api
{
    public class WxSmallController : BaseController
    {
        

        #region 新的


        #region 登录

        /// <summary>
        /// 用户登录Code
        /// </summary>
        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<WxSmallLgResponse> LoginUnAuth(string code)
        {
            if (string.IsNullOrEmpty(code)) return ApiResultHelper.SubError<WxSmallLgResponse>(R("v_RequireName"));
            var res = new WxSmallLgResponse();
            var users = WxUserLibApi.Login(new WxLoginRequest { Code = code });
            var stu = mSession.mstudentBLL.GetOneBySmOpenId(users.openid);
            var sbo = new StuBO();
            if (!string.IsNullOrEmpty(stu.Id))
            {
                var sessionkey = mSession.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = stu.UnionId, UserId = stu.Id });
                var user = new WxUserBO { Id = stu.Id, OpenId = stu.OpenId, UserKey = stu.Id, UserType = stu.Type, UnionId = stu.UnionId, HeadImgUrl = stu.HeadImgUrl, NickName = stu.NickName };
                Common.Cache.CacheHelper.Memcached.Set(sessionkey, user);
                Log.Error(new String[] { "登录", user.ToJson() });
                stu.UnionId = "";
                stu.OpenId = "";

                sbo.IsLogin = true;
                sbo.UserKey = sessionkey;
                sbo.UnionId = "";
                sbo.OpenId = "";
                sbo.SmOpenId = "";
                ObjectHelper.AutoMapping(stu, sbo);
                res.Model = sbo;
                return ApiResultHelper.Success<WxSmallLgResponse>(res);
            }
            sbo.IsLogin = false;
            res.Model = sbo;

            return ApiResultHelper.Success<WxSmallLgResponse>(res);
        }



        /// <summary>
        /// 用户登录
        /// </summary>
        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<WxSmallLgResponse> Login([FromBody] WxLoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Code)) return ApiResultHelper.SubError<WxSmallLgResponse>(R("v_RequireName"));
            var res = new WxSmallLgResponse();
            var users = WxUserLibApi.Login(request);
            var stu = mSession.mstudentBLL.GetOneByUionId(users.unionid);
            var sbo = new StuBO();
            if (!string.IsNullOrEmpty(stu.Id))
            {
                var sessionkey = mSession.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = users.unionid, UserId = stu.Id });
                var user = new WxUserBO { Id = stu.Id, OpenId = stu.OpenId, UserKey = stu.Id, UserType = stu.Type, UnionId = users.unionid, HeadImgUrl = request.HeadImgUrl, NickName = request.NickName };
                Common.Cache.CacheHelper.Memcached.Set(sessionkey, user);
                Log.Error(new String[] { "登录", user.ToJson() });
                stu.UnionId = "";
                stu.OpenId = "";


                sbo.IsLogin = true;
                sbo.UserKey = sessionkey;
                ObjectHelper.AutoMapping(stu, sbo);

                res.Model = sbo;
                sbo.UnionId = "";
                sbo.OpenId = "";
                sbo.SmOpenId = "";


                return ApiResultHelper.Success<WxSmallLgResponse>(res);
            }
            StudentBO bo = new StudentBO();
            ObjectHelper.AutoMapping(users, bo);
            bo.NickName = request.NickName;
            bo.UnionId = users.unionid;
            bo.HeadImgUrl = request.HeadImgUrl;
            bo.SmOpenId = users.openid;
            var s = mSession.mstudentBLL.CreateOrUpdate(bo);
            var sessionkey2 = mSession.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = users.unionid, UserId = s });
            var user2 = new WxUserBO { Id = s, OpenId = stu.OpenId, UserKey = s, UserType = 1, UnionId = users.unionid, HeadImgUrl = request.HeadImgUrl, NickName = request.NickName };
            Common.Cache.CacheHelper.Memcached.Set(sessionkey2, user2);
            Log.Error(new String[] { "登录", user2.ToJson() });
            stu.UnionId = "";
            stu.OpenId = "";

            sbo.IsLogin = true;
            sbo.UserKey = sessionkey2;
            ObjectHelper.AutoMapping(bo, sbo);
            sbo.UnionId = "";
            sbo.OpenId = "";
            sbo.SmOpenId = "";
            res.Model = sbo;
            return ApiResultHelper.Success<WxSmallLgResponse>(res);
        }

        #region
        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<WxSmallLgResponse> LoginUnionId([FromBody] WxLoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Code)) return ApiResultHelper.SubError<WxSmallLgResponse>(R("v_RequireName"));
            var res = new WxSmallLgResponse();
            var users = WxUserLibApi.GetMiniAppUserUnionID(request.Code, request.EncryptedData, request.Iv);
            var stu = mSession.mstudentBLL.GetOneByUionId(users.unionid);
            var sbo = new StuBO();
            if (!string.IsNullOrEmpty(stu.Id))
            {
                var sessionkey = mSession.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = users.unionid, UserId = stu.Id });
                var user = new WxUserBO { Id = stu.Id, OpenId = stu.OpenId, UserKey = stu.Id, UserType = stu.Type, UnionId = users.unionid, HeadImgUrl = stu.HeadImgUrl, NickName = stu.NickName, SmOpenId = stu.OpenId };
                Common.Cache.CacheHelper.Memcached.Set(sessionkey, user);
                stu.UnionId = "";
                stu.OpenId = "";
                sbo.IsLogin = true;
                sbo.UserKey = sessionkey;
                mSession.mstudentBLL.UpdateStudentSmOpenId(users.unionid, users.openid);
                ObjectHelper.AutoMapping(stu, sbo);
                sbo.SessionKey = users.SessionKey;
                res.Model = sbo;
                sbo.UnionId = "";
                sbo.OpenId = "";
                sbo.SmOpenId = "";


                return ApiResultHelper.Success<WxSmallLgResponse>(res);
            }
            StudentBO bo = new StudentBO();
            ObjectHelper.AutoMapping(users, bo);
            bo.NickName = users.nickName;
            bo.UnionId = users.unionid;
            bo.HeadImgUrl = users.avatarUrl;
            bo.SmOpenId = users.openid;
            var s = mSession.mstudentBLL.CreateOrUpdate(bo);
            var sessionkey2 = mSession.mWloginKeyBLL.CreateOrUpdate(new WLoginKeyBO { Status = 0, UnionId = users.unionid, UserId = s });
            var user2 = new WxUserBO { Id = s, OpenId = stu.OpenId, UserKey = s, UserType = 1, UnionId = users.unionid, HeadImgUrl = users.avatarUrl, NickName = users.nickName };
            Common.Cache.CacheHelper.Memcached.Set(sessionkey2, user2);
            stu.UnionId = "";
            stu.OpenId = "";

            sbo.IsLogin = true;
            sbo.UserKey = sessionkey2;
            ObjectHelper.AutoMapping(bo, sbo);
            sbo.SessionKey = users.SessionKey;
            sbo.UnionId = "";
            sbo.OpenId = "";
            sbo.SmOpenId = "";
            res.Model = sbo;
            return ApiResultHelper.Success<WxSmallLgResponse>(res);
        }

        /// <summary>
        /// 获取用户Tel 必须登录获取
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<UserTelResponse> GetUserTel([FromBody]WxLoginRequest request)
        {
            var res = base.CreateObject<UserTelResponse>();
            var que = WxUserLibApi.GetUserTelVO(request.Code, request.EncryptedData, request.Iv);
            if (mActionContext == null || mActionContext.WxLoginUser == null || string.IsNullOrEmpty(mActionContext.WxLoginUser.Id))
            {
                Log.Error(new String[] { "登录没了更新手机失败", "没换成,直接飞机票" });
                throw new schoolException(SubCode.RequireLogin.GetHashCode(), Message.Instance.R("v1"));
            }
            else
            {
                Log.Error(new String[] { "有登录", mActionContext.WxLoginUser.ToJson() });
                mSession.mstudentBLL.UpdateTel("", mActionContext.WxLoginUser.Id, que.phoneNumber, mActionContext.WxLoginUser.Id);
            }
            res.Model = que;
            return base.CallbackData(res);
        }

        #endregion


        #endregion

        #region 字典
        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<DicListRsponese> GetDicList(int category)
        {
            var res = base.CreateObject<DicListRsponese>();
            var que = mSession.mTeacherBLL.GetDicList(category);
            res.List = que;
            return base.CallbackData(res);
        }

        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<DicItemRsponese> GetDicOne(string id)
        {
            var res = base.CreateObject<DicItemRsponese>();
            var que = mSession.mTeacherBLL.GetDicOne(id);
            res.Model = que;
            return base.CallbackData(res);
        }
        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CommonResponse> SaveDic(DicRequest dic)
        {
            if (!string.IsNullOrEmpty(dic.ThrCategory))
            {
                dic.ThrCategory = dic.ThrCategory.TrimEnd(',');
            }
            if (!string.IsNullOrEmpty(dic.TwoCategory))
            {
                dic.TwoCategory = dic.TwoCategory.TrimEnd(',');
            }
            if (!string.IsNullOrEmpty(dic.OneCategory))
            {
                dic.OneCategory = dic.OneCategory.TrimEnd(',');
            }
            var res = base.CreateObject<CommonResponse>();
            DicBO bo = new DicBO()
            {
                Id = dic.Id,
                Category = 1,
                Name = dic.Name, 
                Sort = dic.Sort,
                Points = dic.Points,
                VideoUrl = dic.VideoUrl,
                OneCategory = dic.OneCategory,
                TwoCategory = dic.TwoCategory,
                ThrCategory = dic.ThrCategory
            };
            if (!string.IsNullOrEmpty(dic.PaterId))
            {
                bo.PaterId = dic.PaterId;
            }
            var re = mSession.mTeacherBLL.CreateOrUpdateDic(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }


        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<MDicListRsponese> GetDicByCategorId([FromBody]DICCategoryRequest request)
        {
            var res = base.CreateObject<MDicListRsponese>();
            var que = mSession.mTeacherBLL.GetListByCategorId(new DicCategoryBO { OneCategory = request.OneCategory, ThrCategory = request.ThrCategory, TwoCategory = request.TwoCategory });
            res.List = que;
            return base.CallbackData(res);
        }

        #endregion

        #region 课程
        /// <summary>
        /// 获取报名课程列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CourseListRsponese> GetList([FromBody]CourseListRequest request)
        {
            var res = base.CreateObject<CourseListRsponese>();
            var bo = new CourseListBO();
            ObjectHelper.AutoMapping(request, bo);
            var que = mSession.mCourseBLL.GetList(bo);
            res.List = que;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 获取报名单个课程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CourseRsponese> GetOne(string id)
        {
            var res = base.CreateObject<CourseRsponese>();
            var que = mSession.mCourseBLL.GetOne(id);
            res.Model = que;
            return base.CallbackData(res);
        }
        [HttpGet]
        public ApiResult<CommonResponse> DeleteCourser(string id)
        {
            var re = mSession.mCourseBLL.DeleteCourse(id,mActionContext.WxLoginUser.Id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        #endregion


            #region 留言

            /// <summary>
            /// 添加留言（学生给老师Category=1，老师给学生2）
            /// </summary>
            /// <param name="modelRequest"></param>
            /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveTeacherMessage([FromBody]TeacherMessageRequest modelRequest)
        {
            var modelBo = new TeacherMessageBO();
            modelBo.Id = modelRequest.Id;
            modelBo.UserId = mActionContext.WxLoginUser.Id;
            modelBo.Main = modelRequest.Main;
            modelBo.Title = modelRequest.Title;
            if (modelRequest.Category == MessageToEnum.ToTeacher.GetHashCode())
            {
                modelBo.StudentId = mActionContext.WxLoginUser.Id;
                modelBo.TeacherId = modelRequest.ToUserId;
            }
            else
            {
                modelBo.StudentId = modelRequest.ToUserId;
                modelBo.TeacherId = mActionContext.WxLoginUser.Id;
            }
            var bo = mSession.mTeacherBLL.CreateOrUpdateMessage(modelBo);
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        /// <summary>
        /// 获取留言列表(学生查询Category=2，老师使用Category=1)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<TeacherMessageListResponse> GetTeacherMessageList([FromBody]IdCategoryListRequest request)
        {
            var res = base.CreateObject<TeacherMessageListResponse>();
            var bo = new IdCategoryListBO();
            bo.PageSize = request.PageSize;
            bo.PageIndex = request.PageIndex;
            bo.Category = request.Category;
            //if (request.Category == MessageToEnum.ToStudent.GetHashCode())
            //{
            bo.Id = mActionContext.WxLoginUser.Id;
            bo.ToId = request.Id;
            //}
            //else
            //{
            //    bo.ToId = mActionContext.WxLoginUser.Id;
            //    bo.Id = request.Id;
            //}
            var que = mSession.mTeacherBLL.GetTeacherMessageList(bo);
            res.List = que;
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }

        /// <summary>
        /// 消息列表查询（学生查询使用 category=2，老师category=1)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<TeacherMessageMainListResponse> GetTeacherMessageMainList([FromBody]IdCategoryListRequest request)
        {
            var res = base.CreateObject<TeacherMessageMainListResponse>();
            var bo = new IdCategoryListBO();
            bo.Id = request.Id;
            bo.PageSize = request.PageSize;
            bo.PageIndex = request.PageIndex;
            bo.Category = request.Category;
            var que = mSession.mTeacherBLL.GetTeacherMessageMainList(bo);
            res.List = que;
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }
        #endregion


        #region 老师页面


        /// <summary>
        /// 获取老师列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<TeacherListResponse> GetTeacherList([FromBody]IdListRequest request)
        {
            var res = base.CreateObject<TeacherListResponse>();
            var bo = new TeacherListBO();
            bo.PageSize = request.PageSize;
            bo.PageIndex = request.PageIndex;
            var que = mSession.mTeacherBLL.GetList(bo);
            res.List = que;
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 获取单个老师
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<TeacherResponse> GetTeacher(string Id)
        {
            var res = base.CreateObject<TeacherResponse>();
            var que = mSession.mTeacherBLL.GetOne(Id);
            res.Model = que;
            return base.CallbackData(res);
        }
        #endregion


        #region //场地
        /// <summary>
        /// 获取场地列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<FieldListResponse> GetFieldList(string id)
        {
            var res = base.CreateObject<FieldListResponse>();
            var que = mSession.mFieldBLL.GetList(id);
            res.List = que;
            res.TotNum = que.Count();
            return base.CallbackData(res);
        }

        /// <summary>
        /// 获取学校列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<SchoolListResponse> GetSchool()
        {
            var res = base.CreateObject<SchoolListResponse>();
            var que = mSession.mFieldBLL.GetListSchool();
            res.List = que;
            res.TotNum = que.Count();
            return base.CallbackData(res);
        }
        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<SchoolResponse> GetSchool(string id)
        {
            var res = base.CreateObject<SchoolResponse>();
            var que = mSession.mFieldBLL.GetSchool(id);
            res.Model = que;
            return base.CallbackData(res);
        }

        /// <summary>
        /// 按日期场地获取列表
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<SubListResponse> GetSubListByDay([FromBody]SubRequest request)
        {
            var res = base.CreateObject<SubListResponse>();
            var mo = mSession.mCourseBLL.GetListSub(request.FieldId, request.Day, request.FutureDay);
            res.List = mo;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 获取订场价格
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<SubListResponse> GetSubListBySchoolId([FromBody]SubRequest request)
        {
            var res = base.CreateObject<SubListResponse>();
            int dctype = 0;
            if (!string.IsNullOrEmpty( request.SelType.ToString()))
            {
                dctype = request.SelType;
            }
            var mo = mSession.mCourseBLL.GetListSubBySchoolId(request.SchoolId, request.Day, dctype);

            var mos = mSession.mFieldBLL.GetFilePrice();
            res.Hour = mos.sorttime;
            res.Price = mos.Price;
            res.PriceSec = mos.PriceSec;
            res.List = mo;
            return base.CallbackData(res);
        }

        #endregion


        #region 课程
        [HttpPost]
        public ApiResult<CommonResponse> CreateOrUpdateCourse([FromBody]CourseRequest modelRequest)
        {
            var modelBo = new CourseBO();
            modelBo.Id = modelRequest.Id;
            ObjectHelper.AutoMapping(modelRequest, modelBo);
            modelBo.UserId = mActionContext.WxLoginUser.Id;
            modelBo.Main = modelRequest.Main;
            modelBo.Title = modelRequest.Title; 
            var bo = mSession.mCourseBLL.CreateOrUpdate(modelBo);
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        #endregion

        #region
        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CommonResponse> CreateOrUpdateStudent([FromBody]StudentUpRequest modelRequest)
        {
            var bo = mSession.mstudentBLL.CreateOrUpdateStudent(modelRequest.Id, modelRequest.FullName, modelRequest.Tel, modelRequest.Types, modelRequest.UserId);
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        #endregion


        #endregion


        #region  财务
        [HttpPost]
        public ApiResult<EquListResponse> GetEquList([FromBody] EquListRequest request)
        {
            var res = base.CreateObject<EquListResponse>();
            var que = mSession.mCourseBLL.GetEquList(new EquListBO { Btime = request.Btime, Etime = request.Etime, SchoolId = request.SchoolId, PageIndex = request.PageIndex, PageSize = request.PageSize });
            res.Model = que;
            return base.CallbackData(res);
        }

        [HttpGet]
        public ApiResult<EquResponse> GetEquOne(string id)
        {
            var res = base.CreateObject<EquResponse>();
            var que = mSession.mCourseBLL.GetEquOne(id);
            res.Model = que;
            return base.CallbackData(res);
        }
        [HttpPost]
        public ApiResult<CommonResponse> SaveEqu(EquRequest request)
        {
            var res = base.CreateObject<CommonResponse>();
            var bo = new EquBO();
            ObjectHelper.AutoMapping(request, bo);
            bo.UserId = mActionContext.WxLoginUser.Id;
            var re = mSession.mCourseBLL.CreateOrUpdateEqu(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }


        #endregion


        #region  获取列表
        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CategoryListResponse> GetCategoryList(string parentId)
        {
            var res = base.CreateObject<CategoryListResponse>();
            var que = mSession.mCourseBLL.GetCatergoryList(parentId);
            res.List = que;
            return base.CallbackData(res);
        }

        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CategoryListVOponse> GetCategoryListByCatetory(string category)
        {
            var res = base.CreateObject<CategoryListVOponse>();
            var que = mSession.mCourseBLL.GetCatergoryListByCategory(category); 
            res.List = que;
            var s = mSession.mCourseBLL.GetCatergoryListByCategoryOne("4");
            res.OneList = s;
            return base.CallbackData(res);
        }

        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CategoryResponse> GetCategoryOne(string id)
        {
            var res = base.CreateObject<CategoryResponse>();
            var que = mSession.mCourseBLL.GetCatergoryOne(id);
            res.Model = que;
            return base.CallbackData(res);
        }
        [HttpPost]
        public ApiResult<CommonResponse> SaveCategory(CategoryRequest request)
        {
            var res = base.CreateObject<CommonResponse>();
            var bo = new CategoryBO();
            ObjectHelper.AutoMapping(request, bo);
            bo.UserId = mActionContext.WxLoginUser.Id;
            var re = mSession.mCourseBLL.CreateOrUpdateCategory(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        #region 删除
        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CommonResponse> DeleteCateory(string id)
        {
            var bo = mSession.mCourseBLL.DeleteCategory(id);
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CategoryListALLVOResponse> GetCategoryListAll(string category)
        {
            var res = base.CreateObject<CategoryListALLVOResponse>();
            var que = mSession.mCourseBLL.GetCatergoryAll(category);
            res.Model = que;
            return base.CallbackData(res);
        }

        #endregion


        #endregion

        #region
        [HttpPost]
        public ApiResult<CommonResponse> SaveFilePrice(FilePriceRequest request)
        {
            var res = base.CreateObject<CommonResponse>();
            var bo = new FilePriceBO();
            ObjectHelper.AutoMapping(request, bo);
            bo.Price = request.price;
            bo.PriceSec = request.priceSec;
            bo.sorttime = request.sortime;
            bo.UserId = mActionContext.WxLoginUser.Id;
            var re = mSession.mFieldBLL.CreateOrUpdateFilePrice(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        #endregion

    }
}

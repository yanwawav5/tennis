using school.API.Filter;
using school.BLL;
using school.BLL.Factory;
using school.Common;
using school.Common.Tools;
using school.Model.BO;
using school.Model.DAO;
using school.Model.Enum;
using school.Model.TO;
using school.Model.TO.Request;
using school.Model.TO.Response;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Diagnostics;

namespace school.Api.Controllers
{
    public class UserController : BaseController
    {
        
        #region //新的
        /// <summary>
        /// 视频上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<UpPicVOResponse> Upload()
        {
            try
            { 
                var res = base.CreateObject<UpPicVOResponse>();
                var content = Request.Content;//获取http设置的消息和内容
                var tempUploadFiles = "/uploads/";//保存路径
                var newFileName = "";
                string filePath = "";
                string extname = "";
                string returnurl = "";
                var sp = new MultipartMemoryStreamProvider();
                Task.Run(async () => await Request.Content.ReadAsMultipartAsync(sp)).Wait();

                foreach (var item in sp.Contents)
                {
                    if (item.Headers.ContentDisposition.FileName != null)
                    {
                        var filename = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                        FileInfo file = new FileInfo(filename);
                        //string fileTypes = "gif,jpg,jpeg,png,bmp";
                        string fileTypes = "mp4,mov";
                        if (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) == -1)
                        {
                            throw new ApplicationException("不支持上传文件类型");
                        }

                        //获取后缀
                        extname = System.IO.Path.GetExtension(filename);//获取文件的拓展名称
                        newFileName = Guid.NewGuid().ToString().Substring(0, 9) + extname;
                        string newFilePath = DateTime.Now.ToString("yyyy-MM-dd") + "/";
                        if (!Directory.Exists(HostingEnvironment.MapPath("/") + tempUploadFiles + newFilePath))
                        {
                            Directory.CreateDirectory(HostingEnvironment.MapPath("/") + tempUploadFiles + newFilePath);
                        }
                        filePath = Path.Combine(HostingEnvironment.MapPath("/") + tempUploadFiles + newFilePath, newFileName);
                        returnurl = Path.Combine(tempUploadFiles + newFilePath, newFileName);//图片相对路径
                        var ms = item.ReadAsStreamAsync().Result;
                        using (var br = new BinaryReader(ms))
                        {
                            var data = br.ReadBytes((int)ms.Length);
                            File.WriteAllBytes(filePath, data);//保存图片

                            //var sss = CatchImg("a444a", "2223", returnurl); 
                        }

                    }
                }
                res.Model = new UpPicVO { Str = returnurl };


                return base.CallbackData(res);
            }
            catch (Exception ex)
            {
                throw new schoolException(ex.ToString());
            }
        }


        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<UpPicVOResponse> UploadP()
        {
            try
            {
                var res = base.CreateObject<UpPicVOResponse>();
                var content = Request.Content;//获取http设置的消息和内容
                var tempUploadFiles = "/uploads/";//保存路径
                var newFileName = "";
                string filePath = "";
                string extname = "";
                string returnurl = "";
                var sp = new MultipartMemoryStreamProvider();
                Task.Run(async () => await Request.Content.ReadAsMultipartAsync(sp)).Wait();

                foreach (var item in sp.Contents)
                {
                    if (item.Headers.ContentDisposition.FileName != null)
                    {
                        var filename = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                        FileInfo file = new FileInfo(filename);
                        string fileTypes = "gif,jpg,jpeg,png,bmp";
                        //string fileTypes = "mp4,mov";
                        if (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) == -1)
                        {
                            throw new ApplicationException("不支持上传文件类型");
                        }

                        //获取后缀
                        extname = System.IO.Path.GetExtension(filename);//获取文件的拓展名称
                        newFileName = Guid.NewGuid().ToString().Substring(0, 9) + extname;
                        string newFilePath = DateTime.Now.ToString("yyyy-MM-dd") + "pic/";
                        if (!Directory.Exists(HostingEnvironment.MapPath("/") + tempUploadFiles + newFilePath))
                        {
                            Directory.CreateDirectory(HostingEnvironment.MapPath("/") + tempUploadFiles + newFilePath);
                        }
                        filePath = Path.Combine(HostingEnvironment.MapPath("/") + tempUploadFiles + newFilePath, newFileName);
                        returnurl = Path.Combine(tempUploadFiles + newFilePath, newFileName);//图片相对路径
                        var ms = item.ReadAsStreamAsync().Result;
                        using (var br = new BinaryReader(ms))
                        {
                            var data = br.ReadBytes((int)ms.Length);
                            File.WriteAllBytes(filePath, data);//保存图片

                            //var sss = CatchImg("a444a", "2223", returnurl); 
                        }

                    }
                }
                res.Model = new UpPicVO { Str = returnurl };


                return base.CallbackData(res);
            }
            catch (Exception ex)
            {
                throw new schoolException(ex.ToString());
            }
        }


        //视频截图,fileName视频地址,imgFile图片地址
        public string CatchImg(string fileName, string imgFile)
        {
            //使用ffmpeg抓图
            string ffmpeg = HostingEnvironment.MapPath("ffmpeg.exe");
            //string ffmpeg = HostingEnvironment.MapPath( PublicMethod.ffmpegtool);
            //图片名称
            string flv_img = imgFile + ".jpg";
            //图片大小
            string FlvImgSize = "240x180";
            //建立ffmpeg进程
            System.Diagnostics.ProcessStartInfo ImgstartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            //后台运行
            ImgstartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //进程参数
            //ffmpeg -i input.flv -y -f image2 -ss 10.11 -t 0.001 -s 240x180 catchimg.jpg;
            ImgstartInfo.Arguments = "   -i   " + fileName + "  -y  -f  image2   -ss 2 -vframes 1  -s   " + FlvImgSize + "   " + flv_img;
            try
            {
                //开始抓图
                System.Diagnostics.Process.Start(ImgstartInfo);
                //睡眠一下,等待截图完成,按需设置
                System.Threading.Thread.Sleep(5000);
            }
            catch
            {
                return "";
            }
            //
            if (!System.IO.File.Exists(flv_img))
            {
                return flv_img;
            }

            return "";
        }

        [HttpGet]
        //[schoolActionFilter(IsCk = false)]
        public ApiResult<WechatUserInfoResponse> SaveUser(string code)
        {
            var ouser = WxUserLibApi.GetAccessToken(code);
            var res = base.CreateObject<WechatUserInfoResponse>();
            var tuser = WxUserLibApi.GetRefreshAccessToken(ouser.refresh_token);
            Log.Error(new String[] { "tuser", tuser.ToJson() });
            var users = WxUserLibApi.GetRefreshAccessToken(tuser.access_token, ouser.openId);
            Log.Error(new String[] { "users", ouser.ToJson() });
            res.Model = users;
            return base.CallbackData(res);
        }

        /// <summary>
        /// 添加分享
        /// </summary>
        /// <param name="re"></param>
        /// <returns></returns>
        [HttpPost]
      //  [schoolActionFilter(IsCk = false)]
        public ApiResult<StrKeyResponse> SaveShare([FromBody] ShareInfoRequest re)
        {
            if(string.IsNullOrEmpty(re.Pic))
                throw new schoolException(SubCode.Failure.GetHashCode(), "请等待上传完视频！");
            var res = base.CreateObject<StrKeyResponse>();
          
            //mActionContext.WxLoginUser
            Log.Error(new string[] { "id", mActionContext.WxLoginUser.Id });
           var usermo = mSession.mstudentBLL.GetOne(mActionContext.WxLoginUser.Id, true);
          //  var usermo = mSession.mstudentBLL.GetOne("77950516-edfe-4622-925c-c3ca0ce53bad", true); 
            if (usermo.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误！");
            ShareInfoBO mo = new ShareInfoBO();
            ObjectHelper.AutoMapping(re, mo);
            mo.Url = re.Pic;
            mo.ShareDay = DateTime.Now.ToString("yyyyMMdd");
            mo.UnionId = usermo.UnionId;
            mo.StudentId = mActionContext.WxLoginUser.Id;
            var s = SessionFactory.SessionService.mshareInfoBLL.CreateOrUpdate(mo);
            res.Model = new StrKey { Id = s };
            return base.CallbackData(res);
        }


        [HttpPost]
          //[schoolActionFilter(IsCk = false)]
        public ApiResult<StrKeyResponse> SaveShare2([FromBody] ShareInfoRequest re)
        {
            if (re.Types == 2)
            {
                re.Types = 0;
            }
            
            if(re.Types==2)
            if (string.IsNullOrEmpty(re.Pic))
                throw new schoolException(SubCode.Failure.GetHashCode(), "请等待上传完视频！");
            var res = base.CreateObject<StrKeyResponse>();
            var uid = mActionContext.WxLoginUser.Id;// ;
            Log.Error(new string[] { "id", uid });
            var usermo = mSession.mstudentBLL.GetOne(uid, true);
            //  var usermo = mSession.mstudentBLL.GetOne("77950516-edfe-4622-925c-c3ca0ce53bad", true); 
            if (usermo.FtimeStemp == null)
                throw new schoolException(SubCode.Failure.GetHashCode(), "参数有错误！");
            ShareInfoBO mo = new ShareInfoBO();
            ObjectHelper.AutoMapping(re, mo);
            //if (re.Types == 2)
            //{
            //    mo.StudentCourseClassId = "";
            //    mo.StudentCourseId = re.StudentCourseClassId;
            //} 
            mo.Url = re.Pic;
            mo.ShareDay =DateTime.Now.ToString("yyyyMMdd");
            mo.UnionId = usermo.UnionId;
            mo.StudentId = uid;
            var s = SessionFactory.SessionService.mshareInfoBLL.CreateOrUpdate2(mo);
            res.Model = new StrKey { Id = s };
            return base.CallbackData(res);
        }

        [HttpGet]
        //[schoolActionFilter(IsCk = false)]
        public ApiResult<CommonResponse> SaveClick(string id)
        {
            Log.Error(new string[] { "换成", mActionContext.WxLoginUser.ToJson() });
            if (String.IsNullOrEmpty(mActionContext.WxLoginUser.UnionId))
            {

            }
            var bo = mSession.mshareInfoBLL.CreateOrUpdateClick(id, mActionContext.WxLoginUser.UnionId);
            return bo ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        [HttpGet]
        //[schoolActionFilter(IsCk = false)]
        public ApiResult<PrizeResponse> SaveCj(string id)
        {
            var res = base.CreateObject<PrizeResponse>();

            Log.Error(new string[] { "缓存", mActionContext.WxLoginUser.ToJson() });
            M_StudentPrize mo = new M_StudentPrize()
            {
                ShareInfoId = id,
                CreateUserId = mActionContext.WxLoginUser.UnionId,
                UnionId = mActionContext.WxLoginUser.UnionId,
                
            };
            var bo = mSession.mStudentPrizeBLL.CreateOrUpdate(mo,1,1);
            res.model = bo;
            return base.CallbackData(res);
        }

        [HttpGet]
        [schoolActionFilter(IsCk = false)]
        public ApiResult<PrizeResponse> SaveCj2(string id)
        {
            var res = base.CreateObject<PrizeResponse>();

            //Log.Error(new string[] { "缓存", mActionContext.WxLoginUser.ToJson() });
            //M_StudentPrize mo = new M_StudentPrize()
            //{
            //    ShareInfoId = id,
            //    CreateUserId = mActionContext.WxLoginUser.UnionId,
            //    UnionId = mActionContext.WxLoginUser.UnionId
            //};
            //var bo = mSession.mStudentPrizeBLL.CreateOrUpdate(mo);
            PrizeVO d = new PrizeVO()
            {
                 Prize=1,
                  PrizeNum=2
            };
            res.model = d;
            return base.CallbackData(res);
        }


        [HttpGet]
        public ApiResult<CommonResponse> SaveMsg(string phone, string bs,string id)
        {
            switch (bs)
            {
                case "1":
                    bs = "10";
                    break;
                case "5":
                    bs = "199";
                    break;
            };
            if(string.IsNullOrEmpty(id))
                throw new schoolException(SubCode.Failure.GetHashCode(), "非法输入！");
            var res = base.CreateObject<CommonResponse>();
            var bo = SessionFactory.SessionService.mWtokenBLL.CreateOrUpdateMS(new W_Code {  ShareInfoId=id,Phone = phone, Price = bs, Status = 0, StudentId = mActionContext.WxLoginUser.Id, CreateUserId = mActionContext.WxLoginUser.UnionId });
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        [HttpGet]
        //[schoolActionFilter(IsCk = false)]
        public ApiResult<CommonResponse> SaveCode(string phone, string code)
        {
            var bo = SessionFactory.SessionService.mWtokenBLL.UpdateCode(phone, code);
            return bo ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
            
        }


        #endregion
    }
}

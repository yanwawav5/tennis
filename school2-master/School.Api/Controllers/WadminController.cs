using school.Api.WePay;
using school.API.Filter;
using school.BLL;
using school.Common;
using school.Common.Tools;
using school.Model.BO;
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
using System.Text;
using System.Web.Http;
using System.Xml;

namespace school.Api.Controllers
{
    public class WadminController : BaseController
    {
        

        #region 支付

        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public string callback()
        {
            string strresult = string.Empty;
            try
            {
                string content = Request.Content.ReadAsStringAsync().Result;

                string strxml = content;
                //判断是否请求成功
                if (getxmlvalue(strxml, "return_code").ToLower() == "success")
                {
                    //判断是否支付成功
                    if (getxmlvalue(strxml, "result_code").ToLower() == "success")
                    {
                        //获得签名
                        string getsign = getxmlvalue(strxml, "sign");
                        //进行签名
                        string sign = getsigninfo(getfromxml(strxml), WXPay.KEY);
                        if (sign == getsign)
                        {
                            //校验订单信息
                            string wxordernum = getxmlvalue(strxml, "transaction_id"); //微信订单号
                            string ordernum = getxmlvalue(strxml, "out_trade_no");    //商户订单号
                            string ordertotal = getxmlvalue(strxml, "total_fee");
                            string openid = getxmlvalue(strxml, "openid");
                            //校验订单是否存在
                            if (true)
                            {
                                //2.更新订单的相关状态
                                var oid = mSession.mWserverMainBLL.GetKeyByServerId(ordernum);


                                if (oid == null || string.IsNullOrEmpty(oid.Id.ToString()))
                                {
                                    throw new schoolException(SubCode.Failure.GetHashCode(), "未找到数据或者订单已失效");
                                }
                                var obj = false;
                                if (oid.Category == 0)
                                {
                                    mSession.mStudentCourseBLL.UpdateStudentIsPay(oid.ServerId);
                                }
                                else if (oid.Category == 1)
                                {
                                    var one = mSession.mCourseBLL.OrderOnlyPay(oid.ServerId);
                                    if (one.Status == 0)
                                    {
                                        obj = mSession.mCourseBLL.UpdateOrder(oid.ServerId).IsSuccess;
                                    }
                                    else
                                    {
                                    }
                                }
                                else if (oid.Category == 2)
                                {
                                    var one = mSession.mTeacherBLL.getOneOnlyActStu(oid.ServerId);
                                    if (one.Status == 0)
                                    {
                                        obj = mSession.mTeacherBLL.UpdateActivityStudentStatus(oid.ServerId, 1, "").IsSuccess;
                                    }
                                    else
                                    {
                                    }
                                }
                                //3.返回一个xml格式的结果给微信服务器
                                if (obj)
                                {
                                    strresult = getreturnxml("success", "ok");
                                }
                                else
                                {
                                    throw new schoolException(SubCode.Failure.GetHashCode(), "订单状态更新失败");
                                }
                            }
                        }
                        else
                        {
                            throw new schoolException(SubCode.Failure.GetHashCode(), "签名不一致");
                        }
                    }
                    else
                    {
                        throw new schoolException(SubCode.Failure.GetHashCode(), "支付通知失败");
                    }
                }
                else
                {
                    throw new schoolException(SubCode.Failure.GetHashCode(), "支付通知失败2");
                }
            }
            catch (Exception ex)
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), ex.ToString());

            }
            return strresult;
            //return base.CallbackData(res);
        }

        #region 生成签名
        /// <summary>
        /// 获取签名数据
        ///</summary>
        /// <param name="strparam"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string getsigninfo(Dictionary<string, string> strparam, string key)
        {
            int i = 0;
            string sign = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (KeyValuePair<string, string> temp in strparam)
                {
                    if (temp.Value == "" || temp.Value == null || temp.Key.ToLower() == "sign")
                    {
                        continue;
                    }
                    i++;
                    sb.Append(temp.Key.Trim() + "=" + temp.Value.Trim() + "&");
                }
                sb.Append("key=" + key.Trim() + "");
                sign = WXPay.GetMD5(sb.ToString()).ToUpper();
            }
            catch (Exception ex)
            {
                //utility.addlog("payhelper", "getsigninfo", ex.message, ex);
            }
            return sign;
        }
        #endregion

        #region xml 处理
        /// <summary>
        /// 获取xml值
        /// </summary>
        /// <param name="strxml">xml字符串</param>
        /// <param name="strdata">字段值</param>
        /// <returns></returns>
        public string getxmlvalue(string strxml, string strdata)
        {
            string xmlvalue = string.Empty;
            XmlDocument xmldocument = new XmlDocument();
            xmldocument.LoadXml(strxml);
            var selectsinglenode = xmldocument.DocumentElement.SelectSingleNode(strdata);
            if (selectsinglenode != null)
            {
                xmlvalue = selectsinglenode.InnerText;
            }
            return xmlvalue;
        }

        /// <summary>
        /// 集合转换xml数据 (拼接成xml请求数据)
        /// </summary>
        /// <param name="strparam">参数</param>
        /// <returns></returns>
        public string createxmlparam(Dictionary<string, string> strparam)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("<xml>");
                foreach (KeyValuePair<string, string> k in strparam)
                {
                    if (k.Key == "attach" || k.Key == "body" || k.Key == "sign")
                    {
                        sb.Append("<" + k.Key + "><![cdata[" + k.Value + "]]></" + k.Key + ">");
                    }
                    else
                    {
                        sb.Append("<" + k.Key + ">" + k.Value + "</" + k.Key + ">");
                    }
                }
                sb.Append("</xml>");
            }
            catch (Exception ex)
            {

            }

            return sb.ToString();
        }

        /// <summary>
        /// xml数据转换集合（xml数据拼接成字符串)
        /// </summary>
        /// <param name="xmlstring"></param>
        /// <returns></returns>
        public Dictionary<string, string> getfromxml(string xmlstring)
        {
            Dictionary<string, string> sparams = new Dictionary<string, string>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlstring);
                //xmlelement 
                XmlElement root = doc.DocumentElement;
                int len = root.ChildNodes.Count;
                for (int i = 0; i < len; i++)
                {
                    string name = root.ChildNodes[i].Name;
                    if (!sparams.ContainsKey(name))
                    {
                        sparams.Add(name.Trim(), root.ChildNodes[i].InnerText.Trim());
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return sparams;
        }

        /// <summary>
        /// 返回通知 xml
        /// </summary>
        /// <param name="returncode"></param>
        /// <param name="returnmsg"></param>
        /// <returns></returns>
        public string getreturnxml(string returncode, string returnmsg)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<return_code><![cdata[" + returncode + "]]></return_code>");
            sb.Append("<return_msg><![cdata[" + returnmsg + "]]></return_msg>");
            sb.Append("</xml>");
            return sb.ToString();
        }
        #endregion
        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<WbPayResponse> Pay([FromBody] PayRequest p)
        {
            var res = base.CreateObject<WbPayResponse>();

            var student = mSession.mstudentBLL.GetOne(mActionContext.WxLoginUser.Id);
            var sid = mSession.mWserverMainBLL.SaveKey(p.ServerId, p.Category).ToString();
            var models = WXPay.Pay(p.Code, sid, p.Price, p.ServerName, student.SmOpenId);
            PayRequesEntity str = (PayRequesEntity)models;
            res.Model = str;
            return base.CallbackData(res);
        }


        //[schoolActionFilter(IsNeedLogin = false)]
        //public ApiResult<WbPayResponse> GetKey([FromBody] PayRequest p)
        //{
        //    var res = base.CreateObject<WbPayResponse>();
        //    //var openId = "o191d5WxOV2v4A84fWVMrYBBoNIc";
        //    PayRequesEntity px = new PayRequesEntity()
        //    {
        //        appid = mActionContext?.WxLoginUser?.OpenId,
        //        nonceStr = mActionContext?.WxLoginUser?.Id,
        //        paySign = mHeader?.UserKey
        //    };
        //    res.Model = px;
        //    return base.CallbackData(res);
        //}

        #endregion




        #region 获取用户订购信息
        /// <summary>
        /// 获取订购列表(学生IsStudent=true,老师false）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<StudentCourseListResponse> GetStudentCourseList([FromBody]StudentCourseRequest request)
        {
            var res = base.CreateObject<StudentCourseListResponse>();
            var bo = new StudentCourseListBO();
            //ObjectHelper.AutoMapping(request, bo);
            bo.PageIndex = request.PageIndex;
            bo.PageSize = request.PageSize;
            if (request.IsStudent)
                bo.UserId = mActionContext.WxLoginUser.Id;
            else
                bo.UserId = "";
            bo.AgeCategoryId = request.AgeCategoryId;

            bo.CategoryId = request.CategoryId;
            bo.Normal = request.Normal.ToString();
            if (bo.Normal == "1")
                bo.CategoryId = "5";
            bo.PageIndex = request.PageIndex;
            bo.PageSize = request.PageSize;
            bo.Status = request.Status;
            bo.IsStudent = request.IsStudent; 
            //bo.UserId = "77950516-edfe-4622-925c-c3ca0ce53bad";// mActionContext.WxLoginUser.Id;
            res.List = mSession.mStudentCourseBLL.GetListCourse(bo);
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }

        /// <summary>
        /// 获取订购单条数据学生老师都可使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<StudentCourseResponse> GetStudentCourse(string id)
        {
            var res = base.CreateObject<StudentCourseResponse>();
            var mo = mSession.mStudentCourseBLL.GetOneCourse2(id);
            res.Model = mo;
            return base.CallbackData(res);
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<IdResponse> SaveStudentCourse([FromBody]StudentCourseAddRequest modelRequest)
        {
            var userIdo = mActionContext.WxLoginUser.Id;
            var res = base.CreateObject<IdResponse>();
            var bo = new StudentCourseBO();
            ObjectHelper.AutoMapping(modelRequest, bo);
            bo.UserId = userIdo;
            var re = mSession.mStudentCourseBLL.CreateOrUpdateCourse(bo);
            res.Model = re;
            return base.CallbackData(res);
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> UpdateStudentCourse([FromBody]StudentCourse2Request modelRequest)
        {
            var userIdo = mActionContext.WxLoginUser.Id;
            var res = base.CreateObject<CommonResponse>();
            var bo = new StudentCourseBO();
            ObjectHelper.AutoMapping(modelRequest, bo);
            bo.Name = modelRequest.Name;
            bo.Tel = modelRequest.Tel;
            bo.UserId = userIdo;
            var re = mSession.mStudentCourseBLL.UpdateStudentCourse(bo);
            return base.CallbackData(res);
        }


        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<CommonResponse> EscStudentCouse(string id)
        {
            var res = base.CreateObject<CommonResponse>();
            mSession.mStudentCourseBLL.DeleteStudentCourse(id);
            return base.CallbackData(res);
        }


        /// <summary>
        /// 删除课程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> UpdateStudentCouse([FromBody]IdRequest request)
        {
            var res = base.CreateObject<CommonResponse>();
            mSession.mStudentCourseBLL.UpdateStudentCourseStatus(request.Id, request.Status);
            return base.CallbackData(res);
        }
        #endregion

        #region 学生
        #region 请假
        /// <summary>
        /// 申请请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveStudengLeave([FromBody]StudentLeaveRequest request)
        {
            var res = base.CreateObject<CommonResponse>();
            var bo = new StudentLeaveBO()
            {
                StudentCourseClassId = request.StudentCourseClassId,
                StudentId = mActionContext.WxLoginUser.Id,
                Main = request.Main,
                Status = LeaveStatusEnum.Ask.GetHashCode(),
                UserId = mActionContext.WxLoginUser.Id
            };
            mSession.mStudentCourseBLL.CreateOrUpdateLeave(bo);
            return base.CallbackData(res);
        }
        /// <summary>
        /// 请假状态修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> UpdateStudentLeave([FromBody]IdRequest request)
        {
            var res = base.CreateObject<CommonResponse>();
            mSession.mStudentCourseBLL.UpdateLeaveStauts(request.Id, Int32.Parse(request.Status), mActionContext.WxLoginUser.Id);
            return base.CallbackData(res);
        }

        /// <summary>
        /// 获取请假列表（传递studentId学生使用，TeacherId老师使用）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<StudentLeaveListResponse> GetStudentLeaveStudentList([FromBody]StudentLeaveListRequest request)
        {
            var res = base.CreateObject<StudentLeaveListResponse>();
            var bo = new StudentLeaveListBO()
            {
                TeacherId = request.TeacherId,
                Status = request.Status,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                StudentCourseClassId = request.StudentCourseClassId,
                StudentId = request.StudentId,
                TotNum = 0
            };
            res.List = mSession.mStudentCourseBLL.GetListLeave(bo);
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }

        /// <summary>
        /// 获取单条请假信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<StudentLeaveResponse> GetStudentLeave(string id)
        {
            var res = base.CreateObject<StudentLeaveResponse>();
            var mo = mSession.mStudentCourseBLL.GetOneLeave(id);
            res.Model = mo;
            return base.CallbackData(res);
        }

        #endregion

        #region 学生订单
        /// <summary>
        /// 学生订单
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        //[schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<IdListResponse> SaveOrder([FromBody]OrderRequest modelRequest)
        {
           // var userIdo = "77950516-edfe-4622-925c-c3ca0ce53bad";
          var userIdo = mActionContext.WxLoginUser.Id;
            var res = base.CreateObject<IdListResponse>();
            List<OrderBO> bo = new List<OrderBO>();
            foreach (var item in modelRequest.List)
            {
                var b = new OrderBO()
                {
                    Day = item.Day,
                    FieldId = item.FieldId,
                    HourList = item.HourList,
                    StudentId = userIdo,
                    UserId = userIdo,
                    Category = modelRequest.Category
                };
                bo.Add(b);
            }
            var re = mSession.mCourseBLL.CreateOrUpdateOrder(bo, userIdo, modelRequest.Name, modelRequest.Tel, modelRequest.StudentKjId, modelRequest.Category);
            res.List = re;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 学生端获取自己的
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        //[schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<StudentOrderListResponse> GetStudentOrderList([FromBody]IdListRequest modelRequest)
        {
            var res = base.CreateObject<StudentOrderListResponse>();
            if (string.IsNullOrEmpty(modelRequest.Id))
            {
                modelRequest.Id = mActionContext.WxLoginUser.Id;
            }
            var bo = new IdStatusListBO
            {
                Id = modelRequest.Id,
                PageSize = modelRequest.PageSize,
                PageIndex = modelRequest.PageIndex,
                TotNum = 0,
                Status = modelRequest.Status
            };
            var re = mSession.mstudentBLL.GetOrderList(bo);
            res.List = re;
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }


        /// <summary>
        /// 教练段获取学生的
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        //[schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<StudentOrderListResponse> GetStudentOrderListByTeacher([FromBody]IdStatusCategoryListRequest modelRequest)
        {
            var res = base.CreateObject<StudentOrderListResponse>();
            var bo = new IdStatusListBO
            {
                Id = modelRequest.Id,
                PageSize = modelRequest.PageSize,
                PageIndex = modelRequest.PageIndex,
                TotNum = 0,
                Status = modelRequest.Status,
                CategoryId = modelRequest.Category
            };
            var re = mSession.mstudentBLL.GetOrderList(bo);
            res.List = re;
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }

        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<StudentOrderResponse> GetStudentOrder(string id)
        {
            var res = base.CreateObject<StudentOrderResponse>();
            var re = mSession.mstudentBLL.GetOrder(id);
            res.Model = re;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]

        public ApiResult<CommonResponse> SaveSub([FromBody]TeacherSubRequest modelRequest)
        {
            var bo = new SubscribeAddBO()
            {
                UserId = mActionContext.WxLoginUser.Id,
                List = modelRequest.List
            };
            var re = mSession.mCourseBLL.CreateOrUpdateSub(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }


        #region 取消订单
        /// <summary>
        /// 教练同意或者用户未付款取消订单
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        // [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CommonResponse> SubEsc([FromBody]SubESCRequest modelRequest)
        {
            var bo = new SubscribeESC()
            {
                UserId = mActionContext.WxLoginUser.Id
            };
            if (modelRequest.Category == 0)
                bo.OrderId = modelRequest.Id;
            else
            {
                bo.TeacherCourseId = modelRequest.Id[0];
            }
            var re = mSession.mCourseBLL.EscSub(bo, modelRequest.Status.ToString());
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }


        /// <summary>
        /// 用户申请取消
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        // [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CommonResponse> ApplySubEsc([FromBody]SubESCRequest modelRequest)
        {

            var bo = new SubscribeESC()
            {
                UserId = mActionContext.WxLoginUser.Id
            };
            if (modelRequest.Category == 0)
                bo.OrderId = modelRequest.Id;
            else
            {
                bo.TeacherCourseId = modelRequest.Id[0];
            }
            var re = mSession.mCourseBLL.ApplyEscSub(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        /// <summary>
        /// 用户申请取消
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> EscApplySub([FromBody]SubESCRequest modelRequest)
        {
            var bo = new SubscribeESC()
            {
                UserId = mActionContext.WxLoginUser.Id
            };
            if (modelRequest.Category == 0)
                bo.OrderId = modelRequest.Id;
            else
            {
                bo.TeacherCourseId = modelRequest.Id[0];
            }
            var re = mSession.mCourseBLL.ApplyEscSubEsc(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }


        #endregion
        #endregion

        ///// <summary>
        ///// 添加时间
        ///// </summary>
        ///// <param name="modelRequest"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ApiResult<SubScribeListResponse> SaveSub([FromBody]SubRequest request)
        //{
        //    var res = base.CreateObject<SubScribeListResponse>();
        //    var mo = mSession.mCourseBLL.GetListSub(request.FieldId,request);
        //    res.List = mo;
        //    return base.CallbackData(res); 
        //}



        #endregion

        #region  視頻

        /// <summary>
        /// 获取视频
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]

        public ApiResult<VideoListResponse> GetVideoList([FromBody]IdListRequest request)
        {
            var res = base.CreateObject<VideoListResponse>();
            var bo = new IdListBO()
            {
                Key = request.Key,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotNum = 0
            };
            res.List = mSession.mTeacherBLL.GetVideoList(bo);
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }


        /// <summary>
        /// 获取单个视频
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<VideoResponse> GetVideo(string Id)
        {
            var res = base.CreateObject<VideoResponse>();
            res.Model = mSession.mTeacherBLL.GetVideo(Id);
            return base.CallbackData(res);
        }

        /// <summary>
        /// 保存视频
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveVideo([FromBody]VideoRequest modelRequest)
        {
            var bo = new VideoBO();
            ObjectHelper.AutoMapping(modelRequest, bo);
            bo.UserId = mActionContext.WxLoginUser.Id;
            var re = mSession.mTeacherBLL.CreateOrUpdateVideo(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        #region 删除
        [HttpGet]
        public ApiResult<CommonResponse> DeleteVideo(string id)
        {
            var bo = mSession.mTeacherBLL.DeleteVideo(id, mActionContext.WxLoginUser.Id);
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        #endregion
        #endregion

        #region 教师

        #region  老师
        /// <summary>
        /// 自动添加老师
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<CommonResponse> SaveTeacherByAuto()
        {
            var student = mSession.mstudentBLL.GetOne(mActionContext.WxLoginUser.Id);
            var bo = new TeacherBO()
            {
                StudentId = student.Id,
                Name = student.NickName,
                Tel = student.Tel,
                UserId = student.Id,
            };

            var re = mSession.mTeacherBLL.CreateOrUpdate(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        [HttpPost]
        public ApiResult<CommonResponse> UpdateTeacherInfo([FromBody]TeacherRequest modelRequest)
        {
            var bo = new TeacherBO2();
            ObjectHelper.AutoMapping(modelRequest, bo);
            var re = mSession.mTeacherBLL.UpdateTeacherInfo(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }


        /// <summary>
        /// 自己修改
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> UpdateTeacherInfoByOwn([FromBody]TeacherRequest modelRequest)
        {
            var bo = new TeacherBO2();
            ObjectHelper.AutoMapping(modelRequest, bo);
            bo.StudentId = mActionContext.WxLoginUser.Id;
            var re = mSession.mTeacherBLL.UpdateTeacherInfoByZJ(bo);
            
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        #endregion


        /// <summary>
        /// 获取教案列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost] 
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<TeachingPlanListResponse> GetTeachingPlanList([FromBody]IdListRequest request)
        {
            var res = base.CreateObject<TeachingPlanListResponse>();
            var bo = new IdListBO()
            {
                Key = request.Key,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotNum = 0
            };
            res.List = mSession.mTeacherBLL.GetTeachingPlanList(bo);
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }


        /// <summary>
        /// 获取教案单个
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<TeachingPlanResponse> GetTeachingPlan(string Id)
        {
            var res = base.CreateObject<TeachingPlanResponse>();
            res.Model = mSession.mTeacherBLL.GetTeachingPlan(Id);
            res.List = mSession.mTeacherBLL.GetTeachingPlanInfoList(new IdListBO { Id = Id });
            return base.CallbackData(res);
        }
        /// <summary>
        /// 删除教案
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<CommonResponse> DeleteTeachingPlan(string id)
        {
            var bo = new TeachingPlanBO();
            var re = mSession.mTeacherBLL.DeleteTeachingPlan(id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }


        /// <summary>
        /// 保存教案
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveTeachingPlan([FromBody]TeachingPlanRequest modelRequest)
        {
            var bo = new TeachingPlanBO();
            ObjectHelper.AutoMapping(modelRequest, bo);
            bo.UserId = mActionContext.WxLoginUser.Id;
            bo.List = modelRequest.List;
            var re = mSession.mTeacherBLL.CreateOrUpdateTeachingPlan(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }



        /// <summary>
        /// 保存教案信息
        /// </summary>
        /// <param name="modelRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveTeachingPlanInfo([FromBody]TeachingPlanInfoRequest modelRequest)
        {
            var bo = new TeachingPlanInfoBO();
            var re = mSession.mTeacherBLL.CreateOrUpdateTeachingPlanInfo(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        /// <summary>
        /// 获取子教案列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]

        public ApiResult<TeachingPlanInfoListResponse> GetTeachingPlanInfoList([FromBody]IdListRequest request)
        {
            var res = base.CreateObject<TeachingPlanInfoListResponse>();
            var bo = new IdListBO()
            {

                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotNum = 0
            };
            res.List = mSession.mTeacherBLL.GetTeachingPlanInfoList(bo);
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }


        /// <summary>
        /// 获取教案单个
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<TeachingPlanInfoResponse> GetTeachingPlanInfo(string Id)
        {
            var res = base.CreateObject<TeachingPlanInfoResponse>();
            res.Model = mSession.mTeacherBLL.GetTeachingPlanInfo(Id);
            return base.CallbackData(res);
        }


        #endregion

        #region 学生课程
        /// <summary>
        /// 获取课程表(按校区获取或者所有）
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<TeacherCourseShowListResponse> GetTeacherCourseShowList(int day, string schoolId = "")
        {
            var res = base.CreateObject<TeacherCourseShowListResponse>();
            var re = mSession.mTeacherBLL.GetTeacherShowList(day, schoolId);
            res.List = re;
            return base.CallbackData(res);
        }

        /// <summary>
        /// 获取单条数据课程表单条数据(通过_TeacherCourse表编号)
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<TeacherCourseShowResponse> GetTeacherCourseShow(string id)
        {
            var res = base.CreateObject<TeacherCourseShowResponse>();
            var re = mSession.mTeacherBLL.GetTeacherShow(id);
            var teachingmo = mSession.mTeacherBLL.GetTeachingPlan(re.TeachingPlanId);
            var teachingInfoList = mSession.mTeacherBLL.GetTeachingPlanInfoList(new IdListBO { Id = teachingmo.Id });
            var s = mSession.mStudentCourseBLL.GetListStudentCourseClass(new StudentCourseClassQueryBO { TeacherCourseId = re.Id });
            res.CourseModel = re;
            res.StudentClassListt = s;
            res.TeachingInfoList = teachingInfoList;
            res.TeachingModel = teachingmo;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 内部订场
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveTeacherCourse([FromBody] TeacherCourseRequest request)
        {
            var bo = new TeacherCourseBO();
            ObjectHelper.AutoMapping(request, bo);
            bo.UserId = mActionContext.WxLoginUser.Id;
            var re = mSession.mTeacherBLL.CreateOrUpdateTeacherCourse(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        /// <summary>
        /// 取消订场
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> EscTeacherCourse([FromBody] IdRequest request)
        {
            var re = mSession.mTeacherBLL.EscTeacherCourse(request.Id, mActionContext.WxLoginUser.Id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }


        #endregion


        #region 学生卡卷
        /// <summary>
        /// 获取用户卡卷
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]

        public ApiResult<StudentKjListVORsponese> GetStudentKjList([FromBody]IdListKjRequest request)
        {
            var res = base.CreateObject<StudentKjListVORsponese>();
            var bo = new IdStatusListBO { Id = mActionContext.WxLoginUser.UnionId, CategoryId = request.CategoryId, Status = request.Status, PageIndex = request.PageIndex, PageSize = request.PageSize };
            var re = mSession.mstudentBLL.GetKjList(bo);
            res.List = re;
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 获取卡卷数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<IdResponse> GetStudentKjNum(string categoryId)
        {
            var res = base.CreateObject<IdResponse>();
            var re = mSession.mstudentBLL.GetKjCount(mActionContext.WxLoginUser.UnionId, categoryId);
            res.Model = re.ToString();
            return base.CallbackData(res);
        }
        /// <summary>
        /// 获取卡卷数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<KjInfoResponse> GetStudentKjByCatrgoryId(string categoryId)
        {
            var res = base.CreateObject<KjInfoResponse>();
            if (categoryId == "5")
                categoryId = "1";
            else
                categoryId = "0";
            var re = mSession.mstudentBLL.GetKjCount(mActionContext.WxLoginUser.UnionId, categoryId);
            if (re > 0)
            {
                var mo = mSession.mstudentBLL.StudengKjFirst(mActionContext.WxLoginUser.UnionId, categoryId);
                res.Price = mo.Name;
                res.Id = mo.Id;
            }
            res.Num = re;
            return base.CallbackData(res);
        }


        ///// <summary>
        ///// 获取卡卷数量
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public ApiResult<IdResponse> GetStudentKjNumByCategory(string categoryId)
        //{
        //    var res = base.CreateObject<IdResponse>();
        //    var re = mSession.mstudentBLL.GetKjCount(mActionContext.WxLoginUser.UnionId, categoryId);
        //    res.Model = re.ToString();
        //    return base.CallbackData(res);
        //}

        #endregion

        #region 约课学生报名课程
        /// <summary>
        /// 添加学生课程 学生约课
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveStudentCourseClass([FromBody] IdCouRequest request)
        {
            var re = mSession.mStudentCourseBLL.CreateOrUpdateStudentClass(mActionContext.WxLoginUser.Id, request.TeacherStudentCourseId, request.StudentCourseId);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        /// <summary>
        /// 约课获取老师列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<TeacherSelListResponse> GetApplyTeacherList()
        {
            var res = base.CreateObject<TeacherSelListResponse>();
            res.List = mSession.mStudentCourseBLL.GetTeacherList();
            return base.CallbackData(res);
        }
        /// <summary>
        /// 获取校区
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<KeyValueResponse> GetSchoolByTeacherId(string id)
        {
            var res = base.CreateObject<KeyValueResponse>();
            res.List = mSession.mStudentCourseBLL.GetSchoolByTeacherId(id);
            return base.CallbackData(res);
        }

        /// <summary>
        /// 根据老师校区添加时间获取老师接口
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<TeacherCourseFieldListResponse> GetSchoolByTeacherId([FromBody] TeacherSchoolCategoryRequset request)
        {
            var res = base.CreateObject<TeacherCourseFieldListResponse>();

            res.List = mSession.mStudentCourseBLL.GetTeacherCourseByTeacherSchool(request.TeacherId, request.SchoolId, request.StudentCourserId, request.AddDay);
            return base.CallbackData(res);
        }

        #endregion


        #region 教练课程列表
        /// <summary>
        /// 获取教师下学生
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<TeacherStudentListVOResponse> GetTeacherStudentList([FromBody]IdListRequest request)
        {
            var res = base.CreateObject<TeacherStudentListVOResponse>();
            if (string.IsNullOrEmpty(request.Id))
                request.Id = mActionContext.WxLoginUser.Id;
            res.List = mSession.mTeacherBLL.GetStudentCourseListByTeacher(new IdListBO { Key = request.Key, Id = request.Id });
            return base.CallbackData(res);
        }

        /// <summary>
        /// 获取课程下学生作业
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<TeacherStudentCourseClassResponse> GetListStudentCourseClassByTeacher([FromBody]StudentCourseClassQueryBORequest request)
        {
            var res = base.CreateObject<TeacherStudentCourseClassResponse>();
            var bo = new StudentCourseClassQueryBO();
            ObjectHelper.AutoMapping(request, bo);
            if (string.IsNullOrEmpty(bo.StudentId))
                bo.StudentId = mActionContext.WxLoginUser.Id;
            bo.Status = request.Status;
            res.List = mSession.mTeacherBLL.GetListStudentCourseClassByTeacher(bo);
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }

        /// <summary>
        /// 老师布置作业
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveTeacherJob([FromBody] JobStudentTeacherAddJobRequest request)
        {
            var re = mSession.mStudentCourseBLL.SaveTeacherAddJob(request.Id, request.JobName, request.VideoId, mActionContext.WxLoginUser.Id, request.JobPoints, request.trainNum, request.trainTime);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        /// <summary>
        /// 学生完成作业
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost] 
        public ApiResult<CommonResponse> SaveStudentJob([FromBody] JobStudentStudentAddJobRequest request)
        {
            var re = mSession.mStudentCourseBLL.SaveStudentAddJob(request.Id, request.VideoUrl, mActionContext.WxLoginUser.Id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        /// <summary>
        /// 获取教案详情id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<StudentCourseClassRsponese> GetStudentCourseClassId([FromBody] JobStudentStudentAddJobRequest request)
        {
            var res = base.CreateObject<StudentCourseClassRsponese>();
            res.StudentCourseClass = mSession.mStudentCourseBLL.GetOneStudentCourseClass(request.Id);
            return base.CallbackData(res);
        }


        /// <summary>
        /// 体验课秀一秀
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveTyCouserVideoUrl([FromBody] CourseVideoUrlRequest request)
        {
            var re = mSession.mStudentCourseBLL.SaveTyCouserVideoUrl(request.Id, request.CourseVideoUrl, mActionContext.WxLoginUser.Id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        /// <summary>
        /// 学生评价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveStudentEvaluate([FromBody]JobStudentStudentEvaluateRequest request)
        {
            var re = mSession.mStudentCourseBLL.SaveStudentEvaluate(request.Id, request.TeacherEvaluate, request.StudentStars, mActionContext.WxLoginUser.Id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        /// <summary>
        /// 老师评价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveTeacherEvaluate([FromBody]JobStudentTeacherEvaluateRequest request)
        {
            var re = mSession.mStudentCourseBLL.SaveTeacherEvaluate(request.Id, request.StudentEvaluate, mActionContext.WxLoginUser.Id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        /// <summary>
        /// 秀一秀
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost] 
        //[schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CommonResponse> SaveCourseVideoUrl([FromBody]CourseVideoUrlRequest request)
        {
            //var re = mSession.mStudentCourseBLL.SaveCourseVideoUrl(request.Id, request.CourseVideoUrl, "mActionContext.WxLoginUser.Id");
            var re = mSession.mStudentCourseBLL.SaveCourseVideoUrl(request.Id, request.CourseVideoUrl, mActionContext.WxLoginUser.Id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        /// <summary>
        /// 作业评价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveJobEvaluate([FromBody]JobEvaluateRequest request)
        {
            var re = mSession.mStudentCourseBLL.SaveJobEvaluate(request.Id, request.JobEvaluate, mActionContext.WxLoginUser.Id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        #endregion

        #region  学生获取课表
        /// <summary>
        /// 获取课程下学生作业
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<TeacherStudentCourseClassResponse> GetListStudentCourseClassByStudent([FromBody]StudentCourseClassQueryStudentRequest request)
        {
            var res = base.CreateObject<TeacherStudentCourseClassResponse>();
            var bo = new StudentCourseClassQueryBO()
            {
                Flg = request.Flg,
                Status = request.Status,
                StudentId = mActionContext.WxLoginUser.Id
            };
            res.List = mSession.mStudentCourseBLL.GetListStudentCourseClassByStudent(bo);
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }


        /// <summary>
        /// 获取单条数据课程表单条数据(通过_StudengCourseClass表编号)
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<StudentCourseClassShowResponse> GetStudengCourseClassShow(string id)
        {
            var res = base.CreateObject<StudentCourseClassShowResponse>();
            var s = mSession.mStudentCourseBLL.GetOneStudentCourseClass(id);
            var re = mSession.mTeacherBLL.GetTeacherShow(s.TeacherCourseId);
            var teachingmo = mSession.mTeacherBLL.GetTeachingPlan(re.TeachingPlanId);
            var teachingInfoList = mSession.mTeacherBLL.GetTeachingPlanInfoList(new IdListBO { Id = teachingmo.Id });
            res.CourseModel = re;
            res.StudentCourseClass = s;
            res.TeachingInfoList = teachingInfoList;
            res.TeachingModel = teachingmo;
            return base.CallbackData(res);
        }
        #endregion


        #region 消息推送 
        /// <summary>
        /// 微信模板消息推送
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SendMessage([FromBody]SendMessageRequest request)
        {
            var ms = mSession.mStudentCourseBLL.GetOneStudentCourseClass(request.StudentClassId);
            var dic = new Dictionary<string, MessageContent>();
            //dic.Add("first", new MessageContent { value = "教练提醒你按时完成作业了" });

            dic.Add("name1", new MessageContent { value = ms.TeacherCourseName });

            dic.Add("time2", new MessageContent { value = ms.Day.ToString() + " - " + ms.TeacherCourseTime });
            dic.Add("thing3", new MessageContent { value = ms.FieldName });

            dic.Add("thing4", new MessageContent { value = "教练提醒你按时完成作业了" });
            var stu = mSession.mstudentBLL.GetOne(ms.StudentId);
            var bo = new SendMessageSMBO()
            {
                data = dic,
                template_id = "T483OYclxiJwi6jDJ3oCzV4-XLobJgolTBaqsWGtKPg",
                touser = stu.SmOpenId
            };
            WxUserLibApi.SendMessage(mSession.mWtokenBLL.Get(), bo.ToJson());
            return base.CallbackData(1);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SendMenu([FromBody]MenuRequest request)
        {
            var bo = mSession.mCourseBLL.CreateOrUpdateMenu(request.menuMain, mActionContext.WxLoginUser.Id);
            if (bo.IsSuccess)
            {
                WxUserLibApi.SendMenu(mSession.mWtokenBLL.Get(), request.menuMain);
                return base.CallbackData(1);
            }
            return base.CallbackData(0);
        }

        #endregion

        #region 
        /// <summary>
        /// h获取数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<ClassInfoNumResponse> GetStudentCourseNumInfo()
        {
            var res = base.CreateObject<ClassInfoNumResponse>();
            var mo = mSession.mStudentCourseBLL.GetClassNumInfo(mActionContext.WxLoginUser.Id);
            //var mo = mSession.mStudentCourseBLL.GetClassNumInfo("71951154-5b21-4ea8-9f01-a6e1295b5937");
            
            res.Model = mo;
            return base.CallbackData(res);
        }
        #endregion

        #region
        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CommonResponse> Ste([FromBody]SendMessageRequest request)
        {
            var sasd = request.StudentClassId;
            var s = "";
            return base.CallbackData(1);
        }
        #endregion


        #region  学校
        public ApiResult<CommonResponse> CreateOrUpdeteSchool([FromBody]SchoolRequest modelRequest)
        {
            var modelBo = new SchoolBO();
            modelBo.Id = modelRequest.Id;
            ObjectHelper.AutoMapping(modelRequest, modelBo);
            var bo = mSession.mFieldBLL.CreateOrUpdateSchool(modelBo);
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        [HttpGet]
        public ApiResult<CommonResponse> DeleteSchool(string id)
        {
            var bo = mSession.mFieldBLL.DeleteSchool(id);
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        #endregion

        #region 删除
        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<CommonResponse> DeleteDic(string id)
        {
            var bo = mSession.mTeacherBLL.DeleteDic(id);
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        #endregion

        #region ///场地
        [HttpPost]
        public ApiResult<CommonResponse> CreateOrUpdeteField([FromBody]FieldRequest modelRequest)
        {
            var modelBo = new FieldBO();
            modelBo.Id = modelRequest.Id;
            ObjectHelper.AutoMapping(modelRequest, modelBo);
            var bo = mSession.mFieldBLL.CreateOrUpdate(modelBo);
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        [HttpGet]
        public ApiResult<CommonResponse> DeleteField(string Id)
        {
            var bo = mSession.mFieldBLL.DeleteField(Id);
            return bo.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        #endregion

        #region 活动
        /// <summary>
        /// 内部订场
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CommonResponse> SaveActive([FromBody]ActityRequest request)
        {
            var bo = new ActivityBO();
            ObjectHelper.AutoMapping(request, bo);
            bo.UserId = mActionContext.WxLoginUser.Id;
            bo.StudentId = mActionContext.WxLoginUser.Id;
            var re = mSession.mTeacherBLL.CreateOrUpdateActivity(bo);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        //[HttpGet]
        //public ApiResult<CommonResponse> GetSubscribeList(string id,string categoryId)
        //{
        //    var re = mSession.mTeacherBLL.EscActivity(id, mActionContext.WxLoginUser.Id);
        //    return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        //}

        /// <summary>
        /// 获取单个1为老师
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<ActivityAllVOResponse> GetActive(string id, int isTeacher)
        {
            var res = base.CreateObject<ActivityAllVOResponse>();
            var models = mSession.mTeacherBLL.GetOneActivityVO(id, isTeacher == 1);
            res.Model = models;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 获取活动列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [schoolActionFilter(IsNeedLogin = false)]
        public ApiResult<ActivityListVOResponse> GetActiveList([FromBody] ActiveListRequest request)
        {
            var res = base.CreateObject<ActivityListVOResponse>();
            var bo = new IdActiStatusListBO()
            {
                Day = request.Day,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Status = request.Status,
                StatusInfo = request.StatusInfo,
                Id = request.Id
            };
            var models = mSession.mTeacherBLL.GetActivityList(bo);
            res.List = models;
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 取消活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<CommonResponse> EscActive(string id)
        {
            var re = mSession.mTeacherBLL.EscActivity(id, mActionContext.WxLoginUser.Id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }


        /// <summary>
        /// 报名活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<IdResponse> SaveStudentActive([FromBody]ActivityStudentRequest request)
        {
            var res = base.CreateObject<IdResponse>();
            var bo = new ActivityStudentBO();
            ObjectHelper.AutoMapping(request, bo);
            bo.StudentId = mActionContext.WxLoginUser.Id;
            bo.StudentName = request.StudentName;
            bo.StudentTel = request.StudentTel;
            var re = mSession.mTeacherBLL.CreateOrUpdateActivityStudent(bo);
            res.Model = re;
            return base.CallbackData(res);
            //return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }
        /// <summary>
        /// z状态更新
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>GetActive
        [HttpPost]
        public ApiResult<CommonResponse> UpdateActivityStudentStatus([FromBody]IdRequest request)
        {
            var re = mSession.mTeacherBLL.UpdateActivityStudentStatus(request.Id, Int32.Parse(request.Status), mActionContext.WxLoginUser.Id);
            return re.IsSuccess ? base.CallbackData(1) : ApiResultHelper.SubError(R("v5"));
        }

        /// <summary>
        /// 获取订购时间category 0为内部订场课程 ，1为学生订单，2为活动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<SubListReVOResponse> GetSubListByCategory(string id, int category)
        {
            var res = base.CreateObject<SubListReVOResponse>();
            var models = mSession.mTeacherBLL.GetSubListById(id, category);
            res.Model = models;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 学生获取报名项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<ActivityStudentListByStudentResponse> GetActivityStudentListByStudent([FromBody] ActiveListRequest request)
        {
            var res = base.CreateObject<ActivityStudentListByStudentResponse>();
            var bo = new IdStudentStatusListBO()
            {
                Id = mActionContext.WxLoginUser.Id,
                PageIndex = request.PageIndex,
                StudnetId = mActionContext.WxLoginUser.Id,
                PageSize = request.PageSize
            };
            var models = mSession.mTeacherBLL.GetActivityStudentListByStudent(bo);
            res.List = models;
            res.TotNum = bo.TotNum;
            return base.CallbackData(res);
        }
        /// <summary>
        /// 老师获取项目下学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<TeackerActivityStudentVOResponse> GetSubListByCategory(string id)
        {
            var res = base.CreateObject<TeackerActivityStudentVOResponse>();
            var models = mSession.mTeacherBLL.GetActivityStudentByTeacher(id);
            res.Model = models.Model;
            res.List = models.List;
            return base.CallbackData(res);
        }
        #endregion

        #region 获取报名课程列表
        [HttpPost]
        public ApiResult<CourseListRsponese> GetCourseList([FromBody] CourseRequest request)
        {
            var res = base.CreateObject<CourseListRsponese>();
            var que = mSession.mCourseBLL.GetCourseList(request);
            res.List = que;
            return base.CallbackData(res);
        }
        #endregion

        #region 获取转课的剩余课时
        [HttpPost]
        public ApiResult<CourseRsponese> GetSurplusClassTimes([FromBody] StudentCour3Request request)
        {
            var res = base.CreateObject<CourseRsponese>();
            var que = mSession.mCourseBLL.GetSurplusClassTimes(request);
            res.Model = que;
            return base.CallbackData(res);
        }
        #endregion

        #region 学员转课
        [HttpPost]
        public ApiResult<CommonResponse> TransferStudentCourse([FromBody] StudentCour3Request request)
        {
            var res = base.CreateObject<CommonResponse>();
            var userIdo = mActionContext.WxLoginUser.Id;
            request.UserId = userIdo;
            var re = mSession.mStudentCourseBLL.TransferStudentCourse(request);
            return base.CallbackData(res);
        }
        #endregion
    }



}

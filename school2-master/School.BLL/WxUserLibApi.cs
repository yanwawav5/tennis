using school.Common;
using school.Common.RPC;
using school.Model;
using school.Model.Enum;
using school.Model.TO;
using school.Model.TO.Request;
using school.Model.TO.Response;
using school.Model.VO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;

using Aliyun.Acs.Dysmsapi.Model.V20170525;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace school.BLL
{

    public class WxUserLibApi
    {
        static String product = "Dysmsapi";//短信API产品名称
        static String domain = "dysmsapi.aliyuncs.com";//短信API产品域名
        static String accessId = "LTAI4Fo7mD6T3E7BSDUh5JzD";
        static String accessSecret = "coHgqVsrDroAbEdvGTwIScgOfqk67t";
        static String regionIdForPop = "cn-hangzhou";

        public static bool sends(string phone, string code)
        {
            IClientProfile profile = DefaultProfile.GetProfile(regionIdForPop, accessId, accessSecret);
            DefaultProfile.AddEndpoint(regionIdForPop, regionIdForPop, product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                request.PhoneNumbers = phone;
                request.SignName = "昱隆网球";
                request.TemplateCode = "SMS_183766930";
                request.TemplateParam = "{\"code\":\"" + code + "\"}";
                request.OutId = "xxxxxxxx";
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                System.Console.WriteLine(sendSmsResponse.Message);
                if (sendSmsResponse.Message.ToLower() == "ok")
                {
                    return true;
                }
                else
                {

                    Log.Error(new String[] { "短信发送失败", sendSmsResponse.ToJson() });
                    throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + sendSmsResponse.Message);
                }

            }
            catch (ServerException e)
            {
                Log.Error(new String[] { "短信错误", e.ErrorMessage });
                throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + e.ErrorMessage);
            }
            catch (ClientException e)
            {
                Log.Error(new String[] { "短信错误", e.ErrorMessage });
                throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + e.ErrorMessage);
            }
            return true;
        }


        public static bool sendsSchool(string phone, string school,string day,string field,string tel)
        {
            IClientProfile profile = DefaultProfile.GetProfile(regionIdForPop, accessId, accessSecret);
            DefaultProfile.AddEndpoint(regionIdForPop, regionIdForPop, product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //request.SignName = "上云预发测试";//"管理控制台中配置的短信签名（状态必须是验证通过）"
                //request.TemplateCode = "SMS_71130001";//管理控制台中配置的审核通过的短信模板的模板CODE（状态必须是验证通过）"
                //request.RecNum = "13567939485";//"接收号码，多个号码可以逗号分隔"
                //request.ParamString = "{\"name\":\"123\"}";//短信模板中的变量；数字需要转换为字符串；个人用户每个变量长度必须小于15个字符。"
                //SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
                request.PhoneNumbers = phone;
                request.SignName = "昱隆网球学院";
                request.TemplateCode = "SMS_204296063";
                request.TemplateParam = "{\"school\":\"" + school + "\",\"field\":\"" + field + "\",\"day\":\"" + day + "\",\"phone\":\"" + tel + "\"}";
                request.OutId = "xxxxxxxx";
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                System.Console.WriteLine(sendSmsResponse.Message);
                if (sendSmsResponse.Message.ToLower() == "ok")
                {
                    return true;
                }
                else
                {

                    Log.Error(new String[] { "短信发送失败", sendSmsResponse.ToJson() });
                    throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + sendSmsResponse.Message);
                }

            }
            catch (ServerException e)
            {
                Log.Error(new String[] { "短信错误", e.ErrorMessage });
                throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + e.ErrorMessage);
            }
            catch (ClientException e)
            {
                Log.Error(new String[] { "短信错误", e.ErrorMessage });
                throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + e.ErrorMessage);
            }
            return true;
        }

        public static bool sendTransferStudentCourse(string phone, string old_course, string course, string classTimes)
        {
            IClientProfile profile = DefaultProfile.GetProfile(regionIdForPop, accessId, accessSecret);
            DefaultProfile.AddEndpoint(regionIdForPop, regionIdForPop, product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //request.SignName = "上云预发测试";//"管理控制台中配置的短信签名（状态必须是验证通过）"
                //request.TemplateCode = "SMS_71130001";//管理控制台中配置的审核通过的短信模板的模板CODE（状态必须是验证通过）"
                //request.RecNum = "13567939485";//"接收号码，多个号码可以逗号分隔"
                //request.ParamString = "{\"name\":\"123\"}";//短信模板中的变量；数字需要转换为字符串；个人用户每个变量长度必须小于15个字符。"
                //SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
                request.PhoneNumbers = phone;
                request.SignName = "昱隆网球学院";
                request.TemplateCode = "SMS_214515231";
                request.TemplateParam = "{\"old_course\":\"" + old_course + "\",\"course\":\"" + course + "\",\"num\":\"" + classTimes + "\"}";
                request.OutId = "xxxxxxxx";
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                System.Console.WriteLine(sendSmsResponse.Message);
                if (sendSmsResponse.Message.ToLower() == "ok")
                {
                    return true;
                }
                else
                {

                    Log.Error(new String[] { "短信发送失败", sendSmsResponse.ToJson() });
                    throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + sendSmsResponse.Message);
                }

            }
            catch (ServerException e)
            {
                Log.Error(new String[] { "短信错误", e.ErrorMessage });
                throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + e.ErrorMessage);
            }
            catch (ClientException e)
            {
                Log.Error(new String[] { "短信错误", e.ErrorMessage });
                throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + e.ErrorMessage);
            }
            return true;
        }

        public static bool sendStudentLeave(string phone, string course)
        {
            IClientProfile profile = DefaultProfile.GetProfile(regionIdForPop, accessId, accessSecret);
            DefaultProfile.AddEndpoint(regionIdForPop, regionIdForPop, product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //request.SignName = "上云预发测试";//"管理控制台中配置的短信签名（状态必须是验证通过）"
                //request.TemplateCode = "SMS_71130001";//管理控制台中配置的审核通过的短信模板的模板CODE（状态必须是验证通过）"
                //request.RecNum = "13567939485";//"接收号码，多个号码可以逗号分隔"
                //request.ParamString = "{\"name\":\"123\"}";//短信模板中的变量；数字需要转换为字符串；个人用户每个变量长度必须小于15个字符。"
                //SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
                request.PhoneNumbers = phone;
                request.SignName = "昱隆网球学院";
                request.TemplateCode = "SMS_214515239";
                request.TemplateParam = "{\"course\":\"" + course + "\"}";
                request.OutId = "xxxxxxxx";
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                System.Console.WriteLine(sendSmsResponse.Message);
                if (sendSmsResponse.Message.ToLower() == "ok")
                {
                    return true;
                }
                else
                {

                    Log.Error(new String[] { "短信发送失败", sendSmsResponse.ToJson() });
                    throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + sendSmsResponse.Message);
                }

            }
            catch (ServerException e)
            {
                Log.Error(new String[] { "短信错误", e.ErrorMessage });
                throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + e.ErrorMessage);
            }
            catch (ClientException e)
            {
                Log.Error(new String[] { "短信错误", e.ErrorMessage });
                throw new schoolException(SubCode.Failure.GetHashCode(), "发送失败" + e.ErrorMessage);
            }
            return true;
        }

        #region 用户登录

        /// <summary>
        /// 用户登录
        /// </summary>
        public static WxSmallInfoVO Login(WxLoginRequest request)
        {
            if (String.IsNullOrEmpty(request.Code)) throw new schoolException(SubCode.Failure.GetHashCode(), "请输入code");
            var response = HttpHelper.Get(Config.WxSmallApiURL + "sns/jscode2session?appid=" + Config.WxSmallAppKey + "&secret=" + Config.WxSmallAppSecret + "&js_code=" + request.Code + "&grant_type=authorization_code", "", "", "");
            var wx = JsonConvert.DeserializeObject<WxSmallInfoVO>(response);
            if (!string.IsNullOrEmpty(wx.errmsg))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            Log.Error(new String[] { "获取小程序登录信息", response });
            return (WxSmallInfoVO)Convert.ChangeType(wx, typeof(WxSmallInfoVO));
        }

        #endregion

        #region
        /// <summary>
        /// 获取Token
        /// </summary>
        public static WxSmallTokenVO GetToken()
        {
            var response = HttpHelper.Get("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + Config.WxAppKey + "&secret=" + Config.WxAppSecret, "", "", "");

            Log.Error(new String[] { "response", response });
            var wx = JsonConvert.DeserializeObject<WxSmallTokenVO>(response);
            if (!string.IsNullOrEmpty(wx.errmsg))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            return (WxSmallTokenVO)Convert.ChangeType(wx, typeof(WxSmallTokenVO));
        }
        #endregion

        #region
        /// <summary>
        /// 获取Token
        /// </summary>
        public static WxSmallTokenVO GetWXSMToken()
        {
            var response = HttpHelper.Get("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + Config.WxSmallAppKey + "&secret=" + Config.WxSmallAppSecret, "", "", "");

            Log.Error(new String[] { "response", response });
            var wx = JsonConvert.DeserializeObject<WxSmallTokenVO>(response);
            if (!string.IsNullOrEmpty(wx.errmsg))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            return (WxSmallTokenVO)Convert.ChangeType(wx, typeof(WxSmallTokenVO));
        }
        #endregion

        #region
        /// <summary>
        /// 获取Js
        /// </summary>
        public static WxJsVO GetJs(string token)
        {
            var response = HttpHelper.Get("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + token + "&type=jsapi", "", "", "");

            Log.Error(new String[] { "response", response });
            var wx = JsonConvert.DeserializeObject<WxJsVO>(response);
            if (wx.errcode != 0)
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            return (WxJsVO)Convert.ChangeType(wx, typeof(WxJsVO));
        }
        #endregion



        public static string DecodeUserInfo(string raw, string signature, string encryptedData, string iv)
        {

            byte[] iv2 = Convert.FromBase64String(iv);

            if (string.IsNullOrEmpty(encryptedData)) return "";
            Byte[] toEncryptArray = Convert.FromBase64String(encryptedData);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Convert.FromBase64String("session_key"),
                IV = iv2,
                Mode = System.Security.Cryptography.CipherMode.CBC,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);

        }

        #region
        /// <summary>
        /// 获取Token
        /// </summary>
        public static WxSmallTokenVO GetMonthlyRetain(string token)
        {
            var response = HttpHelper.Post("https://api.weixin.qq.com/datacube/getweanalysisappidmonthlyvisittrend?access_token=" + token, "", "", "", "");
            var wx = JsonConvert.DeserializeObject<WxSmallTokenVO>(response);
            if (!string.IsNullOrEmpty(wx.errmsg))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            return (WxSmallTokenVO)Convert.ChangeType(wx, typeof(WxSmallTokenVO));
        }
        #endregion

        /// <summary>
        /// 请求用户库API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="api"></param>
        /// <param name="json"></param>
        /// <returns></returns>


        #region 微信网页授权

        public static OauthAccessTokenVO GetAccessToken(string code)
        {
            var response = HttpHelper.Get("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + Config.WxAppKey + "&secret=" + Config.WxAppSecret + "&code=" + code + "&grant_type=authorization_code", "", "", "");
            var wx = JsonConvert.DeserializeObject<OauthAccessTokenVO>(response);
            if (!string.IsNullOrEmpty(wx.errmsg))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            return (OauthAccessTokenVO)Convert.ChangeType(wx, typeof(OauthAccessTokenVO));
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        public static OauthAccessTokenVO GetRefreshAccessToken(string accesstoken)
        {
            var response = HttpHelper.Get("https://api.weixin.qq.com/sns/oauth2/refresh_token?appid=" + Config.WxAppKey + "&grant_type=refresh_token&refresh_token=" + accesstoken, "", "", "");
            var wx = JsonConvert.DeserializeObject<OauthAccessTokenVO>(response);
            if (!string.IsNullOrEmpty(wx.errmsg))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            return (OauthAccessTokenVO)Convert.ChangeType(wx, typeof(OauthAccessTokenVO));
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static WechatUserInfoVO GetRefreshAccessToken(string accesstoken, string openId)
        {
            var response = HttpHelper.Get("https://api.weixin.qq.com/sns/userinfo?access_token=" + accesstoken + "&openid=" + openId + "&lang=zh_CN", "", "", "");

            var wx = JsonConvert.DeserializeObject<WechatUserInfoVO>(response);
            if (!string.IsNullOrEmpty(wx.errmsg))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            return (WechatUserInfoVO)Convert.ChangeType(wx, typeof(WechatUserInfoVO));
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static WechatUserInfoVO GetUnionID(string basetoken, string openId)
        {
            var response = HttpHelper.Get("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + basetoken + "&openid=" + openId + "&lang=zh_CN", "", "", "");
            Log.Error(new String[] { "返回微信GetUnionID", response });

            var wx = JsonConvert.DeserializeObject<WechatUserInfoVO>(response);
            if (!string.IsNullOrEmpty(wx.errmsg))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            
            return (WechatUserInfoVO)Convert.ChangeType(wx, typeof(WechatUserInfoVO));
        }
        public static bool SendMenu(string token, string data)
        {
            var response = HttpHelper.Post("https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + token, data, "", "", "");

            Log.Error(new String[] { "response", response });
            var wx = JsonConvert.DeserializeObject<wxre>(response);
            if (!string.IsNullOrEmpty(wx.errmsg)&&wx.errcode!="0")
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            return true;
        }


        //   public static https://api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN

        #endregion


        public static bool  SendMessage(string token,string data)
        {
            var response = HttpHelper.Post("https://api.weixin.qq.com/cgi-bin/message/subscribe/send?access_token=" + token, data, "", "","");

            Log.Error(new String[] { "response", response });
            var wx = JsonConvert.DeserializeObject<wxre>(response);
            
            if (!string.IsNullOrEmpty(wx.errcode) && wx.errcode != "0")
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);
            }
            return true;
        }
       

        #region x小程序
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt(string text, string password, string iv)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = Convert.FromBase64String(password);

            rijndaelCipher.Key = pwdBytes;

            if (!string.IsNullOrEmpty(iv))
            {
                byte[] ivBytes = Convert.FromBase64String(iv);
                rijndaelCipher.IV = ivBytes;
            }
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);

            return Encoding.UTF8.GetString(plainText);

        }
        public static WxUserInfoUnion GetMiniAppUserUnionID(string code, string userInfo, string iv)
        {

            if (String.IsNullOrEmpty(code)) throw new schoolException(SubCode.Failure.GetHashCode(), "请输入code");
            var response = HttpHelper.Get(Config.WxSmallApiURL + "sns/jscode2session?appid=" + Config.WxSmallAppKey + "&secret=" + Config.WxSmallAppSecret + "&js_code=" + code + "&grant_type=authorization_code", "", "", "");
            var wx = JsonConvert.DeserializeObject<WxSmallInfoVO>(response);


            //var obj = Parse(res); 
            if (!string.IsNullOrEmpty(wx.errmsg))
            {
                Log.Error(new String[] { "获取小程序登录信息", response });
                throw new schoolException(SubCode.Failure.GetHashCode(), wx.errmsg);

            }
            var userJson = AESDecrypt(userInfo, wx.session_key, iv);
            Log.Error(new String[] { "获取小程序登录信息揭盲00", userJson });
            var userobj = JsonConvert.DeserializeObject<WxUserInfoUnion>(userJson);
            userobj.SessionKey = wx.session_key;
            Log.Error(new String[] { "获取小程序登录信息揭盲11", userobj.ToJson() });
            

            if (string.IsNullOrEmpty(userobj.unionid))
            {
                throw new schoolException(SubCode.Failure.GetHashCode(), "未获取UnionId");
            }


            return userobj;
        }

        public static UserTelVO GetUserTelVO(string key, string userInfo, string iv)
        { 
            JObject jo = new JObject();
            iv = iv.Replace(" ", "+");
            key = key.Replace(" ", "+");
            userInfo = userInfo.Replace(" ", "+");
            byte[] encryptedData = Convert.FromBase64String(userInfo); 
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Key = Convert.FromBase64String(key); // Encoding.UTF8.GetBytes(AesKey);
            rijndaelCipher.IV = Convert.FromBase64String(iv);// Encoding.UTF8.GetBytes(AesIV);
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            string result = Encoding.UTF8.GetString(plainText);
            UserTelVO vo = JsonConvert.DeserializeObject<UserTelVO>(result); 
            return vo; 

            //} 
            //var wx = Login(new WxLoginRequest { Code = code });
            //var userJson = AESDecrypt(userInfo, wx.session_key, iv);
            //Log.Error(new String[] { "获取电话GetUserTelVO", userJson });
            //UserTelVO vo = JsonConvert.DeserializeObject<UserTelVO>(userJson);
            //return vo;
            //if (string.IsNullOrEmpty(vo.phoneNumber))
            //{
            //    throw new schoolException(SubCode.Failure.GetHashCode(), "未获取UnionId");
            //}

        }
        #endregion


    }

}

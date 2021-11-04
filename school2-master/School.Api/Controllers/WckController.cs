using school.API.Filter;
using school.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Security;

namespace school.Api.Controllers
{
    public class WckController : ApiController
    {
        #region
        /// <summary>
        /// 验证消息真实性
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        [HttpGet]
        [schoolActionFilter(IsCk = false)]
        public string checkSignature(string signature, string timestamp, string nonce, string echostr)
        {

            var token = Config.Token;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("token", token);
            dic.Add("nonce", nonce);
            dic.Add("timestamp", timestamp);
            var list = dic.OrderBy(s => s.Value);
            var conbineStr = "";
            foreach (var s in list)
            {
                conbineStr = conbineStr + s.Value;
            }
            string data = conbineStr;
            //sha1加密
            string secret = FormsAuthentication.HashPasswordForStoringInConfigFile(conbineStr, "SHA1").ToLower();
            var success = signature == secret;
            if (success)
            {
                data = echostr;
            }
            return JsonConvert.SerializeObject(data);
        }
        //}





        /// <summary>
        /// sha1加密
        /// </summary>
        /// <param name = "text" ></ param >
        /// < returns ></ returns >
        private string sha1(string text)
        {
            var encoding = Encoding.UTF8;
            var buffer = encoding.GetBytes(text);
            var cryptoTransformSHA1 =
                new SHA1CryptoServiceProvider();
            var hash = BitConverter.ToString(
                cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash.ToLower();
        }
        #endregion

        //}
    }
}

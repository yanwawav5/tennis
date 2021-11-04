using school.Common;
using school.Model.Enum;
using school.Model.VO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace school.Api.WePay
{
    public class WXPay
    {
        private static string APP_ID = Config.WxSmallAppKey;
        private static string APP_SECRET = Config.WxSmallAppSecret;
        private static string MCH_ID = "1577085301";
        public static string KEY = "c95076c69bb8d736616e5ce06f3bced6";
        private static string NOTIFY_URL = "http://yulong.yulongtennis.com/wapi/wadmin/callback";
        private static string PAY_URL = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        public static Object Pay(string code,string order_code,int price,string ServerName,string userId)
        {
            Parameters para = new Parameters();
            para.appid = APP_ID;//申请的appid
            para.mchid = MCH_ID;//申请的商户号
            para.nonce_str = GetNoncestr();  //随机字符串
            para.notify_url = NOTIFY_URL;//支付结果回调接口
            para.body = ServerName;//商品描述(商品简单描述，该字段请按照规范传递，具体请见参数规定)
            para.out_trade_no = order_code;//商户订单号
            //para.total_fee =(price*100).ToString();//标价金额
             para.total_fee = price.ToString();//标价金额
            para.spbill_create_ip = "192.168.1.1";//终端IP
            para.trade_type = "JSAPI";//交易类型	
            para.key = KEY;//在商家后台设置的密钥
            para.app_secret = APP_SECRET;//在配置小程序时的密钥
            //if (string.IsNullOrEmpty(userId))
            //{
            //    userId = "37fc141c-d1be-4199-a825-1add77dcab65";
            //}
           

            //用code换取opendid
            //JObject jObject = GetOpendidAndSessionkey(code);//用户唯一标识
            string opendid = userId;
            //string opendid = string.Empty;//会话密钥
            //string session_key = string.Empty;
            //if (jObject.Property("openid") != null && jObject.Property("session_key") != null)
            //{
            //    opendid = jObject["openid"].ToString();
            //   // session_key = jObject["session_key"].ToString();

            //}

            string param = "";
            if (!string.IsNullOrEmpty(opendid))
            {
                //获取统一的下单的请求参数
                param = GetUnifiedOrderParam(opendid, para);
                //统一请求下单
                HttpResponseMessage hrm = (HttpResponseMessage)PostUnifiedOrder(PAY_URL, param);
                if (hrm.StatusCode == HttpStatusCode.OK)
                {
                    //拿到请求的结果
                    string re = hrm.Content.ReadAsStringAsync().Result;

                    var payRes = XDocument.Parse(re);
                    var root = payRes.Element("xml");
                    //序列化相应参数返回给小程序
                    var res = GetPayRequestParam(root, para.appid, para.key);
                    if (res == null)
                    {
                        throw new schoolException(SubCode.Failure.GetHashCode(), re);
                    }
                    return res;
                }
            }
            return null;


        }

        //用code获取opendid
        public static JObject GetOpendidAndSessionkey(string code)
        {
            string url = "https://api.weixin.qq.com/sns/jscode2session?appid=" + APP_ID + "&secret=" + APP_SECRET + "&js_code=" + code + "&grant_type=authorization_code";

            JObject JO = new JObject();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage hrm = httpClient.GetAsync(url).Result;
            if (hrm.StatusCode == HttpStatusCode.OK)
            {
                string re = hrm.Content.ReadAsStringAsync().Result;

                JO = (JObject)JsonConvert.DeserializeObject(re);

            }
            hrm.Dispose();
            httpClient.Dispose();
            return JO;
        }


        //获取统一下单的请求参数
        private static string GetUnifiedOrderParam(string openid, Parameters para)
        {
            //参与统一下单签名的参数，除最后的key外，已经按参数名ASCII码从小到大排序
            string unifiedorderSignParam = string.Format("appid={0}&body={1}&mch_id={2}&nonce_str={3}&notify_url={4}&openid={5}&out_trade_no={6}&spbill_create_ip={7}&total_fee={8}&trade_type={9}&key={10}"
                , para.appid, para.body, para.mchid, para.nonce_str, para.notify_url
                , openid, para.out_trade_no, para.spbill_create_ip, para.total_fee, para.trade_type, para.key);
            //MD5加密并将结果转换成大写
            string unifiedorderSign = GetMD5(unifiedorderSignParam).ToUpper();

            //构造统一下单的请求参数
            return string.Format(@"<xml> 
                                 <appid>{0}</appid>                                              
                                <body>{1}</body>
                                <mch_id>{2}</mch_id>   
                                <nonce_str>{3}</nonce_str>
                                <notify_url>{4}</notify_url>
                                <openid>{5}</openid>
                                <out_trade_no>{6}</out_trade_no>
                                <spbill_create_ip>{7}</spbill_create_ip>
                                <total_fee>{8}</total_fee>
                                <trade_type>{9}</trade_type>
                                <sign>{10}</sign>
                                </xml>", para.appid, para.body, para.mchid, para.nonce_str, para.notify_url, openid
                              , para.out_trade_no, para.spbill_create_ip, para.total_fee, para.trade_type, unifiedorderSign);
        }


        //统一请求下单
        private static Object PostUnifiedOrder(string payUrl, string para)
        {
            string result = string.Empty;
            try
            {
                HttpClient client = new HttpClient();
                HttpContent httpContent = new StringContent(para);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpContent.Headers.ContentType.CharSet = "utf-8";
                HttpResponseMessage hrm = client.PostAsync(payUrl, httpContent).Result;
                return hrm;
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }


        //获取返回给小程序的支付参数
        private static PayRequesEntity GetPayRequestParam(XElement root, string appid, string key)
        {
            //当return_code 和result_code都为SUCCESS时才有我们要的prepay_id
            if (root.Element("return_code").Value == "SUCCESS" && root.Element("result_code").Value == "SUCCESS")
            {
                var package = "prepay_id=" + root.Element("prepay_id").Value;//统一下单接口返回的 prepay_id 参数值
                var nonceStr = GetNoncestr();//获取随机字符串
                var signType = "MD5";//加密方式
                var timeStamp = Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds).ToString();//时间戳

                var paySignParam = string.Format("appId={0}&nonceStr={1}&package={2}&signType={3}&timeStamp={4}&key={5}",
                     appid, nonceStr, package, signType, timeStamp, key);
                //二次加签
                var paySign = GetMD5(paySignParam).ToUpper();

                PayRequesEntity payEntity = new PayRequesEntity
                {
                    package = package,
                    nonceStr = nonceStr,
                    paySign = paySign,
                    signType = signType,
                    timeStamp = timeStamp,
                    appid = Config.WxAppKey,
                    mch_id = MCH_ID
                };
                return payEntity;
            }

            return null;
        }

        //生成随机字符串	
        private static string GetNoncestr()
        {
            //生成随机数方法需要去按照指引下载API证书
            //也可以按照以下路径下载：微信商户平台(pay.weixin.qq.com)-->账户中心-->账户设置-->API安全    

            //详细：https://pay.weixin.qq.com/wiki/doc/api/wxa/wxa_api.php?chapter=4_3

            return Guid.NewGuid().ToString().Replace("-", ""); ;
        }


        //参数实体类
        public class Parameters
        {
            public string appid;//申请的appid
            public string mchid;//申请的商户号
            public string nonce_str;  //随机字符串
            public string notify_url;//支付结果回调接口
            public string body;//商品描述(商品简单描述，该字段请按照规范传递，具体请见参数规定)
            public string out_trade_no;//商户订单号
            public string total_fee;//标价金额
            public string spbill_create_ip;//终端IP
            public string trade_type;//交易类型	
            public string key;//在商家后台设置的密钥
            public string app_secret;//在配置小程序时的密钥
        }
        //小程序支付需要的参数
        //public class PayRequesEntity
        //{
        //    /// <summary>
        //    /// 时间戳从1970年1月1日00:00:00至今的秒数,即当前的时间
        //    /// </summary>
        //    public string timeStamp { get; set; }

        //    /// <summary>
        //    /// 随机字符串，长度为32个字符以下。
        //    /// </summary>
        //    public string nonceStr { get; set; }

        //    /// <summary>
        //    /// 统一下单接口返回的 prepay_id 参数值
        //    /// </summary>
        //    public string package { get; set; }

        //    /// <summary>
        //    /// 签名算法
        //    /// </summary>
        //    public string signType { get; set; }

        //    /// <summary>
        //    /// 签名
        //    /// </summary>
        //    public string paySign { get; set; }
        //}


        //MD5加密
        public static string GetMD5(string str)
        {
            byte[] result = Encoding.Default.GetBytes(str);

            MD5 md5 = new MD5CryptoServiceProvider();
            //计算指定字节数组指定区域的哈希值
            byte[] output = md5.ComputeHash(result);

            StringBuilder tmp = new StringBuilder();
            foreach (byte i in output)
            {
                tmp.Append(i.ToString("x2"));
            }
            return tmp.ToString();
        }
    }
}

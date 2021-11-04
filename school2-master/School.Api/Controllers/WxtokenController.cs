using school.Api.WX;
using school.API.Filter;
using school.Common;
using school.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml;
using Yidu.Common.Tools;
using static school.Api.WX.wxmsg;

namespace school.Api.Controllers
{
    public class WxtokenController : ApiController
    {
        /// <summary>
        /// 接入微信验证登陆
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns> 
        [schoolActionFilter(IsCk = false)]
        [HttpGet]
        public async Task<HttpResponseMessage> CheckToken(string echostr, string signature, string timestamp, string nonce)
        {

            if (!SHA1Helper.CheckSignature(Config.Token, signature, timestamp, nonce))
                echostr = "验证不正确";

            HttpResponseMessage responseMessage = new HttpResponseMessage { Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "text/plain") };
            return responseMessage;
        }

        /// <summary>
        /// 关键字回复
        /// </summary>
        /// <returns></returns>
        [schoolActionFilter(IsCk = false)]
        [HttpPost]
        public async Task<HttpResponseMessage> CheckToken()
        {
            // if (!SHA1Helper.CheckSignature(Config.Token, signature, timestamp, nonce))
            string echostr = "";
            {
                var msg = new ReceiveMsgUtil();

                var reader = new StreamReader(Request.Content.ReadAsStreamAsync().Result);
                string originalMessage = reader.ReadToEnd();

                //await Log.Error(new String[] { "CheckToken-1", "测试"+ System.Web.HttpContext.Current.Request.InputStream.ToString() });
                var model = msg.ReceiveMsg(originalMessage);
                if (null != model)
                {
                    var send = new SendMsgUtil();
                    switch (model.MsgType)
                    {
                        case MsgType.text:
                            // 处理接收到的图片信息
                            // 根据用户的信息回复用户一条信息
                            var sendText = new text_sendmsg()
                            {
                                ToUserName = model.FromUserName,
                                FromUserName = model.ToUserName,
                                Content = @"
Hi！亲爱的朋友，欢迎关注昱隆网球学院！昱隆网球是拥有全知识产权的网球培训品牌，全力打造线下教学＋线上交互的网球生活峰值体验，我们的使命是：让网球成为人人可享的优雅生活方式点击下方“品牌介绍”了解更多关于我们
------------------------------如果您希望体验一下昱隆的网球课，请点击下方菜单栏“预约体验”，体验网球的同时还能获得昱隆精品礼品卡，送礼品卡 = 送健康
------------------------------如果您已经是昱隆网球会员，请登录学员端，复习、订场、约课、请假、购物、作业打卡、活动报名一站式搞定。"
                            };
                            echostr = send.SendText(sendText);
                            break;
                        case MsgType.image:
                            break;
                        case MsgType.voice:
                            break;
                        case MsgType.video:
                            break;
                        case MsgType.location:
                            break;
                        case MsgType.link:
                            // 处理接收到的链接信息
                            // (model as link_receivemsg).Url  
                            break;
                        case MsgType.Event:
                            {
                                var eventModel = model as eventmsg;
                                if (eventModel.Event == eventmsg.Subscribe)
                                {
                                    sendText = new text_sendmsg()
                                    {
                                        ToUserName = model.FromUserName,
                                        FromUserName = model.ToUserName,
                                        Content = @"
Hi！亲爱的朋友，欢迎关注昱隆网球学院！昱隆网球是拥有全知识产权的网球培训品牌，全力打造线下教学＋线上交互的网球生活峰值体验，我们的使命是：让网球成为人人可享的优雅生活方式点击下方“品牌介绍”了解更多关于我们
------------------------------如果您希望体验一下昱隆的网球课，请点击下方菜单栏“预约体验”，体验网球的同时还能获得昱隆精品礼品卡，送礼品卡 = 送健康
------------------------------如果您已经是昱隆网球会员，请登录学员端，复习、订场、约课、请假、购物、作业打卡、活动报名一站式搞定。"
                                    };
                                    //var welcome = ConfigurationManager.AppSettings["welcome"];
                                    //if (!string.IsNullOrEmpty(welcome))
                                    //{
                                    //    sendText.Content = welcome;
                                    //}
                                    echostr = send.SendText(sendText);

                                    //if (string.IsNullOrEmpty(eventModel.Ticket))
                                    //{
                                    //    // 用户关注了服务号，可以推送一条欢迎消息给用户
                                    //}
                                    //else
                                    //{
                                    //    // 用户通过扫描二维码关注了服务号，可以推送一条欢迎消息给用户
                                    //}
                                }
                                else if (eventModel.Event == eventmsg.UnSubscribe)
                                {
                                    // 用户取消关注时收到的事件
                                }
                                else if (eventModel.Event == eventmsg.View)
                                {
                                    // 点击类型为View的菜单触发的事件，可以统计用户点击菜单的点击次数（如果菜单有子菜单则不触发）
                                }
                                else if (eventModel.Event == eventmsg.Click)
                                {

                                }
                                else if (eventModel.Event == eventmsg.Location)
                                {
                                    // 当开启位置跟踪，且用户同意发送位置信息时，每次进入服务号或每隔5s以上，上报一次位置信息
                                    // string.Format("event :: {0} :: 经度：{1} :: 纬度：{2} :: 精度：{3}", eventmsg.Click, eventModel.Latitude, eventModel.Longitude, eventModel.Precision);
                                }
                                else if (eventModel.Event == eventmsg.Scan)
                                {
                                    // 用户已经关注，且扫描二维码进入服务号时触发
                                }
                                else if (eventModel.Event == eventmsg.MASSSENDJOBFINISH)
                                {
                                }
                            }
                            break;
                        default:
                            // 微信未知的出发事件
                            break;
                    }
                }

            }
            HttpResponseMessage responseMessage = new HttpResponseMessage { Content = new StringContent(echostr, Encoding.GetEncoding("UTF-8"), "text/plain") };
            return responseMessage;
        }
    }
}
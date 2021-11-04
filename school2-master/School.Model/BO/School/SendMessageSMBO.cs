using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class SendMessageSMBO
    {
        /// <summary>
        /// 发送给人
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 模板编号
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 页面
        /// </summary>
        public string page { get; set; }
        public string Miniprogram { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public Dictionary<string, MessageContent> data { get; set; }
    }
    public class MessageContent {
         public string value { get; set; }
        public string color { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace school.Common
{
    public class ConnContext
    {
        /// <summary>
        /// 从当前线程中获取连接字符串
        /// </summary>
        public static string Get()
        {
            var currentConnetion = CallContext.GetData(Const.CONNECTIONSTRING) as String;
            return currentConnetion;
        }
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static void Set(string connetionString)
        {
            if (!string.IsNullOrEmpty(connetionString))
            {
                CallContext.SetData(Const.CONNECTIONSTRING, connetionString);
            }
        }
    }
}

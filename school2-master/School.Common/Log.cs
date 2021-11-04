using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;
using school.Model.VO;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log/log.config", ConfigFileExtension = "config", Watch = true)]
namespace school.Common
{
    public static class Log
    {
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        /// <summary>
        /// 异常日志
        /// </summary>
        public static Task Error(string methodName, Exception exp)
        {
            return Task.Factory.StartNew(() =>
            {
                var errors = new List<string>();
                GetException(exp, ref errors);
                Error(new string[] { methodName, string.Join("----", errors) });
            });
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        public static Task Error(string[] values)
        {
            return Task.Factory.StartNew(() =>
            {
                if (loginfo.IsErrorEnabled)
                {
                    if (values != null && values.Length > 0)
                    {
                        loginfo.Error(GetLog(Level.Error.DisplayName, values));
                    }
                }
            });
        }
        /// <summary>
        /// 获取分析异常
        /// </summary>
        private static void GetException(Exception exp, ref List<string> errors)
        {
            errors.Add(exp.Message + ":" + exp.StackTrace);
            if (exp.InnerException != null)
            {
                GetException(exp.InnerException, ref errors);
            }
            return;
        }
        /// <summary>
        /// 提示日志
        /// </summary>
        public static Task Info(object[] values)
        {
            return Task.Factory.StartNew(() =>
            {
                if (loginfo.IsInfoEnabled)
                {
                    if (values != null && values.Length > 0)
                    {
                        loginfo.Info(GetLog(Level.Info.DisplayName, values));
                    }
                }
            });
        }
        /// <summary>
        /// 调试日志
        /// </summary>
        public static Task Debug(string[] values)
        {
            return Task.Factory.StartNew(() =>
            {
                if (loginfo.IsDebugEnabled)
                {
                    if (values != null && values.Length > 0)
                    {
                        loginfo.Debug(GetLog(Level.Debug.DisplayName, values));
                    }
                }
            });
        }
        /// <summary>
        /// 获取日志数据
        /// </summary>
        public static string GetLog(string logType, object[] values)
        {
            var log = new BaseLog();
            log.LogType = logType;
            log.Name = values[0];
            log.Result = values[1];
            return log.ToJson();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace school.Common.Tools
{
    public class Utils
    {
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public static Guid ConvertToGuid(String value)
        {
            var guid = Guid.NewGuid();
            Guid.TryParse(value, out guid);
            return guid;
        }
        public static string Md5By32(string text)
        {
            return MD5By32(text, Encoding.UTF8);
        }
        public static string MD5By32(string text, Encoding encoding)
        {
            //如果字符串为空，则返回
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            try
            {
                //创建MD5密码服务提供程序
                var md5 = new MD5CryptoServiceProvider();

                //计算传入的字节数组的哈希值
                byte[] hashCode = md5.ComputeHash(encoding.GetBytes(text));

                //释放资源
                md5.Clear();

                //返回MD5值的字符串表示
                string temp = "";
                int len = hashCode.Length;
                for (int i = 0; i < len; i++)
                {
                    temp += hashCode[i].ToString("x").PadLeft(2, '0');
                }
                return temp;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

     
   
    
      
     
     
    
    }
}

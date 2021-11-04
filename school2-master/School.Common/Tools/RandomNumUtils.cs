using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace school.Common.Tools
{
   public class RandomNumUtils
    {
        private static string GetRandomNo(int maxSize = 8)
        {
            //int minSize = 5;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }
        /// <summary>
        /// 生成日期和rng算法组合而来的随机数
        /// </summary>
        /// <param name="rngMaxSize"></param>
        /// <returns></returns>
        public static string GetDtRng_RandomNo(int rngMaxSize = 8)
        {
            var strDateTime = DateTime.Now.ToString("yyyyMMddHHmmssms");
            var rngNo = RandomNumUtils.GetRandomNo(rngMaxSize);
            var randomNo = $"{strDateTime}_{rngNo}";
            return randomNo;
        }
    }
}

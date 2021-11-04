using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Common.Tools
{
    public static class DateParse
    {
    
        public static string DateToEnglish(DateTime dt, string format)
        {
            return dt.ToString(ExcuteFormat(format), DateTimeFormatInfo.InvariantInfo);
        }
        public static string ExcuteFormat(string format)
        {
            return format.Replace("{", "").Replace("}", "").Replace("uk", "");
        }
        public static DateTime GetDateTime()
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", provider);
        }
   
    }
}

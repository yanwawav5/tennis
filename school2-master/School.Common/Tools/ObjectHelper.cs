using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using school.Model.BO;
using school.Model.Enum;

namespace school.Common.Tools
{
    public class ObjectHelper
    {
        /// <summary>
        /// 获取对象属性
        /// </summary>
        private static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="s">来源赋</param>
        /// <param name="t">待赋值</param>
        /// <param name="excepts">排除的属性名称</param>
        public static void AutoMapping<S, T>(S s, T t, params string[] excepts)
        { 
            PropertyInfo[] propertyInfos = GetProperties(s.GetType());
            Type target = t.GetType();
            bool isCk=false;
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.Name == "ChangeType")
                {
                    isCk = true;
                    break;
                }
            } 
            foreach (var propertyInfo in propertyInfos)
            {
                var flag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;
                var targetProperty = target.GetProperty(propertyInfo.Name,flag);
                if (isCk)
                {
                    if (propertyInfo.Name == "Result" || propertyInfo.Name == "SqlItems")
                        continue;
                }
                if (targetProperty != null && propertyInfo.PropertyType == targetProperty.PropertyType)
                {
                    object value = propertyInfo.GetValue(s, null);
                   
                    //if (value != null)
                    //{
                    if (excepts != null && excepts.Any(p => String.Equals(p, targetProperty.Name, StringComparison.CurrentCultureIgnoreCase))) continue;
                    targetProperty.SetValue(t, value, null);
                    //}
                }
                else if (propertyInfo.PropertyType.Name.ToUpper() == "BYTE[]"&& propertyInfo.Name.ToUpper().Equals("FTIMESTEMP"))
                {
                    ByteLongChang(s, t, propertyInfo, targetProperty);
                }
            }
        }

        private static void ByteLongChang<S, T>(S s, T t, PropertyInfo propertyInfo, PropertyInfo targetProperty)
        {
            object value = propertyInfo.GetValue(s, null);
            if (targetProperty != null && value != null)
            {
                string st = string.Empty;
                byte[] b = (byte[])value;
                var len = b.Length;
                long a = 0;
                for (int i = len - 1; i >= 0; i--)
                {
                    a += b[i] << (7 - i) * 8;
                }
                st = a.ToString();
                if (targetProperty.PropertyType.Name.ToUpper() == "INT64")
                {
                    targetProperty.SetValue(t, a, null);
                }
                else
                {
                    targetProperty.SetValue(t, st, null);
                }
            }
        }
    }
}


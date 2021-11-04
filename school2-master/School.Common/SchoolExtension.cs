using school.Common.RPC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace school.Common
{
    public static class schoolExtension
    {

        #region Newtonsoft.Json

        public static string ToJson(this object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch
            {
                return null;
            }
        }

        public static T ToObject<T>(this string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch
            {
                return default(T);
            }
        }

        #endregion

        #region 格式化字符串 1->xxxx1

        public static String ToString(this int value, int len)
        {
            try
            {
                if (len > 0) return value.ToString(new string('0', len));
            }
            catch { }
            return value.ToString();
        }

        #endregion

        #region List 笛卡尔积
        public static List<List<T>> CartesianProduct<T>(this List<List<T>> sourceList)
        {
            int count = 1;
            sourceList.ForEach(item => count *= item.Count);
            var result = new List<List<T>>();
            for (int i = 0; i < count; ++i)
            {
                var temp = new List<T>();
                int j = 1;
                sourceList.ForEach(item =>
                {
                    j *= item.Count;
                    temp.Add(item[(i / (count / j)) % item.Count]);
                });
                result.Add(temp);
            }
            return result;
        }
        #endregion

        #region 数据库数据转换
        public static int ToInt(this object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }
        public static DateTime? ToDateTime(this object value)
        {
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return null;
            }
        }
        public static bool ToBoolean(this object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region DataRow To DataTable

        public static DataTable ToDataTable(this DataRow[] rows)
        {
            if (rows != null && rows.Length > 0)
            {
                return rows.CopyToDataTable();
            }
            return null;
        }

        #endregion

        #region List转换成DataTable
        /// <summary>
        /// List转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> list)
        {

            //创建属性的集合    
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口   
            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列    
            Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
            {
                pList.Add(p);
                dt.Columns.Add(p.Name);
            });
            foreach (var item in list)
            {
                //创建一个DataRow实例    
                DataRow row = dt.NewRow();
                foreach (var e in pList)
                {
                    if (e.GetValue(item, null) != null)
                    {
                        row[e.Name] = e.GetValue(item, null);
                    }
                }
                //给row 赋值    
                //pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable    
                dt.Rows.Add(row);
            }
            return dt;
        }
        #endregion

        #region HttpClient

        public static void Close(this HttpClient client)
        {
            var httpClients = HttpClientPool.Instance.HttpClients;
            if (httpClients.Count < 10)
            {
                client.DefaultRequestHeaders.Remove(Const.APIHEADER);
                HttpClientPool.Instance.HttpClients.Add(client);
            }
            else
            {
                client.Dispose();
            }
        }

        #endregion


        public static T FirstOrDefaultExtension<T>(this IEnumerable<T> list, Func<T, bool> predicate) where T : new()
        {
            var entity = list.FirstOrDefault(predicate);
            if (entity == null)
                return new T();
            return entity;
          
        }

    }
}

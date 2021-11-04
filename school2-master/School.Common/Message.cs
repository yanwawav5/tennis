using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace school.Common
{
    public class Message
    {
        private static readonly Message mInstance = new Message();
        private readonly Dictionary<string, Dictionary<string, string>> mDictionaries;
        private Message()
        {
            mDictionaries = new Dictionary<string, Dictionary<string, string>>();
            mDictionaries.Add("chs", GetXml("chs"));
            mDictionaries.Add("eng", GetXml("eng"));
        }
        private Dictionary<string, string> GetXml(string key)
        {
            var dic = new Dictionary<string, string>();
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "/Res/Message/" + key + ".xml");
            var root = xmlDoc.SelectSingleNode("values");
            foreach (XmlElement node in root.ChildNodes)
            {
                dic.Add(node.Attributes["name"].Value, node.InnerText);
            }
            return dic;
        }
        public string R(string valueKey, string lanKey = "chs")
        {
            lanKey = "chs";
            if (mDictionaries.ContainsKey(lanKey))
            {
                var valueDic = mDictionaries[lanKey];
                if (valueDic.ContainsKey(valueKey))
                {
                    return valueDic[valueKey];
                }
            }
            return "";
        }

        public static Message Instance
        {
            get { return mInstance; }
        }
    }
}

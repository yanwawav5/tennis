using school.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class BaseCore
    {
        /// <summary>
        /// 属性改变容器
        /// </summary>
        public Dictionary<string, object> ChangedDictionary = new Dictionary<string, object>();
        private ChangeType changeType = ChangeType.Null;
        #region 字段

        private string id;
        #endregion
         
     
        /// <summary>
        /// Id
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}

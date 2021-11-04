using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.Enum
{
    public enum ChangeType
    {
        /// <summary>
        /// 空
        /// </summary>
        Null = 0,
        /// <summary>
        /// 用户
        /// </summary>
        Update = 2,
        /// <summary>
        /// 插入
        /// </summary>
        Insert=1,
        /// <summary>
        /// 删除
        /// </summary>
        Delete=3
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class M_FieldInfo : BaseModel
    {
        /// <summary>
        /// 场地编号
        /// </summary>
        public int FieldId { get; set; }
        /// <summary>
        /// 订购日期
        /// </summary>
        public int OrderDate { get; set; }
        /// <summary>
        /// 订购时间
        /// </summary>
        public int OrderTime { get; set; }
    }
}

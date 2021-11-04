using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_Js : BaseEntity
    {
        public string Ticket { get; set; }
    }

    public class JsVO
    {
        public string Ticket { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建者Id
        /// </summary>
        public string CreateUserId { get; set; } 
    }
}

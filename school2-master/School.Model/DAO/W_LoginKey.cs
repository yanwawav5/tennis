using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_LoginKey : BaseEntity
    {
        public string UnionId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        public string UserId { get; set; }
    }
    
}

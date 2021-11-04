using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_Code : BaseEntity
    {
        public string StudentId { get; set; }
        public string UnionId { get; set; }
        public string Code { get; set; }

        public int Status { get; set; }
        public string Phone { get; set; }
        public string Price { get; set; }
        public string ShareInfoId { get; set; }

    }

    public class CodeVO
    {
        public string ShareInfoId { get; set; }
        public string Price { get; set; }
        public string Phone { get; set; }
        public string StudentId { get; set; }
        public string UnionId { get; set; }
        public string Code { get; set; }

        public int Status { get; set; }
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

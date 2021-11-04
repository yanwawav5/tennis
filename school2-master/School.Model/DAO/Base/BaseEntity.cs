using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO.Base
{
    public class BaseEntity
    {
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
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 更新者Id
        /// </summary>
        public string UpdateUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Compute]
        public byte[] FtimeStemp { get; set; }
    }
}

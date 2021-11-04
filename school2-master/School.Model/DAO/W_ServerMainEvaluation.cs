using school.Model.DAO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.DAO
{
    public class W_ServerMainEvaluation : BaseEntity
    {
        /// <summary>
        /// 服务Id
        /// </summary>
        public string ServerMainId { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Pic { get; set; }
    }
}

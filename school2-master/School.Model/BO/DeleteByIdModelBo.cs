using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
   public class DeleteByIdModelBo
    {
        /// <summary>
        /// 操作编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 操作者姓名
        /// </summary>
        public  string FullName { get; set; }
        public  string ProjectId { get; set; }
        
    }
}

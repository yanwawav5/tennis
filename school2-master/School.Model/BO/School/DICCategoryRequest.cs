using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.BO
{
    public class DICCategoryRequest
    {
        /// <summary>
      /// 第一个条件
      /// </summary>
        public string OneCategory { get; set; }
        /// <summary>
        /// 第二个条件
        /// </summary>
        public string TwoCategory { get; set; }
        /// <summary>
        /// 第三个条件
        /// </summary>
        public string ThrCategory { get; set; }

    }
}

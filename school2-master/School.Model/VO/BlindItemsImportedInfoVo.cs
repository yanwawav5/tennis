using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class BlindItemsImportedInfoVo
    {
        public bool IsSuccess { get; set; }

        public int Amount { get; set; }

        //重复信息
        public string ErrorInfo { get; set; }
    }
}

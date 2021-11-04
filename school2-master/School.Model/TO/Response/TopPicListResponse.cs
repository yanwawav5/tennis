using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class TopPicListResponse : AbsResponse
    {
        public List<TopPicVO> List { get; set; }
        public int TotNum { get; set; }

    }
}

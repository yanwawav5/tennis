using school.Model.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class KeyValueResponse : AbsResponse
    {
        public List<KeyValueBo> List { get; set; }
    }
}

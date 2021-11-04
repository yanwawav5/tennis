using school.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class SubScribeListResponse:AbsResponse
    {
        public List<M_Subscribe> List { get; set; }
    }
}

using school.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class StudentKjVORsponese : AbsResponse
    {
        public StudentKjVO Model { get; set; }
    }

    public class StudentKjListVORsponese : AbsResponse
    {
        public List< StudentKjVO>List{ get; set; }
        public int TotNum { get; set; }
    }
}

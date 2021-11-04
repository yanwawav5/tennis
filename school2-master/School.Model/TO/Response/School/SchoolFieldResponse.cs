using school.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
   public class SchoolResponse :AbsResponse
    {
        public M_School Model { get; set; }
    }
    public class SchoolListResponse : AbsResponse
    {
        public List<M_School> List { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int TotNum { get; set; }
    }


    public class FieldListResponse: AbsResponse
    {
        public List<M_Field> List { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int TotNum { get; set; }
    }
}

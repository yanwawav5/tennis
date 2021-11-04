using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class TeacherMessageListResponse : AbsResponse
    {
        public List<TeacherMessageVO> List { get; set; }
        public int TotNum { get; set; }
    }

    public class TeacherMessageMainListResponse : AbsResponse
    {
        public List<TeacherMessageMainVO> List { get; set; }
        public int TotNum { get; set; }
    } 
    public class TeacherMessageMainVO
    {
        public string Pic { get; set; }
        public string Id { get; set; }
        /// <summary>
        /// 未读数量
        /// </summary>
       public int UnReadNum { get; set; }
        /// <summary>
        /// 留言人
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 最后时间
        /// </summary>
        public string EndTime { get; set; }
    }
}

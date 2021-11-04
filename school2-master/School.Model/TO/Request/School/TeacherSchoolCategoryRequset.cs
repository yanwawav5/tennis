
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Request
{
    public class TeacherSchoolCategoryRequset
    {
        public string TeacherId { get; set; }
        public string SchoolId { get; set; }
        public string StudentCourserId { get; set; }
        //public int AgecategroyId { get; set; }
        //public int CategoryId { get; set; }
        public int AddDay { get; set; }
    }
}

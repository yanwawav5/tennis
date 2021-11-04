using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class TeacherCourseShowResponse : AbsResponse
    {
        public TeacherCourseShowVO CourseModel { get; set; }
        public TeachingPlanVO TeachingModel{get;set;}
        public List<TeachingPlanInfoVO> TeachingInfoList { get; set; }
        public List<StudentCourseClassVO> StudentClassListt { get; set; }
    }
    public class TeacherCourseShowListResponse:AbsResponse
    {
        public List<TeacherCourseShowVO> List { get; set; }
    }



    public class StudentCourseClassShowResponse : AbsResponse
    {
        public TeacherCourseShowVO CourseModel { get; set; }
        public TeachingPlanVO TeachingModel { get; set; }
        public List<TeachingPlanInfoVO> TeachingInfoList { get; set; }
        //public List<StudentCourseClassVO> StudentClassListt { get; set; }
        public StudentCourseClassVO StudentCourseClass { get; set; }
    }
}

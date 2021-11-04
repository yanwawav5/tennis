using school.Model.DAO;
using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class CategoryResponse : AbsResponse
    {
        public M_Category Model { get; set; }
    }

    public class CategoryListResponse : AbsResponse
    {
        public List<M_Category> List { get; set; }
    }
    public class CategoryListVOponse : AbsResponse
    {
        public List<M_Category> OneList { get; set; }
        public List<CategoryListVO> List { get; set; }
    }
    public class CategoryListALLVOResponse : AbsResponse
    {
        public CategoryListAllVO Model { get; set; }
    }


}


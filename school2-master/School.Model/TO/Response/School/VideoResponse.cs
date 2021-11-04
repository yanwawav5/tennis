using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class VideoResponse : AbsResponse
    {
        public VideoVO Model { get; set; }
    }

    public class VideoListResponse : AbsResponse
    {
        public List<VideoVO> List { get; set; }
        public int TotNum { get; set; }
    }
}

using school.Model.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.TO.Response
{
    public class WechatUserInfoResponse:AbsResponse
    {
        public WechatUserInfoVO Model { get; set; }
        //public string nickname { get; set; }
        //public int sex { get; set; }
        //public string province { get; set; }
        //public string city { get; set; }
        //public string country { get; set; }
        //public string headimgurl { get; set; }
        //public string unionid { get; set; }

    }

    public class StrKeyResponse : AbsResponse
    {
        public StrKey Model { get; set; }
        //public string nickname { get; set; }
        //public int sex { get; set; }
        //public string province { get; set; }
        //public string city { get; set; }
        //public string country { get; set; }
        //public string headimgurl { get; set; }
        //public string unionid { get; set; }

    }

    public class StrKey
    {
        public string Id { get; set; }
    }
}

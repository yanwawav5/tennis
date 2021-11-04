using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model
{
    public class WxSmallInfoVO
    {
        public string session_key { get; set; }
        public string openid { get; set; }
        public string unionid { get; set; }
        public string errmsg { get; set; }
        public int errcode { get; set; }
    }

    public class WxUserInfoUnion {

        public string openid { get; set; }
        public string unionid { get; set; }

        public string nickName { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
        public string SessionKey { get; set; }
        // "openId": "OPENID",
        //"nickName": "NICKNAME",
        //"gender": GENDER,
        //"city": "CITY",
        //"province": "PROVINCE",
        //"country": "COUNTRY",
        //"avatarUrl": "AVATARURL",
        //"unionId": "UNIONID",
    }


    public class WxSmallUserInfoVO
    {
        public string session_key { get; set; }
        public string openid { get; set; }
        public string unionid { get; set; }
        public string errmsg { get; set; }
        public int errcode { get; set; }
    }
}

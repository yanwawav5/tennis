using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.Model.VO
{
    public class OauthAccessTokenVO : WxBaseVO
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openId { get; set; }
        public string scope { get; set; }


    }

    public class OauthAccessTokenSecVO : WxBaseVO
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openId { get; set; }
        public string scope { get; set; }

        public string unionid { get; set; }
    }
    
}

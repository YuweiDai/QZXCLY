using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZCHY.Services.AccountUsers
{
    public class WechatAuthorizeResponse
    {
        public string openid { get; set; }
        public string session_key { get; set; }
        public string unionid { get; set; }
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }

    public class WechatUserInfo
    {
        public string openId { get; set; }
        public string nickName { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string avatarUrl { get; set; }
        public string unionId { get; set; }
        public Watermark watermark { get; set; }
    }
    /// <summary>  
    /// 微信用户数据水印  
    /// </summary>  
    public class Watermark
    {
        public string appid { get; set; }
        public string timestamp { get; set; }
    }
}

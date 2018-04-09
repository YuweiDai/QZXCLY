using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QZCHY.API.Models
{
    /// <summary>  
    /// 微信小程序登录信息结构  
    /// </summary>  
    public class WechatLoginModel
    {
        public string Code { get; set; }
        public string EncryptedData { get; set; }
        public string Iv { get; set; }
        public string RawData { get; set; }
        public string Signature { get; set; }
    }
    ///// <summary>  
    ///// 微信小程序用户信息结构  
    ///// </summary>  
    //public class WechatUserInfo
    //{
    //    public string OpenId { get; set; }
    //    public string NickName { get; set; }
    //    public string Gender { get; set; }
    //    public string City { get; set; }
    //    public string Province { get; set; }
    //    public string Country { get; set; }
    //    public string AvatarUrl { get; set; }
    //    public string UnionId { get; set; }
    //    public Watermark Watermark { get; set; }

    //    public class Watermark
    //    {
    //        public string Appid { get; set; }
    //        public string Timestamp { get; set; }
    //    }
    //}
    ///// <summary>  
    ///// 微信小程序从服务端获取的OpenId和SessionKey信息结构  
    ///// </summary>  
    //public class OpenIdAndSessionKey
    //{
    //    public string Openid { get; set; }
    //    public string Session_key { get; set; }
    //    public string Errcode { get; set; }
    //    public string Errmsg { get; set; }
    //}
}
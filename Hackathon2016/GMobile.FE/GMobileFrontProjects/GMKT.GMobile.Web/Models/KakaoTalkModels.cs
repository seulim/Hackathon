using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Globalization;
using GMKT.Web.Membership;



namespace GMKT.GMobile.Web.Models
{
    public class KakaoTalkApplyUserM
    {

        public string[] UserHPNoArr { get; set; } 
        public string TempUserKey { get; set; }
        public bool UseAgree { get; set; } //사용동의
        public EnumOriginChannelType AccessChannel { get; set; }

    }

    public enum EnumOriginChannelType
    {
        KakaoTalkApp = 0,
        GmarketApp = 1,
        UnsuitChannel= 2
    }
        

}
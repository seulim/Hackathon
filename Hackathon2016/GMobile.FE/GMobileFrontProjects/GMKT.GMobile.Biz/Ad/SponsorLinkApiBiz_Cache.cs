using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data.Ad;

namespace GMKT.GMobile.Biz
{
    public class SponsorLinkApiBiz_Cache : CacheContextObject
    {
        [CacheDuration(DurationSeconds = 300)]
        public List<SponsorLinkDataT> GetSponsorLinkList(string channel, int count, string primeKeyword, string moreKeyword, string largeCategory, string middleCategory, string smallCategory, string ip, string url, string ua, string referrer, string menuName)
        {
			return new SponsorLinkApiBiz().GetSponsorLinkList(channel, count, primeKeyword, moreKeyword, largeCategory, middleCategory, smallCategory, ip, url, ua, referrer, menuName);
        }
    }
}

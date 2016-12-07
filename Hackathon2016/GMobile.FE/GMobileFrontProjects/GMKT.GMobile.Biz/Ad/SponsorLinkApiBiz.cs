using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.Ad;
using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Biz
{
    public class SponsorLinkApiBiz
    {
		public List<SponsorLinkDataT> GetSponsorLinkList(string channel, int count, string primeKeyword, string moreKeyword, string largeCategory, string middleCategory, string smallCategory, string ip, string url, string ua, string referrer, string menuName)
        {
            List<SponsorLinkDataT> result = new List<SponsorLinkDataT>();
			ApiResponse<List<SponsorLinkDataT>> response = new SponsorLinkApiDac().GetSponsorLinkList(channel, count, primeKeyword, moreKeyword, largeCategory, middleCategory, smallCategory, ip, url, ua, referrer, menuName);
            if (response != null && response.Data != null)
            {
                result = response.Data;
            }

            return result;
        }
    }
}

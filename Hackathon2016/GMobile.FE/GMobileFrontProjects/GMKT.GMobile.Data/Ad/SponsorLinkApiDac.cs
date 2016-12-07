using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.GMobile.Data.Ad;

namespace GMKT.GMobile.Data.Ad
{
    public class SponsorLinkApiDac : ApiBase
    {
        public SponsorLinkApiDac()
            : base("GMApi")
        {
        }

		public ApiResponse<List<SponsorLinkDataT>> GetSponsorLinkList(string channel, int count, string primeKeyword, string moreKeyword, string largeCategory, string middleCategory, string smallCategory, string ip, string url, string ua, string referrer, string menuName)
        {
            ApiResponse<List<SponsorLinkDataT>> result = ApiHelper.CallAPI<ApiResponse<List<SponsorLinkDataT>>>(
                "POST",
                ApiHelper.MakeUrl("api/SponsorLink/GetSponsorLinkList"),
                new
                {
                    channel = channel,
                    count = count,
                    primeKeyword = primeKeyword,
                    moreKeyword = moreKeyword,
                    largeCategory = largeCategory,
                    middleCategory = middleCategory,
                    smallCategory = smallCategory,
                    ip = ip,
                    url = url,
                    ua = ua,
                    referrer = referrer,
					menuName = menuName
                }
            );

            return result;

        }
    }
}

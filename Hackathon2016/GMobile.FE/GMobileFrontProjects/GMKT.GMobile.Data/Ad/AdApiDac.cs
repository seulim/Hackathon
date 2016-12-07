using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.GMobile.Data.Ad;

namespace GMKT.GMobile.Data.Ad
{
    public class AdApiDac : ApiBase
    {
        public AdApiDac()
            : base("GMApi")
        { 
            
        }

        public ApiResponse<FooterDaData> GetSrpLpFooterDa(string primeKeyword, string categoryCode)
        {
            ApiResponse<FooterDaData> result = ApiHelper.CallAPI<ApiResponse<FooterDaData>>(
                "POST",
                ApiHelper.MakeUrl("api/Banner/GetSrpLpFooterDa"),
                new
                {
                    primeKeyword = primeKeyword,
                    categoryCode = categoryCode
                }
            );

            return result;

        }
    }
}

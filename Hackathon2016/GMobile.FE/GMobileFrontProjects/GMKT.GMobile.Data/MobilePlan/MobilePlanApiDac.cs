using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMobile.Data;

using System.IO;
using System.Net;
using GMKT.GMobile.Util;
using ConnApi.Client;

namespace GMKT.GMobile.Data
{
    public class MobilePlanApiDac : ApiBase
    {
        public MobilePlanApiDac()
			: base("GMApi")
		{
		}

        public ApiResponse<MobileShopPlanT> GetMobilePlanDetail(int sid)
		{
            ApiResponse<MobileShopPlanT> result = ApiHelper.CallAPI<ApiResponse<MobileShopPlanT>>(
				"GET",
                ApiHelper.MakeUrl("api/MobilePlan/GetMobilePlanDetail")
			);
			return result;
		}

        public ApiResponse<MobilePlanViewT> GetMobilePlanList(string groupCd, int pageNo, int iRowCount)
        {
            ApiResponse<MobilePlanViewT> result = ApiHelper.CallAPI<ApiResponse<MobilePlanViewT>>(
                "GET",
                ApiHelper.MakeUrl("api/MobilePlan/GetMobilePlanList")
            );
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data.EventV2
{
    public class PluszoneApiDac : ApiBase
    {
        public PluszoneApiDac()
			: base("GMApi")
		{
		}

        public ApiResponse<PluszoneDataT> GetPluszoneInfo()
        {
            ApiResponse<PluszoneDataT> result = ApiHelper.CallAPI<ApiResponse<PluszoneDataT>>(
                "GET",
                ApiHelper.MakeUrl("api/Pluszone/GetPluszoneInfo")
            );
            return result;
        }
    }
}

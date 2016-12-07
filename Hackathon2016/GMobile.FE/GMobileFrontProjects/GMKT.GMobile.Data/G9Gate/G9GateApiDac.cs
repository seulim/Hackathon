using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data.G9Gate
{
	public class G9GateApiDac : ApiBase
	{
		public G9GateApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<G9GateBannerT> GetG9GateBanner()
		{
			ApiResponse<G9GateBannerT> result = ApiHelper.CallAPI<ApiResponse<G9GateBannerT>>(
				"GET",
				ApiHelper.MakeUrl("api/G9Gate/GetG9GateBanner")
			);
			return result;
		}
	}
}

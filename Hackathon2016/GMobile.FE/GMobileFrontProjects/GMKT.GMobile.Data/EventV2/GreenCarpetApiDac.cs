using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data.EventV2
{
	public class GreenCarpetApiDac : ApiBase
	{
		public GreenCarpetApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<GreenCarpetT> GetGreenCarpetInfo()
		{
			ApiResponse<GreenCarpetT> result = ApiHelper.CallAPI<ApiResponse<GreenCarpetT>>(
				"GET",
				ApiHelper.MakeUrl("api/greencarpet/GetAdminData")
			);
			return result;
		}
	}
}

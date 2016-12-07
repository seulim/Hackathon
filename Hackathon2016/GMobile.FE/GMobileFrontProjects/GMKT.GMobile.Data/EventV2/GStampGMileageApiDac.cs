using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data.EventV2
{
	public class GStampGMileageApiDac : ApiBase
	{
		public GStampGMileageApiDac() : base("GMApi")
		{
		}

		public ApiResponse<GStampDataT> GetGStampInfo()
		{
			ApiResponse<GStampDataT> result = ApiHelper.CallAPI<ApiResponse<GStampDataT>>(
				"GET",
				ApiHelper.MakeUrl("api/pluszone/GetGStampInfo")
			);
			return result;
		}

		public ApiResponse<GMileageDataT> GetGMileageInfo()
		{
			ApiResponse<GMileageDataT> result = ApiHelper.CallAPI<ApiResponse<GMileageDataT>>(
				"GET",
				ApiHelper.MakeUrl("api/pluszone/GetGMileageInfo")
			);
			return result;
		}
	}
}

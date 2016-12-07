using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConnApi.Client;

namespace GMKT.GMobile.Data
{
	public class PlusZoneApiDac : ApiBase
	{
		public PlusZoneApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<AttendanceCheckTotal> GetAttendanceCheckTotalList()
		{
			ApiResponse<AttendanceCheckTotal> result = ApiHelper.CallAPI<ApiResponse<AttendanceCheckTotal>>(
				"GET",
				ApiHelper.MakeUrl("api/AttendanceCheck/GetAttendanceCheckTotalList")
			);
			return result;
		}

		public ApiResponse<List<MobileShopPlan>> GetMobileShopPlan()
		{
			ApiResponse<List<MobileShopPlan>> result = ApiHelper.CallAPI<ApiResponse<List<MobileShopPlan>>>(
				"GET",
				ApiHelper.MakeUrl("api/AttendanceCheck/GetPopularPlanListMain")
			);
			return result;
		}
	}

	
}

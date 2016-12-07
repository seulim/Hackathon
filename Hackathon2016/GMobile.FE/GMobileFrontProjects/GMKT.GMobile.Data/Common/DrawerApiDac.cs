using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConnApi.Client;

namespace GMKT.GMobile.Data
{
	public class DrawerApiDac : ApiBase
	{
		public DrawerApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<MobileDrawerTotal> GetMobileDrawerTotal()
		{
			ApiResponse<MobileDrawerTotal> result = ApiHelper.CallAPI<ApiResponse<MobileDrawerTotal>>(
				"GET",
				ApiHelper.MakeUrl("api/Drawer/GetMobileDrawerTotal")
			);
			return result;
		}
	}

	//public class DrawerApiDac : MobileAPIDacBase
	//{
	//    public ApiResponse<MobileDrawerTotal> GetMobileDrawerTotal()
	//    {
	//        ApiResponse<MobileDrawerTotal> result = ApiHelper.ExecuteAPI<MobileDrawerTotal>(
	//            "/api/Drawer/GetMobileDrawerTotal"
	//        );
	//        return result;
	//    }
	//}
}

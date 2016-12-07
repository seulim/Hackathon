using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMobile.Data;

using System.IO;
using System.Net;
using GMKT.Web.Context;
using GMKT.GMobile.Util;
using ConnApi.Client;


namespace GMKT.GMobile.Data
{
	public class MobileHomeApiDac : ApiBase
	{
		public MobileHomeApiDac()
			: base("GMApi")
		{
		}
		private readonly string COOKIE_NAME_JAEHUID = "jaehuid";

		public ApiResponse<HomeTotalList> GetMobileHomeTotalList(string siteCode)
		{
			ApiResponse<HomeTotalList> result = ApiHelper.CallAPI<ApiResponse<HomeTotalList>>(
				"GET",
				ApiHelper.MakeUrl("api/Home/GetMobileHomeMain"),
				(false == string.IsNullOrEmpty(siteCode)) ? new	{ siteCode = siteCode }	: null
			);
			return result;
		}

		public ApiResponse<HomeTotalListV2> GetMobileHomeTotalListV2(string userInfo, string siteCode)
		{
			ApiResponse<HomeTotalListV2> result = ApiHelper.CallAPI<ApiResponse<HomeTotalListV2>>(
				"GET",
				ApiHelper.MakeUrl("api/Home/GetMobileHomeMainV2"),
				new	{ 
					SiteCode = siteCode,
					UseEmptyListingBanner = true
				},
                ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

		#region [Pilot - gsohn] Pilot Test 코드 (테스트 완료후 삭제)
		public ApiResponse<HomeTotalListV2> GetMobileHomeTotalListV2Pilot(string userInfo, string siteCode, string jaehuid)
		{
			ApiResponse<HomeTotalListV2> result = ApiHelper.CallAPI<ApiResponse<HomeTotalListV2>>(
				"GET",
				ApiHelper.MakeUrl("api/Home/GetMobileHomeMainV2"),
				new	{ 
					SiteCode = siteCode,
					UseEmptyListingBanner = true
				},
				new CookieParameter( GMobileWebContext.EncodedCookieNameOfUserInfo, userInfo ),
				new CookieParameter(COOKIE_NAME_JAEHUID, jaehuid)
			);
			return result;
		}
		#endregion

		public ApiResponse<HomeMainGroup> GetHomeListingDABanner(string exposeArea)
		{
			ApiResponse<HomeMainGroup> result = ApiHelper.CallAPI<ApiResponse<HomeMainGroup>>(
				"GET",
				ApiHelper.MakeUrl("api/Home/GetHomeListingDABanner"),
				new { exposeArea = exposeArea },
				ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

		//public ApiResponse<MobileDrawerTotal> GetMobileDrawerTotal()
		//{
		//    ApiResponse<MobileDrawerTotal> result = ApiHelper.ExecuteAPI<MobileDrawerTotal>(
		//        "/api/Home/GetMobileDrawerTotal"
		//    );
		//    return result;
		//}
	}

	//public class MobileHomeApiDac : MobileAPIDacBase
	//{
	//    public ApiResponse<HomeTotalList> GetMobileHomeTotalList()
	//    {
	//        ApiResponse<HomeTotalList> result = ApiHelper.ExecuteAPI<HomeTotalList>(
	//            "/api/Home/GetMobileHomeTotalList"
	//        );
	//        return result;
	//    }

	//    //public ApiResponse<MobileDrawerTotal> GetMobileDrawerTotal()
	//    //{
	//    //    ApiResponse<MobileDrawerTotal> result = ApiHelper.ExecuteAPI<MobileDrawerTotal>(
	//    //        "/api/Home/GetMobileDrawerTotal"
	//    //    );
	//    //    return result;
	//    //}
	//}
}

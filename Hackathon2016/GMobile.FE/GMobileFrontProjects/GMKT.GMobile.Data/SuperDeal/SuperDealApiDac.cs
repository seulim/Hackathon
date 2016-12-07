using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConnApi.Client;
using GMKT.Web.Context;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Data
{
	public class SuperDealApiDac : ApiBase
	{
		public SuperDealApiDac()
			: base("GMApi")
		{
		}
		//미사용
		public ApiResponse<List<SuperDealCategoryInfo>> GetSuperDealCategory()
		{
			ApiResponse<List<SuperDealCategoryInfo>> result = ApiHelper.CallAPI<ApiResponse<List<SuperDealCategoryInfo>>>(
				"GET",
				ApiHelper.MakeUrl("api/superdeal/GetSuperDealCategory")
			);
			return result;
		}
		//미사용
		public ApiResponse<List<SuperDealItem>> GetSuperDealItems(string code)
		{
			ApiResponse<List<SuperDealItem>> result = ApiHelper.CallAPI<ApiResponse<List<SuperDealItem>>>(
				"GET",
				ApiHelper.MakeUrl("api/superdeal/GetSuperDealItems"),
				new 
				{
					code = code
				}
			);
			return result;
		}

		public ApiResponse<List<SuperDealCategoryV2>> GetSuperDealCategoryV2()
		{
			ApiResponse<List<SuperDealCategoryV2>> result = ApiHelper.CallAPI<ApiResponse<List<SuperDealCategoryV2>>>(
				"GET",
				ApiHelper.MakeUrl("api/superdeal/GetSuperDealCategoryV2")
			);
			return result;
		}

		public ApiResponse<List<SuperDealCategory>> GetSuperDealThemeCategory()
		{
			ApiResponse<List<SuperDealCategory>> result = ApiHelper.CallAPI<ApiResponse<List<SuperDealCategory>>>(
				"GET",
				ApiHelper.MakeUrl("api/SuperDeal/GetSuperDealThemeCategory")
			);
			return result;
		}

		public ApiResponse<List<SuperDealItem>> GetSuperDealItemsV2(string code)
		{
			ApiResponse<List<SuperDealItem>> result = ApiHelper.CallAPI<ApiResponse<List<SuperDealItem>>>(
				"GET",
				ApiHelper.MakeUrl("api/superdeal/GetSuperDealItemsV2"),
				new
				{
					code = code
				}
			);
			return result;
		}
		/// <summary>
		/// 슈퍼딜 상품 목록
		/// </summary>
		/// <param name="displayType"></param>
		/// <param name="gdlcCd"></param>
		/// <param name="gdmcCd"></param>
		/// <returns></returns>
		public ApiResponse<List<HomeMainItem>> GetSuperDealThemeItem(string displayType, string gdlcCd, string gdmcCd)
		{
			ApiResponse<List<HomeMainItem>> result = ApiHelper.CallAPI<ApiResponse<List<HomeMainItem>>>(
				"GET",
				ApiHelper.MakeUrl("api/superdeal/GetSuperDealThemeItem"),
				new
				{
					 displayType=displayType
					 ,gdlcCd=gdlcCd
					 ,gdmcCd=gdmcCd
				}
			);
			return result;
		}
		/// <summary>
		/// 슈퍼딜 테마 메인아이템
		/// </summary>
		/// <returns></returns>
        public ApiResponse<List<HomeMainItem>> GetSuperDealThemeMainItem(string userInfo="", string code="")
		{
            ApiResponse<List<HomeMainItem>> result;
            if (code == "")
            {
                result = ApiHelper.CallAPI<ApiResponse<List<HomeMainItem>>>(
                "GET",
                ApiHelper.MakeUrl("api/superdeal/GetSuperDealThemeMainItem")
                );
            }
            else
            {
                result = ApiHelper.CallAPI<ApiResponse<List<HomeMainItem>>>(
                   "GET",
                   ApiHelper.MakeUrl("api/superdeal/GetSuperDealItemsV2"),
                   new
                   {
                       code = code
                   },
                new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, userInfo)
               );
            }
            return result;
			/*ApiResponse<List<HomeMainItem>> result = ApiHelper.CallAPI<ApiResponse<List<HomeMainItem>>>(
				"GET",
				ApiHelper.MakeUrl("api/superdeal/GetSuperDealThemeMainItem")
			);
			return result;
             * */
		}

		#region [Pilot - gsohn] Pilot Test 코드 (테스트 완료후 삭제)
		public ApiResponse<List<SuperDealItem>> GetSuperDealItemsV2Pilot(string userInfo, string code)
		{
			ApiResponse<List<SuperDealItem>> result = ApiHelper.CallAPI<ApiResponse<List<SuperDealItem>>>(
				"GET",
				ApiHelper.MakeUrl("api/superdeal/GetSuperDealItemsV2"),
				new
				{
					code = code
				},
				new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, userInfo)
			);
			return result;
		}
		#endregion
	}

	//public class SuperDealApiDac : MobileAPIDacBase
	//{
	//    public ApiResponse<List<SuperDealCategoryInfo>> GetSuperDealCategory()
	//    {
	//        ApiResponse<List<SuperDealCategoryInfo>> result = ApiHelper.ExecuteAPI<List<SuperDealCategoryInfo>>(
	//            "/api/superdeal/GetSuperDealCategory"
	//        );
	//        return result;
	//    }

	//    public ApiResponse<List<SuperDealItem>> GetSuperDealItems(string code)
	//    {
	//        ApiResponse<List<SuperDealItem>> result = ApiHelper.ExecuteAPI<List<SuperDealItem>>(
	//            "/api/superdeal/GetSuperDealItems",
	//            ApiHelper.CreateParameter("code", code)
	//        );
	//        return result;
	//    }
	//}
}

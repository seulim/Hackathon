using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.Web.Context;

namespace GMKT.GMobile.Data
{
	public class DeliveryApiDac : ApiBase
	{
		public DeliveryApiDac() : base("GMApi") { }

		public ApiResponse<DaumAddressToCoordChannel> GetAddressSearch(string keyword)
		{
			ApiResponse<DaumAddressToCoordChannel> result = ApiHelper.CallAPI<ApiResponse<DaumAddressToCoordChannel>>(
				"GET",
				ApiHelper.MakeUrl("api/Delivery/GetAddressSearch"),
				new
				{
					keyword = keyword
				}
			);

			return result;
		}

		public ApiResponse<DaumCoordToAddress> GetCoordToAddress(double longitude, double latitude)
		{
			ApiResponse<DaumCoordToAddress> result = ApiHelper.CallAPI<ApiResponse<DaumCoordToAddress>>(
				"GET",
				ApiHelper.MakeUrl("api/Delivery/GetCoordToAddress"),
				new
				{
					longitude = longitude,
					latitude = latitude
				}
			);

			return result;
		}

		public ApiResponse<DeliveryBannerCategory> GetDeliveryBannerCategory(string userInfo, double longitude, double latitude, string zipCode)
		{
			ApiResponse<DeliveryBannerCategory> result = ApiHelper.CallAPI<ApiResponse<DeliveryBannerCategory>>(
				"GET",
				ApiHelper.MakeUrl("api/Delivery/GetDeliveryBannerCategory"),
				new
				{
					longitude = longitude,
					latitude = latitude,
					zipCode = zipCode
				},
				new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, userInfo)
			);

			return result;
		}

		public ApiResponse<DeliveryMain> GetDeliveryMain( string userInfo )
		{
			ApiResponse<DeliveryMain> result = ApiHelper.CallAPI<ApiResponse<DeliveryMain>>(
				"GET",
				ApiHelper.MakeUrl( "api/Delivery/GetDeliveryMain" ),
				new { },
				new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, userInfo)
			);

			return result;
		}

		public ApiResponse<DeliveryMain> GetDeliveryShop(string userInfo, double longitude, double latitude, string zipCode, int myShopPageCount)
		{
			ApiResponse<DeliveryMain> result = ApiHelper.CallAPI<ApiResponse<DeliveryMain>>(
				"GET",
				ApiHelper.MakeUrl("api/Delivery/GetDeliveryShop"),
				new
				{
					longitude = longitude,
					latitude = latitude,
					zipCode = zipCode,
					myShopPageCount = myShopPageCount
				},
				new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, userInfo)
			);

			return result;
		}

		public ApiResponse<DeliveryAgree> GetDeliveryAgreeInfo(string userInfo)
		{
			ApiResponse<DeliveryAgree> result = ApiHelper.CallAPI<ApiResponse<DeliveryAgree>>(
				"GET",
				ApiHelper.MakeUrl("api/Delivery/GetDeliveryAgreeInfo"),
				new {},
				new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, userInfo)
			);

			return result;
		}

		public ApiResponse<int> GetSetDeliveryAgreeInfo( DeliveryPosityionAddType type, string yn, string userInfo )
		{
			ApiResponse<int> result = ApiHelper.CallAPI<ApiResponse<int>>(
				"GET",
				ApiHelper.MakeUrl( "api/Delivery/GetSetDeliveryAgreeInfo" ),
				new	{
					type = type.ToString( "g" )
					,	agreeYN = yn
				},
				new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, userInfo)
			);
			return result;
		}
	}
}

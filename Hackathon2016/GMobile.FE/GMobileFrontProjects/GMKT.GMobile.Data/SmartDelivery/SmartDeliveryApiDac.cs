using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data.SmartDelivery
{
	public class SmartDeliveryApiDac : ApiBase
	{
		public SmartDeliveryApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<List<SmartDeliverCatetoryModel>> GetSmartDeliveryGDLCList()
		{
			ApiResponse<List<SmartDeliverCatetoryModel>> result = ApiHelper.CallAPI<ApiResponse<List<SmartDeliverCatetoryModel>>>(
				"GET",
				ApiHelper.MakeUrl("api/SmartDelivery/GetSmartDeliveryGDLCList")
			);
			return result;
		}

		public ApiResponse<List<SmartDeliveryBannerModel>> GetSmartDeliveryBannerList()
		{
			ApiResponse<List<SmartDeliveryBannerModel>> result = ApiHelper.CallAPI<ApiResponse<List<SmartDeliveryBannerModel>>>(
				"GET",
				ApiHelper.MakeUrl("api/SmartDelivery/GetSmartDeliveryBannerList")
			);
			return result;
		}

		public ApiResponse<List<SmartDeliveryDisplayModel>> GetSmartDeliveryDisplayList(string displayType)
		{
			ApiResponse<List<SmartDeliveryDisplayModel>> result = ApiHelper.CallAPI<ApiResponse<List<SmartDeliveryDisplayModel>>>(
				"GET",
				ApiHelper.MakeUrl("api/SmartDelivery/GetSmartDeliveryDisplayList"),
				new
				{
					displayType = displayType
				}
			);
			return result;
		}

		public ApiResponse<SmartDeliveryBestT> GetSmartDeliveryBest50()
		{
			ApiResponse<SmartDeliveryBestT> result = ApiHelper.CallAPI<ApiResponse<SmartDeliveryBestT>>(
				"GET",
				ApiHelper.MakeUrl("api/SmartDelivery/GetSmartDeliveryBest50")
			);
			return result;
		}


		public ApiResponse<Search.SRPResultModel> GetSmartDeliverySearchResult(SmartDeliverySearchRequest request)
		{
			int timeout = 8000;

			ApiResponse<Search.SRPResultModel> result = ApiHelper.CallAPI<ApiResponse<Search.SRPResultModel>>(
				"GET",
				ApiHelper.MakeUrl("api/SmartDelivery/GetSmartDeliverySearchResult"),
				request, 
				timeout
			);
			return result;
		}
	}
}

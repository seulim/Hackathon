using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.SmartDelivery;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Biz.SmartDelivery
{
	public class SmartDeliveryApiBiz
	{
		public List<SmartDeliverCatetoryModel> GetSmartDeliveryGDLCList()
		{
			List<SmartDeliverCatetoryModel> result = new List<SmartDeliverCatetoryModel>();

			ApiResponse<List<SmartDeliverCatetoryModel>> response = new SmartDeliveryApiDac().GetSmartDeliveryGDLCList();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<SmartDeliverCatetoryModel>();

			return result;
		}

		public List<SmartDeliveryBannerModel> GetSmartDeliveryBannerList()
		{
			List<SmartDeliveryBannerModel> result = new List<SmartDeliveryBannerModel>();

			ApiResponse<List<SmartDeliveryBannerModel>> response = new SmartDeliveryApiDac().GetSmartDeliveryBannerList();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<SmartDeliveryBannerModel>();

			return result;
		}

		public List<SmartDeliveryDisplayModel> GetSmartDeliveryDisplayList(string displayType)
		{
			List<SmartDeliveryDisplayModel> result = new List<SmartDeliveryDisplayModel>();

			ApiResponse<List<SmartDeliveryDisplayModel>> response = new SmartDeliveryApiDac().GetSmartDeliveryDisplayList(displayType);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<SmartDeliveryDisplayModel>();

			return result;
		}

		public SmartDeliveryBestT GetSmartDeliveryBest50()
		{
			SmartDeliveryBestT result = new SmartDeliveryBestT();
			ApiResponse<SmartDeliveryBestT> response = new SmartDeliveryApiDac().GetSmartDeliveryBest50();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new SmartDeliveryBestT();

			return result;
		}

		public SRPResultModel GetSmartDeliverySearchResult(SmartDeliverySearchRequest request)
		{
			SRPResultModel result = new SRPResultModel();
			ApiResponse<SRPResultModel> response = new SmartDeliveryApiDac().GetSmartDeliverySearchResult(request);
			
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new SRPResultModel();

			return result;
		}
	}
}

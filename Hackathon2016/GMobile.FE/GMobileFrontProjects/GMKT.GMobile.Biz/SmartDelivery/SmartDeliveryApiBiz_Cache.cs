using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data.SmartDelivery;

namespace GMKT.GMobile.Biz.SmartDelivery 
{
	public class SmartDeliveryApiBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 60)]
		public List<SmartDeliverCatetoryModel> GetSmartDeliveryGDLCList()
		{
			return new SmartDeliveryApiBiz().GetSmartDeliveryGDLCList();
		}

		[CacheDuration(DurationSeconds = 60)]
		public List<SmartDeliveryBannerModel> GetSmartDeliveryBannerList()
		{
			return new SmartDeliveryApiBiz().GetSmartDeliveryBannerList();
		}

		[CacheDuration(DurationSeconds = 60)]
		public List<SmartDeliveryDisplayModel> GetSmartDeliveryDisplayList(string displayType)
		{
			return new SmartDeliveryApiBiz().GetSmartDeliveryDisplayList(displayType);
		}

		[CacheDuration(DurationSeconds = 60)]
		public SmartDeliveryBestT GetSmartDeliveryBest50()
		{
			return new SmartDeliveryApiBiz().GetSmartDeliveryBest50();
		}
	}
}

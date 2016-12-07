using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.DisplayDB;
using GMobile.Service.Display;
using GMKT.MobileCache;

namespace GMKT.GMobile.Biz.EventV2
{
	public class EventShoppingGroup_Cashe : CacheContextObject
	{
		
		[CacheDuration(DurationSeconds = 300)]
		public List<MobileShopEventGroupT> GetMobileEventGroupItemList(string groupNo, string sGroupNo)
		{
			if (string.IsNullOrEmpty(sGroupNo))
				return new EventShoppingGroupBiz().GetMobileEventGroupItemList(int.Parse(groupNo), null);
			else
				return new EventShoppingGroupBiz().GetMobileEventGroupItemList(int.Parse(groupNo), int.Parse(sGroupNo));
		}

		[CacheDuration(DurationSeconds = 300)]
		public EventShoppingLargeGroupT GetMobileEventLargeGroupinfo(string groupNo)
		{
			return new EventShoppingGroupBiz().GetMobileEventLargeGroupinfo(int.Parse(groupNo));
		}

		[CacheDuration(DurationSeconds = 300)]
		public List<EventShoppingSmallGroupT> GetMobileEventSmallGroupinfo(string groupNo, string sGroupNo)
		{
			if(string.IsNullOrEmpty(sGroupNo))
				return new EventShoppingGroupBiz().GetMobileEventSmallGroupinfo(int.Parse(groupNo), null);
			else
				return new EventShoppingGroupBiz().GetMobileEventSmallGroupinfo(int.Parse(groupNo), int.Parse(sGroupNo));
		}

		[CacheDuration(DurationSeconds = 300)]
		public List<ShopMobileEventGroupInfo> GetShopEventGroupInfo(string sid)
		{
			return new EventShoppingGroupBiz().GetShopEventGroupInfo(int.Parse(sid));
		}

		[CacheDuration(DurationSeconds = 300)]
		public List<ShopMobileEventGroupGoods> GetShopEventGoodsList(string sid, string gid)
		{
			return new EventShoppingGroupBiz().GetShopEventGoodsList(int.Parse(sid), int.Parse(gid));
		}

	}
	
}
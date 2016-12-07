using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.DisplayDB;
using GMobile.Service.Display;
using GMKT.MobileCache;
using GMKT.GMobile.Biz.EventV2;

namespace GMKT.GMobile.Web.Models
{
	/// <summary>
	/// 신규 모바일 기획전 상품 리스트 모델
	/// </summary>
	public class EventShoppingGroupModel
	{
		public List<MobileShopEventGroupT> GoodsList;

		public EventShoppingLargeGroupT LargeGroup;

		public List<EventShoppingSmallGroupT> SmallGroups;
	}

	public class MobileGroupEntity : CacheContextObject
	{
		public EventShoppingGroupModel GetEventGroupGoodsInfo(string groupNo, string sGroupNo)
		{
			return new EventShoppingGroupModel()
			{
				GoodsList = new EventShoppingGroup_Cashe().GetMobileEventGroupItemList(groupNo, null),
				LargeGroup = new EventShoppingGroup_Cashe().GetMobileEventLargeGroupinfo(groupNo),
				SmallGroups = new EventShoppingGroup_Cashe().GetMobileEventSmallGroupinfo(groupNo, null)
			};
		}
	}
	
}
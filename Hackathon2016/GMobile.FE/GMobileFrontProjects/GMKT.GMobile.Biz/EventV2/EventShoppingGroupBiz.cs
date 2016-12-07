using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.DisplayDB;
using GMobile.Service.Display;
using GMKT.MobileCache;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Biz.EventV2
{
	public class EventShoppingGroupBiz : BizBase
	{
		public List<MobileShopEventGroupT> GetMobileEventGroupItemList(int groupNo, int? sGroupNo)
		{
			List<MobileShopEventGroupT> result = new MobileShopEventGroupDac().SelectMobileEventGroupListByGroupNo(groupNo, sGroupNo);

			if (result != null && result.Count > 0)
			{
				foreach (var eachResult in result)
				{
					if (false == string.IsNullOrEmpty(eachResult.GdUrl))
					{
						eachResult.GdUrl = Urls.MItemWebURL + "/Item?goodsCode=" + eachResult.GdUrl;
					}
				}
			}

			return result;
		}

		public EventShoppingLargeGroupT GetMobileEventLargeGroupinfo(int groupNo)
		{
			return new MobileShopEventGroupDac().SelectMobileEventLargeGroupinfo(groupNo);
		}

		public List<EventShoppingSmallGroupT> GetMobileEventSmallGroupinfo(int groupNo, int? sGroupNo)
		{
			return new MobileShopEventGroupDac().SelectMobileEventSmallGroupinfo(groupNo, sGroupNo);
		}

		public List<ShopMobileEventGroupInfo> GetShopEventGroupInfo(int sid)
		{
			return new MobileShopEventGroupDac().SelectShopEventGroupInfo(sid);
		}

		public List<ShopMobileEventGroupGoods> GetShopEventGoodsList(int sid, int gid)
		{
			return new MobileShopEventGroupDac().SelectShopEventGoodsList(sid, gid);
		}
	}
	
}
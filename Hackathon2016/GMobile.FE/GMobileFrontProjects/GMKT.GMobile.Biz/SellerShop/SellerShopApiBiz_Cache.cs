using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.SellerShop;
using GMKT.MobileCache;

namespace GMKT.GMobile.Biz.SellerShop
{
	public class SellerShopApiBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 600)]
		public List<SellerShopCategory> GetShopCategories(string sellerId)
		{
			return new SellerShopApiBiz().GetShopCategories(sellerId);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.GMobile.Data;
using GMKT.MobileCache;
using GMKT.GMobile.Biz.Search;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Data.ShopBest;

namespace GMKT.GMobile.Biz
{
	public class ShopBestApiBiz_Cache : CacheContextObject
	{
		[CacheDuration( DurationSeconds = 3600 )]
		public List<BestSellerGroupInfo> GetBestSellerGroupList()
		{
			return new SearchAPIBiz().GetBestSellerGroupList();
		}

		// brandCount 고정값
		[CacheDuration( DurationSeconds = 1800 )]
		public List<Brand> GetBrandList( string groupCode, int brandCount )
		{
			return new ShopBestApiBiz().GetBrandList( groupCode, brandCount );
		}

		// itemCount, pageSize 고정값
		[CacheDuration( DurationSeconds = 300 )]
		public BrandShopsData GetBrandShopList( string groupCode, int brandNo, int pageNo, int pageSize, int itemCount )
		{
			return new ShopBestApiBiz().GetBrandShopList( groupCode, brandNo, pageNo, pageSize, itemCount );
		}

		// itemCount, pageSize 고정값
		[CacheDuration( DurationSeconds = 1800 )]
		public BestShopsData GetBestShopList( string groupCode, int pageNo, int pageSize, int itemCount )
		{
			return new ShopBestApiBiz().GetBestShopList( groupCode, pageNo, pageSize, itemCount );
		}

		// itemCount, pageSize 고정값
		[CacheDuration( DurationSeconds = 1800 )]
		public NewShopsData GetNewShopList( string groupCode, int pageNo, int pageSize, int itemCount )
		{
			return new ShopBestApiBiz().GetNewShopList( groupCode, pageNo, pageSize, itemCount );
		}
	}
}

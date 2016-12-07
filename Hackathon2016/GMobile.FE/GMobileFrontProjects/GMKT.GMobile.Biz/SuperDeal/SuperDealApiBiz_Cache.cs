using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class SuperDealApiBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 60)]
		public List<SuperDealItem> GetSuperDealItems(string code, string fromWhere)
		{
			return new SuperDealApiBiz().GetSuperDealItems(code, fromWhere);
		}

		[CacheDuration(DurationSeconds = 60)]
		public List<SuperDealCategoryV2> GetSuperDealCategory()
		{
			return new SuperDealApiBiz().GetSuperDealCategory();
		}

		[CacheDuration(DurationSeconds = 60)]
		public List<SuperDealCategory> GetSuperDealThemeCategory()
		{
			return new SuperDealApiBiz().GetSuperDealThemeCategory();
		}

		[CacheDuration(DurationSeconds = 60)]
		public List<HomeMainItem> GetSuperDealThemeItem(string displayType, string gdlcCd, string gdmcCd)
		{
			return new SuperDealApiBiz().GetSuperDealThemeItem( displayType,  gdlcCd,  gdmcCd);
		}
		[CacheDuration(DurationSeconds = 60)]
        public List<HomeMainItem> GetSuperDealThemeMainItem()
		{
			return new SuperDealApiBiz().GetSuperDealThemeMainItem();
		}
		
	}
}

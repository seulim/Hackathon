using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.MobileCache;
using GMKT.GMobile.Data;
using GMKT.GMobile.CommonData;

namespace GMKT.GMobile.Biz
{
	public class MobileCommonBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds=3600)]
		public ApiResponse<List<HomeTabNames>> GetMobileHomeTabNames()
		{
			return new MobileCommonBiz().GetMobileHomeTabNames();
		}

		[CacheDuration(DurationSeconds = 7200)]
		public List<HomeTabNames> GetMobileHomeTabList()
		{
			return new MobileCommonBiz().GetMobileHomeTabList();
		}

		[CacheDuration(DurationSeconds = 600)]
		public DynamicHeader GetVipHeader(string code)
		{
			return new MobileCommonBiz().GetDynamicHeader(HeaderTypeEnum.Vip, code);
		}
	}
}

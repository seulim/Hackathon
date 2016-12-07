using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class BizOnApiBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 600)]
		public List<BizOnItem> GetBizOnBest()
		{
			return new BizOnApiBiz().GetBizOnBest();
		}

		[CacheDuration(DurationSeconds = 60)]
		public BizOnHome GetHome()
		{
			return new BizOnApiBiz().GetHome();
		}

		[CacheDuration(DurationSeconds = 600)]
		public List<BizOnCategoryT> GetBizOnCategoryAll(BizOnCategoryType type)
		{
			return new BizOnApiBiz().GetBizOnCategoryAll(type);
		}

		[CacheDuration(DurationSeconds = 600)]
		public List<BizOnCategoryT> GetBizOnSCategoryList(string bizOnMCategoryCode)
		{
			return new BizOnApiBiz().GetBizOnSCategoryList(bizOnMCategoryCode);
		}
	}
}

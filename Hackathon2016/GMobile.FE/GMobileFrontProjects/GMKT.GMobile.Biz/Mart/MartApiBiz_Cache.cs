using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class MartApiBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 60)]
		public MartView GetMartView(string categoryCode)
		{
			return new MartApiBiz().GetMartView(categoryCode);
		}

		[CacheDuration(DurationSeconds = 60)]
		public MartV2View GetMartV2View(string categoryCode, string goodsCode)
		{
			return new MartApiBiz().GetMartV2View(categoryCode, goodsCode);
		}

		[CacheDuration(DurationSeconds = 60)]
		public MartV3View GetMartV3View(string categoryCode, string goodsCode, long categorySeq)
		{
			return new MartApiBiz().GetMartV3View(categoryCode, goodsCode, categorySeq);
		}

		[CacheDuration(DurationSeconds = 60)]
		public MartContentsView GetMartContentsView(long id = 0)
		{
			return new MartApiBiz().GetMartContentsView(id);
		}
	}
}

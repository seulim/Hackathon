using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class TourApiBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 60)]
		public TourMain GetTourItem(long middleGroupNo, long smallGroupNo, int pageNo, int pageSize, TourOrderEnum order)
		{
			return new TourApiBiz().GetTourItem(middleGroupNo, smallGroupNo, pageNo, pageSize, order);
		}
	}
}

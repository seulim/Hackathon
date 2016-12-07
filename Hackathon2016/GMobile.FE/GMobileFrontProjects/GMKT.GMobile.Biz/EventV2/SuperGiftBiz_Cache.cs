using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.MobileCache;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Biz.EventV2
{
	public class SuperGiftBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 300)]
		public SuperGiftJsonEntityT GetSuperGiftEntity()
		{
			return new SuperGiftBiz().GetSuperGiftEntity();
		}
	}
}

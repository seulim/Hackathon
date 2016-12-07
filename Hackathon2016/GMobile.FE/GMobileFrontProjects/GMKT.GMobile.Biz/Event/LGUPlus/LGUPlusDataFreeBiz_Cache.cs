using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Biz.Util;
using GMKT.MobileCache;
using NetTools;

namespace GMKT.GMobile.Biz
{
	public class LGUPlusDataFreeBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 21600)]
		public IPChecker GetUplusIpAddressRange()
		{
			return new LGUPlusDataFreeBiz().GetUplusIpAddressRange();
		}
	}
}

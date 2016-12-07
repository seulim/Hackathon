using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Biz.EventV2
{
	public class GStampGMileageBiz_Cache : CacheContextObject
	{
		public GStampDataT GetGStampInfo()
		{
			return new GStampGMileageApiBiz().GetGStampInfo();
		}

		public GMileageDataT GetGMileageInfo()
		{
			return new GStampGMileageApiBiz().GetGMileageInfo();
		}
	}
}

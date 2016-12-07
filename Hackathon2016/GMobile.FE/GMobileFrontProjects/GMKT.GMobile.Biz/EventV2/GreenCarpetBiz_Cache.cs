using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Biz.EventV2
{
	public class GreenCarpetBiz_Cache : CacheContextObject
	{
		public GreenCarpetT GetGreenCarpetInfo()
		{
			return new GreenCarpetApiBiz().GetGreenCarpetInfo();
		}
	}
}

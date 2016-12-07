using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;
using GMKT.GMobile.Biz.EventV2;

namespace GMKT.GMobile.Biz
{
	public class PluszoneNewBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 60)]
		public ApiResponse<AttendanceCheckTotal> GetAttendanceCheckTotalList()
		{
			return new PluszoneNewBiz().GetAttendanceCheckTotalList();
		}
	}
}

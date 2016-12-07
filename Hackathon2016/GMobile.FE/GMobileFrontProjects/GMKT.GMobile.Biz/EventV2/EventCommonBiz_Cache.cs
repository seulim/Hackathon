using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.MobileCache;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Biz.EventV2
{
	public class EventCommonBiz_Cache : CacheContextObject
	{
		[CacheDuration( DurationSeconds = 300 )]
		public List<CommonBannerT> GetCommonTopBanner( string eventManageType, string exposeTargetType )
		{
			return new EventCommonBiz().GetCommonTopBanner( eventManageType, exposeTargetType );
		}

        [CacheDuration(DurationSeconds = 3600)]
        public List<NavigationIconT> GetNavigationIcons()
        {
            return new EventCommonBiz().GetNavigationIcons();
        }
	}
}

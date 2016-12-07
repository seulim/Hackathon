using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.MobileCache;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Biz.EventV2
{
	public class CardPointBiz_Cache : CacheContextObject
	{
		[CacheDuration( DurationSeconds = 300 )]
		public CardBenefitJsonEntityT GetCardBenefitEntity()
		{
			return new CardPointApiBiz().GetCardBenefitEntity();
		}

		[CacheDuration( DurationSeconds = 300 )]
		public PointBenefitJsonEntityT GetPointBenefitEntity()
		{
			return new CardPointApiBiz().GetPointBenefitEntity();
		}
	}
}

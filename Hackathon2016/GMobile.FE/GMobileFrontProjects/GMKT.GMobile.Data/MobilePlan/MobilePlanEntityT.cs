using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
    public class MobileShopPlanT
    {
        public Nullable<Int32> Sid { get; set; }
        public string Title { get; set; }
        public string GdlcCd { get; set; }
        public string BannerUrl { get; set; }
        public string MobileThumbImageUrl { get; set; }
        public string MobileBannerImageUrl { get; set; }
        public string MobilePromoteText { get; set; }
    }

	public class MobilePlanViewT
	{
		public Nullable<Int32> Sid { get; set; }
		public string Title { get; set; }
		public string GdlcCd { get; set; }
		public string BannerUrl { get; set; }
		public string MobileThumbImageUrl { get; set; }
		public string MobileBannerImageUrl { get; set; }
		public string MobilePromoteText { get; set; }
		public string GroupNm { get; set; }
		public Nullable<Int32> TotalCounts { get; set; }
		public Nullable<Int32> PageCounts { get; set; }
	}
}

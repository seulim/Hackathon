using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Web.Models.EventV2
{
    public class GStampGMileageModel
    {
		public class GStampDataModel : GStampModel
		{
            public List<NavigationIconT> NaviIcons { get; set; }
			public List<CommonBannerT> TopBanner { get; set; }
			public List<StampDealBannerModel> TodayStampDealBanner { get; set; }
			public List<ExchangeCouponT> ExchangeCouponBanner { get; set; }
			public string[] Notice { get; set; }
			public List<StampExchangeBannerModel> StampExchangeBanner { get; set; }
		}

		public class GMileageDataModel
		{
			public long Balance { get; set; }
			public long ExpirableBalance { get; set; }
            public List<NavigationIconT> NaviIcons { get; set; }
			public List<CommonBannerT> TopBanner { get; set; }
			public List<ExchangeCouponModel> ExchangeCouponBanner { get; set; }
		}

		public class StampDealBannerModel : StampDealBannerT
		{			
			public StampEidModel EntryEid { get; set; }
			public StampEidModel ExchangeEid { get; set; }
		}

		public class StampExchangeBannerModel : StampExchangeBannerT
		{
			public StampEidModel EntryEid { get; set; }
			public StampEidModel ExchangeEid { get; set; }
		}

		public class StampEidModel
		{
			public int Eid { get; set; }
			public string EidString { get; set; }
			public string EncryptString { get; set; }
			public int NeedsStampAmount { get; set; }

			public void setEidScript(string eidScript)
			{
				string[] eidArray = eidScript.Replace("'", "").Split(',');

				if (eidArray.Length > 1)
				{
					EidString = eidArray[0];
					EncryptString = eidArray[1];
				}
			}
		}

		public class ExchangeCouponModel
		{
			public string[] EncryptEid { get; set; }
			public string ImageMobile { get; set; }
			public string Alt { get; set; }
			public string AppMinusCnt { get; set; }
		}
    }
}

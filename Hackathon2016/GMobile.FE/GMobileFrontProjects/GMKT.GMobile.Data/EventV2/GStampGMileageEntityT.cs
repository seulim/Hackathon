using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.EventV2
{
	public class GStampDataT
	{
		public List<BannerInfoT> TopBanner { get; set; }
		public List<StampDealBannerT> TodayStampDealBanner { get; set; }
		public List<ExchangeCouponT> ExchangeCouponBanner { get; set; }
		public List<NoticeT> NoticeList { get; set; }
		public List<StampExchangeBannerT> StampExchangeBanner { get; set; }
	}
	
    /*
	public class BannerInfoT
	{
		public int Seq { get; set; }
		public int Priority { get; set; }
		public string Geid { get; set; }
		public string Eid { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public string Image { get; set; }
		public string ImageMobile { get; set; }
		public string Alt { get; set; }
		public string GuideImage { get; set; }
		public string GuideAlt { get; set; }
		public string DisplayStartDate { get; set; }
		public string DisplayEndDate { get; set; }
	}*/

	public class StampDealBannerT : BannerInfoT
	{
		public int GiveawayAmt { get; set; }
		public int AppMinusCnt { get; set; }

		public int EntryEid { get; set; }
		public string EntryEidScript { get; set; }
		public int EntryAmt { get; set; }

		public int ExchangeEid { get; set; }
		public string ExchangeEidScript { get; set; }
		public int ExchangeAmt { get; set; }

		public bool CompleteExchange { get; set; }
	}

    /*
	public class ExchangeCouponT : BannerInfoT
	{
		public string Type { get; set; }
		public List<EIDInfoT> EIDInfoList { get; set; }
	}*/

    /*
	public class EIDInfoT
	{
		public string EtcSetupContent { get; set; }
		public string Eid { get; set; }
		public string AppWay { get; set; }
		public string AppMinusCnt { get; set; }
	}*/

    /*
	public class NoticeT
	{
		public int Seq { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string NoticeType { get; set; }
		public string NoticeImageUrl { get; set; }
		public string NoticeImageText { get; set; }
		public string DisplayStartDate { get; set; }
		public string DisplayEndDate { get; set; }
	}*/

	public class GMileageDataT
	{
		public List<BannerInfoT> TopBanner { get; set; }
		public List<ExchangeCouponT> ExchangeCouponBanner { get; set; }
	}
}

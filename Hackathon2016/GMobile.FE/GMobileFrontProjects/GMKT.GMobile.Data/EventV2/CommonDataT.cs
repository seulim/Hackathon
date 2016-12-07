using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.EventV2
{

    public class ExchangeCouponT : BannerInfoT
    {
        public string Type { get; set; }
        public int ValidCount { get; set; }
        public List<EIDInfoT> EIDInfoList { get; set; }
    }

    public class EIDInfoT
    {
        public string EtcSetupContent { get; set; }
        public string Eid { get; set; }
        public string AppWay { get; set; }
        public string AppMinusCnt { get; set; }
    }

    public class BannerInfoT
    {
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
		public string MobileUrl { get; set; }
    }

    public class NoticeT
    {
        public string Title { get; set; }
        public string Content { get; set; }
		public string MobileContent { get; set; }
        public string NoticeType { get; set; }
        public string NoticeImageUrl { get; set; }
        public string NoticeImageText { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
    }

	public class StampExchangeBannerT
	{
		public int EventzoneDispSeq { get; set; }
		public int Priority { get; set; }
		public string EventManageType { get; set; }
		public string ExposeTargetType { get; set; }
		public int GiveawayAmt { get; set; }
		public string BannerNm { get; set; }
		public string Image { get; set; }
		public string ImageMobile { get; set; }

		public int EntryEid { get; set; }
		public string EntryEidScript { get; set; }
		public int EntryAmt { get; set; }

		public int ExchangeEid { get; set; }
		public string ExchangeEidScript { get; set; }
		public int ExchangeAmt { get; set; }

		public bool CompleteExchange { get; set; }
		public string DisplayStartDate { get; set; }
		public string DisplayEndDate { get; set; }
	}

    public class PluszoneBannerT
    {
        public PluszoneBannerT(string imageUrl, string imageAlt, string imageLinkUrl = "", string eid = "")
        {
            ImageUrl = imageUrl;
            ImageAlt = imageAlt;
            ImageLinkUrl = imageLinkUrl;
            Eid = eid;
        }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }
        public string ImageLinkUrl { get; set; }
        public string Eid { get; set; }
    }

	public class GuideImage
	{
		public long seq { get; set; }
		public int priority { get; set; }
		public string disp_start_dt { get; set; }
		public string disp_end_dt { get; set; }
		public string image_mobile { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.EventV2
{
    public class PluszoneDataT
    {
        public BannerInfoT Roulette { get; set; }
        public List<BannerInfoT> AttendanceBanner { get; set; }
        public List<BannerInfoT> AttendanceBannerGlobal { get; set; }
        public List<BannerInfoT> MainBanner { get; set; }
        public List<BannerInfoT> PromotionBanner { get; set; }
        public List<BannerInfoT> CategoryBanner { get; set; }
        public List<BannerInfoT> MobileBanner { get; set; }
        public List<BannerInfoT> CardBanner { get; set; }
        public List<BannerInfoT> BandBanner { get; set; }
		public List<GuideImageT> MobileGuidePopup { get; set; }
        public List<WinnerT> WinnerList { get; set; }
        public List<NoticeT> NoticeList { get; set; }
    }

    /*
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
    }*/

    public class WinnerT
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string SubKind { get; set; }
        public string Today { get; set; }
    }

    /*
    public class NoticeT
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string NoticeType { get; set; }
        public string NoticeImageUrl { get; set; }
        public string NoticeImageText { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
    }*/
}

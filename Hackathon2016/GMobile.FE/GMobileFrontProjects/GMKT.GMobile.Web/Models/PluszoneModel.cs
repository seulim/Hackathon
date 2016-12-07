using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Web.Models
{
    public class PluszoneModel : ILandingBannerModel
	{
		public const int REQUIRED_ATTENDANCE_COUNT = 10;

		public int StartDayOfThisMonth { get; set; }
		public int EndDayOfThisMonth { get; set; }
		public DateTime Today { get; set; }

		public int AttendanceCount { get; set; }
		public List<int> AttendanceDate { get; set; }

		public bool IsApply { get; set; }

		public List<EventzoneBannerModel> PluszoneCouponList { get; set; }

        public List<MobileShopPlan> MobileShopPlanList { get; set; }
        public List<CouponBenefitItem> CouponBenefitItem { get; set; }
        public List<PopularPlanQuickLink> PopularPlanQuickLink { get; set; }

        public ILandingBannerEntityT LandingBanner { get; set; }
        public ICampaign Campaign { get; set; }

        public BannerInfoT Roulette { get; set; }
        public List<BannerInfoT> AttendanceBanner { get; set; }
        public List<BannerInfoT> AttendanceBannerGlobal { get; set; }
        public List<BannerInfoT> MainBanner { get; set; }
        public List<BannerInfoT> PromotionBanner { get; set; }
        public List<BannerInfoT> CategoryBanner { get; set; }
        public List<BannerInfoT> MobileBanner { get; set; }
        public List<BannerInfoT> CardBanner { get; set; }
        public List<BannerInfoT> BandBanner { get; set; }
        public List<WinnerT> WinnerList { get; set; }
        public List<NoticeT> NoticeList { get; set; }

		public PluszoneModel()
		{
			AttendanceDate = new List<int>();
			PluszoneCouponList = new List<EventzoneBannerModel>();
            MobileShopPlanList = new List<MobileShopPlan>();
            CouponBenefitItem = new List<CouponBenefitItem>();
            PopularPlanQuickLink = new List<PopularPlanQuickLink>();

            Roulette = new BannerInfoT();
            AttendanceBanner = new List<BannerInfoT>();
            AttendanceBannerGlobal = new List<BannerInfoT>();
            MainBanner = new List<BannerInfoT>();
            PromotionBanner = new List<BannerInfoT>();
            CategoryBanner = new List<BannerInfoT>();
            MobileBanner = new List<BannerInfoT>();
            CardBanner = new List<BannerInfoT>();
            BandBanner = new List<BannerInfoT>();
            WinnerList = new List<WinnerT>();
            NoticeList = new List<NoticeT>();
		}
	}
}
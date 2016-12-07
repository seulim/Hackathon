using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/*
using GMKT.GMobile.ExternalApi;
using GMKT.GMobile.ExternalApi.GMobileApi;
*/
using GMobile.Data.DisplayDB;
using GMobile.Data.Tiger;
using GMobile.Data.Item;
using GMobile.Data.Diver;
using GMobile.Data.Event;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
	public class HomeModelV2 : ILandingBannerModel
    {
		public string SuperDealLinkUrl { get; set; }
		public List<string> TrackingUrlList { get; set; }
        public List<string> TrackingUrlList2 { get; set; }
		public List<string> HomeAdBannerTrackingUrlList { get; set; }
		public List<HomeMainBanner> HomeAdBanner { get; set; }
        public List<HomeMainBanner> HomeIcon { get; set; }
		public List<HomeMainBanner> HomeIcon2 { get; set; }        

		public List<HomeMainBanner> ExternalMallTopBanner { get; set; }
		public List<HomeMainGroup> ExternalMallListingGroup { get; set; }

		public List<HomeMainGroup> ListingGroup { get; set; }

        public ILandingBannerEntityT LandingBanner { get; set; }
        public ICampaign Campaign { get; set; }

		public int CountOfExternalMallAdBanner { get; set; }

		public bool HasHomeNotice { get; set; }
		public HomeNotice HomeNotice { get; set; }
        public bool HasLGUPlusNotice { get; set; }
        public LGUPlusNotice LGUPlusNotice { get; set; }

		/// <summary>
		/// superdeal 카테고리
		/// </summary>
		public List<HomeMainCategory> SuperDealCategoryList { get; set; }

        /// <summary>
        /// 우선 노출 사용 유무
        /// </summary>
        public bool HasFirstExposureMainBanner { get; set; } 

        /// <summary>
        /// 우선 노출 Random Index
        /// </summary>
        public int FirstExposureMainBannerIndex { get; set; }
        
		public HomeModelV2()
		{
			this.TrackingUrlList = new List<string>();
            this.TrackingUrlList2 = new List<string>();
			this.HomeAdBannerTrackingUrlList = new List<string>();
			this.HomeAdBanner = new List<HomeMainBanner>();
			this.HomeIcon = new List<HomeMainBanner>();
			this.HomeIcon2 = new List<HomeMainBanner>();
			this.ExternalMallTopBanner = new List<HomeMainBanner>();
			this.ExternalMallListingGroup = new List<HomeMainGroup>();
			this.ListingGroup = new List<HomeMainGroup>();
			this.SuperDealCategoryList = new List<HomeMainCategory>();
            this.HasFirstExposureMainBanner = false;
            this.FirstExposureMainBannerIndex = -1;
		}
    }

    public class HomeModel : ILandingBannerModel
    {
		public string SuperDealLinkUrl { get; set; }
		public List<HomeAdBanner> HomeAdBanner { get; set; }
        public List<HomeIcon> HomeIcon { get; set; }
        public List<HomeService> HomeService { get; set; }
        public List<SuperDealItem> SuperDeal { get; set; }
		public List<SuperDealCategoryInfo> SuperDealCategoryList { get; set; }
		public List<HomeServiceSet> HomeServiceBanner { get; set; }
        public List<MobilePopupT> PopupList { get; set; }

        public ILandingBannerEntityT LandingBanner { get; set; }
        public ICampaign Campaign { get; set; }

		public int CountOfExternalMallAdBanner { get; set; }

		public bool HasHomeNotice { get; set; }
		public HomeNotice HomeNotice { get; set; }
        public bool HasLGUPlusNotice { get; set; }
        public LGUPlusNotice LGUPlusNotice { get; set; }
    }

    //public class HomeModel
    //{
		/*  MobileCom API 호출시 
		public AdBannerData[] AdBannerDataArray { get; set; }
		public TodaySpecialOfferGoods[] TodaySpecialOfferGoodsArray { get; set; }
		public BestSellerGoods[] BestSellerGoodsArray { get; set; }
		*/
        //public List<AdBannerT> AdBannerList { get; set; }
        //public List<MobileHomeIconT> MobileHomeIconList { get; set; }
        //public List<TodaySpecialT> TodaySpecialList { get; set; }
        //public List<GmarketTodayItemT> TodaySpecial { get; set; }
        //public List<MainPageBannerT> GoodSeries { get; set; }
        //public List<SearchGoods> BestSellerList { get; set; }
        //public List<MobileShopPlanT> PopularPlanList { get; set; }
        //public List<MobilePlanT> PopularAutoPlanList { get; set; }
        //public int PopularPlanSize { get; set; }
        //public List<MobilePopupT> PopupList { get; set; }
        //public MobileAppNoticeT CurrentMobileAppNotice { get; set; }

		// 2013-05-03 이윤호
		// 프로모션바 GAdmin으로 등록할 수 있기 전까지 사용할 모델
		// 반영시간에 따라 자동으로 문구와 URL 변경할 수 있도록 함
        //public string LinkText { get; set; }
        //public string LinkURL { get; set; }

        //public bool SpecialVisible { get; set; }
        //public string SpecialText { get; set; }
        //public string SpecialLink { get; set; }
    //}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
    public class MobileHomeTotalListApiResponse : ApiResponseBase
    {
        public HomeTotalList Data { get; set; }
    }

    public class MobileHomeAdBannerApiResponse : ApiResponseBase
    {
        public List<HomeAdBanner> Data { get; set; }
    }

    public class MobileHomeIconApiResponse : ApiResponseBase
    {
        public List<HomeIcon> Data { get; set; }
    }

    public class MobileHomeServiceApiResponse : ApiResponseBase
    {
        public List<HomeService> Data { get; set; }
    }

    public class MobileHomeSuperDealApiResponse : ApiResponseBase
    {
        public List<HomeService> Data { get; set; }
    }

    #region Sub Entities
	#region V2
	public enum HomeMainGroupType
	{
		AdBanner,
		Icon,
		SuperdealTitle,
		SuperdealItem,
		SuperdealCategory,
		FamilySite,
		ListingBanner1,
		ListingBanner2,
		ListingBanner3,
		ExternalMallAdBanner,
		ExternalMallTopBanner,
		ExternalMallTitle,
		ExternalMallItem,
        SuperdealThemeCategory,
		ListingDABanner
	}

	public enum DeliveryTagType
	{
		None,
		Free,
		Smart,
		Homeplus
	}

	public class HomeTotalListV2
    {
		public List<string> TrackingUrlList { get; set; }
        public List<string> TrackingUrlList2 { get; set; }
		public List<HomeMainGroup> HomeMainGroupList { get; set; }

		public bool HasHomeNotice { get; set; }
		public HomeNotice HomeNotice { get; set; }
        public bool HasLGUPlusNotice { get; set; }
        public LGUPlusNotice LGUPlusNotice { get; set; }
    }

	public class HomeMainGroup
	{
		public List<string> TrackingUrlList { get; set; }
		public HomeMainGroupType Type { get; set; }
		public string Title { get; set; }
		public string LinkUrl { get; set; }
		public string PdsLogJson { get; set; }
		public string FcdCode { get; set; }
		public List<HomeMainBanner> BannerList { get; set; }
		public List<HomeMainItem> ItemList { get; set; }
		public List<HomeMainCategory> CategoryList { get; set; }
        public int FirstExposureMainBannerIndex { get; set; }
        public bool HasFirstExposureMainBanner { get; set; } 
	}

	public class HomeMainBanner
	{
		public Nullable<Int32> Bid { get; set; }
		public string Title { get; set; }
		public string ImageUrl { get; set; }
        public string AnimatedImageUrl { get; set; }
		public string ImageCss { get; set; }
		public string LinkUrl { get; set; }
		public string TrackingUrl { get; set; }
		// 예전에 있었던 건가?? 지금은 안 쓰고?!
		//public Nullable<Byte> TxtImgType { get; set; }
		//public string BnContents { get; set; }
		public string DispStartDt { get; set; }
		public string DispEndDt { get; set; }

		public string Text1 { get; set; }
		public string Text2 { get; set; }
		public string Text3 { get; set; }

		public bool IsNew { get; set; }		
		public bool HasAdTag { get; set; }

		public string Fcd { get; set; }
        public string ImpUrl { get; set; }
		public string PdsLogJson { get; set; }
        public string FirstExposureYn { get; set; }
		public string ClickUrl { get; set; }
		public string BackgroundColor { get; set; }
	}

	public class HomeMainItem
	{
		public string GoodsCode { get; set; }

		public string ItemTitle { get; set; }
		//public string ItemSubTitle { get; set; }
		//public string MainTitle1 { get; set; }
		//public string MainTitle2 { get; set; }

		public string ImageUrl { get; set; }
		public string LinkUrl { get; set; }

		public DateTime DispStartDate { get; set; }
		public DateTime DispEndDate { get; set; }
		public bool IsDisplay { get; set; }

		public int Priority { get; set; }
		public int RankPoint { get; set; }

		public bool HasTagClose { get; set; }
		public bool HasTagLimitAmt { get; set; }
		public bool HasTagNew { get; set; }
		public bool HasTagEncore { get; set; }

		public bool ShowBuyCount { get; set; }
		public bool ShowRemainCount { get; set; }
		public int BuyCount { get; set; }
		public int RemainCount { get; set; }

		public string OriginalPrice { get; set; }
		public int DiscountRate { get; set; }
		public string Price { get; set; }
		public string DiscountPrice { get; set; }
		public string UnitPrice { get; set; }

		public DeliveryTagType DeliveryType { get; set; }		
		public bool IsSoldOut { get; set; }
		public bool IsAdult { get; set; }

		public string SubUrl { get; set; }
		public string ShopGdlcNm { get; set; }
		public string ShopGdlcCd { get; set; }
		public string ShopGdmcCd { get; set; }

		public string ShopGroupCd { get; set; }

		public string Fcd { get; set; }

		/// <summary>
		/// 서비스관/브랜드샾코드 
		/// 
		/// </summary>
		public bool IsStoreInfo { get; set; }
		/// <summary>
		/// 여행상품노출 코드
		/// 0: 사용안함
		/// </summary>
		public bool IsTourInfo { get; set; }

		/// <summary>
		/// 마케팅실전용 코드
		/// 0: 사용안함
		/// </summary>
		public bool IsDeptOnly { get; set; }

		/// <summary>
		/// 하단 연관 컨텐츠 코드
		/// N:미노출
		/// THEME : 테마노출
		/// SALE : 할인쿠폰
		/// </summary>
		public string BottomRelContents { get; set; }

		/// <summary>
		/// 할인쿠폰번호
		/// </summary>
		public string CouponIssueNo { get; set; }

		/// <summary>
		/// 메인노출1
		/// </summary>
		public string ExposeText1 { get; set; }

		/// <summary>
		/// 메인노출2
		/// </summary>
		public string ExposeText2 { get; set; }

		/// <summary>
		/// 연결URL
		/// </summary>
		public string CouponUrl { get; set; }

		/// <summary>
		/// 테마명
		/// </summary>
		public string ThemeNm{get;set;}
		/// <summary>
		/// 메인노출 테마명
		/// </summary>
		public string MainExposeThemeNm { get; set; }

		/// <summary>
		/// 메인노출 테마명클릭시 url
		/// </summary>
		public string MainExposeThemeUrl { get; set; }

		/// <summary>
		/// 스토어Text
		/// </summary>
		public string BrandServiceStoreText { get; set; }

		/// <summary>
		/// 스토어 하위text
		/// </summary>
		public string BrandServiceStoreSubText { get; set; }

		/// <summary>
		/// 스토어 이미지Url
		/// </summary>
		public string BrandServiceStoreImageUrl { get; set; }

		/// <summary>
		/// 스토어 링크
		/// </summary>
		public string BrandServiceStoreLnkUrl { get; set; }

		/// <summary>
		/// 여행상품 문구
		/// </summary>
		public string TourShopServiceStoreText { get; set; }

		/// <summary>
		/// 마케팅실 문구
		/// </summary>
		public string DeptServiceStoreText { get; set; }

		/// <summary>
		/// 스마일캐쉬 유무
		/// </summary>
		public bool IsSmaileCash { get; set; }

		/// <summary>
		/// 스마일 캐쉬 포인트
		/// </summary>
		public int SmileCashPoint { get; set; }

        /// <summary>
        /// 당일 배송유무
        /// </summary>
        public bool StoreSpecialType { get; set; }

		public string PdsLogItemJson { get; set; }
		public string PdsLogServiceShopJson { get; set; }
		public string PdsLogContentsJson { get; set; }
		public string FcdCodeItem { get; set; }
		public string FcdCodeServiceShop { get; set; }
		public string FcdCodeContents { get; set; }
	}

	public class HomeMainCategory : CategoryInfo
	{
		public string CssName { get; set; }
		public string IconOnURL { get; set; }
		public string IconOffURL { get; set; }
		public string Fcd { get; set; }
		public string PdsLogJson { get; set; }
	}
	#endregion

	#region V1
	public class HomeTotalList
    {
		public string SuperDealLinkUrl { get; set; }
        public List<HomeAdBanner> HomeAdBanner { get; set; }
        public List<HomeIcon> HomeIcon { get; set; }
        public List<HomeService> HomeService { get; set; }
        public List<SuperDealItem> SuperDeal { get; set; }
		public List<SuperDealCategoryInfo> SuperDealCategoryList { get; set; }
		public List<HomeServiceSet> HomeServiceBanner { get; set; }
		public List<HomeAdBanner> ExternalMallAdBanner { get; set; }

		public bool HasHomeNotice { get; set; }
		public HomeNotice HomeNotice { get; set; }
        public bool HasLGUPlusNotice { get; set; }
        public LGUPlusNotice LGUPlusNotice { get; set; }
    }

    public class HomeAdBanner
    {
        public Nullable<Int32> Bid { get; set; }
        public string BnTitle { get; set; }
        public string BnLinkUrl { get; set; }
        public string BnImgNm { get; set; }
        public string BnImgPath { get; set; }
        public Nullable<Byte> TxtImgType { get; set; }
        public string BnContents { get; set; }
        public string DispStartDt { get; set; }
        public string DispEndDt { get; set; }
    }

    public class HomeIcon
    {
        public Nullable<Int64> IconSeq { get; set; }
        public string IconText { get; set; }
        public string IconImageUrl { get; set; }
        public string IconLnkUrl { get; set; }
        public Nullable<bool> IsIconNew { get; set; }
    }

    public class HomeService
    {
        public Nullable<Int64> ServiceSeq { get; set; }
        public string ServiceText { get; set; }
        public string ServiceImageUrl { get; set; }
        public string ServiceLnkUrl { get; set; }
        public string TagYn { get; set; }
        public string Fcd { get; set; }
    }

	public class HomeServiceSet
	{
		public Int64 SetSeq { get; set; }
		public char SetType { get; set; }
		public Int32 Priority { get; set; }
		public List<HomeServiceBanner> Banners { get; set; }
	}

	public class HomeServiceBanner
	{
		public Int32 ExposeSeq { get; set; }
		public string ServiceName { get; set; }
		public string LinkUrl { get; set; }
		public string BannerImgUrl { get; set; }
		public string LandScapeBannerImgUrl { get; set; }
	}
	#endregion

	public class MobileDrawerTotal
    {
        public List<GroupCategoryInfo> GroupCategoryInfo { get; set; }
        public List<HomeService> HomeService { get; set; }
    }

	public class HomeNotice
	{
		public long Seq { get; set; }
		public string Title { get; set; }
		public string BannerImageUrl { get; set; }
		public string LandingUrl { get; set; }
		public string PdsLogJson { get; set; }
	}

    public class LGUPlusNotice : HomeNotice
    {
        public string DailySeq { get; set; }
        public string PdsLogImpression { get; set; }
        public string PdsLogClose { get; set; }
        public string PdsLogDetail { get; set; }
        public string PdsLogDoNotSee { get; set; }
    }

    #endregion
}

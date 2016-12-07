using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Data.SellerShop
{
	/*
	* 샵개편: ShopBaseController -> SellerShopBaseController
	* API 호출 방식으로 변경됨에 따라 네이밍을 petapoco의 ~T 대신 ~Data로 변경
	* 참고: GMobileFrontProjects\GMKT.GMobile.Data\ShopDisplayT.cs
	* 원본 파일의 엔티티내 로직들 API에서 처리 하였음
	*/
	public class ShopData
	{
		public SellerData Seller { get; set; }
		//public List<ImageData> MainImg { get; set; }
		public FavoriteShopData Favorite { get; set; }
		public ShopBasicT DisplayInfo { get; set; }
		public string Title { get; set; }
		public string HelpdeskTelNo { get; set; }
		public string Alias { get; set; }
		public string PersonalDomain { get; set; }
		public string SellerId { get; set; } //CustomerNo
		public string CustomerNo { get; set; }
		public char ShopLevel { get; set; }
		public bool IsPlus { get; set; }
		public DateTime StartDatePlusService { get; set; }
		public DateTime EndDatePlusService { get; set; }
		public string Introduction { get; set; }
		public CategoryDisplay CategoryDisplay { get; set; }
		public GeneralCategoryDisplayType GeneralCategoryDisplayType { get; set; }
		public ShopCategoryDisplayType ShopCategoryDisplayType { get; set; }
		public char GoodsCountExposeYn { get; set; }
		public string ShopType { get; set; }

		// Customer Support related
		public string FaxNo { get; set; }
		public string EmailAddress { get; set; }
		public string WeekdayTime { get; set; }
		public string SaturdayTime { get; set; }
		public string HolidayTime { get; set; }
	}

	public class SellerShopPromotion
	{
		public List<ShopDisplayInfoTuple> Main { get; set; }
	}

	public class ShopDisplayInfoTuple
	{
		public ShopDisplayInfoT info { get; set; }
		public List<SellerShopItem> items { get; set; }
	}

	public class ShopDisplayInfoT
	{
		public long ShopDispNo { get; set; }
		public string Title { get; set; }
		public int DispType { get; set; }
		public int ExposeGdCnt { get; set; }
		public int SortType { get; set; }
		public long ImageSeq { get; set; }
		public int ImageRegType { get; set; }
		public string ImageUrl { get; set; }
		public string ImageLinkUrl { get; set; }
		public int DisplayColorType { get; set; }
		public long VdoSeq { get; set; }
		public string VdoPath { get; set; }
		public string VdoKey { get; set; }
		public string HtmlContent { get; set; }
	}

	public class ShopBasicT
	{
		public string Alias { get; set; }
		public string SellerId { get; set; }
		public string Title { get; set; }
		public FavoriteShopData Favorite { get; set; }
		public string ShopType { get; set; }
		public string HelpdeskTelNo { get; set; }
		public long ShopDispNo { get; set; }
		public char ShopLevel { get; set; }
		public int CategoryUseType { get; set; }
		public int GmktCategoryDispType { get; set; }
		public int ShopCategoryDispType { get; set; }
		public char CategoryGdCntExposeYn { get; set; }
		public int CategorySetupApplyType { get; set; }
		public string ShopRepImagePath { get; set; }
		public int MainImageDispType { get; set; }
		public string MainImageDisplayClass { get; set; }
		public int MenuExposeCnt { get; set; }
		public int BookmarkUseType { get; set; }
		public string BookmarkImagePath { get; set; }
		public long MobileShopDispNo { get; set; }
		public CategoryDisplay MobileCategoryUseType { get; set; }
		public GeneralCategoryDisplayType MobileGmktCategoryDispType { get; set; }
		public ShopCategoryDisplayType MobileShopCategoryDispType { get; set; }
		public char MobileCategoryGdCntExposeYn { get; set; }
	}

	public class SellerData
	{
		public string Id { get; set; } //CustomerNo
		public string CustomerNo { get; set; }
		public string UserName { get; set; }	// ID
		public string Name { get; set; }
		public string CompanyName { get; set; }
		public string Manager { get; set; }
		public string PhoneNumber { get; set; }
		public string FaxNo { get; set; }
		public string EmailAddress { get; set; }
		public string BusinessNo { get; set; }
		public string BusinessNoText { get; set; }
		public string OverseasBusinessNo { get; set; }
		public string EcommerceNo { get; set; }
		public string StreetAddress { get; set; }
		public string HelpdeskStartHour { get; set; }
		public string HelpdeskEndHour { get; set; }
		public string CustomerType { get; set; } // @todo: enum으로 변경 예정
		public string SellerGradeValue { get; set; }
		public string SellerGrade { get; set; }
		public string DeliveryGradeValue { get; set; }
		public string DeliveryGrade { get; set; }
		public string CommunicationGradeValue { get; set; }
		public string CommunicationGrade {get; set; }
		public int FeedbackRateValue { get; set; }
		public int FeedbackRate { get; set; }
		public decimal MileageToSave { get; set; }
		public string WeekdayTime { get; set; }
		public string SaturdayTime { get; set; }
		public string HolidayTime { get; set; }
		public string Stat { get; set; }
	}

	public class ImageData
	{
		public string imgURL { get; set; }
		public string imgLinkURL { get; set; }
	}

	public class FavoriteShopData
	{
		public string SellerId { get; set; }
		public string BuyerId { get; set; }
		public DateTime EffectiveDate { get; set; }
		public string Nickname { get; set; }
		public bool IsFavoriteShop { get; set; }
        public long Count { get; set; }

        public string CountStr 
        { 
            get { return (this.Count >= 99999) ? "99999+" : Convert.ToString(this.Count); }
        }
	}

	public class SellerShopItem
	{
		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }
		public string DefaultGoodsImageUrl { get; set; }
		public string LandingUrl { get; set; }
		public string ImageUrl { get; set; }
		public string Image400Url { get; set; }
		public int DiscountRate { get; set; }
		public string SellPrice { get; set; }
		public string AdCouponPrice { get; set; }
		public string Shipping { get; set; }
		public bool AdultYN { get; set; }
		public string BrandName { get; set; }
		public string BrandNameTag
		{
			get
			{
				if(BrandName.Trim() == "기타" || BrandName.Trim() == "기타 (미입력)" || BrandName.Trim() == "")
				{
					return "";
				}
				return "[" + BrandName + "]";
			}
		}

        public SRPItemDeliveryInfo Delivery { get; set; }
        public bool IsFavoriteSeller { get; set; }
        public bool IsPurchasedSeller { get; set; }
        public string BuyCount { get; set; }
		public bool IsCartVisible { get; set; }
	}

	public class ShopBasicData
	{
		public long ShopDispNo { get; set; }
		public char ShopLevel { get; set; }
		public int CategoryUseType { get; set; }
		public int GmktCategoryDispType { get; set; }
		public int ShopCategoryDispType { get; set; }
		public int CategorySetupApplyType { get; set; }
		public int MainImageDispType { get; set; }
		public int MenuExposeCnt { get; set; }
		public int BookmarkUseType { get; set; }
		public string BookmarkImagePath { get; set; }
		public int MobileCategoryUseType { get; set; }
		public int MobileGmktCategoryDispType { get; set; }
		public int MobileShopCategoryDispType { get; set; }
		public char MobileCategoryGdCntExposeYn { get; set; }
	}

	public class SellerShopItemDisplay
	{
		public long AreaNo { get; set; }
		public string Title { get; set; }
		public int DispType { get; set; }
		public int SortType { get; set; }
		public string DispTypeStr { get; set; }
        public char AddYN { get; set; }
        public int ExposeRateType { get; set; }
        public string BgColorCd { get; set; }
        public int AreaUseType { get; set; }
        public string ImageUrl { get; set; }
        public string ImageLnkUrl { get; set; }
	}

	public class ItemDisplayPage
	{
		public List<SellerShopItem> ItemList { get; set; }
		public int PageNo { get; set; }
		public int TotalGoodsCount { get; set; }
		public int PageSize { get; set; }
		public int DispType { get; set; }
		public int SortType { get; set; }
        public string AddYN { get; set; }

        public CategoryBannerInfo PinterestBanner { get; set; }
	}

	public class YoutubeInfo
	{
		public string VodUrl { get; set; }
		public string ImgUrl { get; set; }
		public string Title { get; set; }
		public string Desc { get; set; }
	}

	//샵 메인
	public class SellerShopMain
	{
		public SellerShopItem SelectedItem { get; set; }

		//메인이미지영역
		//public char MainImageExposeYN { get; set; }
		public SellerShopMainImage MainImage { get; set; }
		public List<SellerShopMainImage> MoreMainImages { get; set; }
		//상품전시영역에서 사용
		public long ShopDispNo { get; set; }

		//추가동영상 노출여부, 노출값
		public char AddVdoExposeYN { get; set; }
		public string AddVdoPath { get; set; }
		public string AddVdoKey { get; set; }
		//메뉴탭 노출 여부, 노출값
		public char MenuExposeYN { get; set; }
		public ShopMenuExposeType MenuExposeType { get; set; }
		public List<MenuT> MenuList { get; set; }
		//공지사항노출 여부, 노출값
		public char NoticeExposeYN { get; set; }
		public ShopNewsT FirstNotice { get; set; }

        public SellerShopTopIntro TopIntro { get; set; }
	}

    public class SellerShopTopIntro
    {
        public char ExposeYN { get; set; }
        public TopDispType TopDispType { get; set; }
        public string ImageUrl { get; set; }
    }


	public class MenuT
	{
		public long MenuNo { get; set; }
		public long AreaNo { get; set; }
		public int AreaUseType { get; set; }
		public string MenuNm { get; set; }
		public int MenuUseType { get; set; }
		public int ExposeOrderBy { get; set; }
		public string MenuLnkUrl { get; set; }
	}

	public class SellerShopMainImage
	{
        public string Title { get; set; }
		public List<SellerShopItem> Items { get; set; } // 1.상품전시형 일때 상품정보
		public string ImageUrl { get; set; }
		public string ImageLinkUrl { get; set; }
		public int DisplayColor { get; set; } //2.소이미지형 일때 배경색상 (0-없음,1-청록색,2-핑크색,3-파랑색,4-오렌지색)
		public string VdoPath { get; set; } //6.동영상링크형
		public string VdoKey { get; set; }//6.동영상링크형
	}

	//public class SellerShopNotice
	//{
	//    public long Seq { get; set;}
	//    public string Title {get; set;}
	//    public string Content { get; set; }
	//    public DateTime PostDate { get; set; }
	//}

	//메뉴탭 공지사항 상세보기
	public class SellerShopNoticeDetail : ShopNewsT
	{
		//public string PreviousNoticeLink { get; set; }
		//public string NextNoticeLink { get; set; }
		public string NoticeListLink { get; set; }
	}

	//메뉴탭 공지사항 리스트
	public class SellerShopNoticeList
	{
		public List<ShopNewsT> Notice { get; set; }
		public int NoticeTotalCount { get; set; }
	}

	//샵 메뉴 페이지 
	public class SellerShopMenuContent
	{
		public long MenuNo { get; set; }
		public int MenuUseType { get; set; } //1-홈이동,2-상품전시,3-공지사항,4-링크,5-기획전이벤트
		public int AreaUseType { get; set; }
		public long AreaNo { get; set; }
		//2-상품전시
		public int ItemDispType { get; set; }
		public int ItemSortType { get; set; }
		public int ItemExposeGdCnt { get; set; }
		public string ItemTitle { get; set; }
        public string ItemBgColorCd { get; set; }
		//4-링크
		public string Link { get; set; }
		//5-기획전이벤트
		public string Html { get; set; }
		public string ImageUrl { get; set; }
	}

	public class SellerShopMenuPage
	{
		public List<MenuT> MenuList { get; set; }//메뉴탭 타이틀들 + 링크
		public SellerShopMenuContent SelectedContents { get; set; }// 진입시 선택한 메뉴 내용
	}

	public class SimpleModel
	{
		public bool Success { get; set; }
		public string Message { get; set; }
	}

	//public class SellerShopMenu
	//{
	//    public long MenuNo { get; set; }
	//    public string MenuNM { get; set; }
	//    public string MenuLinkUrl { get; set; }
	//    public int ExposeOrder { get; set; }
	//}

	//샵 카테고리
	public class SellerShopNavigationRequest
	{
		public string SellCustNo { get; set; }
		public int DisplayType { get; set; }
		public bool IsShopCategory { get; set; }
		public string ParentCategory { get; set; }
		public CategoryLevel Level { get; set; }
	}

	public class SellerShopNavigation
	{		
		public string CurrentId { get; set; }
		public string CurrentNm { get; set; }
		public int TotalCount { get; set; }
		public bool IsShopCategory { get; set; }		
		public bool HasParent { get; set; }
		public bool IsDisplayCount { get; set; }
		public List<SellerShopCategory> Children { get; set; }
	}

	public class SellerShopCategory
	{
		public string CategoryId { get; set; }
		public string CategoryNm { get; set; }
		public int CategoryCount { get; set; }
		public string ParentId { get; set; }
		public CategoryLevel Level { get; set; }
		public bool IsLeaf{ get; set; }
		public string LeafYN { get; set; }
	}

	public class SellerShopLPSRP
	{		
		public string ParentCategoryId { get; set; }
		public string CurrentCategoryName { get; set; }
		public string Keyword { get; set; }
		public int TotalGoodsCount { get; set; }
		public List<SellerShopItem> Item { get; set; }
		public List<SRPSearchCategory> Category { get; set; }
		public string LcId { get; set; }
		public string LcIdName { get; set; }
		public string McId { get; set; }
		public string McIdName { get; set; }
		public string ScId { get; set; }
		public string ScIdName { get; set; }

        public CategoryBannerInfo CategoryBannerInfo { get; set; }
	}

	public class SellerShopSearchRequest
	{
		public string MenuName { get; set; }
		public int PageNo { get; set; }
		public int PageSize { get; set; }
		public string Level { get; set; }
		public string LcId { get; set; }
		public string McId { get; set; }
		public string ScId { get; set; }
		public string ShopCategory { get; set; }
		public string Keyword { get; set; }
		public string ScKeyword { get; set; }
		public string ExKeyword { get; set; }
		public string IsFreeDelivery { get; set; }
		public string IsSmartDelivery { get; set; }
		public string IsDiscount { get; set; }
		public string IsMileage { get; set; }
		public string IsStamp { get; set; }
		public decimal MinPrice { get; set; }
		public decimal MaxPrice { get; set; }
		public int SortType { get; set; }
		public string SellCustNo { get; set; }
		public string IsShopCategory { get; set; }
	}

    public class CategoryBannerInfo
    {
        public string ImagePath { get; set; }
        public string ImageLnkUrl { get; set; }
    }

    public class CategoryBannerInfoRequest
    {
        public long ShopDispNo { get; set; }
        public string CategoryCode { get; set; }
    }

	public enum SellerShopSortType
	{
		SellPointDesc = 1,
		New = 2,
		SellPriceAsc = 3,
		ConPointDesc = 4,
		DiscountPriceDesc = 5,
		SellPriceDesc = 6,
		SellerCustomized = 7
	}

	//1이면 1행 플리킹, 2이면 3열고정 
	public enum ShopMenuExposeType
	{
		None = 0,
		OneRowFlicking = 1,
		MoreThanTwoRows = 2
	}

    public enum PageType
    {
        Main = 0,
        List = 1,
        Menu = 2
    }

    public enum TopDispType
    {
        None = 0,
        Basic = 1,
        Image = 2
    }
}

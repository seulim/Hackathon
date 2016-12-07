using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data.Search
{
	public class SRPDirect
	{
		public bool HasSRPDirect { get; set; }
		public string Title { get; set; }
		public string LinkUrl { get; set; }
		public string BannerUrl { get; set; }
		public string PdsLogJson { get; set; }
		public List<CPPLPSRPItemModel> Items { get; set; }
	}

	public class SmartBoxTabEntity
	{
		public string GroupType { get; set; }
		public string Name { get; set; }
		public string TabClickPdsLogJson { get; set; }
		public string MoreClickPdsLogJson { get; set; }		
		public List<SmartBoxTileEntity> TileEntities { get; set; }
		public List<SmartBoxTileEntity> TileMore { get; set; }
	}

	public class SmartBoxModel
	{
		public SRPDirect SRPDirect { get; set; }
		public string DetailApiUrl { get; set; }
		public string DetailApiBody { get; set; }

		public List<SmartBoxTabEntity> TabEntities { get; set; }
		public List<SmartBoxTileEntity> TileHistory { get; set; }
	}

	public class RecommendKeywordModel
	{
		public string Keyword { get; set; }
		public string SmartBoxPostJson { get; set; }
		public string SrpPostJson { get; set; }
		public string PdsLogJson { get; set; }
	}

	public class SearchItemRequest
	{
		public string GoodsCodeList { get; set; }
	}

	public class SearchRequest
	{
		// 차후 기타 페이지에서 사용할 것을 고려해 Required 제외
		public string primeKeyword { get; set; } // 주 검색어
		public string keywordList { get; set; } // 키워드 리스트(연관 검색어에서 사용)
		public string isRecommendKeyword { get; set; } // 연관 검색어 여부
		public string menuName { get; set; } // SRP, LP...
		public int pageSize { get; set; }
		public int pageNo { get; set; }
		public string moreKeyword { get; set; } // 상세 검색어
		public string lcId { get; set; } // 주 카테고리 
		public string mcId { get; set; }
		public string scId { get; set; }
		public string sortType { get; set; } // (보이저) 정렬 
		public string isShippingFree { get; set; } // 무료배송
		public string isSmartDelivery { get; set; } // 스마트배송
		public string isSmallPacking { get; set; } // 스마트배송관 소량포장상품 전용
		public string isMileage { get; set; }
		public string isDiscount { get; set; }
		public string isStamp { get; set; }
		public string isBrand { get; set; } // 브랜드 상품 "Y", "N"
		public string sellCustNo { get; set; } // for PP

		public int minPrice { get; set; }
		public int maxPrice { get; set; }

		public string mClassSeq { get; set; } // 스마트파인더
		public string sClassSeq { get; set; } // 스마트파인더
		public string valueId { get; set; } // 분류속성 코드(스마트파인더에서 제공)
		public string valueIdName { get; set; } //분류속성 이름 (스마트파인더에서 제공)
		public int listingItemGroup { get; set; } // 페이징 하면서 필요한 파라미터.
		public string shopCategory { get; set; } // 샵 카테고리(비즈온)
		public string shopGroupCd { get; set; } // 샵 그룹
		public string branchCode { get; set; } // 홈플러스 지점 번호

		public string brandList { get; set; }  // 브랜드 리스트 delimiter 는 ,
		public string gdNo { get; set; }
		public string attrNo { get; set; }

		public List<SmartBoxTileEntity> tiles { get; set; }
		public string smartBoxGroupType { get; set; }
		public string selectedTileCode { get; set; } // SRP 카테고리 Detail 페이지
		public string isTileDetail { get; set; }
		public SearchExtension searchEx { get; set; }
        public string requestShopCategory { get; set; } 

		public SearchRequest()
		{
			this.isRecommendKeyword = "N";
			this.keywordList = "";
			this.menuName = "SRP";
			this.pageSize = 50;
			this.pageNo = 1;
			this.moreKeyword = "";
			this.lcId = "";
			this.mcId = "";
			this.scId = "";
			this.sortType = "";
			this.isShippingFree = "N";
			this.isSmartDelivery = "N";
			this.isBrand = "N";
			this.isSmallPacking = "N";
			this.isStamp = "N";
			this.isMileage = "N";
			this.isDiscount = "N";
			this.isTileDetail = "N";
			this.sellCustNo = "";
			this.brandList = "";
			this.mClassSeq = "";
			this.sClassSeq = "";
			this.valueId = "";
			this.valueIdName = "";
			this.shopCategory = "";
			this.shopGroupCd = "";
			this.brandList = "";
			this.gdNo = "";
			this.attrNo = "";
			this.tiles = new List<SmartBoxTileEntity>();
			this.smartBoxGroupType = "";
			this.branchCode = "";
			this.searchEx = new SearchExtension();
            this.requestShopCategory = "";
		}
	}

	public class SearchExtension
	{
		public bool isDepartmentStore { get; set; }
		public bool isSmartDeliveryShop { get; set; }
		public string groupCode { get; set; }
	} 


	public class SmartBoxTileEntity
	{
		public string Type { get; set; }
		public bool Selected { get; set; }
		public string Image { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string MoreGroup { get; set; }

		public string DetailApiUrl { get; set; }
		public string DetailApiBody { get; set; }
		public string HistoryName { get; set; }
		public string PdsLogJson { get; set; }
		public string MoreClickPdsLogJson { get; set; }

		public SmartBoxDetailEntity DetailTile { get; set; }

		public override bool Equals(object obj)
		{
			var other = obj as SmartBoxTileEntity;
			if(other == null)
			{
				return false;
			}
			return this.Name == other.Name && Type == other.Type;
		}

		public override int GetHashCode()
		{
			if(Name == null)
			{
				Name = "";
			}
			return Name.GetHashCode();
		}
	}

	public class SmartBoxDetailEntity
	{
		public string CategoryType { get; set; }
		public SmartBoxDetailTile Mtile { get; set; }
		public SmartBoxDetailTile Stile { get; set; }
	}

	public class SmartBoxDetailTile
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public bool Selected { get; set; }
		public string DetailApiUrl { get; set; }
		public string DetailApiBody { get; set; }
	}

	// 모바일 앱/웹 SRP SEARCH 모델
	public class SRPResultModel
	{
		public string ResultItemViewStyle { get; set; }
		public int FocusItemCount { get; set; }
		// 특정검색어에 대해 redirect 를 처리할 경우
		public bool SrpRedirectionYN { get; set; }
		public string SrpRedirectionUrl { get; set; }

		public string LcId { get; set; }
		public string LcIdName { get; set; }
		public string McId { get; set; }
		public string McIdName { get; set; }
		public string ScId { get; set; }
		public string ScIdName { get; set; }
		public string CurrentCategoryLevel { get; set; }
		public string CurrentCategoryName { get; set; }

		public int PageNo { get; set; }
		public int PageSize { get; set; }

		public bool IsShowSmartDeliveryFilter { get; set; }
		public bool IsShowBrandOnlyFilter { get; set; }

		// 바로가기 링크
		public bool ShowDirectLink { get; set; }
		public string DirectLinkURL { get; set; }
		public string DirectLinkText { get; set; }
		public string DirectLinkDesc { get; set; }

        public PromotionBanner KeywordBanner { get; set; }

		public List<CPPLPSRPItemModel> Item { get; set; }
		public List<CPPLPSRPItemModel> TpItem { get; set; }

        public string SmartDeliveryLandingUrl { get; set; }

		// 프리미엄 파트너 리스트
		public List<PartnerT> SPPLists { get; set; }

		public int TotalGoodsCount { get; set; }

		// 2013-04-04 이윤호
		// 모바일 구매 로그 작업
		public long KeywordSeqNo { get; set; }

		public List<CPPLPSRPItemModel> SmartClickAdList { get; set; }

		public List<SRPSearchCategory> Category { get; set; }

		public List<SRPBrand> BrandFinderList { get; set; }

		public string BrandList { get; set; }
		public string SellCustNo { get; set; }

		public SmartFinder SmartFinder { get; set; }

		public bool IsDetailSearch { get; set; }

		public G9BoxItem G9BoxItem { get; set; }

		public string SmartClickAdUrl { get; set; }
		public string SmartClickCntAsPlus { get; set; }
		
		public int BlockAdStartRank { get; set; }
		public List<string> TrackingUrlList { get; set; }

		public List<SRPSearchCategory> ShopCategory { get; set; }

		public SearchExtension searchEx { get; set; }
		
	}

    public class PromotionBanner
    {
        public string LinkUrl { get; set; }
        public string ImageUrl { get; set; }
    }

	// 샵베스트에도 이 모델 씁니다. 2014.10.06 배영렬
	public class CPPLPSRPItemModel
	{
		public bool IsAdult { get; set; }
		public string GoodsCode { get; set; }
		public string OriginalPrice { get; set; }
		public string SalePrice { get; set; }
		public bool IsDisplayWon { get; set; }
		public string AdCouponPrice { get; set; }
		public decimal SellPrice { get; set; }
		public decimal DiscountPrice { get; set; }		
		public string GoodsName { get; set; }
		public string DiscountRate { get; set; }
		public SRPItemDeliveryInfo Delivery { get; set; }
		public string DiscountBannerText { get; set; }
		public string DiscountBannerImage { get; set; }
		public string ImageURL { get; set; }
		// for admin image || etc image
		public string ImageURL2 { get; set; }
		public List<string> LargeImageList { get; set; }
		public string LinkURL { get; set; }
		public int MoreImageCnt { get; set; }
		public bool IsPlusAD { get; set; }
		public Seller Seller { get; set; }
		public bool IsFavoriteSeller { get; set; }
		public bool IsPurchasedSeller { get; set; }
		public string IsTpl { get; set; }
		public bool SubdivYN { get; set; }
		public string LCode { get; set; }
		public string MCode { get; set; }
		public string SCode { get; set; }
		public bool IsFocus { get; set; }
		public bool AnotherTypeStart { get; set; }
		public bool AnotherTypeEnd { get; set; }
		public ListingItemGroup ListingItemGroup { get; set; }
		public string PdsClickUrl { get; set; }
		public string CartDirectAdgateUrl { get; set; }
		public bool IsPowerClickAD { get; set; }
		public string PowerClickADTitle { get; set; }
		public int OptSellDetailNonusePoint { get; set; }
		public int ShopGroupCode { get; set; }
		public Branch Branch { get; set; } // Homeplus
		public bool IsCartVisible { get; set; }
		public string AddCartApiUrl { get; set; }
		public string DepartmentStoreCssName { get; set; }
		public string DepartmentStoreLandingUrl { get; set; }
		public int TotalSearchResultRank { get; set; }

		public int AcmlContrCnt { get; set; }
		public string BuyCount { get; set; }

		public string BrandName { get; set; }
		public string BrandNameTag
		{
			get
			{
				if(BrandName.Trim() == "기타" || BrandName.Trim() == "기타 (미입력)" || string.IsNullOrEmpty(BrandName.Trim()))
				{
					return "";
				}
				return "[" + BrandName + "]";
			}
		}

		public string PdsLogJson { get; set; }
	}

	public class LPSRPBlockAdModel 
	{
		public List<CPPLPSRPItemModel> Item { get; set; }
		public List<string> TrackingUrlList { get; set; }
	}

	public class Branch
	{
		public int Prmt1Cd { get; set; }
		public string Prmt1CdImageUrl { get; set; }
		public int Prmt2Cd { get; set; }
		public decimal BranchPrice { get; set; }
	}

	public enum ListingItemGroup
	{	
		None = 0,
		Plus = 1,
		Tpl = 2,
		Focus = 3,
		General = 4,
		BizOn = 5,
		PowerClick = 6
	}

	public class Seller
	{
		public string SellCustNo { get; set; }
	}

	public class SmartADT
	{
		public bool AdultYN { get; set; }
		public string GdNo { get; set; }
		public string Name { get; set; }
		public string ClickURL { get; set; }
		public string ImageURL { get; set; }
		public string Price { get; set; }
		public decimal SellPrice { get; set; }
		public string DeliveryInfo { get; set; }
		public decimal DiscountPrice { get; set; }
		public string DiscountRate { get; set; }
	}

	// 기획전 모델
	public class SpecialShopModel
	{
		public int pageNo { get; set; }
		public int pageSize { get; set; }
		public string lcId { get; set; }
		public List<SpecialShopItemModel> Items { get; set; }
		public int pageCount { get; set; }
		public int totalCount { get; set; }
		public string CPPTitle { get; set; }
	}

	public class SpecialShopItemModel
	{
		[Column("TOTAL_COUNTS")]
		public int totalCount { get; set; }				// 전체 갯수

		[Column("PAGE_COUNTS")]
		public int pageCount { get; set; }				// 페이지 수

		[Column("SID")]
		public string sSpecialID { get; set; }				// 기획전 SID

		[Column("TITLE")]
		public string sSpecialTitle { get; set; }			// 기획전 제목

		[Column("GDLC_CD")]
		public string sSpeciallcCd { get; set; }

		[Column("MOBILE_THUMB_IMAGE_URL")]
		public string sSpecialImage { get; set; }			// 추천배너이미지		

		public string LandingURL { get; set; }
	}

	public class SRPSearchCategory
	{
		public int CategoryCount { get; set; }
		public string CategoryId { get; set; }
		public string CategoryNm { get; set; }
		public string CategoryLevel { get; set; }
		public string CategoryType { get; set; }
        public string ReqeustCategoryId { get; set; }
	}

	public class SRPBrand
	{
		public string ChoSung { get; set; }
		public string BrandNo { get; set; }
		public string BrandonSubImg { get; set; }
		public string BrandName { get; set; }
		public int BrandCount { get; set; }
	}

	public class SRPItemDeliveryInfo
	{
		public string DeliveryInfo { get; set; }
		public string DeliveryCode { get; set; }
		public string DeliveryFee { get; set; }
		public string DeliveryText { get; set; }
		public string DeliveryType { get; set; }
		public bool ShowDeliveryInfo { get; set; }
	}

	public class CPPCategoryBest100Model
	{
		public List<SRPSearchCategory> CategoryList { get; set; }
		public List<SearchItemModel> Best100Items { get; set; }
		public string lcId { get; set; }
		public string CPPTitle { get; set; }
	}

	public class CategoryGroupInfo
	{
		public long GroupID { get; set; }
		public string GroupName { get; set; }
		public List<string> LargeCategoryList { get; set; }
	}

	public class BestSellerGroupInfo
	{
		public string GroupCode { get; set; }
		public string GroupName { get; set; }
		public int Priority { get; set; }
	}

	public class SmartFinder
	{
		public bool HasSmartFinder { get; set; }

		public string BannerImageUrl { get; set; }
		public string BannerLandingUrl { get; set; }
		public string PdsLogJson { get; set; }

		public SmartFinderSearchOption SearchOption { get; set; }
	}

	public class SmartFinderSearchOption
	{
		public bool HasSearchOption { get; set; }

		public string ColoredTitle { get; set; }
		public string NormalTitle { get; set; }
		public string ColoredDescription { get; set; }
		public string NormalDescription { get; set; }

		public string OptionName { get; set; }
		public string PdsLogJson { get; set; }
		public List<SmartFinderSearchOptionValue> OptionValues { get; set; }
	}

	public class SmartFinderSearchOptionValue
	{
		public string OptionValueName { get; set; }
		public string ValueId { get; set; }
	}

	public class G9BoxItem
	{
		public bool HasG9BoxItem { get; set; }
		public string ImageUrl { get; set; }
		public string LandingUrl { get; set; }
		public string LandingPdsLogJson { get; set; }
		public string TitleLandingUrl { get; set; }
		public string TitlePdsLogJson { get; set; }
		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }
		public string BrandName { get; set; }
		public bool IsShowBrandName { get; set; }
		public int DiscountRate { get; set; }
		public string OriginalPrice { get; set; }
		public string SalePrice { get; set; }
		public bool IsFreeDelivery { get; set; }
		public string SoldQty { get; set; }
		public bool IsDisplayRemainTime { get; set; }
		public string OpenTime { get; set; }
		public string CloseTime { get; set; }
		public string OpenTimeText { get; set; }
		public bool IsShowBuyCount { get; set; }
		public string PromotionTagUrl { get; set; }
		public string PromotionTagName { get; set; }
		public bool IsShowPromotionTag { get; set; }
	}

	public class GetAddCartResponseT
	{
		public int CartCount { get; set; }
		public string CartPid { get; set; }
	}

	public class GetAdvPowerClickItemRequestT
	{
		public string menuName { get; set; }
		public string keyword { get; set; }
		public string lcId { get; set; }
		public string mcId { get; set; }
		public string scId { get; set; }
		public long keywordSeq { get; set; }
		public int startRank { get; set; }
		public bool isDetailSearch { get; set; }
		public List<string> categories { get; set; }
		public List<string> brands { get; set; }

		// add filter
		public string isBrand { get; set; }
		public string isDiscount { get; set; }
		public string isShippingFree { get; set; }
		public string isMileage { get; set; }
		public string isSmartDelivery { get; set; }
		public string sellCustNo { get; set; }
		public int minPrice { get; set; }
		public int maxPrice { get; set; }

		public GetAdvPowerClickItemRequestT()
		{
			this.menuName = "SRP";
			this.keyword = "";
			this.lcId = "";
			this.mcId = "";
			this.scId = "";
			this.keywordSeq = 0;
			this.startRank = 1;
			this.isDetailSearch = false;
			this.categories = new List<string>();
			this.brands = new List<string>();
			this.isBrand = "N";
			this.isDiscount = "N";
			this.isShippingFree = "N";
			this.isMileage = "N";
			this.isSmartDelivery = "N";
			this.sellCustNo = "";
			this.minPrice = 0;
			this.maxPrice = 0;
		}
	}
}

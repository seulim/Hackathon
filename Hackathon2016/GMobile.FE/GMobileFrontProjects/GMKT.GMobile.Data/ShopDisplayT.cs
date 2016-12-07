using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public class ShopT
	{
		public SellerT Seller { get; set; }
		public List<ImageT> MainImg { get; set; }
		public FavoriteShopT Favorite { get; set; }

		[Column("NICKNAME")]
		public string Title { get; set; }

		[Column("HELPDESK_TEL_NO")]
		public string HelpdeskTelNo { get; set; }

		[Column("BASIC_DOMAIN")]
		public string Alias { get; set; }

		[Column("PERSONAL_DOMAIN")]
		public string PersonalDomain { get; set; }

		public string SellerId
		{
			get
			{
				return this.CustomerNo;
			}
			set
			{
				this.CustomerNo = value;
			}
		}

		[Column("SELLER_CUST_NO")]
		public string CustomerNo { get; set; }

		[Column("SHOP_LEVEL")]
		public string ShopLevel { get; set; }

		public bool IsPlus
		{
			get
			{
				return this.ShopLevel == GMKT.GMobile.Data.ShopLevel.Plus &&
					(this.StartDatePlusService <= DateTime.Now) &&
					(DateTime.Now <= this.EndDatePlusService);
			}
		}

		[Column("PLUS_START_DATE")]
		public DateTime StartDatePlusService { get; set; }

		[Column("PLUS_END_DATE")]
		public DateTime EndDatePlusService { get; set; }

		[Column("SHOP_INTRO_CONTENT")]
		public string Introduction { get; set; }

		//[Column("SHOP_LOGO_TYPE")]
		//public LogoType LogoType { get; set; }

		//[Column("SHOP_LOGO_IMAGE_PATH")]
		//public string LogoImagePath { get; set; }

		//[Column("SHOP_LOGO_IMAGE_BLOCK_YN")]
		//public string IsDisplayLogoImageValue { get; set; }
		//public bool IsDisplayLogoImage
		//{
		//    get
		//    {
		//        return !String.IsNullOrEmpty(this.IsDisplayLogoImageValue) &&
		//            this.IsDisplayLogoImageValue.Equals("N") ? true : false;
		//    }
		//}

		//[Column("POPULAR_KEYWORD")]
		//public string PopularKeyword { get; set; }

		//[Column("SEARCH_KEYWORD")]
		//public string SearchKeyword { get; set; }

		//[Column("SEARCH_KEYWORD_LNK_TYPE")]
		//public KeywordLinkType SearchKeywordLinkType { get; set; }

		//[Column("SEARCH_KEYWORD_LNK_URL")]
		//public string SearchKeywordLinkUrl { get; set; }

		//[Column("SEARCH_KEYWORD_LNK_CONTENT")]
		//public string SearchKeywordLinkContent { get; set; }

        [Column("CATEGORY_USE_TYPE")]
        public CategoryDisplay CategoryDisplay { get; set; }

        [Column("GMKT_CATEGORY_DISP_TYPE")]
        public GeneralCategoryDisplayType GeneralCategoryDisplayType { get; set; }

        [Column("SHOP_CATEGORY_DISP_TYPE")]
        public ShopCategoryDisplayType ShopCategoryDisplayType { get; set; }

		[Column("cert_type")]
		public string ShopType { get; set; }

		//[Column("GOODS_CNT_EXPOSE_YN")]
		//public string IsDisplayItemCountValue { get; set; }
		//public bool IsDisplayItemCount
		//{
		//    get
		//    {
		//        if (!String.IsNullOrEmpty(this.IsDisplayItemCountValue) &&
		//            this.IsDisplayItemCountValue.Equals("Y"))
		//            return true;
		//        else
		//            return false;
		//    }
		//}

		// Customer Support related
		[Column("FAX_NO")]
		public string FaxNo { get; set; }

		[Column("INFO_EM")]
		public string EmailAddress { get; set; }

		[Column("WD_CNSL_TIME_CONTENT")]
		public string WeekdayTime { get; set; }

		[Column("SAT_CNSL_TIME_CONTENT")]
		public string SaturdayTime { get; set; }

		[Column("HOLIDAY_CNSL_TIME_CONTENT")]
		public string HolidayTime { get; set; }


		//public static ShopT GetInstance(string alias)
		//{
		//    ShopBiz biz = new ShopBiz();
		//    ShopT shop = biz.GetShop(alias);
		//    return shop;
		//}
	}

	public class ShopDetailT
	{
		//[Column("CUST_NO")]
		//public string SellerId { get; set; }

		[Column("SHOP_DETAIL_INTRO_CONTENT")]
		public string Introduction { get; set; }

		[Column("BLOCK_YN")]
		public string IsBlockValue { get; set; }
		public bool IsBlock
		{
			get
			{
				return !String.IsNullOrEmpty(this.IsBlockValue) &&
					this.IsBlockValue.Equals("Y") ? true : false;
			}
		}
	}

	public class MobileShopInfoT
	{
		public MobileBasicInfoT ShopInfo { get; set; }
		public List<MobileDisplayInfoT> DisplayInfo { get; set; }
		public Dictionary<int, List<string>> SellerOrder { get; set; }
		public List<MobileImageT> MainImages { get; set; }
	}

	public class MobileBasicInfoT
	{
		[Column("SHOP_USE_YN")]
		public string ShopUseYN { get; set; }

		[Column("DESIGN_COLOR_TYPE")]
		public short ColorType { get; set; }

		[Column("SHOP_LEVEL")]
		public string ShopLevel { get; set; }
	}

	public class MobileDisplayInfoT
	{
		[Column("AREA_USE_YN")]
		public string AreaUseYN { get; set; }

		[Column("DISP_TYPE")]
		public short DisplayType { get; set; }

		[Column("SORT_TYPE")]
		public short SortType { get; set; }
		public short Sort
		{
			get
			{
				switch (this.SortType)
				{
					case 1: 
						return (short)DisplayOrder.SellPointDesc;
					case 2: 
						return (short)DisplayOrder.New;
					case 3: 
						return (short)DisplayOrder.ConPointDesc;
					case 4: 
						return (short)DisplayOrder.DiscountPriceDesc;
					default: 
						return (short)DisplayOrder.SellPointDesc;
				}
			}
		}

		[Column("TITLE")]
		public string Title { get; set; }

		[Column("TITLE_EXPOSE_TYPE")]
		public string TitleDisplayType { get; set; }

		[Column("TITLE_IMAGE_PATH")]
		public string TitleImagePath { get; set; }

		[Column("AREA_NO")]
		public long AreaNo { get; set; }

		public List<string> SellerOrder { get; set; }
	}

	public class MobileImageT
	{

		[Column("IMAGE_EXPOSE_ORDERBY")]
		public int DisplayOrder { get; set; }

		[Column("IMAGE_LNK_URL")]
		public string ImgLinkURL { get; set; }

		[Column("IMAGE_PATH")]
		public string ImgPath { get; set; }

		[Column("STATUS")]
		public char Status { get; set; }
	}

	public class OrderSumT
	{
		[Column("TOTAL_COUNT")]
		public int TotalCount { get; set; }
		public string TotalCountText
		{
			get
			{
				return TotalCount.ToString("N0");
			}
		}

		[Column("TOTAL_MONEY")]
		public decimal TotalAmount { get; set; }
		public string TotalAmountText
		{
			get
			{
				return TotalAmount.ToString("N0");
			}
		}
	}

	public class OrderHistoryT
	{
		public OrderSumT OrderSum { get; set; }

		[Column("BASKET_COUNT")]
		public int CartItemsCount { get; set; }
		public string CartItemsCountText
		{
			get
			{
				return CartItemsCount.ToString("N0");
			}
		}

		[Column("INTEREST_COUNT")]
		public int InterestCount { get; set; }
		public string InterestCountText
		{
			get
			{
				return InterestCount.ToString("N0");
			}
		}
	}

	public class FavoriteShopT
	{
		[Column("SELLER_CUST_NO")]
		public string SellerId { get; set; }

		[Column("CUST_NO")]
		public string BuyerId { get; set; }

		[Column("EFT_DT")]
		public DateTime EffectiveDate { get; set; }

		[Column("NICKNAME")]
		public string Nickname { get; set; }

		public bool IsFavoriteShop
		{
			get
			{
				if (!String.IsNullOrEmpty(this.SellerId) &&
					!String.IsNullOrEmpty(this.BuyerId))
					return true;
				else
					return false;
			}
		}
	}

	public class ImageT
	{
		public string imgURL { get; set; }
        public string imgLinkURL { get; set; }
	}

	/// <summary>
	/// 샵 레벨
	/// </summary>
	public struct ShopLevel
	{
		public const string Basic = "B";
		public const string Plus = "P";
	}

	/// <summary>
	/// 제목 노출 유형
	/// </summary>
	public struct TitleDisplayType
	{
		public const string Text = "T";
		public const string Image = "I";
	}

	/// <summary>
	/// 디자인 컬러 유형
	/// </summary>
	public enum DesignColorType
	{
		None = 0,
		Red = 1, //빨강
		Orange = 2, //주황
		Green = 3, // 녹색
		YellowGreen = 4, //연두
		SkyBlue = 5, //하늘색
		Blue = 6, //파랑
		Purple = 7, //보라
		Wine = 8, // 자주
		Pink = 9, // 분홍
		Black = 10 // 검정
	}

	/// <summary>
	/// 메인 상품 전시 유형
	/// </summary>
	public enum ItemDisplayType
	{
		None = 0,
		Default = 1, //기본형
		Mosaic = 2, //모자이크형
		Horizontal = 3, //가로형
		Vertical = 4, //세로형
		BigImage = 5 //큰이미지형
	}

	/// <summary>
	/// 상품 정렬 방법
	/// </summary>
	public enum ItemSortType
	{
		None = 0,
		MostPopular = 1, //판매건수 많은순
		MostClickCount = 2, //조회건수
		MostReview = 3, //상품평 많은 순
		HighestDiscount = 4, //할인가 높은 순
		Manual = 5 //직접 선택하기
	}
}

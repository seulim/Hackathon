using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GMKT.GMobile.Data
{
	#region 마트 V1
	public class MartView
	{
		public string GroupName { get; set; }
		public string SubGroupName { get; set; }
		public List<MartCategory> CategoryList { get; set; }
		public List<MartItem> Items { get; set; }
	}	

	public class MartCategory : CategoryInfo
	{
		public string IconOnURL { get; set; }
		public string IconOffURL { get; set; }
		public string AppIconOnURL { get; set; }
		public string AppIconOffURL { get; set; }
		public string CssName { get; set; }

		public List<MartCategory> ChildCategoryList { get; set; }
	}

	public class MartItem : SuperDealItem
	{
		public string ShopGdlcCd { get; set; }
		public string ShopGdmcCd { get; set; }
		public string ShopGdscCd { get; set; }

		public string MMGoodsName { get; set; } // 마트 전용 상품명
		public string BannerImageUrl { get; set; } // 마트 이미지
		public string Title { get; set; } // 마트 sub title ==> PRD 변경으로 사용 안함
		public string UnitPriceContent { get; set; } // 마트 객단가 ==> PRD 변경으로 사용 안함
		public string GoodsName { get; set; } // 실제 상품명

		// 배송 관련 정보, 
		public string DeliveryFee { get; set; }
		public string CommonDeliveryInfo { get; set; }
		public string DeliveryCode { get; set; }
	}
	#endregion

	#region 마트 V2
	public class MartV2View
	{
		public MartTimeDealT TimeDeal { get; set; }

		public List<MartV2Category> CategoryList { get; set; }
		
		//public List<MartV2Category> SubCategoryList { get; set; }
		
		public List<MartV2ItemGroupT> ItemGroupList { get; set; }
		public List<MartV2BammerTab> BannerList { get; set; }
	}

	public class MartV2Category
	{
		public string CategoryCode { get; set; }

		public bool IsSelected { get; set; }

		public string OnIconUrl { get; set; }
		public string OffIconUrl { get; set; }

		public string OnText { get; set; }
		public string OnTextColor { get; set; }
		public string OffText { get; set; }
		public string OffTextColor { get; set; }

		public List<MartV2Category> SubCategoryList { get; set; }

		public string ApiUrl { get; set; }
	}

	public class MartV2BammerTab
	{
		public string LandingUrl { get; set; }
		public string Title { get; set; }
		public string ImageUrl { get; set; }
	}

	public class MartV2ItemGroupT
	{		
		[JsonConverter(typeof(StringEnumConverter))]
		public MartV2ItemGroupType Type { get; set; }

		public List<MartV2ItemT> ItemList { get; set; }

		public string Title { get; set; }
		public string ImageUrl { get; set; }

		public string LandingUrl { get; set; }
		public string ApiUrl { get; set; }
	}
		
	public enum MartV2ItemGroupType
	{
		X1,
		X2,
		X3,

		A,
		B,

		C1,
		C2,

		S,

		Z
	}

	public class MartV2ItemT
	{
		public string GoodsCode { get; set; }

		public string Title { get; set; }

		public string ImageUrl { get; set; }

		public string OriginalPrice { get; set; }
		public string DiscountPrice { get; set; }
		public string DiscountRate { get; set; }

		public bool IsSoldOut { get; set; }

		public string DeliveryType { get; set; }
		public string DeliveryText { get; set; }

		public string LandingUrl { get; set; }
		public string ApiUrl { get; set; }
		public string LandingMartCategory { get; set; }
	}

	public class MartTimeDealT
	{
		public bool HasTimeDeal { get; set; }		
		public bool NeverDisplayAgain { get; set; }

		public string Id { get; set; }
		public string EndDate { get; set; }

		public string ImageUrl { get; set; }

		public bool IsSoldOut { get; set; }

		public string Title { get; set; }

		public string OriginalPrice { get; set; }
		public string DiscountPrice { get; set; }
		public string DiscountRate { get; set; }

		public string LandingUrl { get; set; }
	}
	#endregion

	#region 마트 V3
	public class MartV3View : MartV2View
	{
		public Boolean IsMain { get; set; }
		public List<MartBrand> BrandList { get; set; }
		public List<MartContents> ContentsList { get; set; }
		public new List<MartV3Category> CategoryList { get; set; }
        public List<MartV3Category> MartCategoryList { get; set; }

		public string IsMainStr 
		{
			get { return this.IsMain ? "true" : "false"; }
		}
	}

	public class MartBrand
	{
        public long Seq { get; set; }
        public string BrandNm { get; set; }
        public string BrandImageUrl { get; set; }
        public string Priority { get; set; }
        public string UseYn { get; set; }
        public string LandingUrl { get; set; }
        public string LnkType { get; set; }
		public string SellerId { get; set; }
	}

	public class MartContentsView
	{
		public MartContents Contents { get; set; }
		public List<MartContents> ContentsList { get; set; }
		public List<MartV2ItemT> ItemList { get; set; }
	}

	public class MartContents
	{	
		public long ContentsSeq { get; set; }
		public string Title { get; set; }
		public string Contents { get; set; }
		public string BannerImage { get; set; }
		public string UseYn { get; set; }
		public string ExposeYn { get; set; }
		public string LandingUrl { get; set; }
	}

	public class MartV3Category
	{
		public int Seq { get; set; }
		public int MartGdlcCd { get; set; }
		public int MartGdmcCd { get; set; }
		public string ImageUrl { get; set; }
		public int Priority { get; set; }
		public string MartCategoryNm { get; set; }
		public bool IsSelected { get; set; }
		public string ApiUrl { get; set; }
	}
	#endregion
}

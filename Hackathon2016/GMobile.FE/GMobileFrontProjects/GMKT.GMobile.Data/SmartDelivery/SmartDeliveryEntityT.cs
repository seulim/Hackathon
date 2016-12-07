using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.SmartDelivery
{
	public class SmartDeliverCatetoryModel
	{
		public string CategoryNM { get; set; }
		public long SmshopSeq { get; set; }
		public string CategoryImageUrl { get; set; }
		public List<SmartDeliveryGDLCModel> SuperDealItems { get; set; }
	}

	public class SmartDeliveryGDLCModel
	{
		public string GdlcCd { get; set; }
		public string GdlcNm { get; set; }
		public string GroupCd { get; set; }
	}

	public class SmartDeliveryBannerModel
	{
		public string BannerType { get; set; }
		public List<SmartDeliveryBannerT> BannerList { get; set; }
	}
	
	public class SmartDeliveryBannerT
	{
		public long BannerSeq { get; set; }
		public string BannerNm { get; set; }
		public long SmshopSeq { get; set; }
		public int Priority { get; set; }
		public string UseYn { get; set; }
		public string ImagePath { get; set; }
		public string LnkPath { get; set; }
		public DateTime DispSdt { get; set; }
		public DateTime DispEdt { get; set; }
		public string BannerNm1 { get; set; }
		public string BannerNm2 { get; set; }
		public string ImagePath1 { get; set; }
		public string ImagePath2 { get; set; }
		public int CdPriority { get; set; }
		public string MobileDispYn { get; set; }
		public string MobileBannerImg { get; set; }
		public string MobileDispLinkUrl { get; set; }
		public string MobileImagePath { get; set; }
		public string MobileImagePath1 { get; set; }
		public string MobileImagePath2 { get; set; }
	}
	public class SmartDeliveryDisplayModel
	{
		public string DisplayType { get; set; }
		public string TabTitle { get; set; }
		public List<SearchItemModel> GoodsList { get; set; }
	}

	public class SmartDeliveryBestT
	{
		public List<SmartDeliveryBestItemT> Items { get; set; }
		public List<SmartDeliveryBestItemT> SmallPackingItems { get; set; }
	}

	public class SmartDeliveryBestItemT
	{
		public string GoodsName { get; set; }
		public string GoodsCode { get; set; }

		public string BrandName { get; set; }
		public string ImageURL { get; set; }
		public string LinkURL { get; set; }

		public string OriginalPrice { get; set; }
		public string DiscountPrice { get; set; }
		public string SalePrice { get; set; }

		public bool SubdivYN { get; set; }
		public string DeliveryFee { get; set; }
	}

	public class SmartDeliverySearchRequest
	{
		// 차후 기타 페이지에서 사용할 것을 고려해 Required 제외
		public string primeKeyword { get; set; } // 주 검색어			
		public string menuName { get; set; } // SRP, LP...
		public int pageSize { get; set; }
		public int pageNo { get; set; }
		public string moreKeyword { get; set; } // 상세 검색어
		public string lcId { get; set; } // 주 카테고리 
		public string mcId { get; set; }
		public string scId { get; set; }
		public string sortType { get; set; } // (보이저) 정렬 
		public string isSmartDelivery { get; set; } // 스마트배송
		public string isSmallPacking { get; set; } // 스마트배송관 소량포장상품 전용
		public string sellCustNo { get; set; } // for PP

		public int minPrice { get; set; }
		public int maxPrice { get; set; }

		public int listingItemGroup { get; set; } // 페이징 하면서 필요한 파라미터.
		
		public string brandList { get; set; }  // 브랜드 리스트 delimiter 는 ,
				
		public SmartDeliverySearchRequest()
		{			
			this.menuName = "SRP";
			this.pageSize = 30;
			this.pageNo = 1;
			this.primeKeyword = "";
			this.moreKeyword = "";
			this.lcId = "";
			this.mcId = "";
			this.scId = "";
			this.sortType = "";			
			this.isSmartDelivery = "N";
			this.isSmallPacking = "N";
			this.sellCustNo = "";
			this.brandList = "";						
		}
	}
}

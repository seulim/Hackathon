using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class ECouponEvent
	{
		[Column("event_seq")]
		public long EventSeq { get; set; }

		[Column("event_nm")]
		public string EventName { get; set; }

		[Column("gd_url")]
		public string LinkUrl { get; set; }

		[Column("banner_image_url")]
		public string BannerImageUrl { get; set; }

		[Column("use_yn")]
		public string UseYn { get; set; }

		[Column("priority")]
		public int Priority { get; set; }
	}

	public partial class ECouponCategory
	{
		[Column("category_cd")]
		public string CategoryCode { get; set; }

		[Column("category_nm")]
		public string CategoryName { get; set; }

		[Column("image_url")]
		public string ImageUrl { get; set; }

		[Column("gdlc_cd")]
		public string GdlcCd { get; set; }

		[Column("gdmc_cd")]
		public string GdmcCd { get; set; }

		[Column("gdsc_cd")]
		public string GdscCd { get; set; }

		[Column("use_yn")]
		public string UseYn { get; set; }

		[Column("priority")]
		public int Priority { get; set; }

		[Column("brand_count")]
		public int BrandCount { get; set; }

		public string IconCss { get; set; }
	}
	
	public class ECouponBestCategory : ECouponCategory
	{
		public List<ECouponItem> Items { get; set; }
	}

	public partial class ECouponBrand
	{
		[Column("brand_cd")]
		public int BrandCode { get; set; }

		[Column("brand_nm")]
		public string BrandName { get; set; }

		[Column("category_cd")]
		public string CategoryCode { get; set; }

		[Column("logo_image_url")]
		public string LogoImageUrl { get; set; }

		[Column("use_yn")]
		public string UseYn { get; set; }

		[Column("priority")]
		public int Priority { get; set; }

		public string LinkURL { get; set; }
	}

	public partial class ECouponBrandMenu
	{
		[Column("menu_seq")]
		public long MenuSeq { get; set; }

		[Column("menu_nm")]
		public string MenuName { get; set; }

		[Column("brand_cd")]
		public int BrandCode { get; set; }

		[Column("image_url")]
		public string ImageUrl { get; set; }

		[Column("use_yn")]
		public string UseYn { get; set; }

		[Column("priority")]
		public int Priority { get; set; }

		[Column("brand_nm")]
		public string BrandName { get; set; }

		[Column("logo_image_url")]
		public string LogoImageUrl { get; set; }

		[Column("total_count")]
		public int TotalCount { get; set; }

		[Column("page_index")]
		public int PageIndex { get; set; }

		[Column("page_size")]
		public int PageSize { get; set; }
	}

	public partial class ECouponItemByMenu
	{
		[Column("long")]
		public long Seq { get; set; }

		[Column("category_cd")]
		public string CategoryCode { get; set; }

		[Column("brand_cd")]
		public int BrandCode { get; set; }

		[Column("menu_seq")]
		public long MenuSeq { get; set; }

		[Column("gd_no")]
		public string ItemNo { get; set; }

		[Column("use_yn")]
		public string UseYn { get; set; }

		[Column("ahead_expose_yn")]
		public string AheadExposeYn { get; set; }

		[Column("cashability_yn")]
		public string CashabilityYn { get; set; }
	}

	public class ECouponItemTop1
	{
		public string CategoryCode { get; set; }
		public int BrandCode { get; set; }
		//public string BrandName { get; set; }
		public long MenuSeq { get; set; }
		//public string MenuName { get; set; }
		public string ItemNo { get; set; }
		public decimal DiscountPrice { get; set; }
		public decimal DealPrice { get; set; }
		public decimal SellPrice { get; set; }
		public int DiscountRate { get; set; }
		public bool HasOption { get; set; }
		public string VipUrl { get; set; }
		public string BasketUrl { get; set; }
		public string OrderUrl { get; set; }

		//todo:캐시 확인
		public string CacheTime { get; set; }
	}

	
	public class ECouponMenuItem
	{
		public int BrandCode { get; set; }
		public string BrandName { get; set; }
		public string BrandImageUrl { get; set; }
		public long MenuSeq { get; set; }
		public string MenuName { get; set; }
		public string MenuImageUrl { get; set; }
		public string MenuLoadingImageUrl { get; set; }
		public int MenuPriority { get; set; }
		public int TotalCount { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }

		public string ItemNo { get; set; }
		public decimal DiscountPrice { get; set; }
		public decimal DealPrice { get; set; }
		public decimal SellPrice { get; set; }
		public bool HasOption { get; set; }
		public string VipUrl { get; set; }
		public string BasketUrl { get; set; }
		public string OrderUrl { get; set; }
	}
}

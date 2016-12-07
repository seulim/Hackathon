using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GMKT.GMobile.Data
{
	#region 백화점 V1
	public enum DepartmentStoreItemType
	{
		Small,
		Large
	}

	public enum DepartmentStoreMainGroupType
	{
		Banner,
		Brand,
		Category,
		Best,
		Shortcut,
		ShopCategory,
		Item,
		BottomShopCategory
	}

	public enum DepartmentStoreMainFcdType
	{
		Search,
		MainBanner,
		Brand,
		Category,
		Best,
		Shortcut
	}

	public class DepartmentStoreSearchT
	{
		public string SearchUrl { get; set; }
		public string SearchHelpText { get; set; }
		public string Fcd { get; set; }
	}

	public class DepartmentStoreBannerSectionT
	{
		// search
		public DepartmentStoreSearchT Search { get; set; }
		// banner
		public List<DepartmentStoreBannerT> MainBanner { get; set; }
		public List<DepartmentStoreBannerT> AllBanner { get; set; }
	}

	public class DepartmentStoreMainGroupT
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public DepartmentStoreMainGroupType Type { get; set; }

		public string Fcd { get; set; }		

		// Banner
		public DepartmentStoreBannerSectionT Banner { get; set; }

		// brand
		public List<DepartmentStoreBrandT> Brand { get; set; }

		// category
		public List<DepartmentStoreGroupCategoryT> Category { get; set; }

		// best
		public DepartmentStoreBestT Best { get; set; }

		// 바로가기
		public DepartmentStoreShortcutT Shortcut { get; set; }

		// category
		public List<DepartmentStoreCategoryT> ShopCategory { get; set; }

		// superdeal item
		public List<HomeMainItem> Item { get; set; }

		// bottom category
		public List<DepartmentStoreCategoryT> BottomShopCategory { get; set; }

	}

	public class DepartmentStoreGroupCategoryT
	{
		public DepartmentStoreGroupCategoryT()
		{
			this.LCategoryList = new List<CategoryInfo>();
		}

		public string CategoryGroupCode { get; set; }
		public int DisplayAreaNo { get; set; }
		public string CategoryGroupName { get; set; }
		public List<CategoryInfo> LCategoryList { get; set; }
		public string IconImageUrl { get; set; }
		public string LandingUrl { get; set; }
	}

	public class DepartmentStoreBannerT
	{
		public long Seq { get; set; }
		public int ExposeRank { get; set; }
		public string BannerTitle { get; set; }
		public string BannerImageUrl { get; set; }
		public string LandingUrl { get; set; }
		public string MainExposeYN { get; set; }
		public DateTime DisplayStartDate { get; set; }
		public DateTime DisplayEndDate { get; set; }
		public string PdsLogJson { get; set; }
	}

	public class DepartmentStoreBrandT
	{
		public DepartmentStoreBrandT()
		{
			this.Item = new List<DepartmentStoreBrandItemT>();
		}

		public long Seq { get; set; }
		public string BrandTitle { get; set; }
		public DateTime DisplayStartDate { get; set; }
		public DateTime DisplayEndDate { get; set; }

		public List<DepartmentStoreBrandItemT> Item { get; set; }

	}

	public class DepartmentStoreBrandItemT
	{
		public string ImageUrl { get; set; }
		public string LandingUrl { get; set; }
		public string Price { get; set; }
		public bool IsSoldOut { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public DepartmentStoreItemType Type { get; set; }
	}

	public class DepartmentStoreBestT
	{
		public DepartmentStoreBestT()
		{
			this.Time = new DepartmentStoreBestTimeT();
			this.Item = new List<DepartmentStoreBestItemT>();
		}

		public DepartmentStoreBestTimeT Time { get; set; }
		public List<DepartmentStoreBestItemT> Item { get; set; }
	}

	public class DepartmentStoreBestTimeT
	{
		public string Year { get; set; }
		public string Month { get; set; }
		public string Day { get; set; }
		public string AmPm { get; set; }
		public string Hour { get; set; }
		public string Minute { get; set; }
	}

	public class DepartmentStoreBestItemT
	{
		public string ImageUrl { get; set; }
		public string DepartmentStoreImageUrl { get; set; }
		public string DepartmentStoreCssName { get; set; }
		public string DepartmentStoreLandingUrl { get; set; }
		public string DepartmentStoreName { get; set; }

		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }
		public string LinkURL { get; set; }
		public string DiscountPrice { get; set; }
		public string Price { get; set; }
		public long Priority { get; set; }

		public string GstampCnt { get; set; }

		public bool AdultYn { get; set; }

		public string DeliveryFee { get; set; }
		public string DeliveryInfo { get; set; }
		public string DeliveryCode { get; set; }
		public string IsTpl { get; set; }

		public string BrandName { get; set; }
		public string SellCustNo { get; set; }
		
		public string BrandNameTag
		{
			get
			{
				if(BrandName.Trim() == "기타" || BrandName.Trim() == "기타 (미입력)")
				{
					return "";
				}
				return "[" + BrandName + "]";
			}
		}
	}

	public class DepartmentStoreShortcutT
	{
		public DepartmentStoreShortcutT()
		{
			this.Item = new List<DepartmentStoreShortcutItemT>();
		}

		public string Title { get; set; }
		public List<DepartmentStoreShortcutItemT> Item { get; set; }
	}

	public class DepartmentStoreShortcutItemT
	{
		public string LandingUrl { get; set; }
		public string ImageUrl { get; set; }
		public string Name { get; set; }
		public string CssName { get; set; }
	}

	public class DateFormat
	{
		public string Year { get; set; }
		public string Month { get; set; }
		public string Day { get; set; }
		public string Hour { get; set; }
		public string Minute { get; set; }
		public string PmAm { get; set; }
	}

	public class DepartmentStoreCategoryT
	{
		public string CategoryName { get; set; }

		public int CategoryNo { get; set; }

		public string LandingUrl { get; set; }

		public string PdsLogJson { get; set; }
	}

	public class DepartmentStoreNowT
	{
		// category
		public List<DepartmentStoreCategoryT> ShopCategory { get; set; }

		// superdeal item
		public List<HomeMainItem> Item { get; set; }

	}
}
	#endregion
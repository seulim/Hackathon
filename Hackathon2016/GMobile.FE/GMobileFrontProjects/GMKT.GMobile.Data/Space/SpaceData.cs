using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GMKT.GMobile.Data
{
	public class SpaceInfo
	{
		public SpaceContentsSection ContentsSection { get; set; }
		public SpaceSection SpaceSection { get; set; }
	}

	public class SpaceContentsSection
	{
		public string ContentsAllUrl { get; set; }
		public List<SpaceContents> ContentsList { get; set; }
	}

	public class SpaceContents
	{
		public long ContentsSeq { get; set; }
		public string Title { get; set; }
		public string GroupName { get; set; }
		public string MainBannerImageUrl { get; set; }
		public string BannerImageUrl { get; set; }
		public string ConnectUrl { get; set; }
	}

	public class SpaceContentsDetail : SpaceContents
	{
		public string Html { get; set; }
		public List<SpaceContentsItem> Items { get; set; }
	}

	public class SpaceContentsItem
	{
		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }

		public string ImageUrl { get; set; }

		public int DiscountRate { get; set; }

		public int SellPrice { get; set; }
		public int AdCouponPrice { get; set; }

		public string Shipping { get; set; }
	}

	public class SpaceSection
	{
		public List<SpaceGroup> GroupList { get; set; }
	}

	public class SpaceGroup
	{
		public long LargeGroupNo { get; set; }
		public string LargeGroupName { get; set; }
		public List<SpaceMiddleGroup> MiddleGroupList { get; set; }
		public string ApiUrl { get; set; }
	}

	public class SpaceMiddleGroup
	{
		public long MiddleGroupNo { get; set; }
		public string MiddleGroupName { get; set; }
		public string ApiUrl { get; set; }
	}

	public class SpaceSectionItem
	{
		public List<SpaceItemGroup> ItemGroupList { get; set; }
		public SpacePaging Paging { get; set; }
	}

	public class SpaceItemGroup
	{
		public long Seq { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public SpaceItemGroupType Type { get; set; }

		public List<SpaceItem> ItemList { get; set; }
	}

	public enum SpaceItemGroupType
	{
		A1,
		A2,
		B1,
		B2,
		C
	}

	public class SpaceItem
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public SpaceItemType Type { get; set; }

		public string Price { get; set; }

		public string ImageUrl { get; set; }

		public string LandingUrl { get; set; }
	}

	public enum SpaceItemType
	{
		SmallBanner,
		LargeBanner,
		Square,
		LargeRectangle
	}

	public class SpacePaging
	{
		public int CurrentPageNo { get; set; }

		public string FirstUrl { get; set; }

		public bool HasNext { get; set; }
		public string NextUrl { get; set; }
	}

	public class SpaceBrandSectionItem
	{
		public List<SpaceBrandGroup> BrandGroupList { get; set; }
	}

	public class SpaceBrandGroup
	{
		public long Seq { get; set; }
		public char CouponImageUseYN { get; set; }
		public string BrandName { get; set; }
		public string ConnectUrl { get; set; }
		public string BrandLogoImageUrl { get; set; }
		public string BrandBannerUrl { get; set; }
		public string BrandDescription { get; set; }
		public List<SpaceBrandItem> ItemList { get; set; }
	}

	public class SpaceBrandItem
	{
		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }
		public string Price { get; set; }

		public string ImageUrl { get; set; }

		public string LandingUrl { get; set; }
	}

	public class SpaceBrandGroupDetail : SpaceBrandGroup
	{
		public string ConnectUrl { get; set; }
		public string Html { get; set; }
		public List<SpaceBrandGroupItem> Items { get; set; }
	}

	public class SpaceBrandGroupItem
	{
		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }
		public string DefaultGoodsImageUrl { get; set; }
		public string LandingUrl { get; set; }
		public string ImageUrl { get; set; }

		public int DiscountRate { get; set; }

		public int SellPrice { get; set; }
		public int AdCouponPrice { get; set; }

		public string Shipping { get; set; }
	}
}

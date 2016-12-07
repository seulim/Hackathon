using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GMKT.GMobile.Data
{
	public class LookInfo
	{
		public LookContentsSection ContentsSection { get; set; }
		public LookSection LookSection { get; set; }
	}

	public class LookContentsSection
	{
		public List<LookContents> ContentsList { get; set; }
	}

	public class LookContents
	{
		public long ContentsSeq { get; set; }
		public string Title { get; set; }
		public string GroupName { get; set; }
		public string BannerImageUrl { get; set; }
		public string LandingUrl { get; set; }
	}

	public class LookSection
	{
		public List<LookGroup> GroupList { get; set; }
	}

	public class LookGroup
	{
		public long GroupNo { get; set; }
		public string GroupName { get; set; }

		public List<LookCategory> CategoryList { get; set; }
	}

	public class LookCategory
	{
		public string Name { get; set; }

		public string CategoryCode { get; set; }
	}

	public class LookContentsDetail : LookContents
	{
		public string Html { get; set; }

		public List<LookContentsItem> Items { get; set; }
	}

	public class LookContentsItem
	{
		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }

		public string ImageUrl { get; set; }

		public int DiscountRate { get; set; }

		public int SellPrice { get; set; }
		public int AdCouponPrice { get; set; }

		public string Shipping { get; set; }
	}

	public class LookSectionItem
	{
		public List<LookItemGroup> ItemGroupList { get; set; }
		public LookPaging Paging { get; set; }
	}

	public class LookItemGroup
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public LookItemGroupType Type { get; set; }

		public List<LookItem> ItemList { get; set; }
	}

	public enum LookItemGroupType
	{
		A,
		A2,
		A3,
		B1,
		B2,
		C,
		D
	}

	public class LookItem
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public LookItemType Type { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public LookItemLogicType LogicType { get; set; }

		public bool IsShopIconVisible { get; set; }
		public string Message { get; set; }

		public string ImageUrl { get; set; }

		public string LandingUrl { get; set; }
	}

	public enum LookItemType
	{
		Banner,
		MediumBanner,
		LargeBanner,

		LargeSquare,
		MediumSquare,
		SmallSquare,

		LargeRectangle,
		MediumRectangle
	}

	public enum LookItemLogicType
	{
		Auto,
		Manual
	}

	public class LookPaging
	{
		public int CurrentPageNo { get; set; }

		public bool HasNext { get; set; }
	}
}

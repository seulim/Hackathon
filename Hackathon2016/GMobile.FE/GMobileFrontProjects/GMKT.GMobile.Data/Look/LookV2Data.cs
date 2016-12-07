using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GMKT.GMobile.Data
{
	#region enum
	public enum LookV2PageType
	{
		Unknown
		,
		Fashion
			, Beauty
	}

	public enum LookV2PromotionType
	{
		Unknown
		,
		AType
			, BType
	}

	public enum LookV2CastContentsType
	{
		Unknown
		,
		Movie
			, Contents
	}

	public enum LookV2CategoryType
	{
		Unknown
		,
		Soho
			, Brand
	}

	public enum LookV2SohoMainItemType
	{
		Unknown
		,
		Banner
			, Item
	}

	public enum LookV2SohoMainItemGroupType
	{
		Unknown
		,
		LeftBigRightTwo
			,
		NormalThree
			, LeftTwoRightBig
	}

	public enum LookV2SohoImageType
	{
		Unknown
		,
		Left
			,
		Right
			, Normal
	}

	public enum LookV2MainGroupType
	{
		Unknown
		,
		Promotion
			,
		Cast
			,
		SohoGallery
			,
		BrandGallery
			, BrandBest
	}

	public enum LookV2BrandMainItemType
	{
		Unknown
		,
		Banner
			, Item
	}

	public enum LookV2BrandMenuType
	{
		Unknown
		,
		Gallery
			, Best
	}

	#endregion

	#region Item
	public class LookV2MainBase
	{
		public string Title { get; set; }
		public string DetailViewText { get; set; }
		public string MoreLandingUrl { get; set; }
	}

	public class LookV2Item
	{
		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }
		public string BrandName { get; set; }
		public string Price { get; set; }
		public string SellPrice { get; set; }
		public string DiscountRate { get; set; }
		public string ImageUrl { get; set; }
		public string LandingUrl { get; set; }
	}
	#endregion

	#region Main
	public class LookV2MainEntity
	{
		public LookV2MainEntity()
		{
			this.Fashion = new LookV2Group();
			this.Beauty = new LookV2Group();
		}
		public LookV2Group Fashion { get; set; }
		public LookV2Group Beauty { get; set; }
	}

	public class LookV2Main
	{
		public LookV2Main()
		{
			this.Fashion = new List<LookV2Group>();
			this.Beauty = new List<LookV2Group>();
		}
		public List<LookV2Group> Fashion { get; set; }
		public List<LookV2Group> Beauty { get; set; }
	}

	public class LookV2Group
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2MainGroupType Type { get; set; }
		public List<LookV2Promotion> Promotion { get; set; }
		public LookV2CastMain Cast { get; set; }
		public LookV2SohoMain SohoGallery { get; set; }
		public LookV2BrandMain BrandGallery { get; set; }
		public LookV2BrandBestMain BrandBest { get; set; }
	}
	#endregion

	#region 카테고리
	public class LookV2Category
	{
		public long Seq { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2PageType PageType { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2CategoryType CategoryType { get; set; }
		public string LCategoryName { get; set; }
		public string LCategoryCode { get; set; }
		public int Priority { get; set; }
		public DateTime RegDate { get; set; }
		public DateTime ChgDate { get; set; }
	}
	#endregion

	#region 프로모션
	public class LookV2Promotion
	{
		public long Seq { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2PageType PageType { get; set; }
		public int DisplayPositionNo { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2PromotionType ExposeType { get; set; }
		public DateTime DisplayStartDate { get; set; }
		public DateTime DisplayEndDate { get; set; }
		public string BannerImageUrl { get; set; }
		public string TagText { get; set; }
		public string MainText1 { get; set; }
		public string MainText2 { get; set; }
		public string SubText { get; set; }
		public string LandingUrl { get; set; }
		public string GoodsCode { get; set; }
		public string Price { get; set; }
		public DateTime RegDate { get; set; }
		public DateTime ChgDate { get; set; }
		public string PromotionClassStr { get; set; }
		public string PromotionSpanClassStr { get; set; }
	}
	#endregion

	#region Cast 컨텐츠
	public class LookV2CastMain : LookV2MainBase
	{
		public LookV2CastMain()
		{
			this.Items = new List<LookV2CastContents>();
		}
		public List<LookV2CastContents> Items { get; set; }
	}

	public class LookV2Cast
	{
		public LookV2Cast()
		{
			this.Cast = new List<LookV2CastContents>();
			this.Paging = new LookV2Paging();
		}

		public List<LookV2CastContents> Cast { get; set; }
		public LookV2Paging Paging { get; set; }
	}

	public class LookV2CastContents
	{
		public long Seq { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2PageType PageType { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2CastContentsType ContentsType { get; set; }
		public long PublisherInfoSeq { get; set; }
		public DateTime DisplayStartDate { get; set; }
		public DateTime DisplayEndDate { get; set; }
		public int Priority { get; set; }
		public string ContentsTitle { get; set; }
		public string ImageUrl { get; set; }
		public string TextHtml { get; set; }
		public string MainExposeYN { get; set; }
		public string MainText1 { get; set; }
		public string MainText2 { get; set; }
		public string LandingUrl { get; set; }
		public DateTime RegDate { get; set; }
		public DateTime ChgDate { get; set; }
		public string PublisherName { get; set; }
		public string PublisherImageUrl { get; set; }
	}

	public class LookV2CastRelatedItem
	{
		public long Seq { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2PageType PageType { get; set; }
		public long CastContentsSeq { get; set; }
		public string TopExposeYN { get; set; }
		public DateTime DisplayStartDate { get; set; }
		public DateTime DisplayEndDate { get; set; }
		public DateTime RegDate { get; set; }
		public DateTime ChgDate { get; set; }
		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }
		public string Price { get; set; }
		public string DiscountRate { get; set; }
		public string ImageUrl { get; set; }
		public string LandingUrl { get; set; }
	}

	public class LookV2CastDetail
	{
		public LookV2CastDetail()
		{
			this.Cast = new LookV2CastContents();
			this.RelatedItems = new List<LookV2CastRelatedItem>();
			this.RelatedCasts = new List<LookV2CastContents>();
		}
		public LookV2CastContents Cast { get; set; }
		public List<LookV2CastRelatedItem> RelatedItems { get; set; }
		public List<LookV2CastContents> RelatedCasts { get; set; }
	}

	public class LookV2CastDetailModel
	{
		public LookV2CastDetailModel()
		{
			this.IsMovie = false;
			this.IsValidMovie = false;
			this.ContentsTitle = String.Empty;
			this.MainText1 = String.Empty;
			this.MainText2 = String.Empty;
			this.PublisherName = String.Empty;
			this.PublisherImageUrl = String.Empty;
			this.ImageUrl = String.Empty;
			this.IframeHtml = String.Empty;
		}

		public long Seq { get; set; }
		public LookV2PageType PageType { get; set; }
		public bool IsMovie {get; set; }
		public bool IsValidMovie { get; set; }
		public string ContentsTitle { get; set; }
		public string MainText1 { get; set; }
		public string MainText2 { get; set; }
		public string PublisherName { get; set; }
		public string PublisherImageUrl { get; set; }
		public string ImageUrl { get; set; }
		public string IframeHtml { get; set; }
		public string TitleLandingUrl { get; set; }

		public List<LookV2CastRelatedItem> RelatedItems { get; set; }
		public List<LookV2CastContents> RelatedCasts { get; set; }
	}
	#endregion

	#region 소호 상품(베너)
	public class LookV2SohoMain : LookV2MainBase
	{
		public LookV2SohoMain()
		{
			this.Items = new List<LookV2SohoMainItemGroup>();
		}
		public List<LookV2SohoMainItemGroup> Items { get; set; }
	}

	public class LookV2SohoMainItemGroup
	{
		public LookV2SohoMainItemGroup()
		{
			this.Items = new List<LookV2SohoMainItem>();
		}
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2SohoMainItemGroupType Type { get; set; }
		public List<LookV2SohoMainItem> Items { get; set; }
		public string SohoMainItemGroupClass { get; set; }
	}

	public class LookV2SohoMainItem
	{
		public long Seq { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2SohoMainItemType ExposeType { get; set; }
		public int Priority { get; set; }
		public string UseYN { get; set; }
		public DateTime DisplayStartDate { get; set; }
		public DateTime DisplayEndDate { get; set; }
		public string MainText { get; set; }
		public string SubText { get; set; }
		public string BannerImageUrl { get; set; }
		public string LandingUrl { get; set; }
		public string Price { get; set; }
		public string DiscountRate { get; set; }
		public string GoodsCode { get; set; }
		public string ExposeImageUrl { get; set; }
		public DateTime RegDate { get; set; }
		public DateTime ChgDate { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2SohoImageType ImageType { get; set; }
	}
	#endregion

	#region 노출 브랜드 갤러리 정보
	public class LookV2BrandMain : LookV2MainBase
	{
		public LookV2BrandMain()
		{
			this.Items = new List<LookV2BrandGalleryItem>();
		}
		public List<LookV2BrandGalleryItem> Items { get; set; }
	}

	public class LookV2BrandBestMain : LookV2MainBase
	{
		public LookV2BrandBestMain()
		{
			this.Items = new List<LookV2Item>();
		}
		public List<LookV2Item> Items { get; set; }
	}

	public class LookV2BrandGalleryMain
	{
		public LookV2BrandGalleryMain()
		{
			this.Category = new List<LookV2Category>();
			this.GalleryItems = new List<LookV2BrandGalleryItem>();
			this.BestItems = new List<LookV2Item>();
		}

		public List<LookV2Category> Category { get; set; }
		public List<LookV2BrandGalleryItem> GalleryItems { get; set; }
		public List<LookV2Item> BestItems { get; set; }
		public LookV2Paging BestPaging { get; set; }
	}

	public class LookV2BrandGalleryItem : LookV2Item
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2BrandMainItemType ExposeType { get; set; }
		public Nullable<Int32> BrandNo { get; set; }
	}

	public class LookV2BrandLp
	{
		public LookV2BrandLp()
		{
			this.Items = new List<LookV2Item>();
		}

		public long BrandGallerySeq { get; set; }
		public string ImageUrl { get; set; }
		public List<LookV2Item> Items { get; set; }
		public Nullable<Int32> BrandNo { get; set; }
	}

	public class LookV2BrandGallery
	{
		public long Seq { get; set; }
		[JsonConverter(typeof(StringEnumConverter))]
		public LookV2PageType PageType { get; set; }
		public long ExposeBrandInfoSeq { get; set; }
		public string TabMainExposeYN { get; set; }
		public int Priority { get; set; }
		public DateTime DisplayStartDate { get; set; }
		public DateTime DisplayEndDate { get; set; }
		public string BrandBannerName { get; set; }
		public string BrandBannerImageUrl { get; set; }
		public string BrandLandingUrl { get; set; }
		public string GoodsCode1 { get; set; }
		public string GoodsCode2 { get; set; }
		public string GoodsName1 { get; set; }
		public string GoodsName2 { get; set; }
		public string GoodsImageUrl1 { get; set; }
		public string GoodsImageUrl2 { get; set; }
		public string LandingUrl1 { get; set; }
		public string LandingUrl2 { get; set; }
		public string Price1 { get; set; }
		public string Price2 { get; set; }
		public string SellPrice1 { get; set; }
		public string SellPrice2 { get; set; }
		public string DiscountRate1 { get; set; }
		public string DiscountRate2 { get; set; }
		public DateTime RegDate { get; set; }
		public DateTime ChgDate { get; set; }
		public Nullable<Int32> BrandNo { get; set; }
	}
	#endregion

	#region 베스트
	public class LookV2Best
	{
		public LookV2Best()
		{
			this.BestItems = new List<LookV2Item>();
			this.Paging = new LookV2Paging();
		}

		public List<LookV2Item> BestItems { get; set; }
		public LookV2Paging Paging { get; set; }
	}

	public class LookV2SearchResult
	{
		public LookV2SearchResult()
		{
			this.Items = new List<LookV2Item>();
		}
		public List<LookV2Item> Items { get; set; }
		public int TotalCount { get; set; }
	}

	public class LookV2Paging
	{
		public LookV2Paging()
		{
			this.FirstUrl = String.Empty;
			this.NextUrl = String.Empty;
		}

		public int TotalCount { get; set; }
		public int CurrentPageNo { get; set; }

		public string FirstUrl { get; set; }

		public bool HasNext { get; set; }
		public string NextUrl { get; set; }
	}
	#endregion

	
}

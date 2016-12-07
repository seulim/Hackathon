using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Data
{
	public class BizOnItem
	{
		public string DeliveryString { get; set; }
		public string RelativeUrl { get; set; }
		public string ItemTitle { get; set; }
		public string LinkURL { get; set; }
		public string OriginalPrice { get; set; }
		public string Price { get; set; }
		public string DiscountPrice { get; set; }
		public string ImageURL { get; set; }
		public int DiscountRate { get; set; }
		public bool IsFreeShipping { get; set; }
	}

	public class BizOnLPModel
	{
		public BizOnLPModel()
		{
			this.LCategoryFirst = new BizOnCategoryT();
			this.MCategoryFirst = new BizOnCategoryT();
			this.SCategoryFirst = new BizOnCategoryT();

			this.MCategoryList = new List<BizOnCategoryT>();
			this.SCategoryList = new List<BizOnCategoryT>();

			this.BizOnItems = new List<CPPLPSRPItemModel>();

			this.Items = new List<CPPLPSRPItemModel>();
		}

		public BizOnCategoryT LCategoryFirst { get; set; }
		public BizOnCategoryT MCategoryFirst { get; set; }
		public BizOnCategoryT SCategoryFirst { get; set; }

		public List<BizOnCategoryT> MCategoryList { get; set; }
		public List<BizOnCategoryT> SCategoryList { get; set; }

		public List<CPPLPSRPItemModel> BizOnItems { get; set; }

		public List<CPPLPSRPItemModel> Items { get; set; }

		public int TotalGoodsCount { get; set; }
		public int PageNo { get; set; }
		public int PageSize { get; set; }

		public string MCategoryCode { get; set; }
		public string MCategoryName { get; set; }
		public string SCategoryCode { get; set; }
		public string SCategoryName { get; set; }

		public string Url { get; set; }
	}

	public class BizOnLPCategoryInfo
	{
		public BizOnLPCategoryInfo()
		{
			this.LCategory = new BizOnCategoryT();
			this.MCategoryList = new List<BizOnCategoryT>();
			this.SCategoryList = new List<BizOnCategoryT>();
		}

		public BizOnCategoryT LCategory { get; set; }
		public List<BizOnCategoryT> MCategoryList { get; set; }
		public List<BizOnCategoryT> SCategoryList { get; set; }
	}

	public enum BizOnCategoryType
	{
		Large, Middle, Small
	}

	public class BizOnCategoryT
	{
		public string CategoryCd { get; set; }
		public string CategoryNm { get; set; }
		public string GdlcCd { get; set; }
		public string GdmcCd { get; set; }
		public string GdscCd { get; set; }
		public int GroupCd { get; set; }
		public string Type { get; set; }
		public string Rank { get; set; }
	}

	public class BizOnHome
	{
		public List<BizOnBannerT> Banners { get; set; }
		public List<BizOnCategoryGroupT> CategoryGroups { get; set; }
		public List<BizOnItem> SpecialPriceSection { get; set; }
		public List<BizOnItem> BestSection { get; set; }
		public List<KeywordSection> KeywordSection { get; set; }
	}

	public class BizOnBannerT
	{
		public int Seq { get; set; }
		public string Type { get; set; }
		public string Title { get; set; }
		public int Priority { get; set; }
		public string ImageUrl { get; set; }
		public string LinkUrl { get; set; }
		public string HtmlContent { get; set; }
		public string GoodsCode { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}

	public class BizOnCategoryGroupT
	{
		public string GroupNm { get; set; }
		public int GroupCd { get; set; }
		public string ImageUrl { get; set; }
		public List<BizOnGdlcT> GdlcCds { get; set; }
	}

	public class BizOnGdlcT
	{
		public string Code { get; set; }
		public string Name { get; set; }
	}

	public class KeywordSection
	{
		public string Keyword { get; set; }
		public List<BizOnItem> Item { get; set; }
	}

}

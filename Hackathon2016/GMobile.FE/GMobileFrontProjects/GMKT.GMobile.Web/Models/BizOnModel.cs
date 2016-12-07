using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Web.Models
{
	public class BizOnBestModel
	{
		public List<BizOnItem> Items { get; set; }
	}

	public class BizOnHomeModel
	{
		public List<BizOnBannerT> Banners { get; set; }
		public List<BizOnCategoryGroupModel> CategoryGroups { get; set; }
		public List<BizOnItem> SpecialPriceSection { get; set; }
		public List<BizOnItem> BestSection { get; set; }
		public List<KeywordSection> KeywordSection { get; set; }
	}

	public class BizOnCategoryGroupModel
	{
		public string GroupNm { get; set; }
		public int GroupCd { get; set; }
		public string GroupClass { get; set; }
		public string ImageUrl { get; set; }
		public List<BizOnGdlcT> GdlcCds { get; set; }
	}

	//public class BizOnGdlcModel
	//{
	//    public string GdlcCd { get; set; }
	//    public string GdlcNm { get; set; }
	//}

	#region BizOnLP
	//public class BizOnLPModel
	//{
	//    public BizOnLPModel()
	//    {
	//        this.LCategoryFirst = new BizOnCategoryT();
	//        this.MCategoryFirst = new BizOnCategoryT();
	//        this.SCategoryFirst = new BizOnCategoryT();

	//        this.MCategoryList = new List<BizOnCategoryT>();
	//        this.SCategoryList = new List<BizOnCategoryT>();

	//        this.BizOnItems = new List<CPPLPSRPItemModel>();

	//        this.Items = new List<CPPLPSRPItemModel>();
	//    }

	//    public BizOnCategoryT LCategoryFirst { get; set; }
	//    public BizOnCategoryT MCategoryFirst { get; set; }
	//    public BizOnCategoryT SCategoryFirst { get; set; }

	//    public List<BizOnCategoryT> MCategoryList { get; set; }
	//    public List<BizOnCategoryT> SCategoryList { get; set; }

	//    public List<CPPLPSRPItemModel> BizOnItems { get; set; }

	//    public List<CPPLPSRPItemModel> Items { get; set; }

	//    public int TotalGoodsCount { get; set; }
	//    public int PageNo { get; set; }
	//    public int PageSize { get; set; }
	//}
	#endregion
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Data.ShopBest;

namespace GMKT.GMobile.Web.Models
{
	public class ShopPageModel
	{
		public string GroupCode { get; set; }
		public List<BestSellerGroupInfo> BestSellerGroupList { get; set; }
	}
	public class ShopListPageModel : ShopPageModel
	{
		public int TotalShopCount { get; set; }
		public int PageSize { get; set; }
		public List<ShopAndItems> ShopAndItems { get; set; }
		public bool RenderScript { get; set; }
		public bool HasRank { get; set; }
	}

	public class BrandListModel : ShopPageModel
	{
		public List<Brand> BrandList { get; set; }
	}

	public class BestShopListModel : ShopListPageModel
	{
	}

	public class BrandShopListModel : ShopListPageModel
	{
		public string BrandName { get; set; }
		public int BrandNo { get; set; }
	}

	public class NewShopListModel : ShopListPageModel
	{
	}

	public class FavoriteShopListModel : ShopListPageModel
	{
	}
}
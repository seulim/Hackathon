using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Data.SellerShop;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Web.Models
{
	public class SellerShopMainModel : ILandingBannerModel
	{
		public ILandingBannerEntityT LandingBanner { get; set; }
		public ICampaign Campaign { get; set; }

		public SellerShopMain MainDisplayInfo { get; set; }
	}

	public partial class SellerNoticeModel
	{
		public string Title;
		public string Content;
		public bool IsNew;
		public string PostDate;
		public long No;
	}

	public class SellerShopLPSRPModel
	{
		public string MenuName { get; set; }
		public string Keyword { get; set; }
		public string CategoryCode { get; set; }
		public string CategoryName { get; set; }
		public string PrevCateCode { get; set; }
		public string ShopCategory { get; set; }
		public string LcId { get; set; }
		public string McId { get; set; }
		public string ScId { get; set; }
		public int Sort { get; set; }

        public List<CategoryInfoT> CategoryList { get; set; }
        public bool IsShopCategory { get; set; }
	}

	public class SellerShopItemDisplayModel
	{
		public List<SellerShopItemDisplay> ItemDisplayList { get; set; }
		public string AddVideoKey { get; set; }
		public SellerShopItem SelectedItem { get; set; }
		public int IsMain { get; set; }
		public int MagazineCount { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.Voyager;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
	public partial class IndexM
	{
	}

	public partial class NoticeListM
	{
		public string Alias { get; set; }
		public ShopNewsType? Type { get; set; }
		public string Keyword { get; set; }

		public string ShopIntroduction { get; set; }
		public int NoticeCount { get; set; }
		public List<ShopNewsT> NoticeList { get; set; }

		public bool HasMoreButton { get; set; }
		public int MoreCount { get; set; }
	}

	public partial class GetMoreNoticeListM
	{
		public string Title;
		public bool IsNew;
		public string PostDate;
		public long No;
	}

	public partial class NoticeM
	{
		public string Alias { get; set; }
		public ShopNewsType? Type { get; set; }

		public string Title { get; set; }
		public bool IsNew { get; set; }
		public string Date { get; set; }
		public string Body { get; set; }

		public string PreviousNoticeLink { get; set; }
		public string NextNoticeLink { get; set; }

		public string NoticeListLink { get; set; }
	}

	public partial class HistoryAddM
	{
		public string Alias { get; set; }
		public string ActionName { get; set; }
		public string Message { get; set; }
		public string Url { get; set; }
		public string ImageUrl { get; set; }
	}

	public class ShopOrderModel
	{
		public string BuyCount { get; set; }
		public string BuyAmount { get; set; }
		public string CartCount { get; set; }
		public string InterestCount { get; set; }
	}

	public class CategoryModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public CategoryLevel Level { get; set; }
		public int ItemCount { get; set; }
		public bool IsShopCategory { get; set; }
		public bool IsDisplayCategory { get; set; }
		public bool HasSubCategory { get; set; }
		public string LinkUrl { get; set; }
		
	}

	public partial class ShopWingModel
	{
		public List<CategoryModel> CategoryList { get; set; }
		public List<ShopNewsT> NewsList { get; set; }
		public List<ShopItemModel> ItemList { get; set; }

		public ShopWingModel()
		{
			if (CategoryList == null) CategoryList = new List<CategoryModel>();
			if (NewsList == null) NewsList = new List<ShopNewsT>();
		}
	}

    public class ShopMainGoodsModel : ILandingBannerModel
	{
		public List<ShopItemGroup> MainItems { get; set; }
		public ShopItemGroup NewItems { get; set; }

		public ShopItemModel SelectedItem { get; set; }

        public ILandingBannerEntityT LandingBanner { get; set; }
        public ICampaign Campaign { get; set; }
    }

	public class ShopItemGroup
	{
		public List<ShopItemModel> Items { get; set; }
	}

	public class ShopItemModel
	{
		public string ItemID { get; set; }

		public string Name { get; set; }
		public string VipUrl { get; set; }
		public string ImagePath { get; set; }
		public string ErrorMessage { get; set; }

		public string SellPrice { get; set; }
		public string DealPrice { get; set; }
		public string DiscountPrice { get; set; }
		public string DiscountRate { get; set; }

		public string DeliveryInfo { get; set; }

		public bool AdultYN { get; set; }
		public bool IsDeliveryFree { get; set; }
		public bool IsBrandOn { get; set; }
		public bool IsSoho { get; set; }
		public bool IsLotte { get; set; }

		public int PageNo { get; set; }

		public string BigImagePath { get; set; }
		public string OnErrorBigImagePath { get; set; }
	}

	public class ShopSearchModel
	{
		public string Keyword { get; set; }
		public string CategoryCode { get; set; }
		public string CategoryName { get; set; }
		public string prevCateCode { get; set; }
		public string prevCateName { get; set; }
		public List<ShopItemModel> Items { get; set; }
		public List<CategoryModel> CategoryInfo { get; set; }
		public bool IsMoreCategpry { get; set; }
		public int PageNo { get; set; }
		public int Sort { get; set; }
		public int TotalCount { get; set; }
		public string Alias { get; set; }
		public bool IsContainCategorySearch { get; set; }
		public bool IsShopCategory { get; set; }
	}

}
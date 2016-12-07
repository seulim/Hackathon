using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using GMKT.GMobile.Data;
namespace GMKT.GMobile.Web.Models
{
	public class ECouponHomeModel
	{
		public List<ECouponEvent> bannerlist;
		public List<ECouponCategoryModel> categorylist;
	}

	public class ECouponCategoryModel
	{
		public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
		public string CategoryCount { get; set; }
		public int BrandCount { get; set; }
		public List<ECouponBrand> brandlist;
	}

	public class ECouponLPModel
	{
		public string BrandName { get; set; }
		public int BrandCode { get; set; }
		public string BrandImageUrl { get; set; }
		public int TotalCount { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public List<ECouponMenuItem> menulist;
	}

    public class ECouponBrandLpModel
    {
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public int TotalSize { get; set; }
        public List<ECouponCategoryModel> CategoryInfoList { get; set; }
        public List<ECouponLPModel> BrandList { get; set; }
    }

    public class ECouponBrandHomeModel
    {
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public int BrandCode { get; set; }
        public string BrandName { get; set; }
        public string BrandImageUrl { get; set; }
        public int MenuItemTotalCount { get; set; }
        public List<ECouponBrandModel> BrandList { get; set; }
        public List<ECouponBrandMenuItemModel> HotBrandMenuItemList { get; set; }
        public List<ECouponBrandMenuItemModel> BrandMenuItemList { get; set; }
    }

    public class ECouponBrandModel
    {
        public int BrandCode { get; set; }
        public string BrandName { get; set; }
        public string LogoImageUrl { get; set; }
        public string LinkURL { get; set; }
    }

    public class ECouponBrandMenuItemModel
    {
        public Int64 MenuSeq { get; set; }
        public string MenuName { get; set; }
        public string MenuImageUrl { get; set; }
        public string MenuLoadingImageUrl { get; set; }
        public int MenuPriority { get; set; }
        public ECouponItemModel ItemInfo { get; set; }
    }

    public partial class ECouponItemModel
    {
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        /// <summary>
        /// 판매가
        /// </summary>
        public decimal SellPrice { get; set; }
        /// <summary>
        /// 판매가.ToString()
        /// </summary>
        public string OriginalPrice { get; set; }
        /// <summary>
        /// 할인가
        /// </summary>
        public decimal DiscountPrice { get; set; }
        /// <summary>
        /// 할인가.ToString()
        /// </summary>
        public string AdCouponPrice { get; set; }
        /// <summary>
        /// 할인 적용가
        /// </summary>
        public string Price { get; set; }
        public string DiscountRate { get; set; }
        public bool HasOption { get; set; }
        public string ImgUrl { get; set; }
        public string VipUrl { get; set; }
        public string BasketUrl { get; set; }
        public string OrderUrl { get; set; }
    }

	//public class ECouponMenuModel
	//{
	//    public int BrandCode { get; set; }
	//    public string BrandName { get; set; }
	//    public long MenuSeq { get; set; }
	//    public string MenuName { get; set; }
	//    public string MenuImageUrl { get; set; }
	//    public string MenuLoadingImageUrl { get; set; }
	//    public int MenuPriority { get; set; }

	//    public string ItemNo { get; set; }
	//    public decimal DiscountPrice { get; set; }
	//    public decimal DealPrice { get; set; }
	//    public decimal SellPrice { get; set; }
	//    public bool HasOption { get; set; }
	//    public string VipUrl { get; set; }
	//    public string BasketUrl { get; set; }
	//    public string OrderUrl { get; set; }
	//}

}
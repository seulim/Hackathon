using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
	class ECouponEntityT
	{
	}

	public class ECouponHomeApiResponse : ApiResponseBase
	{
		public ECouponHome Data { get; set; }
	}

	public class ECouponHome
	{
		public List<ECouponBanner> Banner { get; set; }
		public List<ECouponBrand> Brand { get; set; }
		public List<ECouponCategory> Category { get; set; }
		public List<ECouponItem> HotItem { get; set; }
		public List<ECouponBestCategory> BestCategory { get; set; }
		//public List<ECouponCategoryWithBrandInfo> Category { get; set; }
	}

	public partial class ECouponBanner
	{
		public long EventSeq { get; set; }
		public string EventName { get; set; }
		public string LinkUrl { get; set; }
		public string BannerImageUrl { get; set; }
		public string UseYn { get; set; }
		public int Priority { get; set; }
	}

	public class ECouponItem
	{
		public string ItemNo { get; set; }
		public string ItemName { get; set; }
		public decimal SellPrice { get; set; }
		public string OriginalPrice { get; set; }
		public decimal DiscountPrice { get; set; }
		public string Price { get; set; }
		public string AdCouponPrice { get; set; }
		public string DiscountRate { get; set; }
		public bool HasOption { get; set; }
		public string ImgUrl { get; set; }
		public string VipUrl { get; set; }
		public string BasketUrl { get; set; }
		public string OrderUrl { get; set; }
		public int Priority { get; set; }
	}

	public class ECouponCategoryWithBrandInfo
	{
		public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
		public int BrandCount { get; set; }
		public List<ECouponBrand> BrandList { get; set; }
	}

    public class ECouponBrandLpInfo
    {
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public List<ECouponCategory> CategoryInfoList { get; set; }
        public List<ECouponBrand> BrandList { get; set; }
    }

    public class ECouponBrandLpMore
    {
        public int TotalSize { get; set; }
        public List<ECouponBrand> BrandList { get; set; }
    }

    public class ECouponBrandHomeInfo
    {
        public ECouponCategoryWithBrandInfo CategoryWithBrandInfo { get; set; }
        public ECouponBrand Brand { get; set; }
        public List<ECouponBrandMenuItem> HotBrandMenuItem { get; set; }
        public List<ECouponBrandMenuItem> BrandMenuItem { get; set; }
    }

    public partial class ECouponBrandMenuItem
    {
        public Int64 MenuSeq { get; set; }
        public string MenuName { get; set; }
        public string MenuImageUrl { get; set; }
        public string MenuLoadingImageUrl { get; set; }
        public int MenuPriority { get; set; }
        public int TotalCount { get; set; }
        public ECouponItemInfo ItemInfo { get; set; }
    }

    public class ECouponBrandMenuMore
    {
        public List<ECouponBrandMenuItem> BrandMenuItemList { get; set; }
    }

    public partial class ECouponItemInfo
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
        public int Priority { get; set; }
    }
}

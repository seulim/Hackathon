using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Util;
using GMobile.Data.Voyager;
using GMKT.GMobile.Exceptions;

namespace GMKT.GMobile.Web.Controllers
{
	public class ECouponController : GMobileControllerBase
	{
		const int BANNER_MAX_PAGE_SIZE = 10;
		const int CATEGOORY_MAX_PAGE_SIZE = 100;
		const int BRAND_MAX_PAGE_SIZE = 100;
		const int MENU_MAX_PAGE_SIZE = 100;
        const int DEFAULT_BRANDLP_PAGE_SIZE = 100;
		//
		// GET: /ECoupon/
		public ActionResult Index()
		{
			ViewBag.HeaderTitle = "e쿠폰";
			ApiResponse<ECouponHome> GetECouponHome = new ECouponBiz().GetECouponHome();

			if (GetECouponHome == null || GetECouponHome.ResultCode == 1000)
			{
				throw new BusinessRuleException("e쿠폰 정보를 조회할 수 없습니다.");
			}

			string ecouponImg = string.Empty;
			string firstBrandImg = string.Empty;
			if (GetECouponHome != null && GetECouponHome.Data != null && GetECouponHome.Data.Brand != null && GetECouponHome.Data.Brand.Count > 0)
			{
				firstBrandImg = GetECouponHome.Data.Brand[0].LogoImageUrl;
			}
			else firstBrandImg = null;
			
			if (String.IsNullOrEmpty(firstBrandImg) || firstBrandImg.Contains("/640/ecoupon_ready_lp.jpg"))
			{
				ecouponImg = Urls.MobileImageUrlV2 + "ecoupon_icon.png";
			}
			else
			{
				ecouponImg = firstBrandImg;
			}

			ViewBag.Title = "e쿠폰 - G마켓 모바일";

            PageAttr.FbTitle = "Gmarket e쿠폰";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/ecoupon";
			PageAttr.FbTagImage = ecouponImg;
            PageAttr.FbTagDescription = "Gmarket e쿠폰";
            SetHomeTabName("e쿠폰");
            PageAttr.IsMain = true;
            PageAttr.HeaderType = GMKT.GMobile.CommonData.HeaderTypeEnum.Normal;
            return View("IndexNew2", new LandingBannerSetter(Request).Set(new ECouponHomeEx(GetECouponHome.Data), PageAttr.IsApp));
			//return View("IndexNew", GetECouponHome.Data);
			//return View("IndexNew", new LandingBannerSetter(Request).Set(new ECouponHomeEx(GetECouponHome.Data), PageAttr.IsApp));
           
		}

		/// <summary>
		/// EcouponHome Model Extension Inner Class
		/// </summary>
		public class ECouponHomeEx : ECouponHome, ILandingBannerModel
		{
			public ILandingBannerEntityT LandingBanner { get; set; }
			public ICampaign Campaign { get; set; }

			public ECouponHomeEx(ECouponHome model)
			{
				this.Banner = model.Banner == null ? new List<ECouponBanner>() : model.Banner;
				this.Category = model.Category == null ? new List<ECouponCategory>() : model.Category;
				this.Brand = model.Brand == null ? new List<ECouponBrand>() : model.Brand;
				this.HotItem = model.HotItem == null ? new List<ECouponItem>() : model.HotItem;
				this.BestCategory = model.BestCategory == null ? new List<ECouponBestCategory>() : model.BestCategory;
			}
		}

        /// <summary>
        /// e쿠폰 브랜드홈
        /// </summary>
        /// <param name="brandcode"></param>
        /// <returns></returns>
		public ActionResult Brand_backup(int brandcode)
		{
			ECouponLPModel lpmodel = new ECouponLPModel();
			if (lpmodel.menulist == null)
				lpmodel.menulist = new List<ECouponMenuItem>();

			List<ECouponBrandMenu> menulist = new ECouponBiz().GetMobileECouponBrandMenu(brandcode, MENU_MAX_PAGE_SIZE);

			if (menulist == null || menulist.Count <= 0)
			{
				lpmodel.BrandName = "상품 준비 중";
				lpmodel.BrandImageUrl = "";
				lpmodel.TotalCount = 0;
				lpmodel.PageIndex = 1;
				lpmodel.PageSize = MENU_MAX_PAGE_SIZE;
				lpmodel.menulist = new List<ECouponMenuItem>();
				lpmodel.menulist.Add(new ECouponMenuItem() {
					BrandCode = brandcode,
					BrandName = lpmodel.BrandName,
					MenuImageUrl = String.Format("{0}{1}", Urls.MobileImageUrlV2, "/640/ecoupon_ready_item.jpg"),
					MenuLoadingImageUrl =  String.Format("{0}{1}", Urls.MobileImageUrlV2, "/loading_200x200.gif"),
					MenuName = "준비중",
					MenuPriority = 99,
					MenuSeq = -1,
				});
			}
			else
			{
				foreach(ECouponBrandMenu menu in menulist)
				{
					if (String.IsNullOrEmpty(lpmodel.BrandName) || String.IsNullOrEmpty(lpmodel.BrandImageUrl))
					{
						lpmodel.BrandName = menu.BrandName;
						lpmodel.BrandImageUrl = menu.LogoImageUrl;
						lpmodel.TotalCount = menu.TotalCount;
						lpmodel.PageIndex = menu.PageIndex;
						lpmodel.PageSize = menu.PageSize;
					}

					lpmodel.menulist.Add(new ECouponMenuItem(){
						BrandCode = menu.BrandCode,
						BrandName = menu.BrandName,
						MenuSeq = menu.MenuSeq,
						MenuName = menu.MenuName,
						MenuImageUrl = menu.ImageUrl,
						MenuLoadingImageUrl = String.Format("{0}{1}", Urls.MobileImageUrlV2, "/loading_200x200.gif"),
						MenuPriority = menu.Priority
					});
				}
			}

			return View(lpmodel);
		}

        /// <summary>
        /// e쿠폰 브랜드홈
        /// </summary>
        /// <param name="brandcode"></param>
        /// <returns></returns>
        public ActionResult Brand(string categoryCode, int brandcode)
        {
				ViewBag.HeaderTitle = "e쿠폰";
            ECouponBrandHomeModel returnModel = new ECouponBrandHomeModel();
            returnModel.BrandList = new List<ECouponBrandModel>();
            returnModel.HotBrandMenuItemList = new List<ECouponBrandMenuItemModel>();
            returnModel.BrandMenuItemList = new List<ECouponBrandMenuItemModel>();

            //변수선언
            List<ECouponBrandModel> brandList = new List<ECouponBrandModel>();
            List<ECouponBrandMenuItemModel> hotBrandMenuItemList = new List<ECouponBrandMenuItemModel>();
            List<ECouponBrandMenuItemModel> brandMenuItemList = new List<ECouponBrandMenuItemModel>();
            int menuItemTotalCount = 0;

            ApiResponse<ECouponBrandHomeInfo> brandHomeInfo = new ECouponBiz().GetECouponBrandHomeInfo(categoryCode, brandcode);
            if (brandHomeInfo == null || brandHomeInfo.Data == null || brandHomeInfo.ResultCode == 1000 || brandHomeInfo.Data.CategoryWithBrandInfo == null || brandHomeInfo.Data.Brand == null)
            {
                brandHomeInfo.Data = new ECouponBrandHomeInfo();
                brandHomeInfo.Data.Brand = new ECouponBrand();
                brandHomeInfo.Data.Brand.BrandCode = brandcode;
                brandHomeInfo.Data.Brand.BrandName = "상품 준비 중";
                if (brandHomeInfo.Data.CategoryWithBrandInfo == null)
                {
                    brandHomeInfo.Data.CategoryWithBrandInfo = new ECouponCategoryWithBrandInfo();
                    brandHomeInfo.Data.CategoryWithBrandInfo.CategoryCode = categoryCode;
                    brandHomeInfo.Data.CategoryWithBrandInfo.CategoryName = "카테고리 없음";
                }


                //throw new BusinessRuleException("e쿠폰 브랜드홈 정보를 조회할 수 없습니다.");
            }

            //임시저장
            ECouponCategoryWithBrandInfo categoryWithBrandInfo = brandHomeInfo.Data.CategoryWithBrandInfo;
            ECouponBrand brand = brandHomeInfo.Data.Brand;

            //브랜드리스트 매칭
            if (categoryWithBrandInfo.BrandList!= null && categoryWithBrandInfo.BrandList.Count > 0)
            {
                foreach (ECouponBrand eCouponBrand in categoryWithBrandInfo.BrandList)
                {
                    brandList.Add(new ECouponBrandModel{BrandCode = eCouponBrand.BrandCode, BrandName = eCouponBrand.BrandName});
                }
            }

            //HOT브랜드메뉴아이템 매칭
            if (brandHomeInfo.Data.HotBrandMenuItem != null && brandHomeInfo.Data.HotBrandMenuItem.Count > 0)
            {
                foreach (ECouponBrandMenuItem hotMenuItem in brandHomeInfo.Data.HotBrandMenuItem)
                {
                    ECouponItemModel itemModel = new ECouponItemModel();

                    itemModel.ItemNo = hotMenuItem.ItemInfo.ItemNo;
                    itemModel.ItemName = hotMenuItem.ItemInfo.ItemName;
                    itemModel.SellPrice = hotMenuItem.ItemInfo.SellPrice;
                    itemModel.OriginalPrice = hotMenuItem.ItemInfo.OriginalPrice;
                    itemModel.DiscountPrice = hotMenuItem.ItemInfo.DiscountPrice;
                    itemModel.AdCouponPrice = hotMenuItem.ItemInfo.AdCouponPrice;
                    itemModel.Price = hotMenuItem.ItemInfo.Price;
                    itemModel.DiscountRate = hotMenuItem.ItemInfo.DiscountRate;
                    itemModel.HasOption = hotMenuItem.ItemInfo.HasOption;
                    itemModel.ImgUrl = hotMenuItem.ItemInfo.ImgUrl;
                    itemModel.VipUrl = hotMenuItem.ItemInfo.VipUrl;
                    itemModel.BasketUrl = hotMenuItem.ItemInfo.BasketUrl;
                    itemModel.OrderUrl = hotMenuItem.ItemInfo.OrderUrl;

                    hotBrandMenuItemList.Add(new ECouponBrandMenuItemModel {
                        MenuSeq = hotMenuItem.MenuSeq,
                        MenuName = hotMenuItem.MenuName,
                        MenuImageUrl = hotMenuItem.MenuImageUrl,
                        MenuLoadingImageUrl = hotMenuItem.MenuLoadingImageUrl,
                        MenuPriority = hotMenuItem.MenuPriority,
                        ItemInfo = itemModel
                    });
                }
            }

            //브랜드메뉴아이템 매칭
            if (brandHomeInfo.Data.BrandMenuItem != null && brandHomeInfo.Data.BrandMenuItem.Count > 0)
            {
                foreach (ECouponBrandMenuItem menuItem in brandHomeInfo.Data.BrandMenuItem)
                {
                    ECouponItemModel itemModel = new ECouponItemModel();

                    itemModel.ItemNo = menuItem.ItemInfo.ItemNo;
                    itemModel.ItemName = menuItem.ItemInfo.ItemName;
                    itemModel.SellPrice = menuItem.ItemInfo.SellPrice;
                    itemModel.OriginalPrice = menuItem.ItemInfo.OriginalPrice;
                    itemModel.DiscountPrice = menuItem.ItemInfo.DiscountPrice;
                    itemModel.AdCouponPrice = menuItem.ItemInfo.AdCouponPrice;
                    itemModel.Price = menuItem.ItemInfo.Price;
                    itemModel.DiscountRate = menuItem.ItemInfo.DiscountRate;
                    itemModel.HasOption = menuItem.ItemInfo.HasOption;
                    itemModel.ImgUrl = menuItem.ItemInfo.ImgUrl;
                    itemModel.VipUrl = menuItem.ItemInfo.VipUrl;
                    itemModel.BasketUrl = menuItem.ItemInfo.BasketUrl;
                    itemModel.OrderUrl = menuItem.ItemInfo.OrderUrl;

                    brandMenuItemList.Add(new ECouponBrandMenuItemModel
                    {
                        MenuSeq = menuItem.MenuSeq,
                        MenuName = menuItem.MenuName,
                        MenuImageUrl = menuItem.MenuImageUrl,
                        MenuLoadingImageUrl = menuItem.MenuLoadingImageUrl,
                        MenuPriority = menuItem.MenuPriority,
                        ItemInfo = itemModel
                    });

                    menuItemTotalCount = menuItem.TotalCount;   //브랜드메뉴아이템 총개수
                }
            }

            //최종데이터 모델 매칭
            returnModel.CategoryName = categoryWithBrandInfo.CategoryName;
            returnModel.CategoryCode = categoryWithBrandInfo.CategoryCode;
            returnModel.BrandCode = brand.BrandCode;
            returnModel.BrandName = brand.BrandName;
            returnModel.BrandImageUrl = brand.LogoImageUrl;
            returnModel.MenuItemTotalCount = menuItemTotalCount;
            returnModel.BrandList = brandList;
            returnModel.HotBrandMenuItemList = hotBrandMenuItemList;
            returnModel.BrandMenuItemList = brandMenuItemList;

            return View(returnModel);
        }

        /// <summary>
        /// e쿠폰 브랜드홈 목록더보기
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <param name="brandcode"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetBrandHomeMenuMore(int brandcode, int pageIndex = 1, int pageSize = BRAND_MAX_PAGE_SIZE)
        {
            var result = new ECouponBiz().GetECouponBrandHomeMenuMore(brandcode, pageIndex, pageSize);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// e쿠폰 브랜드LP
        /// </summary>
        /// <param name="brandcode"></param>
        /// <returns></returns>
        public ActionResult BrandLp(string categoryCode)
        {
					ViewBag.HeaderTitle = "e쿠폰";
            ECouponBrandLpModel returnModel = new ECouponBrandLpModel();
            returnModel.CategoryInfoList = new List<ECouponCategoryModel>();
            returnModel.BrandList = new List<ECouponLPModel>();

            ApiResponse<ECouponBrandLpInfo> eCouponBrandLp = new ECouponBiz().GetECouponBrandLp(categoryCode);
            if (eCouponBrandLp == null || eCouponBrandLp.Data == null || eCouponBrandLp.ResultCode == 1000)
            {
                eCouponBrandLp.Data = new ECouponBrandLpInfo();
                eCouponBrandLp.Data.CategoryCode = categoryCode;
                eCouponBrandLp.Data.CategoryName = "카테고리 없음";
                eCouponBrandLp.Data.CategoryInfoList = new List<ECouponCategory>();
                //throw new BusinessRuleException("e쿠폰 브랜드 정보를 조회할 수 없습니다.");
            }

            if (eCouponBrandLp.Data.CategoryInfoList != null && eCouponBrandLp.Data.CategoryInfoList.Count > 0)
            {
                foreach (ECouponCategory category in eCouponBrandLp.Data.CategoryInfoList)
                {
                    returnModel.CategoryInfoList.Add(new ECouponCategoryModel { CategoryName = category.CategoryName, CategoryCode = category.CategoryCode });
                }

                if (eCouponBrandLp.Data.BrandList == null || eCouponBrandLp.Data.BrandList.Count <= 0)
                {
                    returnModel.BrandList.Add(new ECouponLPModel
                    {
                        BrandName = "상품 준비 중",
                        BrandCode = 0,
                        BrandImageUrl = "",
                        TotalCount = 0,
                        PageIndex = 1,
                        PageSize = BRAND_MAX_PAGE_SIZE
                    });
                }
                else
                {
                    foreach (ECouponBrand brand in eCouponBrandLp.Data.BrandList)
                    {
                        returnModel.BrandList.Add(new ECouponLPModel
                        {
                            BrandName = brand.BrandName,
                            BrandCode = brand.BrandCode,
                            BrandImageUrl = brand.LogoImageUrl,
                            TotalCount = 0,
                            PageIndex = 1,
                            PageSize = BRAND_MAX_PAGE_SIZE
                        });
                    }
                }

                returnModel.CategoryName = eCouponBrandLp.Data.CategoryName;
                returnModel.CategoryCode = eCouponBrandLp.Data.CategoryCode;
                returnModel.TotalSize = eCouponBrandLp.Data.BrandList.Count;
            }

            return View(returnModel);
        }

        /// <summary>
        /// e쿠폰 브랜드LP 더보기
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetBrandLpListMore(string categoryCode, int pageIndex = 1, int pageSize = DEFAULT_BRANDLP_PAGE_SIZE)
        {
            var result = new ECouponBiz().GetECouponBrandLpMore(categoryCode, pageIndex, pageSize);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
		public JsonResult GetMenuMore(int brandcode, int page_idx, int page_size)
		{
			ECouponLPModel lpmodel = new ECouponLPModel();
			if (lpmodel.menulist == null)
				lpmodel.menulist = new List<ECouponMenuItem>();

			List<ECouponMenuItem> menulist = new ECouponBiz().GetMobileECouponBrandMenuWithItemPaging(brandcode, page_idx, page_size);

			return Json(menulist);
		}

		public ActionResult TestCache(int id)
		{
			//todo:캐시확인
			ViewBag.CacheTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

			return View();
		}

        [HttpPost]
        public JsonResult GetItemTopOne(long menuseq)
        {
            ECouponItemTop1 itemTop1 = new ECouponBiz().GetMobileECouponItemTopOne(menuseq, 100);

            return Json(itemTop1);
        }

        public ActionResult IndexNew()
        {
            return View();
        }

		/// <summary>
		/// e 쿠폰 장바구니 넣기
		/// </summary>
		/// <param name="itemNo"></param>
		/// <param name="orderQty"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult GetAddCartResult(string itemNo, short orderQty, bool isInstantOrder)
		{
			ApiResponse<Nova.Thrift.AddCartResultI> result = new ECouponApiBiz().GetAddCartResult(itemNo, orderQty, isInstantOrder);
			return Json(result);
		}

		/// <summary>
		/// e 쿠폰 장바구니 삭제
		/// </summary>
		[HttpPost]
		public JsonResult GetRemoveCartResult(string cartPID, string orderIdxString)
		{
			ApiResponse<Nova.Thrift.ResultI> result = new ECouponApiBiz().GetRemoveCartResult(cartPID, orderIdxString);
			return Json(result);
		}
	}
}
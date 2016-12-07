using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Util;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.CommonData;

namespace GMKT.GMobile.Web.Controllers
{
	public class DepartmentStoreController : GMobileControllerBase
	{
		public ActionResult Index()
		{
			SetHomeTabName("백화점");
			PageAttr.IsMain = true;
			PageAttr.HeaderType = HeaderTypeEnum.Normal;
			ViewBag.Title = "백화점 - G마켓 모바일";
			List<DepartmentStoreMainGroupT> departmentStoreMainList = new DepartmentStoreApiBiz_Cache().GetDepartmentStoreMain();
			DepartmentStoreMainGroupT mainModel = new DepartmentStoreMainGroupT()
			{
				Banner = new DepartmentStoreBannerSectionT()
				{
					MainBanner = new List<DepartmentStoreBannerT>(),
					AllBanner = new List<DepartmentStoreBannerT>()
				},
				ShopCategory = new List<DepartmentStoreCategoryT>(),
				Item = new List<HomeMainItem>(),
				BottomShopCategory = new List<DepartmentStoreCategoryT>()
								
			};
			
			if (departmentStoreMainList != null)
			{
				foreach (DepartmentStoreMainGroupT mainEntity in departmentStoreMainList)
				{
					if (mainEntity.Type == DepartmentStoreMainGroupType.Banner)
					{
						if (mainEntity.Banner != null && mainEntity.Banner.MainBanner != null && mainEntity.Banner.MainBanner.Count > 0)
						{
							mainModel.Banner.MainBanner = mainEntity.Banner.MainBanner;
						}

						if (mainEntity.Banner != null && mainEntity.Banner.AllBanner != null && mainEntity.Banner.AllBanner.Count > 0)
						{
							mainModel.Banner.AllBanner = mainEntity.Banner.AllBanner;
						}
					}
					else if (mainEntity.Type == DepartmentStoreMainGroupType.ShopCategory)
					{
						if (mainEntity.ShopCategory != null && mainEntity.ShopCategory.Count > 0)
						{
							mainModel.ShopCategory = mainEntity.ShopCategory;
						}
					}
					else if (mainEntity.Type == DepartmentStoreMainGroupType.Item)
					{
						if (mainEntity.Item != null && mainEntity.Item.Count > 0)
						{
							mainModel.Item = mainEntity.Item;
						}
					}
					else if (mainEntity.Type == DepartmentStoreMainGroupType.BottomShopCategory)
					{
						if (mainEntity.BottomShopCategory != null && mainEntity.BottomShopCategory.Count > 0)
						{
							mainModel.BottomShopCategory = mainEntity.BottomShopCategory;
						}
					}
					
				}
			}

			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket 백화점";
			PageAttr.FbTagUrl = string.Format("{0}/DepartmentStore", Urls.MobileWebUrl);
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket 백화점";
			#endregion

			DepartmentStoreMainModel mainViewModel = new DepartmentStoreMainModel();
			mainViewModel.Banner = mainModel.Banner;
			mainViewModel.ShopCategory = mainModel.ShopCategory;
			mainViewModel.Item = mainModel.Item;
			mainViewModel.BottomShopCategory = mainModel.BottomShopCategory;
			

			#region Lannding Banner
			new LandingBannerSetter(Request).Set(mainViewModel, PageAttr.IsApp);
			#endregion

			return View(mainViewModel);
		}

		public ActionResult Search(SearchRequest input)
		{
			ViewBag.HeaderTitle = "백화점 검색결과";
			PageAttr.IsMain = false;
			PageAttr.HeaderType = HeaderTypeEnum.Minishop;
			PageAttr.HeaderCode = "department";

			ViewBag.menuName = "SRP";
			ViewBag.Title = "백화점 검색 - G마켓 모바일";
			ViewBag.primeKeyword = input.primeKeyword;
			ViewBag.moreKeyword = input.moreKeyword;
			ViewBag.lcId = input.lcId;
			ViewBag.mcId = input.mcId;
			ViewBag.scId = input.scId;
			ViewBag.brandList = input.brandList;
			ViewBag.sortType = input.sortType;
			ViewBag.sellCustNo = input.sellCustNo;
			ViewBag.pageNo = input.pageNo;
			ViewBag.pageSize = input.pageSize;
			ViewBag.mClassSeq = input.mClassSeq;
			ViewBag.sClassSeq = input.sClassSeq;
			ViewBag.valueId = input.valueId;
			ViewBag.valueIdName = input.valueIdName;
			ViewBag.isBizOn = "N";
			ViewBag.keywordList = input.keywordList;
			ViewBag.minPrice = 0;
			ViewBag.maxPrice = 0;

			ViewBag.isDepartmentStore = true;
			
			GMKT.GMobile.Web.Controllers.CategoryController.LandingBannerModel landingBannermodel = new GMKT.GMobile.Web.Controllers.CategoryController.LandingBannerModel();
			new LandingBannerSetter(Request).Set(landingBannermodel, PageAttr.IsApp);
			return View(landingBannermodel);
		}

		public ActionResult Category(string categoryGroup)
		{
			List<DepartmentStoreGroupCategoryT> groupList = new DepartmentStoreApiBiz_Cache().GetDepartmentStoreCategoryGroup();
			DepartmentStoreGroupCategoryT selectedGroup = groupList.FirstOrDefault(T => T.CategoryGroupCode == categoryGroup);

			ViewBag.HeaderTitle = "백화점>" + selectedGroup.CategoryGroupName;

			if(String.IsNullOrEmpty(categoryGroup) || selectedGroup == null)
			{
				return Redirect(Urls.MobileWebUrl);
			}

			ViewBag.categoryGroupList = groupList;
			ViewBag.selectedCategoryGroupCode = selectedGroup.CategoryGroupCode;
			ViewBag.selectedCategoryGroupName = selectedGroup.CategoryGroupName;
			ViewBag.departmentGroupCode = categoryGroup;
			ViewBag.isDepartmentStore = true;

			PageAttr.IsMain = false;
			PageAttr.HeaderType = HeaderTypeEnum.Minishop;
			PageAttr.HeaderCode = "department";

			SearchRequest defaultSearchInput = new SearchRequest();
			ViewBag.menuName = "LP";
			ViewBag.Title = "백화점 상품 - G마켓 모바일";
			ViewBag.minPrice = defaultSearchInput.minPrice;
			ViewBag.maxPrice = defaultSearchInput.maxPrice;
			ViewBag.sortType = defaultSearchInput.sortType;
			ViewBag.sellCustNo = defaultSearchInput.sellCustNo;
			ViewBag.pageNo = defaultSearchInput.pageNo;
			ViewBag.pageSize = defaultSearchInput.pageSize;
			ViewBag.mClassSeq = defaultSearchInput.mClassSeq;
			ViewBag.sClassSeq = defaultSearchInput.sClassSeq;
			ViewBag.valueId = defaultSearchInput.valueId;
			ViewBag.valueIdName = defaultSearchInput.valueIdName;

			/* Landing Banner */
			GMKT.GMobile.Web.Controllers.CategoryController.LandingBannerModel landingBannermodel = new GMKT.GMobile.Web.Controllers.CategoryController.LandingBannerModel();
			new LandingBannerSetter(Request).Set(landingBannermodel, PageAttr.IsApp);

			return View("~/Views/DepartmentStore/Search.cshtml", landingBannermodel);
		}

		public ActionResult Now(string category = "")
		{
			if (string.IsNullOrEmpty(category))
			{
				category = "0";
			}

			PageAttr.IsMain = false;
			PageAttr.HeaderType = HeaderTypeEnum.Minishop;
			PageAttr.HeaderCode = "department";
			ViewBag.HeaderTitle = "백화점";
			ViewBag.Title = "백화점 상품 - G마켓 모바일";
			ViewBag.Category = category;

			DepartmentStoreNowT result = new DepartmentStoreApiBiz_Cache().GetDepartmentStoreNow(category);
			DepartmentStoreNowModel nowViewModel = new DepartmentStoreNowModel();

			nowViewModel.ShopCategory = result.ShopCategory;
			nowViewModel.Item = result.Item;

			#region Lannding Banner
			new LandingBannerSetter(Request).Set(nowViewModel, PageAttr.IsApp);
			#endregion

			return View(nowViewModel);
		}

		[HttpGet]
		public JsonResult GetDepartmentBestItem()
		{
			return Json(new DepartmentStoreApiBiz_Cache().GetDepartmentBestItem(), JsonRequestBehavior.AllowGet);
		}
	}
}

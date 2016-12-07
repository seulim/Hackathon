using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using GMKT.GMobile.Data.SmartDelivery;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Biz.SmartDelivery;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Web.Controllers
{
    public class SmartDeliveryController : GMobileControllerBase
    {
        //
        // GET: /SmartDelivery/

        public ActionResult Index()
        {
				ViewBag.HeaderTitle = "스마트배송";
			SmartDeliveryFrontModel model = new SmartDeliveryFrontModel();
			
			// 스마트배송 카테고리 정보
			List<SmartDeliverCatetoryModel> category = new SmartDeliveryApiBiz_Cache().GetSmartDeliveryGDLCList();
			if (category == null || category.Count < 1)
			{
				category = new SmartDeliveryApiBiz().GetSmartDeliveryGDLCList();
				if (category == null) category = new List<SmartDeliverCatetoryModel>();
			}
			model.Category = category;

			List<SmartDeliveryBannerModel> totalBannerList = new SmartDeliveryApiBiz_Cache().GetSmartDeliveryBannerList();
			if (totalBannerList == null || totalBannerList.Count < 1)
			{
				totalBannerList = new SmartDeliveryApiBiz().GetSmartDeliveryBannerList();
				if (totalBannerList == null) totalBannerList = new List<SmartDeliveryBannerModel>();
			}

			if (totalBannerList != null && totalBannerList.Count > 0)
			{
				foreach (SmartDeliveryBannerModel banner in totalBannerList)
				{
					if (banner != null && banner.BannerList.Count > 0)
					{
						if (banner.BannerType == "mainBannerList")
						{
							model.MainBanner = banner.BannerList;
						}
						else if (banner.BannerType == "deliveryNoticeBannerList")
						{
							model.DeliveryNoticeBanner = banner.BannerList.First();
						}
						else if (banner.BannerType == "qnaBannerList")
						{
							model.QnABanner = banner.BannerList[0];
						}
					}
				}
			}

			List<SmartDeliveryDisplayModel> BestGoodsData = new SmartDeliveryApiBiz_Cache().GetSmartDeliveryDisplayList("BestGoodsData");
			if (BestGoodsData == null || BestGoodsData.Count < 1)
			{
				BestGoodsData = new SmartDeliveryApiBiz().GetSmartDeliveryDisplayList("BestGoodsData");
			}
			
			if (BestGoodsData != null && BestGoodsData.Count > 0)
			{
				if (BestGoodsData[0].GoodsList != null && BestGoodsData[0].GoodsList.Count > 0)
				{ model.BestDisplay = BestGoodsData[0].GoodsList; }
			}

			List<SmartDeliveryDisplayModel> RecommGoodsData = new SmartDeliveryApiBiz_Cache().GetSmartDeliveryDisplayList("RecommGoodsData");
			if (RecommGoodsData == null || RecommGoodsData.Count < 1)
			{
				RecommGoodsData = new SmartDeliveryApiBiz().GetSmartDeliveryDisplayList("RecommGoodsData");
			}
			
			if (RecommGoodsData != null && RecommGoodsData.Count > 0)
			{
				if (RecommGoodsData[0].GoodsList != null && RecommGoodsData[0].GoodsList.Count > 0)
				{ model.RecommDisplay = RecommGoodsData[0].GoodsList; }
			}

			List<SmartDeliveryDisplayModel> HotItemGoodsData = new SmartDeliveryApiBiz_Cache().GetSmartDeliveryDisplayList("HotItemGoodsData");
			if (HotItemGoodsData == null || HotItemGoodsData.Count < 1)
			{
				HotItemGoodsData = new SmartDeliveryApiBiz().GetSmartDeliveryDisplayList("HotItemGoodsData");
			}
			if (HotItemGoodsData != null && HotItemGoodsData.Count > 0)
			{
				model.HotItemDisplay = HotItemGoodsData;
				model.HotItemTitle = new List<string>();
				foreach (SmartDeliveryDisplayModel hotitem in model.HotItemDisplay)
				{
					model.HotItemTitle.Add(hotitem.TabTitle);
				}
			}
			
			List<SmartDeliveryDisplayModel> BrandGoodsData = new SmartDeliveryApiBiz_Cache().GetSmartDeliveryDisplayList("BrandGoodsData");
			if (BrandGoodsData == null || BrandGoodsData.Count < 1)
			{
				BrandGoodsData = new SmartDeliveryApiBiz().GetSmartDeliveryDisplayList("BrandGoodsData");
			}
			if (BrandGoodsData != null && BrandGoodsData.Count > 0)
			{
				model.BrandDisplay = BrandGoodsData;
				model.BrandTitle = new List<string>();
				foreach (SmartDeliveryDisplayModel brand in model.BrandDisplay)
				{
					model.BrandTitle.Add(brand.TabTitle);
				}
			}

			return View(model);
        }

		public ActionResult Search(string keyword = "", string lcId = "", string mcId = "", string scId = "", string isSmallPacking = "N")
		{					
			ViewBag.HeaderTitle = "스마트배송";
			if (keyword == "" || keyword.Length == 0)
			{
				return Redirect("/SmartDelivery");
			}
			
			ViewBag.menuName = "SRP";			
			ViewBag.keyword = keyword;			
			ViewBag.lcId = lcId;
			ViewBag.mcId = mcId;
			ViewBag.scId = scId;			
			ViewBag.isSmallPacking = isSmallPacking;
			ViewBag.isSmartDeliveryShop = true;

			return View();
		}

		public ActionResult List(string lcId = "", string mcId = "", string scId = "", string keyword = "", string isSmallPacking = "N")
		{
			ViewBag.HeaderTitle = "스마트배송";
			if(string.IsNullOrEmpty(lcId) && string.IsNullOrEmpty(mcId) && string.IsNullOrEmpty(scId))
			{
				return Redirect("/SmartDelivery");
			}

			string lCategoryCode = lcId;
			string mCategoryCode = mcId;
			string sCategoryCode = scId;

			if (!String.IsNullOrEmpty(mCategoryCode))
			{
				lCategoryCode = string.Empty;
			}

			if (!String.IsNullOrEmpty(sCategoryCode))
			{
				lCategoryCode = string.Empty;
				mCategoryCode = string.Empty;
			}

			ViewBag.menuName = "LP";			
			ViewBag.keyword = keyword;
			ViewBag.lcId = lCategoryCode;
			ViewBag.mcId = mCategoryCode;
			ViewBag.scId = sCategoryCode;
			ViewBag.lcIdName = GMKT.GMobile.Util.CategoryUtil.GetCategoryName(lcId);
			ViewBag.mcIdName = GMKT.GMobile.Util.CategoryUtil.GetCategoryName(mcId);
			ViewBag.scIdName = GMKT.GMobile.Util.CategoryUtil.GetCategoryName(scId);
			ViewBag.isSmallPacking = isSmallPacking;			
			
			return View("Search");
		}

		public ActionResult SmallPackingList()
		{
			ViewBag.HeaderTitle = "스마트배송";
			ViewBag.menuName = "SPP"; //SmallPackingPage			
			ViewBag.isSmallPacking = "Y";
			
			return View("Search");
		}

		[HttpPost]
		public ActionResult SearchJson([System.Web.Http.FromBody] SmartDeliverySearchRequest request)
		{
			SRPResultModel apiResultModel = new SmartDeliveryApiBiz().GetSmartDeliverySearchResult(request);			

			return Json(apiResultModel);
		}

		public ActionResult Best50(string isSmallPacking = "N")
		{
			ViewBag.HeaderTitle = "스마트배송";
			SmartDeliveryBestT bestmodel = new SmartDeliveryApiBiz_Cache().GetSmartDeliveryBest50();
			if (bestmodel == null)
			{
				bestmodel = new SmartDeliveryApiBiz().GetSmartDeliveryBest50();
				if (bestmodel == null) bestmodel = new SmartDeliveryBestT();
			}

			ViewBag.isSmallPacking = isSmallPacking;

			return View(bestmodel);
		}
    }
}

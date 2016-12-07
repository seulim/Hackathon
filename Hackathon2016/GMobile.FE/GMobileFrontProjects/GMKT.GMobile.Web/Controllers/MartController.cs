using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Util;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.CommonData;

namespace GMKT.GMobile.Web.Controllers
{
	public class MartController : GMobileControllerBase
    {
		public ActionResult Index(string categoryCode = "000000000", string goodsCode = "", long categorySeq = 0)
		{
			ViewBag.Title = "마트 - G마켓 모바일";
			PageAttr.HeaderType = HeaderTypeEnum.Normal;
			PageAttr.IsMain = true;
			SetHomeTabName("마트");

			MartV3View model = new MartApiBiz_Cache().GetMartV3View(categoryCode, goodsCode, categorySeq);
			if (model == null)
				model = new MartApiBiz().GetMartV3View(categoryCode, goodsCode, categorySeq);

			model = new MartApiBiz_Cache().GetMartV3View(categoryCode, goodsCode, categorySeq);
			//MartV3View model = new MartApiBiz().GetMartV3View(categoryCode, goodsCode, categorySeq);

			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket 마트";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Mart";
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket 마트";
			#endregion

			MartV3ViewModel martModel = new MartV3ViewModel();

			martModel.IsMain = model.IsMain;
			martModel.TimeDeal = model.TimeDeal;
			martModel.CategoryList = model.CategoryList;
			martModel.ItemGroupList = model.ItemGroupList;
			martModel.BrandList = model.BrandList;
			martModel.ContentsList = model.ContentsList;
			martModel.BannerList = model.BannerList;
            martModel.MartCategoryList = model.MartCategoryList;
			
			/* Landing Banner */
			new LandingBannerSetter(Request).Set(martModel, PageAttr.IsApp);

			return View(martModel);
		}

		public ActionResult Contents(long id)
		{
			ViewBag.Title = "마트 - G마켓 모바일";
			ViewBag.HeaderTitle = "마트 컨텐츠";

			MartContentsView model = new MartApiBiz_Cache().GetMartContentsView(id);

			if (model == null)
				model = new MartApiBiz().GetMartContentsView(id);

			model = new MartApiBiz_Cache().GetMartContentsView(id);

			MartContentsModel martModel = new MartContentsModel();
			martModel.Contents = model.Contents;
			martModel.ContentsList = model.ContentsList; 
			martModel.ItemList = model.ItemList;
			martModel.contentsSeq = id;

			return View(martModel);
		}

		public ActionResult IndexV2(string categoryCode="000000000", string goodsCode="")
		{
			ViewBag.Title = "마트 - G마켓 모바일";
			SetHomeTabName("마트");

			MartV2View model = new MartApiBiz_Cache().GetMartV2View(categoryCode, goodsCode);
			if (model == null)
			{
			    model = new MartApiBiz().GetMartV2View(categoryCode, goodsCode);				
			}

			model = new MartApiBiz_Cache().GetMartV2View(categoryCode, goodsCode);

			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket 마트";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Mart";
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket 마트";
			#endregion			

			MartV2ViewModel martModel = new MartV2ViewModel();
			martModel.TimeDeal = model.TimeDeal;
			martModel.CategoryList = model.CategoryList;			
			martModel.ItemGroupList = model.ItemGroupList;

			/* Landing Banner */
			new LandingBannerSetter(Request).Set(martModel, PageAttr.IsApp);

			if(model.BannerList != null && model.BannerList.Count > 0)
			{
				ViewBag.NeedHomeplus = true;
				ViewBag.HomeplusUrl = model.BannerList[0].LandingUrl;
			}
			else
			{
				ViewBag.NeedHomeplus = false;
			}

			return View(martModel);
		}

		public ActionResult HomePreview(long groupNo)
		{
			MartV3View model = new MartApiBiz().GetMartV3HomePreview(groupNo);

			MartV3ViewModel martModel = new MartV3ViewModel();
			martModel.CategoryList = model.CategoryList;
			martModel.ItemGroupList = model.ItemGroupList;
			martModel.BrandList = model.BrandList;
			martModel.ContentsList = model.ContentsList;
			martModel.BannerList = model.BannerList;

			return View(martModel);
		}

        public ActionResult IndexOld(string categoryCode)
        {
			ViewBag.Title = "마트 - G마켓 모바일";
			SetHomeTabName("마트");

			string martCategory = "000000000";
			if (!String.IsNullOrEmpty(categoryCode))
			{
				martCategory = categoryCode;
			}

			MartView model = new MartApiBiz_Cache().GetMartView(martCategory);
			if (model == null)
			{
				model = new MartApiBiz().GetMartView(martCategory);
				if (model == null)
				{
					model.CategoryList = new List<MartCategory>();
					model.Items = new List<MartItem>();
				}
			}


			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket 마트";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Mart";
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket 마트";
			#endregion

			MartViewModel martModel = new MartViewModel();
			martModel.CategoryList = model.CategoryList;
			martModel.GroupName = model.GroupName;
			martModel.Items = model.Items;
			martModel.SubGroupName = model.SubGroupName;
			
			/* Landing Banner */
			new LandingBannerSetter(Request).Set(martModel, PageAttr.IsApp);

			return View(martModel);
        }

		[HttpPost]
		public ActionResult GetMartItems(string categoryCode="000000000", string goodsCode="")
		{
			List<MartV2ItemGroupT> result = new List<MartV2ItemGroupT>();

			MartV2View model = new MartApiBiz_Cache().GetMartV2View(categoryCode, goodsCode);
			if (model == null || model.ItemGroupList == null)
			{
				model = new MartApiBiz().GetMartV2View(categoryCode, goodsCode);
				if (model != null && model.ItemGroupList != null)
				{
					result = model.ItemGroupList;
				}
			}
			else
			{
				result = model.ItemGroupList;
			}

			return Content(Newtonsoft.Json.JsonConvert.SerializeObject(result), "application/json");
		}

		[HttpPost]
		public JsonResult GetMartTimeDeal()
		{
			MartTimeDealT result = new MartApiBiz().GetTimeDeal();
			return Json(result);
		}
    }
}

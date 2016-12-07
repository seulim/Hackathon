using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Util;
using GMKT.GMobile.Web.Util;
using GMKT.GMobile.App;

namespace GMKT.GMobile.Web.Controllers
{
	public class SuperDealController : GMobileControllerBase
    {
        public ActionResult Index(string id)
        {
				PageAttr.HeaderType = CommonData.HeaderTypeEnum.Minishop;
				PageAttr.HeaderCode = "superdeal";
			ViewBag.Title = "슈퍼딜 - G마켓 모바일";
			//if (string.IsNullOrEmpty(id)) { SetHomeTabName("슈퍼딜"); }

			CheckAppVersion checkAppVersion = new CheckAppVersion();
			AppVersionDigit checkVersion = checkAppVersion.GetAppVersion("5.3.0");
			bool isOverApp = checkAppVersion.CheckOverVersion(PageAttr.AppVersion, checkVersion);
			if (PageAttr.IsIphoneApp || PageAttr.IsAndroidApp)
			{
				if (isOverApp) 
				{
					bool showTab = true;

					CheckAppVersion checkNewVersion = new CheckAppVersion();
					AppVersionDigit newVersion = checkNewVersion.GetAppVersion("5.3.6");

					if (checkNewVersion.CheckOverVersion(PageAttr.AppVersion, newVersion))
					{
						showTab = false;
					}

					if (showTab)
					{
						SetHomeTabName("슈퍼딜");
					}
				}
			}
			else
			{
				//SetHomeTabName("슈퍼딜");
			}


			SuperDealModel model = new SuperDealModel();
			
			#region API 에서 데이터 가져오기
			// 슈퍼딜 카테고리 정보
			List<SuperDealCategory> category = new SuperDealApiBiz_Cache().GetSuperDealThemeCategory();
			if (category == null || category.Count < 1)
			{
				category = new SuperDealApiBiz().GetSuperDealThemeCategory();
				if (category == null) category = new List<SuperDealCategory>();
			}
			model.ThemeCategory = category;

			// 슈퍼딜 상품 가져오기
			#region [Pilot - gsohn] Pilot Test 이전 코드 (테스트 완료후 복구)
			//List<SuperDealItem> items = new SuperDealApiBiz_Cache().GetSuperDealItems("000000000", PageAttr.FromWhere);
			//if (items == null || items.Count < 1)
			//{
			//	items = new SuperDealApiBiz().GetSuperDealItems("000000000", PageAttr.FromWhere);
			//	if (items == null)
			//	{
			//		items = new List<SuperDealItem>();
			//	}
			//}
			#endregion
			#region [Pilot - gsohn] Pilot Test 코드 (테스트 완료후 삭제)
			string userInfo = string.Empty;
			if (gmktUserProfile != null && false == string.IsNullOrEmpty(gmktUserProfile.UserInfoString))
			{
				userInfo = gmktUserProfile.UserInfoString;
			}
			
			#endregion

			List<HomeMainItem> items = new SuperDealApiBiz().GetSuperDealThemeMainItem(userInfo, "000000000");
			if (items == null) items = new List<HomeMainItem>();

			model.ThemeItems = items;
			#endregion

			//#region 페이스북 공유하기
			//string faceBookImage = "";
			//if (items.Count > 0)
			//{
			//    if (items[0] != null)
			//    {
			//        faceBookImage = String.IsNullOrEmpty(items[0].ImageURL) ? "" : items[0].ImageURL;
			//    }
			//}
			//PageAttr.FbTitle = "G마켓 슈퍼딜";
			//PageAttr.FbTagUrl = Urls.MobileWebUrl + "/SuperDeal";
			//PageAttr.FbTagImage = faceBookImage;
			//PageAttr.FbTagDescription = "G마켓 슈퍼딜";
			//#endregion

			/* Landing Banner */
			new LandingBannerSetter(Request).Set(model, PageAttr.IsApp);

			return View(model);
        }
				/// <summary>
				/// 슈퍼딜 상품목록
				/// </summary>
				/// <param name="DispType"></param>
				/// <param name="GroupNo"></param>
				/// <param name="SubNo"></param>
				/// <returns></returns>
        public ActionResult SuperDealThemeList(string DispType = "1", string GroupNo = "", string SubNo = "", string CateIndex="")
				{
					PageAttr.HeaderType = CommonData.HeaderTypeEnum.Minishop;
					PageAttr.HeaderCode = "superdeal";
					ViewBag.Title = "슈퍼딜 - G마켓 모바일";
					ViewBag.DispType = DispType;
					ViewBag.GroupNo = GroupNo;
					ViewBag.SubNo = SubNo;
                    ViewBag.CateIndex = CateIndex;
					ViewBag.MWebUrl = Urls.MWebUrl; 
					SuperDealModel model = new SuperDealModel();
					if(string.IsNullOrEmpty(SubNo)){
					SubNo = "0";
					}
			// 슈퍼딜 카테고리 정보
					List<SuperDealCategory> category = new SuperDealApiBiz_Cache().GetSuperDealThemeCategory();
					if (category == null || category.Count < 1)
					{
						category = new SuperDealApiBiz().GetSuperDealThemeCategory();
						if (category == null) category = new List<SuperDealCategory>();
					}
					model.ThemeCategory = category;

			// 슈퍼딜 상품 가져오기
					List<HomeMainItem> items = new SuperDealApiBiz_Cache().GetSuperDealThemeItem(DispType, GroupNo, SubNo);
					if (items == null || items.Count < 1)
					{
						items = new SuperDealApiBiz().GetSuperDealThemeItem(DispType, GroupNo, SubNo);
						if (items == null)
						{
							items = new List<HomeMainItem>();
						}
					}

					model.ThemeItems = items;

		
				return View(model);
				}
		public ActionResult SuperDealList(string ShopGdlcCd = "000000000", string GdNo = "", string ShopGdmcCd = "")
		{
			PageAttr.HeaderType = CommonData.HeaderTypeEnum.Minishop;
			PageAttr.HeaderCode = "superdeal";
			ViewBag.Title = "슈퍼딜 - G마켓 모바일";
			ViewBag.GdNo = GdNo;
			ViewBag.selectedGdlcCd = ShopGdlcCd;
			ViewBag.selectedGdmcCd = ShopGdmcCd;
			SuperDealModel model = new SuperDealModel();
			#region API 에서 데이터 가져오기
			// 슈퍼딜 카테고리 정보
			List<SuperDealCategoryV2> category = new SuperDealApiBiz_Cache().GetSuperDealCategory();
			if (category == null || category.Count < 1)
			{
				category = new SuperDealApiBiz().GetSuperDealCategory();
				if (category == null) category = new List<SuperDealCategoryV2>();
			}
			model.Category = category;

			// 슈퍼딜 상품 가져오기
			string superDealCategoryCode = ShopGdlcCd;
			if (!String.IsNullOrEmpty(ShopGdmcCd))
			{
				superDealCategoryCode = ShopGdmcCd;
			}

			List<SuperDealItem> items = new SuperDealApiBiz_Cache().GetSuperDealItems(superDealCategoryCode, PageAttr.FromWhere);
			if (items == null || items.Count < 1)
			{
				items = new SuperDealApiBiz().GetSuperDealItems(ShopGdlcCd, PageAttr.FromWhere);
				if (items == null)
				{
					items = new List<SuperDealItem>();
				}
			}

			model.Items = items;

			if (ShopGdlcCd == "000000000" && items != null && items.Count > 0)
			{
				SuperDealItem itemTypeB = items.Where(i => i.Type == 'B').First();

				model.Items = items.Where(i => i.ItemType == "superdeal" || i.ItemType == "todayspecial").ToList();//items.GetRange(0, indexSevenDealStart);

			}
			
			#endregion

			return View(model);
		}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Biz.Search;
using GMKT.GMobile.Util;
using GMKT.Web.Context;
using GMKT.GMobile.Constant;

namespace GMKT.GMobile.Web.Controllers
{
	public class BizOnController : GMobileControllerBase
    {
		private const int MAX_LP_MILEAGE_ITEMS_COUNT = 4;
        //
        // GET: /BizOn/

        public ActionResult Index()
        {
			ViewBag.HeaderTitle = "BIZ ON";
			ViewBag.Title = "BizOn - G마켓 모바일";
			BizOnHome apiresult = new BizOnHome();

			apiresult = new BizOnApiBiz_Cache().GetHome();

			if (apiresult == null)
			{
				apiresult = new BizOnApiBiz().GetHome();
				if (apiresult == null)
				{
					apiresult = new BizOnHome();
				}
			}

			BizOnHomeModel model = new BizOnHomeModel();

			model = ConvertToBizOnHomeModel(apiresult);

			return View(model);
        }

		public ActionResult Best()
		{
			ViewBag.HeaderTitle = "BIZ ON";
			ViewBag.Title = "BizOn Best - G마켓 모바일";
			List<BizOnItem> model = new List<BizOnItem>();

			model = new BizOnApiBiz_Cache().GetBizOnBest();

			if (model == null || model.Count == 0)
			{
				model = new BizOnApiBiz().GetBizOnBest();
				if (model == null || model.Count == 0)
				{
					model = new List<BizOnItem>();
				}
			}

			return View(model);
		}

		public ActionResult List(string bizOnCategoryCode = "400000090", int pageNo = 1, int pageSize = 20)
		{
			ViewBag.HeaderTitle = "BIZ ON";
			ViewBag.Title = "BizOn Category List - G마켓 모바일";
			ViewBag.pageNo = pageNo;
			ViewBag.pageSize = pageSize;
			ViewBag.getOnlyNormalItems = "N";
			ViewBag.mainYN = "Y";

			BizOnLPModel model = new BizOnLPModel();
			BizOnCategoryT categoryInfo = new BizOnApiBiz().GetBizOnCategoryInfo(bizOnCategoryCode);

			ViewBag.bizOnCategoryCode = bizOnCategoryCode;
			ViewBag.level = categoryInfo.Type;

			
			#region 카테고리 정보
			model = new BizOnApiBiz().GetBizOnCategoryModel(categoryInfo);
			if (model == null) 
			{
				model = new BizOnLPModel();
			}

			model.PageNo = pageNo;
			model.PageSize = pageSize;
			#endregion

			return View(model);
		}

		public ActionResult Guide(string type)
		{
			ViewBag.HeaderTitle = "BIZ ON";
			if (type != null && type != "")
			{
				ViewBag.Type = type.ToLower();
			}
			else
			{
				ViewBag.Type = "";
			}

			return View();
		}

		public ActionResult lstest()
		{
			ViewBag.Host = Request.Url.Host;
			return View();
		}

		[HttpPost]
		public ActionResult Search(string bizOnCategoryCode = "400000090", string mainYN = "N", string getSCategoryInfo = "N", string getOnlyNormalItems = "N", int pageNo = 1, int pageSize = 20)
		{
			BizOnLPModel model = new BizOnLPModel();

			string categoryCode = bizOnCategoryCode;
			BizOnCategoryT categoryInfo = new BizOnApiBiz().GetBizOnCategoryInfo(bizOnCategoryCode);

			#region 카테고리 정보
			if (mainYN.Equals("Y"))
			{
				model = new BizOnApiBiz().GetBizOnCategoryModel(categoryInfo);
			}
			else if (getSCategoryInfo.Equals("Y"))
			{
				List<BizOnCategoryT> sCategoryList = new BizOnApiBiz_Cache().GetBizOnSCategoryList(categoryCode);
				if (sCategoryList == null || sCategoryList.Count == 0)
				{
					sCategoryList = new BizOnApiBiz().GetBizOnSCategoryList(categoryCode);
				}
				if (sCategoryList != null && sCategoryList.Count > 0)
				{
					model.SCategoryFirst = sCategoryList[0];
					model.SCategoryList = sCategoryList;
				}
			}
			#endregion

			#region 상품 정보
			bool isAdult = BizUtil.IsAdultUser(GMobileWebContext.Current.UserProfile.CustNo);

			if (getOnlyNormalItems.Equals("N") && pageNo == 1)
			{
				List<CPPLPSRPItemModel> mileageItmes = new BizOnApiBiz().GetBizOnMileageItems(categoryCode, "", 1, MAX_LP_MILEAGE_ITEMS_COUNT);

				if (mileageItmes != null && mileageItmes.Count > 0)
				{
					for (int i = 0; i < mileageItmes.Count; i++)
					{
						SetItemImageNLink(mileageItmes[i], isAdult);
						model.BizOnItems.Add(mileageItmes[i]);
					}
					//model.BizOnItems = this.ConvertToSearchViewModel(mileageItmes, isAdult);
				}
			}


			// GMApi Search
			SearchRequest request = new SearchRequest();
			request.pageNo = pageNo;
			request.pageSize = pageSize;
			request.shopCategory = categoryCode;
			request.sortType = "RANK_POINT_DESC";
			SRPResultModel apiResultModel = new SearchAPIBiz().PostSearchItem(request, gmktUserProfile.UserInfoString);

			if (apiResultModel != null)
			{
				if (apiResultModel.Item != null && apiResultModel.Item.Count > 0)
				{
					for (int i = 0; i < apiResultModel.Item.Count; i++)
					{
						SetItemImageNLink(apiResultModel.Item[i], isAdult);
						model.Items.Add(apiResultModel.Item[i]);
					}
					model.TotalGoodsCount = apiResultModel.TotalGoodsCount;
				}
			}
			#endregion

			model.PageNo = pageNo;
			model.PageSize = pageSize;


			return Json(model);
		}

		#region Converter
		private BizOnHomeModel ConvertToBizOnHomeModel(BizOnHome apiresult)
		{
			BizOnHomeModel result = new BizOnHomeModel();
			if (apiresult != null)
			{
				result.Banners = apiresult.Banners;
				result.SpecialPriceSection = apiresult.SpecialPriceSection;
				result.BestSection = apiresult.BestSection;
				result.KeywordSection = apiresult.KeywordSection;

				result.CategoryGroups = new List<BizOnCategoryGroupModel>();

				if (apiresult.CategoryGroups != null && apiresult.CategoryGroups.Count > 0)
				{
					foreach (BizOnCategoryGroupT group in apiresult.CategoryGroups)
					{
						BizOnCategoryGroupModel categroupModel = new BizOnCategoryGroupModel();
						categroupModel.GroupCd = group.GroupCd;
						categroupModel.GroupNm = group.GroupNm;
						categroupModel.ImageUrl = group.ImageUrl;
						categroupModel.GroupClass = GetGdlcClass(group.GroupNm);
						categroupModel.GdlcCds = group.GdlcCds;
						//categroupModel.GdlcCds = new List<BizOnGdlcModel>();
						//if (group.GdlcCds != null && group.GdlcCds.Count > 0)
						//{
						//    foreach (string strgdlc in group.GdlcCds)
						//    {
						//        BizOnGdlcModel gdlcModel = new BizOnGdlcModel();
						//        string[] arrGdlcTemp = strgdlc.Split('|');
						//        if (arrGdlcTemp.Length > 0 && arrGdlcTemp.Length >= 2)
						//        {
						//            gdlcModel.GdlcNm = arrGdlcTemp[0];
						//            gdlcModel.GdlcCd = arrGdlcTemp[1];
						//        }
						//        categroupModel.GdlcCds.Add(gdlcModel);
						//    }
						//}
						result.CategoryGroups.Add(categroupModel);
					}
				}
			}
			return result;
		}

		private List<CPPLPSRPItemModel> ConvertToSearchViewModel(List<CPPLPSRPItemModel> mileageItmes, bool isAdult)
		{
			List<CPPLPSRPItemModel> result = new List<CPPLPSRPItemModel>();

			for (int mIndex = 0; mIndex < mileageItmes.Count; mIndex++)
			{
				mileageItmes[mIndex].ImageURL = BizUtil.GetGoodsImagePath(mileageItmes[mIndex].GoodsCode, "M");
				mileageItmes[mIndex].OriginalPrice = mileageItmes[mIndex].SellPrice.ToString("N0");
				mileageItmes[mIndex].SalePrice = this.GetSalePrice(mileageItmes[mIndex]);

				if (String.IsNullOrEmpty(mileageItmes[mIndex].LinkURL))
				{
					mileageItmes[mIndex].LinkURL = Urls.MItemWebURL + "/Item?goodscode=" + mileageItmes[mIndex].GoodsCode;
				}
				SetItemImageNLink(mileageItmes[mIndex], isAdult);
				this.GetDeliveryType(mileageItmes[mIndex].Delivery.DeliveryCode, mileageItmes[mIndex].Delivery.DeliveryText);

				string deliveryText = this.GetDeliveryText(mileageItmes[mIndex].IsTpl,
											mileageItmes[mIndex].Delivery.DeliveryCode, mileageItmes[mIndex].Delivery.DeliveryFee,
											mileageItmes[mIndex].Delivery.DeliveryInfo);

				if (!string.IsNullOrEmpty(deliveryText))
				{
					mileageItmes[mIndex].Delivery.ShowDeliveryInfo = true;
					mileageItmes[mIndex].Delivery.DeliveryText = deliveryText;
					mileageItmes[mIndex].Delivery.DeliveryType = this.GetDeliveryType(mileageItmes[mIndex].Delivery.DeliveryCode, deliveryText);
				}
				else
				{
					// TL, A, M, R, D 코드가 아닌 (예를들면, 디지털쿠폰) 상품일 경우
					mileageItmes[mIndex].Delivery.DeliveryText = mileageItmes[mIndex].Delivery.DeliveryInfo;
					mileageItmes[mIndex].Delivery.DeliveryType = "GRAY";
				}

				result.Add(mileageItmes[mIndex]);
			}

			return result;

		}
		#endregion

		#region Util
		private string GetSalePrice(CPPLPSRPItemModel item)
		{
			decimal adCouponPrice;

			if (decimal.TryParse(item.AdCouponPrice, out adCouponPrice) && (adCouponPrice > 10))
			{
				return adCouponPrice.ToString("N0");
			}
			else
			{
				//휴대폰 상품 중분류 하드코딩(BC 8393)
				List<string> joinItemCategories = new List<string> { "200000801", "200001255", "200001256", "200000800", "200001253", "200001254", "200000802", "200001257", "200001258", "200002090", "200001152" };
#if DEBUG
				//Rental 중분류 하드코딩(BC 8393)
				joinItemCategories.Add("200002521");
#else
				joinItemCategories.Add("200002528");
#endif

				if (joinItemCategories.Contains(item.MCode))
				{
					return "가입상품";
				}
				else
				{
					return "무료";
				}
			}
		}

		private string GetDeliveryText(string isTpl, string deliveryCode, string deliveryFee, string deliveryInfo)
		{
			if ("TL".Equals(isTpl))
				return "스마트배송";
			else if ("A".Equals(deliveryCode))
				return "무료배송";
			else if ("M".Equals(deliveryCode))
			{
				if (deliveryInfo != null && deliveryInfo.Equals("무료"))
				{
					return "무료배송";
				}
				else
				{
					return "조건부무료";
				}
			}
			else if ("R".Equals(deliveryCode))
			{
				if (deliveryInfo != null && deliveryInfo.Equals("무료"))
				{
					return "무료배송";
				}
				else
				{
					return "배송비" + deliveryFee + "원";
				}
			}
			else if ("D".Equals(deliveryCode))
				return "배송비 별도표기";
			else if (deliveryInfo != null && (deliveryInfo.IndexOf("방문사용") >= 0 ||
				deliveryInfo.IndexOf("디지털쿠폰") >= 0))
				return "방문사용";
			else
				return "";
		}

		private string GetDeliveryType(string deliveryCode, string deliveryText)
		{
			if ("스마트배송".Equals(deliveryText))
				return "SMART";
			else if ("무료배송".Equals(deliveryText))
				return "BLUE";
			else if ("방문사용".Equals(deliveryText) || "M".Equals(deliveryCode) ||
				"R".Equals(deliveryCode) || "D".Equals(deliveryCode))
				return "GRAY";
			else
				return "";
		}

		protected void SetItemImageNLink(CPPLPSRPItemModel item, bool isAdult)
		{
			if (item.IsAdult == false) return;

			if (PageAttr.IsAdultUse) return;
			
			item.ImageURL = Urls.MobileImageUrlV2 + Const.ADULT_IMAGE_210.Replace("images/", string.Empty);

			if (PageAttr.IsLogin)
			{
				item.MoreImageCnt = 1;
				if(!isAdult) item.LinkURL = "javascript:alert('죄송합니다.\n성인만 구매 가능한 상품입니다.');";
			}
			else
			{
				//item.LinkURL = ConstDef._GMW_LOGIN_AD_URL + "?rtnurl=" + HttpUtility.UrlEncode(item.LinkURL) + "&adultUseLoinCheck=Y";
				item.LinkURL = Urls.LoginUrl + "?rtnurl=" + HttpUtility.UrlEncode(item.LinkURL) + "&adultUseLoinCheck=Y";
			}
		}

		private string GetGdlcClass(string gdlcNm)
		{
			String result = String.Empty;

			switch(gdlcNm){
				case "회사" :
					result = "office";
					break;
				case "병원" :
					result = "hospital";
					break;
				case "학교" :
					result = "school";
					break;
				case "복지시설" :
					result = "welfare";
					break;
				case "레져시설" :
					result = "leisure";
					break;
				case "레저시설":
					result = "leisure";
					break;
				case "숙박시설" :
					result = "stay";
					break;
				default:
					result = "office";
					break;
			}
			return result;
		}
		#endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Util;
using GMKT.Web.Context;
using GMKT.GMobile.Constant;

namespace GMKT.GMobile.Web.Controllers
{
	public class BestController : GMobileControllerBase
    {
		public ActionResult Index(string code = "G00")
		{
			PageAttr.IsMain = true;
			ViewBag.Title = "베스트 - G마켓 모바일";
			SetHomeTabName("베스트");
			PageAttr.HeaderType = CommonData.HeaderTypeEnum.Normal;

			BestModel model = GetDefaultBestModel();

			#region API 에서 데이터 가져오기
			Best100Main best100Data = new Best100Main();
			best100Data = new BestItemApiBiz_Cache().GetBest100Main(code);

			model.GroupCategory = new List<Best100GroupCateogyDetail>();
			model.BestItems = new List<SearchItemModel>();
			if (best100Data != null)
			{
				if (best100Data.GroupCategoryList != null)
				{
					model.GroupCategory = best100Data.GroupCategoryList;
				}

				if (best100Data.SearchModel != null)
				{
					if (best100Data.SearchModel.Items != null)
					{
						model.BestItems = best100Data.SearchModel.Items;
					}
				}
			}
			else
			{
				best100Data = new BestItemApiBiz().GetBest100Main(code);
				if (best100Data != null)
				{
					if (best100Data.GroupCategoryList != null)
					{
						model.GroupCategory = best100Data.GroupCategoryList;
					}

					if (best100Data.SearchModel != null)
					{
						if (best100Data.SearchModel.Items != null)
						{
							model.BestItems = best100Data.SearchModel.Items;
						}
					}
				}
			}
			#endregion

			#region 페이스북 공유하기
			string faceBookImage = "";
			if (model.BestItems != null && model.BestItems.Count > 0)
			{
				if (model.BestItems[0] != null)
				{
					faceBookImage = String.IsNullOrEmpty(model.BestItems[0].ImageUrl) ? "" : model.BestItems[0].ImageUrl;
				}
			}
			PageAttr.FbTitle = "G마켓 베스트100";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Best";
			PageAttr.FbTagImage = faceBookImage;
			PageAttr.FbTagDescription = "G마켓 베스트100";
			#endregion

            /* Landing Banner */
            new LandingBannerSetter(Request).Set(model, PageAttr.IsApp);
			model.Code = code;
			return View(model);
		}

		[HttpPost]
		public JsonResult GetBestItems(string code)
		{
			List<SearchItemModel> result = new List<SearchItemModel>();

			Best100Main best100Item = new BestItemApiBiz_Cache().GetBest100Main(code);
			if (best100Item != null && best100Item.SearchModel != null && best100Item.SearchModel.Items != null)
			{
				result = best100Item.SearchModel.Items;
			}
			return Json(result);
		}

		public ActionResult BestSellerList(string type = "G", string code = "G01", string listView = "H")
		{
			ViewBag.Title = "베스트 - G마켓 모바일";
			BestModel model = GetDefaultBestModel();

			if (type == "G") // 그룹Best100
			{
				if (String.IsNullOrEmpty(code) || code.Length != 3)
				{
					code = "G01";
				}

				// BEST100 그룹 상품 정보
				SearchResultModel gBestItemsResult = new BestItemApiBiz().GetBest100GroupItems(code, 1, 100);
				if (gBestItemsResult == null)
				{
					gBestItemsResult = new SearchResultModel();
					gBestItemsResult.Items = new List<SearchItemModel>();
				}

				model.Code = code;
				model.BestType = type;
				model.ListName = String.IsNullOrEmpty(gBestItemsResult.ListName) == true ? "" : gBestItemsResult.ListName + " 그룹 베스트";
				model.ListViewType = listView;
				model.BestItems = gBestItemsResult.Items;
				
				// 상품명 필터링
				if (model.BestItems != null)
				{
					foreach (var eachItem in model.BestItems)
					{
						eachItem.GoodsName = ValidationUtil.GetFilteredGoodsName(eachItem.GoodsName);
					}
				}
			}
			else // 카테고리Best100
			{
				List<SearchItemModel> cBestItems;
				List<CategoryInfo> searchResultCategory;
				SearchResultModel gBestItemsResult = new SearchResultModel();

				if (String.IsNullOrEmpty(code))
				{
					cBestItems = new List<SearchItemModel>();
					searchResultCategory = new List<CategoryInfo>();
					code = "";
				}
				else
				{
					// BEST100 카테고리 상품 정보
					gBestItemsResult = new BestItemApiBiz().GetBest100CategoryItems(code, 1, 100);
					if (gBestItemsResult == null)
					{
						gBestItemsResult = new SearchResultModel();
						gBestItemsResult.Items = new List<SearchItemModel>();
					}
					cBestItems = gBestItemsResult.Items;

					searchResultCategory = new CategoryBiz().GetCategory(type, code);
					if (cBestItems == null)
					{
						cBestItems = new List<SearchItemModel>();
					}
					if (searchResultCategory == null)
					{
						searchResultCategory = new List<CategoryInfo>();
					}
				}

				model.BestType = type;
				model.Code = code;
				model.ListName = String.IsNullOrEmpty(gBestItemsResult.ListName) == true ? "" : gBestItemsResult.ListName + " 카테고리 베스트";
				model.ListViewType = listView;
				model.BestItems = cBestItems;
				model.SearchResultCategory = searchResultCategory.OrderBy(c => c.Name).ToList();

				// 상품명 필터링
				if (model.BestItems != null)
				{
					foreach (var eachItem in model.BestItems)
					{
						eachItem.GoodsName = ValidationUtil.GetFilteredGoodsName(eachItem.GoodsName);
					}
				}
			}

			return View(model);
		}

		[NonAction]
		private BestModel GetDefaultBestModel()
		{
			BestModel model = new BestModel();

			#region 성인 상품 처리 위한 플래그 설정
			// 성인상품이면 19금 이미지 보여줌
			// ShowAdultImage = true;

			// 만약 성인 상품인데 로그인 안했으면 무조건 로그인 url로 보내줘야 함.
			// 성인상품인데 미성년자인 경우 VIP 진입 못하게 하고 에러 메시지 보여준다.
			// ShowAdultOnlyMessage = true;

			model.ShowAdultImage = true;
			model.ShowAdultOnlyMessage = false;

			if(PageAttr.IsAdultUse) //성인인증 한 경우
			{
				model.ShowAdultImage = false;
			}
			else if(PageAttr.IsLogin && !BizUtil.IsAdultUser(GMobileWebContext.Current.UserProfile.CustNo)) // 미성년자
			{
				model.ShowAdultOnlyMessage = true;
			}			
			#endregion

			return model;
		}
    }
}

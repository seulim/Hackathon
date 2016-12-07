using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Arche.Data.Voyager;
using GMobile.Data.Diver;
using GMobile.Data.Voyager;
using GMobile.Service.Search;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Util;
using System.Text;
using GMKT.GMobile.Data;
using System.Web.Caching;
using GMKT.GMobile.Biz.Category;
using GMKT.GMobile.Biz.Search;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Web.Controllers
{
	public class CategoryController : GMobileControllerBase
	{
		private const string SELLERADITEMS = "SellerAdItems";
		private bool UseSearchApi = true;

		public ActionResult List(string lcId = "", string mcId = "", string scId = "", int pageSize = 50, int pageNo = 1
														 , string primeKeyword = "", int minPrice = 0, int maxPrice = 0, string moreKeyword = ""
														 , string sortType = ""
														 , string isShippingFree = "N", string isMileage = "N", string isDiscount = "N", string isStamp = "N"
														 , string listType = "H", string sellCustNo = "", string mClassSeq = "", string sClassSeq = ""
														 , string valueId = "", string valueIdName = "")
		{


			if(false == string.IsNullOrEmpty(scId) && string.IsNullOrEmpty(mcId))
			{
				mcId = CategoryUtil.GetParentCategoryCode(scId);
			}

			if(false == string.IsNullOrEmpty(mcId) && string.IsNullOrEmpty(lcId))
			{
				lcId = CategoryUtil.GetParentCategoryCode(mcId);
			}

			string lCategoryCode = lcId;
			string mCategoryCode = mcId;
			string sCategoryCode = scId;

			if(!String.IsNullOrEmpty(mCategoryCode))
			{
				lCategoryCode = string.Empty;
			}

			if(!String.IsNullOrEmpty(sCategoryCode))
			{
				lCategoryCode = string.Empty;
				mCategoryCode = string.Empty;
			}

			


			ViewBag.menuName = "LP";
			ViewBag.Title = "검색결과: " + (primeKeyword == "" ? "" : primeKeyword) + " - G마켓 모바일";
			ViewBag.primeKeyword = primeKeyword;
			ViewBag.moreKeyword = moreKeyword;
			ViewBag.lcId = lCategoryCode;
			ViewBag.mcId = mCategoryCode;
			ViewBag.scId = sCategoryCode;
			ViewBag.lcIdName = CategoryUtil.GetCategoryName(lcId);
			ViewBag.mcIdName = CategoryUtil.GetCategoryName(mcId);
			ViewBag.scIdName = CategoryUtil.GetCategoryName(scId);
			ViewBag.minPrice = minPrice;
			ViewBag.maxPrice = maxPrice;
			ViewBag.sortType = sortType;
			ViewBag.sellCustNo = sellCustNo;
			ViewBag.pageNo = pageNo;
			ViewBag.pageSize = pageSize;
			ViewBag.mClassSeq = mClassSeq;
			ViewBag.sClassSeq = sClassSeq;
			ViewBag.valueId = valueId;
			ViewBag.valueIdName = valueIdName;
			ViewBag.SponsorLinkChannel = "m_gmkt.ch2";
			ViewBag.SpnosorLinkChannelCode = "716500009";

			ViewBag.HeaderTitle = ViewBag.lcIdName;
			if(!String.IsNullOrEmpty(ViewBag.mcIdName))
			{
				ViewBag.HeaderTitle = ViewBag.mcIdName;
			}

			if(!String.IsNullOrEmpty(ViewBag.scIdName))
			{
				ViewBag.HeaderTitle = ViewBag.scIdName;
			}

			//return View("~/Views/Search/Search.cshtml");

			/* Landing Banner */
			LandingBannerModel landingBannermodel = new LandingBannerModel();
			new LandingBannerSetter(Request).Set(landingBannermodel, PageAttr.IsApp);

			return View("~/Views/Search/Search.cshtml", landingBannermodel);
		}

        /// <summary>
        /// Landing Banner Inner Class Model
        /// </summary>
        public class LandingBannerModel : ILandingBannerModel
        {
            public ILandingBannerEntityT LandingBanner { get; set; }
            public ICampaign Campaign { get; set; }
        }

		public ActionResult Category(string lcId = "100000003", string mcId = "", string scId = "", int pageSize = 10, int pageNo = 1
												 , string keyword = "", int minPrice = 0, int maxPrice = 0, string scKeyword = ""
												 , string sortType = "focus_rank_desc"
												 , string isFeeFree = "N", string isMileage = "N", string isDiscount = "N", string isStamp = "N"
												 , string listType = "H")
		{
			
			CPPCategoryBest100Model model = new SearchAPIBiz().GetCPPCategory(lcId);
			ViewBag.HeaderTitle = model.CPPTitle;
			model.lcId = lcId;

            //return View("Category", model);
            /* Class 확장 */
            return View("Category", new LandingBannerSetter(Request).Set(new CPPCategoryBest100ModelEx(model), PageAttr.IsApp));
		}

        /// <summary>
        /// CPPCategoryBest100Model GMKT.GMobile.Data 에 위치.
        /// CPPCategoryBest100Model 확장 클래스
        /// </summary>
        public class CPPCategoryBest100ModelEx : CPPCategoryBest100Model, ILandingBannerModel
        {
            public CPPCategoryBest100ModelEx(CPPCategoryBest100Model model)
            {
                this.Best100Items = model.Best100Items;
                this.CategoryList = model.CategoryList;
                this.CPPTitle = model.CPPTitle;
                this.lcId = model.lcId;
            }

            public ILandingBannerEntityT LandingBanner { get; set; }

            public ICampaign Campaign { get; set; }
        }

		public ActionResult GmarketBest(string lcId = "100000003")
		{
			ViewBag.HeaderTitle = CategoryUtil.GetCategoryName( lcId );
			List<SearchItemModel> model = new BestItemApiBiz().GetGmarketBest100Items(lcId, 1, 100);
			ViewBag.lcId = lcId;
			return View(model);
		}

		public ActionResult SpecialShopList(string lcId = "100000003", int pageNo = 1, int pageSize = 30)
		{
			GMKT.GMobile.Data.Search.SpecialShopModel model = new Data.Search.SpecialShopModel();
			model.pageNo = pageNo;
			model.pageSize = pageSize;
			model.lcId = lcId;
			ViewBag.HeaderTitle = CategoryUtil.GetCategoryName(lcId);

			return View(model);
		}

		public ActionResult SpecialShopListJson(string lcId, int pageNo, int pageSize)
		{
			GMKT.GMobile.Data.Search.SpecialShopModel model = new CategoryAPIBiz().GetSpecialShopList(pageNo, pageSize, lcId);

			model.pageNo = pageNo;
			model.pageSize = pageSize;
			model.lcId = lcId;

			if (model.Items != null && model.Items.Count > 0)
			{
				model.pageCount = model.Items[0].pageCount;
				model.totalCount = model.Items[0].totalCount;
			}

			return Json(model, JsonRequestBehavior.AllowGet);
		}

		private CategoryItemT[] ConvertToCategoryItem(List<SearchGoods> bestGoods)
		{
			List<CategoryItemT> result = new List<CategoryItemT>();

			if (bestGoods == null || bestGoods.Count < 1)
			{
				return result.ToArray();
			}

			foreach (SearchGoods best in bestGoods)
			{
				CategoryItemT item = new CategoryItemT();

				item.AdultYN = best.adult_yn.ToUpper() == "Y" ? true : false;
				item.BuyerMlRate = best.mileage;
				item.DealPrice = best.deal_price;
				item.Delivery.DeliveryInfo = best.delivery_info;
				item.Discount.DiscountPrice = best.sell_price - best.disp_price;
				item.GdNo = Convert.ToInt32(best.gd_no);
				item.GStampCnt = best.gstamp;
				item.Name = best.gd_nm;
				item.SellPrice = best.sell_price;
				//item.TradeWay = best.trad_way;

				result.Add(item);
			}

			return result.ToArray();
		}

		private CategoryItemT[] ConvertToCategoryItem(List<SearchItemModel> bestGoods)
		{
			List<CategoryItemT> result = new List<CategoryItemT>();

			if (bestGoods == null || bestGoods.Count < 1)
			{
				return result.ToArray();
			}

			foreach (SearchItemModel best in bestGoods)
			{
				CategoryItemT item = new CategoryItemT();

				item.AdultYN = best.IsAdult;
				//item.BuyerMlRate = best.mileage;
				//item.DealPrice = best.deal_price;
				item.Delivery.DeliveryInfo = best.DeliveryInfo;
				item.Discount.DiscountPrice = best.SellPrice - best.DispPrice;
				item.GdNo = Convert.ToInt32(best.GoodsCode);
				item.GStampCnt = best.GStampCount;
				item.Name = best.GoodsName;
				item.SellPrice = best.SellPrice;
				//item.TradeWay = best.trad_way;

				result.Add(item);
			}

			return result.ToArray();
		}

		public ActionResult ParterList(string ppsellerno = "")
		{
			CategorySearchModel model = new CategorySearchModel { };
			List<PartnerT> list = new PartnerBiz().GetPartnerListFromDB(ppsellerno);
			if (list != null && list.Count > 0)
				model.PartnerList = list;

			return View(model);
		}

		[NonAction]
		public QueryResult GetCategoryItem(string gdlcCd, string gdmcCd, string gdscCd, int rowCount, int pageNo
														 , int minPrice, int maxPrice, string keyword, string sortType
														 , string isFeeFree, string isMileage, string isDiscount, string isStamp,
														 string sellCustNo,
														 out int categoryLevel, string[] items = null)
		{


			string categoryCode = string.Empty;
			string CategoryGroupField = "LCode";
			categoryLevel = 0;

			#region ::: Set Category
			if (gdlcCd == "" && gdmcCd == "" && gdscCd == "")
			{
				CategoryGroupField = "LCode";
			}
			else
			{
				if (gdlcCd != "" && gdmcCd == "" && gdscCd == "")
				{
					categoryLevel = 1;
					string[] arrGdlcTemp = gdlcCd.Split(',');
					if (arrGdlcTemp.Length > 1)
						CategoryGroupField = "LCode";
					else
						CategoryGroupField = "MCode";

					categoryCode = gdlcCd.Replace(",", "|");
				}
				else if (gdlcCd != "" && gdmcCd != "" && gdscCd == "")		// 중분류
				{
					categoryLevel = 2;
					string[] arrGdmcTemp = gdmcCd.Split(',');
					if (arrGdmcTemp.Length > 1) // 중분류 복수선택
					{
						CategoryGroupField = "MCode";
					}
					else
					{
						CategoryGroupField = "SCode";
					}

					categoryCode = gdmcCd.Replace(",", "|");
				}
				else if (gdlcCd != "" && gdmcCd != "" && gdscCd != "")
				{
					categoryLevel = 3;
					CategoryGroupField = "SCode";
					categoryCode = gdscCd.Replace(",", "|");
				}
			}
			#endregion

			#region ::: Set Search Option
			ItemFilter filter = new ItemFilter();

			if (categoryLevel > 0)
				filter.CategoryOrList = categoryCode.Split(',').ToList();

			if (keyword.Length > 0)
				filter.ItemName = keyword;

			filter.SellCustNo = sellCustNo;

			// 가격 필터링
			if (minPrice > 0 && maxPrice > 0)
			{
				filter.SellPrice.Min = minPrice;
				filter.SellPrice.Max = maxPrice;
			}
			// 배송비 무료
			if ("Y".Equals(isFeeFree))
				filter.DeliveryFeeCondition = DeliveryFeeCondition.FreeOfCharge;
			// 마일리지 여부
			if ("Y".Equals(isMileage))
				filter.BuyerMlRate.Min = 1;
			// 할인 여부
			if ("Y".Equals(isDiscount))
				filter.DiscountPriceYN = true;
			// 스탬프 여부
			if ("Y".Equals(isStamp))
				filter.StickerCnt.Min = 1;
			/*
			if (sellerLoginID.Length > 0)
				filter.SellLoginId = sellerLoginID.ToLower();
			*/

			if (items != null)
			{
				filter.ItemNoList = items.ToList();
			}
			#endregion


			#region ::: Set Sort Option
			SortCollection sc = new SortCollection();
			switch (sortType.ToLower())
			{
				case "focus_rank_desc":
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "rank_point_desc":
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "sell_point_desc":
					sc.Add(Sort.Create("SellPointInfo", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "disc_price_desc":
					sc.Add(Sort.Create("MACRO(DiscountPrice)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					break;
				case "sell_price_asc":
					sc.Add(Sort.Create("MACRO(SellPriceSrch)", SortOrder.Increasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "sell_price_desc":
					sc.Add(Sort.Create("MACRO(SellPriceSrch)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "new":
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "con_point_desc":
					sc.Add(Sort.Create("ConPoint", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "premium_rank_desc":
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				default:
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
			}
			#endregion

			#region :::: Set Groupby
			HistogramQueryCollection hqc = new HistogramQueryCollection();
			hqc.Add(new HistogramQuery(CategoryGroupField, HistogramSortOrder.Decrc));
			#endregion

			if (items == null)
			{
				return new CategoryItemBiz().GetQueryResult(filter, (pageNo - 1) * rowCount, rowCount, sc, hqc);
			}
			else
			{
				return new CategoryItemBiz().GetQueryResult(filter, 0, items.Length, sc, hqc);
			}
		}

		[NonAction]
		public SpecialShoppingList GetSearchPlanList(int pageNo, int pageSize, string gdlcCd)
		{
			return new SearchListBiz().GetSearchSpecialShopList(pageNo, pageSize, gdlcCd, "");
		}


		private CategoryItemT[] GetSellerAdItems(string mNo, string lcId, string mcId, string scId
													, int rowCount, int pageNo, int minPrice, int maxPrice
													, string keyword, string sortType, string isFeeFree, string isMileage
													, string isDiscount, string isStamp, string sellCustNo)
		{
			// 광고상품 가져오기
			List<SellerAdT> itemList = new SellerAdBiz().GetSellerAdListFromDB(mNo, mcId, scId, "");

			var list = itemList.Select(row => row.GdNo).ToArray();

			if (list.Length == 0)
			{
				return new CategoryItemT[0];
			}

			int categoryLevel = 0;
			var queryResult = this.GetCategoryItem(lcId, mcId, scId, rowCount, pageNo
												, minPrice, maxPrice, keyword, sortType
												, isFeeFree, isMileage, isDiscount, isStamp,
												sellCustNo, out categoryLevel, list).Entities;

			List<CategoryItemT> adResult = new List<CategoryItemT>();

			if (queryResult == null)
			{
				queryResult = new CategoryItemT[0];
			}
			else
			{
				CategoryItemT[] categoryResult = (CategoryItemT[])queryResult;
				for (int i = 0; i < itemList.Count; i++)
				{
					for (int j = 0; j < categoryResult.Length; j++)
					{
						if (itemList[i].GdNo == categoryResult[j].GdNo.ToString())
							adResult.Add(categoryResult[j]);
					}
				}
			}

			return adResult.ToArray();
		}

		public ActionResult LPSRPSmartClickAds(string channelID, string adType, string category, string keyword,
			string UA, string serverURL, string Ref)
		{
			string userIP = Request.ServerVariables["REMOTE_ADDR"];

			// IP 가 없을 때 Gmarket 공인 아이피를 보내줌.
			if (userIP == null || "".Equals(userIP))
				userIP = "112.169.64.19";

			List<LPSRPSmartClickBiz.SmartADT> itemList = new LPSRPSmartClickBiz().GetAds(channelID, adType, category,
					keyword, serverURL, PageAttr.IsAdultUse, PageAttr.IsLogin, gmktUserProfile.CustNo, UA, Ref, userIP);

			return Json(itemList, JsonRequestBehavior.AllowGet);
		}
	}
}
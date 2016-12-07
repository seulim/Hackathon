using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Arche.Data.Voyager;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Biz.Search;
using GMKT.GMobile.CommonData;
using GMKT.GMobile.Constant;
using GMKT.Web.Context;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Util;
using GMKT.GMobile.Web.Models;
using GMobile.Data.Voyager;
using GMobile.Service.Search;

namespace GMKT.GMobile.Web.Controllers
{
	public class SearchController : GMobileControllerBase
	{
		// topKeyword, keyword 는 예전 호출을 위해 남겨 둡니다. primeKeyword 로 바뀌었습니다.
		public ActionResult Search(SearchRequest input, string keyword, string topKeyword, string isRecommendKeyword = "N", string isBizOn = "N")
		{
			PageAttr.HasRPMScript = true;
			PageAttr.HeaderType = HeaderTypeEnum.Srp;

			if(!String.IsNullOrEmpty(topKeyword))
			{
				input.primeKeyword = topKeyword;
			}
			else if(!String.IsNullOrEmpty(keyword))
			{
				input.primeKeyword = keyword;
			}

			string totalKeyword = (input.primeKeyword + " " + input.moreKeyword).Trim();

			if(String.IsNullOrEmpty(input.primeKeyword))
			{
				return Redirect("/");
			}

			ViewBag.menuName = "SRP";
			ViewBag.Title = "검색결과: " + totalKeyword + " - G마켓 모바일";
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
			ViewBag.isBizOn = isBizOn;
			ViewBag.isRecommendKeyword =isRecommendKeyword;
			ViewBag.keywordList = input.keywordList;
			ViewBag.minPrice = 0;
			ViewBag.maxPrice = 0;
            ViewBag.SponsorLinkChannel = "m_gmkt.ch1";
            ViewBag.SpnosorLinkChannelCode = "716700001";

			/* Landing Banner */
			GMKT.GMobile.Web.Controllers.CategoryController.LandingBannerModel landingBannermodel = new GMKT.GMobile.Web.Controllers.CategoryController.LandingBannerModel();
			new LandingBannerSetter(Request).Set(landingBannermodel, PageAttr.IsApp);

			return View(landingBannermodel);
		}

		public ActionResult TireSmartFinder(string lClassSeq, string keyword, string lcId, string mcId, string scId)
		{
			if (false == string.IsNullOrEmpty(lClassSeq))
			{
				TireSmartFinderM model = new TireSmartFinderM();

				model.MClassList = new SmartFinderApiBiz().GetSmartFinderMClassList(lClassSeq);
				if (model.MClassList != null && model.MClassList.Count > 0)
				{
					if (false == string.IsNullOrEmpty(keyword))
					{
						model.Keyword = keyword;
					}
					else
					{
						model.Keyword = string.Empty;
					}

					return View(model);
				}
			}

			return Redirect("/");
		}

		[HttpPost]
		public JsonResult GetSmartFinderSClassList(string mClassSeq)
		{
			return Json(new SmartFinderApiBiz().GetSmartFinderSClassList(mClassSeq));
		}

		public static readonly string[] ALLOWED_CROSS_DOMAIN_CALL = { Urls.MobileWebUrl, Urls.MDeliveryUrl, Urls.MItemWebURL, Urls.MItemUrl, Urls.MMyGUrl};

		[HttpGet]
		public ActionResult SearchItem([System.Web.Http.FromBody] SearchRequest input)
		{

			SRPResultModel apiResultModel = new SRPResultModel();

			SearchItemRequest requestInput = new SearchItemRequest();
			requestInput.GoodsCodeList = input.gdNo;

			List<CPPLPSRPItemModel> response = new SearchAPIBiz().PostGetItemInfo(requestInput);

			if (response != null)
			{
				apiResultModel.Item = response;
			}

			if (ALLOWED_CROSS_DOMAIN_CALL.Contains(Request.Headers.Get("Origin")))
			{
				Response.AddHeader("Access-Control-Allow-Origin", Request.Headers.Get("Origin"));
				Response.AddHeader("Access-Control-Allow-Credentials", "true");
			}

			return Json(apiResultModel, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult SearchJson([System.Web.Http.FromBody] SearchRequest input, [System.Web.Http.FromBody] string isBizOn = "N")
		{
            if (!string.IsNullOrEmpty(input.primeKeyword))
            {
                input.primeKeyword = input.primeKeyword.Replace("&amp;", "&");
            }
			// GMApi Search
			SRPResultModel apiResultModel = new SearchAPIBiz().PostSearchItem(input, gmktUserProfile.UserInfoString);

			if(apiResultModel != null)
			{
				List<CPPLPSRPItemModel> bizOnItem = new List<CPPLPSRPItemModel>();
				if(isBizOn == "Y" && input.pageNo == 1)
				{
					bizOnItem = new BizOnApiBiz().GetBizOnMileageItems("", input.primeKeyword, input.pageNo, 4);
				}

				bool isAdult = BizUtil.IsAdultUser(GMobileWebContext.Current.UserProfile.CustNo);

				if(apiResultModel.Item != null)
				{
					for(int i = 0; i < apiResultModel.Item.Count; i++)
					{
						CPPLPSRPItemModel item = apiResultModel.Item[i];
						SetItemImageNLink(item, isAdult);
					}
				}

				if(bizOnItem != null && bizOnItem.Count > 0)
				{
					for(int i = 0; i < bizOnItem.Count; i++)
					{
						CPPLPSRPItemModel item = bizOnItem[i];
						if(i == 0)
						{
							item.AnotherTypeStart = true;
						}
						if(i == bizOnItem.Count - 1)
						{
							item.AnotherTypeEnd = true;
						}
						item.ListingItemGroup = ListingItemGroup.BizOn;

						SetItemImageNLink(item, isAdult);
					}

					if(apiResultModel.Item != null)
					{
						apiResultModel.Item.InsertRange(0, bizOnItem);
					}
				}

				if(apiResultModel.SmartClickAdList != null && apiResultModel.SmartClickAdList.Count > 0)
				{
					for(int i = 0; i < apiResultModel.SmartClickAdList.Count; i++)
					{
						CPPLPSRPItemModel item = apiResultModel.SmartClickAdList[i];
						SetItemImageNLink(item, isAdult);
					}
				}
			}

			return Json(apiResultModel);
		}

		[HttpPost]
		public ActionResult GetSmartBox(SearchRequest input)
		{
            if (!string.IsNullOrEmpty(input.primeKeyword))
            {
                input.primeKeyword = input.primeKeyword.Replace("&amp;", "&");
            }

			if(!input.searchEx.isDepartmentStore && String.IsNullOrEmpty( input.primeKeyword))
			{
				return Json(new object[0]);
			}
			SmartBoxModel result = new SearchAPIBiz().GetSmartBox(input,gmktUserProfile.UserInfoString);
			return Json(result);
		}

		[HttpPost]
		public ActionResult GetSmartBoxDetail(SearchRequest input)
		{
			if(input.searchEx != null && !input.searchEx.isDepartmentStore && String.IsNullOrEmpty(input.primeKeyword))
			{
				return Json(new object[0]);
			}
			List<SmartBoxTileEntity> result = new SearchAPIBiz().GetSmartBoxDetail(input);
			return Json(result);
		}

		[AllowCrossDomainCall("*")]
		[HttpPost]
		public ActionResult GetRecommedKeyword(string primeKeyword, bool needDiver = true)
		{
			if(String.IsNullOrEmpty(primeKeyword))
			{
				return Json(new object[0]);
			}
			List<RecommendKeywordModel> result = new SearchAPIBiz().GetRecommendKeyword(primeKeyword, needDiver);
			return Json(result);
		}

		[AllowCrossDomainCall("*")]
		[HttpPost]
		public JsonResult GetRecommedKeywordFromHeader(string primeKeyword, bool needDiver = true)
		{
			List<RecommendKeywordModel> result = new List<RecommendKeywordModel>();

			if (String.IsNullOrEmpty(primeKeyword) || primeKeyword.Length < 2)
			{
				return Json(new { success = false, data = result});
			}

			result = new SearchAPIBiz().GetRecommendKeyword(primeKeyword, needDiver);
			
			return Json(result);
		}

		[HttpPost]
		public ActionResult GetSmartClickItemsJson(string menuName = "SRP", string primeKeyword = "", string moreKeyword = "", 
			int pageNo = 1, int pageSize = 30, string lcId = "", string mcId = "", string scId = "", 
			string sellCustNo = "", string brandList = ""
			, int minPrice = 0, int maxPrice = 0, string sortType = ""
			, string isShippingFree = "N", string isMileage = "N", string isDiscount = "N", string isStamp = "N"
			, string isSmartDelivery = "N", int startRank = 1, int maxCount = 5, long keywordSeqNo = 0, List<SmartBoxTileEntity> tiles = null
			)
		{
			if(tiles != null && tiles.Where(T => T.Type == "C").Count() == 1)
			{
				var category = tiles.Where(T => T.Type == "C").ToList()[0];
				if(category.Code.StartsWith("1"))
				{
					lcId = category.Code;
				}
				else if(category.Code.StartsWith("2"))
				{
					mcId = category.Code;
				}
				else if(category.Code.StartsWith("3"))
				{
					scId = category.Code;
				}
			}
			List<CPPLPSRPItemModel> result = new List<CPPLPSRPItemModel>();

			result = new SearchAPIBiz().GetSmartClickItems(menuName, primeKeyword, moreKeyword,
				gmktUserProfile.UserInfoString, pageNo, pageSize, lcId, mcId, scId, sellCustNo, brandList,
				minPrice, maxPrice, sortType, isShippingFree, isMileage, isDiscount, isStamp, isSmartDelivery, startRank, maxCount, keywordSeqNo);

			if (result != null && result.Count > 0)
			{
				for (int i = 0; i < result.Count; i++)
				{
					CPPLPSRPItemModel item = result[i];					
					SetItemImageNLink(item, BizUtil.IsAdultUser(GMobileWebContext.Current.UserProfile.CustNo));
				}
			}

			return Json(result);
		}

		[HttpPost]
		public ActionResult GetPowerClickItemsJson(string menuName = "SRP", string primeKeyword = "", string moreKeyword = "", 
			int pageNo = 1, int pageSize = 30, string lcId = "", string mcId = "", string scId = "", 
			string sellCustNo = "", string brandList = ""
			, int minPrice = 0, int maxPrice = 0, string sortType = ""
			, string isShippingFree = "N", string isMileage = "N", string isDiscount = "N", string isStamp = "N", string isBrand = "N"
			, string isSmartDelivery = "N", int startRank = 1, int maxCount = 5, long keywordSeqNo = 0, List<SmartBoxTileEntity> tiles = null
			)
		{
			List<string> categories = new List<string>();
			List<string> brands = new List<string>();

			if (menuName == "LP")
			{
				if (!String.IsNullOrEmpty(scId))
				{
					categories.Add(scId);
				}
				else if (!String.IsNullOrEmpty(mcId))
				{
					categories.Add(mcId);
				}
				else if (!String.IsNullOrEmpty(lcId))
				{
					categories.Add(lcId);
				}

				if (!String.IsNullOrEmpty(brandList))
				{
					brands = brandList.Split(',').ToList();
				}
			}

			if(tiles != null && tiles.Where(T => T.Type == "C").Count() == 1)
			{
				var category = tiles.Where(T => T.Type == "C").ToList()[0];
				if(category.Code.StartsWith("1"))
				{
					lcId = category.Code;
				}
				else if(category.Code.StartsWith("2"))
				{
					mcId = category.Code;
				}
				else if(category.Code.StartsWith("3"))
				{
					scId = category.Code;
				}
			}
			if (menuName == "SRP" && tiles != null) //SRP인경우에만 Adv
			{
				foreach (var t in tiles)
				{
					if (t.Type == "C") //category
					{
						if (t.DetailTile != null)
						{
							if (t.DetailTile.Mtile != null && t.DetailTile.Mtile.Selected)
							{
								categories.Add(t.DetailTile.Mtile.Code);
								continue;
							}
							else if (t.DetailTile.Stile != null && t.DetailTile.Stile.Selected)
							{
								categories.Add(t.DetailTile.Stile.Code);
								continue;
							}
						}
						//middle, small이 아닐경우에만 추가됨
						categories.Add(t.Code);
					}
					else //brand
					{
						brands.Add(t.Code);
					}
				}
			}
			LPSRPBlockAdModel result = new LPSRPBlockAdModel();
			string keyword = string.IsNullOrEmpty(primeKeyword) ? moreKeyword : primeKeyword;

			if (categories.Count > 0 || brands.Count > 0)
			{
				result = new SearchAPIBiz().GetPowerClickItems(menuName, keyword, gmktUserProfile.UserInfoString, lcId, mcId, scId, startRank, keywordSeqNo, categories, brands, isBrand, isDiscount, isShippingFree, isMileage, isSmartDelivery, sellCustNo, minPrice, maxPrice);
			}
			else
			{
				result = new SearchAPIBiz().GetPowerClickItems(menuName, keyword, gmktUserProfile.UserInfoString, lcId, mcId, scId, startRank, keywordSeqNo, null, null, isBrand, isDiscount, isShippingFree, isMileage, isSmartDelivery, sellCustNo, minPrice, maxPrice);
			}

			if (result != null && result.Item != null && result.Item.Count > 0)
			{
				for (int i = 0; i < result.Item.Count; i++)
				{
					CPPLPSRPItemModel item = result.Item[i];					
					SetItemImageNLink(item, BizUtil.IsAdultUser(GMobileWebContext.Current.UserProfile.CustNo));
				}
			}

			return Json(result);
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
				item.LinkURL = Urls.MWebUrl + "/Login/login_mw.asp?rtnurl=" + HttpUtility.UrlEncode( item.LinkURL ) + "&adultUseLoinCheck=Y";
			}
		}



		[AllowCrossDomainCall("*")]
		public ActionResult AutoComplete(string keyword)
		{
			SuggestKeywordModel model = new SuggestKeywordModel();
			
			if (false == string.IsNullOrEmpty(keyword))
			{
				keyword = HttpUtility.UrlDecode(keyword);
				model.Keyword = keyword;

				SuggestionKeywordT suggestKeyword = new SuggestionKeywordBiz().GetSuggestionKeyword(keyword);
				if (suggestKeyword != null && suggestKeyword.suggestKeywordList != null)
				{
					model.SuggestKeywordList = suggestKeyword.suggestKeywordList;
				}
			}

			return View(model);
		}

		[NonAction]
		public QueryResult GetSearchItem(string keyword, int rowCount, int pageNo
														 , int minPrice, int maxPrice, string prevKeyword
														 , string gdlcCd, string gdmcCd, string gdscCd, string sortType
														 , string isFeeFree, string isMileage, string isDiscount, string isStamp, string sellCustNo, out int categoryLevel, out string queryString, string[] items = null)
		{


			string categoryCode = string.Empty;
			string CategoryGroupField = "LCode";
			categoryLevel = 0;
			queryString = "";

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

			if (prevKeyword.Length > 0)
				filter.ItemName = keyword + " " + prevKeyword;
			else
				filter.ItemName = keyword;

			filter.SellCustNo = sellCustNo;

			/*
			if (exceptKeyword.Length > 0)
				filter.ExceptKeyword = exceptKeyword;
			*/

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

			#region ::: 카테고리 필터 설정
			List<string> categoryList = new List<string>();
			List<string> chkCategoryList = new List<string>();
			List<string> chkParentCategoryList = new List<string>();

			// 지정어 사전 확인
			string jijungString = string.Empty;

			if (categoryCode.Length > 1)
			{
				if (categoryCode.Substring(categoryCode.Length - 1) != "")
				{
					categoryCode += "|";
				}
			}

			QADicFinder finder = new QADicFinder(VoyagerHelper.GetConnectionString("FilterFinder"));
			jijungString = finder.GetJijung(keyword.Replace(" ", ""));

			// 지정어 사전에 포함되어 있다면,
			if (jijungString.Length > 0)
			{
				string[] valArray = jijungString.Split(':');
				int catLen = valArray.Length - 10;

				string catType = valArray[0].Trim();
				string catCode = string.Empty;

				// 카테고리 설정 내용이 있어야 하고 적용여부가 N 이 아닐때
				if (catLen > 0 && !catType.ToUpper().Equals("N"))
				{
					if (categoryLevel <= 1)
					{
						// 카테고리 Navi중 가장 마지막 Depth의 카테고리만 저장한다.
						for (int i = 0; i < catLen; i++)
						{
							catCode = valArray[10 + i].Trim();
							string[] catNavi = catCode.Split('>');
							categoryList.Add(catNavi[catNavi.Length - 1]);
						}

					}

					// 적용된 카테고리 리스트를 검색조건에 추가한다. 
					// 'I' 일경우 지정어 사전에 포함된 카테고리가 검색, 그렇지 않은 경우 제외된 카테고리 검색.
					if (catType.ToUpper().Equals("I"))
					{
						if (categoryLevel > 0)
						{
							List<string> CategoryTemp = categoryCode.TrimEnd('|').Split('|').ToList();
							foreach (string s in CategoryTemp)
							{
								bool isAdd = true;
								for (int i = 0; i < categoryList.Count; i++)
								{
									if (s == categoryList[i])
									{
										isAdd = false;
										break;
									}
								}

								if (isAdd)
								{
									if (categoryLevel > 1)
									{
										categoryList.Add(s);
									}
								}
							}
						}

						filter.CategoryOrList = categoryList;
					}
					else
					{
						if (categoryLevel > 0)
						{
							filter.CategoryOrList = categoryCode.TrimEnd('|').Split('|').ToList();
						}
						filter.CategoryNotOrList = categoryList;
					}
				}
				else
				{
					if (categoryLevel > 0)
					{
						filter.CategoryOrList = categoryCode.TrimEnd('|').Split('|').ToList();
					}
				}
			}
			// 카테고리가 존재한다면, 전시 카테고리 확인
			else if (categoryLevel > 0)
			{
				QADicFinder dpFinder = new QADicFinder(VoyagerHelper.GetConnectionString("FilterFinder"));
				string dpString = dpFinder.GetDisplayCategory(categoryCode);

				// 전시카테고리 정보 있음. 처리
				if (dpString.Length > 0)
				{
					// 입력받은 카테고리와 전시카테고리를 Append
					dpString = dpString + categoryCode;
					categoryList = dpString.TrimEnd('|').Split('|').ToList();

					// pclass_cd = 100000XXXX 이면서 class_cd가 2..... 로 시작되는 전시중분류와 3... 로 시작하는 전시소분류가 같이 있을경우
					if (categoryLevel == 1)
					{
						// 해당 경우 GroupBy 결과를 조작해야 한다.
						/*
						foreach (string chkCategory in categoryList)
						{
								if (chkCategory.StartsWith("3"))
								{
										chkCategoryList.Add(chkCategory);
										// 상위 중분류를 결과에서 GroupBy에서 빼기 위해 체크한다.
										//string catInfo = CategoryUtil.GetXMLCategoryItem("S", chkCategory);

										// 해당 전시소분류의 상위분류가 조건에 포함되지 않았을 경우만 체크하여 ..
										if (!categoryList.Contains(catInfo))
										{
												chkParentCategoryList.Add(catInfo);
										}
								}
						}
						*/

						filter.CategoryOrList = categoryList;
					}
					else
					{
						//filter.CategoryOrList = categoryCode.TrimEnd('|').Split('|').ToList();
						filter.CategoryOrList = categoryList;
					}
				}
				else
				{
					filter.CategoryOrList = categoryCode.TrimEnd('|').Split('|').ToList();
				}
			}
			#endregion

			#region ::: Set Sort Option
			SortCollection sc = new SortCollection();
			switch (sortType.ToLower())
			{
				case "rank_point_desc":
					string sortExpression = string.Empty;
					#region ::: 정확도 추가 처리
					// 키워드에 대한 형태소 분석
					// 추가-- 
					string extractorString = string.Empty;
					string notString = string.Empty;
					QADicFinder apFinder = new QADicFinder(VoyagerHelper.GetConnectionString("FilterFinder"));
					extractorString = apFinder.GetExtract(keyword);

					// Not 사전 검색
					QADicFinder notFinder = new QADicFinder(VoyagerHelper.GetConnectionString("FilterFinder"));
					notString = notFinder.GetNotKeyword(extractorString.TrimEnd());

					// Not 검색
					if (notString.Length > 0)
						filter.NotKeyword = Expression.Parse(notString);

					// 정확도점수용 호출
					QADicFinder APFinder = new QADicFinder(VoyagerHelper.GetConnectionString("FilterFinder"));
					string jijungAPString = APFinder.GetAPJijung(keyword.Replace(" ", ""));
					List<string> apJijungList = new List<string>();
					List<string> apJijungMinusList = new List<string>();
					List<string> categoryAPList = new List<string>();


					if (jijungAPString.Length > 0)
					{

						string[] valArray = jijungAPString.Split(':');
						int catLen = valArray.Length - 10;

						string catType = valArray[0].Trim();
						string catCode = string.Empty;

						// 카테고리 설정 내용이 있어야 하고 적용여부가 N 이 아닐때
						if (catLen > 0 && !catType.ToUpper().Equals("N"))
						{
							// 카테고리 Navi중 가장 마지막 Depth의 카테고리만 저장한다.
							for (int i = 0; i < catLen; i++)
							{
								catCode = valArray[10 + i].Trim();
								string[] catNavi = catCode.Split('>');
								categoryAPList.Add(catNavi[catNavi.Length - 1]);
							}
							// 적용된 카테고리 리스트를 정확도 점수에 반영한다. 
							if (catType.ToUpper().Equals("I"))
								apJijungList = categoryAPList;
							else
								apJijungMinusList = categoryAPList;
						}
					}

					int catePoint = 200000;
					int buyPoint = 100000;
					int jijungPoint = 200000;
					int limitPoint = 200000;
					int addSCatePoint = 100000;
					sortExpression = SortAPExpression.GetExpression2(new ItemFilter().ReplaceSpecialChar(keyword.ToLower().Trim()), extractorString.ToLower(), apJijungList, apJijungMinusList, catePoint, buyPoint, jijungPoint, limitPoint, addSCatePoint);

					#endregion

					//queryString = keyword + ":" + prevKeyword + ":" + filter.GetExpression() +":" + sortExpression;

					//RankPoint순으로 정렬, 상품번호로 2차 정렬
					if (extractorString.Trim().Length > 0 && extractorString.Split(' ').Length <= 5)
						sc.Add(Sort.Create(sortExpression, SortOrder.Decreasing));

					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "focus_rank_desc":
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
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
				default:
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
				return new SearchItemBiz().GetQueryResult(filter, (pageNo - 1) * rowCount, rowCount, sc, hqc);
			}
			else
			{
				return new SearchItemBiz().GetQueryResult(filter, 0, items.Length, sc, hqc);
			}

		}


		private SearchItemT[] GetSellerAdItems(string mNo, string keyword, int pageSize, int pageNo
												, int minPrice, int maxPrice, string prevKeyword
												, string lcId, string mcId, string scId, string sortType
												, string isFeeFree, string isMileage, string isDiscount, string isStamp, string sellCustNo)
		{
			// 광고상품 가져오기
			List<SellerAdT> itemList = new SellerAdBiz().GetSellerAdListFromDB(mNo, mcId, scId, keyword);

			var list = itemList.Select(row => row.GdNo).ToArray();

			if (list.Length == 0)
			{
				return new SearchItemT[0];
			}

			int categoryLevel = 0;
			string queryString = "";

			var queryResult = this.GetSearchItem("", pageSize, pageNo
										, minPrice, maxPrice, prevKeyword
										, lcId, mcId, scId, sortType
										, isFeeFree, isMileage, isDiscount
										, isStamp, sellCustNo, out categoryLevel, out queryString, list).Entities;

			List<SearchItemT> adResult = new List<SearchItemT>();

			if (queryResult == null)
			{
				queryResult = new SearchItemT[0];
			}
			else
			{
				SearchItemT[] searchResult = (SearchItemT[])queryResult;

				for (int i = 0; i < itemList.Count; i++)
				{
					for (int j = 0; j < searchResult.Length; j++)
					{
						if (itemList[i].GdNo == searchResult[j].GdNo.ToString())
							adResult.Add(searchResult[j]);
					}
				}
			}

			return adResult.ToArray();
		}

		[HttpPost]
		public JsonResult GetAddCartResult(string itemNo, short orderQty = 1, bool isInstantOrder = false)
		{
			ApiResponse<GetAddCartResponseT> result = new SearchAPIBiz().GetAddCartResult(itemNo, orderQty, isInstantOrder);
			if (result != null)
			{
				if (result.ResultCode == 0)
				{
					return Json(new { success = true, message = result.Message, data = result.Data });
				}
				else
				{
					return Json(new { success = false, message = result.Message, data = result.Data });
				}
			}
			else
			{
				return Json(new { success = false });
			}
		}

		public ActionResult SearchPage()
		{
			return View();
		}

		public JsonResult GetBest100Items(int pageNo = 1, int pageSize = 100, string code = "", bool forMobile = true)
		{
			var best100Item = GenericUtil.AtLeastReturnEmptyList(new BestItemApiBiz().GetBest100Items(pageNo, pageSize, forMobile));
			return Json(best100Item, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult BlockedKeyword(string primeKeyword, string type)
		{
			ViewBag.HeaderTitle = primeKeyword;
			ViewBag.primeKeyword = primeKeyword;
            ViewBag.type = type;
			return View();
		}
	}
}

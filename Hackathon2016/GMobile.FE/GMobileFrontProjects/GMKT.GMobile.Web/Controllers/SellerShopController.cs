using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using GMobile.Data.Voyager;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Util;
using GMKT.GMobile.Filter;
using GMKT.Web.Context;
using GMKT.GMobile.Constant;
using GMKT.GMobile.Biz.SellerShop;
using GMKT.GMobile.Data.SellerShop;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Web.Controllers
{
	public class SellerShopController : SellerShopBaseController
	{
		#region [ Constants ]
		public static readonly int NOTICE_LIST_PAGESIZE = 10;
		#endregion

		//
		// GET: /Shop/
		public ActionResult Index(string goodsCode)
		{
            ViewBag.PageType = PageType.Main;

			SellerShopMainModel model = new SellerShopMainModel();

			ViewBag.HeaderTitle = ViewBag.Shop.Title;
			
			/* Landing Banner */
			new LandingBannerSetter(Request).Set(model, PageAttr.IsApp);

			/*ViewBag.isNewMobileDisplay = true;
			if (this.Shop.DisplayInfo.MobileShopDispNo <= 0)
			{
				ViewBag.isNewMobileDisplay = false;
			}*/

			model.MainDisplayInfo = new SellerShopApiBiz().GetMain(this.Shop.SellerId, goodsCode);
			if(model.MainDisplayInfo != null) 
			{
				this.SetItemImageNLinkByAdult(model.MainDisplayInfo.SelectedItem);
                ViewBag.TopIntro = model.MainDisplayInfo.TopIntro;

                if (model.MainDisplayInfo.MainImage != null && String.IsNullOrEmpty(model.MainDisplayInfo.MainImage.Title))
                    model.MainDisplayInfo.MainImage.Title = "Best Item";
			}

			return View(model);
		}

		public ActionResult Promotion(long id)
		{
			ViewBag.HeaderTitle = ViewBag.Shop.Title;
			SellerShopPromotion items = new SellerShopApiBiz().GetPromotion(id, this.Shop.SellerId);
			if(items == null)
			{
				return AlertMessageAndHistorybackOrClose("기획전이 종료되었습니다", "-1");
			}
			items.Main = GenericUtil.AtLeastReturnEmptyList(items.Main);
			return View(items);
		}

		public ActionResult Menu(long menuNo, long directNoticeSeq = 0)
		{
			ViewBag.HeaderTitle = ViewBag.Shop.Title;
            ViewBag.PageType = PageType.Menu;

			SellerShopMain mainDisplayInfo = new SellerShopApiBiz().GetMain(this.Shop.SellerId, null);

			ViewBag.menuExposeType = ShopMenuExposeType.OneRowFlicking;
			if (mainDisplayInfo != null)
			{
				ViewBag.menuExposeType = mainDisplayInfo.MenuExposeType;
                ViewBag.TopIntro = mainDisplayInfo.TopIntro;
			}
			List<MenuT> menulist = new SellerShopApiBiz().GetMenu(this.Shop.SellerId, this.Shop.DisplayInfo.ShopDispNo);
			MenuT selectedMenu = new MenuT();
			if (menulist != null && menulist.Count > 0)
			{
				selectedMenu = menulist.Where(o => o.MenuNo == menuNo).FirstOrDefault();
				if (selectedMenu == null || selectedMenu.MenuNo <= 0)
				{
					string href = Urls.MobileWebUrl + "/SellerShop/" + this.Alias;
					return AlertMessageAndLocationChange("메뉴번호를 확인하세요.", href);
				}
				else
				{
					ViewBag.selectedMenuNo = menuNo;
					ViewBag.selectedMenuUseType = selectedMenu.MenuUseType;
					ViewBag.directNoticeSeq = directNoticeSeq;
				}
			}
			else
			{
				string href = Urls.MobileWebUrl + "/SellerShop/" + this.Alias;
				return AlertMessageAndLocationChange("판매자 메뉴페이지가 설정되어 있지 않습니다.", href);
				//return RedirectToAction("Index", "SellerShop", new { alias = Alias });
			}

			return View(menulist);
		}

		[HttpPost]
		public JsonResult GetMenuContent(string sellCustNo, long shopDispNo, int menuNo)
		{
			SellerShopMenuContent menuContent = new SellerShopApiBiz().GetMenuContent(sellCustNo, shopDispNo, menuNo);
			if (menuContent != null)
			{
				return Json(new
				{
					success = true,
					menu = menuContent
				});
			}

			return Json(new
			{
				success = false
			});
		}

		public ActionResult Navigation()
		{
			ViewBag.HeaderTitle = ViewBag.Shop.Title;
			bool isShopCategory = (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryOnly) || (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryFirst);
			if(isShopCategory)
			{
				ViewBag.RootLevel = new SellerShopApiBiz().GetRootCategoryLevel(ShopCategoryDisplayType.LargeMediumSmallCategory);
			}
			else
			{
				ViewBag.RootLevel = new SellerShopApiBiz().GetRootCategoryLevel(this.Shop.ShopCategoryDisplayType);
			}
			ViewBag.IsNavigation = true;
			return View();
		}

		public JsonResult GetCategories(string categoryId = "", string categoryNm = "", CategoryLevel level = CategoryLevel.LargeCategory)
		{
			SellerShopNavigationRequest request = this.GetSellerShopCategoryRequest(categoryId, level);
			SellerShopNavigation model = new SellerShopApiBiz().GetNavigation(request);
			
			model.CurrentId = categoryId;
			model.HasParent = !string.IsNullOrEmpty(categoryId);
			model.CurrentNm = string.IsNullOrEmpty(categoryId) ? "전체상품" : categoryNm;
			model.IsDisplayCount = (Char.ToUpper(this.Shop.GoodsCountExposeYn) == 'Y') && model.Children != null;
			
			return Json(model);
		}

		private SellerShopNavigationRequest GetSellerShopCategoryRequest(string categoryId, CategoryLevel level)
		{
			SellerShopNavigationRequest request = new SellerShopNavigationRequest();

			request.SellCustNo = this.Shop.SellerId;			
			request.IsShopCategory = (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryOnly) || (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryFirst);
			request.DisplayType = request.IsShopCategory ? (int)this.Shop.ShopCategoryDisplayType : (int)this.Shop.GeneralCategoryDisplayType;
			request.ParentCategory = categoryId;
			request.Level = level;

			return request;
		}

		#region [ 공지사항 ]

		[HttpPost]
		public JsonResult GetMenuSellerNoticeList(int pageNo = 1, int pageSize = 10)
		{
			List<SellerNoticeModel> noticeList = new List<SellerNoticeModel>();
			SellerShopNoticeList sellerNoticeList = new SellerShopApiBiz().GetMenuSellerNoticeList(this.Shop.SellerId, pageSize, pageNo);
			
			if (sellerNoticeList != null &&  sellerNoticeList.Notice != null)
			{
				if (sellerNoticeList.Notice.Count > 0)
				{
					foreach (ShopNewsT notice in sellerNoticeList.Notice)
					{
						SellerNoticeModel model = new SellerNoticeModel()
						{
							Title = notice.Title,
							IsNew = notice.IsNew,
							PostDate = notice.PostDate.ToString("yyyy.MM.dd"),
							No = notice.No
						};

						noticeList.Add(model);
					}
				}
				else
				{
					noticeList = new List<SellerNoticeModel>(); ;
				}

				return Json(new
				{
					success = true,
					noticeList = noticeList,
					noticeTotalCount = sellerNoticeList.NoticeTotalCount
				});
			}

			return Json(new
			{
				success = false
			});
		}

		[HttpPost]
		public JsonResult GetMenuSellerNoticeDetail(int noticeSeq)
		{
			ShopNewsT sellerNoticeDetail = new SellerShopApiBiz().GetMenuSellerNoticeDetail(this.Shop.SellerId, noticeSeq);
			if (sellerNoticeDetail != null)
			{
				SellerNoticeModel notice = new SellerNoticeModel()
				{
					Title = sellerNoticeDetail.Title,
					Content = HttpUtility.HtmlDecode(sellerNoticeDetail.Content),
					IsNew = sellerNoticeDetail.IsNew,
					PostDate = sellerNoticeDetail.PostDate.ToString("yyyy.MM.dd"),
					No = sellerNoticeDetail.No
				};

				return Json(new
				{
					success = true,
					notice = notice
				});
			}

			return Json(new
			{
				success = false
			});
		}
		#endregion

		public ActionResult HistoryAdd(string alias, string actionName, string message, string url)
		{
			return View(new HistoryAddM(){
				Alias = alias,
				ActionName = actionName,
				Message = message,
				Url = url, 
				ImageUrl = null,
			});
		}

		[HttpGet]
		[GMobileHandleErrorAttribute]
		public JsonResult AddFavoriteShop(string alias)
		{
			bool success = false;
			string message = string.Empty;

			if (false == String.IsNullOrEmpty(gmktUserProfile.CustNo))
			{
				SimpleModel result = new SellerShopApiBiz().AddFavoriteShop(gmktUserProfile.CustNo, this.Shop.SellerId);
				if (result != null)
				{
					success = result.Success;
					message = result.Message;
				}
			}
			else
			{
				success = false;
				message = "로그인이 필요합니다.";
			}

			return Json(new { success = success, message = message }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[GMobileHandleErrorAttribute]
		public JsonResult DelFavoriteShop(string alias)
		{
			bool success = false;
			string message = string.Empty;

			if (false == String.IsNullOrEmpty(gmktUserProfile.CustNo))
			{
				SimpleModel result = new SellerShopApiBiz().DelFavoriteShop(gmktUserProfile.CustNo, this.Shop.SellerId);
				if (result != null)
				{
					success = result.Success;
					message = result.Message;
				}
			}
			else
			{
				success = false;
				message = "로그인이 필요합니다.";
			}

			return Json(new { success = success, message = message }, JsonRequestBehavior.AllowGet);
		}
		
		[HttpGet]
		[GMobileHandleErrorAttribute]
		public JsonResult AddFavoriteItem(string itemNo)
		{
			int retValue = 0;
			string retMsg = string.Empty;

			if (false == String.IsNullOrEmpty(gmktUserProfile.CustNo))
			{
				retValue = new ShopBiz().SelectFavoriteItems(gmktWebContext.UserProfile.CustNo, 1, itemNo);
				if (retValue == 1)
				{
					retValue = new ShopBiz().AddFavoriteItems(gmktWebContext.UserProfile.CustNo, 1, itemNo);
					if (retValue == 1)
					{
						retMsg = "관심상품으로 등록되었습니다.";
					}
					else
					{
						retValue = -999;
						retMsg = "관심상품 등록에 실패했습니다. 다시 등록해주십시오.";
					}
				}
				else
				{
					retValue = -998;
					retMsg = "이미 관심상품으로 등록되었습니다.";
				}
			}
			else
			{
				retValue = -100;
				retMsg = "로그인이 필요합니다.";
			}

			return Json(new { result = retValue, msg = retMsg }, JsonRequestBehavior.AllowGet);
		}

			//
		// GET: /Shop/Search
		public ActionResult Search(string keyword = "", int sort = 1)
		{
			ViewBag.HeaderTitle = ViewBag.Shop.Title;
			if (keyword == "" || keyword.Length == 0)
			{
				return RedirectToAction("Index");
			}

			SellerShopLPSRPModel model = new SellerShopLPSRPModel();
			model.MenuName = "SRP";
			model.Keyword = keyword;					
			model.Sort = sort;
			model.IsShopCategory = (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryOnly) || (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryFirst);

            ViewBag.MenuName = "SRP";
			ViewBag.IsLogin = Convert.ToString(PageAttr.IsLogin).ToLowerInvariant();

			return View("~/Views/SellerShop/List.cshtml", model);
		}

		//
		// GET: /Shop/List
		public ActionResult List(string category = "", int sort = 1, string title = "")
		{
			ViewBag.HeaderTitle = ViewBag.Shop.Title;
            ViewBag.PageType = PageType.List;
			ViewBag.IsLogin = Convert.ToString(PageAttr.IsLogin).ToLowerInvariant();

			SellerShopLPSRPModel model = new SellerShopLPSRPModel();
			model.MenuName = "LP";
			model.Sort = sort;
            model.IsShopCategory = (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryOnly) || (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryFirst);

            if (model.IsShopCategory)
			{
				model.CategoryCode = category;
				model.ShopCategory = category;

				List<SellerShopCategory> shopCategories = new SellerShopApiBiz_Cache().GetShopCategories(this.Shop.SellerId);
				SellerShopCategory currentShopCategory = (shopCategories != null) ? 
					shopCategories.Where(c => c.CategoryId == category).FirstOrDefault() : 
					new SellerShopCategory(){ CategoryNm = "", ParentId = "" };

				model.CategoryName = (currentShopCategory != null) ? currentShopCategory.CategoryNm : "";
				model.PrevCateCode = (currentShopCategory != null && !string.IsNullOrEmpty(currentShopCategory.ParentId)) ? currentShopCategory.ParentId.Trim() : "";
			}
			else 
			{
                model.CategoryCode = category;
				CategoryInfoT categoryInfo = CategoryBiz.GetCategoryInfo(category);
				model.PrevCateCode = CategoryUtil.GetParentCategoryCode(category);

				if(!String.IsNullOrEmpty(category) && categoryInfo != null)
				{
					model.CategoryName = categoryInfo.Name;		
					
					switch(categoryInfo.Level)
					{
						case CategoryLevel.LargeCategory:
							model.LcId = category;
							break;
						case CategoryLevel.MediumCategory:
							model.McId = category;
							break;
						case CategoryLevel.SmallCategory:
							model.ScId = category;
							break;
					}
				}
				else
				{
					model.CategoryName = "";
				}
				
			}

			if(string.IsNullOrEmpty(model.CategoryCode))
			{
				model.CategoryName = "전체상품";
				model.PrevCateCode = "";
			}

			//메인 전시영역 더보기
			if (!string.IsNullOrEmpty(title))
			{
				model.CategoryCode = String.Empty;
				model.CategoryName = title;
				model.PrevCateCode = "";
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult ListPost(string shopCategory="", string keyword="", string lcId="", string mcId="", string scId="",
			int pageNo = 1, int pageSize = 20, int sort = 1, string scKeyword="", int minPrice=0, int maxPrice=0, 
			string isExKeyword="N", string isFreeDelivery="N", string isMileage="N", string isDiscount="N", string isStamp="N", string isSmartDelivery="N")
		{
			bool isShopCategory = (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryOnly) || (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryFirst);

			SellerShopSearchRequest request = new SellerShopSearchRequest();
			request.PageNo = pageNo;
			request.PageSize = pageSize;
			request.SortType = sort;
			request.SellCustNo = this.Shop.SellerId;			
			request.MinPrice = minPrice;
			request.MaxPrice = maxPrice;
			request.IsFreeDelivery = isFreeDelivery;
			request.IsMileage = isMileage;
			request.IsDiscount = isDiscount;
			request.IsStamp = isStamp;
			request.IsSmartDelivery = isSmartDelivery;
			request.Keyword = keyword;
			request.ShopCategory = shopCategory;
			request.Level = "0";

			if(isExKeyword.ToUpper() == "N")
			{
				request.ScKeyword = scKeyword;
				request.ExKeyword = "";
			}
			else
			{
				request.ScKeyword = "";
				request.ExKeyword = scKeyword;
			}

			List<SellerShopCategory> shopCategories = null;
			if (isShopCategory) 
			{	
				request.IsShopCategory = "Y";

				shopCategories = new SellerShopApiBiz_Cache().GetShopCategories(this.Shop.SellerId);

				if (shopCategories != null)
				{
					if (!String.IsNullOrEmpty(keyword) && String.IsNullOrEmpty(shopCategory))			
						request.Level = "1";
					else
					{
						SellerShopCategory sellerShopCategory = shopCategories.Where(n => n.CategoryId == shopCategory).FirstOrDefault();

						if (sellerShopCategory != null)
						{
							switch(sellerShopCategory.Level)
							{
								case CategoryLevel.LargeCategory:
									request.Level = "2";
								break;
								case CategoryLevel.MediumCategory:
									request.Level = "3";
								break;
								case CategoryLevel.SmallCategory:
									request.Level = "4";
								break;
							}
						}
					}
				}
			}
			else
			{
				request.IsShopCategory = "N";
			}
			
			request.LcId = lcId;
			request.McId = mcId;
			request.ScId = scId;

			SellerShopLPSRP result = new SellerShopApiBiz().Search(request);

			if(result != null && result.Item != null)
			{
				foreach(SellerShopItem item in result.Item)
				{
					this.SetItemImageNLinkByAdult(item);
				}
			}

            if (isShopCategory)
            {
				SellerShopCategory currentShopCategory = (shopCategories != null) ?
					shopCategories.Where(c => c.CategoryId == shopCategory).FirstOrDefault() :
					new SellerShopCategory() { CategoryNm = "", ParentId = "" };

				if (currentShopCategory != null)
				{
					string requestCategoryId = currentShopCategory.CategoryId;

					SellerShopCategory lShopCategory = null;
					SellerShopCategory mShopCategory = null;
					SellerShopCategory sShopCategory = null;

					switch (currentShopCategory.Level)
					{
						case CategoryLevel.LargeCategory:
							lShopCategory = currentShopCategory;
							break;
						case CategoryLevel.MediumCategory:
							lShopCategory = (shopCategories != null) ? shopCategories.Where(c => c.CategoryId == currentShopCategory.ParentId).FirstOrDefault() : null;
							mShopCategory = currentShopCategory;
							break;
						case CategoryLevel.SmallCategory:
							requestCategoryId = currentShopCategory.ParentId;

							mShopCategory = (shopCategories != null) ? shopCategories.Where(c => c.CategoryId == currentShopCategory.ParentId).FirstOrDefault() : null;
							if (mShopCategory != null)
								lShopCategory = (shopCategories != null) ? shopCategories.Where(c => c.CategoryId == mShopCategory.ParentId).FirstOrDefault() : null;

							sShopCategory = currentShopCategory;
							break;
					}

					if (lShopCategory != null)
					{
						result.LcId = lShopCategory.CategoryId;
						result.LcIdName = lShopCategory.CategoryNm;
					}

					if (mShopCategory != null)
					{
						result.McId = mShopCategory.CategoryId;
						result.McIdName = mShopCategory.CategoryNm;
					}

					if (sShopCategory != null)
					{
						result.ScId = sShopCategory.CategoryId;
						result.ScIdName = sShopCategory.CategoryNm;
					}
				}
            }

            string bannerCategoryCode = "";
            if (request.ScId.Length > 0)
                bannerCategoryCode = request.ScId;
            else if (request.McId.Length > 0)
                bannerCategoryCode = request.McId;
            else
                bannerCategoryCode = request.LcId;

            CategoryBannerInfoRequest bannerRequest = new CategoryBannerInfoRequest { ShopDispNo = this.Shop.DisplayInfo.ShopDispNo, CategoryCode = bannerCategoryCode };
            CategoryBannerInfo bannerInfo = new SellerShopApiBiz().GetCategoryBanner(bannerRequest);

            if (bannerInfo != null)
                result.CategoryBannerInfo = bannerInfo;
            else
                result.CategoryBannerInfo = new CategoryBannerInfo();

			return Json(result);
		}

        public ActionResult ItemDisplayFromMenu(int dispType = 0, int sortType = 0, long areaNo = 0, string title = "", int isMain = 0, string itemBgColorCd = "")
		{
			SellerShopItemDisplayModel model = new SellerShopItemDisplayModel();
			List<SellerShopItemDisplay> data = new List<SellerShopItemDisplay>();
			
			SellerShopItemDisplay itemDispInfo = new SellerShopItemDisplay();
			itemDispInfo.AreaNo = areaNo;
			itemDispInfo.DispType = dispType == 0 ? 1 : dispType;
			itemDispInfo.DispTypeStr = ConvertItemDispTypeStr(itemDispInfo.DispType);
			itemDispInfo.SortType = sortType;
			itemDispInfo.Title = title;
            itemDispInfo.AddYN = 'Y';
            itemDispInfo.BgColorCd = "#" + itemBgColorCd;

			data.Add(itemDispInfo);

			model.AddVideoKey = "";
			model.SelectedItem = null;
			model.ItemDisplayList = data;
			model.IsMain = isMain;
			model.MagazineCount = data.Where(p => p.DispType == 5).Count();

			return PartialView("ItemDisplay", model);
		}

		public ActionResult ItemDisplay(long shopDispNo, string addVideoKey = "", int isMain = 1, SellerShopItem selectedItem = null)
		{
			SellerShopItemDisplayModel model = new SellerShopItemDisplayModel();
			List<SellerShopItemDisplay> data = new List<SellerShopItemDisplay>();

			model.ItemDisplayList = new List<SellerShopItemDisplay>();
			model.SelectedItem = new SellerShopItem();

			data = new SellerShopApiBiz().GetMainItemDisplayInfo(shopDispNo, this.Shop.ShopLevel.ToString());
			if (data == null || data.Count <= 0)
			{
				SellerShopItemDisplay itemDispInfo = new SellerShopItemDisplay();
				itemDispInfo.AreaNo = 0;
				itemDispInfo.DispType = 1;
				itemDispInfo.DispTypeStr = "main_default";
				itemDispInfo.SortType = 1;
				itemDispInfo.Title = "전체상품";

				data.Add(itemDispInfo);
			}
			
			model.AddVideoKey = addVideoKey;
			model.SelectedItem = selectedItem;
			model.ItemDisplayList = data.OrderByDescending(x => x.AddYN).ToList();
			model.IsMain = isMain;
			model.MagazineCount = data.Where(p => p.DispType == 5).Count();

			return PartialView(model);
		}

		

		[HttpPost]
		public JsonResult GetItemDisplay(int dispType = 0, int sortType = 0, long areaNo = 0, int pageNo = 1, string addYN = "Y")
		{
			ItemDisplayPage itemDisplayData = new ItemDisplayPage();
			
			string sellerCustNo = this.Shop.SellerId;
			
			itemDisplayData = new SellerShopApiBiz().GetItemDisplay(sellerCustNo, dispType, sortType, areaNo, pageNo, this.Shop.DisplayInfo.ShopDispNo);
			
			if(itemDisplayData != null && itemDisplayData.ItemList != null)
			{
                itemDisplayData.AddYN = addYN;
				foreach(SellerShopItem item in itemDisplayData.ItemList)
				{
					this.SetItemImageNLinkByAdult(item);
					////성인 상품 && 성인인증 하지 않은 경우
					//if(item.AdultYN && !PageAttr.IsAdultUse)
					//{	
					//    item.ImageUrl = Urls.MobileImageUrlV2 + Const.ADULT_IMAGE_210.Replace("images/", string.Empty);
					//    item.Image400Url = item.ImageUrl;
						
					//    if (PageAttr.IsLogin)
					//    {
					//        if (!BizUtil.IsAdultUser(GMobileWebContext.Current.UserProfile.CustNo)) // 미성년자
					//        {
					//            item.LandingUrl = "javascript:alert('죄송합니다.\n성인만 구매 가능한 상품입니다.');";
					//        }
					//    }						
					//    else // Login 필요
					//    {							
					//        item.LandingUrl = Urls.LoginUrl + "?rtnurl=" + HttpUtility.UrlEncode(item.LandingUrl) + "&adultUseLoinCheck=Y";
					//    }						
					//}
				}
			}


			return Json(new { success = true, displayData = itemDisplayData });
		}

		[HttpPost]
		public JsonResult GetYoutubeVodInfo(string vodKey)
		{
			YoutubeInfo vodInfo = new YoutubeInfo();

			vodInfo = new SellerShopApiBiz().GetYoutubeInfo(vodKey);

			return Json(new { success = true, vodInfo = vodInfo });
		}

		public static string ConvertItemDispTypeStr(int dispType)
		{
			switch (dispType)
			{
				case 1: // 이미지형(기본)
                case 3: // 박스형(삭제)
                case 4: // 모자이크형(삭제)
					return "image";
				case 2: // 리스트형
                    return "list";
				case 5: // 매거진형
                    return "magazine";
                case 6:// 큰이미지형
                    return "large_image";
                case 7: //핀테그레스형
                    return "pinterest";
                case 8: //롤링형
                    return "rolling";
				default:
                    return "image";
			}
		}

        public static string ConvertExposeRateTypeStr(int exposeRateType)
        {
            switch (exposeRateType)
            {
                case 1: // 정사각형
                    return "";
                case 2: // 원형
                    return "circle";
                case 3: // 세로형
                    return "vertical";
                case 4: // 가로형
                    return "horizontal";
                default:
                    return "";
            }
        }

        private void SetItemImageNLinkByAdult(SellerShopItem item)
        {
            if (item == null || item.AdultYN == false) return;

            if (PageAttr.IsAdultUse) return;

            item.ImageUrl = Urls.MobileImageUrlV2 + Const.ADULT_IMAGE_210.Replace("images/", string.Empty);
            item.Image400Url = item.ImageUrl;

            if (PageAttr.IsLogin)
            {
                if (!BizUtil.IsAdultUser(GMobileWebContext.Current.UserProfile.CustNo)) // 미성년자
                {
                    item.LandingUrl = "javascript:alert('죄송합니다.\n성인만 구매 가능한 상품입니다.');";
                }
            }
            else // Login 필요
            {
                item.LandingUrl = Urls.LoginUrl + "?rtnurl=" + HttpUtility.UrlEncode(item.LandingUrl) + "&adultUseLoinCheck=Y";
            }
        }
	}
}

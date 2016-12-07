using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/*
using GMKT.GMobile.ExternalApi;
using GMKT.GMobile.ExternalApi.GMobileApi;
*/
using GMKT.GMobile.Web.Models;
using GMobile.Data.Tiger;
using GMobile.Data.Diver;
using GMobile.Service.Search;
using GMobile.Service.Home;
using GMobile.Service.Display;
using GMKT.GMobile.Util;
using GMobile.Data.DisplayDB;
using GMobile.Data.Event;
using GMKT.GMobile.Extensions;
using GMobile.Data.Voyager;
using Arche.Data.Voyager;
using System.Web.Caching;
using GMKT.GMobile.Constant;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using GMKT.GMobile.CommonData;
using GMKT.Web.Membership;
using GMKT.Component.Member;

namespace GMKT.GMobile.Web.Controllers
{
	public class HomeController : GMobileControllerBase
	{
		protected string BlueLinkText(string linkText)
		{
			return "<span>" + linkText + "</span>";
		}
		
		public ActionResult Index()
        {
			string siteCode = string.Empty;
			if (PageAttr.Exid == ExIdAppEnum.SFC)
		    {
				siteCode = GMKT.Component.Member.ExternalMallSiteCode.SFC;				
		    }

			PageAttr.HeaderType = HeaderTypeEnum.Normal;

			PageAttr.HasRPMScript = true;

            ViewBag.Title = "G마켓- 쇼핑을 다 담다.";
            HomeModelV2 homeModel = new HomeModelV2();			
			PageAttr.IsMain = true;
			
            SetHomeTabName("홈");

						ViewBag.MItemWebURL = Urls.MItemWebURL;
						ViewBag.MWebUrl = Urls.MWebUrl;
						ViewBag.IsLogin = PageAttr.IsLogin;
			#region [Pilot - gsohn] Pilot Test 이전 코드 (테스트 완료후 복구)
            //string userInfo = string.Empty;
            //if (gmktUserProfile != null && false == string.IsNullOrEmpty(gmktUserProfile.UserInfoString))
            //{
            //    userInfo = gmktUserProfile.UserInfoString;
            //}
            //ApiResponse<HomeTotalListV2> GetMobileHomeTotalList = new MobileHomeBiz().GetMobileHomeTotalListV2(userInfo, siteCode);
			#endregion
			#region [Pilot - gsohn] Pilot Test 코드 (테스트 완료후 삭제)
			string userInfo = string.Empty;
			if (gmktUserProfile != null && false == string.IsNullOrEmpty(gmktUserProfile.UserInfoString))
			{
				userInfo = gmktUserProfile.UserInfoString;
			}
			string jaehuid = string.Empty;
			if (GMKT.Web.Context.GMobileWebContext.Current != null && String.IsNullOrEmpty(GMKT.Web.Context.GMobileWebContext.Current.JahuID) == false)
			{
				jaehuid = GMKT.Web.Context.GMobileWebContext.Current.JahuID;
			}
			ApiResponse<HomeTotalListV2> GetMobileHomeTotalList = new MobileHomeBiz().GetMobileHomeTotalListV2Pilot(userInfo, siteCode, jaehuid);
			#endregion

            if (GetMobileHomeTotalList != null && GetMobileHomeTotalList.Data != null)
            {
				homeModel.ListingGroup = new List<HomeMainGroup>();

				foreach(HomeMainGroup group in GetMobileHomeTotalList.Data.HomeMainGroupList)
				{
					if(group.Type == HomeMainGroupType.AdBanner)
					{
						homeModel.HomeAdBanner = group.BannerList;
						homeModel.HomeAdBannerTrackingUrlList = group.TrackingUrlList;
                        homeModel.HasFirstExposureMainBanner = group.HasFirstExposureMainBanner;
                        homeModel.FirstExposureMainBannerIndex = group.FirstExposureMainBannerIndex;
					}
					else if(group.Type == HomeMainGroupType.SuperdealThemeCategory){
						homeModel.SuperDealCategoryList = group.CategoryList;
					}
					else if(group.Type == HomeMainGroupType.ExternalMallAdBanner && group.BannerList != null)
					{
						homeModel.CountOfExternalMallAdBanner = group.BannerList.Count;
						if (homeModel.HomeAdBanner != null)
						{
							homeModel.HomeAdBanner.InsertRange(0, group.BannerList);
						}
					}
					else if(group.Type == HomeMainGroupType.Icon)
					{
						if(group.BannerList != null && group.BannerList.Count > 5)
						{
							homeModel.HomeIcon = group.BannerList.GetRange(0, 5);
							homeModel.HomeIcon2 = group.BannerList.GetRange(5, group.BannerList.Count - 5);
						}
						else
						{
							homeModel.HomeIcon = group.BannerList;
							homeModel.HomeIcon2 = new List<HomeMainBanner>();
						}
					}
					else if(group.Type == HomeMainGroupType.SuperdealTitle)
					{
						homeModel.SuperDealLinkUrl = group.LinkUrl;
					}
					else if(group.Type == HomeMainGroupType.ExternalMallTopBanner)
					{
						homeModel.ExternalMallTopBanner = group.BannerList;
					}
					else if(group.Type == HomeMainGroupType.ExternalMallItem)
					{
						homeModel.ExternalMallListingGroup.Add(group);
					}
					else
					{
						homeModel.ListingGroup.Add(group);
					}
				}

				homeModel.TrackingUrlList = GetMobileHomeTotalList.Data.TrackingUrlList;
                homeModel.TrackingUrlList2 = GetMobileHomeTotalList.Data.TrackingUrlList2;
				homeModel.HasHomeNotice = GetMobileHomeTotalList.Data.HasHomeNotice;
				homeModel.HomeNotice = GetMobileHomeTotalList.Data.HomeNotice;
                homeModel.HasLGUPlusNotice = GetMobileHomeTotalList.Data.HasLGUPlusNotice;
                homeModel.LGUPlusNotice = GetMobileHomeTotalList.Data.LGUPlusNotice;
                if (homeModel.LGUPlusNotice != null)
                {
                    homeModel.LGUPlusNotice.DailySeq = homeModel.LGUPlusNotice.Seq + DateTime.Now.ToString("yyyyMMdd");
                }
            }

            ViewBag.SwipeAuto = homeModel.HasFirstExposureMainBanner ? "false" : "true";

            List<string> testIdList = new List<string>();
            testIdList.Add("test4plan");
            testIdList.Add("lyj0707");
            testIdList.Add("hunchul");
            testIdList.Add("lyk028");
            testIdList.Add("yozzangga");
            ViewBag.IsUPlusIpAddress = IsUPlusIpAddress() && (testIdList.Contains(gmktUserProfile.LoginID) || DateTime.Now >= new DateTime(2016,11,30,0,0,0));
            /* Landing Banner */
            new LandingBannerSetter(Request).Set(homeModel, PageAttr.IsApp);

            return View(homeModel);
        }

        public ActionResult IndexV1()
        {
			PageAttr.HasRPMScript = true;

            ViewBag.Title = "G마켓- 쇼핑을 다 담다.";
            HomeModel homeModel = new HomeModel();
			PageAttr.IsMain = true;

            SetHomeTabName( "홈");
			string siteCode = string.Empty;
			if (PageAttr.Exid == ExIdAppEnum.SFC)
			{
				siteCode = GMKT.Component.Member.ExternalMallSiteCode.SFC;
			}
            ApiResponse<HomeTotalList> GetMobileHomeTotalList = new MobileHomeBiz().GetMobileHomeTotalList(siteCode);

            if (GetMobileHomeTotalList != null && GetMobileHomeTotalList.Data != null)
            {
                homeModel.HomeAdBanner = GetMobileHomeTotalList.Data.HomeAdBanner;
                homeModel.HomeIcon = GetMobileHomeTotalList.Data.HomeIcon;
                homeModel.HomeService = GetMobileHomeTotalList.Data.HomeService;
                homeModel.SuperDeal = GetMobileHomeTotalList.Data.SuperDeal;
				homeModel.SuperDealCategoryList = GetMobileHomeTotalList.Data.SuperDealCategoryList;
				homeModel.SuperDealLinkUrl = GetMobileHomeTotalList.Data.SuperDealLinkUrl;
				homeModel.HomeServiceBanner = GetMobileHomeTotalList.Data.HomeServiceBanner;
				if (GetMobileHomeTotalList.Data.ExternalMallAdBanner != null)
				{
					homeModel.CountOfExternalMallAdBanner = GetMobileHomeTotalList.Data.ExternalMallAdBanner.Count;
					if (homeModel.HomeAdBanner != null)
					{
						homeModel.HomeAdBanner.InsertRange(0, GetMobileHomeTotalList.Data.ExternalMallAdBanner);
					}
				}

				homeModel.HasHomeNotice = GetMobileHomeTotalList.Data.HasHomeNotice;
				homeModel.HomeNotice = GetMobileHomeTotalList.Data.HomeNotice;
                homeModel.HasLGUPlusNotice = GetMobileHomeTotalList.Data.HasLGUPlusNotice;
                homeModel.LGUPlusNotice = GetMobileHomeTotalList.Data.LGUPlusNotice;
                if (homeModel.LGUPlusNotice != null)
                {
                    homeModel.LGUPlusNotice.DailySeq = homeModel.LGUPlusNotice.Seq + DateTime.Now.ToString("yyyyMMdd");
                }
            }
            else
            {
                //어떻하지?
            }

            homeModel.PopupList = new MobilePopupBiz_Cache().GetMobilePopupList();
            List<string> testIdList = new List<string>();
            testIdList.Add("test4plan");
            testIdList.Add("lyj0707");
            testIdList.Add("hunchul");
            testIdList.Add("lyk028");
            testIdList.Add("yozzangga");
            ViewBag.IsUPlusIpAddress = IsUPlusIpAddress() && (testIdList.Contains(gmktUserProfile.LoginID) || DateTime.Now >= new DateTime(2016, 11, 30, 0, 0, 0));

            /* Landing Banner */
            new LandingBannerSetter(Request).Set(homeModel, PageAttr.IsApp);

            return View(homeModel);
        }

		[HttpPost]
		public JsonResult GetSuperDealItems(string code)
		{
			List<SuperDealItem> result = new SuperDealApiBiz_Cache().GetSuperDealItems(code, PageAttr.FromWhere);
			if (result == null || result.Count < 1)
			{
				result = new SuperDealApiBiz().GetSuperDealItems(code, PageAttr.FromWhere);
				if (result == null)
				{
					result = new List<SuperDealItem>();
				}
			}

			int indexSevenDealStart = 0;

			if (code == "000000000" && result != null && result.Count > 0)
			{
				SuperDealItem itemTypeB = result.Where(i => i.Type == 'B').First();
				indexSevenDealStart = result.IndexOf(itemTypeB);
				result = result.GetRange(0, indexSevenDealStart);
			}

			return Json(result);
		}

		[HttpPost]
		public JsonResult GetSuperDealThemeItems(string DispType = "1", string GroupNo = "", string SubNo = "")
		{
			SuperDealModel model = new SuperDealModel();
			if (string.IsNullOrEmpty(SubNo))
			{
				SubNo = "0";
			}
			List<SuperDealCategory> category = new SuperDealApiBiz_Cache().GetSuperDealThemeCategory();
			if (category == null || category.Count < 1)
			{
				category = new SuperDealApiBiz().GetSuperDealThemeCategory();
				if (category == null) category = new List<SuperDealCategory>();
			}
			
			model.ThemeCategory = category;
			List<HomeMainItem> items = new List<HomeMainItem>();
			if(GroupNo == "")
			{
				items = new SuperDealApiBiz_Cache().GetSuperDealThemeMainItem();
				if (items == null || items.Count < 1)
				{
					items = new SuperDealApiBiz().GetSuperDealThemeMainItem();
					if (items == null)
					{
						items = new List<HomeMainItem>();
					}
				}
				//Main페이지일때
			}
			else
			{
				// 슈퍼딜 상품 가져오기
				items = new SuperDealApiBiz_Cache().GetSuperDealThemeItem(DispType, GroupNo, SubNo);
				if (items == null || items.Count < 1)
				{
					items = new SuperDealApiBiz().GetSuperDealThemeItem(DispType, GroupNo, SubNo);
					if (items == null)
					{
						items = new List<HomeMainItem>();
					}
				}
			}
			

			model.ThemeItems = items;

			return Json(model);
		}

		[HttpGet]
		public JsonResult GetHomeListingDABanner(string exposeArea)
		{
			HomeMainGroup bannerGroup = new MobileHomeBiz().GetHomeListingDABanner(exposeArea);
			if(bannerGroup != null && bannerGroup.BannerList != null && bannerGroup.BannerList.Count > 0)
			{
				var rnd = new Random();
				bannerGroup.BannerList = bannerGroup.BannerList.OrderBy(a => rnd.Next()).ToList();
			}
			else
			{
				bannerGroup = new HomeMainGroup();
				bannerGroup.BannerList = new List<HomeMainBanner>();
			}

			return Json(bannerGroup, JsonRequestBehavior.AllowGet);
		}	
        //[OutputCache(Duration = 600)]
        //public ActionResult IndexOld()
        //{
        //    // 기획전(수동 등록 상품)
        //    List<MobileShopPlanT> popularPlanList = new MobileHomePlanBiz().GetMobileHomePlan();

        //    /*
        //    List<MobilePlanT> popularAutoPlanList = new List<MobilePlanT>();
        //    if (popularPlanList.Count < 15)
        //    {
        //        int rowCount = 15 - popularPlanList.Count;
        //        popularAutoPlanList = new MobilePlanBiz().GetMobilePlanList("", 1, rowCount);
        //    }
        //    */

        //    List<MobileShopPlanT> popularAutoPlanList = new List<MobileShopPlanT>();
        //    List<MobileShopPlanT> popularPlanMergeList = new List<MobileShopPlanT>();

        //    if (popularPlanList.Count < 15)
        //    {
        //        popularAutoPlanList = new MobileHomePlanBiz().GetMobileHomeAutoPlan(15);
        //        popularPlanMergeList = popularPlanList.Union(popularAutoPlanList, item => item.Sid).ToList<MobileShopPlanT>();
        //    }
        //    else{
        //        popularPlanMergeList = popularPlanList;
        //    }


        //    HomeModel model = new HomeModel();

        //    List<MobileHomeSpecialT> specialList = new MobileHomeIconBiz().GetMobileHomeSpecial();
        //    if (specialList != null && specialList.Count > 0)
        //    {

        //        int take = 0;
        //        /*
        //        if (specialList.Count > 1)
        //        {
        //            take = new Random().Next(0, specialList.Count - 1);
        //        }*/


        //        MobileHomeSpecialT specialT = specialList[take];
        //        model.SpecialVisible = true;
        //        model.SpecialText = specialT.banner_text;
        //        model.SpecialLink = specialT.banner_lnk_url;
        //    }
        //    else
        //    {
        //        model.SpecialVisible = false;
        //    }
                        	

        //    //HomeModel model = new HomeModel
        //    //{
        //    //    AdBannerList = new AdBannerBiz().GetMallManagerBanner("MB_SKT_MAIN_UP_BANNER", 20),
        //    //    MobileHomeIconList = new MobileHomeIconBiz().GetMobileHomeIcon(),
        //    //    TodaySpecial = new TodaySpecialBiz().GetTodaySpecialPrice(12),
        //    //    GoodSeries = new TodaySpecialBiz().GetMainPageBanner(4),
        //    //    BestSellerList = BestSellerBiz.GetBestSellerGoodsList(1.ToString(), 30.ToString()),
        //    //    PopularPlanList = popularPlanMergeList,
        //    //    //PopularAutoPlanList = popularPlanMerge,
        //    //    PopularPlanSize = popularPlanMergeList.Count,
        //    //    PopupList = new MobilePopupBiz().GetMobilePopupList()
        //    //};

        //    model.AdBannerList = getAddBannerList();
        //    model.MobileHomeIconList = new MobileHomeIconBiz().GetMobileHomeIcon();
        //    model.TodaySpecial = GetGmarketTodayItems();
        //    model.GoodSeries = new TodaySpecialBiz().GetMainPageBanner(4);
        //    model.BestSellerList = BestSellerBiz.GetBestSellerGoodsList(1.ToString(), 30.ToString());
        //    model.PopularPlanList = popularPlanMergeList;
        //    //model.PopularAutoPlanList = popularPlanMerge;
        //    model.PopularPlanSize = popularPlanMergeList.Count;
        //    model.PopupList = new MobilePopupBiz().GetMobilePopupList();

        //    // 2013-05-03 이윤호
        //    // 프로모션바 GAdmin으로 등록할 수 있기 전까지 사용할 모델
        //    // 반영시간에 따라 자동으로 문구와 URL 변경할 수 있도록 함
        //    DateTime now = DateTime.Now;
        //    if (now < new DateTime(2013, 5, 6, 10, 0, 0))
        //    {
        //        model.LinkText = string.Empty;
        //        model.LinkURL = string.Empty;
        //    }
        //    else if (now < new DateTime(2013, 5, 7, 10, 0, 0))
        //    {
        //        model.LinkText = "오전 10시 선착순~ " + BlueLinkText("하나사면 하나반값!");
        //        model.LinkURL = Urls.MWebUrl + "/Event/m_eventview/index.asp?msid=266";
        //    }
        //    else if (now < new DateTime(2013, 5, 14, 9, 0, 0))
        //    {
        //        model.LinkText = "전국민 " + BlueLinkText("5,000원") + " 쿠폰BOX 증정!";
        //        model.LinkURL = Urls.MWebUrl + "/event/2013/05/0506_campaign/m_main.asp";
        //    }
        //    else if (now < new DateTime(2013, 5, 21, 10, 0, 0))
        //    {
        //        model.LinkText = "출퇴근 시간 6시! " + BlueLinkText("모바일 특가를 잡아라!");
        //        model.LinkURL = Urls.MWebUrl + "/Event/2013/05/0506_time/m_mobile.asp";
        //    }
        //    else
        //    {
        //        model.LinkText = "무한카드혜택 " + BlueLinkText("15개월무이자+즉시할인5%");
        //        model.LinkURL = Urls.MWebUrl + "/event/2013/05/0521_card/card.asp";
        //    }

        //    return View(model);
        //}

		public List<AdBannerT> getAddBannerList()
		{
			List<AdBannerT> list = null;
			if (HttpRuntime.Cache[Const.CACHE_MAIN_AD_BANNER_LIST] == null)
			{
				// 데이터를 최초로 가져올 경우 
				list = new AdBannerBiz().GetMallManagerBanner("MB_SKT_MAIN_UP_BANNER", 20);
				// 5분마다 데이터 갱신
				HttpRuntime.Cache.Insert(Const.CACHE_MAIN_AD_BANNER_LIST, list, null, DateTime.Now.AddMinutes(5),
								Cache.NoSlidingExpiration);
			}

			list = (List<AdBannerT>)HttpRuntime.Cache[Const.CACHE_MAIN_AD_BANNER_LIST];

			if (HttpRuntime.Cache[Const.CACHE_MAIN_AD_BANNER_LIST] == null)
			{
				return new List<AdBannerT>();
			}

			List<AdBannerT> result = new List<AdBannerT>(list);
			return result;
		}

		//
		// GET: /Home/
        //public ActionResult Main()
        //{
        //    PageAttr.IsMain = true;

        //    //EtcShopSoap etcShopSvc = SoapClientHelper.GMobileEtcShopAPI;

        //    HomeModel model = new HomeModel
        //    {
        //        /*
        //        AdBannerDataArray
        //            = SoapClientHelper.GMobileAdBannerAPI.GetMallManagerBanner("MB_SKT_MAIN_UP_BANNER", "20"),
        //        TodaySpecialOfferGoodsArray
        //            = etcShopSvc.GetTodaySpecialOffer("20"),
        //        BestSellerGoodsArray = etcShopSvc.GetBestSellerGoodsList("1","24")
        //        */

        //        AdBannerList = new AdBannerBiz().GetMallManagerBanner("MB_SKT_MAIN_UP_BANNER", 20),
        //        TodaySpecialList = new TodaySpecialBiz().GetTodaySpecialOfferGoods(20),
        //        BestSellerList = BestSellerBiz.GetBestSellerGoodsList(1.ToString(), 24.ToString())
        //    };

        //    return View("Main", model);
        //}

		public ActionResult Test()
		{
			ViewBag.Title = "G마켓- 쇼핑을 다 담다.";
			HomeModel homeModel = new HomeModel();
			PageAttr.IsMain = true;

			SetHomeTabName("홈");
			ApiResponse<HomeTotalList> GetMobileHomeTotalList = new MobileHomeBiz().GetMobileHomeTotalList();

			if (GetMobileHomeTotalList != null && GetMobileHomeTotalList.Data != null)
			{
				homeModel.HomeAdBanner = GetMobileHomeTotalList.Data.HomeAdBanner;
				homeModel.HomeIcon = GetMobileHomeTotalList.Data.HomeIcon;
				homeModel.HomeService = GetMobileHomeTotalList.Data.HomeService;
				homeModel.SuperDeal = GetMobileHomeTotalList.Data.SuperDeal;
				homeModel.SuperDealCategoryList = GetMobileHomeTotalList.Data.SuperDealCategoryList;
			}
			else
			{
				//어떻하지?
			}

			homeModel.PopupList = new MobilePopupBiz_Cache().GetMobilePopupList();

			/* Landing Banner */
			new LandingBannerSetter(Request).Set(homeModel, PageAttr.IsApp);

			return View(homeModel);
		}

		public ActionResult HeaderFooterHtml(HeaderTypeEnum headerType, string title = "Gmarket")
		{
			ViewBag.HeaderType = headerType;
			ViewBag.Title = title;
			return View();
		}

		public ActionResult AppMenu()
		{
			PageAttr.ViewHeader = false;
			PageAttr.ViewFooter = false;


			return View();
		}

		public ActionResult PayInfo()
		{
			return View();
		}

		[ChildActionOnly]
		[OutputCache(Duration = 600)]
		public ActionResult ServiceList()
		{
			ServiceListModel serviceList = new ServiceListModel
			{
				ServiceList = new MobileHomeServiceBiz().GetMobileHomeService()
			};

			return PartialView(serviceList);
		}

        //[OutputCache(Duration = 600)]
        //public ActionResult lazyloadtest()
        //{
            //// 기획전(수동 등록 상품)
            //List<MobileShopPlanT> popularPlanList = new MobileHomePlanBiz().GetMobileHomePlan();

            ///*
            //List<MobilePlanT> popularAutoPlanList = new List<MobilePlanT>();
            //if (popularPlanList.Count < 15)
            //{
            //    int rowCount = 15 - popularPlanList.Count;
            //    popularAutoPlanList = new MobilePlanBiz().GetMobilePlanList("", 1, rowCount);
            //}
            //*/

            //List<MobileShopPlanT> popularAutoPlanList = new List<MobileShopPlanT>();
            //List<MobileShopPlanT> popularPlanMergeList = new List<MobileShopPlanT>();

            //if (popularPlanList.Count < 15)
            //{
            //    popularAutoPlanList = new MobileHomePlanBiz().GetMobileHomeAutoPlan(15);
            //    popularPlanMergeList = popularPlanList.Union(popularAutoPlanList, item => item.Sid).ToList<MobileShopPlanT>();
            //}
            //else
            //{
            //    popularPlanMergeList = popularPlanList;
            //}


            //HomeModel model = new HomeModel();

            //List<MobileHomeSpecialT> specialList = new MobileHomeIconBiz().GetMobileHomeSpecial();
            //if (specialList != null && specialList.Count > 0)
            //{

            //    int take = 0;
            //    /*
            //    if (specialList.Count > 1)
            //    {
            //        take = new Random().Next(0, specialList.Count - 1);
            //    }*/


            //    MobileHomeSpecialT specialT = specialList[take];
            //    model.SpecialVisible = true;
            //    model.SpecialText = specialT.banner_text;
            //    model.SpecialLink = specialT.banner_lnk_url;
            //}
            //else
            //{
            //    model.SpecialVisible = false;
            //}


            ////HomeModel model = new HomeModel
            ////{
            ////    AdBannerList = new AdBannerBiz().GetMallManagerBanner("MB_SKT_MAIN_UP_BANNER", 20),
            ////    MobileHomeIconList = new MobileHomeIconBiz().GetMobileHomeIcon(),
            ////    TodaySpecial = new TodaySpecialBiz().GetTodaySpecialPrice(12),
            ////    GoodSeries = new TodaySpecialBiz().GetMainPageBanner(4),
            ////    BestSellerList = BestSellerBiz.GetBestSellerGoodsList(1.ToString(), 30.ToString()),
            ////    PopularPlanList = popularPlanMergeList,
            ////    //PopularAutoPlanList = popularPlanMerge,
            ////    PopularPlanSize = popularPlanMergeList.Count,
            ////    PopupList = new MobilePopupBiz().GetMobilePopupList()
            ////};

            //model.AdBannerList = new AdBannerBiz().GetMallManagerBanner("MB_SKT_MAIN_UP_BANNER", 20);
            //model.MobileHomeIconList = new MobileHomeIconBiz().GetMobileHomeIcon();
            //model.GoodSeries = new TodaySpecialBiz().GetMainPageBanner(4);
            //model.BestSellerList = BestSellerBiz.GetBestSellerGoodsList(1.ToString(), 30.ToString());
            //model.PopularPlanList = popularPlanMergeList;
            ////model.PopularAutoPlanList = popularPlanMerge;
            //model.PopularPlanSize = popularPlanMergeList.Count;
            //model.PopupList = new MobilePopupBiz().GetMobilePopupList();

            //// 2013-05-03 이윤호
            //// 프로모션바 GAdmin으로 등록할 수 있기 전까지 사용할 모델
            //// 반영시간에 따라 자동으로 문구와 URL 변경할 수 있도록 함
            //DateTime now = DateTime.Now;
            //if (now < new DateTime(2013, 5, 6, 10, 0, 0))
            //{
            //    model.LinkText = string.Empty;
            //    model.LinkURL = string.Empty;
            //}
            //else if (now < new DateTime(2013, 5, 7, 10, 0, 0))
            //{
            //    model.LinkText = "오전 10시 선착순~ " + BlueLinkText("하나사면 하나반값!");
            //    model.LinkURL = Urls.MWebUrl + "/Event/m_eventview/index.asp?msid=266";
            //}
            //else if (now < new DateTime(2013, 5, 14, 9, 0, 0))
            //{
            //    model.LinkText = "전국민 " + BlueLinkText("5,000원") + " 쿠폰BOX 증정!";
            //    model.LinkURL = Urls.MWebUrl + "/event/2013/05/0506_campaign/m_main.asp";
            //}
            //else if (now < new DateTime(2013, 5, 21, 10, 0, 0))
            //{
            //    model.LinkText = "출퇴근 시간 6시! " + BlueLinkText("모바일 특가를 잡아라!");
            //    model.LinkURL = Urls.MWebUrl + "/Event/2013/05/0506_time/m_mobile.asp";
            //}
            //else
            //{
            //    model.LinkText = "무한카드혜택 " + BlueLinkText("15개월무이자+즉시할인5%");
            //    model.LinkURL = Urls.MWebUrl + "/event/2013/05/0521_card/card.asp";
            //}

            //return View(model);
        //}

        public ActionResult LeftDrawer()
        {
						ViewBag.HeaderTitle = "카테고리 전체보기";
						PageAttr.HeaderType = HeaderTypeEnum.Simple;
            ApiResponse<MobileDrawerTotal> Result = new DrawerBiz().GetMobileDrawerTotal();
            
            foreach (HomeService entity in Result.Data.HomeService)
            {
                entity.Fcd = getFCD(entity.ServiceText);
            }

            return View(Result.Data);
        }

		public ActionResult SFCNotice()
		{
			return View();
		}

		public ActionResult IPCheck()
		{
			string ipAddress = UserUtil.IPAddressBySecure();
			bool isUplus = new LGUPlusDataFreeBiz().IsUPlusIpAddress(ipAddress);

			ViewBag.ipAddress = ipAddress;
			ViewBag.isUplus = isUplus;
			return View();
		}

        private bool IsUPlusIpAddress()
        {
            string ipAddress = UserUtil.IPAddressBySecure();
            return new LGUPlusDataFreeBiz().IsUPlusIpAddress(ipAddress);
        }

		public List<GmarketTodayItemT> GetGmarketTodayItems()
		{
			if (HttpRuntime.Cache[Const.CACHE_GMARKET_TODAY_ITEMS] == null)
			{
				// 데이터를 최초로 가져올 경우 
				List<GmarketTodayItemT> itemResult = LoadGmarketTodayItems();
				// 1분마다 데이터 갱신
				HttpRuntime.Cache.Insert(Const.CACHE_GMARKET_TODAY_ITEMS, itemResult, null, DateTime.Now.AddMinutes(1),
									Cache.NoSlidingExpiration);
			}

			if (HttpRuntime.Cache[Const.CACHE_GMARKET_TODAY_ITEMS] == null)
			{
				return new List< GmarketTodayItemT>() ;
			}

			return (List<GmarketTodayItemT>)HttpRuntime.Cache[Const.CACHE_GMARKET_TODAY_ITEMS];
		}

        protected string getFCD(string text)
        {
            string result = string.Empty;

            switch (text)
            {
                case "플러스존": return "716520121";
                case "쿠폰존": return "716520122";
                case "G마켓베스트": return "716520123";
                case "패밀리G9": return "716520124";
                case "해외직구": return "716520125";
                case "Giftcard": return "716520126";
                case "스마일페이": return "716520127";
                case "스마트배송": return "716520128";
                case "e쿠폰": return "716520129";
                case "신세계백화점": return "716520130";
                case "슈퍼딜": return "716520131";
                case "사업자혜택": return "716520132";
                case "홈플러스 당일배송": return "716520133";
                case "항공/여행": return "716520134";
                default: return "";
            }
        }

		protected List<GmarketTodayItemT> LoadGmarketTodayItems()
		{
			List<GmarketTodayItemT> itemResult = new List<GmarketTodayItemT>();
			
			TodaySpecialBiz todaySpecialBiz = new TodaySpecialBiz();

			// 오늘만 특가, 슈퍼딜
			List<GmarketTodayItemT> itemList = todaySpecialBiz.GetGmarketTodayItems();

			if (itemList != null)
			{
				// 32개가 넘을 경우 32개로 TRIM
				if (itemList.Count > 32)
					itemList = itemList.GetRange(0, 32);

				List<string> itemNoList = new List<string>();

				for (int i = 0; i < itemList.Count; i++)
				{
					itemNoList.Add(itemList[i].GoodsCode);
				}

				itemResult = voyagerQuery(itemNoList, itemList);

				if (itemResult != null && itemList.Count == itemResult.Count) return itemResult;
			}

			// 베스트 슈퍼딜을 추가로 가져옴.(오늘만 특가, 슈퍼딜에서 품절 혹은 모바일판매불가상품이 있을 경우)
			List<GmarketTodayBestSuperDealT> todayBestSuperDealList = todaySpecialBiz.SelectSuperDealExtra(DateTime.Now);

			if (todayBestSuperDealList != null && todayBestSuperDealList.Count > 0)
			{
				List<GmarketTodayItemT> bestSuperDealItems = todayBestSuperDealList.ToList<GmarketTodayItemT>();

				// 베스트 슈퍼딜을 하나씩 가져와서 Voyager 를 태운뒤에 itemResult 에 추가한다.
				// 루프를 돌려서 voyager 를 태우는 이유는 이 데이터가 Caching 되기 때문임.
				for (int i = 0; i < bestSuperDealItems.Count; i++)
				{
					List<string> list = new List<string> { bestSuperDealItems[i].GoodsCode };

					// GoodsCode 가 이미 itemResult 에 추가돼 있는지 체크한다.
					bool bAlreadyAdded = itemResult.Exists(
						delegate(GmarketTodayItemT item)
						{
							return item.GoodsCode.Equals(list[0]);
						});

					if (bAlreadyAdded) continue;

					// 추가되지 않은 gdno 이므로 voyager 를 호출해서 add 한다.
					List<GmarketTodayItemT> tempList = voyagerQuery(list, bestSuperDealItems);

					if (tempList.Count == 1)
					{
						itemResult.AddRange(tempList);

						if (itemList.Count == itemResult.Count) break;
					}
				}
			}

			if ( itemResult == null )
				itemResult = new List<GmarketTodayItemT>();

			return itemResult;
		}

		protected List<GmarketTodayItemT> voyagerQuery(List<string> itemNoList, List<GmarketTodayItemT> itemList )
		{
			List<GmarketTodayItemT> itemResult = new List<GmarketTodayItemT>();

			if (itemNoList != null && itemNoList.Count > 0 && itemList != null && itemList.Count > 0)
			{
				SearchItemT[] voyagerResult = GetItems(itemNoList, "rank_point_desc");

				if (voyagerResult != null)
				{
					for (int i = 0; i < voyagerResult.Length; i++)
					{
						if (voyagerResult[i] != null)
						{
							GmarketTodayItemT item = null;

							for (int j = 0; j < itemList.Count; j++)
							{
								if (itemList[j] != null && itemList[j].GoodsCode == voyagerResult[i].GdNo.ToString())
								{
									item = itemList[j];
									break;
								}
							}

							if (item != null)
							{
								item.GoodsCode = voyagerResult[i].GdNo.ToString();
								item.OriginalPrice = voyagerResult[i].SellPrice.ToString();
								if (voyagerResult[i].Discount != null)
									item.Price = (voyagerResult[i].SellPrice - voyagerResult[i].Discount.DiscountPrice).ToString();
								else
									item.Price = voyagerResult[i].SellPrice.ToString();

								try
								{
									int d = (int)((double)voyagerResult[i].Discount.DiscountPrice /
												(double)voyagerResult[i].SellPrice * 100.0);
									item.DiscountRate = d;
								}
								catch (Exception ex)
								{
									item.DiscountRate = 0;
								}

								if (voyagerResult[i].Delivery != null && false == string.IsNullOrEmpty(voyagerResult[i].Delivery.DeliveryInfo))
									item.DeliveryInfo = voyagerResult[i].Delivery.DeliveryInfo;
								else
									item.DeliveryInfo = string.Empty;

								itemResult.Add(item);
							}
						}
					}
				}
			}

			return itemResult;
		}

		protected SearchItemT[] GetItems(List<string> itemNoList, string sortType)
		{
			SearchItemT[] result = null;

			ItemFilter filter = new ItemFilter();
			filter.ItemNoList = itemNoList;

			/*
			 * 카테고리 제한해제. 김웅석대리 요청. 김대용 작업.
			List<string> categoryNotOrList = new List<string>();
			categoryNotOrList.Add("200001957");
			categoryNotOrList.Add("200001468");
			categoryNotOrList.Add("200001955");
			categoryNotOrList.Add("200001578");
			categoryNotOrList.Add("200000892");
			categoryNotOrList.Add("200001530");
			categoryNotOrList.Add("200002122");
			categoryNotOrList.Add("200001456");
			categoryNotOrList.Add("200001078");
			categoryNotOrList.Add("200002064");
			filter.CategoryNotOrList = categoryNotOrList;
			*/

			List<int> tradewayNotOrList = new List<int>();
			tradewayNotOrList.Add((int)TradeWay.ReservedItem);
			filter.TradewayNotOrList = tradewayNotOrList;

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
				default:
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
			}
			#endregion

			result = new SearchItemBiz().GetItems(filter, 0, itemNoList.Count, sc);

			return result;
		}
	}
}

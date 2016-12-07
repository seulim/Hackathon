using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMobile.Service.Search;
using GMobile.Service.Vertical;
using GMobile.Data.Diver;
using GMobile.Data.Voyager;
using GMobile.Data.DisplayDB;
using Arche.Data.Voyager;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Extensions;


namespace GMKT.GMobile.Web.Controllers
{
	public class VerticalController : GMobileControllerBase
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Shop(int shopSeq = 1)
		{
            
			// step1 - shopSeq로 기본 정보 조회
			MobileShopKindT shopInfo = new MobileShopKindBiz().GetMobileShopKind(shopSeq);

			ViewBag.HeaderTitle = shopInfo.ShopNm;

			// 전문관 배너
			List<MobileShopBannerT> banners = new MobileShopBannerBiz().GetMobileShopBanner(shopSeq);

			// 전문관 베스트 상품(수동 등록 상품)
			// 30건 가져오자. 
			List<MobileShopBestT> bestItems = new MobileShopBestBiz().GetMobileShopBest(shopSeq, 30);

			// 수동입력한 건이 30건 보다 작으면서 자동로직을 선택시 자동 로직으로 가져오자.
			List<SpecialShopT> shopGoods = new List<SpecialShopT>();
			if (bestItems.Count < 30 && !shopInfo.ShopBestGdType.Equals('0'))
			{
				int rowCount = 30 - bestItems.Count;
				shopGoods = new SpecialShopBiz().GetSpecialShopBestSeller(shopInfo.ShopBestGdType, rowCount);
			}

			// 전문관 인기 기획전(수동 등록 상품)
			// 15건 가져오자.
			List<MobileShopPlanT> popularPlans = new MobileShopPlanBiz().GetMobileShopPlan(shopSeq, 15);

			// 수동입력한 건이 15건 보다 작으면서 자동로직을 선택시 자동 로직으로 가져오자.
			List<MobileShopPlanT> popularAutoPlans = new List<MobileShopPlanT>();
			List<MobileShopPlanT> popularPlansMerge = new List<MobileShopPlanT>();
			if (popularPlans.Count < 15 && !shopInfo.ShopPopularGdMdType.Equals('0'))
			{
				popularAutoPlans = new MobileShopPlanBiz().GetMobileShopPlanAuto(shopInfo.ShopPopularGdMdType, 15);
				popularPlansMerge = popularPlans.Union(popularAutoPlans, item => item.Sid).ToList<MobileShopPlanT>();
			}
			else
			{
				popularPlansMerge = popularPlans;
			}

			// Result 세팅
			SpecialShopModel model = new SpecialShopModel
			{
				ShopInfo = shopInfo,
				ShopBanners = banners,
				ShopBestItems = bestItems,
				ShopBestAutoItems = shopGoods,
				ShopPopularPlans = popularPlansMerge,
				//ShopPopularAutoPlans = popularAutoPlans,
				BestResultSize = bestItems.Count + shopGoods.Count,
				PlanResultSize = popularPlansMerge.Count
			};

			//System.Web.HttpContext.Current.Response.Write(model.PlanResultSize);

            ViewBag.Title = shopInfo.ShopNm + " - G마켓 모바일";

            /* Landing Banner */
            new LandingBannerSetter(Request).Set(model, PageAttr.IsApp);

			return View(model);
		}

        public ActionResult MocaRedirect()
        {
            return View();
        }
	}
}

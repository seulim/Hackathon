using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Web;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Util;
using GMKT.GMobile.Data.SellerShop;
using GMKT.GMobile.Biz.SellerShop;
using GMKT.GMobile.Web.Util;
using GMKT.GMobile.CommonData;

namespace GMKT.GMobile.Web
{
	public class SellerShopBaseController : GMobileControllerBase
    {
		protected string Alias
		{
			get
			{
				var alias = this.ControllerContext.RouteData.Values["alias"];
				if (alias != null)
				{
					return alias.ToString();
				}
				else 
				{
					return Request.QueryString["alias"] != null ? Request.QueryString["alias"] : (Request.Form["alias"] != null ? Request.Form["alias"] : "" );
				}
			}
		}
		protected ShopData Shop { get; set; }
		protected SellerData Seller { get; set; }
		protected string RedirectUrl { get; set; }

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
			if (this.RedirectUrl != null)
			{
				filterContext.Result = new RedirectResult(this.RedirectUrl);
			}
		}


		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			
			base.Initialize(requestContext);
			this.RedirectUrl = null;
			if (String.IsNullOrEmpty(Alias))
			{
				//requestContext.HttpContext.Response.Redirect(Urls.MobileWebUrl);
				this.RedirectUrl = Urls.MobileWebUrl;
				return;
			}

			ViewBag.HasShortcutIcon = false;

			this.Shop = null;
			//샵정보 조회
			this.Shop = new SellerShopApiBiz().GetShop(Alias);
			if (this.Shop == null)
			{
				this.RedirectUrl = Urls.MobileWebUrl;
				return;
			}
			else
			{
				if (this.Shop.DisplayInfo != null && this.Shop.DisplayInfo.MobileShopDispNo > 0)
				{
					if (Char.ToUpper(this.Shop.DisplayInfo.ShopLevel) == 'P')//plus
					{
						if (this.Shop.DisplayInfo.MainImageDispType >= 0)
						{
							this.Shop.DisplayInfo.MainImageDisplayClass = GetMainImageDisplayClass(this.Shop.DisplayInfo.MainImageDispType);
						}

						CheckAppVersion checkAppVersion = new CheckAppVersion();
						if (PageAttr.IsAndroidApp && checkAppVersion.CheckOverVersion(PageAttr.AppVersion, checkAppVersion.GetAppVersion("5.3.7")))
						{
							ViewBag.HasShortcutIcon = true;
						}

						if(this.Shop.DisplayInfo.MainImageDispType == 7)
						{
							this.Shop.DisplayInfo.MainImageDisplayClass = "view--brand";
						}
					}
					else //basic
					{
						this.Shop.DisplayInfo.MainImageDisplayClass = "view--basic";
					}

					//카테고리 설정 공통
					if(this.Shop.DisplayInfo.CategorySetupApplyType == 2) {
						this.Shop.CategoryDisplay = this.Shop.DisplayInfo.MobileCategoryUseType;
						this.Shop.GeneralCategoryDisplayType = this.Shop.DisplayInfo.MobileGmktCategoryDispType;
						this.Shop.ShopCategoryDisplayType = this.Shop.DisplayInfo.MobileShopCategoryDispType;
						this.Shop.GoodsCountExposeYn = this.Shop.DisplayInfo.MobileCategoryGdCntExposeYn;
					}
				}
				else //신규 모바일샵 세팅 안한 경우 
				{
					this.Shop.DisplayInfo = new ShopBasicT();
					this.Shop.DisplayInfo.MainImageDisplayClass = "view--basic";
				}
			}

			//판매자정보 조회
			this.Seller = new SellerShopApiBiz().GetSeller(this.Shop.SellerId);
			if (this.Seller == null)
			{
				this.RedirectUrl = Urls.MobileWebUrl;
				return;
			}
			//회원 상태가 정상이 아닐경우
			if (this.Seller.Stat != "S2")
			{
				this.RedirectUrl = Url.Action("ShopClosed", "Etc");
				return;
			}

			this.Shop.Seller = this.Seller;

			//관심매장
			if (PageAttr.IsLogin)
			{
				this.Shop.Favorite = new SellerShopApiBiz().GetFavoriteShopInfo(gmktWebContext.UserProfile.CustNo, this.Seller.Id);
			}

			if (this.Shop.Favorite == null)
			{
				this.Shop.Favorite = new FavoriteShopData();
			}

			PageAttr.HeaderType = HeaderTypeEnum.Minishop;
			PageAttr.HeaderCode = EncodeUtil.custNoEncode(this.Shop.SellerId);
			ViewBag.Shop = this.Shop;
			ViewBag.Alias = this.Alias;
			ViewBag.IsNavigation = false;

            //Navigation
            bool isShopCategory = (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryOnly) || (this.Shop.CategoryDisplay == CategoryDisplay.ShopCategoryFirst);
            if (isShopCategory)
                ViewBag.RootLevel = new SellerShopApiBiz().GetRootCategoryLevel(ShopCategoryDisplayType.LargeMediumSmallCategory);
            else
                ViewBag.RootLevel = new SellerShopApiBiz().GetRootCategoryLevel(this.Shop.ShopCategoryDisplayType);
            //Navigation
		}

		protected string GetMainImageDisplayClass(int mainImageDispType)
		{
			//1.상품전시형,2-소이미지형,3-대이미지형,4-스킨형,5-매거진형,6-동영상링크

			//* 메인이미지에 따른 class 정의
			//베이직 (Basic) : <div class="view--basic"> , 메인이미지 미사용 or 메인 외 페이지 : <div class="view--basic slim">
			//상품전시형 (Basic/Plus) : <div class="view--display"> , 메인이미지 미사용 or 메인 외 페이지 : <div class="view--display slim">
			//소이미지형 (Plus) : <div class="view--img-small"> , 메인이미지 미사용 or 메인 외 페이지 : <div class="view--img-small slim">
			//대이미지형 (Plus) : <div class="view--img-full"> , 메인이미지 미사용 or 메인 외 페이지 : <div class="view--img-full slim">
			//스킨형 (Plus) : <div class="view--skin"> , 메인이미지 미사용 or 메인 외 페이지 : <div class="view--skin slim">
			//매거진형 (Plus) : <div class="view--magazine"> , 메인이미지 미사용 or 메인 외 페이지 : <div class="view--magazine slim">
			//동영상링크형 (Plus) : <div class="view--video"> , 메인이미지 미사용 or 메인 외 페이지 : <div class="view--video slim">
			switch (mainImageDispType)
			{
				case 0:
					return "view--basic";
				case 1:
					return "view--display";
				case 2:
					return "view--img-small";
				case 3:
					return "view--img-full";
				case 4:
					return "view--skin";
				case 5:
					return "view--magazine";
				case 6:
					return "view--video";
				default:
					return "view--basic";
			}
		}
	}
}

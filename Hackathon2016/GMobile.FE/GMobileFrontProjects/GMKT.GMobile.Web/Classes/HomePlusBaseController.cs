using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Data.HomePlus;
using GMKT.GMobile.Data;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Biz.HomePlus;
using GMKT.Web.Context;
using Nova.Thrift;
using GMKT.Web.Membership;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Web.Classes
{
	public class HomePlusBaseController : GMobileControllerBase
	{
		protected SpecialShopPrimaryAddrT shippingAddr { get; set; }
		protected SpecialShopBranchInfoTimeTable settedBranch { get; set; }
		protected string sellCustNo { get; set; }
		protected List<SpecialShopCategoryT> categoryList { get; set; }
		protected List<CartListDetailI> cartList { get; set; }
        protected string branchName { get; set; }

		protected string RedirectUrl { get; set; }

		//protected override void OnActionExecuting(ActionExecutingContext filterContext)
		//{
		//    base.OnActionExecuting(filterContext);
		//    if (this.RedirectUrl != null)
		//    {
		//        filterContext.Result = new RedirectResult(this.RedirectUrl);
		//    }
		//}

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			base.Initialize(requestContext);
			
			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket 홈플러스";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/HomePlus";
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket 홈플러스";
			#endregion

			//홈플러스 판매자 ID : SHomeplus / 판매자번호 : 117205821
			this.sellCustNo = "117205821";

			this.shippingAddr = null;
			this.settedBranch = null;
			this.cartList = new List<CartListDetailI>(); ;
            this.branchName = string.Empty;

			if (PageAttr.IsLogin)
			{
				string cookieZipCd = string.Empty;//new GMobileCookieHelper().GetCookieValue(COOKIENAME_ADDR_INFO, COOKIENAME_ZIP_CODE);

				//if (String.IsNullOrEmpty(cookieZipCd))
				//{
					//당일배송관 설정 여부 chk
					SpecialShopPrimaryAddrT addrApiResult = new HomePlusApiBiz().GetSpecialShopPrimaryAddr(gmktUserProfile.CustNo, this.sellCustNo);
					if (addrApiResult != null)
					{
						//배송보낼 고객주소
						this.shippingAddr = addrApiResult;
						//고객이 설정한 당일배송관 정보 & 배송시간표
						cookieZipCd = this.shippingAddr.FriendZipCode;
						//new GMobileCookieHelper().SetCookieValue(COOKIENAME_ADDR_INFO, COOKIENAME_ZIP_CODE, cookieZipCd);
					}
				//}
				//else
				//{
				//    this.shippingAddr = new SpecialShopPrimaryAddrT();
				//    this.shippingAddr.FriendZipCode = cookieZipCd;
				//}

				SpecialShopZipBranchMatchingT branch = new HomePlusApiBiz().GetSpecialShopBranchInfo(cookieZipCd, this.sellCustNo);
				if (branch != null)
				{
					this.settedBranch = new SpecialShopBranchInfoTimeTable();
					this.settedBranch.BranchInfo = branch;
				}

				ViewBag.settedBranch = this.settedBranch;
				ViewBag.shippingAddr = this.shippingAddr;

				if (this.settedBranch != null && this.settedBranch.BranchInfo != null)
				{
					CartListResultI cartResult = new HomePlusApiBiz().GetCartResult(this.settedBranch.BranchInfo.BranchCd);
					if (cartResult != null && cartResult.Result != null && cartResult.Result.ReturnCode == "000")
					{
						this.cartList = cartResult.CartListDetail;
					}
				}
				ViewBag.cartList = this.cartList;

			}

			//홈플러스 카테고리 날개

			SpecialShopCategory cateInfo = new HomePlusApiBiz().GetSpecialShopCategoryList(this.sellCustNo);
			if (cateInfo != null)
			{
				this.categoryList = cateInfo.CategoryList;
				ViewBag.categoryList = this.categoryList;
				ViewBag.categoryImage = cateInfo.CategoryImage.MobileImagePath;
			}
		}

	}
}

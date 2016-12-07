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
using GMKT.GMobile.Biz.SellerShop;
using GMKT.GMobile.Data.SellerShop;

namespace GMKT.GMobile.Web.Controllers
{
	public class ShopGateController : GMobileControllerBase
	{
		public static readonly int FIRST_PAGE_NO = 1;
		public static readonly int BEST_ITEM_PAGE_SIZE = 3;
		
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

		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			base.Initialize(requestContext);

			// 암호화된 custNo로 alias를 가져온다.
			if (requestContext != null && requestContext.HttpContext != null && requestContext.HttpContext.Request != null && false == string.IsNullOrEmpty(requestContext.HttpContext.Request.QueryString["custNo"]))
			{
				string custNo = requestContext.HttpContext.Request.QueryString["custNo"].ToString();

				if (false == string.IsNullOrEmpty(custNo))
				{
					try
					{
						if (false == string.IsNullOrEmpty(custNo))
						{
							string decCustNo = EncodeUtil.custNoDecode(custNo);
							requestContext.RouteData.Values["Alias"] = new ShopBiz().GetShopDomain(decCustNo);

							//샵정보 조회
							this.Shop = new SellerShopApiBiz().GetShop(Alias);//new ShopBiz().GetShop(Alias);
						}
					}
					catch (Exception)
					{
						requestContext.HttpContext.Response.End();
					}
				}
			}
		}

		public RedirectToRouteResult Index(string custNo, string nickName = "")
		{
			if (!String.IsNullOrEmpty(Alias) && this.Shop != null)
				return RedirectToAction("Index", "SellerShop", new { alias = Alias });
			else
			    return RedirectToAction("Search", "Search", new { keyword = nickName, sellCustNo = custNo });
		}

		public ActionResult BestItem(string custNo, string lcId, string goodsCode="")
		{
			List<BestItemModel> model = new List<BestItemModel>();
            ViewBag.GoodsCode = goodsCode;

			string sellerCustNo = null;
			if (this.Shop != null && false == string.IsNullOrEmpty(this.Shop.SellerId))
			{
				sellerCustNo = this.Shop.SellerId;
			}
			else if (!String.IsNullOrEmpty(custNo))
			{
				sellerCustNo = EncodeUtil.custNoDecode(custNo);
			}

			if (false == string.IsNullOrEmpty(sellerCustNo))
			{
				SearchItemT[] mainItem = new ShopSearchBiz().GetSellerItem(sellerCustNo, string.Empty, lcId, false, FIRST_PAGE_NO, BEST_ITEM_PAGE_SIZE, DisplayOrder.SellPointDesc, PageAttr.Current.Exid);				

				// 상품명 필터링
				if (mainItem != null)
				{
					foreach (var eachItem in mainItem)
					{					
						if (eachItem != null)
						{
							model.Add(new BestItemModel()
							{
								Name = ValidationUtil.GetFilteredGoodsName(eachItem.Name),
								Price = GetPrice(eachItem),
								PriceAppendString = IsFreeItem(eachItem) ? "" : "원",
								ImageUrl = BizUtil.GetGoodsImagePath(eachItem.GdNo.ToString(), "M3"),
								LandingUrl = string.Format("{0}?goodscode={1}", Urls.MItemUrl, eachItem.GdNo),
                                GoodsCode = eachItem.GdNo.ToString()
							});
						}
					}
				}
			}		
			
			return View(model);
		}

		private bool IsFreeItem(SearchItemT item)
		{
			return item.SellPrice == 2;
		}

		private string GetPrice(SearchItemT item)
		{
			//휴대폰 상품 중분류 하드코딩(BC 8393)
			List<string> joinItemCategories = new List<string>{"200000801","200001255","200001256","200000800","200001253","200001254","200000802","200001257","200001258","200002090","200001152"};
#if DEBUG
			//Rental 중분류 하드코딩(BC 8393)
			joinItemCategories.Add("200002521");
#else
			joinItemCategories.Add("200002528");
#endif

			if(IsFreeItem(item))
			{
				return joinItemCategories.Contains(item.MCode.ToString()) ? "가입상품" : "무료";
			}
			else if(item.Discount != null && item.Discount.DiscountPrice > 0)
			{
				return (item.SellPrice - item.Discount.DiscountPrice).ToString("N0");
			}
			else
			{
				return item.SellPrice.ToString("N0");
			}
		}
  }
}
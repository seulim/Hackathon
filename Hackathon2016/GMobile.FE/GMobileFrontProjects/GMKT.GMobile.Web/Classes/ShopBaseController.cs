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

namespace GMKT.GMobile.Web
{
	public class ShopBaseController : GMobileControllerBase
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

		protected string GoodsCode
		{
			get
			{
				return Request.QueryString["goodscode"] != null ? Request.QueryString["goodscode"] : (Request.Form["goodscode"] != null ? Request.Form["goodscode"] : "");
			}
		}
		protected ShopT Shop { get; set; }
		protected SellerT Seller { get; set; }
		protected MobileShopInfoT Mobile { get; set; }
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
			this.RedirectUrl = Url.Action("Index", "SellerShop", new { alias = Alias, goodscode = GoodsCode });
			return;
		}
	}
}

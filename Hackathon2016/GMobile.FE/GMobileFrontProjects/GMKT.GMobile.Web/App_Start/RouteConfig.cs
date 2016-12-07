using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GMKT.GMobile.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "SiteOffForApp",
				url: "sync/siteoff.html",
				defaults: new { controller = "SiteOff", action = "Index", id = UrlParameter.Optional },
				namespaces: new string[] { "GMKT.GMobile.Web.Controllers" }
			);

			routes.MapRoute(
				name: "BestTab",
				url: "Display/BestSellerList",
				defaults: new { controller = "Best", action = "Index", id = UrlParameter.Optional },
				namespaces: new string[] { "GMKT.GMobile.Web.Controllers" }
			);

			routes.MapRoute(
				name: "GStampTab",
				url: "GStamp/{id}",
				defaults: new { controller = "GStamp", action = "Index", id = UrlParameter.Optional },
				constraints: new { id = "^[0-9]+" }
			);

			routes.MapRoute(
				name: "ShopSrpLp",
				url: "Shop/{alias}/{action}/{id}",
				defaults: new { controller = "Shop", action = "Index", id = UrlParameter.Optional },
				namespaces: new string[] { "GMKT.GMobile.Web.Controllers" }
			);

			routes.MapRoute(
				name: "SellerShopSrpLp",
				url: "SellerShop/{alias}/{action}/{id}",
				defaults: new { controller = "SellerShop", action = "Index", id = UrlParameter.Optional },
				namespaces: new string[] { "GMKT.GMobile.Web.Controllers" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new string[] { "GMKT.GMobile.Web.Controllers" }
			);
		}
	}
}
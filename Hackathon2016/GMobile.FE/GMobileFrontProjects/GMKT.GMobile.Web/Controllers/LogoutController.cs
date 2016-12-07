using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.Web.Context;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Web.Controllers
{
	public class LogoutController : GMobileControllerBase
	{
		public ActionResult Index()
		{
			GMobileWebContext.Current.MemberLogOut();

			return new RedirectResult(Urls.MobileWebUrl);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMKT.GMobile.Web.Controllers
{
	public class AppGateController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult V2()
		{
			return View("IndexV2");
		}
	}
}

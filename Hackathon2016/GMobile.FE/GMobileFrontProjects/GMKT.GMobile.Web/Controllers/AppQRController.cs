using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMKT.GMobile.Web.Controllers
{
	public class AppQRController : GMobileControllerBase
    {
        //
        // GET: /AppQR/

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult OtherStores()
		{
			return View();
		}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Web.Controllers.EventV2
{
	public class SmilePayController : Controller
	{
		public ActionResult Index()
		{
			return Redirect( Urls.MWebUrl + "/Event/m_eventview/index.asp?msid=685" );
		}
	}
}

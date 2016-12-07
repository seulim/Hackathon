using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMobile.Service.Home;
using GMobile.Data.DisplayDB;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Web.Controllers
{
	public class MobileAppController : GMobileControllerBase
    {
        //
        // GET: /MobileApp/

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult ShowAppNotice( int seq )
		{
			List<MobileAppNoticeT> list = new MobileNoticeBiz().GetAppNoticeWithSeq( seq );
			if (list != null && list.Count > 0)
				ViewBag.AppNotice = list[0];
			return View();
		}

		public ActionResult GetCurrentAppNotice( string osType = "" )
		{
			string resCode = "";
			string resMsg = "정상";
			string url = "";
			string seq = "";

			MobileAppNoticeT notice = null;

			if ( osType == null || (!"I".Equals( osType.ToUpper() ) && !"A".Equals( osType.ToUpper() ) ) )
			{
				resCode = "9999";
				resMsg = "osType 이 올바르지 않습니다.";
			}
			else
			{
				List<MobileAppNoticeT> list = null;
				list = new MobileNoticeBiz().GetCurrentAppNotice(osType);

				if (list != null && list.Count > 0)
				{
					notice = list[0];
					resCode = "0000";
					resMsg = "정상";
					url = HttpUtility.UrlEncode(Urls.MobileWebUrl +
											"/mobileapp/showAppNotice" + "?seq=" + notice.Seq);
					seq = notice.Seq + "";
				}
				else
				{
					resCode = "0001";
					resMsg = "공지내용이 존재하지 않습니다.";
				}
			}

			return Content( "RESCODE=" + resCode + "|" + 
							"RESMSG=" + resMsg + "|" + 
							"URL=" + url + "|" + 
							"SEQ=" + seq );
		}

		public ActionResult Redirect()
		{
			return View();
		}
    }
}

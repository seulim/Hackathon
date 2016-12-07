using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using System.Text;
using System.Net;

namespace GMKT.GMobile.Web.Controllers
{
    public class EventController : GMobileMemberControllerBase
    {
		public ActionResult EventList()
		{
			ViewBag.HeaderTitle = "모바일 고객센터";
            ViewBag.Title = "응모결과 보기 - G마켓 모바일";
			return View();
		}

		public ActionResult EventListJson(string OrderKind = "S", int pageNo = 1, int pageSize = 10 )
        {
			string userInfoCookie = Request.Cookies["user%5Finfo"].Value;

			string url = "http://eventnet.gmarket.co.kr/Partial/JsonDefaultMyApplicantEventList?strSdate=" + 
				HttpUtility.UrlEncode( DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss" ) ) +
				"&strEdate=" + HttpUtility.UrlEncode(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) +
				"&PageNo=" + pageNo + 
				"&PageSize=" + pageSize +
				"&OrderKind=" + OrderKind;

			WebClient client = new WebClient();
			client.Headers.Add(HttpRequestHeader.Cookie, "user%5Finfo=" + userInfoCookie);
			client.Encoding = Encoding.UTF8;
			string html = client.DownloadString(url);

			return Content( html );
        }
    }
}

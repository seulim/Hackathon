using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Data.Ad;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Util;
using GMKT.Component.Member;

namespace GMKT.GMobile.Web.Controllers
{
    public class AdController : GMobileControllerBase
    {
        //
        // GET: /Ad/
        public ActionResult LandingBanner()
        {
            return View();
        }

		public ActionResult CartDirect()
        {
            return View();
        }

        public ActionResult SponsorLink(string url)
        {
            ViewBag.IframeUrl = string.IsNullOrEmpty(url) ? string.Empty : url;
            return View();
        }

        [HttpPost]
        public JsonResult GetSponsorLinkList(string channel = "", int count = 0, string primeKeyword = "", string moreKeyword = "", string largeCategory = "", string middleCategory = "", string smallCategory = "", string referrer = "", string menuName = "")
        {
            string ip = UserUtil.IPAddressBySecure();
			string url = string.Empty;
			if (Request.UrlReferrer != null)
			{
				url = Request.UrlReferrer.ToString();
			}

            string ua = Request.UserAgent;
            List<SponsorLinkDataT> result = new List<SponsorLinkDataT>();
            List<SponsorLinkDataT> tempResult = new SponsorLinkApiBiz().GetSponsorLinkList(channel, count, primeKeyword, moreKeyword, largeCategory, middleCategory, smallCategory, ip, url, ua, referrer, menuName);
            
            if (tempResult != null)
            {
                result = tempResult;
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult GetSrpLpFooterDa(string primeKeyword = "", string largeCategory = "", string middleCategory = "", string smallCategory = "")
        {
            FooterDaData result = new FooterDaData();

            string categoryCode = string.Empty;

            if (!string.IsNullOrEmpty(smallCategory.Trim()))
            { 
                categoryCode = smallCategory;
            }
            else if (!string.IsNullOrEmpty(middleCategory.Trim()))
            {
                categoryCode = middleCategory;
            }
            else
            {
                categoryCode = largeCategory;
            }

            FooterDaData srpLpFooterDa = new AdApiBiz().GetSrpLpFooterDa(primeKeyword, categoryCode);

            if (srpLpFooterDa != null)
            {
                result = srpLpFooterDa;
            }

            return Json(result);
        }
    }
}

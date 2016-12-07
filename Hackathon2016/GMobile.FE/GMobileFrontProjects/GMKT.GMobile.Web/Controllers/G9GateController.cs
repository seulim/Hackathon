using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz.G9Gate;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Web.Controllers
{
    public class G9GateController : GMobileControllerBase
    {
        //
        // GET: /G9Gate/

        public ActionResult Index(string redirectUrl)
        {
			PageAttr.ViewHeader = false;
			PageAttr.ViewFooter = false;
			ViewBag.Title = "";

			G9GateModel model = new G9GateModel();
			model.IsLogin = PageAttr.IsLogin;
			model.CustName = gmktUserProfile.CustName;

			G9GateBannerT banner = new G9GateApiBiz().GetG9GateBanner();

			model.BannerImageUrl = (banner != null) ? banner.BannerImg : string.Empty;
			model.BannerLandingUrl = (banner != null) ? banner.BannerLink : string.Empty;
			model.BannerAlt = (banner != null) ? banner.BannerAlterText : string.Empty;

			string channelType = PageAttr.IsApp ? "mobile_app" : "mobile_web";
			string gateAspUrl = string.Format("{0}/challenge/neo_include/login/redirect_gateway.asp?site=g9&channelType={1}", Urls.CoreRootUrl, channelType);			
			
			if (this.IsDev())
			{
				//DEV
				gateAspUrl += "&env=dev";
			}

			model.RedirectUrl = string.Concat(gateAspUrl, "&redir=", HttpUtility.UrlEncode(this.GetG9JaehuUrl(redirectUrl)));

			return View(model);
        }

		private string GetG9JaehuUrl(string redirectUrl)
		{
			string ret = redirectUrl;
			string[] splitted = redirectUrl.Split(new string[]{"#"}, StringSplitOptions.RemoveEmptyEntries);

			bool bDev = this.IsDev();

			if(splitted != null)
			{
				if(splitted.Length > 0)
				{
					string jaehuid = String.Empty;
					if (PageAttr.IsAndroidApp)
					{
						//DEV = 200006257;
						//jaehuid = "200006446";
						jaehuid = bDev ? "200006257" : "200006446";
					}
					else if (PageAttr.IsIphoneApp)
					{
						//DEV = 200006258;
						//jaehuid = "200006453";
						jaehuid = bDev ? "200006258" : "200006453";
					}
					else
					{
						//DEV = 200006256;
						//jaehuid = "200006445";
						jaehuid = bDev ? "200006256" : "200006445";
					}
					ret = splitted[0];
					ret += ret.Contains("?") ? "&jaehuid=" : "?jaehuid=";
					ret += jaehuid;
				}

				for(int i = 1; i < splitted.Length; i++)
				{
					ret += "#" + splitted[i];
				}
			}

			return ret;
		}

		private bool IsDev()
		{
			if (Request != null && Request.Url != null && Request.Url.Host != null && !String.IsNullOrEmpty(Request.Url.Host))
			{
				string host = Request.Url.Host;
				if (host.IndexOf("dev") > -1 || host.IndexOf("local") > -1 || host.IndexOf("md") > -1)
				{
					return true;
				}
			}

			return false;
		}
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ArcheFx.Diagnostics;
using GMKT.Web.Context;

namespace GMKT.GMobile.Web
{
	public class GMobileActionFilter : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			//HttpCookieCollection cookies = filterContext.HttpContext.Request.Cookies;
			//SetCookieWebAppInfo(cookies);
		}

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			HttpCookieCollection cookies = filterContext.HttpContext.Request.Cookies;
			SetCookieWebAppInfo(cookies);
		}

		public void SetCookieWebAppInfo(HttpCookieCollection cookies)
		{
			HttpCookie webAppCookie = new HttpCookie("Web-App-Info");
			string pcidForApp = GMKT.Web.Context.GMobileWebContext.Current.GetPCID();

			if (string.IsNullOrEmpty(pcidForApp))
			{
				webAppCookie.Value = "WEB";
				cookies.Add(webAppCookie);
			}
			else if (pcidForApp.StartsWith("4"))
			{
				//ArcheFx.Diagnostics.SimpleTrace.WriteWarnning(pcidForApp);
				webAppCookie.Value = "IPHONE";
				cookies.Add(webAppCookie);
			}
			else if (pcidForApp.StartsWith("3"))
			{
				//ArcheFx.Diagnostics.SimpleTrace.WriteWarnning(pcidForApp);
				webAppCookie.Value = "ANDROID";
				cookies.Add(webAppCookie);
			}
			else
			{
				webAppCookie.Value = "WEB";
				cookies.Add(webAppCookie);
			}

			// 한번만 처리한다.
			if (cookies["Web-OS-Type"] == null || String.IsNullOrEmpty(cookies["Web-OS-Type"].Value))
			{
				HttpCookie webOSTypeCookie = new HttpCookie("Web-OS-Type");

				string httpUserAgent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] ?? String.Empty;
				httpUserAgent = httpUserAgent.ToLower();
				if (httpUserAgent.Contains("windows") == true)
				{
					webOSTypeCookie.Value = "windows";
				}
				else if (httpUserAgent.Contains("iphone") == true || httpUserAgent.Contains("ipad") == true
					|| (!String.IsNullOrEmpty(pcidForApp) && pcidForApp.StartsWith("4")))
				{
					webOSTypeCookie.Value = "ios";
				}
				else if (httpUserAgent.Contains("android") == true
					|| (!String.IsNullOrEmpty(pcidForApp) && pcidForApp.StartsWith("3")))
				{
					webOSTypeCookie.Value = "android";
				}
				else
				{
					webOSTypeCookie.Value = "unknown";
				}

				cookies.Add(webOSTypeCookie);

			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using GMKT.GMobile.Util;
using GMKT.Web.Mvc;
using GMobile.Data.Tiger;
using ArcheFx.EnterpriseServices;
using GMKT.Framework.EnterpriseServices;
using System.Collections;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Web.Util;
using Newtonsoft.Json;
using GMKT.Web.Context;
using GMKT.Component.Member.Util;
using GMKT.Web.Membership;

namespace GMKT.GMobile.Web
{
	public class GMobileControllerBase : GMobileBaseController
	{
		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{
			base.Initialize(requestContext);

			ViewBag.Title = "G마켓";
		}

		public static void Logger(String lines)
		{
        
			// Write the string to a file.append mode is enabled so that the log
			// lines get appended to  test.txt than wiping content and writing the log
			try
			{
				bool IsExists = System.IO.Directory.Exists("d:\\err_log");

				if (!IsExists)
					System.IO.Directory.CreateDirectory("d:\\err_log");

				System.IO.StreamWriter file = new System.IO.StreamWriter("d:\\err_log\\err_log.txt", true);
				file.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] " + lines);
				file.Close();
			}
			catch (Exception ex)
			{
			}
		}

        protected override void SetJaehuidCookie()
        {
			string jaehuid = System.Web.HttpContext.Current.Request.QueryString[QUERY_STRING_NAME_JAEHUID];

			if (false == string.IsNullOrEmpty(jaehuid))
			{
				this.AddCookie(COOKIE_NAME_JAEHUID, JaehuIdPool.GetInstance().GetMobileJaehuId(jaehuid));
			}
			else if (false == string.IsNullOrEmpty(GMobileWebContext.Current.JahuID))
			{
				if (false == JaehuIdPool.GetInstance().IsValid(GMobileWebContext.Current.JahuID))
				{
					this.AddCookie(COOKIE_NAME_JAEHUID, GMKT.GMobile.Biz.JaehuIdBiz.DEFAULT_MOBILE_JAEHUID);
				}
			}
			else
			{
				this.AddCookie(COOKIE_NAME_JAEHUID, GMKT.GMobile.Biz.JaehuIdBiz.DEFAULT_MOBILE_JAEHUID);
			}
        }

        protected void SetHomeTabName(string homeTabName)
        {
            if (string.IsNullOrEmpty(homeTabName) == true) return;

			//ApiResponse<List<HomeTabNames>> GetHomeTabNames = new MobileCommonBiz_Cache().GetMobileHomeTabNames();
			//if (GetHomeTabNames == null || GetHomeTabNames.Data == null) return;

			List<HomeTabNames> homeTabNames = new MobileCommonBiz_Cache().GetMobileHomeTabList();

			if (homeTabNames == null || homeTabNames.Count < 1)
			{
				homeTabNames = new MobileCommonBiz().GetMobileHomeTabList();
			}

			PageAttr.HomeTabNames = homeTabNames;
            PageAttr.HomeTabName = homeTabName;
        }

		#region case에 따른 자바스크립트 액션

		/// <summary>
		/// Alert 메시지 후 referrer가 있을 경우 history.back(), 없을경우 window.close
		/// </summary>
		/// <param name="message">alert 메시지</param>
		[NonAction]
		protected ContentResult AlertMessageAndHistorybackOrClose(string message, string backCnt)
		{
			string tempStr = "<script type='text/javascript'>document.domain='gmarket.co.kr';alert('" + message + "'); if(opener!=null){self.opener = self; window.close();}else{history.go(" + backCnt + ");}</script>";
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}

		/// <summary>
		/// top 부모페이지 redirect(자식페이지가 iframe으로 삽입되어 있을 때)
		/// </summary>
		/// <param name="href">redirect url</param>
		[NonAction]
		protected ContentResult TopLocationChange(string href)
		{
			string tempStr = "<script type='text/javascript'>document.domain='gmarket.co.kr'; window.top.location.href='" + href + "';</script>";
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}

		/// <summary>
		/// Alert 메시지 후 top 부모페이지 redirect(자식페이지가 iframe으로 삽입되어 있을 때)
		/// </summary>
		/// <param name="message">alert 메시지</param>
		/// <param name="href">redirect url</param>
		[NonAction]
		protected ContentResult AlertMessageAndTopLocationChange(string message, string href)
		{
			string tempStr = "<script type='text/javascript'>document.domain='gmarket.co.kr'; alert('" + message + "'); window.top.location.href='" + href + "';</script>";
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}

		/// <summary>
		/// Alert 메시지 후 자기페이지 redirect
		/// </summary>
		/// <param name="message">alert 메시지</param>
		/// <param name="href">redirect url</param>
		[NonAction]
		protected ContentResult AlertMessageAndLocationChange(string message, string href)
		{
			string tempStr = "<script type='text/javascript'>document.domain='gmarket.co.kr'; alert('" + message + "'); document.location.href='" + href + "';</script>";
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}

		/// <summary>
		/// Alert 메시지 후 현재창 Close(자식페이지가 popup으로 뜰 때)
		/// </summary>
		/// <param name="message">alert 메시지</param>
		/// <param name="href">redirect url</param>
		[NonAction]
		protected ContentResult AlertMessageAndClose(string message)
		{
			string tempStr = "<script type='text/javascript'>document.domain='gmarket.co.kr'; alert('" + message + "'); self.opener=self; window.close();</script>";
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}

		/// <summary>
		/// Alert 메시지 후 opener페이지 redirect, 현재창 Close(자식페이지가 popup으로 뜰 때)
		/// </summary>
		/// <param name="message">alert 메시지</param>
		/// <param name="href">redirect url</param>
		[NonAction]
		protected ContentResult AlertMessageAndOpenerLocationChange(string message, string href)
		{
			string tempStr = "<script type='text/javascript'>document.domain='gmarket.co.kr'; alert('" + message + "'); window.opener.document.location.href='" + href + "'; self.opener=self; window.close();</script>";
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}

		/// <summary>
		/// Alert 메시지 후 현재창, 부모창 Close(자식페이지가 popup으로 뜨고, 부모창도 popup일 때)
		/// </summary>
		/// <param name="message"></param>
		[NonAction]
		protected ContentResult AlertMessageAndTopClose(string message)
		{
			string tempStr = "<script type='text/javascript'>document.domain='gmarket.co.kr'; alert('" + message + "'); window.opener.top.close(); window.close();</script>";
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}

		/// <summary>
		/// Alert 메시지 후 top opener페이지 redirect, 현재창 Close(자식페이지가 popup으로 뜰 때)
		/// </summary>
		/// <param name="message">alert 메시지</param>
		/// <param name="href">redirect url</param>
		[NonAction]
		protected ContentResult AlertMessageAndTopOpenerLocationChange(string message, string href)
		{
			string tempStr = "<script type='text/javascript'>document.domain='gmarket.co.kr'; alert('" + message + "'); window.opener.top.document.location.href='" + href + "'; self.opener=self; window.close();</script>";
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}

		/// <summary>
		/// opener페이지 redirect, 현재창 Close(자식페이지가 popup으로 뜰 때)
		/// </summary>
		/// <param name="href">redirect url</param>
		[NonAction]
		protected ContentResult OpenerLocationChange(string href)
		{
			string tempStr = "<script type='text/javascript'>window.opener.document.location.href='" + href + "'; self.opener=self; window.close();</script>";
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}

		/// <summary>
		/// top opener페이지 redirect, 현재창 Close(자식페이지가 popup으로 뜰 때)
		/// </summary>
		/// <param name="href">redirect url</param>
		[NonAction]
		protected ContentResult TopOpenerLocationChange(string href)
		{
			string tempStr = "<script type='text/javascript'>window.opener.top.document.location.href='" + href + "'; self.opener=self; window.close();</script>";
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}

		/// <summary>
		/// 자바스크립트 직접입력
		/// </summary>
		/// <param name="javascript"></param>
		[NonAction]
		protected ContentResult RegisterOnlyJavascriptWithoutResponse(string javascript)
		{
			string tempStr = String.Format("<script type='text/javascript'> {0} </script>", javascript);
			return Content(tempStr, "text/html", System.Text.Encoding.UTF8);
		}
		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMobile.Data.DisplayDB;
using GMobile.Service.Home;
using GMKT.GMobile.Web.Models;
using System.IO;
using System.Collections;

namespace GMKT.GMobile.Web.Controllers
{
	public class EtcController : GMobileControllerBase
	{
		[HttpPost]
		[ActionName("NoticeList")]
		public ActionResult NoticeList_Post(int pageNo = 1)
		{
			NoticeListModel model = new NoticeListModel
			{
				pageNo = pageNo,
				totalCount = 0,
				NoticeList = new List<NoticeT>()
			};

			return Json(new { hasRsMsg = "False", rsCd = "SUCCESS", rsMsg = "", list = RenderPartialViewToString("NoticeList_Post", model, this.ControllerContext) }, "application/json", JsonRequestBehavior.AllowGet);
		}

		public ActionResult NoticeList( string nKind = "ALL" )
		{
			ViewBag.HeaderTitle = "모바일 고객센터";
            ViewBag.Title = "공지사항";
			NoticeListModel model = new NoticeListModel
			{
				nKind = nKind
			};

			return View( model );
		}

		[HttpPost]
		public ActionResult NoticeListJson( string nKind, int pageNo = 1, int pageSize = 15 )
		{
			List<NoticeT> list = new MobileNoticeBiz().GetMobileNoticeList(nKind, pageNo, pageSize );

			Hashtable hash = new Hashtable();

			if ( list.Count > 0 )
			{
				hash.Add("TotalCount", list[0].TotalCount);
				hash.Add("PageCount", list[0].TotalPage);
			}
			else
				hash.Add("TotalCount", 0);

			hash.Add("PageSize", pageSize);
			hash.Add("Items", list);

			return Json( hash, JsonRequestBehavior.AllowGet );			
		}

		public ActionResult NoticeDetail(int seq, string nKind = "ALL" )
		{
			ViewBag.HeaderTitle = "모바일 고객센터";
            ViewBag.Title = "공지사항";
			NoticeT notice = new MobileNoticeBiz().GetMobileNoticeDetail(seq);

			if ( notice != null && notice.Content != null )
			{
				notice.Content = notice.Content.Replace("\r", "");
				notice.Content = notice.Content.Replace("\n", "<br/>");
			}
			else if ( notice == null && seq == 13 )
			{
				// iOS 6 버전 앱의 경우 안내페이지 링크.
				return Redirect("http://mobile.gmarket.co.kr/customercenter/FaqDetail?seq=11406&catecd=");
			}

			NoticeDetailModel model = new NoticeDetailModel
			{
				Notice = notice,
				queryKind = nKind
			};

			return View(model);
		}

		public string RenderPartialViewToString(string viewPath, object model, ControllerContext context)
		{
			var viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);
			var view = viewEngineResult.View;

			context.Controller.ViewData.Model = model;

			string result = String.Empty;
			using (var sw = new StringWriter())
			{
				var ctx = new ViewContext(context, view,
								context.Controller.ViewData,
								context.Controller.TempData,
								sw);
				view.Render(ctx, sw);
				result = sw.ToString();
			}

			return result;
		}

		public ActionResult ShopClosed()
		{
			return View();
		}
	}
}
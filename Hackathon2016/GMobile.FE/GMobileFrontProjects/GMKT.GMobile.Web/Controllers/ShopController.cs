using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using GMobile.Data.Voyager;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Util;
using GMKT.GMobile.Filter;
using GMKT.Web.Context;
using GMKT.GMobile.Constant;

namespace GMKT.GMobile.Web.Controllers
{
	public class ShopController : ShopBaseController
	{
		//
		// GET: /Shop/
		public ActionResult Index(string goodsCode)
		{
			return View();
		}

		//
		// GET: /Shop/Search
		public ActionResult Search(string keyword = "", string category="", int pageNo = 1, int pageSize = 20, int sort = 2)
		{
			return View();
		}

		//
		// GET: /Shop/List
		public ActionResult List(string category = "", string isshop = "false", string prevcate = "", string keyword = "", int pageNo = 1, int pageSize = 20, int sort = 2, int dpno = -1)
		{
			return View();
		}

		#region [ 공지사항 ]
		public ActionResult NoticeList(string alias, ShopNewsType? type, string keyword = "")
		{
			return View();
		}

		[HttpPost]
		public JsonResult GetMoreNoticeList(string alias, ShopNewsType? type, string keyword, int pageNo, int pageSize = 0)
		{
			return Json(new	{ result = false });
		}

		public ActionResult Notice(string alias, long no, ShopNewsType? type, string keyword = "")
		{
			return View();
		}
		#endregion

		public ActionResult HistoryAdd(string alias, string actionName, string message, string url)
		{
			return View(new HistoryAddM(){
				Alias = alias,
				ActionName = actionName,
				Message = message,
				Url = url, 
				ImageUrl = null,
			});
		}

		//
		// GET: /Shop/SearchTest
		public ActionResult SearchTest()
		{
			return View();
		}


		//
		// GET: /Shop/Test

		public ActionResult Test()
		{
			System.Collections.ArrayList aaa = CategoryUtil.GetXMLCategoryInfo("S", "300023597");

			return View(aaa);
		}

		[HttpPost]
		[GMobileHandleErrorAttribute]
		public JsonResult GetMyShoppingInfo(string alias)
		{
			return Json(new	{ success = false }, JsonRequestBehavior.DenyGet);
		}

		[HttpGet]
		[GMobileHandleErrorAttribute]
		public JsonResult AddFavoriteShop(string alias)
		{
			return Json(new { success = false, message = string.Empty }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[GMobileHandleErrorAttribute]
		public JsonResult DelFavoriteShop(string alias)
		{
			return Json(new { success = false, message = string.Empty }, JsonRequestBehavior.AllowGet);
		}
		
		[HttpGet]
		[GMobileHandleErrorAttribute]
		public JsonResult AddFavoriteItem(string itemNo)
		{
			return Json(new { result = 0, msg = string.Empty }, JsonRequestBehavior.AllowGet);
		}

		[ChildActionOnly]
		public PartialViewResult SideMenu()
		{
			return PartialView();
		}

		[HttpPost]
		public ActionResult SearchPost(string keyword = "", string category = "", int pageNo = 1, int pageSize = 20, int sort = 0)
		{
			return Json(new ShopSearchModel());
		}

		[HttpPost]
		public ActionResult ListPost(string category = "", string isshop = "false", string prevcate = "", string keyword = "", int pageNo = 1, int pageSize = 20, int sort = 0, string alias = "")
		{
			return Json(new ShopSearchModel());
		}

		[NonAction]
		private List<ShopItemModel> ConvertToShopItemModel(SearchItemT[] items, int pageNo)
		{
			return new List<ShopItemModel>();
		}
		
	}
}

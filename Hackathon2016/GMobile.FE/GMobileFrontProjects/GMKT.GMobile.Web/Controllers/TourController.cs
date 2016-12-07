using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using GMKT.GMobile.CommonData;

namespace GMKT.GMobile.Web.Controllers
{
    public class TourController : GMobileControllerBase
    {
        //
        // GET: /Tour/

        public ActionResult Index()
        {
			ViewBag.Title = "여행 - G마켓 모바일";
			SetHomeTabName("여행");

			PageAttr.IsMain = true;
			PageAttr.HeaderType = HeaderTypeEnum.Normal;

			TourMain model = new TourApiBiz_Cache().GetTourItem(0, 0, 1, 80, TourOrderEnum.RankPointDesc);
			if(model == null)
			{
				model = new TourApiBiz().GetTourItem(0, 0, 1, 80, TourOrderEnum.RankPointDesc);
			}
            return View(model);
        }
		
		[HttpPost]
		public JsonResult GetTourItem(long middleGroupNo = 0, long smallGroupNo = 0, int pageNo = 1, int pageSize = 80, TourOrderEnum order = TourOrderEnum.RankPointDesc)
		{
			TourMain result = new TourApiBiz_Cache().GetTourItem(middleGroupNo, smallGroupNo, pageNo, pageSize, order);
			if(result == null)
			{
				result = new TourApiBiz().GetTourItem(middleGroupNo, smallGroupNo, pageNo, pageSize, order);
			}
			return Json(result);
		}
    }
}

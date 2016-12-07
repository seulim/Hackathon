using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Biz;

namespace GMKT.GMobile.Web.Controllers
{
    public class BoardController : GMobileControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBy(int id)
        {
            return Json(new BoardApiBiz_Cache().GetFromCacheBy(id), JsonRequestBehavior.AllowGet);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hackathon.BizDac;
using Google.Apis.Vision.v1.Data;
using Google.Apis.Vision.v1;

namespace Hackathon2016.Controllers
{
	public class HomeController : Controller
	{
		[HttpPost]
		public ActionResult ImageDetect(string filepath)
		{
			string retText = "";
			filepath = filepath.Replace("\\", "/");

			TextDetectionBiz biz = new TextDetectionBiz();
			VisionService client = biz.CreateAuthorizedClient();
			IList<AnnotateImageResponse> response = biz.DetectText(client, filepath);

			if (response != null && response[0].TextAnnotations != null && response[0].TextAnnotations[0].Description != null)
				retText = response[0].TextAnnotations[0].Description;

			return Json(retText);
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
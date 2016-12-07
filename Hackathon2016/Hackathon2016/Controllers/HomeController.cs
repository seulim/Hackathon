using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hackathon.BizDac;
using Google.Apis.Vision.v1.Data;
using Google.Apis.Vision.v1;
using System.Drawing;

namespace Hackathon2016.Controllers
{
	public class HomeController : Controller
	{
		[HttpPost]
		public ActionResult ImageDetectML(string filepath)
		{
			string retText = "";
			filepath = filepath.Replace("\\", "/");
			Bitmap src = System.Drawing.Image.FromFile(filepath) as Bitmap;

			// 카드 이미지 리사이즈 
			Size resize = new Size(500, 300); 
			Bitmap resizeImage = new Bitmap(src, resize);
			resizeImage.Save("C:/git_repo/Hackathon2016/Hackathon2016/Hackathon2016/TestImage/result/return.jpg");
			
			
			int x, y, w, h;
			x = 46; // 왼쪽 위 모서리점 x축
			y = 165;// 왼쪽 위 모서리점 y축
			w = 22; //넓이
			h = 34; //높이

			// 카드번호 16자리 자르기		
			List<Bitmap> CardNumberList = new List<Bitmap>();
			for (int i = 0; i < 16; i++)
			{
				Rectangle cropRect = new Rectangle(x, y, w, h);
				Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

				using (Graphics g = Graphics.FromImage(target))
				{
					g.DrawImage(resizeImage, new Rectangle(0, 0, target.Width, target.Height),
									 cropRect,
									 GraphicsUnit.Pixel);
				}

				// 자른 이미지 리사이징
				Size resize2 = new Size(28, 36);
				Bitmap resizeImage2 = new Bitmap(target, resize2);
				resizeImage2.Save("C:/git_repo/Hackathon2016/Hackathon2016/Hackathon2016/TestImage/result/" + i + ".jpg");

				// 4자리씩 끊고 공백 추가
				if (i % 4 == 3)
					x = x + w + 21; 
				else
					x = x + w;

				CardNumberList.Add(resizeImage2);
			}

			//유효 년도/년월 자르기

			return Json(retText);
		}

		[HttpPost]
		public ActionResult ImageDetect(string filepath)
		{
			string retText = "";
			filepath = filepath.Replace("\\", "/");

			TextDetectionBiz biz = new TextDetectionBiz();
			VisionService client = biz.CreateAuthorizedClient();
			//IList<AnnotateImageResponse> response = biz.DetectTextByImgProcessing(client, filepath); //이미지프로세싱
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
			//ImageDetect("C:/git_repo/Hackathon2016/Hackathon2016/Hackathon2016/TestImage/test1.jpg");
			//ImageDetect("C:/git_repo/Hackathon2016/Hackathon2016/Hackathon2016/TestImage/test2.jpg");
			//ImageDetect("C:/git_repo/Hackathon2016/Hackathon2016/Hackathon2016/TestImage/test3.jpg");

			return View();
		}

		public ActionResult Contact()
		{

			return View();
		}
	}
}
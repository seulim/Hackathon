using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Biz.EventV2;
using GMKT.GMobile.Data.EventV2;
using GMKT.GMobile.Web.Models.EventV2;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Web.Controllers.EventV2
{
	public class GreenCarpetController : EventControllerBase
    {
        //
        // GET: /GreenCarpet/
		protected GreenCarpetModel result = new GreenCarpetModel();


        public ActionResult Index()
        {
			//SetHomeTabName("쿠폰");
			ViewBag.Title = "그린카펫 - G마켓 모바일";
            ViewBag.HeaderTitle = "출첵/쿠폰";
            PageAttr.HeaderType = CommonData.HeaderTypeEnum.Simple;

			GetGreenCarpetInfo();

            #region 페이스북 공유하기
            string faceBookImage = String.Format("{0}/640/main/pluszone_ico.png", Urls.MobileImageUrlV2);
            PageAttr.FbTitle = "G마켓 그린카펫";
            PageAttr.FbTagUrl = Urls.MobileWebUrl + "/GreenCarpet";
            PageAttr.FbTagImage = faceBookImage;
            PageAttr.FbTagDescription = "G마켓 그린카펫";
            #endregion

			return View("~/Views/EventV2/GreenCarpet/Index.cshtml", result);
        }

		protected void GetGreenCarpetInfo()
		{
			String TOP_BANNER_EVENT_MANAGE_TYPE = "25";
			String TOP_BANNER_EXPOSE_TARGET_TYPE = "06";

            result.NaviIcons = new EventCommonBiz().GetNavigationIcons();

			List<CommonBannerT> topBanner = new EventCommonBiz_Cache().GetCommonTopBanner(TOP_BANNER_EVENT_MANAGE_TYPE, TOP_BANNER_EXPOSE_TARGET_TYPE);

			result.TopBanner = new List<CommonBannerT>();
			if (topBanner != null && topBanner.Count > 0)
			{
				foreach (CommonBannerT info in topBanner)
				{
					result.TopBanner.Add(info);
				}
			}
			
			GreenCarpetT apiresult = new GreenCarpetBiz_Cache().GetGreenCarpetInfo();
			//GreenCarpetModel result = new GreenCarpetModel();
			if (apiresult != null)
			{
				result.TotalWinAmt = 0;
				if (apiresult.main.Count > 0)
				{
					result.MainPoster = apiresult.main;
				}
				//foreach (MainPosterT poster in result.MainPoster)
				//{
				//    poster.img_list = null;
				//}

				if (apiresult.notice_list.Count > 0)
				{
					result.Notice = apiresult.notice_list[0].Content.Split(new string[] { "<br>" }, System.StringSplitOptions.RemoveEmptyEntries);
				}
			}
		}
    }
}

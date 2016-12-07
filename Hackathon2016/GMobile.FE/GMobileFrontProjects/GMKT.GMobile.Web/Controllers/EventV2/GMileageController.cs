using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Biz.EventV2;
using GMKT.GMobile.Data.EventV2;
using GMKT.Component.Member;
using GMKT.GMobile.Util;
using GMKT.GMobile.Data.Member;
using GMKT.GMobile.Biz.Member;

namespace GMKT.GMobile.Web.Controllers
{
	public class GMileageController : EventControllerBase
    {
        //
        // GET: /GMileage/

        public ActionResult Index()
        {
			//SetHomeTabName("쿠폰");
			ViewBag.Title = "Smile Point - G마켓 모바일";
			
			DateTime today = DateTime.Now;

			GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.GMileageDataModel result = new GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.GMileageDataModel();

			if (PageAttr.IsLogin)
			{
				SmilePointBalanceT smilePointInfo = new SmileCashApiBiz().PostSmilePointBalance();
				if (smilePointInfo != null && smilePointInfo.ReturnCode == "0000")
				{
					result.Balance = smilePointInfo.Balance;
					result.ExpirableBalance = smilePointInfo.ExpirableBalance;
				}
			}

			String TOP_BANNER_EVENT_MANAGE_TYPE = "25";
			String TOP_BANNER_EXPOSE_TARGET_TYPE = "04";

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

			GMKT.GMobile.Data.EventV2.GMileageDataT apiresult = new GStampGMileageBiz_Cache().GetGMileageInfo();
			if (apiresult != null)
			{
				result.ExchangeCouponBanner = new List<GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.ExchangeCouponModel>();
				if (apiresult.ExchangeCouponBanner.Count > 0)
				{
					foreach (ExchangeCouponT info in apiresult.ExchangeCouponBanner)
					{
						GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.ExchangeCouponModel model = new GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.ExchangeCouponModel();
						DateTime disStrDt = Convert.ToDateTime(info.DisplayStartDate);
						DateTime disEdDt = Convert.ToDateTime(info.DisplayEndDate);
						if (disStrDt <= today && disEdDt > today && info.Type.ToLower() == "gmileage")
						{
							model.ImageMobile = info.ImageMobile;
							model.Alt = info.Alt;
							foreach (EIDInfoT eidInfo in info.EIDInfoList)
							{
								if (eidInfo.EtcSetupContent == "EXCHANGE_EID" && eidInfo.Eid != null && eidInfo.Eid !="")
								{
									model.EncryptEid = eidInfo.Eid.Split(',');
									model.AppMinusCnt =eidInfo.AppMinusCnt;

									result.ExchangeCouponBanner.Add(model);
								}
							}
						}
					}
				}
			}

            #region 페이스북 공유하기
            string faceBookImage = String.Format("{0}/640/main/pluszone_ico.png", Urls.MobileImageUrlV2);
			PageAttr.FbTitle = "G마켓 Smile Point";
            PageAttr.FbTagUrl = Urls.MobileWebUrl + "/GMileage";
            PageAttr.FbTagImage = faceBookImage;
			PageAttr.FbTagDescription = "G마켓 Smile Point";
            #endregion

			return View("~/Views/EventV2/GMileage/Index.cshtml",result);
        }
    }
}

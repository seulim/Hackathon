using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Web.Models.EventV2;
using GMKT.GMobile.Biz.EventV2;
using GMKT.GMobile.Data.EventV2;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Web.Controllers.EventV2
{
	public class GStampController : EventControllerBase
    {

		#region member variables
		//private GStampModel _gStampModel;
		private GStampGMileageModel.GStampDataModel _gStampModel;
		#endregion

		#region getter, setter
		/// <summary>
		/// 현재 보유하고 있는 G스탬프의 갯수입니다.
		/// </summary>
		protected int PossibleGStampCount
		{
			get
			{
				return _gStampModel.PossibleGStampCount;
			}
			set
			{
				_gStampModel.PossibleGStampCount = value;
			}
		}

		/// <summary>
		/// G스탬프로 응모할 수 있는 경품들의 List입니다.
		/// </summary>
		protected List<GStampFoodModel> GStampFoods
		{
			get
			{
				return _gStampModel.GStampFoods;
			}
			set
			{
				_gStampModel.GStampFoods = value;
			}
		}

		/// <summary>
		/// View에서 rendering할 탭의 번호입니다. 기본값은 0(스탬프 쿠폰교환)입니다.
		/// </summary>
		protected int CurrentTabNo
		{
			get
			{
				return _gStampModel.CurrentTabNo;
			}
			set
			{
				_gStampModel.CurrentTabNo = value;
			}
		}

		/// <summary>
		/// GStamp 사용제한 Blacklist message
		/// </summary>
		protected string MesssageGStampBlacklist
		{
			get
			{
				return _gStampModel.MesssageGStampBlacklist;
			}
			set
			{
				_gStampModel.MesssageGStampBlacklist = value;
			}
		}
		#endregion
		#region constructor
		/// <summary>
		/// 기본 Constructor입니다. 내부 변수를 초기화합니다.
		/// </summary>
		public GStampController()
		{
			_gStampModel = new GStampGMileageModel.GStampDataModel();
		}
		#endregion

		/// <summary>
		/// 로그인 중이라면 현재 보유하고 있는 G스탬프의 갯수를 받아옵니다. G스탬프로 교환할 수 있는 간식 목록을 불러와 Model에 담고 View를 반환합니다.
		/// </summary>
		/// <returns>GStamp의 View를 반환합니다.</returns>
		public ActionResult Index(int id = 0)
		{
			//SetHomeTabName("쿠폰");
			ViewBag.Title = "스탬프 쿠폰교환 / 간식교환 - G마켓 모바일";
            ViewBag.HeaderTitle = "출첵/쿠폰";
            PageAttr.HeaderType = CommonData.HeaderTypeEnum.Simple;

			CurrentTabNo = id;
			GetGStampDataModel();

			if (PageAttr.IsLogin)
			{
				GetStampIssueCount();
				GetBlacklistGstamp();
			}

            #region 페이스북 공유하기
            string faceBookImage = String.Format("{0}/640/main/pluszone_ico.png", Urls.MobileImageUrlV2);
            PageAttr.FbTitle = "G마켓 G스탬프";
            PageAttr.FbTagUrl = Urls.MobileWebUrl + "/GStamp";
            PageAttr.FbTagImage = faceBookImage;
            PageAttr.FbTagDescription = "G마켓 G스탬프";
            #endregion

			return View("~/Views/EventV2/GStamp/Index.cshtml", _gStampModel);
		}

		//[ChildActionOnly]
		public JsonResult getServerNowTime()
		{
			return Json(DateTime.Now.ToJavaScriptMilliseconds());
		}

		/// <summary>
		/// GStamp api  결과중에서 display날짜가 맞는 것만 추린다.
		/// </summary>
		/// <returns>GetGStampDataModel</returns>
		protected void GetGStampDataModel()
		{
			DateTime today = DateTime.Now;
			String TOP_BANNER_EVENT_MANAGE_TYPE = "25";
			String TOP_BANNER_EXPOSE_TARGET_TYPE = "02";

            _gStampModel.NaviIcons = new EventCommonBiz().GetNavigationIcons();

			List<CommonBannerT> topBanner = new EventCommonBiz_Cache().GetCommonTopBanner(TOP_BANNER_EVENT_MANAGE_TYPE, TOP_BANNER_EXPOSE_TARGET_TYPE);

			_gStampModel.TopBanner = new List<CommonBannerT>();
			if (topBanner != null && topBanner.Count > 0)
			{
				foreach (CommonBannerT info in topBanner)
				{
					_gStampModel.TopBanner.Add(info);
				}
			}

			GMKT.GMobile.Data.EventV2.GStampDataT apiresult = new GStampGMileageBiz_Cache().GetGStampInfo();
			if (apiresult != null)
			{
				// Today stamp deal banner list
				_gStampModel.TodayStampDealBanner = new List<GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.StampDealBannerModel>();
				if (apiresult.TodayStampDealBanner.Count > 0)
				{
					foreach (StampDealBannerT info in apiresult.TodayStampDealBanner)
					{
						GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.StampDealBannerModel result = new GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.StampDealBannerModel();
						DateTime disStrDt = Convert.ToDateTime(info.DisplayStartDate);
						DateTime disEdDt = Convert.ToDateTime(info.DisplayEndDate);
						if (disStrDt <= today && disEdDt > today)
						{
							result.GiveawayAmt = info.GiveawayAmt;
							result.AppMinusCnt = info.AppMinusCnt;						
							result.ImageMobile = info.ImageMobile;
							result.DisplayStartDate = info.DisplayStartDate;
							result.DisplayEndDate = info.DisplayEndDate;

							if (!String.IsNullOrEmpty(info.EntryEidScript))
							{
								result.EntryEid = new GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.StampEidModel();
								result.EntryEid.Eid = info.EntryEid;
								result.EntryEid.setEidScript(info.EntryEidScript);
								result.EntryEid.NeedsStampAmount = info.EntryAmt;
							}

							if (!String.IsNullOrEmpty(info.ExchangeEidScript))
							{
								result.ExchangeEid = new GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.StampEidModel();
								result.ExchangeEid.Eid = info.ExchangeEid;
								result.ExchangeEid.setEidScript(info.ExchangeEidScript);
								result.ExchangeEid.NeedsStampAmount = info.ExchangeAmt;
							}

							result.CompleteExchange = info.CompleteExchange;

							_gStampModel.TodayStampDealBanner.Add(result);
						}
					}
				}

				// Exchange coupon banner
				_gStampModel.ExchangeCouponBanner = new List<ExchangeCouponT>();
				if (apiresult.ExchangeCouponBanner.Count > 0)
				{
					foreach (ExchangeCouponT info in apiresult.ExchangeCouponBanner)
					{
						DateTime disStrDt = Convert.ToDateTime(info.DisplayStartDate);
						DateTime disEdDt = Convert.ToDateTime(info.DisplayEndDate);
						if (disStrDt <= today && disEdDt > today && info.Type.ToLower() == "gstamp")
						{
							_gStampModel.ExchangeCouponBanner.Add(info);
						}
					}
				}

				// Notice list
				if (apiresult.NoticeList.Count > 0)
				{
					foreach (NoticeT info in apiresult.NoticeList)
					{
						DateTime disStrDt = Convert.ToDateTime(info.DisplayStartDate);
						DateTime disEdDt = Convert.ToDateTime(info.DisplayEndDate);
						if (disStrDt <= today && disEdDt > today)
						{
							_gStampModel.Notice = info.MobileContent.Split(new string[] { "<br>" }, System.StringSplitOptions.RemoveEmptyEntries);
						}
					}
				}

				// Stamp exchange banner list
				if (apiresult.StampExchangeBanner.Count > 0)
				{
					_gStampModel.StampExchangeBanner = new List<GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.StampExchangeBannerModel>();

					foreach (StampExchangeBannerT banner in apiresult.StampExchangeBanner)
					{
						GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.StampExchangeBannerModel result = new GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.StampExchangeBannerModel();
						
						DateTime disStrDt = Convert.ToDateTime(banner.DisplayStartDate);
						DateTime disEdDt = Convert.ToDateTime(banner.DisplayEndDate);
						if (disStrDt <= today && disEdDt > today)
						{
							result.EventzoneDispSeq = banner.EventzoneDispSeq;
							result.Priority = banner.Priority;
							result.EventManageType = banner.EventManageType; 
							result.ExposeTargetType = banner.ExposeTargetType;
							result.GiveawayAmt = banner.GiveawayAmt;
							result.BannerNm = banner.BannerNm;
							result.Image = banner.Image;
							result.ImageMobile = banner.ImageMobile;

							if (!String.IsNullOrEmpty(banner.EntryEidScript))
							{
								result.EntryEid = new GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.StampEidModel();
								result.EntryEid.Eid = banner.EntryEid;
								result.EntryEid.setEidScript(banner.EntryEidScript);
								result.EntryEid.NeedsStampAmount = banner.EntryAmt;
							}

							if (!String.IsNullOrEmpty(banner.ExchangeEidScript))
							{
								result.ExchangeEid = new GMKT.GMobile.Web.Models.EventV2.GStampGMileageModel.StampEidModel();
								result.ExchangeEid.Eid = banner.ExchangeEid;
								result.ExchangeEid.setEidScript(banner.ExchangeEidScript);
								result.ExchangeEid.NeedsStampAmount = banner.ExchangeAmt;
							}

							result.CompleteExchange = banner.CompleteExchange;

							_gStampModel.StampExchangeBanner.Add(result);
						}
					}
				}
			}
		}

		/// <summary>
		/// 현재 로그인 한 사용자가 보유하고 있는 G스탬프의 갯수를 받아옵니다.
		/// </summary>
		protected void GetStampIssueCount()
		{
			GStampIssueT gStampIssue = new GStampBiz().GetStampIssueCount(gmktUserProfile.CustNo);

			PossibleGStampCount = gStampIssue.PossibleIssue;
		}
		
		/// <summary>
		/// G스탬프로 쿠폰을 교환합니다.
		/// </summary>
		/// <param name="id">쿠폰 번호(번호는 View를 참조)</param>
		/// <returns>응모 결과 View를 반환합니다.</returns>
		//public RedirectResult ExchangeCoupon(int id)
		//{
		//    if (PageAttr.IsLogin)
		//    {
		//        string[] encryptedString = EncryptForEventPlatform(EID[id]);

		//        return CommonApplyEventPlatformGmarket(encryptedString[0], encryptedString[1], "");
		//    }
		//    else // // 20140128 - blacklist 모바일 적용
		//    {
		//        string href = GMKT.GMobile.Util.Urls.LoginUrl +
		//            "?URL=" + "http://" + Request.Url.Host + Url.Action("Index");
		//        return Redirect(href);
		//    }
		//}

		/// <summary>
		/// 현재 로그인 한 사용자가 G스탬프 사용 제한 Blacklist여부 확인
		/// </summary>
		protected void GetBlacklistGstamp()
		{
			MesssageGStampBlacklist = string.Empty;

			// 20140128 - blacklist 모바일 적용
			if (gmktUserProfile.BlackList != null)
			{
				if (gmktUserProfile.BlackList.Contains(GMKT.Web.Membership.BlackListTypeEnum.GstampUse))
				{
					MesssageGStampBlacklist = GMKT.Web.Membership.RedirectionMessageResource.ResourceManager.GetString(GMKT.Web.Membership.BlackListAlertMessageEnum.BLACKLIST_GSTAMPUSE_MSG.ToString());
				}
			}
		}
	
    }
}

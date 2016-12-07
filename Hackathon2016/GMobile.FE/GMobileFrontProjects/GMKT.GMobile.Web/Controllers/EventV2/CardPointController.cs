using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GMKT.GMobile.Web.Models.EventV2;
using GMKT.GMobile.Biz.EventV2;
using GMKT.GMobile.Data.EventV2;
using GMKT.GMobile.Util;
using GMKT.Framework;

namespace GMKT.GMobile.Web.Controllers.EventV2
{
	public class CardPointController : EventControllerBase
	{
		private const string TOP_BANNER_EVENT_MANAGE_TYPE = "25";
		private const string TOP_BANNER_EXPOSE_TARGET_TYPE = "07";

		private const int THIS_MONTH_CARD_BENEFIT_COUNT_LIMIT = 15;

		public ActionResult Index()
		{
			//SetHomeTabName( "쿠폰" );
			ViewBag.Title = "Gmarket";
            ViewBag.HeaderTitle = "출첵/쿠폰";
            PageAttr.HeaderType = CommonData.HeaderTypeEnum.Simple;

			CardPointModel model = new CardPointModel();

			CardBenefitJsonEntityT cardBenefit = new CardPointBiz_Cache().GetCardBenefitEntity();
			if(cardBenefit != null )
			{
				if( cardBenefit.cardbenefit.mobile_card_halbu_list != null && cardBenefit.cardbenefit.mobile_card_halbu_list.Count > 0 )
				{
					model.ThisMonthHalbu = cardBenefit.cardbenefit.mobile_card_halbu_list[0].mobile_content;
				}


				if( cardBenefit.cardbenefit.card_event_list != null && cardBenefit.cardbenefit.card_event_list.Count > 0 )
				{
					List<BannerT> thisMonthCardBenefit = new List<BannerT>();
					var cardEventProcessed = cardBenefit.cardbenefit.card_event_list
							.SelectMany( T => T.banner_list) // 각각의 banner_list 를 묶어 평면화 시킨후에 하나의 IEnumerable 로 만들기
							.OrderBy( T=> T.priority) // priority 로 orderby
							.ThenBy( T => T.seq ) // seq 로 thenby
							.Take( THIS_MONTH_CARD_BENEFIT_COUNT_LIMIT )// 갯수 제한
							.ToList(); 

					foreach( CardBenefitBannerT i in cardEventProcessed )
					{
						thisMonthCardBenefit.Add( new BannerT( i.image_mobile, i.alt, i.url ) );
					}

					model.ThisMonthCardBenefit = thisMonthCardBenefit;
				}

				model.MobileGuidePopupImageUrl = Urls.MobileImageShortUrl + "/mobile.gmarket/images/640/main/layer/layer_pt_info.png";
				if (cardBenefit.cardbenefit != null && cardBenefit.cardbenefit.mobile_guide_popup != null)
				{
					if (cardBenefit.cardbenefit.mobile_guide_popup.Count > 0)
					{
						if (!String.IsNullOrEmpty(cardBenefit.cardbenefit.mobile_guide_popup[0].image_mobile))
						{
							model.MobileGuidePopupImageUrl = cardBenefit.cardbenefit.mobile_guide_popup[0].image_mobile;
						}
					}
				}
			}

            model.NaviIcons = new EventCommonBiz().GetNavigationIcons();
            List<CommonBannerT> topBanner = new EventCommonBiz_Cache().GetCommonTopBanner(TOP_BANNER_EVENT_MANAGE_TYPE, TOP_BANNER_EXPOSE_TARGET_TYPE);

            if (topBanner.Count > 0)
            {
                model.TopBanner = new BannerT(topBanner[0].ImageUrl, topBanner[0].Alt, topBanner[0].LinkUrl);
            }

			PointBenefitJsonEntityT pointBenefit = new CardPointBiz_Cache().GetPointBenefitEntity();
			List<Banner2T> temp = new List<Banner2T>();
			if( pointBenefit != null )
			{
				if( pointBenefit.pointAdd.jaehu_list != null && pointBenefit.pointAdd.jaehu_list.Count > 0 )
				{
					foreach( var i in pointBenefit.pointAdd.jaehu_list )
					{
						temp.Add( new Banner2T( i.image_mobile, i.alt, i.guide_image_mobile, i.guide_alt ) );
					}
				}
				model.PointAdd = temp;

				temp = new List<Banner2T>();
				if( pointBenefit.pointBenefit.jaehu_list != null && pointBenefit.pointBenefit.jaehu_list.Count > 0 )
				{
					foreach( var i in pointBenefit.pointBenefit.jaehu_list )
					{
						temp.Add( new Banner2T( i.image_mobile, i.alt, i.guide_image_mobile, i.guide_alt ) );
					}
				}
				model.PointBenefit = temp;
			}

            #region 페이스북 공유하기
            string faceBookImage = String.Format("{0}/640/main/pluszone_ico.png", Urls.MobileImageUrlV2);
            PageAttr.FbTitle = "G마켓 카드/포인트";
            PageAttr.FbTagUrl = Urls.MobileWebUrl + "/CardPoint";
            PageAttr.FbTagImage = faceBookImage;
            PageAttr.FbTagDescription = "G마켓 카드/포인트";
            #endregion

			#region 아시아나 오케이 캐시백 재등록

			bool tmpReregOcb = new CardPointApiBiz().IsReregOCB(gmktUserProfile.CustNo);
			if (tmpReregOcb == true)
			{
				if (Request.Cookies["ocbDisp"] != null)
				{
					if (Request.Cookies["ocbDisp"].Value == "N")
					{
						tmpReregOcb = false;
					}
				}
			}
			model.IsReregOCB = tmpReregOcb;

			bool tmpReregAsiana = new CardPointApiBiz().IsReregAsiana(gmktUserProfile.CustNo);
			if (tmpReregAsiana == true)
			{
				if (Request.Cookies["asianaDisp"] != null)
				{
					if (Request.Cookies["asianaDisp"].Value == "N")
					{
						tmpReregAsiana = false;
					}
				}
			}
			model.IsReregAsiana = tmpReregAsiana; 
			#endregion


			return View( "~/Views/EventV2/CardPoint/Index.cshtml", model );
		}

		#region 아시아나카드등록 (BC_9070)
		public string SaveAsianaCard(FormCollection form)
		{
			string CustNo = gmktUserProfile.CustNo;
			string LoignId = gmktUserProfile.LoginID;
			string CardNo = form["asiana_card_no"];
			string sHtml = "";

			if (!string.IsNullOrEmpty(CardNo) && CardNo.Length == 9)
			{
				if (IsJaehuCard("A"))
				{
					sHtml = "이미 등록된 회원 번호가 있습니다.\n\n수정을 원하시면 \"적립 내역 보기\"에서\n\n회원번호 삭제 후 이용해 주세요.";
				}
				else
				{
					int result = new GMKT.Component.Member.MyInfoBiz().SetJaehuCardInfo(CustNo, CardNo, LoignId, "A");
					if (result == 0)
					{
						sHtml = "정상적으로 저장 되었습니다.\n\n\"적립 내역보기\"을 확인해 주세요.";
					}
					else if (result == -3)
					{
						sHtml = "동일한 아시아나클럽 회원번호에 등록할 수 있는 ID 개수 제한됩니다. ID 10개를 초과하였습니다. 다른 회원번호로 등록 가능합니다.";
					}
					else
					{
						sHtml = "회원번호 등록에 실패했습니다.";
					}
				}
			}
			else
			{
				sHtml = "카드번호를 정확히 입력해주세요.";
			}

			return sHtml;
		}
		#endregion

		#region 아시아나카드 마일리지 이력 (BC_9070)
		public JsonResult JsonAsianaMileageHisotry(int PageNo = 1)
		{
			string CustNo = gmktUserProfile.CustNo;
			int PageSize = 5;

			List<GMKT.Component.Member.AsianaMileageHistoryT> AsianaMileageListT = new GMKT.Component.Member.MyInfoBiz().GetAsianaMileageList(CustNo, PageNo, PageSize);

			for (int i = 0; i < AsianaMileageListT.Count; i++)
			{
				if (AsianaMileageListT[i].TransDt == "1900-01-01")
					AsianaMileageListT[i].TransDt = Convert.ToDateTime(AsianaMileageListT[i].RegDt).AddMonths(1).ToString("yyyy-MM-") + "17";

			}
			return Json(AsianaMileageListT, JsonRequestBehavior.AllowGet);
		}
		#endregion

		#region 제휴카드 가져오기 (BC_9070)
		public string JaehuCardNo(string type)
		{
			string CustNo = gmktUserProfile.CustNo;
			GMKT.Component.Member.JaehuCardInfoT JaehuCardInfo = new GMKT.Component.Member.MyInfoBiz().GetJaehuCardInfo(CustNo, type);
			if (JaehuCardInfo != null)
				return JaehuCardInfo.CardNo;
			else
				return "";
		}
		#endregion

		#region 제휴카드 등록 유무 (BC_9070)
		public bool IsJaehuCard(string type)
		{
			string CustNo = gmktUserProfile.CustNo;
			GMKT.Component.Member.JaehuCardInfoT JaehuCardInfo = new GMKT.Component.Member.MyInfoBiz().GetJaehuCardInfo(CustNo, type);
			if (JaehuCardInfo != null && !string.IsNullOrEmpty(JaehuCardInfo.CardNo))
				return true;
			else
				return false;
		}
		#endregion

		#region OK캐시백 포인트 이력 (BC_9070)
		public JsonResult JsonOKCashbagHisotry(int PageNo = 1)
		{
			string CustNo = gmktUserProfile.CustNo;
			int PageSize = 5;

			List<GMKT.Component.Member.OKCashbagListT> okcashbagList = new GMKT.Component.Member.OKCashbagBiz().GetBookCashList(CustNo, PageNo, PageSize);

			return Json(okcashbagList, JsonRequestBehavior.AllowGet);

		}
		#endregion

		#region 제휴 카드 삭제 (BC_9070)
		public ActionResult DeleteJaehuCardInfo(string JaeHuID)
		{
			string custNo = gmktUserProfile.CustNo;
			string loginId = gmktUserProfile.LoginID;
			string msg = "";
			string CardType = "";
			int result = -100;
			try
			{
				if (JaeHuID == "OKCASHBAG")
				{
					CardType = "O";
				}
				else if (JaeHuID == "ASIANA")
				{
					CardType = "A";
				}

				if (!string.IsNullOrEmpty(CardType))
				{
					result = new GMKT.Component.Member.MyInfoBiz().RemoveJaehuCardInfo(custNo, loginId, CardType);
					if (result == 0)
					{
						msg = "카드 정보가 삭제되었습니다.";
					}
					else if (result == -1)
					{
						msg = "카드번호 삭제 실패";
					}
					else if (result == -2)
					{
						msg = "등록된 카드번호 없음";
					}
				}
				else
				{
					msg = "선택된 제휴사가 없음";
				}
			}
			catch (Exception e)
			{
				msg = "삭제 중 오류가 발생됨";
			}
			return Content("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}", "application/json");
		}
		#endregion
	}
}

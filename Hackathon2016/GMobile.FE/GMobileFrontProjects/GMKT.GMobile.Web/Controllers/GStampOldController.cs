using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using GMKT.GMobile.Web.Models;
using GMKT.Framework.Security;
using GMKT.Web.Filter;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace GMKT.GMobile.Web.Controllers
{
	public class GStampOldController : EventControllerBase
	{
		#region constants
		protected const int GEID = 103518;
		protected readonly int[] EID = 
		{
			114066, //1000원 응모
			114067,	//2000원 응모
			114068,	//3000원 응모
			114069,	//4000원 응모
			114070,	//5000원 응모
			114071,	//10000원 응모
			114072,	//1000원 교환
			114073,	//2000원 교환
			114074,	//3000원 교환
			114075,	//5000원 교환
		};
		#endregion

		#region member variables
		private GStampModel _gStampModel;
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
		public GStampOldController()
		{
			_gStampModel = new GStampModel();
		}
		#endregion

		/// <summary>
		/// 로그인 중이라면 현재 보유하고 있는 G스탬프의 갯수를 받아옵니다. G스탬프로 교환할 수 있는 간식 목록을 불러와 Model에 담고 View를 반환합니다.
		/// </summary>
		/// <returns>GStamp의 View를 반환합니다.</returns>
		public ActionResult Index(int id = 0)
		{
            ViewBag.Title = "스탬프 쿠폰교환 / 간식교환 - G마켓 모바일";
			PageAttr.MainCss = "marketing_v2.css";

			CurrentTabNo = id;

			if (PageAttr.IsLogin)
			{
				GetStampIssueCount();
				GetBlacklistGstamp();
			}

			GetEventFoodList();

			return View(_gStampModel);
		}

		/// <summary>
		/// G스탬프로 쿠폰을 교환합니다.
		/// </summary>
		/// <param name="id">쿠폰 번호(번호는 View를 참조)</param>
		/// <returns>응모 결과 View를 반환합니다.</returns>
		public RedirectResult ExchangeCoupon(int id)
		{
			if (PageAttr.IsLogin)
			{
				string[] encryptedString = EncryptForEventPlatform(EID[id]);

				return CommonApplyEventPlatformGmarket(encryptedString[0], encryptedString[1], "");
			}
			else // // 20140128 - blacklist 모바일 적용
			{
				string href = GMKT.GMobile.Util.Urls.LoginUrl +
					"?URL=" + "http://" + Request.Url.Host + Url.Action("Index");
				return Redirect(href);
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
		/// G스탬프로 교환할 수 있는 간식 목록을 불러옵니다.
		/// </summary>
		/// <returns>간식 목록을 성공적으로 불러왔는지를 반환합니다.</returns>
		protected bool GetEventFoodList()
		{
			
			List<EventzoneGiftT> eventzoneGiftList = new GStampBiz().GetEventzoneGiftList();
			
			/*
			List<EventzoneGiftT> eventzoneGiftList = new EventzoneBiz().GetEventzoneGiftList
			(
				"GB",
				new DateTime(2012,11,1),
				AMOUNT_OF_GET_EVENTZONE_GIFT_LIST
			);
			*/
			if (eventzoneGiftList.Count == 0)
			{
				return false;
			}
			else
			{
				// 2013-03-29 이윤호 추가
				// 간식 목록 갯수가 홀수인 경우 마지막에 첫 번째 상품을 한 번 더 노출하는 방식으로 
				// 총 개수가 짝수로 맞춤
				// ex) 5개인 경우
				//		1-2, 3-4, 5-1
				if (eventzoneGiftList.Count % 2 == 1)
				{
					eventzoneGiftList.Add(eventzoneGiftList.First());
				}

				foreach (var eachEventzoneGift in eventzoneGiftList)
				{
					try
					{
						EventWinnerT eventWinner = new GStampBiz().GetEventRemainCount((int)eachEventzoneGift.ChangeEid, DateTime.Now);

						GStampFoodModel eachGStampFood = new GStampFoodModel()
						{
							ApplyEidEncryptedString = EncryptForEventPlatform(eachEventzoneGift.ApplyEid),
							ChangeEidEncryptedString = EncryptForEventPlatform((int)eachEventzoneGift.ChangeEid),
							ApplyCount = new GStampBiz().GetEventStickerMinusCount(eachEventzoneGift.ApplyEid).Count,
							ChangeCount = new GStampBiz().GetEventStickerMinusCount((int)eachEventzoneGift.ChangeEid).Count,
							RemainCount = eventWinner.WinCount - eventWinner.WinnerCount,
							Title = eachEventzoneGift.Title,
							ImageUrl = eachEventzoneGift.MobileImageUrl
						};

						GStampFoods.Add(eachGStampFood);
					}
					catch (NullReferenceException)
					{
						continue;
					}
				}
				
				return true;
			}
		}
		
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

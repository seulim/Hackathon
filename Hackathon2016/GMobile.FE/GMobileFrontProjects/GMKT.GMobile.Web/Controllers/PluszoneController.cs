using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Filter;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Web.Controllers
{
	public class PluszoneController : EventControllerBase
	{
		#region member variables
		/// <summary>
		/// View로 넘겨줄 PluszoneModel입니다. 직접 접근보다는 getter, setter를 통한 접근을 권장합니다.
		/// </summary>
		private PluszoneModel _pluszoneModel;
		#endregion

		#region getter, setter
		/// <summary>
		/// 이번 달 1일의 요일을 계산하기 위한 첫 주에 비어있는 칸입니다. (0: 일요일, 4: 목요일)
		/// </summary>
		protected int StartDayOfThisMonth
		{
			get	
			{	
				return _pluszoneModel.StartDayOfThisMonth;	
			}
			set	
			{	
				_pluszoneModel.StartDayOfThisMonth = value;	
			}
		}

		/// <summary>
		/// 이번 달의 마지막 날입니다.
		/// </summary>
		protected int EndDayOfThisMonth
		{
			get 
			{ 
				return _pluszoneModel.EndDayOfThisMonth;
			}
			set
			{
				_pluszoneModel.EndDayOfThisMonth = value;
			}
		}

		/// <summary>
		/// 오늘의 날짜입니다.
		/// </summary>
		protected DateTime Today
		{
			get
			{
				return _pluszoneModel.Today;
			}
			set
			{
				_pluszoneModel.Today = value;
			}
		}

		/// <summary>
		/// 이번 달 출석체크한 횟수입니다.
		/// </summary>
		protected int AttendanceCount 
		{
			get
			{
				return _pluszoneModel.AttendanceCount;
			}
			set
			{
				_pluszoneModel.AttendanceCount = value;
			}
		}

		/// <summary>
		/// 이번 달 출석체크한 날짜들의 List입니다.
		/// </summary>
		protected List<int> AttendanceDate 
		{
			get
			{
				return _pluszoneModel.AttendanceDate;
			}
			set
			{
				_pluszoneModel.AttendanceDate = value;
			}
		}

		/// <summary>
		/// 오늘 이미 출석체크를 했는지 여부입니다.
		/// </summary>
		protected bool IsApply 
		{
			get
			{
				return _pluszoneModel.IsApply;
			}
			set
			{
				_pluszoneModel.IsApply = value;
			}
		}

        /// <summary>
        /// 출석도장으로 응모할 수 있는 경품들의 List입니다.
        /// </summary>
        protected List<EventzoneBannerModel> PluszoneCouponList
        {
            get
            {
                return _pluszoneModel.PluszoneCouponList;
            }
            set
            {
                _pluszoneModel.PluszoneCouponList = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<MobileShopPlan> MobileShopPlanList
        {
            get
            {
                return _pluszoneModel.MobileShopPlanList;
            }
            set
            {
                _pluszoneModel.MobileShopPlanList = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<CouponBenefitItem> CouponBenefitItem
        {
            get
            {
                return _pluszoneModel.CouponBenefitItem;
            }
            set
            {
                _pluszoneModel.CouponBenefitItem = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<PopularPlanQuickLink> PopularPlanQuickLink
        {
            get
            {
                return _pluszoneModel.PopularPlanQuickLink;
            }
            set
            {
                _pluszoneModel.PopularPlanQuickLink = value;
            }
        }


        #endregion

		#region constructor
		/// <summary>
		/// 기본 Constructor입니다. 내부 변수를 초기화합니다.
		/// </summary>
		public PluszoneController()
		{
			_pluszoneModel = new PluszoneModel();
		}
		#endregion

        /// <summary>
        /// 이번 달 달력을 그리고 로그인 상태라면 출석체크한 날을 가져옵니다. 마지막으로 응모할 수 있는 행운경품들을 불러오고 이 내용들을 Model에 담아 View로 전달합니다.
        /// </summary>
        /// <returns>Pluszone의 VIew를 반환합니다.</returns>
        public ActionResult Index()
        {
            SetHomeTabName("쿠폰");
            PageAttr.MainCss = "marketing_v2.css";
			ViewBag.Title = "쿠폰 - G마켓 모바일";

            MakeCalendar();

            if (PageAttr.IsLogin)
                MarkAttendances();

            GetCoupons();
            GetAttendanceCheckTotalList();

			#region 페이스북 공유하기
			string faceBookImage = String.Format("{0}/640/main/pluszone_ico.png", Urls.MobileImageUrlV2);
			PageAttr.FbTitle = "G마켓 쿠폰";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Pluszone";
			PageAttr.FbTagImage = faceBookImage;
			PageAttr.FbTagDescription = "G마켓 쿠폰";
			#endregion

            /* Landing Banner */
            new LandingBannerSetter(Request).Set(_pluszoneModel, PageAttr.IsApp);

            return View(_pluszoneModel);
        }

        /// <summary>
        /// 이번 달 달력을 그리고 로그인 상태라면 출석체크한 날을 가져옵니다. 마지막으로 응모할 수 있는 행운경품들을 불러오고 이 내용들을 Model에 담아 View로 전달합니다.
        /// </summary>
        /// <returns>Pluszone의 VIew를 반환합니다.</returns>
        public ActionResult IndexOld()
        {
            PageAttr.MainCss = "marketing_v2.css";

            MakeCalendar();

            if (PageAttr.IsLogin)
                MarkAttendances();

            GetCoupons();

            return View(_pluszoneModel);
        }


		/// <summary>
		/// 달력을 그리기 위해 오늘 날짜와 이번 달이 무슨 요일에 시작하는지(첫 주 앞 부분을 몇 칸 비우고 달력을 그려야 하는지), 이번 달 마지막 날은 며칠인지 기록합니다.
		/// </summary>
		protected void MakeCalendar()
		{
			DateTime today = DateTime.Now;

			DayOfWeek startDayOfThisMonth = new DateTime(today.Year, today.Month, 1).DayOfWeek;
			int endDayOfThisMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddDays(-1).Day;

			StartDayOfThisMonth = (int)startDayOfThisMonth;
			EndDayOfThisMonth = endDayOfThisMonth;
			Today = today;
		}

		/// <summary>
		/// 이번 달에 이미 룰렛을 돌려 출석체크를 한 날을 가져옵니다. 오늘 룰렛을 이미 돌렸는지도 검사합니다.
		/// </summary>
		protected void MarkAttendances()
		{
			// 출석정보 가져옴
			List<PluszoneAttendanceT> pluszoneAttendances = new PluszoneBiz().GetPluszoneAttendanceThisMonth(gmktUserProfile.CustNo);

			// 총 출석일 기록
			AttendanceCount = pluszoneAttendances.Count;

			// 개별 출석일 기록
			foreach (PluszoneAttendanceT eachPluszoneAttendance in pluszoneAttendances)
			{
				AttendanceDate.Add(eachPluszoneAttendance.AttendanceDate.Day);
			}

			// 오늘 룰렛을 이미 돌렸는지 검사
			if (AttendanceDate.Contains(Today.Day))
				IsApply = true;
			else
				IsApply = false;
		}

		/// <summary>
		/// 출석도장으로 응모할 수 있는 행운경품들을 불러옵니다.
		/// </summary>
		/// <returns>행운경품을 성공적으로 받아왔는지를 반환합니다.</returns>
		protected bool GetCoupons()
		{
			List<EventzoneBannerT> pluszoneCouponList = new PluszoneBiz().GetCouponList();

			if (pluszoneCouponList.Count == 0)
			{
				return false;
			}
			else
			{
				foreach (var eachPluszoneCoupon in pluszoneCouponList)
				{
					EventzoneBannerModel eventzoneBanner = new EventzoneBannerModel()
					{
						BannerUrl = eachPluszoneCoupon.MobileBannerUrl,
						Title = eachPluszoneCoupon.BannerTitle,
						EidEncryptedString = EncryptForEventPlatform(eachPluszoneCoupon.Eid)
					};

					PluszoneCouponList.Add(eventzoneBanner);
				}

				return true;
			}
		}

        protected void GetAttendanceCheckTotalList()
        {
            ApiResponse<AttendanceCheckTotal> attendanceCheckTotal = new PluszoneNewBiz_Cache().GetAttendanceCheckTotalList();
            if (attendanceCheckTotal == null || attendanceCheckTotal.Data == null)
            {
				attendanceCheckTotal = new PluszoneNewBiz().GetAttendanceCheckTotalList();
				if (attendanceCheckTotal == null || attendanceCheckTotal.Data == null)
				{
					MobileShopPlanList = new List<MobileShopPlan>();
					CouponBenefitItem = new List<CouponBenefitItem>();
					PopularPlanQuickLink = new List<PopularPlanQuickLink>();
				}
            }

			MobileShopPlanList = attendanceCheckTotal.Data.MobileShopPlan;
			CouponBenefitItem = attendanceCheckTotal.Data.CouponBenefitItem;
			PopularPlanQuickLink = attendanceCheckTotal.Data.PopularPlanQuickLink;
        }

		/// <summary>
		/// 출석체크를 하고 경품을 지급받습니다.
		/// </summary>
		/// <returns>Json object(Result[, success, getEidRandom, str, encStr, StatusCode, ErrorType, ErrorMessage])</returns>
		[HttpPost]
		[GMobileHandleErrorAttribute]
		[Obsolete("Promotion팀의 이벤트 플랫폼을 사용하기로 변경되어 더 이상 사용하지 않는 Action입니다.", true)]
		public JsonResult SendAttendance()
		{
			PluszoneBiz pluszoneBiz = new PluszoneBiz();

			int[] resultSendAttendance = pluszoneBiz.SendAttendance(gmktUserProfile.CustNo, gmktUserProfile.LoginID);

			int optionNo = resultSendAttendance[0];
			int eid = resultSendAttendance[1];

			string[] encryptedString = EncryptForEventPlatform(eid);

			return Json
			(
				new
				{
					Result = "Success",
					success = true,
					angleForRotate = pluszoneBiz.ConvertISeqNumToAngle(optionNo),
					str = encryptedString[0],
					encStr = encryptedString[1]
				},
				JsonRequestBehavior.DenyGet
			);
		}
	}
}

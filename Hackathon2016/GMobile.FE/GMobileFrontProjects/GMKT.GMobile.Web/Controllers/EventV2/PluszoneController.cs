using System;
using System.Collections.Generic;
using System.Web.Mvc;
//using GMKT.GMobile.Biz;
using GMKT.GMobile.Biz.EventV2;
using GMKT.GMobile.Data;
//using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Filter;
using GMKT.GMobile.Util;
using GMKT.GMobile.Data.EventV2;
using GMKT.GMobile.Web.Models.EventV2;
using GMobile.Service.Display;
            
using GMobile.Data.EventZone;
using GMobile.Service.EventZone;


namespace GMKT.GMobile.Web.Controllers
{
	public class PluszoneController : EventControllerBase
	{
        private const string TOP_BANNER_EVENT_MANAGE_TYPE = "25";
        private const string TOP_BANNER_EXPOSE_TARGET_TYPE = "01";

		#region member variables
		/// <summary>
		/// View로 넘겨줄 PluszoneModel입니다. 직접 접근보다는 getter, setter를 통한 접근을 권장합니다.
		/// </summary>
		private PluszoneModel _pluszoneModel;
		#endregion

		#region getter, setter
        protected List<NavigationIconT> NaviIcons
        {
            get
            {
                return _pluszoneModel.NaviIcons;
            }
            set
            {
                _pluszoneModel.NaviIcons = value;
            }
        }


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
        protected List<PluszoneBannerModel> PluszoneCouponList
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

        protected PluszoneBannerT TopBanner
        {
            get
            {
                return _pluszoneModel.TopBanner;
            }
            set
            {
                _pluszoneModel.TopBanner = value;
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

        /// <summary>
        /// 
        /// </summary>
        protected BannerInfoT Roulette
        {
            get
            {
                return _pluszoneModel.Roulette;
            }
            set
            {
                _pluszoneModel.Roulette = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<BannerInfoT> AttendanceBanner
        {
            get
            {
                return _pluszoneModel.AttendanceBanner;
            }
            set
            {
                _pluszoneModel.AttendanceBanner = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<BannerInfoT> AttendanceBannerGlobal
        {
            get
            {
                return _pluszoneModel.AttendanceBannerGlobal;
            }
            set
            {
                _pluszoneModel.AttendanceBannerGlobal = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<BannerInfoT> MainBanner
        {
            get
            {
                return _pluszoneModel.MainBanner;
            }
            set
            {
                _pluszoneModel.MainBanner = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<BannerInfoT> PromotionBanner
        {
            get
            {
                return _pluszoneModel.PromotionBanner;
            }
            set
            {
                _pluszoneModel.PromotionBanner = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<BannerInfoT> CategoryBanner
        {
            get
            {
                return _pluszoneModel.CategoryBanner;
            }
            set
            {
                _pluszoneModel.CategoryBanner = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<BannerInfoT> MobileBanner
        {
            get
            {
                return _pluszoneModel.MobileBanner;
            }
            set
            {
                _pluszoneModel.MobileBanner = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<BannerInfoT> CardBanner
        {
            get
            {
                return _pluszoneModel.CardBanner;
            }
            set
            {
                _pluszoneModel.CardBanner = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<BannerInfoT> BandBanner
        {
            get
            {
                return _pluszoneModel.BandBanner;
            }
            set
            {
                _pluszoneModel.BandBanner = value;
            }
        }

		/// <summary>
		/// 
		/// </summary>
		protected string MobileGuidePopupImageUrl
		{
			get
			{
				return _pluszoneModel.MobileGuidePopupImageUrl;
			}
			set
			{
				_pluszoneModel.MobileGuidePopupImageUrl = value;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        protected List<WinnerT> WinnerList
        {
            get
            {
                return _pluszoneModel.WinnerList;
            }
            set
            {
                _pluszoneModel.WinnerList = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected List<NoticeT> NoticeList
        {
            get
            {
                return _pluszoneModel.NoticeList;
            }
            set
            {
                _pluszoneModel.NoticeList = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected PlanListT ShoppingPlanList
        {
            get
            {
                return _pluszoneModel.ShoppingPlanList;
            }
            set
            {
                _pluszoneModel.ShoppingPlanList = value;
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
            //SetHomeTabName("쿠폰");
            ViewBag.Title = "플러스존 - G마켓 모바일";
            ViewBag.HeaderTitle = "쿠폰/출첵";
            PageAttr.HeaderType = CommonData.HeaderTypeEnum.Simple;
            MakeCalendar();

            NaviIcons = new EventCommonBiz().GetNavigationIcons();

            List<CommonBannerT> topBanner = new EventCommonBiz_Cache().GetCommonTopBanner(TOP_BANNER_EVENT_MANAGE_TYPE, TOP_BANNER_EXPOSE_TARGET_TYPE);
            if (topBanner.Count > 0)
            {
                TopBanner = new PluszoneBannerT(topBanner[0].ImageUrl, topBanner[0].Alt, topBanner[0].LinkUrl);
            }

            if (PageAttr.IsLogin)
                MarkAttendances();

            GetPluszoneInfo();
            GetCoupons();            

            #region 페이스북 공유하기
            string faceBookImage = String.Format("{0}/640/main/pluszone_ico.png", Urls.MobileImageUrlV2);
            PageAttr.FbTitle = "G마켓 플러스존";
            PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Pluszone";
            PageAttr.FbTagImage = faceBookImage;
            PageAttr.FbTagDescription = "G마켓 플러스존";
            #endregion

            /* Landing Banner */
            new LandingBannerSetter(Request).Set(_pluszoneModel, PageAttr.IsApp);

			ShoppingPlanList.PlanList = new MobilePlanBiz().GetMobilePlanList("", 1, 10);
            ShoppingPlanList.pageNo = 1;

            //6월 1일 적용 플러스존 노출 여부 결정(날짜 or Test아이디)
            bool IsTestIdCheck = gmktUserProfile.LoginID == "lyj0707" || gmktUserProfile.LoginID == "test4plan" || gmktUserProfile.LoginID == "lyk028" || gmktUserProfile.LoginID == "momoharry";
            if (DateTime.Now >= new DateTime(2016, 6, 1, 0, 0, 0) || IsTestIdCheck)
            {
                return View("~/Views/EventV2/pluszone/Index.cshtml", _pluszoneModel);
            }
            
            return View("~/Views/EventV2/pluszone/IndexV1.cshtml", _pluszoneModel);
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
            List<PluszoneNewAttendanceT> pluszoneAttendances = new GMKT.GMobile.Biz.PluszoneBiz().GetPluszoneAttendanceThisMonth(gmktUserProfile.CustNo);

			// 총 출석일 기록
			AttendanceCount = pluszoneAttendances.Count;

			// 개별 출석일 기록
            foreach (PluszoneNewAttendanceT eachPluszoneAttendance in pluszoneAttendances)
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
            if (AttendanceBanner.Count == 0)
			{
				return false;
			}
			else
			{
                foreach (var eachPluszoneCoupon in AttendanceBanner)
				{
                    if (string.IsNullOrEmpty(eachPluszoneCoupon.Eid))
                    {
                        eachPluszoneCoupon.Eid = "0";
                    }
                    PluszoneBannerModel pluszoneBanner = new PluszoneBannerModel()
					{
						BannerUrl = eachPluszoneCoupon.ImageMobile,
						Title = eachPluszoneCoupon.Title,
                        EidEncryptedString = EncryptForEventPlatform(Convert.ToInt32(eachPluszoneCoupon.Eid))
					};

                    PluszoneCouponList.Add(pluszoneBanner);
				}

				return true;
			}
		}

        protected void GetPluszoneInfo()
        {
            ApiResponse<PluszoneDataT> pluszoneInfo = new PluszoneBizV2_Cache().GetPluszoneInfo();
            if (pluszoneInfo == null || pluszoneInfo.Data == null)
            {
                pluszoneInfo = new PluszoneBizV2().GetPluszoneInfo();
                if (pluszoneInfo == null || pluszoneInfo.Data == null)
                {
                    Roulette = new BannerInfoT();
                    AttendanceBanner = new List<BannerInfoT>();
                    AttendanceBannerGlobal = new List<BannerInfoT>();
                    MainBanner = new List<BannerInfoT>();
                    PromotionBanner = new List<BannerInfoT>();
                    CategoryBanner = new List<BannerInfoT>();
                    MobileBanner = new List<BannerInfoT>();
                    CardBanner = new List<BannerInfoT>();
                    BandBanner = new List<BannerInfoT>();
                    WinnerList = new List<WinnerT>();
                    NoticeList = new List<NoticeT>();
                }                
            }
            
            Roulette = pluszoneInfo.Data.Roulette;
            AttendanceBanner = pluszoneInfo.Data.AttendanceBanner;
            AttendanceBannerGlobal = pluszoneInfo.Data.AttendanceBannerGlobal;
            MainBanner = pluszoneInfo.Data.MainBanner;
            PromotionBanner = pluszoneInfo.Data.PromotionBanner;
            CategoryBanner = pluszoneInfo.Data.CategoryBanner;
            MobileBanner = pluszoneInfo.Data.MobileBanner;
            CardBanner = pluszoneInfo.Data.CardBanner;
            BandBanner = pluszoneInfo.Data.BandBanner;

			MobileGuidePopupImageUrl = Urls.MobileImageShortUrl + "/mobile.gmarket/images/640/main/layer/layer_pz.png";
			if (pluszoneInfo.Data.MobileGuidePopup != null && pluszoneInfo.Data.MobileGuidePopup.Count > 0)
			{
				MobileGuidePopupImageUrl = pluszoneInfo.Data.MobileGuidePopup[0].ImageMobile;
			}
			

            WinnerList = pluszoneInfo.Data.WinnerList;
            NoticeList = pluszoneInfo.Data.NoticeList;
        }


        #region 출석체크 마일리지 응모



        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encStr"></param>
        /// <param name="eidCnt"></param>
        public string RouletteMileageDrawEveryDay(string str, string encStr, int eidCnt)
        {
            int lastDate = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month);

            if (!PageAttr.IsLogin)
            {
                return Urls.LoginUrl;
            }

            MarkAttendances();
            GetPluszoneInfo();
            DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
            DateTime endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);

            MyBenefitVipT t = new MyBenefitVipT();
            string custNo = base.gmktUserProfile.CustNo;

            string[] strEid = new string[3];
            int i=0;
            foreach (var item in AttendanceBannerGlobal)
	        {   
		        strEid[i]= item.Eid;
                i++;
	        }
            int DownloadedCoupon = 0;
            if (eidCnt == 1)
            {
                if (AttendanceCount >= 10)
                {

                 
                    t = new MyBenefitVipBiz().SelectGetEventApplyCount(custNo, startDate.ToShortDateString(), strEid[0], "", "", "");
                    if (t != null)
                    {
                        DownloadedCoupon = t.apply_cnt.xToInt32();
                    }                                      
                    if (DownloadedCoupon == 0)
                    {
                        return "CommonApplyEventPlatformMobile('" + str + "', '" + encStr + "', 'N', 'KO')";
                    }
                    else
                    {
                        return "javascript:alert('이미 응모 하셨습니다.');";
                    }
                }
                else
                {
                    return "javascript:alert('10회 이상 출석 체크 후 응모해주세요');";
                }
            }
            else if (eidCnt == 2)
            {
                if (AttendanceCount >= 15)
                {            
                    t = new MyBenefitVipBiz().SelectGetEventApplyCount(custNo, startDate.ToShortDateString(), strEid[1], "", "", "");
                    if (t != null)
                    {
                        DownloadedCoupon = t.apply_cnt.xToInt32();
                    }                                           
                    if (DownloadedCoupon == 0)
                    {
                        return "CommonApplyEventPlatformMobile('" + str + "', '" + encStr + "', 'N', 'KO')";
                    }
                    else
                    {
                        return "javascript:alert('이미 응모 하셨습니다.');";
                    }
                }
                else
                {
                    return "javascript:alert('15회 이상 출석 체크 후 응모해주세요');";
                }
            }
            else if (eidCnt == 3)
            {
                if (AttendanceCount >= lastDate)
                {
                    t = new MyBenefitVipBiz().SelectGetEventApplyCount(custNo, startDate.ToShortDateString(), strEid[2], "", "", "");
                    if (t != null)
                    {
                        DownloadedCoupon = t.apply_cnt.xToInt32();
                    }   
                    if (DownloadedCoupon == 0)
                    {
                        return "CommonApplyEventPlatformMobile('" + str + "', '" + encStr + "', 'N', 'KO')";
                    }
                    else
                    {
                        return "javascript:alert('이미 응모 하셨습니다.');";
                    }
                }
                else
                {
                    return "javascript:alert('100% 출석 후 응모 가능합니다.');";
                }
            }
            else
            {
                return "javascript:alert('응모 조건을 확인해주세요.');";
            }
        }
        
        #endregion
	}
}

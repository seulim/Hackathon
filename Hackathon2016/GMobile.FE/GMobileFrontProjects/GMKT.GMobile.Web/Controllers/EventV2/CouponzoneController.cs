using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Web.Models.EventV2;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;
using GMKT.GMobile.Biz.EventV2;
using GMKT.GMobile.Biz;
using GMobile.Data.EventZone;
using GMobile.Service.EventZone;
using GMKT.Framework.Security;
using GMKT.GMobile.Util;
using GMKT.GMobile.Data.Member;
using GMKT.Framework;

namespace GMKT.GMobile.Web.Controllers.EventV2
{
    public class CouponzoneController : EventControllerBase
    {
        private const string TOP_BANNER_EVENT_MANAGE_TYPE = "25";
        private const string TOP_BANNER_EXPOSE_TARGET_TYPE = "03";

        #region member variables
        /// <summary>
        /// View로 넘겨줄 PluszoneModel입니다. 직접 접근보다는 getter, setter를 통한 접근을 권장합니다.
        /// </summary>
        private TotalCouponzoneModel _totalCouponzoneModel;
        #endregion

        #region getter, setter
        protected List<NavigationIconT> NaviIcons
        {
            get
            {
                return _totalCouponzoneModel.NaviIcons;
            }
            set
            {
                _totalCouponzoneModel.NaviIcons = value;
            }
        }

        protected BannerT TopBanner
        {
            get
            {
                return _totalCouponzoneModel.TopBanner;
            }
            set
            {
                _totalCouponzoneModel.TopBanner = value;
            }
        }

        protected CouponzoneModel Couponzone
        {
            get
            {
                return _totalCouponzoneModel.Couponzone;
            }
            set
            {
                _totalCouponzoneModel.Couponzone = value;
            }
        }

        protected SpecialCouponModel SpecialCoupon
        {
            get
            {
                return _totalCouponzoneModel.SpecialCoupon;
            }
            set
            {
                _totalCouponzoneModel.SpecialCoupon = value;
            }
        }

        public BuyerGradeBenefitModel BuyerGradeBenefit
        {
            get
            {
                return _totalCouponzoneModel.BuyerGradeBenefit;
            }
            set
            {
                _totalCouponzoneModel.BuyerGradeBenefit = value;
            }
        }

        protected List<ExchangeCouponT> ExchangeCouponBanner
        {
            get
            {
                return _totalCouponzoneModel.ExchangeCouponBanner;
            }
            set
            {
                _totalCouponzoneModel.ExchangeCouponBanner = value;
            }
        }

        protected List<BannerInfoT> TopSeasonBanner
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.TopSeasonBanner;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.TopSeasonBanner = value;
            }
        }

        protected List<PromotionBannerInfoT> ProCouponBanner
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.ProCouponBanner;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.ProCouponBanner = value;
            }
        }

        protected List<BannerInfoT> BottomCouponBanner
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.BottomCouponBanner;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.BottomCouponBanner = value;
            }
        }

        protected List<ExtendGradeCouponT> GradeCoupon
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.GradeCoupon;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.GradeCoupon = value;
            }
        }

        protected GradeBenefitsT GradeBenefit
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.GradeBenefit;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.GradeBenefit = value;
            }
        }

        protected MemberCouponsT MemberCoupon
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.MemberCoupon;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.MemberCoupon = value;
            }
        }

        protected ExchangeCouponsT ExchangeCoupon
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.ExchangeCoupon;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.ExchangeCoupon = value;
            }
        }

		protected CouponzoneGuidePopupT MobileGuidePopup
		{
			get
			{
				return _totalCouponzoneModel.Couponzone.MobileGuidePopup;
			}
			set
			{
				_totalCouponzoneModel.Couponzone.MobileGuidePopup = value;
			}
		}

        protected List<NoticeT> NoticeListGrade
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.NoticeListGrade;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.NoticeListGrade = value;
            }
        }

        protected List<NoticeT> NoticeListMember
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.NoticeListMember;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.NoticeListMember = value;
            }
        }

        protected List<NoticeT> NoticeListExchange
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.NoticeListExchange;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.NoticeListExchange = value;
            }
        }

        protected List<NoticeT> NoticeListSpecial
        {
            get
            {
                return _totalCouponzoneModel.Couponzone.NoticeListSpecial;
            }
            set
            {
                _totalCouponzoneModel.Couponzone.NoticeListSpecial = value;
            }
        }

        protected List<SpecialCouponTabT> CouponTabList
        {
            get
            {
                return _totalCouponzoneModel.SpecialCoupon.CouponTabList;
            }
            set
            {
                _totalCouponzoneModel.SpecialCoupon.CouponTabList = value;
            }
        }

        protected List<GradeCouponInfoT> CurrentGradeModel
        {
            get
            {
                return _totalCouponzoneModel.BuyerGradeBenefit.CurrentGradeModel;
            }
            set
            {
                _totalCouponzoneModel.BuyerGradeBenefit.CurrentGradeModel = value;
            }
        }

        protected List<CardEventCouponT> CardEventCoupon
        {
            get
            {
                return _totalCouponzoneModel.CardEventCoupon;
            }
            set
            {
                _totalCouponzoneModel.CardEventCoupon = value;
            }
        }

		protected List<MonthlyCouponT> MonthlyCoupon
        {
            get
            {
                return _totalCouponzoneModel.MonthlyCoupon;
            }
            set
            {
                _totalCouponzoneModel.MonthlyCoupon = value;
            }
        }

        protected List<MonthlyCouponT> MonthlyCoupon1
        {
            get
            {
                return _totalCouponzoneModel.MonthlyCoupon1;
            }
            set
            {
                _totalCouponzoneModel.MonthlyCoupon1 = value;
            }
        }
        
        protected List<MonthlyCouponT> MonthlyCoupon2
        {
            get
            {
                return _totalCouponzoneModel.MonthlyCoupon2;
            }
            set
            {
                _totalCouponzoneModel.MonthlyCoupon2 = value;
            }
        }

		protected MemberGradeCrossModel MemberGradeCross
		{
			get
			{
				return _totalCouponzoneModel.MemberGradeCross;
			}
			set
			{
				_totalCouponzoneModel.MemberGradeCross = value;
			}
		}

        protected bool IsTestIdCheck
        {
            get {
                return _totalCouponzoneModel.IsTestIdCheck;
            }

            set {
                _totalCouponzoneModel.IsTestIdCheck = value;
            }
        }

        #endregion

        #region constructor
        /// <summary>
		/// 기본 Constructor입니다. 내부 변수를 초기화합니다.
		/// </summary>
        public CouponzoneController()
		{
            _totalCouponzoneModel = new TotalCouponzoneModel();
		}
		#endregion

        public ActionResult Index(int tabIndex = -1)
        {
			string popKind = Request.QueryString["pop"] != null ? Request.QueryString["pop"] : "";

            //SetHomeTabName("쿠폰");
            ViewBag.Title = "쿠폰존 - G마켓 모바일";
            ViewBag.HeaderTitle = "쿠폰/출첵";
            PageAttr.HeaderType = CommonData.HeaderTypeEnum.Simple;

            #region 페이스북 공유하기
            string faceBookImage = String.Format("{0}/640/main/pluszone_ico.png", Urls.MobileImageUrlV2);
            PageAttr.FbTitle = "G마켓 쿠폰존";
            PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Couponzone";
            PageAttr.FbTagImage = faceBookImage;
            PageAttr.FbTagDescription = "G마켓 쿠폰존";
            #endregion

            IsTestIdCheck = gmktUserProfile.LoginID == "lyj0707" || gmktUserProfile.LoginID == "test4plan" || gmktUserProfile.LoginID == "lyk028" || gmktUserProfile.LoginID == "momoharry";
            

            NaviIcons = new EventCommonBiz().GetNavigationIcons();
            //TopBanner 추가
            List<CommonBannerT> topBanner = new EventCommonBiz_Cache().GetCommonTopBanner(TOP_BANNER_EVENT_MANAGE_TYPE, TOP_BANNER_EXPOSE_TARGET_TYPE);
            if (topBanner.Count > 0)
            {
                TopBanner = new BannerT(topBanner[0].ImageUrl, topBanner[0].Alt, topBanner[0].LinkUrl);
            }

            GetCouponzoneInfo(tabIndex);
            GetCardEventCoupon();

            if (DateTime.Now >= new DateTime(2016, 12, 1, 0, 0, 0) || IsTestIdCheck)  
            {
                GetMonthlyCouponNew();
                GetMonthlyCouponNew1();
                GetMonthlyCouponNew2();
            }
            else
            {
                GetMonthlyCoupon();
                GetMonthlyCoupon1();
                GetMonthlyCoupon2();
            }
			
            GetBuyerGradeBenefit();

            //16-11-11 크로스체크 호출 오류로 인해 주석처리 nis733
			//GetMemberGradeCross();

			ViewBag.pop = popKind;
            return View("~/Views/EventV2/couponzone/Index.cshtml", _totalCouponzoneModel);
        }

        protected void GetCardEventCoupon()
        {
            List<CardEventCouponT> CardEventCoupons = new List<CardEventCouponT>();
            
            CardEventCouponT Shinhan = new CardEventCouponT();
            Shinhan.Type = "Shinhan";
            Shinhan.Eid = 134239;
            Shinhan.EidEncryptedString = EncryptForEventPlatform(Shinhan.Eid);
            MyBenefitCouponT ShinWinner = new MyBenefitBiz().SelectCoupon(Shinhan.Eid.ToString(), base.gmktUserProfile.CustNo);
            Shinhan.DownloadedCoupon = ShinWinner.CouponCnt;
            CardEventCoupons.Add(Shinhan);

            CardEventCouponT Lotte = new CardEventCouponT();
            Lotte.Type = "Lotte";
            Lotte.Eid = 134240;
            Lotte.EidEncryptedString = EncryptForEventPlatform(Lotte.Eid);
            MyBenefitCouponT LotWinner = new MyBenefitBiz().SelectCoupon(Lotte.Eid.ToString(), base.gmktUserProfile.CustNo);
            Lotte.DownloadedCoupon = LotWinner.CouponCnt;
            CardEventCoupons.Add(Lotte);

            CardEventCoupon = CardEventCoupons;
        }

        //이달의 스페셜 쿠폰     ------  (2016년12월1일부터 호출되는 메서드)
        protected void GetMonthlyCouponNew()
        {  
            List<MonthlyCouponT> MonthlyCoupons = new List<MonthlyCouponT>();

            bool isTest = Request.Url.Host.IndexOf("dev") > -1 || Request.Url.Host.IndexOf("local") > -1 || Request.Url.Host.IndexOf("md") > -1 || Request.Url.Host.IndexOf("mst") > -1;
            int cnt =3;

            int[] testEids = { 142113, 142114, 142115 };
            int[] realEids = { 142113, 142114, 142115 };//  global/KR 둘중에하나만 받도록 함(global EID 별도전달)
            int[] globalEids = { 142113, 142114, 142115 };  //글로벌 같음()        

            string[] cssClasses = { "cl01", "cl02", "cl03", "cl04", "cl07" };  //개편후 안씀

            string[] imageUrls = {  "/mobile/main/coupon/img_special_cp01.jpg"
                                                 , "/mobile/main/coupon/img_special_cp02.jpg"
                                                 , "/mobile/main/coupon/img_special_cp03.jpg"   };
            string[] titles = {  "패션 / 뷰티 10% 할인"
                                        , "리빙 / 레저 10% 할인"
                                        , "마트 / 식품 5% 할인" };
            string[] descriptions = {  "10% 할인쿠폰. 1만5천원이상 구매시 최대5천원"
                                                    , "10% 할인쿠폰. 1만5천원이상 구매시 최대5천원"
                                                    , "5% 할인쿠폰. 1만5천원이상 구매시 최대5천원" };
            string[] texts =  {  "10% 할인쿠폰. 1만5천원이상 구매시 최대5천원"
                                        , "10% 할인쿠폰. 1만5천원이상 구매시 최대5천원"
                                        , "5% 할인쿠폰. 1만5천원이상 구매시 최대5천원" };

            bool[] hasTags = { true, true, true };
            bool[] isEverydayCoupon = {  false, false, false };

            //mo : 1일 9시부터 발급, mo2 : 발급시간 제한 없음
            string[] couponTypeStr = {  "mo2", "mo2", "mo2" };
            string[] checkType = {  null, null, null };

            for (var i = 0; i < cnt; i++)
            {
                MonthlyCouponT monthlyCoupon = new MonthlyCouponT();

                monthlyCoupon.Eid = isTest ? testEids[i] : realEids[i];
                monthlyCoupon.EidEncryptedString = EncryptForEventPlatform(monthlyCoupon.Eid);
                                               
                int globalDownloadCount = 0;
              
                if (isEverydayCoupon[i])
                {
                    DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
                    DateTime endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);

                    globalDownloadCount  = new CouponzoneBiz().GetCouponDownloadedCount(globalEids[i], startDate, endDate);
                    if (globalDownloadCount == 0)
                    {
                        monthlyCoupon.DownloadedCoupon = new CouponzoneBiz().GetCouponDownloadedCount(monthlyCoupon.Eid, startDate, endDate);
                    }
                    else
                    {
                        monthlyCoupon.DownloadedCoupon = globalDownloadCount;
                    }
                }
                else
                {
                    MyBenefitCouponT globalWinner = new MyBenefitBiz().SelectCoupon(globalEids[i].ToString(), base.gmktUserProfile.CustNo);
                    globalDownloadCount = (globalWinner != null) ? globalWinner.CouponCnt : 0;

                    if (globalDownloadCount == 0)
                    {
                        MyBenefitCouponT monthlyWinner = new MyBenefitBiz().SelectCoupon(monthlyCoupon.Eid.ToString(), base.gmktUserProfile.CustNo);
                        monthlyCoupon.DownloadedCoupon = (monthlyWinner != null) ? monthlyWinner.CouponCnt : 0;
                    }
                    else
                    {
                        monthlyCoupon.DownloadedCoupon = globalDownloadCount;
                    }
                }

                monthlyCoupon.CssClass = cssClasses[i];
                monthlyCoupon.ImageUrl = Urls.MobileImagePics + imageUrls[i];
                monthlyCoupon.Title = titles[i];
                monthlyCoupon.Description = descriptions[i];
                monthlyCoupon.Text = texts[i];
                monthlyCoupon.HasTag = hasTags[i];
                monthlyCoupon.CouponType = couponTypeStr[i];
                monthlyCoupon.CheckType = checkType[i];

                MonthlyCoupons.Add(monthlyCoupon);
            }

            MonthlyCoupon = MonthlyCoupons;
        }

        //스마일 쿠폰  영역       ------  (2016년12월1일부터 호출되는 메서드)
        protected void GetMonthlyCouponNew1()
        {  
            List<MonthlyCouponT> MonthlyCoupons1 = new List<MonthlyCouponT>();

            bool isTest = Request.Url.Host.IndexOf("dev") > -1 || Request.Url.Host.IndexOf("local") > -1 || Request.Url.Host.IndexOf("md") > -1 || Request.Url.Host.IndexOf("mst") > -1;

            int cnt = 1;
            int[] testEids = { 142116 };
            int[] realEids = { 142116 };
            string[] cssClasses = { "cl05" };
            string[] imageUrls = { "/mobile/main/coupon/img_smilepay_cp01.jpg" };
            string[] titles = { "스마일페이 이용시 천원 할인" };
            string[] descriptions = { "1천원 할인쿠폰. 1만원이상 구매시 사용" };
            string[] texts = { "1천원 할인쿠폰. 1만원이상 구매시 사용" };
            bool[] hasTags = { false };
            bool[] isEverydayCoupon = { false };
            //mo : 1일 9시부터 발급, mo2 : 발급시간 제한 없음
            string[] couponTypeStr = { "mo2" };
            string[] checkType = { null };           

            for (var i = 0; i < cnt; i++)
            {
                MonthlyCouponT monthlyCoupon1 = new MonthlyCouponT();

                monthlyCoupon1.Eid = isTest ? testEids[i] : realEids[i];
                monthlyCoupon1.EidEncryptedString = EncryptForEventPlatform(monthlyCoupon1.Eid);
                if (isEverydayCoupon[i])
                {
                    DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
                    DateTime endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
                    monthlyCoupon1.DownloadedCoupon = new CouponzoneBiz().GetCouponDownloadedCount(monthlyCoupon1.Eid, startDate, endDate);
                }
                else
                {
                    MyBenefitCouponT monthlyWinner = new MyBenefitBiz().SelectCoupon(monthlyCoupon1.Eid.ToString(), base.gmktUserProfile.CustNo);
                    monthlyCoupon1.DownloadedCoupon = (monthlyWinner != null) ? monthlyWinner.CouponCnt : 0;
                }

                monthlyCoupon1.CssClass = cssClasses[i];
                monthlyCoupon1.ImageUrl = Urls.MobileImagePics + imageUrls[i];
                monthlyCoupon1.Title = titles[i];
                monthlyCoupon1.Description = descriptions[i];
                monthlyCoupon1.Text = texts[i];
                monthlyCoupon1.HasTag = hasTags[i];
                monthlyCoupon1.CouponType = couponTypeStr[i];
                monthlyCoupon1.CheckType = checkType[i];

                MonthlyCoupons1.Add(monthlyCoupon1);
            }

            MonthlyCoupon1 = MonthlyCoupons1;
        }

        //웰컴혜택                     ------  (2016년12월1일부터 호출되는 메서드)
        protected void GetMonthlyCouponNew2()
        {
            List<MonthlyCouponT> MonthlyCoupons2 = new List<MonthlyCouponT>();

            bool isTest = Request.Url.Host.IndexOf("dev") > -1 || Request.Url.Host.IndexOf("local") > -1 || Request.Url.Host.IndexOf("md") > -1 || Request.Url.Host.IndexOf("mst") > -1;
            int cnt = 2;

            int[] testEids = { 142111, 142112 };  //국문 영문 동시 사용 EID
            int[] realEids = { 142111, 142112 };            

            string[] cssClasses = { "cl01", "cl02", "cl03", "cl04", "cl07" };  //개편후 안씀 예비로 유지함.

            string[] imageUrls = { "/mobile/main/coupon/img_welcome_cp01.jpg"
                                                 , "/mobile/main/coupon/img_welcome_cp01.jpg" };

            string[] titles = { "신규 가입 30% 할인"
                                  , "2016년 첫구매 30% 할인" };

            string[] descriptions = { "30% 할인쿠폰. 1만5천원이상 구매시 최대1만원"
                                                    , "30% 할인쿠폰. 1만5천원이상 구매시 최대1만원" };
            string[] texts = { "30% 할인쿠폰. 1만5천원이상 구매시 최대1만원"
                                      , "30% 할인쿠폰. 1만5천원이상 구매시 최대1만원" };
                     
            bool[] hasTags = { false, false };
            bool[] isEverydayCoupon = { false, false };
            //mo : 1일 9시부터 발급, mo2 : 발급시간 제한 없음
            string[] couponTypeStr = { "mo2", "mo2" };
            string[] checkType = { null, "BUY_CHECK" };

            for (var i = 0; i < cnt; i++)
            {
                MonthlyCouponT monthlyCoupon2 = new MonthlyCouponT();

                monthlyCoupon2.Eid = isTest ? testEids[i] : realEids[i];
                monthlyCoupon2.EidEncryptedString = EncryptForEventPlatform(monthlyCoupon2.Eid);
                
                if (isEverydayCoupon[i])
                {
                    DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
                    DateTime endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
       
                    monthlyCoupon2.DownloadedCoupon = new CouponzoneBiz().GetCouponDownloadedCount(monthlyCoupon2.Eid, startDate, endDate);
                }
                else
                {
                    MyBenefitCouponT monthlyWinner = new MyBenefitBiz().SelectCoupon(monthlyCoupon2.Eid.ToString(), base.gmktUserProfile.CustNo);
                    monthlyCoupon2.DownloadedCoupon = (monthlyWinner != null) ? monthlyWinner.CouponCnt : 0;       
                }

                monthlyCoupon2.CssClass = cssClasses[i];
                monthlyCoupon2.ImageUrl = Urls.MobileImagePics + imageUrls[i];
                monthlyCoupon2.Title = titles[i];
                monthlyCoupon2.Description = descriptions[i];
                monthlyCoupon2.Text = texts[i];
                monthlyCoupon2.HasTag = hasTags[i];
                monthlyCoupon2.CouponType = couponTypeStr[i];
                monthlyCoupon2.CheckType = checkType[i];

                MonthlyCoupons2.Add(monthlyCoupon2);
            }

            MonthlyCoupon2 = MonthlyCoupons2;
        }

        //이달의 스페셜 쿠폰     ------  (2016년11월1일부터 호출되는 메서드)
        protected void GetMonthlyCoupon()
        {
            List<MonthlyCouponT> MonthlyCoupons = new List<MonthlyCouponT>();

            bool isTest = Request.Url.Host.IndexOf("dev") > -1 || Request.Url.Host.IndexOf("local") > -1 || Request.Url.Host.IndexOf("md") > -1 || Request.Url.Host.IndexOf("mst") > -1;
            int cnt = 3;

            int[] testEids = { 141510, 141511, 141512 };
            int[] realEids = { 141510, 141511, 141512 };//  global/KR 둘중에하나만 받도록 함(global EID 별도전달)
            int[] globalEids = { 141550, 141551, 141552 };

            string[] cssClasses = { "cl01", "cl02", "cl03", "cl04", "cl07" };  //개편후 안씀

            string[] imageUrls = {  "/mobile/main/coupon/img_special_cp01.jpg"
                                                 , "/mobile/main/coupon/img_special_cp02.jpg"
                                                 , "/mobile/main/coupon/img_special_cp03.jpg"   };
            string[] titles = {  "패션 / 뷰티 10% 할인"
                                        , "리빙 / 레저 10% 할인"
                                        , "마트 / 식품 5% 할인" };
            string[] descriptions = {  "10% 할인쿠폰. 1만5천원이상 구매시 최대5천원"
                                                    , "10% 할인쿠폰. 1만5천원이상 구매시 최대5천원"
                                                    , "5% 할인쿠폰. 1만5천원이상 구매시 최대5천원" };
            string[] texts =  {  "10% 할인쿠폰. 1만5천원이상 구매시 최대5천원"
                                        , "10% 할인쿠폰. 1만5천원이상 구매시 최대5천원"
                                        , "5% 할인쿠폰. 1만5천원이상 구매시 최대5천원" };

            bool[] hasTags = { true, true, true };
            bool[] isEverydayCoupon = { false, false, false };

            //mo : 1일 9시부터 발급, mo2 : 발급시간 제한 없음
            string[] couponTypeStr = { "mo2", "mo2", "mo2" };
            string[] checkType = { null, null, null };

            for (var i = 0; i < cnt; i++)
            {
                MonthlyCouponT monthlyCoupon = new MonthlyCouponT();

                monthlyCoupon.Eid = isTest ? testEids[i] : realEids[i];
                monthlyCoupon.EidEncryptedString = EncryptForEventPlatform(monthlyCoupon.Eid);

                int globalDownloadCount = 0;

                if (isEverydayCoupon[i])
                {
                    DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
                    DateTime endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);

                    globalDownloadCount = new CouponzoneBiz().GetCouponDownloadedCount(globalEids[i], startDate, endDate);
                    if (globalDownloadCount == 0)
                    {
                        monthlyCoupon.DownloadedCoupon = new CouponzoneBiz().GetCouponDownloadedCount(monthlyCoupon.Eid, startDate, endDate);
                    }
                    else
                    {
                        monthlyCoupon.DownloadedCoupon = globalDownloadCount;
                    }
                }
                else
                {
                    MyBenefitCouponT globalWinner = new MyBenefitBiz().SelectCoupon(globalEids[i].ToString(), base.gmktUserProfile.CustNo);
                    globalDownloadCount = (globalWinner != null) ? globalWinner.CouponCnt : 0;

                    if (globalDownloadCount == 0)
                    {
                        MyBenefitCouponT monthlyWinner = new MyBenefitBiz().SelectCoupon(monthlyCoupon.Eid.ToString(), base.gmktUserProfile.CustNo);
                        monthlyCoupon.DownloadedCoupon = (monthlyWinner != null) ? monthlyWinner.CouponCnt : 0;
                    }
                    else
                    {
                        monthlyCoupon.DownloadedCoupon = globalDownloadCount;
                    }
                }

                monthlyCoupon.CssClass = cssClasses[i];
                monthlyCoupon.ImageUrl = Urls.MobileImagePics + imageUrls[i];
                monthlyCoupon.Title = titles[i];
                monthlyCoupon.Description = descriptions[i];
                monthlyCoupon.Text = texts[i];
                monthlyCoupon.HasTag = hasTags[i];
                monthlyCoupon.CouponType = couponTypeStr[i];
                monthlyCoupon.CheckType = checkType[i];

                MonthlyCoupons.Add(monthlyCoupon);
            }

            MonthlyCoupon = MonthlyCoupons;
        }

        //스마일 쿠폰  영역       ------  (2016년11월1일부터 호출되는 메서드)
        protected void GetMonthlyCoupon1()
        {
            List<MonthlyCouponT> MonthlyCoupons1 = new List<MonthlyCouponT>();

            bool isTest = Request.Url.Host.IndexOf("dev") > -1 || Request.Url.Host.IndexOf("local") > -1 || Request.Url.Host.IndexOf("md") > -1 || Request.Url.Host.IndexOf("mst") > -1;

            int cnt = 1;
            int[] testEids = { 141513 };
            int[] realEids = { 141513 };
            string[] cssClasses = { "cl05" };
            string[] imageUrls = { "/mobile/main/coupon/img_smilepay_cp01.jpg" };
            string[] titles = { "스마일페이 이용시 천원 할인" };
            string[] descriptions = { "1천원 할인쿠폰. 1만원이상 구매시 사용" };
            string[] texts = { "1천원 할인쿠폰. 1만원이상 구매시 사용" };
            bool[] hasTags = { false };
            bool[] isEverydayCoupon = { false };
            //mo : 1일 9시부터 발급, mo2 : 발급시간 제한 없음
            string[] couponTypeStr = { "mo2" };
            string[] checkType = { null };

            for (var i = 0; i < cnt; i++)
            {
                MonthlyCouponT monthlyCoupon1 = new MonthlyCouponT();

                monthlyCoupon1.Eid = isTest ? testEids[i] : realEids[i];
                monthlyCoupon1.EidEncryptedString = EncryptForEventPlatform(monthlyCoupon1.Eid);
                if (isEverydayCoupon[i])
                {
                    DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
                    DateTime endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);
                    monthlyCoupon1.DownloadedCoupon = new CouponzoneBiz().GetCouponDownloadedCount(monthlyCoupon1.Eid, startDate, endDate);
                }
                else
                {
                    MyBenefitCouponT monthlyWinner = new MyBenefitBiz().SelectCoupon(monthlyCoupon1.Eid.ToString(), base.gmktUserProfile.CustNo);
                    monthlyCoupon1.DownloadedCoupon = (monthlyWinner != null) ? monthlyWinner.CouponCnt : 0;
                }

                monthlyCoupon1.CssClass = cssClasses[i];
                monthlyCoupon1.ImageUrl = Urls.MobileImagePics + imageUrls[i];
                monthlyCoupon1.Title = titles[i];
                monthlyCoupon1.Description = descriptions[i];
                monthlyCoupon1.Text = texts[i];
                monthlyCoupon1.HasTag = hasTags[i];
                monthlyCoupon1.CouponType = couponTypeStr[i];
                monthlyCoupon1.CheckType = checkType[i];

                MonthlyCoupons1.Add(monthlyCoupon1);
            }

            MonthlyCoupon1 = MonthlyCoupons1;
        }

        //웰컴혜택                     ------  (2016년11월1일부터 호출되는 메서드)
        protected void GetMonthlyCoupon2()
        {
            List<MonthlyCouponT> MonthlyCoupons2 = new List<MonthlyCouponT>();

            bool isTest = Request.Url.Host.IndexOf("dev") > -1 || Request.Url.Host.IndexOf("local") > -1 || Request.Url.Host.IndexOf("md") > -1 || Request.Url.Host.IndexOf("mst") > -1;
            int cnt = 2;

            int[] testEids = { 141508, 141509 };  //국문 영문 동시 사용 EID
            int[] realEids = { 141508, 141509 };

            string[] cssClasses = { "cl01", "cl02", "cl03", "cl04", "cl07" };  //개편후 안씀 예비로 유지함.

            string[] imageUrls = { "/mobile/main/coupon/img_welcome_cp01.jpg"
                                                 , "/mobile/main/coupon/img_welcome_cp01.jpg" };

            string[] titles = { "신규 가입 30% 할인"
                                  , "2016년 첫구매 30% 할인" };

            string[] descriptions = { "30% 할인쿠폰. 1만5천원이상 구매시 최대1만원"
                                                    , "30% 할인쿠폰. 1만5천원이상 구매시 최대1만원" };
            string[] texts = { "30% 할인쿠폰. 1만5천원이상 구매시 최대1만원"
                                      , "30% 할인쿠폰. 1만5천원이상 구매시 최대1만원" };

            bool[] hasTags = { false, false };
            bool[] isEverydayCoupon = { false, false };
            //mo : 1일 9시부터 발급, mo2 : 발급시간 제한 없음
            string[] couponTypeStr = { "mo2", "mo2" };
            string[] checkType = { null, "BUY_CHECK" };

            for (var i = 0; i < cnt; i++)
            {
                MonthlyCouponT monthlyCoupon2 = new MonthlyCouponT();

                monthlyCoupon2.Eid = isTest ? testEids[i] : realEids[i];
                monthlyCoupon2.EidEncryptedString = EncryptForEventPlatform(monthlyCoupon2.Eid);

                if (isEverydayCoupon[i])
                {
                    DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0);
                    DateTime endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 23, 59, 59);

                    monthlyCoupon2.DownloadedCoupon = new CouponzoneBiz().GetCouponDownloadedCount(monthlyCoupon2.Eid, startDate, endDate);
                }
                else
                {
                    MyBenefitCouponT monthlyWinner = new MyBenefitBiz().SelectCoupon(monthlyCoupon2.Eid.ToString(), base.gmktUserProfile.CustNo);
                    monthlyCoupon2.DownloadedCoupon = (monthlyWinner != null) ? monthlyWinner.CouponCnt : 0;
                }

                monthlyCoupon2.CssClass = cssClasses[i];
                monthlyCoupon2.ImageUrl = Urls.MobileImagePics + imageUrls[i];
                monthlyCoupon2.Title = titles[i];
                monthlyCoupon2.Description = descriptions[i];
                monthlyCoupon2.Text = texts[i];
                monthlyCoupon2.HasTag = hasTags[i];
                monthlyCoupon2.CouponType = couponTypeStr[i];
                monthlyCoupon2.CheckType = checkType[i];

                MonthlyCoupons2.Add(monthlyCoupon2);
            }

            MonthlyCoupon2 = MonthlyCoupons2;
        }


        protected void GetCouponzoneInfo(int tabIndex)
        {
            string custNo = base.gmktUserProfile.CustNo;
            custNo = GMKTCryptoLibrary.EncryptPrivateInfo(custNo);
            TotalCouponzoneDataT totalCouponzoneInfo = new CouponzoneBiz_Cache().GetCouponzoneInfo(custNo);

            if (totalCouponzoneInfo != null)
            {
                if (totalCouponzoneInfo.Couponzone == null)
                {
                    TopSeasonBanner = new List<BannerInfoT>();
                    ProCouponBanner = new List<PromotionBannerInfoT>();
                    BottomCouponBanner = new List<BannerInfoT>();
                    GradeCoupon = new List<ExtendGradeCouponT>();
                    GradeBenefit = new GradeBenefitsT();
                    MemberCoupon = new MemberCouponsT();
                    ExchangeCoupon = new ExchangeCouponsT();
					MobileGuidePopup = ConvertToCouponzoneGuidePopup(null);
                    NoticeListGrade = new List<NoticeT>();
                    NoticeListMember = new List<NoticeT>();
                    NoticeListExchange = new List<NoticeT>();
                    NoticeListSpecial = new List<NoticeT>();
                }
                else
                {
                    TopSeasonBanner = totalCouponzoneInfo.Couponzone.TopSeasonBanner;
                    ProCouponBanner = totalCouponzoneInfo.Couponzone.ProCouponBanner;
                    BottomCouponBanner = totalCouponzoneInfo.Couponzone.BottomCouponBanner;
										GradeBenefit = totalCouponzoneInfo.Couponzone.GradeBenefit;
                    GradeCoupon = ConvertGradeCoupons(totalCouponzoneInfo.Couponzone.GradeCoupon);
                    MemberCoupon = totalCouponzoneInfo.Couponzone.MemberCoupon;					
                    if (MemberCoupon != null)
                    {
                        MemberCoupon.MemberCryptoNos = GMKTCryptoLibraryOption.EncodeTo64(GMKTCryptoLibraryOption.DesEncrypt(totalCouponzoneInfo.Couponzone.MemberCoupon.Seq + "|" + totalCouponzoneInfo.Couponzone.MemberCoupon.Eid + "|", CRYPT_MD5_FOOTER));
												int winnerCount = new CouponzoneBiz().GetDownloadedCouponPackCount( Convert.ToInt64( MemberCoupon.Eid ) );
												MemberCoupon.DownloadedCoupon = winnerCount;						
						MemberCoupon.Count = 0;
						if(MemberCoupon.Fashion != null) MemberCoupon.Count += MemberCoupon.Fashion.Count;
						if(MemberCoupon.Baby != null) MemberCoupon.Count += MemberCoupon.Baby.Count;
						if(MemberCoupon.Digital != null) MemberCoupon.Count += MemberCoupon.Digital.Count;
						if(MemberCoupon.Books != null) MemberCoupon.Count += MemberCoupon.Books.Count;
                    }
                    else
                    {
                        MemberCoupon = new MemberCouponsT();
						MemberCoupon.Count = 0;
                    }
                    ExchangeCouponBanner = ConvertExchangeCoupon(totalCouponzoneInfo.Couponzone.ExchangeCoupon);
                    NoticeListGrade = totalCouponzoneInfo.Couponzone.NoticeListGrade;
                    NoticeListMember = totalCouponzoneInfo.Couponzone.NoticeListMember;
                    NoticeListExchange = totalCouponzoneInfo.Couponzone.NoticeListExchange;
                    NoticeListSpecial = totalCouponzoneInfo.Couponzone.NoticeListSpecial;
					MobileGuidePopup = ConvertToCouponzoneGuidePopup(totalCouponzoneInfo.Couponzone.MobileGuidePopup);
					
                }

                if (totalCouponzoneInfo.SpecialCoupon == null)
                {
                    CouponTabList = new List<SpecialCouponTabT>();
                }
                else
                {
                    foreach (var couponTabList in totalCouponzoneInfo.SpecialCoupon.CouponTabList)
                    {
                        SpecialCouponTabT CouponTab = new SpecialCouponTabT();
                        CouponTab = couponTabList;
                        foreach (SpecialCouponInfoT coupon in couponTabList.CouponList)
                        {
                            SpecialCouponInfoT CouponInfo = new SpecialCouponInfoT();
                            int ceid = coupon.Eid == null ? 0 : (int)coupon.Eid;

                            MyBenefitCouponT winner = new MyBenefitBiz().SelectCoupon(ceid.ToString(), base.gmktUserProfile.CustNo);
                            coupon.DownloadedCoupon = winner.CouponCnt;
                            coupon.EidEncryptedString = EncryptForEventPlatform(ceid);
                        }

                        CouponTab.CouponList = CouponTab.CouponList;
                        CouponTabList.Add(CouponTab);
                    }
                    SpecialCoupon.SelectedIndex = tabIndex;
                }                
            }
        }

        protected DateTime GetLastDayOfMonth(DateTime dtDate)
        {
            DateTime dtTo = dtDate;
            dtTo = dtTo.AddMonths(1);
            dtTo = dtTo.AddDays(-(dtTo.Day));
            return dtTo;
        }

		private CouponzoneGuidePopupT ConvertToCouponzoneGuidePopup(GuidePopupT data)
		{
			CouponzoneGuidePopupT result = new CouponzoneGuidePopupT();
			result.GradeCouponGuideImageUrl = Urls.MobileImageShortUrl + "/mobile.gmarket/images/640/main/layer/layer_cp_mem.png";
            result.MemberCouponGuideImageUrl = "";//Urls.MobileImageShortUrl + "/mobile.gmarket/images/640/main/layer/layer_cpinfo.png";			
            result.ExchangeCouponGuideImageUrl = Urls.MobileImagePics + "/mobile/main/layer/layer_cp_change.png"; 
			result.SpecialCouponGuideImageUrl = Urls.MobileImageShortUrl + "/mobile.gmarket/images/640/main/layer/layer_cp_special.png";

			if (data == null)
			{
				return result;
			}

			if (data.GradeCoupon != null && data.GradeCoupon.Count > 0 && !String.IsNullOrEmpty(data.GradeCoupon[0].ImageMobile))
			{
				result.GradeCouponGuideImageUrl = data.GradeCoupon[0].ImageMobile;
			}

			if (data.MemberCoupon != null && data.MemberCoupon.Count > 0 && !String.IsNullOrEmpty(data.MemberCoupon[0].ImageMobile))
			{
				result.MemberCouponGuideImageUrl = data.MemberCoupon[0].ImageMobile;
			}

			if (data.ExchangeCoupon != null && data.ExchangeCoupon.Count > 0 && !String.IsNullOrEmpty(data.ExchangeCoupon[0].ImageMobile))
			{
				result.ExchangeCouponGuideImageUrl = data.ExchangeCoupon[0].ImageMobile;
			}

			if (data.SpecialCoupon != null && data.SpecialCoupon.Count > 0 && !String.IsNullOrEmpty(data.SpecialCoupon[0].ImageMobile))
			{
				result.SpecialCouponGuideImageUrl = data.SpecialCoupon[0].ImageMobile;
			}

			return result;
		}


        protected List<ExchangeCouponT> ConvertExchangeCoupon(ExchangeCouponsT exchangeCoupons)
        {
            List<ExchangeCouponT> couponDataList = new List<ExchangeCouponT>();

            foreach(var gstamp in exchangeCoupons.Gstamp)
            {
                int valid = 0;
                foreach (EIDInfoT data in gstamp.EIDInfoList)
                {
                    if (data.Eid != "")
                    {
                        valid += 1;
                    }
                }
                gstamp.ValidCount = valid;
                couponDataList.Add(gstamp);
            }

            foreach (var gmileage in exchangeCoupons.Gmileage)
            {
                int valid = 0;
                foreach (EIDInfoT data in gmileage.EIDInfoList)
                {
                    if (data.Eid != "")
                    {
                        valid += 1;
                    }
                }
                gmileage.ValidCount = valid;
                couponDataList.Add(gmileage);
            }

            return couponDataList;
        }

        protected List<ExtendGradeCouponT> ConvertGradeCoupons(GradeCouponsT gradeCoupon)
        {
            List<ExtendGradeCouponT> expendDataList = new List<ExtendGradeCouponT>();
            DateTime nov = new DateTime(2014, 11, 1, 0, 0, 0);

            if (gradeCoupon.Svip != null)
            {
                ExtendGradeCouponT expendData = new ExtendGradeCouponT();
                expendData.Eid = gradeCoupon.Svip.Eid;
                expendData.Seq = gradeCoupon.Svip.Seq;
                expendData.Grade = "SVIP";
                expendData.NextGrade = "SVIP";
                expendData.GradeCode = "SV";
                expendData.PromotionMileage = 500;
                expendData.ClassName = "ico_svip";
				if (GradeBenefit != null && GradeBenefit.Svip != null && GradeBenefit.Svip.Count > 0)
				{
					int decodedEid = 0;
					BenefitCouponT mileage = GradeBenefit.Svip.Where(T => T.Eid != null && T.Eid != "").First();
					
					if (mileage != null)
					{
						expendData.PromotionMileageImageUrl = mileage.BannerImgMobile;
						decodedEid = GetPromotionMileageEid(mileage.Eid);
					}
					expendData.PromotionEid = decodedEid;
				}
                expendData.List = gradeCoupon.Svip.List;
                expendDataList.Add(expendData);
            }
            if (gradeCoupon.Vip != null)
            {
                ExtendGradeCouponT expendData = new ExtendGradeCouponT();
                expendData.Eid = gradeCoupon.Vip.Eid;
                expendData.Seq = gradeCoupon.Vip.Seq;
                expendData.Grade = "VIP";
                expendData.NextGrade = "SVIP";
                expendData.GradeCode = "V";
                expendData.PromotionMileage = 200;
                expendData.ClassName = "ico_vip";
				if (GradeBenefit != null && GradeBenefit.Vip != null && GradeBenefit.Vip.Count > 0)
				{
					int decodedEid = 0;
					BenefitCouponT mileage = GradeBenefit.Vip.Where(T => T.Eid != null && T.Eid != "").First();

					if (mileage != null)
					{
						expendData.PromotionMileageImageUrl = mileage.BannerImgMobile;
						decodedEid = GetPromotionMileageEid(mileage.Eid);
					}
					expendData.PromotionEid = decodedEid;
				}

                expendData.List = gradeCoupon.Vip.List;
                expendDataList.Add(expendData);
            }
            if (gradeCoupon.Family != null)
            {
                ExtendGradeCouponT expendData = new ExtendGradeCouponT();
                expendData.Eid = gradeCoupon.Family.Eid;
                expendData.Seq = gradeCoupon.Family.Seq;
                expendData.Grade = "FAMILY";
                expendData.NextGrade = "VIP";
                expendData.GradeCode = "N";
                expendData.PromotionMileage = 0;
                expendData.ClassName = "ico_family";
                if (GradeBenefit != null && GradeBenefit.Family != null && GradeBenefit.Family.Count > 0)
                {
                    int decodedEid = 0;
                    BenefitCouponT mileage = GradeBenefit.Family.Where(T => T.Eid != null && T.Eid != "").First();

                    if (mileage != null)
                    {
                        expendData.PromotionMileageImageUrl = mileage.BannerImgMobile;
                        decodedEid = GetPromotionMileageEid(mileage.Eid);
                    }
                    expendData.PromotionEid = decodedEid;
                }

                expendData.List = gradeCoupon.Family.List;
                expendDataList.Add(expendData);
            }
            /*if (gradeCoupon.Gold != null)
            {
                ExtendGradeCouponT expendData = new ExtendGradeCouponT();
                expendData.Eid = gradeCoupon.Gold.Eid;
                expendData.Seq = gradeCoupon.Gold.Seq;
                expendData.Grade = "Gold";
                expendData.NextGrade = "VIP";
                expendData.GradeCode = "G";
                expendData.PromotionMileage = 200;
                expendData.ClassName = "ico ico_gold";
				if (GradeBenefit != null && GradeBenefit.Gold != null && GradeBenefit.Gold.Count > 0)
				{
					int decodedEid = 0;
					BenefitCouponT mileage = GradeBenefit.Gold.Where(T => T.Eid != null && T.Eid != "").First();

					if (mileage != null)
					{
						expendData.PromotionMileageImageUrl = mileage.BannerImgMobile;
						decodedEid = GetPromotionMileageEid(mileage.Eid);
					}
					expendData.PromotionEid = decodedEid;
				}
                expendData.List = gradeCoupon.Gold.List;
                expendDataList.Add(expendData);
            }
            if (gradeCoupon.Silver != null)
            {
                ExtendGradeCouponT expendData = new ExtendGradeCouponT();
                expendData.Eid = gradeCoupon.Silver.Eid;
                expendData.Seq = gradeCoupon.Silver.Seq;
                expendData.Grade = "Silver";
                expendData.NextGrade = "Gold";
                expendData.GradeCode = "S";
                expendData.PromotionMileage = 100;
                expendData.ClassName = "ico ico_silver";
				if (GradeBenefit != null && GradeBenefit.Silver != null && GradeBenefit.Silver.Count > 0)
				{
					int decodedEid = 0;
					BenefitCouponT mileage = GradeBenefit.Silver.Where(T => T.Eid != null && T.Eid != "").First();

					if (mileage != null)
					{
						expendData.PromotionMileageImageUrl = mileage.BannerImgMobile;
						decodedEid = GetPromotionMileageEid(mileage.Eid);
					}
					expendData.PromotionEid = decodedEid;
				}
                expendData.List = gradeCoupon.Silver.List;
                expendDataList.Add(expendData);
            }
            if (gradeCoupon.NewGrade != null)
            {
                ExtendGradeCouponT expendData = new ExtendGradeCouponT();
                expendData.Eid = gradeCoupon.NewGrade.Eid;
                expendData.Seq = gradeCoupon.NewGrade.Seq;
                expendData.Grade = "New";
                expendData.NextGrade = "Silver";
                expendData.GradeCode = "N";
                expendData.PromotionMileage = 0;
                expendData.ClassName = "ico ico_new";
                expendData.PromotionEid = 0;
                expendData.List = gradeCoupon.NewGrade.List;
                expendDataList.Add(expendData);
            }*/
            return expendDataList;
        }

		private int GetPromotionMileageEid(string encodedEid)
		{
			int result = 0;

			if (String.IsNullOrEmpty(encodedEid))
			{
				return result;
			}

			string[] encodedEidArr;
			encodedEidArr = encodedEid.Replace(" ", "").Replace("'", "").Split(',');
			if (encodedEidArr != null && encodedEidArr.Length > 0)
			{
				string originalEidString = String.Empty;
				string deCodedEidString = GMKTCryptoLibrary.AesGCryptoDecrypt(encodedEidArr[0]);
				if (!String.IsNullOrEmpty(deCodedEidString))
				{
					string[] eids = deCodedEidString.Split(Convert.ToChar(CRYPT_MAIN_DELIMITER));
					if (eids != null && eids.Length > 1)
					{
						originalEidString = eids[1];
						Int32.TryParse(originalEidString, out result);
					}
				}
			}

			return result;
		}

        protected void GetBuyerGradeBenefit()
        {
            BuyerGradeBenefit = new BuyerGradeBenefitModel();
            //int this3MonthCount = 0;
            long iThis12MonthPoint = 0;
            int totalPurchaseCount = 0;
            if (base.gmktUserProfile.CustNo != null)
            {
                MyBenefitT totalBuyerPoint = new MyBenefitBiz().SelectGetBuyerTotalGradePoint(base.gmktUserProfile.CustNo);
                //MyBenefitT buyerGradePoint = new MyBenefitBiz().SelectGetBuyerPeriodGradePoint(base.gmktUserProfile.CustNo, DateTime.Now.AddMonths(-2).ToString("yyyy-MM-01"), DateTime.Now.AddDays(1).ToShortDateString());
                //this3MonthCount = buyerGradePoint.this_month_count;
                iThis12MonthPoint = new GMKT.Component.Member.MyInfoBiz().GetLastCustomPaidCount(base.gmktUserProfile.CustNo, -12);
                totalPurchaseCount = totalBuyerPoint.total_count;
            }

            if (string.IsNullOrEmpty(base.gmktUserProfile.CustName) == false)
            {
                if (base.gmktUserProfile.CustName.Length > 4)
                {
                    BuyerGradeBenefit.DisplayCustName = string.Format("{0}...", base.gmktUserProfile.CustName.Substring(0, 4));
                }
                else
                {
                    BuyerGradeBenefit.DisplayCustName = base.gmktUserProfile.CustName;
                }
            }

            if (gmktUserProfile.CustType != null)
            {
                if (gmktUserProfile.CustType == EnumMemberType.PersonalBuyer)
                    BuyerGradeBenefit.CustType = "PP";
                else if (gmktUserProfile.CustType == EnumMemberType.CorpBuyer)
                    BuyerGradeBenefit.CustType = "PC";
                else if (gmktUserProfile.CustType == EnumMemberType.PersonalSeller)
                    BuyerGradeBenefit.CustType = "EP";
                else if (gmktUserProfile.CustType == EnumMemberType.CorpSeller)
                    BuyerGradeBenefit.CustType = "EC";
                else if (gmktUserProfile.CustType == EnumMemberType.GlobalSeller)
                    BuyerGradeBenefit.CustType = "EG";
                else
                    BuyerGradeBenefit.CustType = "";
            }
            else
                BuyerGradeBenefit.CustType = "";


            string myGrade = convertMyGrade(base.gmktUserProfile.BuyerGrade);
            BuyerGradeBenefit.MyGrade = myGrade;
            BuyerGradeBenefit.PromotionPoint = 0;
            BuyerGradeBenefit.NextGrade = "";
						if( base.gmktUserProfile.BuyerGrade == GMKT.Framework.BuyerGradeEnum.SVIP)
						{
							ExtendGradeCouponT mileage = GradeCoupon.Where( T => T.Grade == "SVIP" ).First();
							if( mileage != null )
							{
								BuyerGradeBenefit.MileageImage = mileage.PromotionMileageImageUrl;
								BuyerGradeBenefit.MileageAlt = mileage.PromotionMileageImageAlt;
							}
						}
						else if( base.gmktUserProfile.BuyerGrade == GMKT.Framework.BuyerGradeEnum.VIP)
						{
							ExtendGradeCouponT mileage = GradeCoupon.Where( T => T.Grade == "VIP" ).First();
							if( mileage != null )
							{
								BuyerGradeBenefit.MileageImage = mileage.PromotionMileageImageUrl;
								BuyerGradeBenefit.MileageAlt = mileage.PromotionMileageImageAlt;
							}
						}
                        else if (base.gmktUserProfile.BuyerGrade == GMKT.Framework.BuyerGradeEnum.FAMILY && GradeBenefit != null && GradeBenefit.Family != null && GradeBenefit.Family.Count > 0)
                        {
                            ExtendGradeCouponT mileage = GradeCoupon.Where(T => T.Grade == "FAMILY").First();
                            if (mileage != null)
                            {
                                BuyerGradeBenefit.MileageImage = mileage.PromotionMileageImageUrl;
                                BuyerGradeBenefit.MileageAlt = mileage.PromotionMileageImageAlt;
                            }
                        }
						/*else if( base.gmktUserProfile.BuyerGrade == GMKT.Framework.BuyerGradeEnum.Gold && GradeBenefit != null && GradeBenefit.Gold != null && GradeBenefit.Gold.Count > 0 )
						{
							ExtendGradeCouponT mileage = GradeCoupon.Where( T => T.Grade == "Gold" ).First();
							if( mileage != null )
							{
								BuyerGradeBenefit.MileageImage = mileage.PromotionMileageImageUrl;
								BuyerGradeBenefit.MileageAlt = mileage.PromotionMileageImageAlt;
							}
						}
						else if( base.gmktUserProfile.BuyerGrade == GMKT.Framework.BuyerGradeEnum.Silver && GradeBenefit != null && GradeBenefit.Silver != null && GradeBenefit.Silver.Count > 0 )
						{
							ExtendGradeCouponT mileage = GradeCoupon.Where( T => T.Grade == "Silver" ).First();
							if( mileage != null )
							{
								BuyerGradeBenefit.MileageImage = mileage.PromotionMileageImageUrl;
								BuyerGradeBenefit.MileageAlt = mileage.PromotionMileageImageAlt;
							}
						}
						else if( base.gmktUserProfile.BuyerGrade == GMKT.Framework.BuyerGradeEnum.New && GradeBenefit != null && GradeBenefit.NewGrade != null && GradeBenefit.NewGrade.Count > 0 )
						{
							ExtendGradeCouponT mileage = GradeCoupon.Where( T => T.Grade == "New" ).First();
							if( mileage != null )
							{
								BuyerGradeBenefit.MileageImage = mileage.PromotionMileageImageUrl;
								BuyerGradeBenefit.MileageAlt = mileage.PromotionMileageImageAlt;
							}
						}*/
						
            CurrentGradeModel = new List<GradeCouponInfoT>();
            BuyerGradeBenefit.PurchaseCount = -1;

            for (int i = 0; i < GradeCoupon.Count; i++)
            {
                if (GradeCoupon[i].Grade == myGrade)
                {		
                        //admin에서 받아온 값
                        if (GradeCoupon[i].List != null)
                        {
                            for (int j = 0; j < GradeCoupon[i].List.Count; j++)
                            {
                                int eid = 0;
                                if (!string.IsNullOrEmpty(GradeCoupon[i].List[j].Eid))
                                {
                                    eid = int.TryParse(GradeCoupon[i].List[j].Eid, out eid) ? eid : 0;
                                    GradeCoupon[i].List[j].EidEncryptedString = EncryptForEventPlatform(eid);
                                }
                                MyBenefitCouponT winner = new MyBenefitBiz().SelectCoupon(eid.ToString(), base.gmktUserProfile.CustNo);
                                GradeCoupon[i].List[j].DownloadedCoupon = winner.CouponCnt;
                            }

                            //마일리지 다운받기(임시)
                            if (GradeCoupon[i].PromotionEid != 0)
                            {
                                BuyerGradeBenefit.PromotionTargetYN = new MyBenefitBiz().SelectUpgradeCustomLimitInfo(base.gmktUserProfile.CustNo, GetGradeCode(GradeCoupon[i].Grade));

                                if (BuyerGradeBenefit.PromotionTargetYN == "Y")
                                {
                                    BuyerGradeBenefit.PromotionEidEncryptedString = EncryptForEventPlatform(GradeCoupon[i].PromotionEid);
                                    MyBenefitCouponT winner = new MyBenefitBiz().SelectCoupon(GradeCoupon[i].PromotionEid.ToString(), base.gmktUserProfile.CustNo);
                                    BuyerGradeBenefit.PromotionDownloadedCoupon = winner.CouponCnt;
                                }
                            }
                        }

                    BuyerGradeBenefit.PromotionPoint = GradeCoupon[i].PromotionMileage;
                    BuyerGradeBenefit.NextGrade = GradeCoupon[i].NextGrade;
                    BuyerGradeBenefit.GradeCryptoNos = GMKTCryptoLibraryOption.EncodeTo64(GMKTCryptoLibraryOption.DesEncrypt(GradeCoupon[i].Seq + "|" + GradeCoupon[i].Eid + "|" + GradeCoupon[i].Grade, CRYPT_MD5_FOOTER));
                    BuyerGradeBenefit.GradeCode = GradeCoupon[i].GradeCode;
                    CurrentGradeModel = GradeCoupon[i].List;
                    BuyerGradeBenefit.ClassName = GradeCoupon[i].ClassName.Replace("ico_", "");

                    if (myGrade == "SVIP")
                    {
                        if (iThis12MonthPoint >= 80)
                            BuyerGradeBenefit.PurchaseCount = 0;
                        else
                            BuyerGradeBenefit.PurchaseCount = Convert.ToInt32(80 - iThis12MonthPoint);

                        /*if (totalPurchaseCount >= 50)
                        {
                            if (this3MonthCount >= 30)
                            {
                                BuyerGradeBenefit.PurchaseCount = 0;
                            }
                            else
                            {
                                BuyerGradeBenefit.PurchaseCount = 30 - this3MonthCount;
                            }
                        }
                        else
                        {
                            BuyerGradeBenefit.PurchaseCount = 50 - totalPurchaseCount;
                        }*/
                        
                    }
                    else if (myGrade == "VIP")
                    {
                        if (iThis12MonthPoint >= 80)
                            BuyerGradeBenefit.PurchaseCount = 0;
                        else
                            BuyerGradeBenefit.PurchaseCount = Convert.ToInt32(80 - iThis12MonthPoint);
                        /*if (totalPurchaseCount >= 50)
                        {
                            if (this3MonthCount >= 30)
                            {
                                BuyerGradeBenefit.PurchaseCount = 0;
                            }
                            else
                            {
                                BuyerGradeBenefit.PurchaseCount = 30 - this3MonthCount;
                            }
                        }
                        else
                        {
                            BuyerGradeBenefit.PurchaseCount = 50 - totalPurchaseCount;
                        }*/
                    }
                    else if (myGrade == "FAMILY")
                    {
                        if (iThis12MonthPoint >= 5)
                            BuyerGradeBenefit.PurchaseCount = 0;
                        else
                            BuyerGradeBenefit.PurchaseCount = Convert.ToInt32(5 - iThis12MonthPoint);
                    }
                    /*else if (myGrade == "Gold")
                    {
                        if (totalPurchaseCount >= 50)
                        {
                            if (this3MonthCount >= 5)
                            {
                                BuyerGradeBenefit.PurchaseCount = 0;
                            }
                            else
                            {
                                BuyerGradeBenefit.PurchaseCount = 5 - this3MonthCount;
                            }
                        }
                        else
                        {
                            BuyerGradeBenefit.PurchaseCount = 50 - totalPurchaseCount;
                        }
                    }
                    else if (myGrade == "Silver")
                    {
                        if (this3MonthCount >= 2)
                        {
                            BuyerGradeBenefit.PurchaseCount = 0;
                        }
                        else
                        {
                            BuyerGradeBenefit.PurchaseCount = 2 - this3MonthCount;
                        }
                    }
                    else if (myGrade == "New")
                    {
                        if (this3MonthCount >= 1)
                        {
                            BuyerGradeBenefit.PurchaseCount = 0;
                        }
                        else
                        {
                            BuyerGradeBenefit.PurchaseCount = 1 - this3MonthCount;
                        }
                    }*/
                }
            }
        }

        private string convertMyGrade(BuyerGradeEnum buyerGrade)
        {
            switch (Convert.ToInt32(buyerGrade))
            {
                case 10:
                case 20:
                    return "SVIP";
                case 30:
                case 40:
                    return "VIP";
                case 50:
                    return "FAMILY";
                default:
                    return "FAMILY";
            }
        }

        [HttpGet]
        public ActionResult ApplyCouponPack(string cType, string cNos, string cLevel)
        {
            int seq = 0, eid = 0;
            string levelCode = "";
            bool success = false;
            string errorMsg = "";
            int errorCode = 0;
            object data = null;

            string custNo = base.gmktUserProfile.CustNo;
            string custId = base.gmktUserProfile.LoginID;

            string encNos = GMKTCryptoLibraryOption.DecodeFrom64(cNos);
            encNos = GMKTCryptoLibraryOption.DesDecrypt(encNos, CRYPT_MD5_FOOTER);

            if (encNos != "")
            {
                string[] encNoArray = encNos.Split('|');
                if (encNoArray.Length == 3)
                {
                    seq = int.TryParse(encNoArray[0], out seq) ? seq : 0;
                    eid = int.TryParse(encNoArray[1], out eid) ? eid : 0;
                    levelCode = encNoArray[2];
                }
                else
                {
                    success = true;
                    errorCode = -1006; //seq, eid 인코딩 값이 바르지 않음
                    errorMsg = "잘못된 접근입니다.";
                }

                if (!PageAttr.IsLogin)
                {
                    success = true;
                    errorCode = -1000;
                    errorMsg = "로그인이 필요합니다."; //로그인이 안됨
                }
                else
                {
                    if (cType == "gc")
                    {
                        if (levelCode != cLevel)
                        {
                            success = true;
                            errorCode = -1001; //등급이 잘못 들어옴
                            errorMsg = levelCode + " 고객만 응모가능합니다.";
                        }
                    }
                    else if(cType != "mc" && cType != "gc" )
                    {
                        success = true;
                        errorCode = -1002; //cType 코드 잘못 들어옴
                        errorMsg = "잘못된 접근입니다.";
                    }

					//쿠폰팩 회원타입 유효성 체크
					CouponPackCustTypeCheckResultT checkResult = new CouponzoneBiz().CheckCouponPackCustType(eid, custNo);
					if(errorCode == 0 && checkResult != null)
					{
						success = true;
						errorCode = checkResult.ResultCode;

						switch(checkResult.ResultCode)
						{															
							case -1:
								errorMsg = "잘못된 접근입니다.";
								break;
							case -2:
								errorMsg = "발급 가능한 회원이 아닙니다.";
								break;
							case -3:
								errorMsg = "잘못된 접근입니다.";
								break;
						}						
					}
		
                    if (errorCode == 0)
                    {
                        CouponPackDownloadResultT rtnCouponPack = new CouponzoneBiz().ApplyCouponPack(seq, eid, custNo, custId, "M", "K");
                        if (rtnCouponPack.ResultCode == 0)
                        {
                            success = true;
                            errorCode = 0;
                        }
                        else if (rtnCouponPack.ResultCode == -1)
                        {
                            success = true;
                            errorCode = -1003; //쿠폰 그룹번호(eid)가 존재하지 않음
                            errorMsg = "잘못된 접근입니다.";
                        }
                        else if (rtnCouponPack.ResultCode == -2)
                        {
                            success = true;
                            errorCode = -1004; //이미 쿠폰 받았음
                            errorMsg = "이미 발급 받으셨습니다.";
                        }
                        else if (rtnCouponPack.ResultCode == -3)
                        {
                            success = true;
                            errorCode = -1005; //eid나 custno가 잘못 들어감
                            errorMsg = "잘못된 접근입니다.";
                        }
                    }


                }
            }
            else
            {
                success = true;
                errorCode = -1007; //인코딩 값이 없음
                errorMsg = "잘못된 접근입니다.";
            }

            return Json(new {
                success = success,
                errorCode = errorCode,
                errorMsg = errorMsg,
                data = data
            }, JsonRequestBehavior.AllowGet);
            
        }

        public int GetGradeCode(string grade)
        {
            switch (grade)
            {
                case "SVIP":
                    return 10;
                case "VIP":
                    return 20;
                case "Gold":
                    return 30;
                case "Silver":
                    return 40;
                case "New":
                    return 50;
                default:
                    return 0;
            }
        }

		protected MemberGradeCrossModel GetMemberGradeCross()
		{
			string custNo = base.gmktUserProfile.CustNo;
			MemberGradeCross.GradeAgreeYn = false;
			MemberGradeCross.ReasonCode = String.Empty;
			try
			{
				MemberGradeCross.GradeAgreeYn = new MemberApiBiz().GetEbayGradeAgreeYN(custNo);

				bool isSVip = gmktUserProfile.BuyerGrade == BuyerGradeEnum.SVIP;

				if (!String.IsNullOrEmpty(custNo) && isSVip)
				{
					MemberGradeCross.ReasonCode = new MemberApiBiz().GetBenefitSharingReasonCode(custNo);
				}
			}
			catch (Exception e)
			{

			}

			return MemberGradeCross;
		}

        public static string GetGradeCouponCnt(string grade)
        {
            string sRet = "";
            switch (grade)
            {
                case "SVIP":
                    sRet = "4";
                    break;
                case "VIP":
                    sRet = "2";
                    break;
                case "FAMILY":
                    sRet = "1";
                    break;
            }
            return sRet;
        }

		[HttpGet]
		public JsonResult SetMemberGradeCross()
		{
			string custNo = base.gmktUserProfile.CustNo;
			CrossResultT apiResult = new MemberApiBiz().SetEbayGradeAgree(custNo);

			return Json(new { code = apiResult.ResultCode, msg = apiResult.ResultDescription }, JsonRequestBehavior.AllowGet);
		}
		
		[HttpGet]
		public ActionResult AgreeCross()
		{
            ViewBag.Title = "쿠폰존 - G마켓 모바일";

			List<NavigationIconT> model = new EventCommonBiz().GetNavigationIcons();
			return View("~/Views/EventV2/couponzone/AgreeCross.cshtml", model);
		}

        [HttpGet]
        public ActionResult AgreeCrossCorp()
        {
            ViewBag.Title = "쿠폰존 - G마켓 모바일";

            List<NavigationIconT> model = new EventCommonBiz().GetNavigationIcons();
            return View("~/Views/EventV2/couponzone/AgreeCrossCorp.cshtml", model);
        }

        [HttpPost]
        public JsonResult CouponValidCheck(string checkType)
        {
            CouponValidCheckResultT result = new CouponValidCheckResultT();
            result.ResultCode = CouponValidCheckResult.Fail;
            result.ResultMessage = "쿠폰 발급에 문제가 생겼습니다.";
            result.ResultType = string.Empty;

            if (!PageAttr.IsLogin)
            {
                result.ResultCode = CouponValidCheckResult.Fail;
                result.ResultMessage = "로그인이 필요합니다.";
                result.ResultType = "GOLOGIN";
                return Json(new { resultCode = result.ResultCode, resultMessage = result.ResultMessage, resultType = result.ResultType });
            }

            if(string.IsNullOrEmpty(checkType)){
                result.ResultCode = CouponValidCheckResult.Success;
                result.ResultMessage = string.Empty;
                return Json(new { resultCode = result.ResultCode, resultMessage = result.ResultMessage, resultType = result.ResultType });
            }

            CouponValidCheckResultT tempResult = new CouponzoneBiz().GetCouponValidation(checkType);
            if (tempResult != null)
            {
                result = tempResult;
            }

            return Json(new { resultCode = result.ResultCode, resultMessage = result.ResultMessage, resultType = result.ResultType });
        }
    }
}

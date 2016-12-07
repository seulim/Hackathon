using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Web.Models.EventV2
{
    public class TotalCouponzoneModel
    {
        public List<NavigationIconT> NaviIcons { get; set; }
        public BannerT TopBanner { get; set; }
        public CouponzoneModel Couponzone { get; set; }
        public SpecialCouponModel SpecialCoupon { get; set; }
        public BuyerGradeBenefitModel BuyerGradeBenefit { get; set; }
        public List<ExchangeCouponT> ExchangeCouponBanner { get; set; }
        public List<CardEventCouponT> CardEventCoupon { get; set; }
		public MemberGradeCrossModel MemberGradeCross { get; set; }
        public List<MonthlyCouponT> MonthlyCoupon { get; set; }
        public List<MonthlyCouponT> MonthlyCoupon1 { get; set; }
        public List<MonthlyCouponT> MonthlyCoupon2 { get; set; }
        public bool IsTestIdCheck { get; set; }

        public TotalCouponzoneModel()
        {
            NaviIcons = new List<NavigationIconT>();
            TopBanner = new BannerT("", "", "", "");
            Couponzone = new CouponzoneModel();
            SpecialCoupon = new SpecialCouponModel();
            BuyerGradeBenefit = new BuyerGradeBenefitModel();
            ExchangeCouponBanner = new List<ExchangeCouponT>();
            CardEventCoupon = new List<CardEventCouponT>();
			MemberGradeCross = new MemberGradeCrossModel();
            IsTestIdCheck = false;
        }
    }

    public class BuyerGradeBenefitModel
    {
        public string GradeCryptoNos { get; set; }
        public string GradeCode { get; set; }
        public string DisplayCustName { get; set; }
        public string MyGrade { get; set; }
				public string MileageImage { get; set; }
				public string MileageAlt { get; set; }
        public string NextGrade { get; set; }
        public int PromotionPoint { get; set; }
        public int PurchaseCount { get; set; }
        public string ClassName { get; set; }
        public string[] PromotionEidEncryptedString { get; set; }
        public int PromotionDownloadedCoupon { get; set; }
        public string PromotionTargetYN { get; set; }
        public List<GradeCouponInfoT> CurrentGradeModel { get; set; }
        public string CustType { get; set; }

        public BuyerGradeBenefitModel()
        {
            DisplayCustName = "";
            PurchaseCount = 0;
            CurrentGradeModel = new List<GradeCouponInfoT>();
        }
    }

    public class SpecialCouponModel
    {
		public int SelectedIndex { get; set; }
        public List<SpecialCouponTabT> CouponTabList { get; set; }

        public SpecialCouponModel()
        {
            CouponTabList = new List<SpecialCouponTabT>();
        }
    }
 
	public class CouponzoneGuidePopupT
	{
		public string GradeCouponGuideImageUrl { get; set; }
		public string MemberCouponGuideImageUrl { get; set; }
		public string ExchangeCouponGuideImageUrl { get; set; }
		public string SpecialCouponGuideImageUrl { get; set; }
	}

    public class CouponzoneModel
    {
        public List<BannerInfoT> TopSeasonBanner { get; set; }
        public List<PromotionBannerInfoT> ProCouponBanner { get; set; }
		public List<BannerInfoT> BottomCouponBanner { get; set; }
        public List<ExtendGradeCouponT> GradeCoupon { get; set; }
		public GradeBenefitsT GradeBenefit { get; set; }
		public MemberCouponsT MemberCoupon { get; set; }
		public ExchangeCouponsT ExchangeCoupon { get; set; }
		public CouponzoneGuidePopupT MobileGuidePopup { get; set; }
        public List<NoticeT> NoticeListGrade { get; set; }
        public List<NoticeT> NoticeListMember { get; set; }
        public List<NoticeT> NoticeListExchange { get; set; }
        public List<NoticeT> NoticeListSpecial { get; set; }

        public CouponzoneModel()
        {
            TopSeasonBanner = new List<BannerInfoT>();
            ProCouponBanner = new List<PromotionBannerInfoT>();
		    BottomCouponBanner = new List<BannerInfoT>();
            GradeCoupon = new List<ExtendGradeCouponT>();
		    GradeBenefit = new GradeBenefitsT();
		    MemberCoupon = new MemberCouponsT();
		    ExchangeCoupon = new ExchangeCouponsT();
			MobileGuidePopup = new CouponzoneGuidePopupT();
            NoticeListGrade = new List<NoticeT>();
            NoticeListMember = new List<NoticeT>();
            NoticeListExchange = new List<NoticeT>();
            NoticeListSpecial = new List<NoticeT>();
        }        
    }

    public class CardEventCouponT
    {
        public string Type { get; set; }
        public int Eid { get; set; }
        public string[] EidEncryptedString { get; set; }
        public int DownloadedCoupon { get; set; }
    }

	public class MonthlyCouponT
	{
		public int Eid { get; set; }
		public string[] EidEncryptedString { get; set; }
		public int DownloadedCoupon { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Text { get; set; }
		public string CssClass { get; set; }
		public string ImageUrl { get; set; }
		public bool HasTag { get; set; }
		public string CouponType { get; set; }
        public string CheckType { get; set; }
	}

	public class MemberGradeCrossModel
	{
		public bool GradeAgreeYn { get; set; }
		public string ReasonCode { get; set; }
	}
}
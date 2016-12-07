using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.EventV2
{
    public enum CouponValidCheckResult
    {
        Success = 1,
        Fail = 0
    }

    public class TotalCouponzoneDataT
    {
        public CouponzoneDataT Couponzone { get; set; }
        public SpecialCouponDataT SpecialCoupon { get; set; }
    }

    public class CouponzoneDataT
    {
        public List<BannerInfoT> TopSeasonBanner { get; set; }
        public List<PromotionBannerInfoT> ProCouponBanner { get; set; }
        public List<BannerInfoT> BottomCouponBanner { get; set; }
        public GradeCouponsT GradeCoupon { get; set; }
        public GradeBenefitsT GradeBenefit { get; set; }
        public MemberCouponsT MemberCoupon { get; set; }
        public ExchangeCouponsT ExchangeCoupon { get; set; }
		public GuidePopupT MobileGuidePopup { get; set; }
        public List<NoticeT> NoticeListGrade { get; set; }
        public List<NoticeT> NoticeListMember { get; set; }
        public List<NoticeT> NoticeListExchange { get; set; }
        public List<NoticeT> NoticeListSpecial { get; set; }
    }

	public class GuidePopupT
	{
		public List<GuideImageT> GradeCoupon { get; set; }
		public List<GuideImageT> MemberCoupon { get; set; }
		public List<GuideImageT> ExchangeCoupon { get; set; }
		public List<GuideImageT> SpecialCoupon { get; set; }
	}

	public class GuideImageT
	{
		public long Seq { get; set; }
		public int Priority { get; set; }
		public string DisplayStartDate { get; set; }
		public string DisplayEndDate { get; set; }
		public string ImageMobile { get; set; }
	}

    public class PromotionBannerInfoT
    {
        public int Seq { get; set; }
        public int Priority { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string ImageMobile { get; set; }
        public string Alt { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
        public string PosType { get; set; }
				public string MobileUrl { get; set; }
    }

    public class GradeCouponsT
    {
        public GradeCouponT Svip { get; set; }
        public GradeCouponT Vip { get; set; }
        public GradeCouponT Gold { get; set; }
        public GradeCouponT Silver { get; set; }
        public GradeCouponT NewGrade { get; set; }
        public GradeCouponT Family { get; set; }
    }

    public class GradeCouponT
    {
        public int Seq { get; set; }
        public int Eid { get; set; }
        public List<GradeCouponInfoT> List { get; set; }
    }

    public class GradeCouponInfoT
    {
        public int EventzoneDispEventSeq { get; set; }
        public int EventzoneDispSeq { get; set; }
        public int Priority { get; set; }
        public string ExposeTargetType { get; set; }
        public string EtcSetupContent { get; set; }
        public string Geid { get; set; }
        public string Eid { get; set; }
        public string EidScript { get; set; }
        public string GiveawayAmt { get; set; }
        public string MlangEventNm { get; set; }
        public string EventUrl { get; set; }
        public string EventImage { get; set; }
        public string EventImageMobile { get; set; }
        public string EventImageAlt { get; set; }
        public string DispStartDt { get; set; }
        public string DispEndDt { get; set; }
        public string[] EidEncryptedString { get; set; }
        public int DownloadedCoupon { get; set; }

    }

    public class CouponT
    {
        public int EventzoneDispEventSeq { get; set; }
        public int EventzoneDispSeq { get; set; }
        public int Priority { get; set; }
        public string ExposeTargetType { get; set; }
        public string EtcSetupContent { get; set; }
        public string Geid { get; set; }
        public string Eid { get; set; }
        public string GiveawayAmt { get; set; }
        public string MlangEventNm { get; set; }
        public string EventUrl { get; set; }
        public string EventImage { get; set; }
        public string EventImageMobile { get; set; }
        public string EventImageAlt { get; set; }
        public string DispStartDt { get; set; }
        public string DispEndDt { get; set; }
    }

    public class GradeBenefitsT
    {
        public List<BenefitCouponT> Svip { get; set; }
        public List<BenefitCouponT> Vip { get; set; }
        public List<BenefitCouponT> Gold { get; set; }
        public List<BenefitCouponT> Silver { get; set; }
        public List<BenefitCouponT> NewGrade { get; set; }
        public List<BenefitCouponT> Family { get; set; }
    }

    public class BenefitCouponT : CouponT
    {
        public string MlangBannerBm { get; set; }
        public string BannerUrl { get; set; }
        public string BannerImg { get; set; }
        public string BannerImgMobile { get; set; }
        public string BannerImgAlt { get; set; }
        public string GuideImg { get; set; }
        public string GuideImgAlt { get; set; }
    }

    public class MemberCouponsT
    {
        public int Seq { get; set; }
        public int Eid { get; set; }
        public string MemberCryptoNos { get; set; }
		public int DownloadedCoupon { get; set; }
		public int Count { get; set; }
        public List<CouponT> Fashion { get; set; }
        public List<CouponT> Baby { get; set; }
        public List<CouponT> Digital { get; set; } //json에서 현재 disital 로 오타나있음
        public List<CouponT> Books { get; set; }
    }

    public class ExchangeCouponsT
    {
        public List<ExchangeCouponT> Gstamp { get; set; }
        public List<ExchangeCouponT> Gmileage { get; set; }
    }

    /*
    public class ExchangeCouponT : BannerInfoT
    {
        public string Type { get; set; }
        public List<EIDInfoT> EIDInfoList { get; set; }
    }*/

    /*
    public class EIDInfoT
    {
        public string EtcSetupContent { get; set; }
        public string Eid { get; set; }
        public string AppWay { get; set; }
        public string AppMinusCnt { get; set; }
    }*/

    public class SpecialCouponDataT
    {
        public List<SpecialCouponTabT> CouponTabList { get; set; }
    }

    public class SpecialCouponTabT
    {
        public long? DisplaySeq { get; set; }
        public short? Priority { get; set; }
        public string ExposeTargetType { get; set; }
        public string BannerPosType { get; set; }
        public int? Geid { get; set; }
        public int? Eid { get; set; }
        public string MlangBannerName { get; set; }
        public string BannerUrl { get; set; }
        public string BannerImageUrl { get; set; }
        public string MobileBannerImageUrl { get; set; }
        public string MlangBannerImageText { get; set; }
        public string EventGuideImageUrl { get; set; }
        public string MlangGuideImageText { get; set; }
        public string EtcDesc { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
        public string TabName { get; set; }
        public string TabOnImageUrl { get; set; }
        public string TabOffImageUrl { get; set; }
        public string MobileTabOnImageUrl { get; set; }
        public string MobileTabOffImageUrl { get; set; }
        public string GMomGrade { get; set; }

        public List<SpecialCouponInfoT> CouponList { get; set; }
    }

    public class SpecialCouponInfoT
    {
        public long? EventzoneDisplaySeq { get; set; }
        public long? EventzoneDisplayEventSeq { get; set; }
        public short? Priority { get; set; }
        public string ExposeTargetType { get; set; }
        public int? Geid { get; set; }
        public int? Eid { get; set; }
        public string MlangEventName { get; set; }
        public string EtcSetupContent { get; set; }
        public string EventUrl { get; set; }
        public string GiveawayAmt { get; set; }
        public string EventImageUrl { get; set; }
        public string MobileEventImageUrl { get; set; }
        public string MlangEventImageText { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
        public string BannerUseYN { get; set; }
        public string BannerImageUrl { get; set; }
        public string BannerImageText { get; set; }
        public string ButtonName { get; set; }
        public string ConnUrl { get; set; }
        public string CouponButtonYN { get; set; }
        public string[] EidEncryptedString { get; set; }
        public int DownloadedCoupon { get; set; }
    }

    public class ExtendGradeCouponT
    {
        public int Seq { get; set; }
        public int Eid { get; set; }
        public string GradeCode { get; set; }
        public string Grade { get; set; }
        public string NextGrade { get; set; }
        public int PromotionMileage { get; set; }
				public string PromotionMileageImageUrl { get; set; }
				public string PromotionMileageImageAlt { get; set; }
        public string ClassName { get; set; }
        public int PromotionEid { get; set; }
        public List<GradeCouponInfoT> List { get; set; }
    }

    public class EventzoneDisplayInfoT
    {
        public long EventzoneDisplaySeq { get; set; }
        public int Priority { get; set; }
        public string EventManageType { get; set; }
        public string ExposeTargetType { get; set; }
        public string BannerPosType { get; set; }
        public int Geid { get; set; }
        public int Eid { get; set; }
        public string EtcDescribe { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
    }

    public class CouponPackDownloadResultT
    {
        public int ResultCode { get; set; }
    }

	public class CouponPackCustTypeCheckResultT
	{
		public int ResultCode { get; set; }		
	}

    public class CouponValidCheckResultT
    {
        public CouponValidCheckResult ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string ResultType { get; set; }
    }
}

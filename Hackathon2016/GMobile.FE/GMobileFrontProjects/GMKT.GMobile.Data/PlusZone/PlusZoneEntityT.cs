using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
    public class AttendanceCheckTotal
    {
        public List<CouponBenefitItem> CouponBenefitItem { get; set; }
        public List<MobileShopPlan> MobileShopPlan { get; set; }
        public List<PopularPlanQuickLink> PopularPlanQuickLink { get; set; }
    }

    public class CouponBenefitItem
    {
        public string BenefitName { get; set; }
        public string ImagePath { get; set; }
        public string LinkUrl { get; set; }
    }

    public class MobileShopPlan
    {
        public Nullable<Int32> Sid { get; set; }
        public string Title { get; set; }
        public string GdlcCd { get; set; }
        public string BannerUrl { get; set; }
        public string MobileThumbImageUrl { get; set; }
        public string MobileBannerImageUrl { get; set; }
        public string MobilePromoteText { get; set; }
        public string LinkUrl { get; set; }
    }

    public class PopularPlanQuickLink
    {
        public string PopularPlanName { get; set; }
        public string ImagePath { get; set; }
        public string LinkUrl { get; set; }
    }
}

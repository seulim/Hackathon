using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.DisplayDB;
using GMobile.Data.Diver;

namespace GMKT.GMobile.Web.Models
{
    public class SpecialShopModel : ILandingBannerModel
	{
		public MobileShopKindT ShopInfo { get; set; }
		public List<MobileShopBannerT> ShopBanners { get; set; }
		public List<MobileShopBestT> ShopBestItems { get; set; }
		public List<SpecialShopT> ShopBestAutoItems { get; set; }
		public List<MobileShopPlanT> ShopPopularPlans { get; set; }
		public List<MobileShopPlanT> ShopPopularAutoPlans { get; set; }
		public int BestResultSize { get; set; }
		public int PlanResultSize { get; set; }

        public GMKT.GMobile.Data.ILandingBannerEntityT LandingBanner { get; set; }
        public ICampaign Campaign { get; set; }
    }
}
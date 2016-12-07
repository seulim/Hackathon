using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
    public class SuperDealModel : ILandingBannerModel
	{
		public List<SuperDealCategoryV2> Category { get; set; }
		public List<SuperDealCategory> ThemeCategory { get; set; }
		public List<SuperDealItem> Items { get; set; }
		public List<HomeMainItem> ThemeItems { get; set; }
		public string SevenHotDealTitle { get; set; }
		public List<SuperDealItem> SevenHotDealItems { get; set; }

        public ILandingBannerEntityT LandingBanner { get; set; }
        public ICampaign Campaign { get; set; }
    }
}
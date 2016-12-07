using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.Diver;
using GMobile.Data.DisplayDB;

namespace GMKT.GMobile.Web.Models
{
    public class DisplayModel : ILandingBannerModel
	{
		public string pageKind { get; set; }
		public string groupCode { get; set; }
		public string ecp_gdlc { get; set; }
		public string ecp_gdmc { get; set; }
		public string ecp_gdsc { get; set; }
		public string listView { get; set; }
		public string lcId { get; set; }
		public string sid { get; set; }
		public int pageNo { get; set; }
		public string groupCd { get; set; }
		public int totalCount { get; set; }
		public Hashtable Categorys { get; set; }
		public MobileShopPlanT Plan { get; set; }
		public List<SearchGoods> Items { get; set; }
		public List<MobilePlanT> planList { get; set; }
		public string ListName { get; set; }

        public Dictionary<string, string> dicData { get; set; }

        public GMKT.GMobile.Data.ILandingBannerEntityT LandingBanner { get; set; }
        public ICampaign Campaign { get; set; }

				public List<ShopMobileEventGroupInfo> groupInfos { get; set; }
				public List<ShopMobileEventGroupGoods> GoodsItems { get; set; }

    }
}
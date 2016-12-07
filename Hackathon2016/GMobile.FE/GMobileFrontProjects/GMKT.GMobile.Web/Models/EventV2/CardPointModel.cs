using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Web.Models.EventV2
{
	public class CardPointModel
	{
        public List<NavigationIconT> NaviIcons { get; set; }
		public BannerT TopBanner { get; set; }

		public List<BannerT> ThisMonthCardBenefit { get; set; }
		public List<Banner2T> PointAdd { get; set; }
		public List<Banner2T> PointBenefit { get; set; }

		public string ThisMonthHalbu { get; set; }

		public string MobileGuidePopupImageUrl { get; set; }

		public bool IsReregOCB { get; set; }
		public bool IsReregAsiana { get; set; }
	}
}
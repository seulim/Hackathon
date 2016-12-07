using System;
using System.Collections.Generic;
using System.Linq;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
    public class MartViewModel : MartView, ILandingBannerModel
    {
		public ILandingBannerEntityT LandingBanner { get; set; }
		public ICampaign Campaign { get; set; }
    }

	public class MartV2ViewModel : MartV2View, ILandingBannerModel
	{
		public ILandingBannerEntityT LandingBanner { get; set; }
		public ICampaign Campaign { get; set; }
	}

	public class MartV3ViewModel : MartV3View, ILandingBannerModel
	{
		public ILandingBannerEntityT LandingBanner { get; set; }
		public ICampaign Campaign { get; set; }
	}

	public class MartContentsModel : MartContentsView
	{
		public long contentsSeq;
	}
}

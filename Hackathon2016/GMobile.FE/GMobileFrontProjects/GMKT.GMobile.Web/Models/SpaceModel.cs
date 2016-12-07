using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
	public class SpaceModel : SpaceInfo, ILandingBannerModel
	{
		public ILandingBannerEntityT LandingBanner { get; set; }
		public ICampaign Campaign { get; set; }
	}

	public class SpaceContentsDetailModel : SpaceContents
	{
		public string Html { get; set; }
		public List<SpaceContentsItem> Items { get; set; }
	}

	public class SpaceBrandDetailModel : SpaceBrandGroup
	{
		public string ConnectUrl { get; set; }
		public string Html { get; set; }
		public List<SpaceBrandGroupItem> Items { get; set; }
	}
}
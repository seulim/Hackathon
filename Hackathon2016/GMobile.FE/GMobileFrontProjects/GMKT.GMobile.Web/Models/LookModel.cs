using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
	public class LookModel : LookInfo, ILandingBannerModel
	{
		public ILandingBannerEntityT LandingBanner { get; set; }
		public ICampaign Campaign { get; set; }
	}
}
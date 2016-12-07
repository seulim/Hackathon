using System;
using System.Collections.Generic;
using System.Linq;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
	public class DepartmentStoreMainModel:DepartmentStoreMainGroupT,ILandingBannerModel
	{
		public ILandingBannerEntityT LandingBanner { get; set; }
		public ICampaign Campaign { get; set; }
	}

	public class DepartmentStoreNowModel : DepartmentStoreNowT, ILandingBannerModel
	{
		public ILandingBannerEntityT LandingBanner { get; set; }
		public ICampaign Campaign { get; set; }
	}
}
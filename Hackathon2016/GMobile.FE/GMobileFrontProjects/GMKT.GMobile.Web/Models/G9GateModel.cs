using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMKT.GMobile.Web.Models
{
	public class G9GateModel
	{
		public bool IsLogin { get; set; }
		public string CustName { get; set; }
		public string RedirectUrl { get; set; }
		public string BannerImageUrl { get; set; }
		public string BannerLandingUrl { get; set; }
		public string BannerAlt { get; set; }
	}
}
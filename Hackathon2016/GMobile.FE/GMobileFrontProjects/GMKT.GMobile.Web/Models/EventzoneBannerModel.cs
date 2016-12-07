using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMKT.GMobile.Web.Models
{
	public class EventzoneBannerModel
	{
		public string Title { get; set; }

		public string BannerUrl { get; set; }

		public string[] EidEncryptedString { get; set; }
	}
}
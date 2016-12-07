using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Web.Models.EventV2
{
	public class BannerT
	{
		public BannerT( string imageUrl, string imageAlt, string imageLinkUrl = "", string eid = "" )
		{
			ImageUrl = imageUrl;
			ImageAlt = imageAlt;
			ImageLinkUrl = imageLinkUrl;
			Eid = eid;
		}
		public string ImageUrl { get; set; }
		public string ImageAlt { get; set; }
		public string ImageLinkUrl { get; set; }
		public string Eid { get; set; }
	}

	public class Banner2T
	{
		public Banner2T( string imageUrl, string imageAlt, string guideImageUrl, string guideImageAlt, string imageLinkUrl = "", string eid = "" )
		{
			ImageUrl = imageUrl;
			ImageAlt = imageAlt;
			ImageLinkUrl = imageLinkUrl;
			GuideImageUrl = guideImageUrl;
			GuideImageAlt = guideImageAlt;
			Eid = eid;
		}
		public string ImageUrl { get; set; }
		public string ImageAlt { get; set; }
		public string GuideImageUrl { get; set; }
		public string GuideImageAlt { get; set; }
		public string ImageLinkUrl { get; set; }
		public string Eid { get; set; }
	}
}

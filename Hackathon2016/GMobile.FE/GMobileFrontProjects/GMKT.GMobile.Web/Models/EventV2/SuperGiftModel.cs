using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Web.Models.EventV2
{
	public class SuperGiftModel
	{
		public List<NavigationIconT> NaviIcons { get; set; }

		public bool? IsVip { get; set; }
		public string HistoryTitle { get; set; }
		public BuyHistoryT BuyHistory { get; set; }
		public BannerT TopBanner { get; set; }
		public BannerT MainTemplete { get; set; }

		public List<BannerT> MainTempleteOptions { get; set; }

		public bool IsAlreadyApplied { get; set; }
		
		public string EntryTarget { get; set; }
		public string EntryCondition { get; set; }
		public DateTime? EntryStartDate { get; set; }
		public DateTime? EntryEndDate { get; set; }
		public string WinGuide { get; set; }

		public List<BannerT> GuideBanners { get; set; }
		public string SnsHtml { get; set; }
		public List<String> NoticeHtml { get; set; }

		public string MobileGuidePopupImageUrl { get; set; }

		public string ShareInfoImage1 { get; set; }
		public string ShareInfoImage2 { get; set; }
		public string ShareInfoImage3 { get; set; }
		public string ShareBannerImage { get; set; }
		public string ShareUrl { get; set; }
		public string ShareInfoText { get; set; }

		public bool IsShareApplied { get; set; }

		public string ECif { get; set; }
		public string EPif { get; set; }
	}
}
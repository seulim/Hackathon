using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Data.EventV2;

namespace GMKT.GMobile.Web.Models.EventV2
{
	public class GreenCarpetModel
	{
        public List<NavigationIconT> NaviIcons { get; set; }
		public List<CommonBannerT> TopBanner { get; set; }
		public List<MainPosterT> MainPoster { get; set; }
		//public List<HistoryT> history { get; set; }
		public string[] Notice { get; set; }
		public int TotalWinAmt { get; set; } //총 체험고객
	}
}

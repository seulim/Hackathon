using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.Item;
using GMobile.Data.Tiger;

namespace GMKT.GMobile.Web.Models
{
	public class TodayModel
	{
		public List<TodaySpecialPriceT> TodaySpecial { get; set; }
		public List<MainPageBannerT> GoodSeries { get; set; }

		// 2013-05-09 이윤호
		// 링크 만을 이용해 굿시리즈 탭으로 이동할 수 있도록 수정
		public int CurrentTabNo { get; set; }
	}
}
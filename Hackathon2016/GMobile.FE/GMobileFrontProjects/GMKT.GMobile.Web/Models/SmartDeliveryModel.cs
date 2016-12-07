using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data.SmartDelivery;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
	public class SmartDeliveryFrontModel
	{
		//category
		public List<SmartDeliverCatetoryModel> Category {get; set;}

		//banner - main, 긴급배송공지, qna
		public List<SmartDeliveryBannerT> MainBanner { get; set; }
		public SmartDeliveryBannerT DeliveryNoticeBanner { get; set; }
		public SmartDeliveryBannerT QnABanner { get; set; }

		//goods - best 10, 금주추천, 카테고리hotitem, 금주의브랜드
		public List<SearchItemModel> BestDisplay { get; set; }
		public List<SearchItemModel> RecommDisplay { get; set; }
		public List<SmartDeliveryDisplayModel> HotItemDisplay { get; set; }
		public List<string> HotItemTitle { get; set; }
		public List<SmartDeliveryDisplayModel> BrandDisplay { get; set; }
		public List<string> BrandTitle { get; set; }

	}
}
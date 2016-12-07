using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
    public class BestModel : ILandingBannerModel
	{
		public string BestType = "";
		public string ListName = "";
		public string ListViewType = "H";
		public string Code = "";
		// 성인상품이면 19금 이미지 보여줌
		public bool ShowAdultImage = true;

		// 만약 성인 상품인데 로그인 안했으면 무조건 로그인 url로 보내줘야 함
		// 성인상품인데 미성년자인 경우 VIP 진입 못하게 하고 에러 메시지 보여준다.
		public bool ShowAdultOnlyMessage = false;

		public List<Best100GroupCateogyDetail> GroupCategory;
		public List<Best100CateogyDetail> Category;
		public List<SearchItemModel> BestItems;
		public List<CategoryInfo> SearchResultCategory;

        public ILandingBannerEntityT LandingBanner { get; set; }
        public ICampaign Campaign { get; set; }
    }
}
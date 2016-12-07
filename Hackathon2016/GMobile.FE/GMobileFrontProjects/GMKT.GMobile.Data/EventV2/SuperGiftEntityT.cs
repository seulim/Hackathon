using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.EventV2
{
	public class BuyHistoryT
	{
		public int Count { get; set; }
		public decimal TotalPayment { get; set; }
	}

	public class SuperGiftJsonEntityT
	{
		public SuperGiftT supergift { get; set; }
	}

	public class SuperGiftT
	{
		public List<BannerInfo> top_banner { get; set; }
		public List<MonthlyEventT> monthly_event { get; set; }
		public List<SocialT> social { get; set; }
		public List<WinnerReviewT> winner_review { get; set; }
		public List<MobileBbsT> mobile_bbs { get; set; }
		public List<GuideImage> mobile_guide_popup { get; set; }
	}

	public class BannerInfo
	{
		public int seq { get; set; }
		public int priority { get; set; }
		public string g_eid { get; set; }
		public string eid { get; set; }
		public string title { get; set; }
		public string url { get; set; }
		public string image { get; set; }
		public string image_mobile { get; set; }
		public string alt { get; set; }
		public string guide_image { get; set; }
		public string guide_alt { get; set; }
		public DateTime disp_start_dt { get; set; }
		public DateTime disp_end_dt { get; set; }
	}

	public class MonthlyEventT
	{
		public string seq { get; set; }
		public string priority { get; set; }

		public int org_eid1 { get; set; }
		public int org_eid2 { get; set; }
		public int org_eid3 { get; set; }

		public string eid1 { get; set; }
		public string eid2 { get; set; }
		public string eid3 { get; set; }

		public string eidEvtImg1 { get; set; }
		public string eidEvtImg2 { get; set; }
		public string eidEvtImg3 { get; set; }

		public string eidAlt1 { get; set; }
		public string eidAlt2 { get; set; }
		public string eidAlt3 { get; set; }

		public string entry_target { get; set; }

		public string image { get; set; }
		public string image_mobile { get; set; }
		public string alt { get; set; }
		public string disp_start_dt { get; set; }
		public string disp_end_dt { get; set; }
		public string main_template_image_url { get; set; }
		public string mobile_main_template_image_url { get; set; }
		public string main_template_bg_image_url { get; set; }
		public string mobile_main_template_bg_image_url { get; set; }
		public string main_template_text { get; set; }
		public string giveaway_hist_slot_image_url { get; set; }
		public string mobile_giveaway_hist_slot_image_url { get; set; }
		public string giveaway_image_url { get; set; }
		public string mobile_giveaway_image_url { get; set; }
		public string entry_target_text { get; set; }
		public string entry_condition_text { get; set; }

		public DateTime? entry_start_dt { get; set; }
		public DateTime? entry_end_dt { get; set; }

		public string win_guide_text { get; set; }
		public string notice_title { get; set; }
		public string notice_content { get; set; }
		public string notice_type { get; set; }

		public List<GuideTempleteT> guide_templetes { get; set; }
	}

	public class GuideTempleteT
	{
		public string eventzone_disp_event_seq { get; set; }
		public string eventzone_disp_seq { get; set; }
		public string priority { get; set; }
		public string expose_target_type { get; set; }
		public string etc_setup_content { get; set; }
		public string g_eid { get; set; }
		public string eid { get; set; }
		public string giveaway_amt { get; set; }
		public string mlang_event_nm { get; set; }
		public string event_url { get; set; }
		public string event_image { get; set; }
		public string event_image_mobile { get; set; }
		public string event_image_alt { get; set; }
		public DateTime disp_start_dt { get; set; }
		public DateTime disp_end_dt { get; set; }
	}

	public class SocialT
	{
		public string seq { get; set; }
		public string priority { get; set; }
		public string url { get; set; }
		public string image { get; set; }
		public string image_mobile { get; set; }
		public string image_mobile1 { get; set; }
		public string image_mobile2 { get; set; }
		public string image_mobile3 { get; set; }
		public string image_mobile4 { get; set; }
		public string alt { get; set; }
		public string disp_start_dt { get; set; }
		public string disp_end_dt { get; set; }
		public string notice_content { get; set; }
		public string notice_type { get; set; }
		public string mobile_content { get; set; }
		public string mobile_url { get; set; }
		public string mobile_disp_text { get; set; }
		public string eid { get; set; }
		public int org_eid { get; set; }
	}

	public class WinnerReviewT
	{
		public string seq { get; set; }
		public string priority { get; set; }
		public string url { get; set; }
		public string image { get; set; }
		public string image_mobile { get; set; }
		public string alt { get; set; }
		public string disp_start_dt { get; set; }
		public string disp_end_dt { get; set; }
		public string notice_content { get; set; }
		public string notice_type { get; set; }
	}

	public class MobileBbsT
	{
		public string seq { get; set; }
		public string priority { get; set; }
		public string disp_start_dt { get; set; }
		public string disp_end_dt { get; set; }
		public string notice_type { get; set; }
		public string mobile_content { get; set; }
	}
}

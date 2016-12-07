using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.EventV2
{
	public class GreenCarpetT
	{
		public List<MainPosterT> main { get; set; }
		public List<HistoryT> history { get; set; }
		public List<NoticeT> notice_list { get; set; }
	}

	public class MainPosterT
	{
		public int seq { get; set; }
		public int priority { get; set; }
		public DateTime disp_start_dt { get; set; }
		public DateTime disp_end_dt { get; set; }
		public string title { get; set; }
		public string title_img { get; set; }
		public string cd_nm { get; set; }
		public string screen_place { get; set; }
		public string main_l_img { get; set; }
		public string main_s_img { get; set; }
		public string target_cust { get; set; }
		public string entry_period { get; set; }
		public string entry_s_dt { get; set; }
		public string entry_e_dt { get; set; }
		public string entry_condition { get; set; }
		public string win_notice { get; set; }
		public string use_way { get; set; }
		public string dt_place { get; set; }
		public string open_dt { get; set; }
		public string lead_actor { get; set; }
		public string main_content { get; set; }
		public string sub_content { get; set; }
		public string scrn_info_url { get; set; }
		public string scrn_site_url { get; set; }
		public string scrn_vdo_url { get; set; }
		public string giveaway_entry_period { get; set; }
		public string giveaway_entry_way { get; set; }
		public string winner_announ { get; set; }
		public string win_amt { get; set; }
		public int entry_eid_cnt { get; set; }

		public List<EidT> eid_list { get; set; }
		public List<ImageT> img_list { get; set; }
		public List<GiveawayT> giveaway_list { get; set; }
	}

	public class EidT
	{
		public int event_seq { get; set; }
		public string etc_setup_content { get; set; }
		public int priority { get; set; }
		public string eid_script { get; set; }
		public string event_nm { get; set; }
		public string event_img { get; set; }
	}

	public class ImageT
	{
		public int event_seq { get; set; }
		public string etc_setup_content { get; set; }
		public int priority { get; set; }
		public string event_img { get; set; }
	}

	public class GiveawayT
	{
		public int event_seq { get; set; }
		public string etc_setup_content { get; set; }
		public int priority { get; set; }
		public string GdNo { get; set; }
		public string GdNm { get; set; }
		public int no { get; set; }
		public string event_img { get; set; }
	}

	public class HistoryT
	{
		public int priority { get; set; }
		public string cd_nm { get; set; }
		public string history_nm { get; set; }
		public string history_img_url { get; set; }
		public string history_title { get; set; }
		public string history_content { get; set; }
		public DateTime disp_start_dt { get; set; }
		public DateTime disp_end_dt { get; set; }
	}
}

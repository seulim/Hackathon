using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.EventV2
{
	public class CardBenefitJsonEntityT
	{
		public CardBenefitT cardbenefit { get; set; }
	}

	public class CardBenefitT
	{
		public List<CardHalbuT> card_halbu_list { get; set; }
		public List<CardHalbuT> card_halbu_shop_goods_list { get; set; }
		public List<CardBenefitBannerT> band_banner { get; set; }
		public List<CardEventT> card_event_list { get; set; }
		public List<JaeHuCardT> jaehu_card_list { get; set; }
		public List<CardPointBenenfitT> card_point_benefit_list { get; set; }
		public List<GuideImage> mobile_guide_popup { get; set; }
		public List<HalbuT> mobile_card_halbu_list { get; set; }
	}

	public class CardHalbuT
	{
		public string title { get; set; }
		public List<string> date { get; set; }
		public List<string> halbu { get; set; }
		public List<string> condition { get; set; }
	}

	public class CardBenefitBannerT
	{
		public string seq { get; set; }
		public int priority { get; set; }
		public string url { get; set; }
		public string image { get; set; }
		public string image_mobile { get; set; }
		public string alt { get; set; }
		public DateTime disp_start_dt { get; set; }
		public DateTime disp_end_dt { get; set; }
	}

	public class CardEventT
	{
		public string banner_pos_type { get; set; }
		public string s_class_cd_nm { get; set; }
		public List<CardBenefitBannerT> banner_list { get; set; }
	}

	public class JaeHuCardT
	{
		public string event_image { get; set; }
		public string event_image_mobile { get; set; }
		public DateTime disp_start_dt { get; set; }
		public DateTime disp_end_dt { get; set; }
		public string banner_url { get; set; }
		public string banner_image_url { get; set; }
		public string mlang_banner_image_text { get; set; }
		public string s_class_cd_nm { get; set; }
		public string event_guide_image_url { get; set; }
		public string event_guide_image_text { get; set; }
		public List<BtnLinkT> btnLink_list { get; set; }
	}

	public class BtnLinkT
	{
		public string text { get; set; }
		public string url { get; set; }
	}

	public class CardPointBenenfitT
	{
		public string banner_pos_type { get; set; }
		public string s_class_cd_nm { get; set; }
		public List<CardBenefitBannerT> banner_list { get; set; }
	}

	public class PointBenefitJsonEntityT
	{
		public PointBenefitT pointBenefit { get; set; }
		public PointBenefitT pointAdd { get; set; }
	}

	public class PointBenefitT
	{
		public List<PointBenefitBannerT> band_banner { get; set; }
		public List<JaehuT> jaehu_list { get; set; }
		public List<FaqT> faq_list { get; set; }
	}

	public class PointBenefitBannerT
	{
		public string seq { get; set; }
		public string priority { get; set; }
		public string url { get; set; }
		public string image { get; set; }
		public string image_mobile { get; set; }
		public string alt { get; set; }
		public DateTime disp_start_dt { get; set; }
		public DateTime disp_end_dt { get; set; }
	}

	public class JaehuT
	{
		public string seq { get; set; }
		public string priority { get; set; }
		public string title { get; set; }
		public string image { get; set; }
		public string image_mobile { get; set; }
		public string alt { get; set; }
		public string guide_image { get; set; }
		public string guide_image_mobile { get; set; }
		public string guide_alt { get; set; }
		public DateTime disp_start_dt { get; set; }
		public DateTime disp_end_dt { get; set; }
		public string banner_pos_type { get; set; }
		public List<CategoryGroupT> category_group_list { get; set; }
	}

	public class CategoryGroupT
	{
		public string eventzone_disp_seq { get; set; }
		public string group_name { get; set; }
		public List<CategoryT> category_list { get; set; }
	}

	public class CategoryT
	{
		public string category_cd { get; set; }
		public string category_name { get; set; }
	}

	public class FaqT
	{
		public string banner_pos_type { get; set; }
		public string s_class_cd_nm { get; set; }
		public List<ArticleT> article_list { get; set; }
	}

	public class ArticleT
	{
		public string seq { get; set; }
		public string priority { get; set; }
		public DateTime disp_start_dt { get; set; }
		public DateTime disp_end_dt { get; set; }
		public string notice_title { get; set; }
		public string notice_content { get; set; }
		public string notice_type { get; set; }
		public string notice_image_url { get; set; }
		public string notice_image_text { get; set; }
	}

	public class HalbuT
	{
		public string seq { get; set; }
		public string priority { get; set; }
		public DateTime disp_start_dt { get; set; }
		public DateTime disp_end_dt { get; set; }
		public string mobile_content { get; set; }
	}
}

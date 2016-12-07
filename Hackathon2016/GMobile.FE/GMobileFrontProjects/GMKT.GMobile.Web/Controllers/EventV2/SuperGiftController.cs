using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GMKT.GMobile.Web.Models.EventV2;
using GMKT.GMobile.Biz.EventV2;
using GMKT.GMobile.Data.EventV2;
using GMKT.GMobile.Util;
using GMKT.Framework;

namespace GMKT.GMobile.Web.Controllers.EventV2
{
	public class SuperGiftController : EventControllerBase
	{
		private const string TOP_BANNER_EVENT_MANAGE_TYPE = "25";
		private const string TOP_BANNER_EXPOSE_TARGET_TYPE = "08";

		private const string HISTORY_DIRECTPAYMENT = "DP";
		private const string HISTORY_MOBILEPAYMENT = "MP";

		public ActionResult Index()
		{
			//SetHomeTabName( "쿠폰" );
			ViewBag.Title = "Gmarket";
			SuperGiftModel model = new SuperGiftModel();

			model.NaviIcons = new EventCommonBiz().GetNavigationIcons();

			List<CommonBannerT> topBanner = new EventCommonBiz_Cache().GetCommonTopBanner( TOP_BANNER_EVENT_MANAGE_TYPE, TOP_BANNER_EXPOSE_TARGET_TYPE );
			if( topBanner != null && topBanner.Count > 0 )
			{
				model.TopBanner = new BannerT( topBanner[0].ImageUrl, topBanner[0].Alt, topBanner[0].LinkUrl );
			}

			SuperGiftJsonEntityT jsonData = new SuperGiftBiz_Cache().GetSuperGiftEntity();
			if( jsonData != null )
			{
				SuperGiftT data = jsonData.supergift;				

				if( data.top_banner.Count > 0 )
				{
					model.TopBanner = new BannerT( data.top_banner[0].image_mobile, data.top_banner[0].alt, data.top_banner[0].url );
				}

				if( data.monthly_event.Count > 0 )
				{
					model.MainTemplete = new BannerT( data.monthly_event[0].mobile_main_template_image_url, data.monthly_event[0].alt, "", data.monthly_event[0].eid1 );

					model.MainTempleteOptions = new List<BannerT>();


					// alt 가 text 로 대체
					if( !String.IsNullOrEmpty( data.monthly_event[0].eid2 ) )
					{
						//옵션이 있는 경우에는 전부 옵션화
						if( !String.IsNullOrEmpty( data.monthly_event[0].eid1 ) )
						{
							model.MainTempleteOptions.Add( new BannerT( "", data.monthly_event[0].eidAlt1, "", data.monthly_event[0].eid1 ) );
						}
						model.MainTempleteOptions.Add( new BannerT( "", data.monthly_event[0].eidAlt2, "", data.monthly_event[0].eid2 ) );
					}

					if( !String.IsNullOrEmpty( data.monthly_event[0].eid3 ) )
					{
						model.MainTempleteOptions.Add( new BannerT( "", data.monthly_event[0].eidAlt3, "", data.monthly_event[0].eid3 ) );
					}

					model.EntryTarget = data.monthly_event[0].entry_target_text;
					model.EntryStartDate = data.monthly_event[0].entry_start_dt;
					model.EntryEndDate = data.monthly_event[0].entry_end_dt;

					if( data.monthly_event[0].entry_target == HISTORY_DIRECTPAYMENT )
					{
						model.BuyHistory = new SuperGiftBiz().GetMonthlyBuyHistory();
						model.HistoryTitle = "바로접속";
					}
					else
					{
						model.BuyHistory = new SuperGiftBiz().GetMobileBuyHistory();
						model.HistoryTitle = "모바일접속";
					}

					model.EntryCondition = data.monthly_event[0].entry_condition_text;
					model.WinGuide = data.monthly_event[0].win_guide_text;

					model.GuideBanners = new List<BannerT>();
					if( data.monthly_event[0].guide_templetes.Count > 0 )
					{
						foreach( GuideTempleteT i in data.monthly_event[0].guide_templetes )
						{
							model.GuideBanners.Add( new BannerT( i.event_image_mobile, i.event_image_alt, i.event_url ) );
						}
					}
				}

				if( data.social.Count > 0 )
				{
					model.ShareInfoImage1 = data.social[0].image_mobile1;
					model.ShareInfoImage2 = data.social[0].image_mobile2;
					model.ShareInfoImage3 = data.social[0].image_mobile3;
					model.ShareBannerImage = data.social[0].image_mobile4;
					model.ShareInfoText = data.social[0].mobile_disp_text;
					model.SnsHtml = data.social[0].mobile_content;
					model.ShareUrl = data.social[0].mobile_url;

					DateTime today = DateTime.Now.Date;
					model.IsShareApplied = new EventCommonBiz().GetCountOfAppliedEids( today, "", data.social[0].org_eid ) == 0 ? false : true;
					
					try
					{
						model.ECif = data.social[0].eid.Split( ',' )[1].Trim( ' ', '\'' );
						model.EPif = data.social[0].eid.Split( ',' )[0].Trim( ' ', '\'' );
					}
					catch
					{
						model.ECif = "";
						model.EPif = "";
					}
				}

				if( data.mobile_bbs.Count > 0 )
				{
					model.NoticeHtml = data.mobile_bbs[0].mobile_content.Split(new string[] { "<br>" }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
				}

				model.MobileGuidePopupImageUrl = Urls.MobileImageShortUrl + "/mobile.gmarket/images/640/main/layer/layer_supergift.png";
				if (data.mobile_guide_popup != null && data.mobile_guide_popup.Count > 0)
				{
					if (!String.IsNullOrEmpty(data.mobile_guide_popup[0].image_mobile))
					{
						model.MobileGuidePopupImageUrl = data.mobile_guide_popup[0].image_mobile;
					}
				}

				if( PageAttr.IsLogin )
				{
					model.IsVip = gmktUserProfile.BuyerGrade == BuyerGradeEnum.SVIP || gmktUserProfile.BuyerGrade == BuyerGradeEnum.VIP;
					if( model.IsVip.Value )
					{
						DateTime timestamp = DateTime.Today;
						if( data.monthly_event != null && data.monthly_event.Count > 0)
						{
							if( new EventCommonBiz().GetCountOfAppliedEids( timestamp, "", data.monthly_event[0].org_eid1, data.monthly_event[0].org_eid2, data.monthly_event[0].org_eid3 ) > 0 )
							{
								model.IsAlreadyApplied = true;
							}
						}
					}
				}
			}

			#region 페이스북 공유하기
			string faceBookImage = String.Format( "{0}/640/main/pluszone_ico.png", Urls.MobileImageUrlV2 );
			PageAttr.FbTitle = "G마켓 슈퍼기프트";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/SuperGift";
			PageAttr.FbTagImage = faceBookImage;
			PageAttr.FbTagDescription = "G마켓 슈퍼기프트";
			#endregion

			return View( "~/Views/EventV2/SuperGift/Index.cshtml", model );
		}

		public RedirectResult ApplyMainCoupon( string id )
		{
			if( PageAttr.IsLogin )
			{
				string[] encryptedString = id.Split( ',' );
				return CommonApplyEventPlatformGmarket( encryptedString[0].Trim( '\'' ), encryptedString[1].Trim( '\'' ), "" );
			}
			else // // 20140128 - blacklist 모바일 적용
			{
				string href = Urls.LoginUrl + "?URL=" + "http://" + Request.Url.Host + Url.Action( "Index" );
				return Redirect( href );
			}
		}

		public int GetCountOfAppliedEidsForShare()
		{
			DateTime timeStamp = DateTime.Now.Date;
			int eid = new SuperGiftBiz_Cache().GetSuperGiftEntity().supergift.social[0].org_eid;
			return new EventCommonBiz().GetCountOfAppliedEids( timeStamp, "", eid );
		}
		
	}
}

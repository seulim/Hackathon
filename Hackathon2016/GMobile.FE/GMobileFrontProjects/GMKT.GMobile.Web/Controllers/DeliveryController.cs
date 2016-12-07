using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Biz.Common;
using GMKT.GMobile.Data;
using GMKT.GMobile.Util;
using GMKT.GMobile.Web.Models;
using GMKT.Web.Context;
using GMKT.GMobile.Web.Util;
using GMKT.GMobile.CommonData;
using GMKT.Web.Membership;

namespace GMKT.GMobile.Web.Controllers
{
	public class DeliveryController : GMobileControllerBase
	{
		public static readonly string[] ALLOWED_CROSS_DOMAIN_CALL = { Urls.MobileWebUrl, Urls.MDeliveryUrl };

		public ActionResult Index()
		{
			ViewBag.Title = "배달 - G마켓 모바일";
			SetHomeTabName("배달");

			PageAttr.IsMain = true;
			PageAttr.HeaderType = HeaderTypeEnum.Normal;

			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket 배달";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Delivery";
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket 배달";
			#endregion

			DeliveryModel model = new DeliveryModel();
			
			if( PageAttr.IsLogin && gmktUserProfile != null && false == string.IsNullOrEmpty( gmktUserProfile.UserInfoString ) )
			{
				model.Agreement = GetAgreemetType();
				string userInfo = gmktUserProfile.UserInfoString;
				if( String.IsNullOrEmpty( GMobileWebContext.Current.UserProfile.DeliveryAgreementString ) == false )
				{
					DeliveryApiBiz biz = new DeliveryApiBiz();
					if( GMobileWebContext.Current.UserProfile.IsAgreeDeliveryAgreementPosition
						&& model.Agreement.IsAgreePositionInfo == false)
					{
						if( biz.SetDeliveryAgreeInfo( DeliveryPosityionAddType.Position, "Y", userInfo ) == 0 )
						{
							model.Agreement.IsAgreePositionInfo = true;
						}
					}
					if( GMobileWebContext.Current.UserProfile.IsAgreeDeliveryAgreementThirdParty
						&& model.Agreement.IsAgreeThirdParty == false )
					{
						if( biz.SetDeliveryAgreeInfo( DeliveryPosityionAddType.ThirdParty, "Y", userInfo ) == 0 )
						{
							model.Agreement.IsAgreeThirdParty = true;
						}
					}
					GMobileWebContext.Current.UserProfile.DeleteDeliveryAgreement();
				}
			}
			else
			{
				model.Agreement = new DeliveryAgree();
				if( String.IsNullOrEmpty( GMobileWebContext.Current.UserProfile.DeliveryAgreementString ) == false )
				{
					if( GMobileWebContext.Current.UserProfile.IsAgreeDeliveryAgreementPosition )
					{
						model.Agreement.IsAgreePositionInfo = true;
					}
					if( GMobileWebContext.Current.UserProfile.IsAgreeDeliveryAgreementThirdParty )
					{
						model.Agreement.IsAgreeThirdParty = true;
					}
				}
			}

			return View( model );
		}

		public ActionResult Agreement()
		{
			if( PageAttr.IsApp )
			{
				DeliveryAgreementModel model = new DeliveryAgreementModel();
				model.Agreement = GetAgreemetType();
				ViewBag.IsApp = true;
				return View( model );
			}
			else
			{
				return null;
			}
		}

		public ActionResult BannerAll()
		{
			ViewBag.HeaderTitle = "배달 프로모션 전체";
			BannerAllM model = new BannerAllM();

			DeliveryBannerCategory bannerCategory = new DeliveryApiBiz().GetDeliveryBannerCategory(string.Empty, 0, 0, "0");
			if (bannerCategory != null && bannerCategory.Banner != null)
			{
				model.BannerList = bannerCategory.Banner;
			}
			else
			{
				model.BannerList = new List<DeliveryBannerCategoryT>();
			}

			return View(model);
		}
		
		[HttpPost]
		public JsonResult GetAddressSearch(string keyword)
		{
			if (ALLOWED_CROSS_DOMAIN_CALL.Contains(Request.Headers.Get("Origin")))
			{
				Response.AddHeader("Access-Control-Allow-Origin", Request.Headers.Get("Origin"));
				Response.AddHeader("Access-Control-Allow-Credentials", "true");
			}
			
			DaumAddressToCoordChannel model = new DeliveryApiBiz().GetAddressSearch(keyword);
			
			return Json(model);
		}

		[HttpPost]
		public JsonResult GetCoordToAddress(double longitude, double latitude)
		{
			if (ALLOWED_CROSS_DOMAIN_CALL.Contains(Request.Headers.Get("Origin")))
			{
				Response.AddHeader("Access-Control-Allow-Origin", Request.Headers.Get("Origin"));
				Response.AddHeader( "Access-Control-Allow-Credentials", "true" );
			}

			DaumCoordToAddress model = new DeliveryApiBiz().GetCoordToAddress(longitude, latitude);

			return Json(model);
		}

		[HttpPost]
		public JsonResult GetDeliveryBannerCategory(double longitude, double latitude, string zipCode)
		{
			if (ALLOWED_CROSS_DOMAIN_CALL.Contains(Request.Headers.Get("Origin")))
			{
				Response.AddHeader("Access-Control-Allow-Origin", Request.Headers.Get("Origin"));
				Response.AddHeader( "Access-Control-Allow-Credentials", "true" );
			}

			string userInfo = string.Empty;
			if( gmktUserProfile != null && false == string.IsNullOrEmpty( gmktUserProfile.UserInfoString ) )
			{
				userInfo = gmktUserProfile.UserInfoString;
			}

			DeliveryBannerCategory model = new DeliveryApiBiz().GetDeliveryBannerCategory( userInfo, longitude, latitude, zipCode );

			return Json(model);
		}

		[HttpPost]
		public JsonResult GetDeliveryMain()
		{
			if( ALLOWED_CROSS_DOMAIN_CALL.Contains( Request.Headers.Get( "Origin" ) ) )
			{
				Response.AddHeader( "Access-Control-Allow-Origin", Request.Headers.Get( "Origin" ) );
				Response.AddHeader( "Access-Control-Allow-Credentials", "true" );
			}

			string userInfo = string.Empty;
			if( gmktUserProfile != null && false == string.IsNullOrEmpty( gmktUserProfile.UserInfoString ) )
			{
				userInfo = gmktUserProfile.UserInfoString;
			}

			DeliveryMain model = new DeliveryApiBiz().GetDeliveryMain( userInfo );

			return Json( model );
		}

		[HttpPost]
		public JsonResult GetDeliveryShop(double longitude, double latitude, string zipCode, int myShopPageCount)
		{
			if (ALLOWED_CROSS_DOMAIN_CALL.Contains(Request.Headers.Get("Origin")))
			{
				Response.AddHeader("Access-Control-Allow-Origin", Request.Headers.Get("Origin"));
				Response.AddHeader( "Access-Control-Allow-Credentials", "true" );
			}

			string userInfo = string.Empty;
			if (gmktUserProfile != null && false == string.IsNullOrEmpty(gmktUserProfile.UserInfoString))
			{
				userInfo = gmktUserProfile.UserInfoString;
			}

			DeliveryMain model = new DeliveryApiBiz().GetDeliveryShop(userInfo, longitude, latitude, zipCode, myShopPageCount);

			return Json(model);
		}

		[HttpPost]
		public JsonResult GetCustNo()
		{
			if( ALLOWED_CROSS_DOMAIN_CALL.Contains( Request.Headers.Get( "Origin" ) ) )
			{
				Response.AddHeader( "Access-Control-Allow-Origin", Request.Headers.Get( "Origin" ) );
				Response.AddHeader( "Access-Control-Allow-Credentials", "true" );
			}

			if( gmktUserProfile != null && false == string.IsNullOrEmpty( gmktUserProfile.UserInfoString ) )
			{
				try
				{
					string custNo = gmktUserProfile.CustNo;

                    
					var encryptedString = MobileAESHelper.AESEncrypt( RandomUtil.GetString( 3 ) + "__" + custNo + "__" + DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
					return Json( new { encryptedString = encryptedString.Replace('+', '@') } );
				}
				catch
				{
					return Json( new { encryptedString = "" } );
				}
			}
			else
			{
                GMobileNonUserProfile gmktNonUserProfile = new GMobileNonUserProfile();

                if (gmktNonUserProfile != null && false == string.IsNullOrEmpty(gmktNonUserProfile.UserInfoString))
                {
                    string custNo = gmktNonUserProfile.CustNo;

                    
					var encryptedString = MobileAESHelper.AESEncrypt( RandomUtil.GetString( 3 ) + "__" + custNo + "__" + DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss" ) );
					return Json( new { encryptedString = encryptedString.Replace('+', '@') } );
                }
                else
                {
                    return Json( new { encryptedString = "" } );
                }
			}
		}

		[HttpPost]
		public JsonResult SetAgreement( [System.Web.Http.FromBody] DeliveryAgree input )
		{
			if( ALLOWED_CROSS_DOMAIN_CALL.Contains( Request.Headers.Get( "Origin" ) ) )
			{
				Response.AddHeader( "Access-Control-Allow-Origin", Request.Headers.Get( "Origin" ) );
				Response.AddHeader( "Access-Control-Allow-Credentials", "true" );
			}

			string userInfo = string.Empty;
			if( gmktUserProfile != null && false == string.IsNullOrEmpty( gmktUserProfile.UserInfoString ) )
			{
				userInfo = gmktUserProfile.UserInfoString;
				DeliveryAgree result = new DeliveryAgree();
				DeliveryApiBiz biz = new DeliveryApiBiz();
				if( input.IsAgreePositionInfo )
				{
					if( biz.SetDeliveryAgreeInfo( DeliveryPosityionAddType.Position, "Y", userInfo ) == 0 )
					{
						result.IsAgreePositionInfo = true;
					}
				}

				if( input.IsAgreeThirdParty )
				{
					if( biz.SetDeliveryAgreeInfo( DeliveryPosityionAddType.ThirdParty, "Y", userInfo ) == 0 )
					{
						result.IsAgreeThirdParty = true;
					}
				}

				return Json( result );
			}
			else
			{
				if( input.IsAgreePositionInfo )
				{
					GMobileWebContext.Current.UserProfile.UpdateDeliveryAgreementPosition(input.IsAgreePositionInfo);
				}
				if( input.IsAgreeThirdParty )
				{
					GMobileWebContext.Current.UserProfile.UpdateDeliveryAgreementThirdParty(input.IsAgreeThirdParty);
				}
				
				return null;
			}
		}

		private DeliveryAgree GetAgreemetType()
		{
			if( ALLOWED_CROSS_DOMAIN_CALL.Contains( Request.Headers.Get( "Origin" ) ) )
			{
				Response.AddHeader( "Access-Control-Allow-Origin", Request.Headers.Get( "Origin" ) );
				Response.AddHeader( "Access-Control-Allow-Credentials", "true" );
			}

			string userInfo = string.Empty;
			if( gmktUserProfile != null && false == string.IsNullOrEmpty( gmktUserProfile.UserInfoString ) )
			{
				userInfo = gmktUserProfile.UserInfoString;
				return new DeliveryApiBiz().GetDeliveryAgreeInfo( userInfo );
			}
			else
			{
				return new DeliveryAgree();
			}
		}
	}
}
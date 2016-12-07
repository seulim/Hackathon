using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GMKT.Framework;
using GMKT.GMobile.Util;
using GMKT.Component.Member.SelfAuth;
using GMKT.Component.Member.Util;

namespace GMKT.GMobile.Web.Controllers
{
	public class AuthentificationController : GMobileControllerBase
	{
		public ActionResult AdultCheck(string rtnurl)
		{
			ViewBag.HeaderTitle = "로그인";
			if (!Request.IsSecureConnection)
			{
				return new RedirectResult(Request.Url.AbsoluteUri.Replace(Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower()));
			}

			if( !PageAttr.IsLogin )
			{
				return AlertMessageAndHistorybackOrClose( "로그인 하시기 바랍니다.", "-1" );
			}

			
			if(gmktUserProfile.UserRole == Framework.RoleEnum.NonMember)
			{
				gmktNonUserProfile.UpdateAdultUseLoginCheckCookie(true);
			}
			else
			{
				gmktUserProfile.UpdateAdultUseLoginCheckCookie(true);
			}

			if( gmktUserProfile.CustType != null )
			{
				if( rtnurl == null )
				{
					rtnurl = Urls.MobileWebUrl;
				}
				if( gmktUserProfile.CustType == EnumMemberType.PersonalBuyer )
				{
					ViewBag.Ctype = "PP";
					ViewBag.rtnurl = rtnurl;
					return View();
				}
				else if( gmktUserProfile.CustType == EnumMemberType.PersonalSeller )
				{
					ViewBag.Ctype = "EP";
					ViewBag.rtnurl = rtnurl;
					return View();
				}
				else
				{
					//TODO by sue
					if(gmktUserProfile.UserRole == RoleEnum.BuyerMember || gmktUserProfile.UserRole == RoleEnum.SellerMember)
					{
						string adultAuthCookie = MemberUtil.GetEncAdultAuthCookies( gmktUserProfile.CustNo, gmktUserProfile.CustType, gmktUserProfile.IsForeigner );
						MemberUtil.SetAdultCookieAfterSelfAuth(adultAuthCookie);
						//gmktUserProfile.UpdateAdultAuth( adultAuthCookie );
					}
					else
					{
						MemberUtil.SetAdultCookieAfterSelfAuth();
					}
					////Set adultUseLoginCheck 'N'
					//gmktUserProfile.UpdateAdultUseLoginCheckCookie( false );
					return Redirect( rtnurl );
				}
			}
			else
			{
				return AlertMessageAndHistorybackOrClose( "로그인 오류! 다시 확인 하신 후 로그인 하시기 바랍니다.", "-1" );
			}
		}

		public ActionResult AdultSuccess(string rtnurl)
		{
			ViewBag.rtnurl = rtnurl;
			return View();
		}
	}
}

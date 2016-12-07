using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Util;
using GMKT.Component.Member;
using GMKT.Component.Member.SelfAuth;
using GMKT.Component.Member.Data.Entity;
using GMKT.IPin.IPinV2Proxy.IPinV2ServiceFacade;
using GMKT.Framework.Security;
using GMKT.GMobile.Biz.Member;
using GMKT.GMobile.Data.Member;
using GMKT.Component.Member.Util;

namespace GMKT.GMobile.Web.Controllers
{
	public class IPinAuthController : GMobileControllerBase
	{
		private string gmktRequestSeqno = String.Empty;
		private string ipinRequestSeqno = String.Empty;
		private string fromType;
		private string certfyMethodType;
		private string nextUrl;
		private string custType;	// 회원 타입 추가 (PP, PC, EP, EC)

		public ActionResult Test()
		{
			return View();
		}

		public ActionResult IPinAuthForSignUp( string cType, string fType )
		{
			//SSL 체크 
			if( !Request.IsSecureConnection )
			{
				return new RedirectResult( Request.Url.AbsoluteUri.Replace( Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower() ) );
			}

			//본인인증 페이지 접근 유효성 체크
			// fromType(본인인증 페이지 호출경로) 없는 경우
			if( String.IsNullOrEmpty( fType ) )
			{
				string href = Urls.MobileWebUrl + "/SignUp";
				return AlertMessageAndTopLocationChange( "비정상적인 페이지 호출입니다. Code:I0", href );
			}

			// custType(회원 가입 타입)이 없는 경우 : PP, PC, EP, EC
			if( String.IsNullOrEmpty( cType ) )
			{
				string href = Urls.MobileWebUrl + "/SignUp";
				return AlertMessageAndTopLocationChange( "비정상적인 페이지 호출입니다. Code:I1", href );
			}

			fromType = fType.ToUpper();				// 모바일 국문 회원가입 본인인증 : MREGI
			custType = cType.ToUpper();				// 회원 타입 저장 (PP, PC, EP, EC)

			//인증페이지 호출경로 타입체크 
			if( fromType != AuthFromType.MobileRegistration )
			{
				string href = Urls.MobileWebUrl + "/SignUp";
				return AlertMessageAndTopLocationChange( "비정상적인 페이지 호출입니다. Code:I2", href );
			}

			//TODO : 주석제거(테스트위해 주석처리함)
			//앱이 아닐떄만 refer 페이지를 확인함
			if( !PageAttr.IsAndroidApp && !PageAttr.IsIphoneApp )
			{
				if( Request.UrlReferrer != null && String.Compare( Request.UrlReferrer.AbsolutePath, "/SignUp/SignUpAuth", true ) == 0 )
				{
				}
				else
				{
					string href = Urls.MobileWebUrl + "/SignUp";
					return AlertMessageAndTopLocationChange( "비정상적인 페이지 호출입니다. Code:I3", href );
				}
			}

			if( PageAttr.IsLogin )
			{
				string href = Urls.MobileWebUrl + "/SignUp";
				return AlertMessageAndTopLocationChange( "로그인 상태입니다.", href );
			}

			return RegistrationPageForMobileMemberCommon();
		}

		#region 회원 가입 > 인증성공/실패 redirect 없는 기본 아이핀인증 페이지 호출 - RegistrationPageForMobileMemberCommon()
		[NonAction]
		private ActionResult RegistrationPageForMobileMemberCommon()
		{
			string custNo = gmktUserProfile.CustNo;
			string loginID = gmktUserProfile.LoginID;
			string custName = gmktUserProfile.CustName;
			nextUrl = String.Empty;
			certfyMethodType = AuthMethodType.IPin; // 인증수단 : 아이핀

			string certOrganName = new SelfAuthBiz().GetCustomSelfAuthSettingByServiceCode( fromType );
			CustomSelfAuthLogT selfAuthInfo = null;

			// 본인인증이력T context 채우기(비로그인상태)
			selfAuthInfo = SelfAuthHelper.FillSelfAuthContextForGuest( fromType, certfyMethodType, AuthStatus.AuthRequest, UserUtil.IPAddressBySecure(), String.Empty, certOrganName );

			//TODO : DCM에 lion_write 추가 필요..
			long seqNo = new SelfAuthBiz().AddCustomSelfAuthLog( selfAuthInfo );
			string href = Urls.MobileWebUrl + "SignUp";

			// 본인인증 정보 테이블에 request data insert
			if( seqNo < 0 )
			{
				return AlertMessageAndTopLocationChange( "본인인증 정보를 가져오는 도중 오류가 발생되었습니다. 잠시 후 다시 시도해주세요.", href );
			}

			// 인증요청번호 생성
			string reqSeq = SelfAuthHelper.GetRequestSeqno( seqNo );

			// nextURL 세팅
			string protocolTypeUrl = Request.IsSecureConnection ? Urls.MobileWebUrlSecure : Urls.MobileWebUrl;
			string returnUrl = protocolTypeUrl + "/IPinAuth/IPinAuthForSignUpResult" + "?next=" + nextUrl + "&rSeq=" + reqSeq + "&fType=" + fromType + "&mType=" + certfyMethodType + "&cType=" + custType;

			// IPin 암호화된 데이터 가져오기
			GetIPinRequestDataResponseT res = new GMKT.IPin.IPinV2Proxy.IPinV2().GetIPinRequestData( returnUrl, "A" );
			if( res != null )
			{
				if( res.ResultType == CommonResultType.SUCCESS )
				{
					ViewBag.IPinV2CertUrl = res.IPinV2CertUrl;
					ViewBag.EncodeData = res.EncodeData;

					return View();
				}
				else
				{
					return AlertMessageAndTopLocationChange( res.ResultDescription, href );
				}
			}
			else
			{
				return AlertMessageAndTopLocationChange( "오류가 발생하였습니다. 다시 시도해주세요.", href );
			}
		}
		#endregion

		public ActionResult IPinAuthForAdult( string cType, string fType, string rtnurl )
		{
			if( !Request.IsSecureConnection )
			{
				return new RedirectResult( Request.Url.AbsoluteUri.Replace( Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower() ) );
			}

			//본인인증 페이지 접근 유효성 체크
			// fromType(본인인증 페이지 호출경로) 없는 경우
			if( String.IsNullOrEmpty( fType ) )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:I0", "-1" );
			}

			// custType(회원 가입 타입)이 없는 경우 : PP, PC, EP, EC
			if( String.IsNullOrEmpty( cType ) )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:I1", "-1" );
			}

			fromType = fType.ToUpper();				// 모바일 국문 회원가입 본인인증 : MREGI
			custType = cType.ToUpper();				// 회원 타입 저장 (PP, PC, EP, EC)

			//인증페이지 호출경로 타입체크 
			if( fromType != AuthFromType.MobileRegistration && fromType != AuthFromType.MobileAdult )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:I2", "-1" );
			}

			string custNo = gmktUserProfile.CustNo;
			string loginID = gmktUserProfile.LoginID;
			string custName = gmktUserProfile.CustName;
			nextUrl = rtnurl;
			certfyMethodType = AuthMethodType.IPin; // 인증수단 : 아이핀

			string certOrganName = new SelfAuthBiz().GetCustomSelfAuthSettingByServiceCode( fromType );
			CustomSelfAuthLogT selfAuthInfo = null;

			if(gmktUserProfile != null && !String.IsNullOrEmpty(gmktUserProfile.LoginID) && (gmktUserProfile.UserRole == Framework.RoleEnum.BuyerMember || gmktUserProfile.UserRole == Framework.RoleEnum.SellerMember))
			{
				selfAuthInfo = SelfAuthHelper.FillSelfAuthContextForLoginMember( gmktUserProfile.CustNo, gmktUserProfile.LoginID, gmktUserProfile.CustName, fromType, certfyMethodType, AuthStatus.AuthRequest, UserUtil.IPAddressBySecure(), String.Empty, certOrganName );
			}
			else
			{
				selfAuthInfo = SelfAuthHelper.FillSelfAuthContextForGuest( fromType, certfyMethodType, AuthStatus.AuthRequest, UserUtil.IPAddressBySecure(), String.Empty, certOrganName );
			}

			long seqNo = new SelfAuthBiz().AddCustomSelfAuthLog( selfAuthInfo );

			if( seqNo < 0 )
			{
				return AlertMessageAndHistorybackOrClose( "본인인증 정보를 가져오는 도중 오류가 발생되었습니다. 잠시 후 다시 시도해주세요.", "-1" );
			}

			string reqSeq = SelfAuthHelper.GetRequestSeqno( seqNo );

			// nextURL 세팅
			string protocolTypeUrl = Request.IsSecureConnection ? Urls.MobileWebUrlSecure : Urls.MobileWebUrl;
			string returnUrl = protocolTypeUrl + "/IPinAuth/IPinAuthForAdultResult" + "?next=" + nextUrl + "&rSeq=" + reqSeq + "&fType=" + fromType + "&mType=" + certfyMethodType + "&cType=" + custType;

			// IPin 암호화된 데이터 가져오기
			GetIPinRequestDataResponseT res = new GMKT.IPin.IPinV2Proxy.IPinV2().GetIPinRequestData( returnUrl, "A" );
			if( res != null )
			{
				if( res.ResultType == CommonResultType.SUCCESS )
				{
					ViewBag.IPinV2CertUrl = res.IPinV2CertUrl;
					ViewBag.EncodeData = res.EncodeData;

					return View();
				}
				else
				{
					return AlertMessageAndHistorybackOrClose( res.ResultDescription, "-1" );
				}
			}
			else
			{
				return AlertMessageAndHistorybackOrClose( "오류가 발생하였습니다. 다시 시도해주세요.", "-1" );
			}
		}

		public ActionResult IPinAuthForSignUpResult( string next, string rSeq, string fType, string mType, string cType, string enc_data, string RequestSeqNo )
		{
			string href = Urls.MobileWebUrl + "/SignUp";

			//SSL 체크
			if( !Request.IsSecureConnection )
			{
				return new RedirectResult( Request.Url.AbsoluteUri.Replace( Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower() ) );
			}

			// 본인인증 페이지 접근 유효성 체크
			if( String.IsNullOrEmpty( fType ) || String.IsNullOrEmpty( enc_data ) || String.IsNullOrEmpty( rSeq ) || String.IsNullOrEmpty( RequestSeqNo ) || next == null )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:I4", "-3" );
			}

			// mType(인증방법)이 없는 경우 비정상호출
			if( String.IsNullOrEmpty( mType ) || String.Compare( mType, AuthMethodType.IPin, true ) != 0 )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:I5", "-3" );
			}

			fromType = fType.ToUpper();
			custType = cType.ToUpper();
			certfyMethodType = mType.ToUpper();
			gmktRequestSeqno = rSeq;
			ipinRequestSeqno = RequestSeqNo;
			nextUrl = next;


			//인증페이지 호출경로 타입체크 
			if( fromType != AuthFromType.MobileRegistration )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:I6", "-3" );
			}

			if( PageAttr.IsLogin )
			{
				return AlertMessageAndTopLocationChange( "로그인 상태입니다.", href );
			}

			#region 각 호출 페이지별 비즈니스 수행
			GetIPinUserInfoResponseT result = new GMKT.IPin.IPinV2Proxy.IPinV2().GetIPinUserInfo( ipinRequestSeqno, enc_data );
			if( result != null && result.ResultType == CommonResultType.SUCCESS )
			{
				// 회원 가입 여부 확인 (DI값 존재 여부 확인)
				if( !PageAttr.IsLogin )
				{
					// 인증자 DI로 회원확인 (PP, EP)
					ResultT memberApiResult = new RegistrationApiBiz().GetVerificationRegistrationPersonal( result.DI, custType, result.BirthDate );
					if( memberApiResult != null )
					{
						int sCheckExistYn = (int)memberApiResult.ResultCode;
						if( sCheckExistYn == 0 )
						{
							// 신규 회원 가입 진행 성공
						}
						else
						{
							//return AlertMessageAndHistorybackOrClose(memberApiResult.ResultDescription, "-3");
							return AlertMessageAndTopLocationChange( memberApiResult.ResultDescription, href );
						}
					}
					else
					{
						return AlertMessageAndTopLocationChange( "오류가 발생하였습니다. 다시 시도해주세요.", href );
					}
				}
				else
				{
					return AlertMessageAndTopLocationChange( "로그인 상태입니다.", href );
				}

				// 인증성공
				return RegistrationMemberCertifySuccessProcess( result, enc_data );
			}
			else
			{
				// 인증실패
				return RegistrationMemberCertifyFailProcess( result, enc_data );
			}
			#endregion
		}

		public ActionResult IPinAuthForAdultResult( string next, string rSeq, string fType, string mType, string cType, string enc_data, string RequestSeqNo )
		{
			//SSL 체크
			if( !Request.IsSecureConnection )
			{
				return new RedirectResult( Request.Url.AbsoluteUri.Replace( Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower() ) );
			}

			// 본인인증 페이지 접근 유효성 체크
			if( String.IsNullOrEmpty( fType ) || String.IsNullOrEmpty( enc_data ) || String.IsNullOrEmpty( rSeq ) || String.IsNullOrEmpty( RequestSeqNo ) || next == null )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:I4", "-3" );
			}

			// mType(인증방법)이 없는 경우 비정상호출
			if( String.IsNullOrEmpty( mType ) || String.Compare( mType, AuthMethodType.IPin, true ) != 0 )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:I5", "-3" );
			}

			fromType = fType.ToUpper();
			custType = cType.ToUpper();
			certfyMethodType = mType.ToUpper();
			gmktRequestSeqno = rSeq;
			ipinRequestSeqno = RequestSeqNo;
			nextUrl = next;


			//인증페이지 호출경로 타입체크 
			if( fromType != AuthFromType.MobileAdult )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:I6", "-3" );
			}

			#region 각 호출 페이지별 비즈니스 수행
			GetIPinUserInfoResponseT result = new GMKT.IPin.IPinV2Proxy.IPinV2().GetIPinUserInfo( ipinRequestSeqno, enc_data );
			if( result != null && result.ResultType == CommonResultType.SUCCESS )
			{
				string birthDay = result.BirthDate;
				DateTime dtBirthDay = Convert.ToDateTime( birthDay.Substring( 0, 4 ) + "-" + birthDay.Substring( 4, 2 ) + "-" + birthDay.Substring( 6, 2 ) );
				TimeSpan res = DateTime.Now - dtBirthDay;
				int age = int.Parse( new DateTime( res.Ticks ).ToString( "yy" ) );

				if( age <= 19 )
				{
					return AlertMessageAndHistorybackOrClose( "해당 상품은 청소년보호법의 규정에 의하여 19세 미만의 청소년이 이용할 수 없습니다.", String.Empty );
				}
				
				//2016-07-06 sookykim 비회원 로그인 개선
				if(gmktUserProfile.UserRole == Framework.RoleEnum.BuyerMember || gmktUserProfile.UserRole == Framework.RoleEnum.SellerMember)
				{
					if(!String.IsNullOrEmpty(gmktUserProfile.CustNo) && !String.IsNullOrEmpty(gmktUserProfile.LoginID))
					{
						List<CustomIpinInfoT> customIpinInfo = new MyInfoBiz().GetCustomIpinInfoByDI( "ID", String.Empty, String.Empty, result.DI, gmktUserProfile.CustNo );
						{
							if( customIpinInfo == null || customIpinInfo.Count == 0 )
							{
								new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( gmktRequestSeqno ), result.UserName, AuthMethodType.IPin, AuthStatus.NotOneSelf, gmktRequestSeqno + "_" + ipinRequestSeqno, String.Empty, enc_data );
								return AlertMessageAndHistorybackOrClose( "로그인 정보와 본인인증 고객정보가 일치하지 않습니다. 확인하시고 다시 시도해주세요", "-3" );
							}
						}
					}
					new SelfAuthBiz().SetCustomAdultAuth( gmktUserProfile.CustNo, mType );
					new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( gmktRequestSeqno ), result.UserName, AuthMethodType.IPin, AuthStatus.AuthSucess, gmktRequestSeqno + "_" + ipinRequestSeqno, String.Empty, enc_data );
					string adultAuthCookie = MemberUtil.GetEncAdultAuthCookies( gmktUserProfile.CustNo, gmktUserProfile.CustType, gmktUserProfile.IsForeigner );
					MemberUtil.SetAdultCookieAfterSelfAuth(adultAuthCookie);
				}
				else
				{
					MemberUtil.SetAdultCookieAfterSelfAuth();
				}

				if( PageAttr.IsApp )
				{
					return RegisterOnlyJavascriptWithoutResponse( "alert('인증에 성공했습니다.');parent.successRedirect()" );
				}
				else
				{
					return AlertMessageAndTopLocationChange( "인증에 성공했습니다.", nextUrl );
				}
			}
			else
			{
				new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( gmktRequestSeqno ), result.UserName, AuthMethodType.IPin, AuthStatus.AuthFail, gmktRequestSeqno + "_" + ipinRequestSeqno, String.Empty, enc_data );
				// 인증실패
				return AlertMessageAndHistorybackOrClose( "본인인증에 오류가 발생되었습니다.", "-3" );
			}
			#endregion
		}

		#region 회원 가입 > 인증성공 프로세스 - RegistrationMemberCertifySuccessProcess(GetIPinUserInfoResponseT successInfo, string encodeData)
		[NonAction]
		private ActionResult RegistrationMemberCertifySuccessProcess( GetIPinUserInfoResponseT successInfo, string encodeData )
		{
			// 인증성공정보저장 (gmktRequestSeqno + "_" + ipinRequestSeqno)
			new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( gmktRequestSeqno ), successInfo.UserName, AuthMethodType.IPin, AuthStatus.AuthSucess, gmktRequestSeqno + "_" + ipinRequestSeqno, String.Empty, encodeData );

			if( fromType == AuthFromType.MobileRegistration )
			{
				//return CertifySuccessForFrontRegistrationMember(successInfo, encodeData);
				return RegisterOnlyJavascriptWithoutResponse( String.Format( "alert('본인인증이 성공하였습니다.'); window.top.GotoTermsAgree('{0}','{1}','{2}','{3}');", encodeData, ipinRequestSeqno, "I", "PP" ) );
			}

			return new EmptyResult();
		}
		#endregion


		#region 회원 가입 > 인증실패 프로세스 - RegistrationMemberCertifyFailProcess(GetIPinUserInfoResponseT failInfo, string encodeData)
		[NonAction]
		private ActionResult RegistrationMemberCertifyFailProcess( GetIPinUserInfoResponseT failInfo, string encodeData )
		{
			// 인증실패정보저장
			if( failInfo != null )
			{
				new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( gmktRequestSeqno ), failInfo.UserName, AuthMethodType.IPin, AuthStatus.AuthFail, gmktRequestSeqno + "_" + ipinRequestSeqno, String.Empty, encodeData );
			}

			if( fromType == AuthFromType.MobileRegistration )
			{
				return AlertMessageAndHistorybackOrClose( "본인인증에 오류가 발생되었습니다.\\n" + failInfo.ResultDescription, "-3" );
			}

			return new EmptyResult();
		}
		#endregion
	}
}
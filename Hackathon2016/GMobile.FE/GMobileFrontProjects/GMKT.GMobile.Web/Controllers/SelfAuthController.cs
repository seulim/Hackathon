using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Util;
using GMKT.Component.Member;
using GMKT.Component.Member.SelfAuth;
using GMKT.Component.Member.Data.Entity;
using GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade;
using GMKT.GMobile.Data.Member;
using GMKT.GMobile.Biz.Member;
using GMKT.Component.Member.Util;

namespace GMKT.GMobile.Web.Controllers
{
	public class SelfAuthController : GMobileControllerBase
	{
		private string successNextUrl = String.Empty;
		private string errorNextUrl = String.Empty;
		private string requestSeqno = String.Empty;
		private string fromType;
		private string certfyMethodType;
		private string nextUrl;
		private CertAuthType authType;
		private string custType;	// 회원 타입 추가 (PP, PC, EP, EC)

		public ActionResult SelfAuthForSignUp( string cType, string fType )
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
				return AlertMessageAndTopLocationChange( "비정상적인 페이지 호출입니다. Code:M0", href );
			}

			// custType(회원 가입 타입)이 없는 경우 : PP, PC, EP, EC
			if( String.IsNullOrEmpty( cType ) )
			{
				string href = Urls.MobileWebUrl + "/SignUp";
				return AlertMessageAndTopLocationChange( "비정상적인 페이지 호출입니다. Code:M1", href );
			}

			fromType = fType.ToUpper();				// 모바일 국문 회원가입 본인인증 : MREGI
			custType = cType.ToUpper();				// 회원 타입 저장 (PP, PC, EP, EC)

			//인증페이지 호출경로 타입체크 
			if( fromType != AuthFromType.MobileRegistration )
			{
				string href = Urls.MobileWebUrl + "/SignUp";
				return AlertMessageAndTopLocationChange( "비정상적인 페이지 호출입니다. Code:M2", href );
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
					return AlertMessageAndTopLocationChange( "비정상적인 페이지 호출입니다. Code:M3", href );
				}
			}

			if( PageAttr.IsLogin )
			{
				string href = Urls.MobileWebUrl + "/SignUp";
				return AlertMessageAndTopLocationChange( "로그인 상태입니다.", href );
			}

			return RegistrationPageForMobileMemberCommon();
		}

		#region 회원 가입 > 인증성공/실패 redirect 없는 기본 휴대폰인증 페이지 호출 - RegistrationPageForMobileMemberCommon()
		[NonAction]
		private ActionResult RegistrationPageForMobileMemberCommon()
		{
			string custNo = gmktUserProfile.CustNo;
			string loginID = gmktUserProfile.LoginID;
			string custName = gmktUserProfile.CustName;
			string href = Urls.MobileWebUrl + "/SignUp";
			certfyMethodType = AuthMethodType.MobilePhone; // 인증수단 : 휴대폰
			successNextUrl = String.Empty;
			errorNextUrl = String.Empty;
			authType = CertAuthType.MobilePhone;

			string certOrganName = new SelfAuthBiz().GetCustomSelfAuthSettingByServiceCode( fromType );
			CustomSelfAuthLogT selfAuthInfo = null;

			// 본인인증이력T context 채우기(비로그인상태)
			selfAuthInfo = SelfAuthHelper.FillSelfAuthContextForGuest( fromType, certfyMethodType, AuthStatus.AuthRequest, UserUtil.IPAddressBySecure(), String.Empty, certOrganName );

			//TODO : DCM에 lion_write 추가 필요..
			long seqNo = new SelfAuthBiz().AddCustomSelfAuthLog( selfAuthInfo );

			// 본인인증 정보 테이블에 request data insert
			if( seqNo < 0 )
			{
				return AlertMessageAndTopLocationChange( "본인인증 정보를 가져오는 도중 오류가 발생되었습니다. 잠시 후 다시 시도해주세요.", href );
			}

			// 인증요청번호 생성
			string reqSeq = SelfAuthHelper.GetRequestSeqno( seqNo );

			//성공 url, 실패 url 세팅
			string protocolTypeUrl = Request.IsSecureConnection ? Urls.MobileWebUrlSecure : Urls.MobileWebUrl;
			string returnUrl = protocolTypeUrl + "/SelfAuth/SelfAuthForSignUpResult" + "?next=" + successNextUrl + "&rSeq=" + reqSeq + "&fType=" + fromType + "&mType=" + certfyMethodType + "&cType=" + custType;
			string errorUrl = protocolTypeUrl + "/SelfAuth/SelfAuthForSignUpResult" + "?next=" + errorNextUrl + "&rSeq=" + reqSeq + "&fType=" + fromType + "&mType=" + certfyMethodType + "&cType=" + custType;

			GetCertificateRequestDataV2ResponseT res = new GMKT.IPin.CertificateAuthProxyV2.CertificateAuthV2().GetCertificateRequestData( authType, reqSeq, returnUrl, errorUrl, "B" );
			if( res != null )
			{
				if( res.ResultType == CommonResultType.SUCCESS )
				{
					// 본인인증 팝업 url로 replace
					return RegisterOnlyJavascriptWithoutResponse( String.Format( "location.replace('{0}', '', 'width=300, height=280');", res.CertPageUrl ) );

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

		public ActionResult SelfAuthForAdult( string cType, string fType, string rtnurl )
		{
			if( !Request.IsSecureConnection )
			{
				return new RedirectResult( Request.Url.AbsoluteUri.Replace( Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower() ) );
			}

			// 본인인증 페이지 접근 유효성 체크
			// fromType(본인인증 페이지 호출경로) 없는 경우
			if( String.IsNullOrEmpty( fType ) )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:M0", "-1" );
			}

			// custType(회원 가입 타입)이 없는 경우 : PP, PC, EP, EC
			if( String.IsNullOrEmpty( cType ) )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:M1", "-1" );
			}

			fromType = fType.ToUpper();				// 모바일 국문 회원가입 본인인증 : MREGI
			custType = cType.ToUpper();				// 회원 타입 저장 (PP, PC, EP, EC)

			//인증페이지 호출경로 타입체크 
			if( fromType != AuthFromType.MobileRegistration && fromType != AuthFromType.MobileAdult )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:M2", "-1" );
			}

			string custNo = gmktUserProfile.CustNo;
			string loginID = gmktUserProfile.LoginID;
			string custName = gmktUserProfile.CustName;
			certfyMethodType = AuthMethodType.MobilePhone; // 인증수단 : 휴대폰
			successNextUrl = rtnurl;
			errorNextUrl = Urls.MobileWebUrl;
			authType = CertAuthType.MobilePhone;

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

			// 본인인증 정보 테이블에 request data insert
			if( seqNo < 0 )
			{
				return AlertMessageAndHistorybackOrClose( "본인인증 정보를 가져오는 도중 오류가 발생되었습니다. 잠시 후 다시 시도해주세요.", "-1" );
			}

			// 인증요청번호 생성
			string reqSeq = SelfAuthHelper.GetRequestSeqno( seqNo );

			//성공 url, 실패 url 세팅
			string protocolTypeUrl = Request.IsSecureConnection ? Urls.MobileWebUrlSecure : Urls.MobileWebUrl;
			string returnUrl = protocolTypeUrl + "/SelfAuth/SelfAuthForAdultResult" + "?next=" + successNextUrl + "&rSeq=" + reqSeq + "&fType=" + fromType + "&mType=" + certfyMethodType + "&cType=" + custType;
			string errorUrl = protocolTypeUrl + "/SelfAuth/SelfAuthForAdultResult" + "?next=" + errorNextUrl + "&rSeq=" + reqSeq + "&fType=" + fromType + "&mType=" + certfyMethodType + "&cType=" + custType;

			GetCertificateRequestDataV2ResponseT res = new GMKT.IPin.CertificateAuthProxyV2.CertificateAuthV2().GetCertificateRequestData( authType, reqSeq, returnUrl, errorUrl, "B" );
			if( res != null )
			{
				if( res.ResultType == CommonResultType.SUCCESS )
				{
					// 본인인증 팝업 url로 replace
					return RegisterOnlyJavascriptWithoutResponse( String.Format( "location.replace('{0}', '', 'width=300, height=280');", res.CertPageUrl ) );
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

		public ActionResult SelfAuthForSignUpResult( string next, string rSeq, string fType, string mType, string cType, string encodeData )
		{
			string href = Urls.MobileWebUrl + "/SignUp";

			//SSL 체크
			if( !Request.IsSecureConnection )
			{
				return new RedirectResult( Request.Url.AbsoluteUri.Replace( Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower() ) );
			}

			// 본인인증 페이지 접근 유효성 체크
			if( String.IsNullOrEmpty( rSeq ) || String.IsNullOrEmpty( fType ) || String.IsNullOrEmpty( mType ) || String.IsNullOrEmpty( cType ) || String.IsNullOrEmpty( encodeData ) || next == null )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:M4", "-3" );
			}

			// mType(인증방법)이 없는 경우 비정상호출
			if( String.IsNullOrEmpty( mType ) || String.Compare( mType, AuthMethodType.MobilePhone, true ) != 0 )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:M5", "-3" );
			}

			fromType = fType.ToUpper();
			custType = cType.ToUpper();
			certfyMethodType = mType.ToUpper();
			requestSeqno = rSeq;
			nextUrl = next;


			//인증페이지 호출경로 타입체크 
			if( fromType != AuthFromType.MobileRegistration )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:M6", "-3" );
			}

			if( PageAttr.IsLogin )
			{
				return AlertMessageAndTopLocationChange( "로그인 상태입니다.", href );
			}

			#region 각 호출 페이지별 비즈니스 수행
			GetCertificateDecodeDataV2ResponseT result = new GMKT.IPin.CertificateAuthProxyV2.CertificateAuthV2().GetCertificateDecodeData( requestSeqno, encodeData ); // G206989_2014091502463809578864427404
			if( result != null && result.ResultType == CommonResultType.SUCCESS )
			{
				if( String.IsNullOrEmpty( result.ErrorCode ) )
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
					return RegistrationMemberCertifySuccessProcess( result, encodeData );
				}
				else
				{
					// 인증실패
					return RegistrationMemberCertifyFailProcess( result, encodeData );
				}
			}
			else
			{
				// 인증실패
				return RegistrationMemberCertifyFailProcess( result, encodeData );
			}
			#endregion
		}

		public ActionResult SelfAuthForAdultResult( string next, string rSeq, string fType, string mType, string cType, string encodeData )
		{
			//SSL 체크
			if( !Request.IsSecureConnection )
			{
				return new RedirectResult( Request.Url.AbsoluteUri.Replace( Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower() ) );
			}

			// 본인인증 페이지 접근 유효성 체크
			if( String.IsNullOrEmpty( rSeq ) || String.IsNullOrEmpty( fType ) || String.IsNullOrEmpty( mType ) || String.IsNullOrEmpty( cType ) || String.IsNullOrEmpty( encodeData ) || next == null )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:M4", "-3" );
			}

			// mType(인증방법)이 없는 경우 비정상호출
			if( String.IsNullOrEmpty( mType ) || String.Compare( mType, AuthMethodType.MobilePhone, true ) != 0 )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:M5", "-3" );
			}

			fromType = fType.ToUpper();
			custType = cType.ToUpper();
			certfyMethodType = mType.ToUpper();
			requestSeqno = rSeq;
			nextUrl = next;


			//인증페이지 호출경로 타입체크 
			if( fromType != AuthFromType.MobileAdult )
			{
				return AlertMessageAndHistorybackOrClose( "비정상적인 페이지 호출입니다. Code:M6", "-3" );
			}

			#region 각 호출 페이지별 비즈니스 수행
			GetCertificateDecodeDataV2ResponseT result = new GMKT.IPin.CertificateAuthProxyV2.CertificateAuthV2().GetCertificateDecodeData( requestSeqno, encodeData ); // G206989_2014091502463809578864427404
			if( result != null && result.ResultType == CommonResultType.SUCCESS )
			{
				if( String.IsNullOrEmpty( result.ErrorCode ) )
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
									new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( result.RequestSeqNo ), result.Name, "M", AuthStatus.NotOneSelf, result.RequestSeqNo, String.Empty, encodeData );

									return AlertMessageAndHistorybackOrClose( "로그인 정보와 본인인증 고객정보가 일치하지 않습니다. 확인하시고 다시 시도해주세요.", "-3" );
								}
							}
						}
						new SelfAuthBiz().SetCustomAdultAuth( gmktUserProfile.CustNo, mType );
						new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( result.RequestSeqNo ), result.Name, ConvertToAuthMethodType( result.CertKind ), AuthStatus.AuthSucess, result.RequestSeqNo, String.Empty, encodeData );
						//성인인증이력 쿠키 갱신
						string adultAuthCookie = MemberUtil.GetEncAdultAuthCookies( gmktUserProfile.CustNo, gmktUserProfile.CustType, gmktUserProfile.IsForeigner );
						MemberUtil.SetAdultCookieAfterSelfAuth(adultAuthCookie);
					}
					else
					{
						MemberUtil.SetAdultCookieAfterSelfAuth();
					}

					if( PageAttr.IsApp )
					{
						string tempStr = "<script type='text/javascript'>alert(\"인증에 성공하였습니다.\");location.href = '" + Urls.MobileWebUrl + "/Authentification/AdultSuccess?rtnurl=" + nextUrl + "'</script>";
						return Content( tempStr, "text/html", System.Text.Encoding.UTF8 );
					}
					else
					{
						return AlertMessageAndLocationChange( "인증에 성공했습니다.", nextUrl );
					}
				}
				else
				{
					new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( result.RequestSeqNo ), result.Name, ConvertToAuthMethodType( result.CertKind ), AuthStatus.AuthFail, result.RequestSeqNo, result.ErrorCode, encodeData );
					// 인증실패
					return AlertMessageAndHistorybackOrClose( "본인인증에 오류가 발생되었습니다.", "-3" );
				}
			}
			else
			{
				new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( result.RequestSeqNo ), result.Name, ConvertToAuthMethodType( result.CertKind ), AuthStatus.AuthFail, result.RequestSeqNo, result.ErrorCode, encodeData );
				// 인증실패
				return AlertMessageAndHistorybackOrClose( "본인인증에 오류가 발생되었습니다.", "-3" );
			}
			#endregion
		}


		#region 회원 가입 > 인증성공 프로세스 - RegistrationMemberCertifySuccessProcess(GetIPinUserInfoResponseT successInfo, string encodeData)
		[NonAction]
		private ActionResult RegistrationMemberCertifySuccessProcess( GetCertificateDecodeDataV2ResponseT successInfo, string encodeData )
		{
			// 인증성공정보저장 (gmktRequestSeqno + "_" + ipinRequestSeqno)
			new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( successInfo.RequestSeqNo ), successInfo.Name, ConvertToAuthMethodType( successInfo.CertKind ), AuthStatus.AuthSucess, successInfo.RequestSeqNo, String.Empty, encodeData );

			if( fromType == AuthFromType.MobileRegistration )
			{
				//return CertifySuccessForFrontRegistrationMember(successInfo, encodeData);
				return RegisterOnlyJavascriptWithoutResponse( String.Format( "alert('본인인증이 성공하였습니다.'); window.top.GotoTermsAgree('{0}','{1}','{2}','{3}');", encodeData, requestSeqno, "M", "PP" ) );
			}

			return new EmptyResult();
		}
		#endregion


		#region 회원 가입 > 인증실패 프로세스 - RegistrationMemberCertifyFailProcess(GetIPinUserInfoResponseT failInfo, string encodeData)
		[NonAction]
		private ActionResult RegistrationMemberCertifyFailProcess( GetCertificateDecodeDataV2ResponseT failInfo, string encodeData )
		{
			// 인증실패정보저장
			new SelfAuthBiz().SetCustomSelfAuthLog( SelfAuthHelper.GetSeqno( failInfo.RequestSeqNo ), failInfo.Name, ConvertToAuthMethodType( failInfo.CertKind ), AuthStatus.AuthFail, failInfo.RequestSeqNo, failInfo.ErrorCode, encodeData );

			if( fromType == AuthFromType.MobileRegistration )
			{
				return AlertMessageAndHistorybackOrClose( "본인인증에 오류가 발생되었습니다.\\n" + failInfo.ErrorDescription, "-3" );
			}

			return new EmptyResult();
		}
		#endregion


		#region CertAuthType 을 string 값으로 convert - ConvertTo(CertAuthType src)
		/// <summary>
		/// CertAuthType 을 string 값으로 convert - ConvertTo(CertAuthType src)
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static string ConvertToAuthMethodType( CertAuthType src )
		{
			switch( src )
			{
				case CertAuthType.Selection: return AuthMethodType.Selection;			// 선택
				case CertAuthType.MobilePhone: return AuthMethodType.MobilePhone;		// 핸드폰
				case CertAuthType.CreditCard: return AuthMethodType.CreditCard;			// 신용카드
				case CertAuthType.Certificate: return AuthMethodType.Certificate;		// 공인인증서
				case CertAuthType.BankAccount: return AuthMethodType.BankAccount;		// 금융계좌
				default: return "";
			}
		}
		#endregion
	}
}

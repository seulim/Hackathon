using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.Web.Filter;
using GMKT.IPin.IPinV2Proxy.IPinV2ServiceFacade;
using GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade;
using GMKT.GMobile.Biz.Member;
using GMKT.Component.Member;
using GMKT.Web.Context;
using GMKT.GMobile.Data.Member;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Web.Controllers
{
	public class SignUpController : GMobileControllerBase
    {
        //
        // GET: /SignUp/

        public ActionResult Index(string fromWhere = "")
        {
			//TODO : 주석제거(테스트위해 주석처리함)
			//SSL 체크 
			if (!Request.IsSecureConnection)
			{
				return new RedirectResult(Request.Url.AbsoluteUri.Replace(Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower()));
			}
			
			if (PageAttr.IsLogin)
			{
				return AlertMessageAndLocationChange("회원가입은 로그아웃 후 이용이 가능합니다.", Urls.MobileWebUrl);
			}

			ViewBag.FromWhere = fromWhere;

			return View();
        }

		public ActionResult SignUpAuth(string mType, string cType, string fType)
		{
			//SSL 체크 
			if (!Request.IsSecureConnection)
			{
				return new RedirectResult(Request.Url.AbsoluteUri.Replace(Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower()));
			}

			//로그인 체크
			if (PageAttr.IsLogin)
			{
				return AlertMessageAndLocationChange("회원가입은 로그아웃 후 이용이 가능합니다.", Urls.MobileWebUrl);
			}

			if (mType == "M") //휴대폰인증
			{
				ViewBag.AuthUrl = "/SelfAuth/SelfAuthForSignUp?cType=" + cType + "&fType=" + fType;
				ViewBag.mType = "M";
				ViewBag.iframeHeight = 900;
				ViewBag.iframeWidth = 325;
			}
			else if (mType == "I") //아이핀인증
			{
				ViewBag.AuthUrl = "/IPinAuth/IPinAuthForSignUp?cType=" + cType + "&fType=" + fType;
				ViewBag.mType = "I";
				ViewBag.iframeHeight = "700px";
				ViewBag.iframeWidth = "100%";
			}

			return View();
		}

		public JsonResult GetVerificationRegistrationCorp(string corpIdNo, string custType)
		{
			ResultT memberApiResult = new RegistrationApiBiz().GetVerificationRegistrationCorp(corpIdNo, custType);
			return Json(memberApiResult);
		}

		public ActionResult TermsAgree(FormCollection form)
		{
			//SSL 체크 
			if (!Request.IsSecureConnection)
			{
				return new RedirectResult(Request.Url.AbsoluteUri.Replace(Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower()));
			}

			//로그인 체크
			if (PageAttr.IsLogin)
			{
				return AlertMessageAndLocationChange("회원가입은 로그아웃 후 이용이 가능합니다.", Urls.MobileWebUrl);
			}

			if (form["cType"] == "PC") {
                CorpAuthResultT memberApiResult = new RegistrationApiBiz().GetCorpIdNoInfo(form["companyNo"]);
                if (memberApiResult != null && (memberApiResult.ResultCode == "01" || memberApiResult.ResultCode == "02" || memberApiResult.ResultCode == "03" || memberApiResult.ResultCode == "04" || memberApiResult.ResultCode == "07"))
				{
                    if (string.IsNullOrEmpty(memberApiResult.CorpName) || string.IsNullOrEmpty(memberApiResult.RepName))
                    {
                        string href = Urls.MobileWebUrl + "/SignUp";
                        return AlertMessageAndLocationChange("사업자 인증정보 확인이 필요 합니다. 고객님이 입력하신 정보와 인증기관(NICE 평가 정보) 등록된 정보가 다릅니다. NICE 평가정보㈜ 고객센터 (02-3771-1390)로 문의 주시면 신속하게 처리 받으실 수 있습니다. 불편을 드려 죄송합니다.", href);
                    }
					ViewBag.CompanyName = memberApiResult.CorpName;
				}
				else
				{
					string href = Urls.MobileWebUrl + "/SignUp";
                    return AlertMessageAndLocationChange("입력하신 사업자 번호의 사업자 등록 상태 확인이 필요 합니다.국세청 홈택스 서비스(www.hometax.go.kr)의 사업자 등록 상태 조회(조회서비스 -> 기타내역조회) 또는 국번없이 126(1번)을통해 확인 부탁 드립니다. 이용에 불편을 드려 죄송합니다.", href);
				}
			}

			ViewBag.MethodType = form["mType"];
			ViewBag.CustType = form["cType"];
			ViewBag.EncData = form["encdata"];
			ViewBag.SeqNo = form["seqno"];
			ViewBag.CompanyNo = form["companyNo"];
			ViewBag.FromWhere = form["fromWhere"];

			return View();
		}

		public ActionResult TermsAgreeForPP(string fromWhere = "")
		{
			//SSL 체크 
			if (!Request.IsSecureConnection)
			{
				return new RedirectResult(Request.Url.AbsoluteUri.Replace(Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower()));
			}

			//로그인 체크
			if (PageAttr.IsLogin)
			{
				return AlertMessageAndLocationChange("회원가입은 로그아웃 후 이용이 가능합니다.", Urls.MobileWebUrl);
			}

			ViewBag.FromWhere = fromWhere;

			return View();
		}

		[RequireHttps]
		public ActionResult BuyerAgree()
		{
			//약관동의_ G마켓 구매회원 약관
            return Content(new PolicyBiz().GetMemberPolicy(11));
		}
		[RequireHttps]
		public ActionResult FinanceAgree()
		{
			//약관동의_ 전자금융서비스 이용약관
            return Content(new PolicyBiz().GetMemberPolicy(12));
		}
		[RequireHttps]
		public ActionResult UsePersonalInfoAgree()
		{
			//약관동의_ 개인정보 수집 및 이용
            return Content(new PolicyBiz().GetMemberPolicy(34));
		}
		[RequireHttps]
		public ActionResult TreatPersonalInfoAgree()
		{
			//약관동의_ 개인정보의 취급위탁
            return Content(new PolicyBiz().GetMemberPolicy(35));
		}
		[RequireHttps]
		public ActionResult G9BuyerAgree()
		{
			//약관동의_ G9 구매회원 약관
            return Content(new PolicyBiz().GetMemberPolicy(14));
		}

		[RequireHttps]
		public ActionResult TermsAgreeSummary()
		{
			//약관 중요사항 summary
			return Content(new PolicyBiz().GetMemberPolicy(49));
		}

		public ActionResult SignUpForm(FormCollection form)
		{
			//SSL 체크 
			if (!Request.IsSecureConnection)
			{
				return new RedirectResult(Request.Url.AbsoluteUri.Replace(Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower()));
			}

			//로그인 체크
			if (PageAttr.IsLogin)
			{
				return AlertMessageAndLocationChange("회원가입은 로그아웃 후 이용이 가능합니다.", Urls.MobileWebUrl);
			}

			string sCustType = form["hidCustType"]; //PP or  PC
			string sAuthMethodType = form["hidMethodType"]; // M or I  or S(간편가입) (PP일때)
			//string sEncData = form["hidEncData"];
			//string sSeqNo = form["hidSeqNo"];
			string sPersonalInfoTreatAgrYN = form["PersonalInfoTreatAgrYN"];
			string sG9JoinYN = form["G9JoinYN"]; 
			string sCompanyNo = form["hidCompanyNo"];

			ViewBag.CustType = sCustType;
			ViewBag.MethodType = sAuthMethodType;
			//ViewBag.EncData = sEncData;
			//ViewBag.SeqNo = sSeqNo;
			ViewBag.PersonalInfoTreatAgrYN = sPersonalInfoTreatAgrYN;
			ViewBag.G9JoinYN = sG9JoinYN;
			ViewBag.CompanyNo = sCompanyNo;
			ViewBag.PersonalInfoThirdPartySupportAgrYN = form["PersonalInfoThirdPartySupportAgrYN"];
			ViewBag.PersonalInfoCollectUseAgrYN = form["PersonalInfoCollectUseAgrYN"];
			ViewBag.SmsRcvYn = form["SmsRcvYn"];
			ViewBag.ERcvYn = form["ERcvYn"];
			ViewBag.FromWhere = String.IsNullOrEmpty(form["hidFromWhere"]) ? String.Empty : form["hidFromWhere"];

			if (sCustType == "PP") 
			{
				
				
				//if (sAuthMethodType == AuthMethodType.MobilePhone) //휴대폰인증
				//{
				//    GetCertificateDecodeDataV2ResponseT result = new GMKT.IPin.CertificateAuthProxyV2.CertificateAuthV2().GetCertificateDecodeData(sSeqNo, sEncData); // G206989_2014091502463809578864427404
				//    if (result.ResultType == GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade.CommonResultType.SUCCESS)
				//    {
				//        ViewBag.Name = result.Name;
				//        ViewBag.PhoneNo = result.MobilePhoneNum;
				//        if (result.MobilePhoneNum.Length == 11)
				//        {
				//            ViewBag.PhoneNo1st = result.MobilePhoneNum.Substring(0, 3);
				//            ViewBag.PhoneNo2nd = result.MobilePhoneNum.Substring(3, 4);
				//            ViewBag.PhoneNo3rd = result.MobilePhoneNum.Substring(7, 4);
				//            ViewBag.PhoneNoTxt = result.MobilePhoneNum.Substring(0, 3) + "-" + result.MobilePhoneNum.Substring(3, 4) + "-" + result.MobilePhoneNum.Substring(7, 4);
				//        }
				//        else 
				//        {
				//            ViewBag.PhoneNo1st = result.MobilePhoneNum.Substring(0, 3);
				//            ViewBag.PhoneNo2nd = result.MobilePhoneNum.Substring(3, 3);
				//            ViewBag.PhoneNo3rd = result.MobilePhoneNum.Substring(6, 4);
				//            ViewBag.PhoneNoTxt = result.MobilePhoneNum.Substring(0, 3) + "-" + result.MobilePhoneNum.Substring(3, 3) + "-" + result.MobilePhoneNum.Substring(6, 4);
				//        }
				//        ViewBag.BirthDate = result.BirthDate;
				//        ViewBag.BirthDateTxt = result.BirthDate.Substring(0, 4) + "년 " + result.BirthDate.Substring(4, 2) + "월 " + result.BirthDate.Substring(6, 2) + "일";
				//    }
				//}
				//else if (sAuthMethodType == AuthMethodType.IPin) //아이핀인증
				//{
				//    GetIPinUserInfoResponseT result = new GMKT.IPin.IPinV2Proxy.IPinV2().GetIPinUserInfo(sSeqNo, sEncData);
				//    if (result.ResultType == GMKT.IPin.IPinV2Proxy.IPinV2ServiceFacade.CommonResultType.SUCCESS)
				//    {
				//        ViewBag.Name = result.UserName;
				//        ViewBag.BirthDate = result.BirthDate;
				//        ViewBag.BirthDateTxt = result.BirthDate.Substring(0, 4) + "년 " + result.BirthDate.Substring(4, 2) + "월 " + result.BirthDate.Substring(6, 2) + "일";
				//    }
				//} 
			}
			else if (sCustType == "PC")
			{
                CorpAuthResultT memberApiResult = new RegistrationApiBiz().GetCorpIdNoInfo(sCompanyNo);
                if (memberApiResult != null && (memberApiResult.ResultCode == "01" || memberApiResult.ResultCode == "02" || memberApiResult.ResultCode == "03" || memberApiResult.ResultCode == "04" || memberApiResult.ResultCode == "07"))
				{
                    if (string.IsNullOrEmpty(memberApiResult.CorpName) || string.IsNullOrEmpty(memberApiResult.RepName))
                    {
                        string href = Urls.MobileWebUrl + "/SignUp";
                        return AlertMessageAndLocationChange("사업자번호를 다시 입력해 주세요.", href);
                    }
					ViewBag.CompanyName = memberApiResult.CorpName;
				}
				else
				{
					string href = Urls.MobileWebUrl + "/SignUp";
					return AlertMessageAndLocationChange("사업자번호를 다시 입력해 주세요.", href);
				}
			}
			
			return View();
		}

		public JsonResult GetExistsLoginId(string id)
		{
			Boolean memberApiResult = new RegistrationApiBiz().GetExistsLoginId(id);
			return Json(memberApiResult);
		}

		public JsonResult GetExistsEmail(string email)
		{
			Boolean memberApiResult = new RegistrationApiBiz().GetExistsEmail(email);
			return Json(memberApiResult);
		}

		[HttpPost]
		public ActionResult GetSignUpValidation(FormCollection form)
		{
			//TODO for test
			//object testLoginId = "test4plan";
			//return RedirectToAction("SignUpComplete", new { loginId = testLoginId });
			
			//SSL 체크 
			if (!Request.IsSecureConnection)
			{
				return new RedirectResult(Request.Url.AbsoluteUri.Replace(Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower()));
			}

			//로그인 체크
			if (PageAttr.IsLogin)
			{
				return AlertMessageAndLocationChange("회원가입은 로그아웃 후 이용이 가능합니다.", Urls.MobileWebUrl);
			}

			string sCustType = form["hidCustType"]; //PP or  PC
			string sAuthMethodType = form["hidMethodType"]; // M or I  or S(간편가입) (PP일때)
			string sEncData = form["hidEncData"];
			string sSeqNo = form["hidSeqNo"];

			string sHtml;
			string sCertType = String.Empty;	//string sCertType = form["CertType"];
			string sCertName = String.Empty;	//string sCertName = form["CertName"];
			string sGender = String.Empty;
			string sNationalInfo = String.Empty;
			string sCI = String.Empty;
			string sDI = String.Empty;
			string sBirthDate = String.Empty;	// string sBirthDate = form["BirthDate"];
			bool isGender = false;
			bool isForeign = false;

			string sBizNum = String.Empty;
			string sBizCorpName = String.Empty;		// 상호명
			string sGbankPd01 = string.Empty;
			string fromWhere = String.IsNullOrEmpty(form["hidFromWhere"]) ? String.Empty : form["hidFromWhere"];

			if (sCustType == "PP")
			{
				if (sAuthMethodType == AuthMethodType.MobilePhone)	// M
				{
					GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade.GetCertificateDecodeDataV2ResponseT result1
				= new GMKT.IPin.CertificateAuthProxyV2.CertificateAuthV2().GetCertificateDecodeData(sSeqNo, sEncData);

					if (result1.ResultType == GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade.CommonResultType.SUCCESS)
					{
						sCertType = "M";
						sCertName = result1.Name;
						sGender = ConvertToGenderType(result1.Gender);						// 0 여성, 1 남성
						sNationalInfo = ConvertToNationalInfoType(result1.NationalInfo);	// 0 내국인, 1 외국인
						sCI = result1.CI;
						sDI = result1.DI;
						sBirthDate = result1.BirthDate;
					}
				}
				else if (sAuthMethodType == AuthMethodType.IPin)	// I
				{
					GMKT.IPin.IPinV2Proxy.IPinV2ServiceFacade.GetIPinUserInfoResponseT result2 = new GMKT.IPin.IPinV2Proxy.IPinV2().GetIPinUserInfo(sSeqNo, sEncData);

					if (result2.ResultType == GMKT.IPin.IPinV2Proxy.IPinV2ServiceFacade.CommonResultType.SUCCESS)
					{
						sCertType = "I";
						sCertName = result2.UserName;
						sGender = result2.GenderCode;			// 0 여성, 1 남성		
						sNationalInfo = result2.NationalInfo;	// 0 내국인, 1 외국인
						sCI = result2.CI;
						sDI = result2.DI;
						sBirthDate = result2.BirthDate;
					}
				}
				else if (sAuthMethodType == "S")	// 간편가입
				{
					sCertType = "S";
					sCertName = form["name_inp"];
				}
				else
				{
					sHtml = "<script type='text/javascript'>alert('본인인증수단이 잘못되었습니다.');history.back(-1);</script>";
					return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
				}

				//if (String.IsNullOrEmpty(sCertName) || String.IsNullOrEmpty(sGender) || String.IsNullOrEmpty(sNationalInfo) || String.IsNullOrEmpty(sCI) || String.IsNullOrEmpty(sDI))
				//{
				//    sHtml = "<script type='text/javascript'>alert('본인인증수단이 잘못되었습니다.');history.back(-1);</script>";
				//    return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
				//}

				isGender = (sGender == "1") ? true : false; // 0 여성, 1 남성
				isForeign = (sNationalInfo == "1") ? true : false; // 0 내국인, 1 외국인
			}
			else //사업자
			{
				sBizNum = form["hidCompanyNo"];
				sBizCorpName = form["hidCompanyName"];		// 회원이름

				if (String.IsNullOrEmpty(sBizNum) || String.IsNullOrEmpty(sBizCorpName))
				{
					sHtml = "<script type='text/javascript'>alert('본인인증수단이 잘못되었습니다.');history.back(-1);</script>";
					return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
				}
			}


			string sMobilePhoneNum = String.Empty;
			if (string.IsNullOrEmpty(form["hidHpNumTxt"]))
			{
				sMobilePhoneNum = form["hidHpNum1"] + "-" + form["hpNum2"] + "-" + form["hpNum3"];
			}
			else{
				sMobilePhoneNum = form["hidHpNumTxt"];
			}
			
			string sPersonalInfoTreatAgrYN = (form["PersonalInfoTreatAgrYN"] == "on") ? "Y" : "N";
			string sG9JoinYN = (form["G9JoinYN"] == "on") ? "Y" : "N";

			string sLoginId = form["userId"];

			string sPwd1, sPwd2;
			sPwd1 = form["userPW"];	// 비밀번호1
			sPwd2 = form["userPW2"];	// 비밀번호2

			//Validation 체크
			if (sPwd1.Length > 15 || sPwd2.Length > 15 && (sPwd1 != sPwd2))
			{
				sHtml = "<script type='text/javascript'>alert('비밀번호가 정확하지 않습니다.');history.back(-1);</script>";
				return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
			}

			string sAftEncPwd;
			sAftEncPwd = string.Empty;

			if (!string.IsNullOrEmpty(sPwd1) && !string.IsNullOrEmpty(sPwd2))
			{
				sAftEncPwd = sPwd1;
			}
			else
			{
				sHtml = "<script type='text/javascript'>alert('비밀번호가 정확하지 않습니다.');history.back(-1);</script>";
				return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
			}

			if (sCustType == "PC")
			{
				string sGbankPwd1, sGbankPwd2;
				sGbankPwd1 = form["gbankPW"];	// 비밀번호1
				sGbankPwd2 = form["gbankPW2"];	// 비밀번호2

				//Validation 체크
				if (sGbankPwd1.Length > 15 || sGbankPwd2.Length > 15 && (sGbankPwd1 != sGbankPwd2))
				{
					sHtml = "<script type='text/javascript'>alert('G통장 비밀번호가 정확하지 않습니다.');history.back(-1);</script>";
					return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
				}

				if (!string.IsNullOrEmpty(sGbankPwd1) && !string.IsNullOrEmpty(sGbankPwd2))
				{
					sGbankPd01 = sGbankPwd1;
				}
				else
				{
					sHtml = "<script type='text/javascript'>alert('G통장 비밀번호가 정확하지 않습니다.');history.back(-1);</script>";
					return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
				}

				if (sAftEncPwd.Equals(sGbankPd01))
				{
					sHtml = "<script type='text/javascript'>alert('입력하신 G통장 비밀번호는 회원 가입 비밀번호와 동일하여 사용하실 수 없습니다.');history.back(-1);</script>";
					return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
				}

				// G통장 비밀번호
				if (RegistrationReturn.NoError != ValidationPassword(sGbankPd01))
				{
					sHtml = "<script type='text/javascript'>alert('G통장 비밀번호가 잘못되었습니다.');history.back(-1);</script>";
					return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
				}
			}

			string sEmail1 = form["email1"];
			string sEmail2 = form["email2"];
			string sEmail = string.Concat(sEmail1, "@", sEmail2);
			string sERcvYn = (form["chkEmailRcv"] == "on") ? "Y" : "N";;		// 이메일 수신
			string sSmsRcvYn = (form["chkSmsRcv"] == "on") ? "Y" : "N";;	// SMS 수신
			string sPersonalInfoThirdPartySupportAgrYN = (form["PersonalInfoThirdPartySupportAgrYN"] == "on") ? "Y" : "N"; ;// 개인정보 제 3자 제공에 대한 동의
			string sPersonalInfoCollectUseAgrYN = (form["PersonalInfoCollectUseAgrYN"] == "on") ? "Y" : "N"; ; // 개인정보 수집 및 이용

			if (sCustType == "PP")
			{
				sERcvYn = (form["ERcvYn"] == "on") ? "Y" : "N";;		// 이메일 수신
				sSmsRcvYn = (form["SmsRcvYn"] == "on") ? "Y" : "N";;	// SMS 수신
			}

			#region 유효성 체크
			RegistrationReturn enumRegiReturn = RegistrationReturn.NoError;

			// 휴대폰
			if (string.IsNullOrEmpty(sMobilePhoneNum))
			{
				sHtml = "<script type='text/javascript'>alert('휴대폰번호는 필수 항목입니다.\\n휴대폰번호를 올바르게 입력해 주세요.');history.back(-1);</script>";
				return Content(sHtml, "text/html", System.Text.Encoding.UTF8); ;
			}

			// 아이디
			enumRegiReturn = ValidationId(sLoginId);
			if (RegistrationReturn.NoError != enumRegiReturn)
			{
				sHtml = "<script type='text/javascript'>alert('회원 아이디가 잘못되었습니다.');history.back(-1);</script>";
				return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
			}
			// 아이디 중복체크
			Boolean memberApiExistsLoginIdResult = new RegistrationApiBiz().GetExistsLoginId(sLoginId);
			if (memberApiExistsLoginIdResult)
			{
				sHtml = "<script type='text/javascript'>alert('회원 아이디가 중복되었습니다.');history.back(-1);</script>";
				return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
			}

			// 비밀번호
			enumRegiReturn = ValidationPassword(sAftEncPwd);
			if (RegistrationReturn.NoError != enumRegiReturn)
			{
				sHtml = "<script type='text/javascript'>alert('비밀번호가 잘못되었습니다.');history.back(-1);</script>";
				return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
			}

			// 이메일
			if (RegistrationValidation.IsValidEmailAddress(sEmail) == false)
			{
				sHtml = "<script type='text/javascript'>alert('이메일이 잘못되었습니다.');history.back(-1);</script>";
				return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
			}
			
			#endregion

			// API 호출 객체 생성

			if (sCustType == "PP")
			{
				//var request = new RegistrationPersonalBuyerT
				//{
				//    CustNm = sCertName,
				//    BirthDay = sBirthDate,
				//    Gender = isGender,
				//    LoginId = sLoginId,
				//    CustPv01 = sAftEncPwd,
				//    Email = sEmail,
				//    Hp = sMobilePhoneNum,
				//    IpinCI = sCI,
				//    IpinDI = sDI,
				//    ERcvYn = sERcvYn,
				//    SmsRcvYn = sSmsRcvYn,
				//    JaehuId = GMobileWebContext.Current.JahuID,
				//    IsForeign = isForeign,
				//    RegWhere = "M",														// 인입구분 프론트-F, 모바일-M, G9 - G
				//    AuthType = sCertType,												// 중복확인을 위한 인증방법 휴대폰-M, 아이핀-I
				//    PersonalInfoTreatAgrYN = sPersonalInfoTreatAgrYN,					// 개인정보 취급위탁 여부
				//    G9JoinYN = sG9JoinYN 												// 구매회원 G9구매회원 가입 동의 여부
				//};

				//string CompleteUrl = Urls.MobileWebUrl + "/SignUp/SignUpComplete";

				//var memberApiResult = new RegistrationApiBiz().RegistrationPersonalBuyer(request);

				var request = new SimpleRegistrationPersonalBuyerT
				{
					CustNm = sCertName,
					//BirthDay = sBirthDate,
					//Gender = isGender,
					LoginId = sLoginId,
					CustPv01 = sAftEncPwd,
					Email = sEmail,
					Hp = sMobilePhoneNum,
					//IpinCI = sCI,
					//IpinDI = sDI,
					ERcvYn = sERcvYn,
					SmsRcvYn = sSmsRcvYn,
					JaehuId = GMobileWebContext.Current.JahuID,
					//IsForeign = isForeign,
					RegWhere = "M",														// 인입구분 프론트-F, 모바일-M, G9 - G
					AuthType = sCertType,												// 중복확인을 위한 인증방법 휴대폰-M, 아이핀-I
					//PersonalInfoTreatAgrYN = sPersonalInfoTreatAgrYN,					// 개인정보 취급위탁 여부
					G9JoinYN = sG9JoinYN,												// 구매회원 G9구매회원 가입 동의 여부]
					PersonalInfoThirdPartySupportAgrYN = sPersonalInfoThirdPartySupportAgrYN, // 개인정보 제 3자 제공에 대한 동의
					PersonalInfoCollectUseAgrYN = sPersonalInfoCollectUseAgrYN		// 개인정보 수집 및 이용
				};

				ResultT memberApiResult = new RegistrationApiBiz().SimpleRegistrationPersonalBuyer(request);

				if (memberApiResult != null)
				{
					if (memberApiResult.ResultCode == RegistrationReturn.NoError
						|| memberApiResult.ResultCode == RegistrationReturn.SyncError
						|| memberApiResult.ResultCode == RegistrationReturn.SendJoinEmailError
						|| memberApiResult.ResultCode == RegistrationReturn.SendSMSError)
					{
						//가입성공
						return RedirectToAction("SignUpComplete", new { loginId = sLoginId, fromWhere = fromWhere });
					}
					else
					{
						sHtml = "<script type='text/javascript'>alert('" + memberApiResult.ResultDescription + "');history.back(-1);</script>";
						return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
					}
				}
				else
				{
					sHtml = "<script type='text/javascript'>alert('오류가 발생하였습니다. 다시 시도해주세요.');history.back(-1);</script>";
					return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
				}
			}
			else //사업자
			{
				//RegistrationCorpBuyerT
				var request = new RegistrationCorpBuyerT
				{
					CustNm = sBizCorpName,	// 상호명
					CorpIdNo = sBizNum,
					LoginId = sLoginId,
					CustPv01 = sAftEncPwd,
					GbankPd01 = sGbankPd01,
					Email = sEmail,
					Hp = sMobilePhoneNum,
					ERcvYn = sERcvYn,
					SmsRcvYn = sSmsRcvYn,
					JaehuId = GMobileWebContext.Current.JahuID,
					RegWhere = "M",
					PersonalInfoTreatAgrYN = sPersonalInfoTreatAgrYN,				// 개인정보 취급위탁 여부
					G9JoinYN = sG9JoinYN											// 구매회원 G9구매회원 가입 동의 여부
				};

				string CompleteUrl = Urls.MobileWebUrl + "/SignUp/SignUpComplete";

				var memberApiResult = new RegistrationApiBiz().RegistrationCorpBuyer(request);
				if (memberApiResult != null)
				{
					if (memberApiResult.ResultCode == RegistrationReturn.NoError
						|| memberApiResult.ResultCode == RegistrationReturn.SyncError
						|| memberApiResult.ResultCode == RegistrationReturn.SendJoinEmailError
						|| memberApiResult.ResultCode == RegistrationReturn.SendSMSError)
					{
						//가입성공
						return RedirectToAction("SignUpComplete", new { loginId = sLoginId, fromWhere = fromWhere });
					}
					else
					{
						sHtml = "<script type='text/javascript'>alert('" + memberApiResult.ResultDescription + "');history.back(-1);</script>";
						return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
					}
				}
				else
				{
					sHtml = "<script type='text/javascript'>alert('오류가 발생하였습니다. 다시 시도해주세요.');history.back(-1);</script>";
					return Content(sHtml, "text/html", System.Text.Encoding.UTF8);
				}
			}
		}

		public ActionResult SignUpComplete(string loginId, string fromWhere = "")
		{
			//if (!String.IsNullOrEmpty(fromWhere) && fromWhere.Trim().ToUpper().Equals("A"))
			//{
			//    string redirectUrl = String.Empty;
			//    string couponzoneUrl = Urls.MobileWebUrl + "/Couponzone";
			//    redirectUrl = Urls.MemberSignInRootSecure + "/mobile/SelfAuthGate/SelfAuthGate?returnUrl=" + couponzoneUrl;
			//    return new RedirectResult(redirectUrl);
			//}

			ViewBag.FromWhere = String.IsNullOrEmpty(fromWhere) ? String.Empty : fromWhere;

			//SSL 체크 
			if (!Request.IsSecureConnection)
			{
				return new RedirectResult(Request.Url.AbsoluteUri.Replace(Urls.MobileWebUrl.ToLower(), Urls.MobileWebUrlSecure.ToLower()));
			}

			//로그인 체크
			if (PageAttr.IsLogin)
			{
				return Redirect(Urls.MobileWebUrl);
			}
			else
			{
				ViewBag.loginId = loginId;
				return View();
			}	
		}

		#region 본인인증로그 복호화 유틸
		// 성별 코드 - 0:여성 / 1:남성
		public static string ConvertToGenderType(GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade.GenderType src)
		{
			switch (src)
			{
				case GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade.GenderType.Female: return "0";
				case GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade.GenderType.Male: return "1";
				default: return "0";
			}
		}

		public static string ConvertToNationalInfoType(GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade.NationalInfoType src)
		{
			switch (src)
			{
				case GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade.NationalInfoType.Local: return "0";
				case GMKT.IPin.CertificateAuthProxyV2.CertificateAuthServiceV2Facade.NationalInfoType.Foreigner: return "1";
				default: return "0";
			}
		}
		#endregion

		#region 아이디 Validation
		public RegistrationReturn ValidationId(string loginId)
		{
			if (RegistrationValidation.IsValidIDLength(loginId) == false)
			{
				return RegistrationReturn.InvalidIDLength;
			}

			if (RegistrationValidation.IsValidIDCharacter(loginId) == false)
			{
				return RegistrationReturn.InvalidIDCharacter;
			}

			if (RegistrationValidation.IsValidIDIncludeHpno(loginId) == false)
			{
				return RegistrationReturn.InvalidIDIncludeHpno;
			}

			return RegistrationReturn.NoError;
		}
		#endregion

		#region 비밀번호 Validation
		public RegistrationReturn ValidationPassword(string passwd)
		{
			if (RegistrationValidation.IsValidPasswordLength(passwd) == false)
			{
				return RegistrationReturn.InvalidPasswdCharacter;
			}

			if (RegistrationValidation.IsValidPasswordCharacter(passwd) == false)
			{
				return RegistrationReturn.InvalidPasswdCharacter;
			}

			return RegistrationReturn.NoError;
		}
		#endregion
	}
}

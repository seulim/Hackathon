using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Data.Member;
using GMKT.GMobile.Biz;
using GMKT.Component.Member;
using GMKT.Framework.Security;

namespace GMKT.GMobile.Web.Controllers
{
	public class CardRegistController : GMobileMemberControllerBase
    {
		[HttpPost]
		public JsonResult SaveOKCashbag(string cardNo, string cardPw)
		{
			string msg = string.Empty;
			int code = 0;

			//카드번호 인증
			OCBAuthResultM ocbAuth = new MemberApiBiz().GetOCBAuthInfo(cardNo, cardPw);

			if (ocbAuth != null)
			{
				if (ocbAuth.RetCode == "0")
				{
					if (false == string.IsNullOrEmpty(cardNo) && gmktUserProfile != null && false == string.IsNullOrEmpty(gmktUserProfile.CustNo) && false == string.IsNullOrEmpty(gmktUserProfile.LoginID))
					{
						try{
							int setResult = new MyInfoBiz().SetJaehuCardInfo(gmktUserProfile.CustNo, GMKTCryptoLibrary.AesCryptoGMKTEncrypt(cardNo), gmktUserProfile.LoginID, "O");

							if (setResult == 0)
							{
								code = 0;
								msg = "OK캐쉬백 카드가 등록되었습니다.";
							}
							else if (setResult == -3)
							{
								code = -3;
								msg = "동일한 OK캐쉬백 카드 번호에 등록할 수 있는 ID 개수 제한됩니다. ID 10개를 초과하였습니다. 다른 카드번호로 등록 가능합니다.";
							}else{
								code = -5;
								msg = "카드 등록에 실패했습니다.(" + ocbAuth.RetMsg + ")";
							}	
						}catch(Exception e){
							code = -6;
							msg = "카드 등록에 실패했습니다.";
						}
					}
					else
					{
						code = -2;
						msg = "정보가 올바르지 않습니다.";
					}
				}
				else
				{
					code = -1;
					msg = "카드번호 인증에 실패했습니다.(" + ocbAuth.RetMsg + ")";
				}
			}
			else
			{
				code = -4;
				msg = "카드번호 인증에 실패했습니다.";
			}

			return Json(new { Code = code, Msg = msg });
		}

		[HttpPost]
		public JsonResult SaveAsianaCard(string asianaCardNo)
		{
			int code = 0;
			string msg = string.Empty;

			if (false == string.IsNullOrEmpty(asianaCardNo) && asianaCardNo.Length == 9 &&
				gmktUserProfile != null &&
				false == string.IsNullOrEmpty(gmktUserProfile.CustNo) && false == string.IsNullOrEmpty(gmktUserProfile.LoginID))
			{
				int setResult = new MyInfoBiz().SetJaehuCardInfo(gmktUserProfile.CustNo, asianaCardNo, gmktUserProfile.LoginID, "A");

				if (setResult == 0)
				{
					code = 0;
					msg = "회원님의 아시아나클럽 회원정보가 정상적으로 저장되었습니다.";
				}
				else if (setResult == -3)
				{
					code = -3;
					msg = "동일한 아시아나클럽 회원번호에 등록할 수 있는 ID 개수 제한됩니다. ID 10개를 초과하였습니다. 다른 회원번호로 등록 가능합니다.";
				}
				else
				{
					code = -2;
					msg = "아시아나클럽 회원번호 등록에 실패했습니다.";
				}
			}
			else
			{
				code = -1;
				msg = "정보가 올바르지 않습니다.";
			}

			return Json(new { Code = code, Msg = msg });
		}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.Member;

namespace GMKT.GMobile.Biz
{
	public class MemberApiBiz
	{
		public OCBAuthResultM GetOCBAuthInfo(string cardno, string password)
		{
			OCBAuthResultM result = new OCBAuthResultM();

			ApiResponse<OCBAuthInfoM> ocbInfo = new MemberApiDac().GetOCBAuthInfo(cardno, password);
			if (ocbInfo != null)
			{
				if (ocbInfo.Data != null)
				{
					if (ocbInfo.Data.sRetCode == "0")
					{
						result.RetCode = "0";
						result.RetMsg = ocbInfo.Data.sRetMsg;
					}
					else
					{
						result.RetCode = ocbInfo.Data.sRetCode;
						result.RetMsg = ocbInfo.Data.sRetMsg;
					}
				}
			}
			return result;
		}

		public Boolean GetEbayGradeAgreeYN(string custno)
		{
			Boolean rtnAgree = false;
			ApiResponse<Boolean> gradeAgree = new MemberApiDac().GetEbayGradeAgreeYN(custno);
			if (gradeAgree != null){
				if (gradeAgree.ResultCode == 0){
					rtnAgree = gradeAgree.Data;
				}
			}
			return rtnAgree;
		}

		public string GetBenefitSharingReasonCode(string custno)
		{
			string result = String.Empty;

			if (String.IsNullOrEmpty(custno))
			{
				return result;
			}

			ApiResponse<String> response = new MemberApiDac().GetBenefitSharingReasonCode(custno);
			if (response != null)
			{
				if (response.ResultCode == 0)
				{
					result = response.Data;
				}
			}
			return result;
		}

		public CrossResultT SetEbayGradeAgree(string custno)
		{
			CrossResultT rsltAgree = new CrossResultT();
			ApiResponse<CrossResultT> gradeAgree = new MemberApiDac().SetEbayGradeAgree(custno);
			if (gradeAgree != null)
			{
				if (gradeAgree.Data != null)
				{
					rsltAgree = gradeAgree.Data;
				}
			}
			return rsltAgree;
		}	
			
	}
}

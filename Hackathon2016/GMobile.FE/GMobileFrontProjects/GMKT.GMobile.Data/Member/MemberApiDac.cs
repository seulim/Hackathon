using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data.Member
{
	public class MemberApiDac: ApiBase
	{
		public MemberApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<OCBAuthInfoM> GetOCBAuthInfo(string cardno, string passwd)
		{
			var result = ApiHelper.CallAPI<ApiResponse<OCBAuthInfoM>>(
				"GET",
				ApiHelper.MakeUrl("api/Member/GetOCBAuthInfo"),
				new
				{
					cardno = cardno,
					passwd = passwd
				}
			);

			return result;
		}

		public ApiResponse<Boolean> GetEbayGradeAgreeYN(string custno)
		{
			var result = ApiHelper.CallAPI<ApiResponse<Boolean>>(
				"GET",
				ApiHelper.MakeUrl("api/Member/GetEbayGradeAgreeYN"),
				new
				{
					custno = custno
				}
			);
			return result;
		}

		public ApiResponse<String> GetBenefitSharingReasonCode(string custno)
		{
			var result = ApiHelper.CallAPI<ApiResponse<String>>(
				"GET",
				ApiHelper.MakeUrl("api/Member/GetBenefitSharingReasonCode"),
				new
				{
					custno = custno
				}
			);
			return result;
		}

		public ApiResponse<CrossResultT> SetEbayGradeAgree(string custno)
		{
			ApiResponse<CrossResultT> result = ApiHelper.CallAPI<ApiResponse<CrossResultT>>(
				"GET",
				ApiHelper.MakeUrl("api/Member/GetEbayGradeAgreeResult"),
				new
				{
					custno = custno
				}
			);
			return result;
		}
	}
}

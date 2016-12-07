using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.Component.Member;

namespace GMKT.GMobile.Data.Member
{
	public class RegistrationApiDac : ApiBase
	{
		public RegistrationApiDac()	: base("GMApi")
		{
		}

		public ApiResponse<Boolean> GetExistsLoginId(string loginId)
		{
			ApiResponse<Boolean> result = ApiHelper.CallAPI<ApiResponse<Boolean>>(
				"GET",
				ApiHelper.MakeUrl("api/Registration/GetExistsLoginId"),
				new
				{
					loginid = loginId
				}
			);
			return result;
		}

		public ApiResponse<Boolean> GetExistsEmail(string email)
		{
			var result = ApiHelper.CallAPI<ApiResponse<Boolean>>(
				"GET",
				ApiHelper.MakeUrl("api/Registration/GetExistsEmail"),
				new
				{
					email = email
				}
			);

			return result;
		}

        public ApiResponse<CorpAuthResultT> GetCorpIdNoInfo(string corpIdNo)
		{
			// 본 호출 케이스 Object 타입
            var result = ApiHelper.CallAPI<ApiResponse<CorpAuthResultT>>(
				"GET",
				ApiHelper.MakeUrl("api/Registration/GetCorpIdNoInfo"),
				new
				{
					corpIdNo = corpIdNo		// 왼쪽 값은 API에 전달될 parameter 명 오른쪽 값은 위의 string loginId
				}
			);
			return result;
		}

		public ApiResponse<ResultT> GetVerificationRegistrationPersonal(string di, string custType, string birthDate)
		{
			var result = ApiHelper.CallAPI<ApiResponse<ResultT>>(
				"GET",
				ApiHelper.MakeUrl("api/Registration/GetVerificationRegistrationPersonal"),
				new
				{
					di = di,
					custType = custType,
					birthDate = birthDate
				}
			);

			return result;
		}

		public ApiResponse<ResultT> GetVerificationRegistrationCorp(string corpIdNo, string custType)
		{
			var result = ApiHelper.CallAPI<ApiResponse<ResultT>>(
				"GET",
				ApiHelper.MakeUrl("api/Registration/GetVerificationRegistrationCorp"),
				new
				{
					corpIdNo = corpIdNo,
					custType = custType
				}
			);

			return result;
		}

		#region 회원 가입 > 개인 구매자(PP)
		public ApiResponse<ResultT> RegistrationPersonalBuyer(RegistrationPersonalBuyerT request)
		{
			var result = ApiHelper.CallAPI<ApiResponse<ResultT>>(
				"POST",
				ApiHelper.MakeUrl("api/Registration/RegistrationPersonalBuyer"),
				request
			);

			return result;
		}
		#endregion

		#region 회원 가입 > 개인 구매자 간편가입(PP)
		public ApiResponse<ResultT> SimpleRegistrationPersonalBuyer(SimpleRegistrationPersonalBuyerT request)
		{
			var result = ApiHelper.CallAPI<ApiResponse<ResultT>>(
				"POST",
				ApiHelper.MakeUrl("api/Registration/SimpleRegistrationPersonalBuyer"),
				request
			);

			return result;
		}
		#endregion

		#region 사업자구매회원(PC) 가입
		public ApiResponse<ResultT> RegistrationCorpBuyer(RegistrationCorpBuyerT request)
		{
			var result = ApiHelper.CallAPI<ApiResponse<ResultT>>(
				"POST",
				ApiHelper.MakeUrl("api/Registration/RegistrationCorpBuyer"),
				request
			);

			return result;
		}
		#endregion


	}
}

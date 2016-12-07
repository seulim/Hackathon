using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.Member;
using GMKT.Component.Member;

namespace GMKT.GMobile.Biz.Member
{
	public class RegistrationApiBiz
	{
		public Boolean GetExistsLoginId(string loginId)
		{
			Boolean result = new Boolean();
			ApiResponse<Boolean> response = new RegistrationApiDac().GetExistsLoginId(loginId);
			if (response.ResultCode != 0)
				return false;

			if (response.Data)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public Boolean GetExistsEmail(string email)
		{
			Boolean result = new Boolean();
			ApiResponse<Boolean> response = new RegistrationApiDac().GetExistsEmail(email);
			if (response.ResultCode != 0)
				return false;

			if (response.Data)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

        public CorpAuthResultT GetCorpIdNoInfo(string corpIdNo)
		{
            CorpAuthResultT result = new CorpAuthResultT();
            corpIdNo = corpIdNo.Replace("-", "");
            ApiResponse<CorpAuthResultT> response = new RegistrationApiDac().GetCorpIdNoInfo(corpIdNo);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
                // 재사용 할 일 없을꺼 같아서 메서드 따로 안만듬.(MemberComponentBiz 호출을 위핸 Entity Converting)
                CorpBuyerAuthResultT authReqEntity = new CorpBuyerAuthResultT()
                {
                    ResultCode = response.Data.ResultCode,
                    ResultDescription = response.Data.ResultDescription,
                    RegistrationStatusCode = response.Data.RegistrationStatusCode,
                    CorpName = response.Data.CorpName,
                    RepName = response.Data.RepName,
                    Seq = response.Data.Seq
                };

                result = response.Data;
                if (!new RegistrationBiz().CorpBuyerAuthInfoValidation(authReqEntity, 5))
                {
                    result.CorpName = string.Empty;
                    result.RepName = string.Empty;
                }
				
			}
			return result;
		}

		public ResultT GetVerificationRegistrationPersonal(string di, string custType, string birthDate)
		{
			ResultT result = new ResultT();
			ApiResponse<ResultT> response = new RegistrationApiDac().GetVerificationRegistrationPersonal(di, custType,birthDate);
			if (response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}
			return result;
		}

		public ResultT GetVerificationRegistrationCorp(string corpIdNo, string custType)
		{
			ResultT result = new ResultT();
			ApiResponse<ResultT> response = new RegistrationApiDac().GetVerificationRegistrationCorp(corpIdNo, custType);
			if (response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}
			return result;
		}

		public ResultT RegistrationPersonalBuyer(RegistrationPersonalBuyerT request)
		{
			ResultT result = new ResultT();
			ApiResponse<ResultT> response = new RegistrationApiDac().RegistrationPersonalBuyer(request);
			if (response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}
			return result;

		}

		public ResultT SimpleRegistrationPersonalBuyer(SimpleRegistrationPersonalBuyerT request)
		{
			ResultT result = new ResultT();
			ApiResponse<ResultT> response = new RegistrationApiDac().SimpleRegistrationPersonalBuyer(request);
			if (response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}
			return result;

		}

		public ResultT RegistrationCorpBuyer(RegistrationCorpBuyerT request)
		{
			ResultT result = new ResultT();
			ApiResponse<ResultT> response = new RegistrationApiDac().RegistrationCorpBuyer(request);
			if (response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}
			return result;
		}
	}
}

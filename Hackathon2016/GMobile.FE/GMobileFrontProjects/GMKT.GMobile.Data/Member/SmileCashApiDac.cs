using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.GMobile.Util;


namespace GMKT.GMobile.Data.Member
{
	public class SmileCashApiDac : ApiBase
	{
		public SmileCashApiDac() : base("GMApi") { }


		public ApiResponse<SmileUserInfoResultT> GetCreateSmileMember(string custNo)
		{
			ApiResponse<SmileUserInfoResultT> result = ApiHelper.CallAPI<ApiResponse<SmileUserInfoResultT>>(
				"GET",
				ApiHelper.MakeUrl("api/SmileCash/GetCreateSmileMember"),
				new{
					custNo = custNo
				}
				//ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

		public ApiResponse<SmilePointBalanceT> PostSmilePointBalance()
		{
			ApiResponse<SmilePointBalanceT> result = ApiHelper.CallAPI<ApiResponse<SmilePointBalanceT>>(
				"POST",
				ApiHelper.MakeUrl("api/SmileCash/PostSmilePointBalance"),
				ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}
	}
}

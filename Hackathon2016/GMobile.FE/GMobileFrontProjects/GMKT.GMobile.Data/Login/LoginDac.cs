using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConnApi.Client;
using Newtonsoft.Json;

namespace GMKT.GMobile.Data
{
	public class LoginDac : ApiBase
	{
		public LoginDac()
			: base("GMApiSsl")
		{
		}

		public ApiResponse<CartPidResponse> GetBasketOrderInfo(string pid, string custNo)
		{
			ApiResponse<CartPidResponse> result = ApiHelper.CallAPI<ApiResponse<CartPidResponse>>(
				"GET",
				ApiHelper.MakeUrl("api/Login/GetEscrowCartPidInfo"),
				new
				{
					pid = pid,
					custNo = custNo
				}
			);
			return result;
		}

		public ApiResponse<LoginResponseT> PostLogin(LoginRequestT request)
		{
			ApiResponse<LoginResponseT> result = ApiHelper.CallAPI<ApiResponse<LoginResponseT>>(
				"POST",
				ApiHelper.MakeUrl("api/Login/PostLogin"),
				request
			);

			return result;
		}

		public ApiResponse<LoginResponseT> PostAutoLogin(LoginRequestT request)
		{
			string json = JsonConvert.SerializeObject(request);

			var result = ApiHelper.CallAPI<ApiResponse<LoginResponseT> >(
				"POST",
				ApiHelper.MakeUrl("api/Login/PostAutoLogin"),
				request
			);
						
			return result;
		}
	}
}

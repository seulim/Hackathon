using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data
{
	public class BasketDac : ApiBase
	{
		public BasketDac()
			: base("MEscrow")
		{
		}

		public ApiResponse<BasketOrderInfoT> GetBasketOrderInfo(string pid, string custNo)
		{
			ApiResponse<BasketOrderInfoT> result = ApiHelper.CallAPI<ApiResponse<BasketOrderInfoT>>(
				"POST",
				ApiHelper.MakeUrl("ko/cart/api/cartpid"),
				new
				{
					buyerNo = custNo,
					isLogin = true,
					cartPID = pid
				}
			);
			return result;
		}
	}
}

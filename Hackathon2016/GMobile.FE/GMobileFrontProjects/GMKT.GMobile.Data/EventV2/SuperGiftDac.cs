using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConnApi.Client;
using GMKT.GMobile.Util;


namespace GMKT.GMobile.Data.EventV2
{
	public class SuperGiftDac : ApiBase
	{
		public SuperGiftDac() : base( "GMApi" ) { }

		public ApiResponse<BuyHistoryT> GetMonthlyBuyHistory( string startDate, string endDate, string isGdLc, string isPlusBuy, string transportStat )
		{
			ApiResponse<BuyHistoryT> result = ApiHelper.CallAPI<ApiResponse<BuyHistoryT>>(
				"GET",
				ApiHelper.MakeUrl( "api/SuperGift/GetMonthlyBuyHistory" )
				, new
				{
					startDate = startDate
					,	endDate = endDate
					,	isGdLc = isGdLc
					, isPlusBuy = isPlusBuy
					, transportStat = transportStat
				},
				ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

		public ApiResponse<BuyHistoryT> GetMobileBuyHistory( string startDate, string endDate, string transportStat )
		{
			ApiResponse<BuyHistoryT> result = ApiHelper.CallAPI<ApiResponse<BuyHistoryT>>(
				"GET",
				ApiHelper.MakeUrl( "api/SuperGift/GetMobileBuyHistory" )
				, new
				{
					startDate = startDate
					,	endDate = endDate
					,	transportStat = transportStat
				},
				ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

		public ApiResponse<SuperGiftJsonEntityT> GetSuperGiftEntity()
		{
			ApiResponse<SuperGiftJsonEntityT> result = ApiHelper.CallAPI<ApiResponse<SuperGiftJsonEntityT>>(
				"GET",
				ApiHelper.MakeUrl( "api/SuperGift/GetAdminData" )
			);
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConnApi.Client;

namespace GMKT.GMobile.Data.EventV2
{
	public class CardPointDac : ApiBase
	{
		public CardPointDac() : base( "GMApi" ) { }

		public ApiResponse<CardBenefitJsonEntityT> GetCardBenefitEntity()
		{
			ApiResponse<CardBenefitJsonEntityT> result = ApiHelper.CallAPI<ApiResponse<CardBenefitJsonEntityT>>(
				"GET",
				ApiHelper.MakeUrl( "api/CardBenefit/GetAdminData" )
			);
			return result;
		}

		public ApiResponse<PointBenefitJsonEntityT> GetPointBenefitEntity()
		{
			ApiResponse<PointBenefitJsonEntityT> result = ApiHelper.CallAPI<ApiResponse<PointBenefitJsonEntityT>>(
				"GET",
				ApiHelper.MakeUrl( "api/PointBenefit/GetAdminData" )
			);
			return result;
		}
	}
}

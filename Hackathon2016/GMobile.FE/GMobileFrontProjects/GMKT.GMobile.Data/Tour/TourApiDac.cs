using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data
{
	public class TourApiDac : ApiBase
	{
		public TourApiDac() : base("GMApi")	{ }

		public ApiResponse<TourMain> GetTourItem(long middleGroupNo, long smallGroupNo, int pageNo, int pageSize, TourOrderEnum order)
		{
			ApiResponse<TourMain> result = ApiHelper.CallAPI<ApiResponse<TourMain>>(
				"GET",				
				ApiHelper.MakeUrl("api/Tour/GetTourItem"),
				new 
				{
					middleGroupNo = middleGroupNo,
					smallGroupNo = smallGroupNo,
					pageNo = pageNo,
					pageSize = pageSize,
					order = order
				}
			);

			return result;
		}

	}
}

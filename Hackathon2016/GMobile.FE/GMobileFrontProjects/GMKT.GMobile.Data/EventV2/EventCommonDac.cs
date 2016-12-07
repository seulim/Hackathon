using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConnApi.Client;
using GMKT.GMobile.Util;



namespace GMKT.GMobile.Data.EventV2
{
	public class EventCommonDac : ApiBase
	{
		public EventCommonDac() : base( "GMApi" ) { }

		public ApiResponse<List<CommonBannerT>> GetCommonBanner( string eventManageType, string exposeTargetType )
		{
			ApiResponse<List<CommonBannerT>> result = ApiHelper.CallAPI<ApiResponse<List<CommonBannerT>>>
			(
				"GET",
				ApiHelper.MakeUrl( "api/EventCommon/GetEventCommonBanner" )
				, new
				{
					eventManageType = eventManageType
					,	exposeTargetType = exposeTargetType
				}
			);
			return result;
		}

        public ApiResponse<List<NavigationIconT>> GetNavigationIcons()
        {
            ApiResponse<List<NavigationIconT>> result = ApiHelper.CallAPI<ApiResponse<List<NavigationIconT>>>
            (
                "GET",
                ApiHelper.MakeUrl("api/Pluszone/GetNavigationIcons")
            );
            return result;
		}
        

		public ApiResponse<int> GetCountOfAppliedEids( DateTime timestamp, string custNo, params int[] eids )
		{
			ApiResponse<int> result = null;
			if( String.IsNullOrEmpty(custNo))
			{
				result = ApiHelper.CallAPI<ApiResponse<int>>(
					"GET",
					ApiHelper.MakeUrl( "api/EventCommon/GetCountOfAppliedEids" )
					, new
					{
						timestamp = timestamp.ToShortDateString()
						,	eid1 = eids.ElementAtOrDefault( 0 )
						,	eid2 = eids.ElementAtOrDefault( 1 )
						,	eid3 = eids.ElementAtOrDefault( 2 )
						,	eid4 = eids.ElementAtOrDefault( 3 )
					},
					ConnApiUtil.GetUserInfoCookieParameter()
				);
			}
			else
			{
				result = ApiHelper.CallAPI<ApiResponse<int>>(
					"GET",
					ApiHelper.MakeUrl( "api/EventCommon/GetCountOfAppliedEids" )
					, new
					{
						timestamp = timestamp
						,	eid1 = eids.ElementAtOrDefault( 0 )
						,	eid2 = eids.ElementAtOrDefault( 1 )
						,	eid3 = eids.ElementAtOrDefault( 2 )
						,	eid4 = eids.ElementAtOrDefault( 3 )
						, custNo = custNo
					}
				);
			}
			return result;
		}
	}
}

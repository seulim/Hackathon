using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;
using GMKT.Framework.EnterpriseServices;
using GMKT.MobileCache;

namespace GMKT.GMobile.Biz.EventV2
{
    public class EventCommonBiz
	{
		public List<CommonBannerT> GetCommonTopBanner( string eventManageType, string exposeTargetType )
		{
			ApiResponse<List<CommonBannerT>> response = new EventCommonDac().GetCommonBanner( eventManageType, exposeTargetType );
			if( response != null )
			{
				return response.Data;
			}
			else
			{
				return new List<CommonBannerT>();
			}
		}

        public List<NavigationIconT> GetNavigationIcons()
        {
            ApiResponse<List<NavigationIconT>> response = new EventCommonDac().GetNavigationIcons();
            if (response != null)
            {
                return response.Data;
            }
            else
            {
                return new List<NavigationIconT>();
            }
        }

		/// <summary>
		/// eid 4개 까지만 확인 가능합니다.
		/// </summary>
		public int GetCountOfAppliedEids( DateTime timestamp, string custNo = "", params int[] eids )
		{
			ApiResponse<int> response = new EventCommonDac().GetCountOfAppliedEids( timestamp, custNo, eids );
			if( response != null )
			{
				return response.Data;
			}
			else
			{
				return -1;
			}
		}
	}
}

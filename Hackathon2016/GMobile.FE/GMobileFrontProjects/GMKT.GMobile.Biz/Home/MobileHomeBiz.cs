using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
    public class MobileHomeBiz
    {
        public ApiResponse<HomeTotalList> GetMobileHomeTotalList(string siteCode = "")
        {
            return new MobileHomeApiDac().GetMobileHomeTotalList(siteCode);
        }

		public ApiResponse<HomeTotalListV2> GetMobileHomeTotalListV2(string userInfo, string siteCode = "")
		{
			return new MobileHomeApiDac().GetMobileHomeTotalListV2(userInfo, siteCode);
		}

		#region [Pilot - gsohn] Pilot Test 코드 (테스트 완료후 삭제)
		public ApiResponse<HomeTotalListV2> GetMobileHomeTotalListV2Pilot(string userInfo, string siteCode = "", string jaehuid = "")
		{
			return new MobileHomeApiDac().GetMobileHomeTotalListV2Pilot(userInfo, siteCode, jaehuid);
		}
		#endregion

		public HomeMainGroup GetHomeListingDABanner(string exposeArea)
		{
			ApiResponse<HomeMainGroup> response = new MobileHomeApiDac().GetHomeListingDABanner(exposeArea);
			if(response != null && response.Data != null)
			{
				return response.Data;
			}

			return null;
		}

        //public ApiResponse<MobileDrawerTotal> GetMobileDrawerTotal()
        //{
        //    return new MobileHomeApiDac().GetMobileDrawerTotal();
        //}
    }
}

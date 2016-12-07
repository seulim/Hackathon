using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
    public class MobilePlanBiz
    {
        public ApiResponse<MobileShopPlanT> GetMobilePlanDetail(int sid)
        {
            return new MobilePlanApiDac().GetMobilePlanDetail(sid);
        }

        public ApiResponse<MobilePlanViewT> GetMobilePlanList(string groupCd, int pageNo, int iRowCount)
        {
            return new MobilePlanApiDac().GetMobilePlanList(groupCd, pageNo, iRowCount);
        }
    }
}

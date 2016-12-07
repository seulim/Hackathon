using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
    public class PluszoneNewBiz
    {
        public ApiResponse<AttendanceCheckTotal> GetAttendanceCheckTotalList()
        {
            return new PlusZoneApiDac().GetAttendanceCheckTotalList();
        }

        public ApiResponse<List<MobileShopPlan>> GetMobileShopPlan()
        {
            return new PlusZoneApiDac().GetMobileShopPlan();
        }
    }
}

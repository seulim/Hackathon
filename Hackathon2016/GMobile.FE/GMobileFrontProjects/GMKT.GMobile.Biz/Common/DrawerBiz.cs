using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
    public class DrawerBiz
    {
        public ApiResponse<MobileDrawerTotal> GetMobileDrawerTotal()
        {
            return new DrawerApiDac().GetMobileDrawerTotal();
        }
    }
}

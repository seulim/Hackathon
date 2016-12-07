using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.Ad;
using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Util;


namespace GMKT.GMobile.Biz
{
    public class AdApiBiz
    {
        public FooterDaData GetSrpLpFooterDa(string primeKeyword, string categoryCode)
        {
            FooterDaData result = new FooterDaData();
            ApiResponse<FooterDaData> response = new AdApiDac().GetSrpLpFooterDa(primeKeyword, categoryCode);
            if (response != null && response.Data != null)
            {
                result = response.Data;
            }

            return result;
        }
    }
}

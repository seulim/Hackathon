using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using System.Data;

namespace GMKT.GMobile.Data
{
    public class PartnerDac : MicroDacBase
    {
        /// <summary>
        /// LP/SRP - 프리미어 파트너 리스트
        /// </summary>
        /// <param name="pp_seller_no">프리미어 파트너 번호?</param>
        /// <returns>프리미어 파트너 리스트</returns>
        public List<PartnerT> SelectPartner(string pp_seller_no)
        {
            return MicroDacHelper.SelectMultipleEntities<PartnerT>(
                "display_read"
                , "dbo.up_gmkt_admin_get_pp"
                , MicroDacHelper.CreateParameter("@pp_seller_no", pp_seller_no, SqlDbType.VarChar)
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using System.Data;
using GMKT.Framework.Data;

namespace GMKT.GMobile.Data
{
    public class JaehuIdDac : MicroDacBase
    {
		public List<JaehuT> SelectJaehuList()
		{
			return MicroDacHelper.SelectMultipleEntities<JaehuT>(
				"tiger_read",
				"dbo.up_gmkt_admin_search_permit_benefit_contract_code_mob"
			);
		}
    }
}

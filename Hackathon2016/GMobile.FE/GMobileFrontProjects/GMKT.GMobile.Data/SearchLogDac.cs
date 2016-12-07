using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using System.Data;
using GMKT.Framework.Data;

namespace GMKT.GMobile.Data
{
	public class SearchLogDac : MicroDacBase
	{
		public long SetKeywordLog(string sKeyword, string regWhere, string sSearchSiteType, string sInnerSearchYn)
		{
			return MicroDacHelper.SelectScalar<long>(
				"keyword_write",
				"dbo.up_gmkt_front_common_get_keyword_seq",
				MicroDacHelper.CreateParameter("@keyword", sKeyword, SqlDbType.VarChar, 100),
				MicroDacHelper.CreateParameter("@reg_where", regWhere, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@search_site_type", sSearchSiteType, SqlDbType.Char, 1),
				MicroDacHelper.CreateParameter("@inner_search_yn", sInnerSearchYn, SqlDbType.Char, 1)
			);
			
		}
	}
}

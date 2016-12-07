using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using GMKT.Framework.EnterpriseServices;
using GMKT.Framework.Cache;


namespace GMKT.GMobile.Data
{
	public class SearchKeywordRedirectDac : MicroDacBase
	{
		public List<SearchKeywordRedirectT> SelectSearchKeywordRedirect()
		{
			return MicroDacHelper.SelectMultipleEntities<SearchKeywordRedirectT>(
				"searchdb_read",
				"dbo.up_srp_search_keyword_getList",
				MicroDacHelper.CreateParameter("@mobile_yn", "Y", SqlDbType.Char, 1)
			);
		}
	}
}

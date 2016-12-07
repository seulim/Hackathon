using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheFx.EnterpriseServices;

using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;
namespace GMKT.GMobile.Biz
{
	public class SearchLogBiz : BizBase
	{
		[Transaction(TransactionOption.NotSupported)]
		public long SetKeywordInfo(string sKeyword, string regWhere, string sSearchSiteType, string sInnerSearchYn)
		{
			return new SearchLogDac().SetKeywordLog(sKeyword, regWhere, sSearchSiteType, sInnerSearchYn);
		}
	}
}

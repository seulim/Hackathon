using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArcheFx.EnterpriseServices;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;
using System.Diagnostics;
using System.Collections;
// tolhm cache test
using GMKT.Framework.Cache;

namespace GMKT.GMobile.Biz
{
	public class SearchKeywordRedirectBiz : BizBase
	{
		private static SearchKeywordRedirectPool SearchKeywordRedirectPool
		{
			get { return SearchKeywordRedirectPool.GetPool(); }
		}

		public static Hashtable GetSearchKeywordRedirectTable()
		{
			return SearchKeywordRedirectPool.GetSearchKeywordRedirectTable();
		}

		public string GetSearchKeywordRedirectUrl(string keyword)
		{
			string redirectUrl = "";

			string trimedKeyword = keyword.ToLower().Replace(" ", "");
			Hashtable redirectTable = SearchKeywordRedirectBiz.GetSearchKeywordRedirectTable();
			if (redirectTable != null && redirectTable.Count > 0 && redirectTable.ContainsKey(trimedKeyword))
			{
				redirectUrl = (string)redirectTable[trimedKeyword];
			}

			if (!String.IsNullOrEmpty(redirectUrl) && !redirectUrl.StartsWith("http://"))
			{
				redirectUrl = "http://" + redirectUrl;
			}

			if(redirectUrl.CompareTo("http://") == 0)
			{
				redirectUrl = "";
			}

			return redirectUrl;
		}

		[Transaction(TransactionOption.NotSupported)]
		public List<SearchKeywordRedirectT> GetSearchKeywordRedirectListFromDB()
		{
			return new SearchKeywordRedirectDac().SelectSearchKeywordRedirect();
		}

	}
}

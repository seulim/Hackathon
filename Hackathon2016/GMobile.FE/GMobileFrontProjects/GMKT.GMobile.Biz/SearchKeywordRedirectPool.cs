using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using GMKT.GMobile.Data;
using System.Diagnostics;
using System.Collections;

namespace GMKT.GMobile.Biz
{
	internal class SearchKeywordRedirectPool
	{

		#region [ Member Variables ]
		private static SearchKeywordRedirectPool searchKeywordPoll;
		private static TimeSpan updateTerm = TimeSpan.FromMinutes(30d);
		private static Timer updateTimer;

		// data
		private Hashtable searchKeywordRedirectTable = null;
		#endregion

		#region [ Constructors ]
		private SearchKeywordRedirectPool() { }

		private SearchKeywordRedirectPool(Hashtable redirectTable)
		{
			this.searchKeywordRedirectTable = redirectTable;
		}
		#endregion

		#region [ Public Functions ]
		public Hashtable GetSearchKeywordRedirectTable()
		{
			if (searchKeywordRedirectTable != null && searchKeywordRedirectTable.Count > 0)
			{
				return searchKeywordRedirectTable;
			}
			else
			{
				return new Hashtable();
			}
		}
		#endregion

		#region [ Functions for Update Pool Thread ]
		internal static SearchKeywordRedirectPool GetPool()
		{
			if (searchKeywordPoll != null) return searchKeywordPoll;

			lock (typeof(SearchKeywordRedirectPool))
			{
				if (searchKeywordPoll != null) return searchKeywordPoll;

				Thread thread = new Thread(UpdatePool);
				thread.Name = "SearchKeywordRedirectPoolThread";
				thread.Start();
				thread.Join();
			}

			return searchKeywordPoll;
		}

		private static void UpdatePool(object param)
		{
			if (updateTimer != null)
			{
				updateTimer.Dispose();
				updateTimer = null;
			}

			try
			{
				searchKeywordPoll = GetPoolInternal();
			}
			catch { }

			updateTimer = new Timer(new TimerCallback(UpdatePool), null, updateTerm, TimeSpan.FromMilliseconds(-1));
		}

		private static SearchKeywordRedirectPool GetPoolInternal()
		{
			List<SearchKeywordRedirectT> dbList = new SearchKeywordRedirectBiz().GetSearchKeywordRedirectListFromDB();
			Hashtable redirectTable = new Hashtable();

			if (dbList != null && dbList.Count > 0)
			{
				foreach (SearchKeywordRedirectT info in dbList)
				{
					string redirectURL = "";

					redirectURL = info.MobileLinkURL.Trim();

					if (!String.IsNullOrEmpty(info.Keyword) && !redirectTable.ContainsKey(info.Keyword.ToLower().Replace(" ", "")))
					{
						redirectTable.Add(info.Keyword.ToLower().Replace(" ", ""), redirectURL);
					}

					if (!String.IsNullOrEmpty(info.RelKeyword) && info.RelKeyword.Contains('|'))
					{
						string[] relKeyword;
						relKeyword = info.RelKeyword.Split('|');
						foreach (string rk in relKeyword)
						{
							if (!String.IsNullOrEmpty(rk) && !redirectTable.ContainsKey(rk.ToLower().Replace(" ", "")))
							{
								redirectTable.Add(rk.ToLower().Replace(" ", ""), redirectURL);
							}
						}
					}
				}
			}

			return new SearchKeywordRedirectPool(redirectTable);
		}
		#endregion
	}
}

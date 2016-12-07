using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;

using GMKT.GMobile.Constant;
using System.Text.RegularExpressions;

namespace GMKT.GMobile.Biz
{
	public class SuggestionKeywordBiz
	{
		public SuggestionKeywordT GetSuggestionKeyword(string keyword)
		{
			if (null == keyword || 0 == keyword.Length) return null;

			//Dictionary<string, SuggestionKeywordT> dicGoodsList = new Dictionary<string, SuggestionKeywordT>();
			//StringBuilder sbKeywords = new StringBuilder();
			try
			{
                FrontApiWebClient webClient = new FrontApiWebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;

				//string strUrl = string.Concat(Const.AUTOCOMPLETE_SERVER_URL, "?p=1&q=", HttpUtility.UrlEncode(keyword, Encoding.GetEncoding(949)));
				//string strUrl = string.Concat(Const.AUTOCOMPLETE_SERVER_URL, "?p=1&q=", keyword);
				//string strUrl = @"http://searchservicedev.gmarket.co.kr/challenge/neo_search/auto_complete_temp.asp?p=1&q=" + HttpUtility.UrlEncode(keyword, Encoding.GetEncoding(949));
                string strUrl = "http://frontapi.gmarket.co.kr/autocompleteV2/kr/mobile/" + keyword;


				string htmlString = webClient.DownloadString(strUrl);
				string strResult = Regex.Replace(htmlString, @"<(.|\n)*?>", string.Empty);

				SuggestionKeywordT result = new SuggestionKeywordT();

				string[] saResult = strResult.Split(';');
				foreach (string item in saResult)
				{
					string[] saRow = item.Split('=');
					if (2 != saRow.Length) continue;

					string[] saPreName = saRow[0].Trim().Split(' ', '\t');
					string name = saPreName[saPreName.Length - 1];

					switch (name)
					{
						case "qs_qc":
							result.changeKeyword = saRow[1].Replace('\'', ' ').Trim();
							break;
						case "qs_q":
							result.originalKeyword = saRow[1].Replace('\'', ' ').Trim();
							break;
						case "qs_m":
							result.positionKeyword = saRow[1].Replace('\'', ' ').Trim();
							break;
						case "qs_ac_id":
							result.resultKeyword = saRow[1].Replace('\'', ' ').Trim();
							break;
						case "qs_ac_list":
							string qs_ac_list = saRow[1];
							qs_ac_list = qs_ac_list.Substring(qs_ac_list.IndexOf('(') + 1,
																									qs_ac_list.IndexOf(')') - qs_ac_list.IndexOf('(') - 1);
							string[] saQs_ac = qs_ac_list.Split(',');

							result.suggestKeywordList = new List<string>();

							foreach (string quoted in saQs_ac)
							{
								string qs_ac = quoted.Replace('\'', ' ').Trim();
								if ("" == qs_ac) continue;

								result.suggestKeywordList.Add(qs_ac);
							}
							break;

						default:
							break;
					}
				}

				return result;
			}
			catch (Exception ex)
			{
                ArcheFx.Diagnostics.Trace.WriteError(ex);
				return new SuggestionKeywordT();
			}
		}
	}

    public class FrontApiWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            request.Timeout = 500;
            return request;
        }
    }
}

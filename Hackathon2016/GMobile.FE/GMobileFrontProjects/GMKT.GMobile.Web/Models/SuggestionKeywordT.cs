using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMKT.GMobile.Web.Models
{
	public class SuggestionKeywordT
	{
		public string changeKeyword { get; set; }
		public string originalKeyword { get; set; }
		public string positionKeyword { get; set; }
		public string resultKeyword { get; set; }
		public List<string> suggestKeywordList { get; set; }
	}
}
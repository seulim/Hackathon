using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMKT.GMobile.Web.Models
{
	public class SuggestKeywordModel
	{
		public string Keyword { get; set; }
		public List<string> SuggestKeywordList { get; set; } 
	}
}
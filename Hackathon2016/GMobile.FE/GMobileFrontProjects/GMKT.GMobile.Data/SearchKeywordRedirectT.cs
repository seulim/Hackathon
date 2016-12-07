using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class SearchKeywordRedirectT
	{
		[Column("KEYWORD")]
		public string Keyword { get; set; }

		[Column("REL_KEYWORD")]
		public string RelKeyword { get; set; }

		[Column("MOBILE_LNK_URL")]
		public string MobileLinkURL { get; set; }
	}
}

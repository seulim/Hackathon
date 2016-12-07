using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GMKT.GMobile.Web.Models
{
    public class ShareAndShortcutModel
    {
		public string SchemeName { get; set; }
		public string ShareFcdCode { get; set; }
		public string ShortcutFcdCode { get; set; }
		public string TargetUrl { get; set; }
		public string Title { get; set; }
    }
}

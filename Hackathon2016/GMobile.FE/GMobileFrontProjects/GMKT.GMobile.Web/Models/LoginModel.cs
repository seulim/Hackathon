using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using GMobile.Data.DisplayDB;

namespace GMKT.GMobile.Web.Models
{
	public partial class LoginModel
	{
		public bool g_isMobileBrowser { get; set; }
		public string sAdultUseLoinCheck { get; set; }
		public string sTargetUrl { get; set; }
		public string sEcpGdMc { get; set; }
		public int failCheck { get; set; }
	}

	public partial class LoginM
	{
		public bool IsMobileBrowser { get; set; }
		public bool IsAdultLogin { get; set; }
		public bool IsFailLogin { get; set; }
		public bool needMerge { get; set; }

		public string ReturnUrl { get; set; }
		public string FromWhere { get; set; }
	}

	public partial class LoginSFCM
	{
		public bool IsFailLogin { get; set; }
		public bool IsNotSFCMember { get; set; }

		public string ReturnUrl { get; set; }
	}
}
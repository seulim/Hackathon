using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
	public class PushAgreementResultT
	{
		public bool IsChangeAgreementYn { get; set; }
		public int sRetCode { get; set; }
		public string sMessage { get; set; }
	}

	public class PushAgreementInfoT
	{
		public string CustNo { get; set; }
		public int AppNo { get; set; }
		public string ServiceAgreeYn { get; set; }
	}
}

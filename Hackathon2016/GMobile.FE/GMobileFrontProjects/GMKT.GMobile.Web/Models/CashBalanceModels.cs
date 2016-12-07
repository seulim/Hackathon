using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.Web.Models;

namespace GMKT.GMobile.Web.Models
{
	#region [======= 2014 선물권리뉴얼 =======]

	#region [선물권 인증확인]
	public class GiftCardAuthCheckResultM
	{
		public int RetCode { get; set; }
		public string RetReason { get; set; }
		public string GiftChnl { get; set; }
		public int TokenSeq { get; set; }
		public string AUth7Code { get; set; }
		public decimal TokenMoney { get; set; }
	}
	#endregion [선물권 인증확인]
	// 인증코드 재발송 확인
	public class ReSendAuthCodeM
	{
		public string RetCode { get; set; }
		public string RetReason { get; set; }
	}

	#endregion [======= 2014 선물권리뉴얼 =======]
}

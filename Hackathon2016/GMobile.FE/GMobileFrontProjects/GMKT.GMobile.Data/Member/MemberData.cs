using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data.Member
{
	public class SmileApiReturnBase
	{
		public string ReturnCode { get; set; }
		public string ReturnValue { get; set; }
		public string ErrorMessage { get; set; }
		public SmileApiReturnBase()
		{
			ReturnCode = string.Empty;
			ReturnValue = string.Empty;
			ErrorMessage = string.Empty;
		}
	}

	public partial class SmileUserInfoResultT : SmileApiReturnBase
	{
		//public string ReturnCode { get; set; } //0000이면 성공
		//public string ReturnValue { get; set; }
		//public string ErrorMessage { get; set; }
		public string UserKey { get; set; }
		public int UserStatus { get; set; } //1: 등록, 2: 탈퇴, 3: 회원가입 안함, 4: 회원 미매칭
	}

	public class SmileApiRequest
	{
		public string UserKey { get; set; }
	}

	public class SmilePointBalanceT : SmileApiReturnBase
	{
		public int Balance { get; set; }
		public int ExpirableBalance { get; set; }
	}

	public class OCBAuthInfoM
	{
		public string sRetCode { get; set; }
		public string sRetMsg { get; set; }
		public string sUsableOkCashBagPoint { get; set; }
	}

	public class OCBAuthResultM
	{
		public string RetCode { get; set; }
		public string RetMsg { get; set; }
	}

	public class RegistAjaxM
	{
		public bool Success { get; set; }
	}

}

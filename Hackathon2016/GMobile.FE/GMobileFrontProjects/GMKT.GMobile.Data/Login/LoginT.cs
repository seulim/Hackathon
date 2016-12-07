using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public class LoginRequestT
	{
		public ParameterMember parameterMember;
		public CookieMember cookieMember;
		public ServerVariableMember serverVariableMember;

		public LoginRequestT()
		{
			parameterMember = new ParameterMember();
			cookieMember = new CookieMember();
			serverVariableMember = new ServerVariableMember();
		}

		public class ParameterMember
		{
			public string strLoginId = "";
			public string strPasswd = "";
			public bool isAutoLogin = false;
			public string clientIp = "";
			public string jaehuId = "";
		}

		public class CookieMember
		{
			public string pcidx = "";
			public string mgpid = "";
			public UserInfoT user_info;
			public string pcuid = "";
			public string order_info__pbid = "";

			public CookieMember()
			{
				user_info = new UserInfoT();
			}
		}

		public class ServerVariableMember
		{
			public string httpClientIp = "";
			public string httpXForwardedFor = "";
			public string remoteAddr = "";
			public string userAgent = "";
			public string httpReferrer = "";
		}
	}

	public class LoginResponseT
	{
		public ReturnMember returnMember { get; set; }
		public GlobalMember globalMember { get; set; }
		public CookieMember cookieMember { get; set; }

		public LoginResponseT()
		{
			returnMember = new ReturnMember();
			globalMember = new GlobalMember();
			cookieMember = new CookieMember();
		}

		public class ReturnMember
		{
			public int resultCode { get; set; }
		}

		public class GlobalMember
		{
			public string g_memberWay { get; set; }
			public string g_custNo { get; set; }
		}

		public class CookieMember
		{
			public string mgpid { get; set; }
			public UserInfoT user_info { get; set; }
			public string PCIDJCN { get; set; }

			public CookieMember()
			{
				user_info = new UserInfoT();
			}
		}
	}

	public class UserInfoT
	{
		public int CouponCnt { get; set; }
		public int PackCnt { get; set; }
		public string Pif { get; set; }
		public string Sif { get; set; }
		public string time { get; set; }
		public string isMember { get; set; }
		public string isDonatee { get; set; }
		public string isZeroMargin { get; set; }
		public string isEmailValid { get; set; }
		public string isAdultUse { get; set; }
		public string adultUseLoinCheck { get; set; }
		public string adultContentAuthYN { get; set; }
		public string custType { get; set; }
		public string ageGroup { get; set; }
		public string jaehuCustNo { get; set; }
		public string safe_login { get; set; }
		public string CR_Type { get; set; }
		public string corpIdNo { get; set; }
	}

	public class BasketOrderInfoT : ApiResponseBase
    {
		[Column("RET_CODE")]
		public string RetCode { get; set; }

		[Column("P_BID")]
		public string PBid { get; set; }
    }

	public class CartPidResponse
	{
		public int result { get; set; }
		public string data { get; set; }
	}

	#region Sub Entities

	#endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;
using GMKT.GMobile.Util;
using System.Web;
using System.Net;
using System.IO;
using GMKT.Web;
using System.Text.RegularExpressions;
using GMKT.Web.Context;
using GMKT.Framework.Security;
using System.Collections.Specialized;
using RestSharp;
using ConnApi.Client;

namespace GMKT.GMobile.Biz
{
	public class LoginApiBiz
	{
		public LoginApiResponse<PostLoginResponseT> PostLogin(string id, string password, bool isAutoLogin, string ipAddress, string httpRefererUrl, NameValueCollection cookies, string siteCode = "", bool needMerge = false)
		{
			List<CookieParameter> cookieList = new List<CookieParameter>();

			for (int i = 0; i < cookies.Count; i++)
			{
				cookieList.Add(new CookieParameter(cookies.GetKey(i), cookies.Get(i)));
			}

			return new LoginApiDac().PostLogin(id, password, isAutoLogin, ipAddress, httpRefererUrl, cookieList.ToArray(), siteCode, needMerge);
		}

		public LoginApiResponse<PostLoginResponseT> PostNonMemberLogin(string name, string password, string telNo, NameValueCollection cookies)
		{
			List<CookieParameter> cookieList = new List<CookieParameter>();
			for (int i = 0; i < cookies.Count; i++)
			{
				cookieList.Add(new CookieParameter(cookies.GetKey(i), cookies.Get(i)));
			}

			return new LoginApiDac().PostNonMemberLogin( name, password, telNo, cookieList.ToArray());
		}

		public LoginApiResponse<PostLoginResponseT> PostNonMemberOrderLogin(NameValueCollection cookies)
		{
			List<CookieParameter> cookieList = new List<CookieParameter>();
			for(int i = 0; i < cookies.Count; i++)
			{
				cookieList.Add(new CookieParameter(cookies.GetKey(i), cookies.Get(i)));
			}

			return new LoginApiDac().PostNonMemberOrderLogin(cookieList.ToArray());
		}

		public AutoLoginToken GetToken(string id, string pw, string pcid)
		{
			return new LoginApiDac().GetAutoLoginToken(id, pw, pcid).Data;
		}

		public LoginApiResponse<NonMemberOrderPasswordResetResult> ResetNonMemberOrderPassword(string custName, string phoneNo, string packNo, string sendMethod, string ipAddress)
		{
			return new LoginApiDac().ResetNonMemberOrderPassword(custName, phoneNo, packNo, sendMethod, ipAddress);
		}
	}
}

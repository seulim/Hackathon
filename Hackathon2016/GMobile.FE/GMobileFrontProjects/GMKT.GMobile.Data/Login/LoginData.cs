using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ConnApi.Client;
using Newtonsoft.Json;
using RestSharp;

namespace GMKT.GMobile.Data
{
	public class PostLoginResponseT
	{
		public string LandingUrl { get; set; }
		public bool IsAdult { get; set; }
		public bool DisableAutoLogin { get; set; }
	}
	/*
	public class LoginApiResponse<T> : RestResponseBase, IRestResponse<T>
	{
		public T Data { get; set; }
	}
	*/
	public class LoginApiResponse<T>
	{
		public HttpCookieCollection Cookies { get; set; }

		public int ResultCode { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }
	}

	public class AutoLoginToken
	{
		public String Token { get; set; }
	}

	public class NonMemberOrderPasswordResetResult
	{
		public int ResultCode { get; set; }
		public int NonMemResultCode { get; set; }
		public string ResultReason { get; set; }
		public string ResultDescription { get; set; }
	}
}

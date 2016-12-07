using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PetaPoco;
using ConnApi.Client;
using Newtonsoft.Json;
using RestSharp;


namespace GMKT.GMobile.Data.Sns
{
	class KaKaoStoryAPIEntityT
	{
	}
	
	public partial class SnsKaKaoInfo
	{
		public SnsServiceKind snsServiceKind { get; set; }
		public string CustNo { get; set; }
		public string LoginID { get; set; }
		public string SnsID { get; set; }
		public string SnsUserName { get; set; }
		public string SnsProfileImage { get; set; }
		public string SnsProfileBio { get; set; }
		public string SnsToken { get; set; }
		public string SnsTokenSecret { get; set; }
		public string AppName { get; set; }
	}

	public class KaKaoApiResponse<T>
	{
		public HttpCookieCollection Cookies { get; set; }

		public int ResultCode { get; set; }
		public string Message { get; set; }
		public T Data { get; set; }
	}

	public enum SnsServiceKind
	{
		UNKNOWN,
		TWITTER,
		FACEBOOK,
		YOZM,
		ME2DAY,
		CLOG,
		NATE,
		MINIHOMPI,
		KAKAOSTORY
	}

	public class SnsKaKaoStoryT
	{
		public string Title { get; set; }
		public string Message { get; set; }
		public string Image { get; set; }
		public string RequestUrl { get; set; }
		public string LinkUrl { get; set; }
		public string LandingUrl { get; set; }		
		public string Description { get; set; }
		public string Host { get; set; }
		public string PostingText { get; set; }
	}

	public class ContentType
	{
		public const string multi = "multipart/form-data; boundary=";
		public const string content = "application/x-www-form-urlencoded;charset=utf-8";
		public const string nothing = "";
	}
}

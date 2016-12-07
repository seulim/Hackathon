using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Util;
using GMKT.GMobile.Biz;
using System.Collections;
using GMKT.Web.Context;
using GMKT.Framework.Security;
using GMKT.GMobile.Data;
using System.Collections.Specialized;
using GMKT.Component.Member;
using GMKT.GMobile.Filter;
using GMKT.GMobile.App;



namespace GMKT.GMobile.Web.Controllers
{
	public class LoginController : GMobileControllerBase
	{
		private int LOGIN_EXPIRE_TIME_MINUTES = 120;
		protected static readonly List<string> MOBILE_USER_AGENT_LIST = new List<string>()
		{
			"lgtelecom",
			"NATEBrowser",
			"LG-CT810",
			"LG/BL40",
			"POLARIS",
			"SAMSUNG",
			"iphone",
			"ipod",
			"IEMobile",
			"Opera Mobi",
			"OperaMini",
			"Windows CE",
			"blackberry",
			"symbian",
			"nokia",
			"HTC",
			"Mobile Safari",
			"Opera Mini"
		};

		protected bool IsMobileBrowser(string userAgent)
		{
			if (false == string.IsNullOrEmpty(userAgent))
			{
				foreach (string eachAgent in MOBILE_USER_AGENT_LIST)
				{
					if (userAgent.ToLower().IndexOf(eachAgent.ToLower()) > -1)
						return true;
				}
			}
			
			return false;
		}

		protected void SetEtcInfoCookies()
		{
			string posShopCd = Request["pos_shop_cd"] == null ? "" : Request["pos_shop_cd"].Trim();
			string posClassCd = Request["pos_class_cd"] == null ? "" : Request["pos_class_cd"].Trim();
			string posClassKind = Request["pos_class_kind"] == null ? "" : Request["pos_class_kind"].Trim();

			if (false == string.IsNullOrEmpty(posShopCd))
			{
				GMobileWebContext.Current.ShoppingInfo.SetShopCd(posShopCd);
			}

			if (false == string.IsNullOrEmpty(posClassCd))
			{
				GMobileWebContext.Current.ShoppingInfo.SetClassCd(posClassCd);
			}

			if (false == string.IsNullOrEmpty(posClassKind))
			{
				GMobileWebContext.Current.ShoppingInfo.SetClassKind(posClassKind);
			}
		}

		public ActionResult Index(string rtnurl = "", string fromWhere = "")
		{
			return Login(rtnurl, fromWhere);
		}

		public ActionResult Login(string rtnurl = "", string fromWhere = "", string type = "", bool merge = false)
		{
			if( PageAttr.IsIpad && (gmktWebContext.PCID ?? "").StartsWith( "4" ) )
			{
				return Redirect( Urls.LoginUrl );
			}

			this.SetEtcInfoCookies();

			LoginM model = new LoginM
			{
				IsMobileBrowser = this.IsMobileBrowser(Request.ServerVariables["HTTP_USER_AGENT"]), // TODO: ylyl - 고치자...
				IsAdultLogin = false,
				IsFailLogin = false,
				ReturnUrl = Urls.MobileWebUrl,
				needMerge = merge
			};

			string adultUseLoinCheck = Request["adultUseLoinCheck"];
			if (false == string.IsNullOrEmpty(adultUseLoinCheck) && "Y".Equals(adultUseLoinCheck.ToUpper()))
			{
				model.IsAdultLogin = true;
				if(gmktUserProfile.UserRole == Framework.RoleEnum.NonMember)
				{
					gmktNonUserProfile.UpdateAdultUseLoginCheckCookie(true);
				}
				else
				{
					gmktUserProfile.UpdateAdultUseLoginCheckCookie(true);
				}
			}
			
			string failCheck = Request["failCheck"];
			int failCode = 0;
			if (false == string.IsNullOrEmpty(failCheck) && Int32.TryParse(failCheck, out failCode))
			{
				model.IsFailLogin = true;
			}

			if (false == string.IsNullOrEmpty(rtnurl))
			{
				model.ReturnUrl = rtnurl;
			}
			else
			{
				model.ReturnUrl = Request.QueryString["URL"];
			}

			model.FromWhere = String.Empty;
			if (!String.IsNullOrEmpty(fromWhere))
			{
				model.FromWhere = fromWhere;
			}

			ViewBag.Type = type;
			if(fromWhere.ToUpper() == "A")
			{
				ViewBag.Type = "old";
			}



			return View("LoginV2", model);
		}

		[ValidateInput(false)]
		[HttpPost]
		public ActionResult Login(string id, string pwd, string url, bool autoLogin = false, string fromWhere = "", bool push_agree = false, bool merge = false )
		{
			if (false == string.IsNullOrEmpty(id) && false == string.IsNullOrEmpty(pwd))
			{
				NameValueCollection cookies = new NameValueCollection();
				for (var i = 0; i < Request.Cookies.Count; i++)
				{
					HttpCookie eachCookie = Request.Cookies[i];

					cookies.Set(eachCookie.Name, eachCookie.Value);
				}

				string ipAddress = UserUtil.IPAddressBySecure();
				string httpRefererUrl = Request.ServerVariables["HTTP_REFERER"];
				if (false == string.IsNullOrEmpty(httpRefererUrl))
				{
					httpRefererUrl = httpRefererUrl.Replace("'", "''");
				}
				else
				{
					httpRefererUrl = string.Empty;
				}

				LoginApiResponse<PostLoginResponseT> response = new LoginApiBiz().PostLogin(id, pwd, autoLogin, ipAddress, httpRefererUrl, cookies, needMerge:merge);
				if (response != null)
				{
					string message = string.Empty;
					string returnUrl = string.Empty;
					string defaultReturnUrl = PageAttr.IsApp ? string.Empty : Urls.MobileWebUrl;
					//Landing Url
					if (true == string.IsNullOrEmpty(response.Data.LandingUrl))
					{
						returnUrl = IsValidateRedirectUrl(url) ? url : defaultReturnUrl;
					}
					else
					{
						returnUrl = response.Data.LandingUrl;
					}

					//로그인 메세지
					if (false == string.IsNullOrEmpty(response.Message))
					{
						message = response.Message;
					}

					//쿠키
					if (response.Cookies != null)
					{
						for (int i = 0; i < response.Cookies.Count; i++)
						{
							Response.Cookies.Set(response.Cookies[i]);
						}
					}
					
					PageAttr.ViewHeader = false;
					PageAttr.ViewFooter = false;
					if (response.ResultCode == LoginErrorCode.NO_ERROR) //도용의심 낮음도 NO_ERROR
					{
						if(PageAttr.IsApp)
						{
							List<string> query = new List<string>();
							query.Add("success=true");
							if (autoLogin && response.Data.DisableAutoLogin == false) //도용의심 낮음인 경우 DisableAutoLogin이 true이다
							{
								string token = new LoginApiBiz().GetToken(id, pwd, GMobileWebContext.Current.PCID).Token;
								query.Add("token=" + HttpUtility.UrlEncode(token));
							}
							if(push_agree)
							{
							  query.Add("push=true");
							}
							else
							{
							  query.Add("push=false");
							}
							query.Add("adult=" + response.Data.IsAdult.ToString().ToLower());
							query.Add("id=" + HttpUtility.UrlEncode(id));
							returnUrl = "gmarket://login?type=member&rtnurl=" + HttpUtility.UrlEncode(returnUrl) + "&expiretime=" + LOGIN_EXPIRE_TIME_MINUTES * 60 + "&";
							returnUrl += String.Join("&", query);
							return Redirect(returnUrl);
						}

						if (false == string.IsNullOrEmpty(fromWhere) && fromWhere.Trim().ToUpper().Equals("A"))
						{
							string redirectUrl = String.Empty;
							string couponzoneUrl = Urls.MobileWebUrl + "/Couponzone";
							if (GMobileWebContext.Current.UserProfile.IsSimpleBuyer)
							{
								redirectUrl = Urls.MemberSignInRootSecure + "/mobile/SelfAuthGate/SelfAuthGate?returnUrl=" + couponzoneUrl;
							}
							else
							{
								redirectUrl = couponzoneUrl;
							}
							returnUrl = redirectUrl;
						}

						return View("LoginSuccess", "", returnUrl);
					}
					else if(true == string.IsNullOrEmpty(response.Data.LandingUrl) && false == string.IsNullOrEmpty(response.Message))
					{
						if(PageAttr.IsApp)
						{
							List<string> query = new List<string>();
							query.Add("success=false");
							query.Add("message=" + HttpUtility.UrlEncode(response.Message));
							returnUrl = "gmarket://login?";
							returnUrl += String.Join("&", query);
							return Redirect(returnUrl);
						}
						return AlertMessageAndHistorybackOrClose(response.Message, "-1");
					}
					else if(false == string.IsNullOrEmpty(response.Data.LandingUrl) && true == string.IsNullOrEmpty(response.Message))
					{
						// same as no_error
						if(PageAttr.IsApp)
						{
							List<string> query = new List<string>();
							query.Add("success=true");
							if(autoLogin)
							{
								string token = new LoginApiBiz().GetToken(id, pwd, GMobileWebContext.Current.PCID).Token;
								query.Add("token=" + HttpUtility.UrlEncode(token));
							}
							if(push_agree)
							{
							  query.Add("push=true");
							}
							else
							{
							  query.Add("push=false");
							}
							query.Add("id=" + HttpUtility.UrlEncode(id));
							query.Add("adult=" + response.Data.IsAdult.ToString().ToLower());
							returnUrl = "gmarket://login?type=member&rtnurl=" + HttpUtility.UrlEncode(returnUrl) + "&expiretime=" + LOGIN_EXPIRE_TIME_MINUTES * 60 + "&";
							returnUrl += String.Join("&", query);
							return Redirect(returnUrl);
						}
						else if(String.IsNullOrEmpty(returnUrl))
						{
							returnUrl = Urls.MobileWebUrl;
						}

						return View("LoginSuccess", "", returnUrl);
					}
					else if (false == string.IsNullOrEmpty(response.Data.LandingUrl) && false == string.IsNullOrEmpty(response.Message))
					{
						return AlertMessageAndLocationChange(response.Message, returnUrl);
					}
					else
					{
						return View("LoginSuccess", "", returnUrl);
					}
				}
				else
				{
					return AlertMessageAndHistorybackOrClose("로그인 중 오류가 발생했습니다.", "-1");
				}
			}
			else
			{
				return RedirectToAction("Login");
			}
		}

		[HttpPost]
		public ActionResult LoginForNonMember(string nuserId, string nuserPw, string nuserPn, string nuserPn2, string nuserPn3, string url)
		{
			string tellno = nuserPn + "-" + nuserPn2 + "-" + nuserPn3;
			if(false == string.IsNullOrEmpty(nuserId) && false == string.IsNullOrEmpty(tellno) && false == string.IsNullOrEmpty(nuserPw))
			{
				NameValueCollection cookies = new NameValueCollection();
				for (var i = 0; i < Request.Cookies.Count; i++)
				{
					HttpCookie eachCookie = Request.Cookies[i];
					cookies.Set(eachCookie.Name, eachCookie.Value);
				}

				LoginApiResponse<PostLoginResponseT> response = new LoginApiBiz().PostNonMemberLogin(nuserId, nuserPw, tellno, cookies);
				if (response != null && response.Data != null)
				{
					string message = string.Empty;
					string returnUrl = string.Empty;

					//Landing Url
					if (true == string.IsNullOrEmpty(response.Data.LandingUrl))
					{
						if (false == string.IsNullOrEmpty(url))
						{
							returnUrl = url;
						}
					}
					else
					{
						returnUrl = response.Data.LandingUrl;
					}

					//로그인 메세지
					if (false == string.IsNullOrEmpty(response.Message))
					{
						message = response.Message;
					}

					//쿠키
					if (response.Cookies != null)
					{
						for (int i = 0; i < response.Cookies.Count; i++)
						{
							Response.Cookies.Set(response.Cookies[i]);
						}
					}
					
					PageAttr.ViewHeader = false;
					PageAttr.ViewFooter = false;
					if (response.ResultCode == LoginErrorCode.NO_ERROR) //도용의심 낮음도 NO_ERROR
					{
						if(PageAttr.IsApp)
						{
							returnUrl = "gmarket://login?success=true&type=guestmember&rtnurl=" + HttpUtility.UrlEncode(Urls.MMyGUrl) + "&expiretime=" + LOGIN_EXPIRE_TIME_MINUTES * 60;
						}
						else if(String.IsNullOrEmpty(returnUrl))
						{
							returnUrl = Urls.MMyGUrl;
						}
						return Redirect(returnUrl);
					}
					// 도용의심이 있을리가 없겠지만 그냥 살려둡니다.
					else if(true == string.IsNullOrEmpty(response.Data.LandingUrl) && false == string.IsNullOrEmpty(response.Message))
					{
						return AlertMessageAndHistorybackOrClose(response.Message, "-1");
					}
					else if(false == string.IsNullOrEmpty(response.Data.LandingUrl) && true == string.IsNullOrEmpty(response.Message))
					{
						return Redirect(returnUrl);
					}
					else if (false == string.IsNullOrEmpty(response.Data.LandingUrl) && false == string.IsNullOrEmpty(response.Message))
					{
						return AlertMessageAndLocationChange(response.Message, returnUrl);
					}
					else
					{
						return Redirect(returnUrl);
					}
				}
				else
				{
					return AlertMessageAndHistorybackOrClose("로그인 중 오류가 발생했습니다.", "-1");
				}
			}
			else
			{
				return RedirectToAction("Login");
			}
		}

		//[AllowCrossDomainCall("*")]
		[HttpGet]
		public JsonpResult ResetNonMemberOrderPassword(string custName, string phoneNo, string packNo, string sendMethod)
		{
			string ipAddress = UserUtil.IPAddressBySecure();
			LoginApiResponse<NonMemberOrderPasswordResetResult> response = new LoginApiBiz().ResetNonMemberOrderPassword(custName, phoneNo, packNo, sendMethod, ipAddress);
			NonMemberOrderPasswordResetResult returnData = new NonMemberOrderPasswordResetResult();
			returnData.ResultCode = -1;

			if(response != null && response.Data != null)
			{
				returnData = response.Data;
			}
			
			return this.Jsonp(returnData);
		}

		[HttpPost]
		public ActionResult LoginForNonMemberOrder(string url)
		{
			NameValueCollection cookies = new NameValueCollection();
			for(var i = 0; i < Request.Cookies.Count; i++)
			{
				HttpCookie eachCookie = Request.Cookies[i];
				cookies.Set(eachCookie.Name, eachCookie.Value);
			}

			LoginApiResponse<PostLoginResponseT> response = new LoginApiBiz().PostNonMemberOrderLogin(cookies);

			if(response != null && response.Data != null)
			{
				string message = string.Empty;
				string returnUrl = string.Empty;

				//Landing Url
				if(true == string.IsNullOrEmpty(response.Data.LandingUrl))
				{
					if(false == string.IsNullOrEmpty(url))
					{
						returnUrl = url;
					}
				}
				else
				{
					returnUrl = response.Data.LandingUrl;
				}

				//로그인 메세지
				if(false == string.IsNullOrEmpty(response.Message))
				{
					message = response.Message;
				}

				//쿠키
				if(response.Cookies != null)
				{
					for(int i = 0; i < response.Cookies.Count; i++)
					{
						Response.Cookies.Set(response.Cookies[i]);
					}
				}

				PageAttr.ViewHeader = false;
				PageAttr.ViewFooter = false;
				if(response.ResultCode == LoginErrorCode.NO_ERROR) //도용의심 낮음도 NO_ERROR
				{
					if(PageAttr.IsApp)
					{
						returnUrl = "gmarket://login?success=true&type=nonmember&rtnurl=" + HttpUtility.UrlEncode(returnUrl) + "&expiretime=" + LOGIN_EXPIRE_TIME_MINUTES * 60;
					}

					if(String.IsNullOrEmpty(returnUrl))
					{
						returnUrl = Urls.MobileWebUrl;
					}
					return Redirect(returnUrl);
				}
				else if(true == string.IsNullOrEmpty(response.Data.LandingUrl) && false == string.IsNullOrEmpty(response.Message))
				{
					return Redirect(returnUrl);
				}
				else if(false == string.IsNullOrEmpty(response.Data.LandingUrl) && true == string.IsNullOrEmpty(response.Message))
				{
					return Redirect(returnUrl);
				}
				else if(false == string.IsNullOrEmpty(response.Data.LandingUrl) && false == string.IsNullOrEmpty(response.Message))
				{
					return AlertMessageAndLocationChange(response.Message, returnUrl);
				}
				else
				{
					return Redirect(returnUrl);
				}
			}
			else
			{
				return AlertMessageAndHistorybackOrClose("로그인 중 오류가 발생했습니다.", "-1");
			}
		}

		[AllowAnonymous]
		public ActionResult LoginSFC(string rtnurl = "")
		{
			LoginSFCM model = new LoginSFCM()
			{
				IsFailLogin = false,
				IsNotSFCMember = false,
				ReturnUrl = Urls.MobileWebUrl
			};

			if (false == string.IsNullOrEmpty(rtnurl))
			{
				model.ReturnUrl = rtnurl;
			}
			else if (false == string.IsNullOrEmpty(Request.QueryString["URL"]))
			{
				model.ReturnUrl = Request.QueryString["URL"];
			}

			return View(model);
		}

		[AllowAnonymous]
		[ValidateInput(false)]
		[HttpPost]
		public ActionResult LoginSFC(string id, string pwd, string url, bool auto_login = false, bool push_agree = false)
		{
			if (false == string.IsNullOrEmpty(id) && false == string.IsNullOrEmpty(pwd))
			{
				NameValueCollection cookies = new NameValueCollection();
				for (var i = 0; i < Request.Cookies.Count; i++)
				{
					HttpCookie eachCookie = Request.Cookies[i];

					cookies.Set(eachCookie.Name, eachCookie.Value);
				}

				string ipAddress = UserUtil.IPAddressBySecure();
				string httpRefererUrl = Request.ServerVariables["HTTP_REFERER"];
				if (false == string.IsNullOrEmpty(httpRefererUrl))
				{
					httpRefererUrl = httpRefererUrl.Replace("'", "''");
				}
				else
				{
					httpRefererUrl = string.Empty;
				}

				LoginApiResponse<PostLoginResponseT> response = new LoginApiBiz().PostLogin(id, pwd, auto_login, ipAddress, httpRefererUrl, cookies, ExternalMallSiteCode.SFC);
				if (response != null)
				{
					string message = string.Empty;
					string returnUrl = string.Empty;

					//Landing Url
					if (true == string.IsNullOrEmpty(response.Data.LandingUrl))
					{
						returnUrl = IsValidateRedirectUrl(url) ? url : Urls.MobileWebUrl;
					}
					else
					{
						returnUrl = response.Data.LandingUrl;
					}

					//로그인 메세지
					if (false == string.IsNullOrEmpty(response.Message))
					{
						message = response.Message;
					}

					//쿠키
					if (response.Cookies != null)
					{
						for (int i = 0; i < response.Cookies.Count; i++)
						{
							Response.Cookies.Set(response.Cookies[i]);
						}
					}


					if(response.ResultCode == LoginErrorCode.NO_ERROR) //도용의심 낮음도 NO_ERROR
					{
						string token = "";
						if (auto_login && response.Data.DisableAutoLogin == false) //도용의심 낮음인 경우 DisableAutoLogin이 true이다
						{
							// TODO: ylyl - test
							string pcId = GMobileWebContext.Current.PCID;
							token = new LoginApiBiz().GetToken(id, pwd, pcId).Token;
						}

						return RedirectToAction("LoginSFCComplete", new {returnUrl, push_agree, auto_login, token, mid=id });
					}
					else if (true == string.IsNullOrEmpty(response.Data.LandingUrl) && false == string.IsNullOrEmpty(response.Message))
					{
						return AlertMessageAndHistorybackOrClose(response.Message, "-1");
					}
					else if (false == string.IsNullOrEmpty(response.Data.LandingUrl) && true == string.IsNullOrEmpty(response.Message))
					{
						return new RedirectResult(returnUrl);
					}
					else if (false == string.IsNullOrEmpty(response.Data.LandingUrl) && false == string.IsNullOrEmpty(response.Message))
					{
						return AlertMessageAndLocationChange(response.Message, returnUrl);
					}
					else
					{
						return new RedirectResult(returnUrl);
					}
				}
				else
				{
					return AlertMessageAndHistorybackOrClose("로그인 중 오류가 발생했습니다.", "-1");
				}
			}
			else
			{
				return RedirectToAction("LoginSFC");
			}
		}

		[AllowAnonymous]
		public ActionResult LoginSFCComplete(string returnUrl = "", bool auto_login = false, bool push_agree = false, string token = "", string mid = "")
		{
			string defaultRedirection = "login://?rtnurl=" + HttpUtility.UrlEncode( returnUrl ) + "&";
			List<string> query = new List<string>();
			if(auto_login)
			{
				query.Add("token=" + HttpUtility.UrlEncode(token));
			}

			if(push_agree)
			{
				query.Add("push=true");
			}
			else
			{
				query.Add("push=false");
			}

			query.Add("id=" + mid);

			ViewBag.URL = defaultRedirection + String.Join("&", query);

			return View();
		}

        [NonAction]
        private bool IsValidateRedirectUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                Uri stringUri = new Uri(url);
                return (stringUri.Scheme == Uri.UriSchemeHttp || stringUri.Scheme == Uri.UriSchemeHttps) && stringUri.Host.IndexOf("gmarket.co.kr") != -1;
            }
            else
            {
                return false;
            }
        }
	}
}

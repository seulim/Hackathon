using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;
using GMKT.GMobile.Web.Util;
using GMKT.Web;
using System.Web;
using System.Net.Cache;
using GMKT.Component.Sns;
using GMKT.Component.Sns.OpenApi;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Biz.Sns;
using GMKT.GMobile.Data.Sns;



namespace GMKT.GMobile.Web.Controllers
{
	public class SnsController : Controller
	{
		#region Twitter

		public ActionResult Twitter(string returnUrl)
		{
			TwitterApi twitterApi = new TwitterApi("TWITTER_GMAREKT_BBS", "http");
			twitterApi.CallBackUrl = string.Format("{0}://{1}/Sns/TwitterCallBack?returnurl={2}", Request.Url.Scheme, Request.Url.Host, returnUrl);

			if (Request["oauth_token"] == null)
			{
				//Redirect the user to Twitter for authorization.
				//Using oauth_callback for local testing.				
				return Redirect(twitterApi.GetAuthorizationLink());
			}
			else
			{
				GetUserDetailsFromTwitter();
			}

			return View();
		}

		public ActionResult TwitterCallBack(string returnUrl)
		{
			GetUserDetailsFromTwitter();

			return Redirect(returnUrl);
		}

		private void GetUserDetailsFromTwitter()
		{

			string returnUrl = Request.QueryString["returnUrl"];
			TwitterApi twitterApi = new TwitterApi("TWITTER_GMAREKT_BBS", "http");
			twitterApi.CallBackUrl = Url.Encode(returnUrl);

			if (Request["oauth_token"] != null & Request["oauth_verifier"] != null)
			{
				twitterApi.GetAccessToken(Request["oauth_token"], Request["oauth_verifier"]);

				SnsTotalInfo uinfo = twitterApi.GetUserInfo(twitterApi.GetOAuthInfo());
				if (uinfo != null)
				{
					uinfo.SnsToken = twitterApi.Token;
					uinfo.SnsTokenSecret = twitterApi.TokenSecret;
					uinfo.snsService = SnsService.TWITTER;
					SetCookie(uinfo);
				}
			}
		}

		[HttpPost]
		public void TwUpdate(string status, string planId)
		{
			CookieHelper cookieHelper = new CookieHelper();
			SnsFeedInfo snsFeedInfo = new SnsFeedInfo();
			TwitterApi twitterApi = new TwitterApi("TWITTER_GMAREKT_BBS", "http");
			twitterApi.Token = cookieHelper.GetCookieValue("TW", "twitter_token");
			twitterApi.TokenSecret = cookieHelper.GetCookieValue("TW", "twitter_token_secret");

			snsFeedInfo.Message = status;
			snsFeedInfo.Picture = cookieHelper.GetCookieValue("TW", "profile_image_url");

			twitterApi.Publish(snsFeedInfo, () =>
			{
				return new SnsUserDetailInfoT()
				{
					SnsAccessToken = twitterApi.Token,
					SnsAccessTokenSecret = twitterApi.TokenSecret
				};
			});
		}

		[HttpPost]
		public void FbUpdate(string message, string link, string picture, string name)
		{
			CookieHelper cookieHelper = new CookieHelper();
			SnsFeedInfo snsFeedInfo = new SnsFeedInfo();

			FacebookApi facebookApi = new Component.Sns.OpenApi.FacebookApi("FACEBOOK_GMAREKT_BBS", "http");
			facebookApi.Token = cookieHelper.GetCookieValue("FB", "facebook_token");
			facebookApi.TokenSecret = cookieHelper.GetCookieValue("FB", "facebook_token_secret");

			snsFeedInfo.Message = message;
			snsFeedInfo.Picture = picture;
			snsFeedInfo.Name = name;
			snsFeedInfo.Link = link;

			facebookApi.Publish(snsFeedInfo, () =>
			{
				return new SnsUserDetailInfoT()
				{
					SnsAccessToken = facebookApi.Token,
					SnsAccessTokenSecret = facebookApi.TokenSecret
				};
			});
		}

		[HttpGet]
		public string GetShortenLink(string planId)
		{
			HttpWebRequest webRequest = System.Net.WebRequest.Create(string.Format("http://gmkt.kr/RequestShortenUrl.asp?SrinkGb=p&SrinkCd={0}", planId)) as HttpWebRequest;
			webRequest.Method = "GET";
			webRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
			webRequest.KeepAlive = false;

			string responseData = WebResponseGet(webRequest);

			webRequest = null;

			var sep = responseData.IndexOf(',');
			if (sep > 0 && responseData.Substring(0, sep) == "0")
			{
				return responseData.Substring(sep + 1);
			}

			return string.Empty;
		}

		private string WebResponseGet(WebRequest webRequest)
		{
			using (HttpWebResponse res = (HttpWebResponse)webRequest.GetResponse())
			{
				using (Stream streamResponse = res.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(streamResponse))
					{
						return streamReader.ReadToEnd();
					}
				}
			}
		}
		#endregion

		#region FaceBook

		public ActionResult FaceBook(string returnUrl)
		{
			FacebookApi facebookApi = new Component.Sns.OpenApi.FacebookApi("FACEBOOK_GMAREKT_BBS", "http");
			facebookApi.CallBackUrl = string.Format("{0}://{1}/Sns/FacebookCallBack?returnurl={2}", Request.Url.Scheme, Request.Url.Host, returnUrl);

			if (Request["oauth_token"] == null)
			{
				//Redirect the user to Twitter for authorization.
				//Using oauth_callback for local testing.			
				return Redirect(facebookApi.GetAuthorizationLink());
			}

			return View();
		}

		public ActionResult FacebookCallBack()
		{
			string returnUrl = Request.QueryString["returnUrl"];
			string code = Request.QueryString["code"];

			FacebookApi facebookApi = new Component.Sns.OpenApi.FacebookApi("FACEBOOK_GMAREKT_BBS", "http");
			facebookApi.CallBackUrl = Url.Encode(Request.Url.ToString());

			if (!string.IsNullOrEmpty(code))
			{

				if (facebookApi != null)
				{
					facebookApi.GetAccessToken(code);
				}

				SnsTotalInfo uinfo = facebookApi.GetUserInfo(facebookApi.GetOAuthInfo());
				if (uinfo != null)
				{
					uinfo.SnsToken = facebookApi.Token;
					uinfo.snsService = SnsService.FACEBOOK;
					SetCookie(uinfo);
				}
			}

			return Redirect(string.Format("/Sns/Turnnel?redirectUrl={0}", Url.Encode(returnUrl)));
		}
		#endregion


		#region KaKaoStory

		public ActionResult KaKaoStory(string returnUrl)
		{
			KaKaoStoryAPIBiz  KaKaoStoryApi = new KaKaoStoryAPIBiz();
			string sCallbackUrl = string.Format("{0}://{1}/Sns/KaKaoStoryCallBack", Request.Url.Scheme, Request.Url.Host);
			string sAuthUrl = KaKaoStoryApi.GetAuthorize(sCallbackUrl, returnUrl);

			if (Request["oauth_token"] == null)
			{
				return Redirect(sAuthUrl);
			}

			return View();
		}

		public ActionResult KaKaoStoryCallBack(string code, string state)
		{
			string sReturnUrl = string.Empty;

			if (!string.IsNullOrEmpty(code))
			{
				KaKaoStoryAPIBiz KaKaoStoryApi = new KaKaoStoryAPIBiz();
				KaKaoStoryAPIBiz kakaoApi = new KaKaoStoryAPIBiz();

				string sCallBackUrl = string.Format("{0}://{1}/Sns/KaKaoStoryCallBack", Request.Url.Scheme, Request.Url.Host);
				SnsKaKaoInfo snsinfo = KaKaoStoryApi.GetToken("authorization_code", code, sCallBackUrl);

				if (snsinfo != null && snsinfo.SnsToken != null && snsinfo.SnsToken != "")
				{
					//User Info Get
					snsinfo = kakaoApi.GetProfile(snsinfo.SnsToken, snsinfo);
					SnsTotalInfo uinfo = new SnsTotalInfo();
					if (snsinfo != null)
					{
						uinfo.SnsID = snsinfo.SnsID;
						uinfo.SnsUserName = snsinfo.SnsUserName;
						uinfo.SnsScreenName = snsinfo.SnsUserName;
						uinfo.SnsToken = snsinfo.SnsToken;
						uinfo.SnsTokenSecret = snsinfo.SnsTokenSecret;
						uinfo.snsService = SnsService.KAKAOSTORY;
						uinfo.SnsProfileImage = snsinfo.SnsProfileImage;
						uinfo.SnsProfileBio = "";
						SetCookie(uinfo);
					}
				}
			}

			return Redirect(string.Format("/Sns/Turnnel?redirectUrl={0}", Url.Encode(state)));
		}


		[HttpPost]
		public void KsUpdate(string message, string link, string picture, string name, string bdno)
		{
			CookieHelper cookieHelper = new CookieHelper();
			KaKaoStoryAPIBiz kakaoApi = new KaKaoStoryAPIBiz();

			string sResult = string.Empty;
			string sToken = cookieHelper.GetCookieValue("KS", "kakaostory_token");
			string sReflashToken = cookieHelper.GetCookieValue("KS", "kakaostory_token_secret");

			SnsKaKaoStoryT kakaoStoryT = new SnsKaKaoStoryT()
			{
				Title = "",
				Message = message,
				Image = picture,
				RequestUrl = link,
				LandingUrl = link,
				LinkUrl = "http://event.gmarket.co.kr/bbs/sns/kakaostory/LinkInfoData.asp?bm_seqno=" + bdno,
				Description = "",
				Host = ""
			};

			kakaoStoryT.PostingText = kakaoApi.GetLinkInfo(sToken, kakaoStoryT);			
			if (!string.IsNullOrEmpty(kakaoStoryT.PostingText))
			{
				sResult = kakaoApi.SetPosting(sToken, kakaoStoryT);
			}
		}

		#endregion


		#region Etc

		public ActionResult Turnnel(string redirectUrl)
		{
			return View();
		}

		internal void SetCookie(SnsTotalInfo snsInfo)
		{
			var cookieHelper = new CookieHelper();

			switch (snsInfo.snsService)
			{
				case SnsService.FACEBOOK:
					cookieHelper.SetNonEncodingCookieValue("FB", "id", Uri.EscapeDataString(snsInfo.SnsID), GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetNonEncodingCookieValue("FB", "name", Uri.EscapeDataString(snsInfo.SnsID), GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("FB", "kind", "FB", GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetNonEncodingCookieValue("FB", "screen_name", Uri.EscapeDataString(snsInfo.SnsUserName), GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("FB", "profile_image_url", snsInfo.SnsProfileImage, GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("FB", "facebook_token", snsInfo.SnsToken, GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("FB", "facebook_token_secret", snsInfo.SnsTokenSecret, GMKTEnvironment.Instance.BaseDomainUrl);
					break;

				case SnsService.TWITTER:
					cookieHelper.SetNonEncodingCookieValue("TW", "name", Uri.EscapeDataString(snsInfo.SnsUserName), GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("TW", "kind", "TW", GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("TW", "screen_name", snsInfo.SnsScreenName, GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("TW", "profile_image_url", snsInfo.SnsProfileImage, GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("TW", "twitter_token", snsInfo.SnsToken, GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("TW", "twitter_token_secret", snsInfo.SnsTokenSecret, GMKTEnvironment.Instance.BaseDomainUrl);
					break;

				case SnsService.KAKAOSTORY:
					cookieHelper.SetNonEncodingCookieValue("KS", "id", Uri.EscapeDataString(snsInfo.SnsID), GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetNonEncodingCookieValue("KS", "name", Uri.EscapeDataString(snsInfo.SnsID), GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("KS", "kind", "KS", GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetNonEncodingCookieValue("KS", "screen_name", Uri.EscapeDataString(snsInfo.SnsUserName), GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("KS", "profile_image_url", snsInfo.SnsProfileImage, GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("KS", "kakaostory_token", snsInfo.SnsToken, GMKTEnvironment.Instance.BaseDomainUrl);
					cookieHelper.SetCookieValue("KS", "kakaostory_token_secret", snsInfo.SnsTokenSecret, GMKTEnvironment.Instance.BaseDomainUrl);

					break;
			}
		}
		#endregion
	}
}
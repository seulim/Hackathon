using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using ConnApi.Client;
using Newtonsoft.Json;
using RestSharp;


namespace GMKT.GMobile.Data.Sns
{
	public class KaKaoStoryApiDac
	{
		private const string m_authUrl = "https://kauth.kakao.com";
		private const string m_apiUrl = "https://kapi.kakao.com";
		private string sKaKao_App_Key = "05b6cc44e7ce66742d6dbd652f572da0";
		private string authorize_code = string.Empty;
		private string sCallbackUrl = string.Empty;

		public string m_KaKaoAppKey
		{
			get { return sKaKao_App_Key; }
		}

		#region 생성자
		public KaKaoStoryApiDac()
		{
			System.Collections.Specialized.NameValueCollection settings = System.Configuration.ConfigurationSettings.AppSettings;
			sKaKao_App_Key = (settings["KaKaoAppKey"] != null && settings["KaKaoAppKey"].Length > 0) ? settings["KaKaoAppKey"].ToString() : sKaKao_App_Key;
		}
		#endregion


		#region 사용자 관리
		//코드 받기
		//로그인 버튼 클릭시 먼저 사용자 동의를 거쳐 토큰을 발급 받을 수 있는 코드를 받아와야 합니다.
		//GET /oauth/authorize?client_id={app_key}&redirect_uri={redirect_uri}&response_type=code HTTP/1.1
		//Host: kauth.kakao.com		
		public string GetAuthorize(string sCallBackUrl, string returnUrl)
		{
			string result = string.Format(m_authUrl + "/oauth/authorize?client_id={0}&redirect_uri={1}&response_type=code&state={2}", sKaKao_App_Key, sCallBackUrl, returnUrl);

			return result;
		}
		#endregion


		//사용자 토큰 받기(grant_type:authorization_code) / 갱신(grant_type:refresh_token)
		//코드를 얻은 다음, 이를 이용하여 실제로 API를 호출할 수 있는 사용자 토큰(Access Token, Refresh Token)을 받아 올 수 있습니다.
		//POST /oauth/token HTTP/1.1
		//Host: kauth.kakao.com

		public string GetToken(string grant_type, string sCode, string sCallbackUrl)
		{
			string response = string.Empty;
			string url = m_authUrl + "/oauth/token";
			string postData = string.Format("grant_type={0}&client_id={1}&redirect_uri={2}&code={3}"
											, grant_type.ToString()
											, this.sKaKao_App_Key
											, sCallbackUrl
											, sCode);
			try
			{
				response = KWebRequest(
							"POST"
							, url
							, postData
							, null
							, null
							, ContentType.content);
			}
			catch (Exception e)
			{
				throw e;
			}

			return response;
			/*

			string url = m_authUrl + "/oauth/token";
			KaKaoApiHelper kakaoapi = new KaKaoApiHelper("kakaostory");
			ApiResponse<SnsKaKaoInfo> result = kakaoapi.CallPostAPI<ApiResponse<SnsKaKaoInfo>>(
				"POST",
				url,
				new
				{
					grant_type = grant_type,
					client_id = this.sKaKao_App_Key,
					redirect_uri = sCallbackUrl,
					code = sCode
				}
			);
			return result;
			 * */
		}


		#region 카카오스토리
		//카카오스토리 프로필 요청
		//카카오스토리 프로필 요청은 사용자 중에 카카오스토리를 사용하는 사용자에 한해 카카오스토리의 프로필 정보를 얻어 올 수 있는 기능입니다. 해당 기능을 사용하기 위해서는 성공적인 로그인 후에 얻을 수 있는 사용자 토큰이 필요합니다.
		//GET /v1/api/story/profile HTTP/1.1
		//Host: kapi.kakao.com
		//Authorization: Bearer {access_token}
		public string GetProfile(string access_token)
		{
			string response = string.Empty;
			string url = m_apiUrl + "/v1/api/story/profile";
			Dictionary<string, string> header = new Dictionary<string, string>();
			header.Add("Authorization", "Bearer " + access_token);

			try
			{
				response = KWebRequest(
							"GET"
							, url
							, ""
							, header
							, null
							, ContentType.content);											
			}
			catch (Exception e)
			{
				throw e;
			}

			return response;
		}
		
		//링크(Link) 포스팅
		//스크랩할 URL로 부터 해당 페이지의 정보를 얻은 후, 그 정보를 바탕으로 포스팅합니다.
		//GET /v1/api/story/linkinfo HTTP/1.1
		//Host: kapi.kakao.com
		//Authorization: Bearer {access_token}
		/*
		public string PostingData2(string access_token, Dictionary<string, object> param)
		{
			string resultStr = "";
			string sLinkInfo = "";
			try
			{
				//Get Link Data
				string linkResult = GetLinkData(access_token, param["linkUrl"].ToString());

				if (!string.IsNullOrEmpty(linkResult))
				{
					dynamic parseData = GMKT.Framework.Serialize.GMKTSerializeFormatter.JsonDeserialize<dynamic>(linkResult);
					string sTitle = parseData["title"] == null ? "" : Convert.ToString(parseData["title"]);
					string sImage = parseData["image"] == null ? "" : Convert.ToString(parseData["image"]);
					string sRequestUrl = parseData["requested_url"] == null ? "" : Convert.ToString(parseData["requested_url"]);
					string sDescription = parseData["description"] == null ? "" : Convert.ToString(parseData["description"]);
					string sHost = parseData["host"] == null ? "" : Convert.ToString(parseData["host"]);

					if (param["description"] != null && param["description"].ToString() != "") sDescription = param["description"].ToString();
					sDescription = string.IsNullOrEmpty(sDescription) ? "G마켓- 쇼핑을 다 담다." : sDescription;
					sHost = "GMARKET.CO.KR";

					sLinkInfo = MakePostingText(sTitle, sImage, param["link"].ToString(), sRequestUrl, sDescription, sHost);

					//Posting Link Data
					resultStr = PostingLinkData(access_token, param, sLinkInfo);
				}
			}
			catch (Exception e)
			{
				throw e;
			}

			return resultStr;
		}
		 * */

		//SETP1: 링크 포스팅을 하기 위해서 먼저 해당 링크로 부터 정보를 얻어오는 API를 호출합니다. 정보를 성공적으로 얻어오면, 응답 결과로 미리보기를 제공할 수 있습니다.
		//GET /v1/api/story/linkinfo HTTP/1.1
		//Host: kapi.kakao.com
		//Authorization: Bearer {access_token}
		public string GetLinkInfo(string access_token, string targetUrl)
		{
			string response = string.Empty;
			string url = m_apiUrl + "/v1/api/story/linkinfo";

			try
			{
				Dictionary<string, string> header = new Dictionary<string, string>();
				header.Add("Authorization", "Bearer " + access_token);

				string postData = string.Format("url={0}"
												, HttpContext.Current.Server.UrlEncode(targetUrl));

				response = KWebRequest(
								"GET"
								, url
								, postData
								, header
								, null
								, ContentType.content);

			}
			catch (Exception e)
			{
				throw e;
			}

			return response;
		}

		//SETP2: 얻어온 스크랩 정보와 함께 올리고자 하는 내용을 선택적으로 추가하여 링크 포스팅 API를 호출합니다.
		//POST /v1/api/story/post/link HTTP/1.1
		//Host: kapi.kakao.com
		//Authorization: Bearer {access_token}
		public string PostingData(string access_token, SnsKaKaoStoryT kakaoStoryT)
		{
			string response = string.Empty;
			string url = m_apiUrl + "/v1/api/story/post/link";

			Dictionary<string, string> header = new Dictionary<string, string>();
			header.Add("Authorization", "Bearer " + access_token);

			string postData = string.Format("content={0}&link_info={1}&permission={2}"
											, HttpContext.Current.Server.UrlEncode(kakaoStoryT.Message)
											, HttpContext.Current.Server.UrlEncode(kakaoStoryT.PostingText)
											, 'F');

			response = KWebRequest(
							"POST"
							, url
							, postData
							, header
							, null
							, ContentType.content);

			return response;
		}
		#endregion


		#region KaKaoStory Web Request
		/// <summary>
		/// Web Request Wrapper
		/// </summary>
		/// <param name="method">Http Method</param>
		/// <param name="url">Full url to the web resource</param>
		/// <param name="postData">Data to post in querystring format</param>
		/// <returns>The web server response.</returns>
		public string KWebRequest(string sMethod, string url, string postData, Dictionary<string, string> headers, CookieContainer cookieContainer, string contentType)
		{
			HttpWebRequest webRequest = null;
			StreamWriter requestWriter = null;
			string responseData = "";
			string querystring = "";

			//Convert the querystring to postData
			if (sMethod == "POST" || sMethod == "DELETE")
			{
				querystring = "";
			}
			else
			{
				querystring = postData;
				if (querystring.Length > 0)
				{
					url += "?" + querystring;
				}
			}

			webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
			webRequest.Method = sMethod;
			webRequest.ServicePoint.Expect100Continue = false;
			//webRequest.UserAgent  = "Identify your application please.";
			//webRequest.Timeout = 20000;

			if (cookieContainer != null)
			{
				webRequest.CookieContainer = cookieContainer;
			}

			/*
			if (headers != null)
			{
				foreach (string key in headers.Keys)
					webRequest.Headers.Add(key, headers[key]);
			}
			*/

			if (headers != null)
			{
				foreach (string key in headers.Keys)
				{
					if (key.ToUpper().Equals("USER-AGENT"))
						webRequest.UserAgent = headers[key];
					else
						webRequest.Headers.Add(key, headers[key]);
				}
			}

			if (sMethod == "POST" || sMethod == "DELETE")
			{
				if (ContentType.nothing != contentType) webRequest.ContentType = contentType;

				//POST the data.
				requestWriter = new StreamWriter(webRequest.GetRequestStream());
				try
				{
					requestWriter.Write(postData);
				}
				catch
				{
					throw;
				}
				finally
				{
					requestWriter.Close();
					requestWriter = null;
				}
			}

			responseData = WebResponseGet(webRequest);

			webRequest = null;

			return responseData;
		}

		public string KWebRequest(string sMethod, string url, string postData)
		{
			return KWebRequest(sMethod, url, postData, null, null, ContentType.content);
		}

		public string KWebRequest(string sMethod, string url, string postData, Dictionary<string, string> headers)
		{
			return KWebRequest(sMethod, url, postData, headers, null, ContentType.content);
		}

		/// <summary>
		/// Process the web response.
		/// </summary>
		/// <param name="webRequest">The request object.</param>
		/// <returns>The response data.</returns>
		private string WebResponseGet(HttpWebRequest webRequest)
		{
			StreamReader responseReader = null;
			string responseData = "";

			try
			{
				responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
				responseData = responseReader.ReadToEnd();
			}
			catch (WebException ex)
			{
				Console.Write(ex.Message);
				throw;
			}
			catch
			{
				throw;
			}
			finally
			{
				webRequest.GetResponse().GetResponseStream().Close();
				responseReader.Close();
				responseReader = null;
			}

			return responseData;
		}
	}
	#endregion

	/*
	public class KaKaoApiHelper : ApiHelper
	{
		public KaKaoApiHelper(string apiName) : base(apiName) { }

		public ApiResponse<T> CallPostAPI<T>(string resource, object content, string access_token, params ApiParameter[] apiParams)
				where T : new()
		{
			IRestRequest request = new RestRequest(resource, Method.POST) { RequestFormat = DataFormat.Json }.AddBody(content);

			var headerParameters = new List<HeaderParameter>();
			if (!string.IsNullOrEmpty(access_token)) headerParameters.Add(new HeaderParameter("Authorization", "Bearer " + access_token));

			foreach (HeaderParameter param in headerParameters)
			{
				request.AddHeader(param.Name, param.Value);
			}

			foreach (ApiParameter param in apiParams)
			{
				if (param is HeaderParameter)
				{
					var headerParam = param as HeaderParameter;
					request.AddHeader(headerParam.Name, headerParam.Value);
				}
				else if (param is CookieParameter)
				{
					var cookieParam = param as CookieParameter;
					request.AddCookie(cookieParam.Name, cookieParam.Value);
				}
			}

			try{
			var response = Client.Execute<ApiResponse<T>>(request);
			if (response != null)
			{
				switch (response.StatusCode)
				{
					case HttpStatusCode.NotFound:
						throw new Exception("[HttpStatusCode.NotFound]" + response.ErrorMessage);
					case HttpStatusCode.BadGateway:
						throw new Exception("[HttpStatusCode.BadGateway]" + response.ErrorMessage);
				}

				if (response.ErrorException != null)
				{
					throw response.ErrorException;
				}
				else if (!string.IsNullOrEmpty(response.ErrorMessage))
				{
					throw new Exception(response.ErrorMessage);
				}
				else
				{
					ApiResponse<T> result = response.Data;

		
					// TODO: ylyl - 
					if (response.Cookies != null)
					{
						result.Cookies = new System.Web.HttpCookieCollection();

						foreach (var eachCookie in response.Cookies)
						{
							System.Web.HttpCookie thisCookie = new System.Web.HttpCookie(eachCookie.Name, eachCookie.Value);
							thisCookie.Domain = eachCookie.Domain;
							thisCookie.Expires = eachCookie.Expires;

							result.Cookies.Add(thisCookie);
						}
					}
			

					return result;
				}
			}
			}
			catch (Exception e)
			{
				if (e is System.Net.WebException)
				{
					var errorStream = ((System.Net.WebException)e).Response.GetResponseStream();

					string error = new System.IO.StreamReader(errorStream).ReadToEnd();

					//Dictionary<string, object> parseData = (Dictionary<string, object>)SNSUtil.JavaDeSeialize(error);

					//return ErrorHandle(System.Web.HttpUtility.UrlDecode(error), true);

					//return string.Format("-1|{0}", error);
					//return error.ToString();
				}

				//return string.Format("-1|{0}", e.Message);
			}

			// TODO: ylyl -  
			return null;
		}
	}
	 * */
}

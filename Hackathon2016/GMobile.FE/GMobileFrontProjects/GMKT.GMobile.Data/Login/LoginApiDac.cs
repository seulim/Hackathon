using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using ConnApi.Client;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using System.ComponentModel;

namespace GMKT.GMobile.Data
{
	public class LoginApiHelper : ApiHelper
	{
		public LoginApiHelper(string apiName) : base(apiName) { }

		public LoginApiResponse<T> CallPostAPI<T>(string resource, object content, params ApiParameter[] apiParams) 
				where T : new()
		{
			IRestRequest request = new RestRequest(resource, Method.POST) { RequestFormat = DataFormat.Json }.AddBody(content);

			var headerParameters = new List<HeaderParameter>(DefaultParameters);

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

			var response = Client.Execute<LoginApiResponse<T>>(request);
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
					LoginApiResponse<T> result = response.Data;

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

			// TODO: ylyl -  
			return null;
		}
	}

	public class LoginApiDac
	{
		protected LoginApiHelper LoginApiHelper { get; set; }

		public LoginApiDac()
		{
			LoginApiHelper = new LoginApiHelper("GMApiSsl");
		}

		public LoginApiResponse<PostLoginResponseT> PostLogin(string id, string password, bool isAutoLogin, string ipAddress, string httpRefererUrl, CookieParameter[] cookies, string siteCode, bool needMerge)
		{
			LoginApiResponse<PostLoginResponseT> result = LoginApiHelper.CallPostAPI<PostLoginResponseT>(
				LoginApiHelper.MakeUrl("api/Login/PostLogin"),
				new
				{
					Id = id,
					Password = password,
					IsAutoLogin = isAutoLogin,
					IPAddress = ipAddress,
					HttpRefererUrl = httpRefererUrl,
					siteCode = siteCode,
					needMerge = needMerge
				},
				cookies
			);

			return result;
		}

		public LoginApiResponse<PostLoginResponseT> PostNonMemberLogin(string name, string password, string telNo, CookieParameter[] cookies)
		{
			LoginApiResponse<PostLoginResponseT> result = LoginApiHelper.CallPostAPI<PostLoginResponseT>(
				LoginApiHelper.MakeUrl("api/Login/PostNonmemberLogin"),
				new
				{
					buyerName = name,
					password = password,
					buyerTelNo = telNo
				},
				cookies
			);
			return result;
		}

		public LoginApiResponse<PostLoginResponseT> PostNonMemberOrderLogin(CookieParameter[] cookies)
		{
			LoginApiResponse<PostLoginResponseT> result = LoginApiHelper.CallPostAPI<PostLoginResponseT>(
				LoginApiHelper.MakeUrl("api/Login/PostNonmemberOrderLogin"),
				new
				{},
				cookies
			);
			return result;
		}


		public LoginApiResponse<AutoLoginToken> GetAutoLoginToken(string id, string pw, string pcid)
		{
			string excryptText;
			using(RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
			{
				UTF8Encoding byteConverter = new UTF8Encoding();
				RSA.FromXmlString(RSAKeyValue2);
				string text = "MEMBERID=" + id + "<;>" + "PASSWORD=" + pw + "<;>" + "PCID=" + pcid;
				excryptText = Convert.ToBase64String(RSA.Encrypt(byteConverter.GetBytes(text), false));
			}

			LoginApiResponse<AutoLoginToken> result = LoginApiHelper.CallPostAPI<AutoLoginToken>(
				LoginApiHelper.MakeUrl("api/Login/PostAutoLoginTokenCreate4096_PCID"),
				new
				{
					TokenString = excryptText,
					MemberId = id,
					PCID = pcid
				}
			);
			return result;
		}

		public LoginApiResponse<NonMemberOrderPasswordResetResult> ResetNonMemberOrderPassword(string custName, string phoneNo, string packNo, string sendMethod, string ipAddress)
		{
			LoginApiResponse<NonMemberOrderPasswordResetResult> result = LoginApiHelper.CallPostAPI<NonMemberOrderPasswordResetResult>(
				LoginApiHelper.MakeUrl("api/Login/ResetNonMemberOrderPassword"),
				new
				{
					name = custName,
					hpno = phoneNo,
					packno = packNo,
					sendMethod = sendMethod,
					ipAddress = ipAddress,
					reg_id = "GMKTMobile"
				}
			);
			return result;
		}

		private static string RSAKeyValue2 = @"<RSAKeyValue><Modulus>xCNfxI37t57MCJD8/1swGQfVnKhlZQz+aKPbXpJ5qWcQRnR24xNtg3pzTvvURTaNIY6DEUF6w6MfwQ98YdI8Cnder2rxmxZk1UDZoQwSnZe2+uHy6s2rijIWM5OPGoBwkg9wUy9cQSJWrEcXo3FBoQa630aJHI0raJteOIbWNKIOFfnkpmjsf6U78ObFNEPztGlEt4dMmNMAsFUvpr+3f6zB40iGnijNO6oJysoAWpB1Zfx91cf7Uw7q/9YDnBs6URujRcZUkK2kbBWgEF4bJa9s+MWC85dXlj9zgiT2V5D+pPgpJPbU/FrX4iZS3dJmRUnDkNRRDRaJBcO5vwCnBw==</Modulus><Exponent>AQAB</Exponent><P>4nIQeWQoX8CfkZAzif6Ga7t56F6nrTOGVIFdj+zpqIx04BIM7cf8rK/EcyetnwWoQBzhYXjSLyf3lCG07zks9gWmpKNTQeXynxqbSby8M36JR1ZhNCkI0a9IXxtqFM6LRZHYHaiA93OAJZlHhPEAvcRcQIVnMVSWu++PgxPWvGU=</P><Q>3bywHIlv7mzmhNXTqaNshrUsEE0t6XcGArgURkm9d01A7kjw2PuRlmB/9SI7wlvKG/hsrGdNsJ+D28I4+ex8K0PKe6NkJkqLX+pvTQLSuSZw1Wj3d5+NE60c50hukuWqQ5+TaoRfrYlBd0ZpuR835y2VXWrnZUwN0B0fBdXnMPs=</Q><DP>1SkCkQLTbq9ohiH0IiZSav9j1nWj6cri3JGafW1K1rrBGlxjh0IGfJhImQ30xgkRRKjrEFInqPQ6flrsc3Si+kR/heOo8BOBvHvTSio8D01B9ME/Z1ZpUtlpiv3Hciaru6V6hxCjtkwniT0ssdwcEobRQHtTlIBIFEmCEqWOMnE=</DP><DQ>S/2qbV08mv1/Uu7lBzIWs2PLohqRLX249Z2YdS44XHaBVihxQiuVDXAs6hm9WTKT5VoSBFOy8GD6k4nQdPZy53DoJ88rN2Q8t7ZkRoQ9K27dJIZAqSJo+uFNEoZkPruuylYZ81tCXvq3EOV0vhjQ081vbwCAJNsXsMcKJrnT7Zk=</DQ><InverseQ>ZoJNeYCKhv7075IO8Gps03eEX1w9GEalFmMkM3x3t2iA03Z+J2RsA/ze49Cfu9fF4K14QSATPLs3b+UI4A1G1Gd3hx9UjgaZp/QTC1jOqPSEnSLmAKKQ8wq+rE1mZapZ07sQOjfGKoFsi2jpTSwV1+VEsA5p3tTLbAfHHSvysJM=</InverseQ><D>ixPmSZLARdDVLtHYhB2d+nHCt6X01/WhxIND/0hv+n45XSwiTRwukIdYkk4hk759zqimKas7qNPXpWcctKSFrIo+WrbDgUwczdaBOyA7ZwH4Xueu/249T+36LjJYy9r9d6R46pNvoEQAH4L9sQiTF+KUydPTT6qOeEuLQRvjckelVesd9hD/4o1xaorjR1QnSGyIVNZ7ge0bJ1FIzyKxDZ8RyrfuWM0yYllbbu5X65+7Jam7e/7rIUq/hedMNGbtdrEi2Ec1t3Kxn2KFBYcTm3J143JZFIae/4TEb3J+ZHlqzDeQwLnC6UQ3QqPQz4VSP44GiE+rBFDDp7+t89qvmQ==</D></RSAKeyValue>";
	}
}

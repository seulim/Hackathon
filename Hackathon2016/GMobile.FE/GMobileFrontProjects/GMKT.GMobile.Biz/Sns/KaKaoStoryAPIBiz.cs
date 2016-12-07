using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.Sns;


namespace GMKT.GMobile.Biz.Sns
{
	public class KaKaoStoryAPIBiz
	{
		public string GetAuthorize(string sCallBackUrl, string returnUrl)
		{
			string response = new KaKaoStoryApiDac().GetAuthorize(sCallBackUrl, UrlEncode(returnUrl));

			return response;
		}

		public SnsKaKaoInfo GetToken(string grant_type, string code, string sCallbackUrl)
		{
			SnsKaKaoInfo result = null;
		
			string response = new KaKaoStoryApiDac().GetToken(grant_type, code, UrlEncode(sCallbackUrl));
			if (!string.IsNullOrEmpty(response))
			{
				result = string.IsNullOrEmpty(response) ? null : this.BindTokenEntity(response);
			}
			
			return result;
		}

		public SnsKaKaoInfo GetProfile(string access_token, SnsKaKaoInfo snsInfo)
		{
			SnsKaKaoInfo result = null;

			string response = new KaKaoStoryApiDac().GetProfile(access_token);
			if (!string.IsNullOrEmpty(response))
			{
				result = string.IsNullOrEmpty(response) ? snsInfo : this.BindUserInfoEntity(response, snsInfo);				
			}

			return result;
		}

		public string GetLinkInfo(string access_token, SnsKaKaoStoryT kakaoStoryT)
		{
			string result = string.Empty;

			string response = new KaKaoStoryApiDac().GetLinkInfo(access_token, kakaoStoryT.LinkUrl);
			if (!string.IsNullOrEmpty(response))
			{
				dynamic parseData = GMKT.Framework.Serialize.GMKTSerializeFormatter.JsonDeserialize<dynamic>(response);
				kakaoStoryT.Title = parseData["title"] == null ? kakaoStoryT.Title : Convert.ToString(parseData["title"]);
				kakaoStoryT.Image = parseData["image"] == null ? kakaoStoryT.Image : Convert.ToString(parseData["image"]);
				kakaoStoryT.RequestUrl = parseData["requested_url"] == null ? kakaoStoryT.RequestUrl : Convert.ToString(parseData["requested_url"]);
				kakaoStoryT.Description = parseData["description"] == null ? "G마켓- 쇼핑을 다 담다." : Convert.ToString(parseData["description"]);
				kakaoStoryT.Host = "GMARKET.CO.KR";//parseData["host"] == null ? "GMARKET.CO.KR" : Convert.ToString(parseData["host"]);

				result = BindPostingText(kakaoStoryT);
			}

			return result;
		}


		public string SetPosting(string access_token, SnsKaKaoStoryT kakaoStoryT)
		{
			string result = string.Empty;

			string response = new KaKaoStoryApiDac().PostingData(access_token, kakaoStoryT);
			if (!string.IsNullOrEmpty(response))
			{
				dynamic parseData = GMKT.Framework.Serialize.GMKTSerializeFormatter.JsonDeserialize<dynamic>(response);
				result = parseData["id"] == null ? "" : Convert.ToString(parseData["id"]);
			}

			return result;
		}

		#region DataBinding
		//토큰 정보 Binding
		private SnsKaKaoInfo BindTokenEntity(string response)
		{
			dynamic parseData = GMKT.Framework.Serialize.GMKTSerializeFormatter.JsonDeserialize<dynamic>(response);

			return new SnsKaKaoInfo()
			{
				SnsToken = parseData["access_token"],
				SnsTokenSecret = parseData["refresh_token"],
				snsServiceKind = SnsServiceKind.KAKAOSTORY
			};
		}

		//사용자 정보 Binding
		private SnsKaKaoInfo BindUserInfoEntity(string response, SnsKaKaoInfo snsInfo)
		{
			dynamic parseData = GMKT.Framework.Serialize.GMKTSerializeFormatter.JsonDeserialize<dynamic>(response);
			
			string snsid = parseData["permalink"] == null ? "" : parseData["permalink"];
			return new SnsKaKaoInfo()
			{
				SnsUserName = parseData["nickName"],
				SnsProfileImage = parseData["profileImageURL"],
				SnsID = snsid.Trim().Replace("https://story.kakao.com/", ""),				
				SnsToken = snsInfo.SnsToken,
				SnsTokenSecret = snsInfo.SnsTokenSecret,
				snsServiceKind = SnsServiceKind.KAKAOSTORY
			};
		}


		//Posting 전문 Binding
		private string BindPostingText(SnsKaKaoStoryT kakaoStoryT)
		{
			//string sTitle, string sImage, string sUrl, string sRequestUrl, string sDescription, string sHost
			string sResult = string.Empty;

			sResult = "{\"url\":\"{Url}\",\"requested_url\":\"{RequestUrl}\",\"host\":\"{Host}\",\"title\":\"{Title}\",\"image\":{Image},\"description\":\"{Description}\",\"section\":\"\"}";

			sResult = sResult.Replace("{Title}", kakaoStoryT.Title);
			sResult = sResult.Replace("{Image}", kakaoStoryT.Image);
			sResult = sResult.Replace("{Url}", kakaoStoryT.LandingUrl);
			sResult = sResult.Replace("{RequestUrl}", kakaoStoryT.RequestUrl);
			sResult = sResult.Replace("{Description}", kakaoStoryT.Description);
			sResult = sResult.Replace("{Host}", kakaoStoryT.Host);

			return sResult;
		}
		#endregion



		/// <summary>
		/// This is a different Url Encode implementation since the default .NET one outputs the percent encoding in lower case.
		/// While this is not a problem with the percent encoding spec, it is used in upper case throughout OAuth
		/// </summary>
		/// <param name="value">The value to Url encode</param>
		/// <returns>Returns a Url encoded string</returns>
		public string UrlEncode(string value)
		{
			value = HttpUtility.UrlEncode(value).Replace("+", "%20");

			value = Regex.Replace(value, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());

			value = value.Replace("(", "%28").Replace(")", "%29").Replace("$", "%24").Replace("!", "%21").Replace(
				"*", "%2A").Replace("'", "%27").Replace(",", "%2C");//.Replace("%", "%25");

			value = value.Replace("%7E", "~");

			return value;
		}
	}
}

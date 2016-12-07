using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

using ArcheFx;
using ArcheFx.EnterpriseServices;

using GMKT.Framework;
using GMKT.Framework.EnterpriseServices;

namespace GMKT.GMobile.Biz
{
	public class KakaoTalkServiceBiz : BizBase
	{
		protected string m_serverUrl = "https://tms.kakao.com/";

		protected enum api_type 
		{
			add,
			add_temp,
			remove,
			certCodeSend
		};

		private string sKakao_Plus_Key = string.Empty;

		protected string m_Kakao_Plus_Key
		{
			get { return sKakao_Plus_Key; }
		}

		#region 생성자
		public KakaoTalkServiceBiz()
		{
			System.Collections.Specialized.NameValueCollection settings = System.Configuration.ConfigurationSettings.AppSettings;
			sKakao_Plus_Key = (settings["Kakao_Plus_Key"] != null && settings["Kakao_Plus_Key"].Length > 0) ? settings["Kakao_Plus_Key"].ToString() : string.Empty;
		}
		#endregion

		#region 맞춤정보 수신여부 및 user_key 조회
		public KakaoTalkReceiverStatusT SelectReceiverStatusInfo(string custNo)
		{
			return new KakaoTalkServiceDac().SelectReceiverStatusInfo(custNo);
		}
		#endregion

        #region 회원 핸드폰번호 조회
        public CustomHPNoT SelectUserHPNo(string custNo)
        {
            return new KakaoTalkServiceDac().SelectUserHPNo(custNo);
        }
        #endregion


		#region 임시 user_key로 친구등록
		public CommonResultT AddUserTemp(string custNo, string phoneNumber, string tempUserKey, string chgId, string regChannelCd)
		{
			CommonResultT resultApi = new CommonResultT();
			CommonResultT result = new CommonResultT();
			string sPhoneNumber_Kakao = string.Empty;

            sPhoneNumber_Kakao = phoneNumber.Replace("-", string.Empty);
            sPhoneNumber_Kakao = "82" + sPhoneNumber_Kakao.Substring(1);

            if (sPhoneNumber_Kakao.Length != 11 && sPhoneNumber_Kakao.Length != 12)
            {
                result.result_code = "-1001";
                result.result_message = "휴대폰 번호를 확인하여 주세요.";
                return result;
            }

			string sendData = "{\"plus_key\":\"" + sKakao_Plus_Key + "\", \"phone_number\":\"" + sPhoneNumber_Kakao + "\", \"temp_user_key\":\"" + tempUserKey + "\"}";
			resultApi = CallKakaoAPI(api_type.add_temp, sendData);

			if(resultApi == null)
			{
				result.result_code = "-1002";
				result.result_message = "API 호출 실패";
				return result;
			}
			else if (resultApi.result_code.Equals("1000"))
			{
				if (resultApi.user_key.Equals(string.Empty))
				{
					result.result_code = "-1003";
					result.result_message = "user_key 반환 실패";
					return result;
				}

				// dac호출
                string addResult = new KakaoTalkServiceDac().InsertReceiver(custNo, chgId, resultApi.user_key, phoneNumber, regChannelCd);

				if (addResult.Equals("0"))
				{
					result = resultApi;
				}
				else
				{
					result = resultApi;
					result.result_code = "-1004";
					result.result_message = "카카오톡 서비스 신청되었으나 지마켓 등록시 실패";
					return result;
				}
			}
			else
			{
				result = resultApi;
			}
			return result;
		}
		#endregion

		#region 인증코드로 친구 등록 요청하기
        public CommonResultT AddUser(string custNo, string phoneNumber, string certCode, string chgId, string regChannelCd)
		{
			CommonResultT resultApi = new CommonResultT();
			CommonResultT result = new CommonResultT();
			string sPhoneNumber_Kakao = string.Empty;

            sPhoneNumber_Kakao = phoneNumber.Replace("-", string.Empty);
            sPhoneNumber_Kakao = "82" + sPhoneNumber_Kakao.Substring(1);

            if (sPhoneNumber_Kakao.Length != 11 && sPhoneNumber_Kakao.Length != 12)
            {
                result.result_code = "-1001";
                result.result_message = "휴대폰 번호를 확인하여 주세요.";
                return result;
            }

			string sendData = "{\"plus_key\":\"" + sKakao_Plus_Key + "\", \"phone_number\":\"" + sPhoneNumber_Kakao + "\", \"cert_code\":\"" + certCode + "\"}";
			resultApi = CallKakaoAPI(api_type.add, sendData);

			if (resultApi == null)
			{
				result.result_code = "-1002";
				result.result_message = "API 호출 실패";
				return result;
			}
			else if (resultApi.result_code.Equals("1000"))
			{
				if (resultApi.user_key.Equals(string.Empty))
				{
					result.result_code = "-1003";
					result.result_message = "user_key 반환 실패";
					return result;
				}

				// dac호출
                string addResult = new KakaoTalkServiceDac().InsertReceiver(custNo, chgId, resultApi.user_key, phoneNumber, regChannelCd);

				if (addResult.Equals("0"))
				{
					result = resultApi;
				}
				else
				{
					result = resultApi;
					result.result_code = "-1004";
					result.result_message = "카카오톡 서비스 신청되었으나 지마켓 등록시 실패";
					return result;
				}
			}
			else
			{
				result = resultApi;
			}
			return result;
		}
		#endregion

		#region 인증코드 발송
		public CommonResultT SendCertCode(string phoneNumber)
		{
			CommonResultT resultApi = new CommonResultT();
			CommonResultT result = new CommonResultT();

            phoneNumber = phoneNumber.Replace("-", string.Empty);
            phoneNumber = "82" + phoneNumber.Substring(1);

            if (phoneNumber.Length != 11 && phoneNumber.Length != 12)
            {
                result.result_code = "-1001";
                result.result_message = "휴대폰 번호를 확인하여 주세요.";
                return result;
            }

			string sendData = "{\"plus_key\":\"" + sKakao_Plus_Key + "\", \"phone_number\":\"" + phoneNumber + "\"}";
			resultApi = CallKakaoAPI(api_type.certCodeSend, sendData);

			if (resultApi == null)
			{
				result.result_code = "-1002";
				result.result_message = "API 호출 실패";
				return result;
			}
			else
			{
				result = resultApi;
			}
			return result;
		}
		#endregion

		#region Kakao API 호출
		private CommonResultT CallKakaoAPI(api_type type, string sendData)
		{
			CommonResultT result = new CommonResultT();
			string methodUrl = string.Empty;
			string sResultText = string.Empty;

			try
			{
				switch (type)
				{
					case api_type.add:
						methodUrl = "v1/users/add_with_cert_code";
						break;
					case api_type.add_temp:
						methodUrl = "v1/users/add_with_temp_user_key";
						break;
					case api_type.remove:
						methodUrl = "v1/users/remove";
						break;
					case api_type.certCodeSend:
						methodUrl = "v1/cert_codes/send";
						break;
					default:
						result.result_code = "-1";
						result.result_message = "api명을 확인하여 주세요.";
						return result;
				}
                                
				sResultText = GetResponseWithPost(m_serverUrl + methodUrl, sendData);

				result = GMKT.Framework.Serialize.GMKTSerializeFormatter.JsonDeserialize<CommonResultT>(sResultText);

                result.result_text = sendData + sResultText;

				if (result.result_code == "1000")
				{
					result.result_message = "성공";
				}
				else if (result.result_code == "2101" || result.result_code == "2102" || result.result_code == "2103" || result.result_code == "2104") //이미 설정 해지된 상태(2101)이면 update해준다.
				{
					result.result_message = "이미 설정 해지된 상태이므로 gMarket Update필요합니다.";
				}
				else
				{
					result.result_message = "실패";
				}
			}
			catch (Exception e)
			{
				result.result_code = "-1";
				result.result_message = "실패(" + e.Message + ")";
			}

			return result;
		}

		/// <summary>
		/// Gets the response with post.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <param name="postData">The post data.</param>
		/// <returns></returns>
		protected string GetResponseWithPost(string sendUrl, string sendData)
		{
			string strReturn = "";
			HttpWebRequest request = null;
			Stream requestStream = null;
			HttpWebResponse response = null;
			StreamReader streamReader = null;
			try
			{
				request = (HttpWebRequest)WebRequest.Create(sendUrl);

				request.Method = "POST";
				byte[] objBytes = Encoding.UTF8.GetBytes(sendData);

				request.ContentType = "application/json; charset=utf-8";// "application/x-www-form-urlencoded";
				request.Accept = "application/json";
				request.ContentLength = objBytes.Length;

				request.Timeout = 30000;
				requestStream = request.GetRequestStream();
				requestStream.Write(objBytes, 0, objBytes.Length);

				//IAsyncResult ar = objRequest.BeginGetResponse(new AsyncCallback(GetScrapingResponse), objRequest);
				//// Wait for request to complete
				//ar.AsyncWaitHandle.WaitOne(1000 * 60 * 3, true);
				//if (objRequest.HaveResponse == false)
				//{
				//   throw new Exception("No Response!!!");
				//}
				//objResponse = (HttpWebResponse)objRequest.EndGetResponse(ar);

				response = (HttpWebResponse)request.GetResponse();
				streamReader = new StreamReader(response.GetResponseStream());
				strReturn = streamReader.ReadToEnd();

			}
			catch (Exception exp)
			{
				throw exp;
			}
			finally
			{
				request = null;
				requestStream = null;
				if (response != null)
					response.Close();
				response = null;
				streamReader = null;
			}
			return strReturn;
		}
		#endregion
	}
}

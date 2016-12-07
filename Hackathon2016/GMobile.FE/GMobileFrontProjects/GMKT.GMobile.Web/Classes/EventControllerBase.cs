using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using GMKT.GMobile.Util;
using GMKT.Web.Mvc;
using GMKT.Framework.Security;
using GMKT.Framework.Constant;

namespace GMKT.GMobile.Web
{
	public class EventControllerBase : GMobileControllerBase
	{
		#region [ Constants ]
		// 
		/// <summary>
		/// 암호화할 각 항목을 구분하는 구분자입니다.
		/// </summary>
		protected static readonly string CRYPT_MAIN_DELIMITER = GMKTConstants.Hex7F;
		/// <summary>
		/// 암호화할 때 뒤에 붙이는 문구입니다.
		/// </summary>
		protected static readonly string CRYPT_MD5_FOOTER = "id6znjen28n5119";

		/// <summary>
		/// CommonApplyEventPlatformGmarket에서 Redirect할 URL입니다.
		/// </summary>
		protected static readonly string COMMON_APPLY_EVENT_PLATFORM_GMARKET_URL = Urls.EventNetRoot + "/eventplatform/Apply";
		#endregion

		/// <summary>
		/// 쿠폰 정보를 가지고 Event Platform으로 Redirect시키는 함수입니다.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="encStr"></param>
		/// <param name="groupYn"></param>
		/// <returns>Redirect한 결과를 RedirectResult로 반환합니다.</returns>
		public RedirectResult CommonApplyEventPlatformGmarket(string str, string encStr, string groupYn, string tabIndex="")
		{
			if (groupYn == "")
				groupYn = "N";

			// 쿠키 설정
			HttpCookie cookie = new HttpCookie("ECif");
			cookie.Value = encStr;
			cookie.Path = "/";
			cookie.Domain = "gmarket.co.kr";
			 
			// 쿠키 추가
			Response.Cookies.Add(cookie);
			string openerURL = "http://" + Request.Url.Host + Url.Action("Index");
			if(!string.IsNullOrEmpty(tabIndex)) openerURL += HttpUtility.UrlEncode("?tabIndex="+tabIndex);
			

			string href = COMMON_APPLY_EVENT_PLATFORM_GMARKET_URL +
				"?epif=" + str +
				"&openerURL=" + openerURL +
				"&groupYn=" + groupYn +
				"&isMobile=Y";

			return Redirect(href);
		}

        /// <summary>
        /// 쿠폰 정보를 가지고 Event Platform으로 Redirect시키는 함수입니다. ,  anchor 추가
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encStr"></param>
        /// <param name="groupYn"></param>
        /// /// <param name="anchor">리턴받을 #ID 엥커값.</param>
        /// <returns>Redirect한 결과를 RedirectResult로 반환합니다.</returns>
        public RedirectResult CommonApplyEventPlatformGmarketAnchor(string str, string encStr, string groupYn , string anchor,string tabIndex = "")
        {
            if (groupYn == "")
                groupYn = "N";

            // 쿠키 설정
            HttpCookie cookie = new HttpCookie("ECif");
            cookie.Value = encStr;
            cookie.Path = "/";
            cookie.Domain = "gmarket.co.kr";

            // 쿠키 추가
            Response.Cookies.Add(cookie);
            string openerURL = "http://" + Request.Url.Host + Url.Action("Index") ;
            if (!string.IsNullOrEmpty(tabIndex)) openerURL += HttpUtility.UrlEncode("?tabIndex=" + tabIndex);


            string href = COMMON_APPLY_EVENT_PLATFORM_GMARKET_URL +
                "?epif=" + str +
                "&openerURL=" + openerURL +
                "&groupYn=" + groupYn +
                "&isMobile=Y"+
                "&anchor=" + anchor 
                ;

            return Redirect(href);
        }

		/// <summary>
		/// 쿠폰 정보를 가지고 Event Platform으로 Redirect시키는 함수입니다.(확장판)
		/// </summary>
		/// <param name="str"></param>
		/// <param name="encStr"></param>
		/// <param name="groupYn"></param>
		/// <returns>Redirect한 결과를 RedirectResult로 반환합니다.</returns>
		public virtual RedirectResult CommonApplyEventPlatformGmarketEx(string str, string encStr, string groupYn, string returlUrl)
		{
			if (groupYn == "")
				groupYn = "N";

			// 쿠키 설정
			HttpCookie cookie = new HttpCookie("ECif");
			cookie.Value = encStr;
			cookie.Path = "/";
			cookie.Domain = "gmarket.co.kr";

			// 쿠키 추가
			Response.Cookies.Add(cookie);

			string href = COMMON_APPLY_EVENT_PLATFORM_GMARKET_URL +
				"?epif=" + str +
				"&openerURL=" + Server.UrlEncode(returlUrl) +
				"&groupYn=" + groupYn +
				"&isMobile=Y";

			return Redirect(href);
		}

		/// <summary>
		/// 이벤트 암호화 스트링 및 쿠키용 암호화 스트링을 생성합니다.
		/// </summary>
		/// <param name="eid">eid</param>
		/// <returns>return[0]: 이벤트 암호화 스트링, return[1]: 쿠키용 암호화 스트링</returns>
		protected string[] EncryptForEventPlatform(int eid)
		{
			string[] result = new string[2];

			string cryptData = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + CRYPT_MAIN_DELIMITER +
						eid + CRYPT_MAIN_DELIMITER +
						gmktUserProfile.CustNo + CRYPT_MAIN_DELIMITER +
						DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + CRYPT_MAIN_DELIMITER;

			result[0] = GMKTCryptoLibrary.AesGCryptoEncrypt(cryptData);
			result[1] = GMKTCryptoLibraryOption.MD5(result[0] + CRYPT_MD5_FOOTER);

			return result;
		}
	}
}
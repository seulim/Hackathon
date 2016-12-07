using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace GMKT.GMobile.Web.Util
{
	public class CookieHelper
    {
		public string GetCookieEncode(string strValue)
		{
			string strBase = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			StringBuilder sb = new StringBuilder();
			foreach (char c in strValue.ToCharArray())
			{
				if (strBase.IndexOf(c) < 0)
				{ // 영문,숫자를 제외한 문자의 경우 인코딩

					byte[] ascii = Encoding.GetEncoding("euc-kr").GetBytes(c.ToString());


					foreach (byte b in ascii)
					{
						string enc = String.Format("{0:x}", b);
						if (enc == "20")
							sb.Append("+");
						else
						{
							sb.Append("%");
							sb.Append(enc.ToUpper());
						}
					}
				}
				else
				{
					sb.Append(c.ToString());
				}
			}

			string strReturn = sb.ToString();
			return strReturn;
		}

		public string GetSingleCookieValue(string Key1)
		{
			HttpContext _context = HttpContext.Current;
			HttpCookieCollection _Cookie = _context.Request.Cookies;
			string rtnValue = string.Empty;

			Key1 = GetCookieEncode(Key1);
			try
			{
				rtnValue = _Cookie[Key1].Value;
			}
			catch { return null; }

			return HttpUtility.UrlDecode(rtnValue, System.Text.Encoding.GetEncoding("euc-kr"));
		}

		public string GetCookieValue(string Key1, string Key2)
		{
			HttpContext _context = HttpContext.Current;
			HttpCookieCollection _Cookie = _context.Request.Cookies;
			string rtnValue = string.Empty;

			//Key1 = GetCookieEncode(Key1);
			//Key2 = GetCookieEncode(Key2);

			try
			{
				rtnValue = _Cookie[Key1][Key2].ToString();
			}
			catch { return null; }

			return HttpUtility.UrlDecode(rtnValue, System.Text.Encoding.GetEncoding("euc-kr"));
		}

		public string SetCookieValue(string Key1, string Key2, string Value, string domain)
		{
			try
			{
				HttpContext _context = HttpContext.Current;
				//Key1 = GetCookieEncode(Key1);
				//Key2 = GetCookieEncode(Key2);
				//Value = GetCookieEncode(Value);
				Value = HttpUtility.UrlEncode(Value, System.Text.Encoding.GetEncoding("euc-kr"));

				HttpCookie MysiteCookie;
				if (_context.Request.Cookies[Key1] == null)
				{

					_context.Response.Cookies[Key1].Domain = domain;
					_context.Response.Cookies[Key1][Key2] = Value;
				}
				else if (_context.Response.Cookies[Key1] != null && !String.IsNullOrEmpty(_context.Response.Cookies[Key1].Value))
				{
					MysiteCookie = _context.Response.Cookies[Key1];
					MysiteCookie.Domain = domain;
					MysiteCookie.Values.Set(Key2, Value);
					_context.Response.Cookies.Set(MysiteCookie);
				}
				else
				{
					MysiteCookie = _context.Request.Cookies[Key1];
					MysiteCookie.Domain = domain;
					MysiteCookie.Values.Set(Key2, Value);


					_context.Response.Cookies.Set(MysiteCookie);
				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}

			return "";
		}

        public string SetNonEncodingCookieValue(string Key1, string Key2, string Value, string domain)
        {
            try
            {
                HttpContext _context = HttpContext.Current;
                //Key1 = GetCookieEncode(Key1);
                //Key2 = GetCookieEncode(Key2);
                //Value = GetCookieEncode(Value);
                //Value = HttpUtility.UrlEncode(Value, System.Text.Encoding.GetEncoding("euc-kr"));

                HttpCookie MysiteCookie;
                if (_context.Request.Cookies[Key1] == null)
                {

                    _context.Response.Cookies[Key1].Domain = domain;
                    _context.Response.Cookies[Key1][Key2] = Value;
                }
                else if (_context.Response.Cookies[Key1] != null && !String.IsNullOrEmpty(_context.Response.Cookies[Key1].Value))
                {
                    MysiteCookie = _context.Response.Cookies[Key1];
                    MysiteCookie.Domain = domain;
                    MysiteCookie.Values.Set(Key2, Value);
                    _context.Response.Cookies.Set(MysiteCookie);
                }
                else
                {
                    MysiteCookie = _context.Request.Cookies[Key1];
                    MysiteCookie.Domain = domain;
                    MysiteCookie.Values.Set(Key2, Value);


                    _context.Response.Cookies.Set(MysiteCookie);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        public string SetCookieValue(string Key1, string Key2, string Value, string domain, DateTime expiredDateTime)
        {
            try
            {
                HttpContext _context = HttpContext.Current;

                Value = HttpUtility.UrlEncode(Value, System.Text.Encoding.GetEncoding("euc-kr"));

                HttpCookie MysiteCookie;
                if (_context.Request.Cookies[Key1] == null)
                {

                    _context.Response.Cookies[Key1].Domain = domain;
                    _context.Response.Cookies[Key1][Key2] = Value;
                    _context.Response.Cookies[Key1].Expires = expiredDateTime;
                }
                else if (_context.Response.Cookies[Key1] != null && !String.IsNullOrEmpty(_context.Response.Cookies[Key1].Value))
                {
                    MysiteCookie = _context.Response.Cookies[Key1];
                    MysiteCookie.Domain = domain;
                    MysiteCookie.Values.Set(Key2, Value);
                    MysiteCookie.Expires = expiredDateTime;

                    _context.Response.Cookies.Set(MysiteCookie);
                }
                else
                {
                    MysiteCookie = _context.Request.Cookies[Key1];
                    MysiteCookie.Domain = domain;
                    MysiteCookie.Values.Set(Key2, Value);
                    MysiteCookie.Expires = expiredDateTime;

                    _context.Response.Cookies.Set(MysiteCookie);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

		public void DelCookie(string cookiename, string domain)
		{
			HttpCookie cookie = System.Web.HttpContext.Current.Response.Cookies[cookiename];
			cookie.Expires = DateTime.Now.AddDays(-1);
			cookie.Domain = domain;
		}
    }
}

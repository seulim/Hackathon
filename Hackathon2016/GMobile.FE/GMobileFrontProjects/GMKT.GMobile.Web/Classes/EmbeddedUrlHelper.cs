using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMKT.GMobile.Web
{
    public static class EmbeddedUrlHelper
    {
        public static string GetLinkIsEmbedded(string scheme, string targeturl, string type, string title, string addparam, string addparamforapp, PageAttr pageAttr)
        {
            string url = null;
            
            if (pageAttr.IsAndroidApp == true || pageAttr.IsIphoneApp == true)
            {
                if (string.IsNullOrEmpty(type) == false)
                {
                    url = string.Format("gmarket://{0}?type={1}&title={2}&targeturl={3}",
                        scheme,
                        type,
                        HttpUtility.UrlEncode(title),
                        HttpUtility.UrlEncode(targeturl));
                }
                else
                {
                    url = string.Format("gmarket://{0}?title={1}&targeturl={2}",
                        scheme,
                        HttpUtility.UrlEncode(title),
                        HttpUtility.UrlEncode(targeturl));
                }
                
                if (false == string.IsNullOrEmpty(addparamforapp))
                {
                    url += "&" + addparamforapp;
                }
            }
            else
            {
                string deli = "&";
                string hash = string.Empty;
                if (false == string.IsNullOrEmpty(addparam))
                {
                    if (0 <= targeturl.IndexOf(addparam))
                    {
                        addparam = string.Empty;
                    }
                }
                if (0 <= targeturl.IndexOf("#"))
                {
                    hash = targeturl.Substring(targeturl.IndexOf("#"));
                    targeturl = targeturl.Substring(0, targeturl.IndexOf("#"));
                }
                if (0 > targeturl.IndexOf("?"))
                {
                    deli = "?";
                }
                url = string.Format("{0}{1}{2}",
                    targeturl,
                    (false == string.IsNullOrEmpty(addparam)) ? deli + addparam : string.Empty,
                    hash);
            }

            return url;
        }
    }
}
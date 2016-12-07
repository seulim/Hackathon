using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

using GMKT.GMobile.Util;
using System.Collections.Specialized;


namespace GMKT.GMobile.Data
{
	//[Serializable]
	//public class ApiResponse<TData>
	//{
	//    public string Message { get; set; }
	//    public int ResultCode { get; set; }
	//    public TData Data { get; set; }
	//}

	//public class ApiParameter
	//{
	//    public ApiParameter(string name, object value)
	//    {
	//        this.Name = name;
	//        this.Value = value.ToString();
	//    }
	//    public string Name { get; set; }
	//    public object Value { get; set; }
	//    public string GenerateParameter()
	//    {
	//        return string.Format("{0}={1}", Name, Value);
	//    }
	//}

	//public class MobileAPIHelper
	//{
	//    public static string METHOD_GET = @"GET";

	//    public ApiParameter CreateParameter(string paramName, object paramValue)
	//    {
	//        ApiParameter param = new ApiParameter(paramName, paramValue);
	//        return param;
	//    }
				
	//    public ApiResponse<TData> ExecuteAPI<TData>(string apiUrl, params ApiParameter[] parameters)
	//        where TData : new()
	//    {
	//        JavaScriptSerializer serializer = new JavaScriptSerializer();
	//        var url = Urls.GMApiUrl + apiUrl;

	//        StringBuilder sb = new StringBuilder();
	//        if (parameters != null && parameters.Count() > 0)
	//        {
	//            foreach (ApiParameter p in parameters)
	//            {
	//                if (sb.Length > 0)
	//                    sb.Append("&");

	//                sb.Append(p.GenerateParameter());
	//            }
	//            url = String.Format("{0}?{1}", url, sb.ToString());
	//        }

	//        ApiResponse<TData> response = new ApiResponse<TData>();

	//        HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
	//        req.Method = METHOD_GET;
	//        req.ContentType = "application/json";
	//        req.Accept = "application/json";

	//        WebResponse webResponse = req.GetResponse();
	//        using (Stream webStream = webResponse.GetResponseStream())
	//        {
	//            if (webStream != null)
	//            {
	//                using (StreamReader responseReader = new StreamReader(webStream))
	//                {
	//                    string tmp = responseReader.ReadToEnd();
	//                    response = serializer.Deserialize<ApiResponse<TData>>(tmp);
	//                    responseReader.Close();
	//                }
	//                webStream.Close();
	//            }
	//            else
	//            {
	//                //
	//            }
	//        }
	//        webResponse.Close();
						
	//        return response;
	//    }

	//}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMobile.Data;

using System.IO;
using System.Net;
using GMKT.GMobile.Util;
using GMKT.GMobile.Data;

using ConnApi.Client;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Data
{
	public class CategoryApiDac : ApiBase
	{
		public CategoryApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<List<GroupCategoryInfo>> GetGroupCategory()
		{
			ApiResponse<List<GroupCategoryInfo>> result = ApiHelper.CallAPI<ApiResponse<List<GroupCategoryInfo>>>(
				"GET",
				ApiHelper.MakeUrl("api/category/GetGroupCategory")
			);
			return result;
		}

		public ApiResponse<List<GroupCategoryInfo>> GetGmarketBestByCategory( string lcId )
		{
			ApiResponse<List<GroupCategoryInfo>> result = ApiHelper.CallAPI<ApiResponse<List<GroupCategoryInfo>>>(
				"GET",
				ApiHelper.MakeUrl("api/category/GetGroupCategory")
			);

			return result;
		}

		public ApiResponse<SpecialShopModel> GetSpecialShopList(int pageNo, int pageSize, string lcId = "")
		{
			ApiResponse<SpecialShopModel> result = ApiHelper.CallAPI<ApiResponse<SpecialShopModel>>(
				"GET",
				ApiHelper.MakeUrl("api/Search/GetSpecialShopList"),
				new
				{
					pageNo = pageNo,
					pageSize = pageSize,
					lcId = lcId
				}
			);

			return result;
		}
	}

	//public class CategoryApiDac : MobileAPIDacBase
	//{
	//    public readonly string LOCAL_API_URL = "gmapidev.gmarket.co.kr";
	//    public readonly int LOCAL_API_PORT = 80;
	//    public readonly string DEBUG_MODE = "debugMode";
        
	//    public static Encoding utf8 = Encoding.UTF8;

	//    public static string METHOD_GET = @"GET";
	//    public static string METHOD_PUT = @"PUT";
	//    public static string METHOD_POST = @"POST";
	//    public static string METHOD_DELETE = @"DELETE";

	//    //http://gmapidev.gmarket.co.kr/api/Best/GetBest100GroupCategoryInfo

	//    public string GetBest100GroupCategoryInfo()
	//    {
            
	//        //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
	//        //var url = Urls.GMApiUrl + "/api/Best/GetBest100GroupCategoryInfo";

	//        //var res = new GetMobileHomeTotalList();

	//        //HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
	//        //req.Method = METHOD_GET;
	//        //req.ContentType = "application/json";
	//        //req.Accept = "application/json";

	//        //try
	//        //{
	//        //    WebResponse webResponse = req.GetResponse();
	//        //    using (Stream webStream = webResponse.GetResponseStream())
	//        //    {
	//        //        if (webStream != null)
	//        //        {
	//        //            using (StreamReader responseReader = new StreamReader(webStream))
	//        //            {
	//        //                string tmp = responseReader.ReadToEnd();
	//        //                res = serializer.Deserialize<GetMobileHomeTotalList>(tmp);
	//        //                responseReader.Close();
	//        //            }
	//        //            webStream.Close();
	//        //        }
	//        //        else
	//        //        {
	//        //            //
	//        //        }
	//        //    }
	//        //    webResponse.Close();
	//        //}
	//        //catch (Exception ex)
	//        //{
	//        //    ArcheFx.Diagnostics.Trace.WriteError(ex); // logging
	//        //    res = new GetMobileHomeTotalList();
	//        //}

	//        //if (res.ResultCode != 0) ArcheFx.Diagnostics.Trace.WriteLine("API ERROR: " + res.ResultCode.ToString());
	//        //return res;
	//        return "";
	//    }

	//    public ApiResponse<List<GroupCategoryInfo>> GetGroupCategory()
	//    {
	//        ApiResponse<List<GroupCategoryInfo>> result = ApiHelper.ExecuteAPI<List<GroupCategoryInfo>>(
	//            "/api/category/GetGroupCategory"
	//        );
	//        return result;
	//    }
	//}


}

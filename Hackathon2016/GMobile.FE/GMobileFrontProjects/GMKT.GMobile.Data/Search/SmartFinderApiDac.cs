using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.GMobile.Data.Search;
using System.Web;

namespace GMKT.GMobile.Data
{
	public class SmartFinderApiDac : ApiBase
	{
		public SmartFinderApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<List<SmartFinderMClass>> GetSmartFinderMClassList(string lClassSeq)
		{
			ApiResponse<List<SmartFinderMClass>> result = ApiHelper.CallAPI<ApiResponse<List<SmartFinderMClass>>>(
				"GET",
				ApiHelper.MakeUrl("api/SmartFinder/GetSmartFinderMClassList"),
				new
				{
					lClassSeq = lClassSeq
				}
			);

			return result;
		}

		public ApiResponse<List<SmartFinderSClass>> GetSmartFinderSClassList(string mClassSeq)
		{
			ApiResponse<List<SmartFinderSClass>> result = ApiHelper.CallAPI<ApiResponse<List<SmartFinderSClass>>>(
				"GET",
				ApiHelper.MakeUrl("api/SmartFinder/GetSmartFinderSClassList"),
				new
				{
					mClassSeq = mClassSeq
				}
			);

			return result;
		}
	}
}

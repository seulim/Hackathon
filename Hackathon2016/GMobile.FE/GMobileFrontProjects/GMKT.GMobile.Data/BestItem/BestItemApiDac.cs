using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data
{
	public class BestItemApiDac : ApiBase
	{
		public BestItemApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<List<Best100GroupCateogyDetail>> GetBest100GroupCategoryInfo()
		{
			ApiResponse<List<Best100GroupCateogyDetail>> result = ApiHelper.CallAPI<ApiResponse<List<Best100GroupCateogyDetail>>>(
				"GET",
				ApiHelper.MakeUrl("api/best/GetBest100GroupCategoryInfo")
			);
			return result;
		}

		public ApiResponse<List<Best100CateogyDetail>> GetBest100CategoryList()
		{
			ApiResponse<List<Best100CateogyDetail>> result = ApiHelper.CallAPI<ApiResponse<List<Best100CateogyDetail>>>(
				"GET",
				ApiHelper.MakeUrl("api/best/GetBest100CategoryList")
			);
			return result;
		}

		public ApiResponse<List<SearchItemModel>> GetBest100Items(int pageNo, int pageSize, bool forMobile)
		{
			ApiResponse<List<SearchItemModel>> result = ApiHelper.CallAPI<ApiResponse<List<SearchItemModel>>>(
				"GET",
				ApiHelper.MakeUrl("api/best/GetBest100Items"),
				new
				{
					pageNo = pageNo,
					pageSize = pageSize,
					forMobile = forMobile
				}
			);
			return result;
		}

		public ApiResponse<SearchResultModel> GetBest100GroupItems(string groupCode, int pageNo, int pageSize)
		{
			ApiResponse<SearchResultModel> result = ApiHelper.CallAPI<ApiResponse<SearchResultModel>>(
				"GET",
				ApiHelper.MakeUrl("api/best/GetBest100GroupItems"),
				new
				{
					code = groupCode,
					pageNo = pageNo,
					pageSize = pageSize
				}
			);
			return result;
		}

		public ApiResponse<SearchResultModel> GetBest100CategoryItems(string code, int pageNo, int pageSize)
		{
			ApiResponse<SearchResultModel> result = ApiHelper.CallAPI<ApiResponse<SearchResultModel>>(
				"GET",
				ApiHelper.MakeUrl("api/best/GetBest100CategoryItems"),
				new
				{
					code = code,
					pageNo = pageNo,
					pageSize = pageSize
				}
			);
			return result;
		}

		public GmarketBestItemResponse GetGmarketBest100Items(string lcId, int pageNo, int pageSize)
		{
			GmarketBestItemResponse result = ApiHelper.CallAPI<GmarketBestItemResponse>(
				"GET",
				ApiHelper.MakeUrl("api/search/GetGmarketBest100Items"),
				new
				{
					lcId = lcId,
					pageNo = pageNo,
					pageSize = pageSize
				}
			);
			return result;
		}

		public ApiResponse<Best100Main> GetBest100Main(string code = "")
		{
			ApiResponse<Best100Main> result = ApiHelper.CallAPI<ApiResponse<Best100Main>>(
				"GET",
				ApiHelper.MakeUrl("api/best/GetBest100Main"),
				new
				{
					code = code
					, forMobile = true
				}
			);
			return result;
		}
	}

	//public class BestItemApiDac : MobileAPIDacBase
	//{
	//    public ApiResponse<List<Best100GroupCateogyDetail>> GetBest100GroupCategoryInfo()
	//    {
	//        ApiResponse<List<Best100GroupCateogyDetail>> result = ApiHelper.ExecuteAPI<List<Best100GroupCateogyDetail>>(
	//            "/api/best/GetBest100GroupCategoryInfo"
	//        );
	//        return result;
	//    }

	//    public ApiResponse<List<Best100CateogyDetail>> GetBest100CategoryList()
	//    {
	//        ApiResponse<List<Best100CateogyDetail>> result = ApiHelper.ExecuteAPI<List<Best100CateogyDetail>>(
	//            "/api/best/GetBest100CategoryList"
	//        );
	//        return result;
	//    }

	//    public ApiResponse<List<SearchItemModel>> GetBest100Items()
	//    {
	//        ApiResponse<List<SearchItemModel>> result = ApiHelper.ExecuteAPI<List<SearchItemModel>>(
	//            "/api/best/GetBest100Items"
	//        );
	//        return result;
	//    }

	//    public ApiResponse<List<SearchItemModel>> GetBest100GroupItems(string groupCode, int pageNo, int pageSize)
	//    {
	//        ApiResponse<List<SearchItemModel>> result = ApiHelper.ExecuteAPI<List<SearchItemModel>>(
	//            "/api/best/GetBest100GroupItems",
	//            ApiHelper.CreateParameter("groupCode", groupCode),
	//            ApiHelper.CreateParameter("pageNo", pageNo),
	//            ApiHelper.CreateParameter("pageSize", pageSize)
	//        );
	//        return result;
	//    }
	//}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Data
{
	public class BizOnApiDac : ApiBase
	{
		public BizOnApiDac() : base("GMApi") { }

		public ApiResponse<BizOnHome> GetHome()
		{
			ApiResponse<BizOnHome> result = ApiHelper.CallAPI <ApiResponse<BizOnHome>>(
				"GET",
				ApiHelper.MakeUrl("api/BizOn/GetHome")
			);
			return result;
		}

		public ApiResponse<List<BizOnItem>> GetBizOnBest(int pageNo, int pageSize)
		{
			ApiResponse<List<BizOnItem>> result = ApiHelper.CallAPI<ApiResponse<List<BizOnItem>>>(
				"GET",
				ApiHelper.MakeUrl("api/BizOn/GetBestItem"),
				new
				{
					pageNo = pageNo,
					pageSize = pageSize
				}
			);
			return result;
		}

		public ApiResponse<List<BizOnCategoryT>> GetBizOnCategory(BizOnCategoryType categoryType)
		{
			ApiResponse<List<BizOnCategoryT>> result = ApiHelper.CallAPI<ApiResponse<List<BizOnCategoryT>>>(
				"GET",
				ApiHelper.MakeUrl("api/BizOn/GetCategory"),
				new
				{
					categoryType = (int)categoryType,
				}
			);
			return result;
		}

		public ApiResponse<List<BizOnCategoryT>> GetBizOnCategory(BizOnCategoryType categoryType, string parentCode)
		{
			ApiResponse<List<BizOnCategoryT>> result = ApiHelper.CallAPI<ApiResponse<List<BizOnCategoryT>>>(
				"GET",
				ApiHelper.MakeUrl("api/BizOn/GetCategory"),
				new
				{
					categoryType = (int)categoryType,
					parentCode = String.IsNullOrEmpty(parentCode) ? "" : parentCode
				}
			);
			return result;
		}

		public ApiResponse<List<CPPLPSRPItemModel>> GetBizOnMileageItems(string shopCategory, int pageNo, int pageSize)
		{
			ApiResponse<List<CPPLPSRPItemModel>> result = ApiHelper.CallAPI<ApiResponse<List<CPPLPSRPItemModel>>>(
				"GET",
				ApiHelper.MakeUrl("api/BizOn/GetSearchGoods"),
				new
				{
					shopCategory = shopCategory,
					isBizOnMileage = "Y",
					pageNo = pageNo,
					pageSize = pageSize
				}
			);
			return result;
		}

		public ApiResponse<List<CPPLPSRPItemModel>> GetBizOnMileageItems(string shopCategory, string keyword, int pageNo, int pageSize)
		{
			ApiResponse<List<CPPLPSRPItemModel>> result = ApiHelper.CallAPI<ApiResponse<List<CPPLPSRPItemModel>>>(
				"GET",
				ApiHelper.MakeUrl("api/BizOn/GetSearchGoods"),
				new
				{
					keyword = keyword,
					isBizOnMileage = "Y",
					pageNo = pageNo,
					pageSize = pageSize
				}
			);
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data
{
	public class DepartmentStoreApiDac : ApiBase
	{
		public DepartmentStoreApiDac() : base("GMApi") { }

		public ApiResponse<List<DepartmentStoreMainGroupT>> GetDepartmentStoreMain()
		{
			ApiResponse<List<DepartmentStoreMainGroupT>> result = ApiHelper.CallAPI<ApiResponse<List<DepartmentStoreMainGroupT>>>(
				"GET",
				ApiHelper.MakeUrl("api/DepartmentStore/GetDepartmentStoreMainV2"),
				new
				{

				}
			);
			return result;
		}

		public ApiResponse<List<DepartmentStoreGroupCategoryT>> GetDepartmentStoreCategoryGroup()
		{
			ApiResponse<List<DepartmentStoreGroupCategoryT>> result = ApiHelper.CallAPI<ApiResponse<List<DepartmentStoreGroupCategoryT>>>(
				"GET",
				ApiHelper.MakeUrl("api/DepartmentStore/GetDepartmentStoreCategory")
			);
			return result;
		}

		public ApiResponse<DepartmentStoreBestT> GetDepartmentStoreBest(int pageNo, int pageSize)
		{
			ApiResponse<DepartmentStoreBestT> result = ApiHelper.CallAPI<ApiResponse<DepartmentStoreBestT>>(
				"GET",
				ApiHelper.MakeUrl("api/DepartmentStore/GetDepartmentStoreBest")
				, new 
				{
					pageNo
					, pageSize
				}
			);
			return result;
		}

		public ApiResponse<DepartmentStoreNowT> GetDepartmentStoreNow(string category)
		{
			ApiResponse<DepartmentStoreNowT> result = ApiHelper.CallAPI<ApiResponse<DepartmentStoreNowT>>(
				"GET",
				ApiHelper.MakeUrl("api/DepartmentStore/GetDepartmentStoreNow"),
				new { category = category }
			);
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class DepartmentStoreApiBiz 
	{
		public List<DepartmentStoreMainGroupT> GetDepartmentStoreMain()
		{
			List<DepartmentStoreMainGroupT> result = new List<DepartmentStoreMainGroupT>();

			ApiResponse<List<DepartmentStoreMainGroupT>> response = new DepartmentStoreApiDac().GetDepartmentStoreMain();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null)
			{
				result = new List<DepartmentStoreMainGroupT>();
			}

			return result;
		}

		public List<DepartmentStoreGroupCategoryT> GetDepartmentStoreCategoryGroup()
		{
			ApiResponse<List<DepartmentStoreGroupCategoryT>> response = new DepartmentStoreApiDac().GetDepartmentStoreCategoryGroup();
			return GenericUtil.AtLeastReturnEmptyList(response.Data);
		}

		public DepartmentStoreBestT GetDepartmentStoreBest(int pageNo, int pageSize)
		{
			ApiResponse<DepartmentStoreBestT> response = new DepartmentStoreApiDac().GetDepartmentStoreBest(pageNo, pageSize);
			return GenericUtil.AtLeastReturnNewObjct(response.Data);
		}

		public DepartmentStoreNowT GetDepartmentStoreNow(string category)
		{
			ApiResponse<DepartmentStoreNowT> response = new DepartmentStoreApiDac().GetDepartmentStoreNow(category);
			return GenericUtil.AtLeastReturnNewObjct(response.Data);
		}
	}
}

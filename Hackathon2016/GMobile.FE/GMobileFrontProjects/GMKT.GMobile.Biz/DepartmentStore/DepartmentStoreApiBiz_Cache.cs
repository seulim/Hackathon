using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class DepartmentStoreApiBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 60)]
		public List<DepartmentStoreMainGroupT> GetDepartmentStoreMain()
		{
			return new DepartmentStoreApiBiz().GetDepartmentStoreMain();
		}

		[CacheDuration(DurationSeconds = 600)]
		public List<DepartmentStoreGroupCategoryT> GetDepartmentStoreCategoryGroup()
		{
			return new DepartmentStoreApiBiz().GetDepartmentStoreCategoryGroup();
		}

		private const int DEFAULT_SIZE_FOR_SRP_NO_RESULT = 10;
		[CacheDuration(DurationSeconds = 300)]
		public DepartmentStoreBestT GetDepartmentBestItem()
		{
			return GenericUtil.AtLeastReturnNewObjct(new DepartmentStoreApiBiz().GetDepartmentStoreBest(1, DEFAULT_SIZE_FOR_SRP_NO_RESULT));
		}

		[CacheDuration(DurationSeconds = 60)]
		public DepartmentStoreNowT GetDepartmentStoreNow(string category)
		{
			return new DepartmentStoreApiBiz().GetDepartmentStoreNow(category);
		}
	}
}

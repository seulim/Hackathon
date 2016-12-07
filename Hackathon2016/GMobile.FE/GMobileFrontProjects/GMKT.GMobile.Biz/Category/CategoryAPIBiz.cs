using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Biz.Category
{
	public class CategoryAPIBiz
	{
		private CategoryApiDac apidac = null;

		public CategoryApiDac APIDac
		{
			get
			{
				if (apidac == null)
					apidac = new CategoryApiDac();

				return apidac;
			}
		}

		public SpecialShopModel GetSpecialShopList(int pageNo , int pageSize, string lcId  )
		{
			SpecialShopModel result = new SpecialShopModel();

			ApiResponse<SpecialShopModel> response =
				APIDac.GetSpecialShopList( pageNo, pageSize, lcId );

			if (response != null && response.Data != null)
			{
				result = response.Data;
			}

			return result;
		}
	}
}

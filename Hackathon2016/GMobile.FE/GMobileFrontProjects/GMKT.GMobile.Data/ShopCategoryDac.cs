using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using GMKT.Framework.EnterpriseServices;
using GMKT.Framework.Cache;


namespace GMKT.GMobile.Data
{
	public class ShopCategoryDac : MicroDacBase
	{
		[GMKTCache(GMKTCacheType.LocalCache, "ShopCategoriesCache", 1)]
		public List<ShopCategoryT> SelectShopCategories(string sellerId)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ShopCategoryT>(
				"minishop_read",
				"dbo.UP_GMKTNet_ShopCategory_SelectShopCategories",
				MicroDacHelper.CreateParameter("@cust_no", sellerId, SqlDbType.VarChar, 10));
		}

		[GMKTCache(GMKTCacheType.LocalCache, "ShopCategoryProductCache", 1)]
		public List<ShopCategoryItemT> SelectShopCategoryItems(string shopCategoryCode, string sellerId)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ShopCategoryItemT>(
				"minishop_read",
				"dbo.UP_GMKTNet_ShopCategory_SelectShopCategoryProducts",
				MicroDacHelper.CreateParameter("@shop_category_code", shopCategoryCode, SqlDbType.Char, 8),
				MicroDacHelper.CreateParameter("@cust_no", sellerId, SqlDbType.VarChar, 10));
		}

		[GMKTCache(GMKTCacheType.LocalCache, "ShopCategoryInfoCache", 1)]
		public List<ShopCategoryInfoT> SelectShopCategoryInfoList(string sellerId, string categoryType)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ShopCategoryInfoT>(
				"minishop_read",
				"dbo.UP_GMKTNet_ShopCategory_SelectShopCategoryInfos",
				MicroDacHelper.CreateParameter("@cust_no", sellerId, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@category_type", categoryType, SqlDbType.Char, 1));
		}
	}
}

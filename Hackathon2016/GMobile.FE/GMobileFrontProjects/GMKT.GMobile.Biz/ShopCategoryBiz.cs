using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheFx.EnterpriseServices;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	[Transaction(TransactionOption.NotSupported)]
	public class ShopCategoryBiz : BizBase
	{
		/// <summary>
		/// 샵 카테고리 정보 조회
		/// </summary>
		/// <param name="sellerId">판매자 고객 번호</param>
		/// <returns></returns>
		public IEnumerable<ShopCategoryT> GetShopCategories(string sellerId)
		{
			return new ShopCategoryDac().SelectShopCategories(sellerId);
		}

		/// <summary>
		/// 샵 카테고리 분류별 조회
		/// </summary>
		/// <param name="sellerId">판매자 고객 번호</param>
		/// <param name="categoryLevel">카테고리 분류 단계</param>
		/// <returns></returns>
		public IEnumerable<ShopCategoryT> GetShopCategories(string sellerId, CategoryLevel categoryLevel)
		{
			return GetShopCategories(sellerId).Where(x => x.Level == categoryLevel);
		}

		public IEnumerable<ShopCategoryT> GetShopCategories(string sellerId, ShopCategoryDisplayType displayType)
		{
			if (displayType == ShopCategoryDisplayType.MediumCategoryOnly)
			{
				return GetShopCategories(sellerId, CategoryLevel.MediumCategory);
			}
			else
			{
				return GetShopCategories(sellerId, CategoryLevel.LargeCategory);
			}
		}

		public ShopCategoryT GetShopCategory(string categoryCode, string sellerId)
		{
			return GetShopCategories(sellerId).Where(x => x.CategoryCode == categoryCode).SingleOrDefault();
		}

		public List<ShopCategoryT> GetShopCategoryChildren(string categoryCode, string sellerId)
		{
			return GetShopCategories(sellerId).Where(x => x.ParentCategoryCode == categoryCode).ToList();
		}

		/// <summary>
		/// 샵 카테고리 상품 개수 조회
		/// </summary>
		/// <param name="shopCategoryCode">샵 카테고리 코드</param>
		/// <param name="sellerId">판매자 고객 번호</param>
		/// <returns></returns>
		public int CountShopCategoryProduct(string shopCategoryCode, string sellerId)
		{
			return CountShopCategoryProduct(shopCategoryCode, sellerId, 0);
		}

		private int CountShopCategoryProduct(string shopCategoryCode, string sellerId, int count)
		{
			IEnumerable<ShopCategoryT> shopCategories = GetShopCategories(sellerId);

			ShopCategoryT shopCategory = shopCategories.Where(x => x.CategoryCode == shopCategoryCode).SingleOrDefault();

			if (shopCategory != null && shopCategory.LeafYN == "Y")
			{
				return new ShopCategoryDac().SelectShopCategoryItems(shopCategoryCode, sellerId).Count;
			}
			else
			{
				IEnumerable<ShopCategoryT> childCategories = shopCategories.Where(x => x.ParentCategoryCode == shopCategoryCode).Select(x => x);

				foreach (ShopCategoryT childCategory in childCategories)
				{
					count += CountShopCategoryProduct(childCategory.CategoryCode, sellerId, count) + new ShopCategoryDac().SelectShopCategoryItems(shopCategoryCode, sellerId).Count;
				}

				return count;
			}
		}

		public List<ShopCategoryInfoT> GetShopCategoryInfoList(string sellerId, CategoryType categoryType)
		{
			List<ShopCategoryInfoT> shopCategoryInfoList = null;

			if (categoryType == CategoryType.General)
			{
				shopCategoryInfoList = new ShopCategoryDac().SelectShopCategoryInfoList(sellerId, "N");
			}
			else
			{
				shopCategoryInfoList = new ShopCategoryDac().SelectShopCategoryInfoList(sellerId, "S");
			}
			if (shopCategoryInfoList == null) shopCategoryInfoList = new List<ShopCategoryInfoT>();

			return shopCategoryInfoList;
		}
	}
}

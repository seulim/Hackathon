using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;
using GMobile.Data.Voyager;
using Arche.Data.Voyager;
using GMobile.Service.Search;

namespace GMKT.GMobile.Biz
{
	public class ShopSearchBiz : BizBase
	{
		public ShopSearchBiz()
		{

		}

		#region 판매자의 상품정보만 검색
		public SearchItemT[] GetSellerItem(string sellerCustNo, string keyword, int pageNo, int pageSize, DisplayOrder sort)
		{
			if (IsValidParameters(sellerCustNo, pageNo, pageSize) == false)
			{
				return new SearchItemT[0];
			}

			return new SearchBiz().GetSearchItem(keyword, sellerCustNo, "", "", "", pageNo, pageSize, sort);
		}

		public SearchItemT[] GetSellerItem(string sellerCustNo, string keyword, string category, bool isShopCategory, int pageNo, int pageSize, DisplayOrder sort, GMKT.Web.Membership.ExIdAppEnum exid = GMKT.Web.Membership.ExIdAppEnum.None)
		{
			if (IsValidParameters(sellerCustNo, pageNo, pageSize) == false)
			{
				return new SearchItemT[0];
			}

			if (isShopCategory)
			{
				return new SearchBiz().GetSearchItem(keyword, sellerCustNo, category, pageNo, pageSize, sort, exid);
			}
			else 
			{
				return GetSellerItem(sellerCustNo, keyword, category, pageNo, pageSize, sort, exid);
			}
		}

		protected SearchItemT[] GetSellerItem(string sellerCustNo, string keyword, string category, int pageNo, int pageSize, DisplayOrder sort, GMKT.Web.Membership.ExIdAppEnum exid = GMKT.Web.Membership.ExIdAppEnum.None)
		{
			if (IsValidParameters(sellerCustNo, pageNo, pageSize) == false)
			{
				return new SearchItemT[0];
			}

			string gdlc = "";
			string gdmc = "";
			string gdsc = "";

			//카테고리정보
			if (!String.IsNullOrEmpty(category))
			{
				CategoryInfoT cateinfo = CategoryBiz.GetCategoryInfo(category);
				switch (cateinfo.Level)
				{
					case CategoryLevel.LargeCategory:
						gdlc = category;
						break;
					case CategoryLevel.MediumCategory:
						gdmc = category;
						break;
					case CategoryLevel.SmallCategory:
						gdsc = category;
						break;
					default:
						gdlc = category;
						break;
				}
			}

			return new SearchBiz().GetSearchItem(keyword, sellerCustNo, gdlc, gdmc, gdsc, pageNo, pageSize, sort, exid);
		}

		public SearchItemT[] GetSellerItem(string sellerCustNo, string keyword, string gdlc, string gdmc, string gdsc, int pageNo, int pageSize, DisplayOrder sort)
		{
			if (IsValidParameters(sellerCustNo, pageNo, pageSize) == false)
			{
				return new SearchItemT[0];
			}

			return new SearchBiz().GetSearchItem(keyword, sellerCustNo, gdlc, gdmc, gdsc, pageNo, pageSize, sort);
		}
		#endregion

		#region 판매자의 상품정보 및 카테고리 정보 검색
		public SearchResultT GetSearchResult(string sellerCustNo, string keyword, int pageNo, int pageSize, DisplayOrder sort)
		{
			if (IsValidParameters(sellerCustNo, pageNo, pageSize) == false)
			{
				SearchResultT result = new SearchResultT();
				result.Items = new SearchItemT[0];
				result.CategoryInfo = new List<CategoryInfoT>();
				result.TotalCount = 0;
			}
				
			return new SearchBiz().GetSearchResult(keyword, sellerCustNo, "", "", "", pageNo, pageSize, sort);
		}

		public SearchResultT GetSearchResult(string sellerCustNo, string keyword, string category, bool isShopCategory, int pageNo, int pageSize, DisplayOrder sort)
		{
			if (IsValidParameters(sellerCustNo, pageNo, pageSize) == false)
			{
				SearchResultT result = new SearchResultT();
				result.Items = new SearchItemT[0];
				result.CategoryInfo = new List<CategoryInfoT>();
				result.TotalCount = 0;
				return result;
			}

			if (isShopCategory == true)
			{
				return GetSearchResult(sellerCustNo, keyword, category, pageNo, pageSize, SearchBiz.GetSortCollection(sort));
			}
			else
			{
				return GetSearchResult(sellerCustNo, keyword, category, pageNo, pageSize, sort);
			}
			

		}

		protected SearchResultT GetSearchResult(string sellerCustNo, string keyword, string shopcategory, int pageNo, int pageSize, SortCollection sort)
		{
			SearchResultT result = new SearchResultT();

			IEnumerable<ShopCategoryT> shopCategories = new ShopCategoryDac().SelectShopCategories(sellerCustNo);

			ItemFilter filter = new ItemFilter();
			// 키워드
			if (!String.IsNullOrEmpty(keyword) && keyword.Length > 0)
			{
				filter.ItemName = keyword;
			}
			else
			{
				if (String.IsNullOrEmpty(shopcategory))
				{
					List<string> shopCategoryCodes = new List<string>();
					foreach (ShopCategoryT shopcate in shopCategories)
					{
						if (shopcate != null && !String.IsNullOrEmpty(shopcate.CategoryCode))
						{
							shopCategoryCodes.Add(shopcate.CategoryCode);
						}
					}
					filter.MiniShopCategoryOrList = shopCategoryCodes;
				}
			}

			// 판매자 번호
			if (!String.IsNullOrEmpty(sellerCustNo) && sellerCustNo.Length > 0)
			{
				filter.SellCustNo = sellerCustNo;
			}

			if (!String.IsNullOrEmpty(shopcategory) && shopcategory.Length > 0)
			{
				filter.MiniShopCategory = shopcategory;
			}

			ShopCategoryT shopCategory = shopCategories.Where<ShopCategoryT>(c => c.CategoryCode == shopcategory).FirstOrDefault<ShopCategoryT>();

			HistogramQueryCollection hqc = new HistogramQueryCollection();
			string CategoryGroupField1 = "";
			if (shopCategory != null)
			{
				switch (shopCategory.Level)
				{
					case CategoryLevel.LargeCategory:
						CategoryGroupField1 = Tables.ItemTable.MiniShopCategoryMCode;
						break;
					default:
						break;
				}

			}

			if (!String.IsNullOrEmpty(CategoryGroupField1))
			{
				hqc.Add(new HistogramQuery(CategoryGroupField1, HistogramSortOrder.Decrc));
			}

			QueryResult queryResult = new SearchItemBiz().GetQueryResult(filter, (pageNo - 1) * pageSize, pageSize, sort, hqc);

			if (queryResult != null)
			{
				Histogram hs1 = null;

				if (!String.IsNullOrEmpty(CategoryGroupField1))
				{
					hs1 = (Histogram)queryResult.Histograms[CategoryGroupField1];
				}

				if (hs1 != null)
				{
					List<CategoryInfoT> categoryList = new List<CategoryInfoT>();

					foreach (HistogramEntry entry in hs1)
					{
						if (entry != null)
						{
							ShopCategoryT sc = shopCategories.Where<ShopCategoryT>(c => c.CategoryCode == entry.Value.ToString("00000000")).FirstOrDefault<ShopCategoryT>();

							if (sc != null)
							{
								CategoryInfoT category = new CategoryInfoT();
								category.Id = sc.CategoryCode;
								category.Name = sc.Name;
								category.Level = sc.Level;
								category.ItemCount = entry.Count;
								categoryList.Add(category);
							}
						}
					}
					result.CategoryInfo = categoryList;
				}

				result.Items = (SearchItemT[])queryResult.Entities;
				result.TotalCount = queryResult.Size;
			}

			return result;
		}

		protected SearchResultT GetSearchResult(string sellerCustNo, string keyword, string category, int pageNo, int pageSize, DisplayOrder sort)
		{
			if (IsValidParameters(sellerCustNo, pageNo, pageSize) == false)
			{
				SearchResultT result = new SearchResultT();
				result.Items = new SearchItemT[0];
				result.CategoryInfo = new List<CategoryInfoT>();
				result.TotalCount = 0;
				return result;
			}

			string gdlc = "";
			string gdmc = "";
			string gdsc = "";

			//카테고리정보
			if (!String.IsNullOrEmpty(category))
			{
				CategoryInfoT categoryInfo = CategoryBiz.GetCategoryInfo(category);
				if (categoryInfo != null)
				{
					switch (categoryInfo.Level)
					{
						case CategoryLevel.LargeCategory:
							gdlc = category;
							break;
						case CategoryLevel.MediumCategory:
							gdmc = category;
							break;
						case CategoryLevel.SmallCategory:
							gdsc = category;
							break;
						default:
							gdlc = category;
							break;
					}
				}
			}

			return new SearchBiz().GetSearchResult(keyword, sellerCustNo, gdlc, gdmc, gdsc, pageNo, pageSize, sort);
		}

		public SearchResultT GetSearchResult(string sellerCustNo, string keyword, string gdlc, string gdmc, string gdsc, int pageNo, int pageSize, DisplayOrder sort)
		{
			if (IsValidParameters(sellerCustNo, pageNo, pageSize) == false)
			{
				SearchResultT result = new SearchResultT();
				result.Items = new SearchItemT[0];
				result.CategoryInfo = new List<CategoryInfoT>();
				result.TotalCount = 0;
			}

			return new SearchBiz().GetSearchResult(keyword, sellerCustNo, gdlc, gdmc, gdsc, pageNo, pageSize, sort);
		}
		#endregion

		#region 판매자의 카테고리 정보 검색
		public List<CategoryInfoT> GetAllCategoriesFromCache(string sellerCustNo)
		{
			List<CategoryInfoT> categoryList = null;

			// 캐쉬에서 먼저 조회
			try
			{
				categoryList = (List<CategoryInfoT>)CacheHelper.GetCacheItem("GetAllCategoriesFromCache", sellerCustNo);
			}
			catch { }

			if (categoryList != null && categoryList.Count > 0)
			{
				return categoryList;
			}

			ItemFilter filter = new ItemFilter();
			filter.SellCustNo = sellerCustNo;

			categoryList = GetCategories(filter, GeneralCategoryDisplayType.LargeMediumSmallCategory);

			try
			{
				CacheHelper.SetCacheItem("GetAllCategoriesFromCache", sellerCustNo, categoryList, 60 * 5); //5분
			}
			catch {	}

			return categoryList;
		}

		public List<CategoryInfoT> GetCategories(string sellerCustNo, GeneralCategoryDisplayType displayType)
		{
			ItemFilter filter = new ItemFilter();
			filter.SellCustNo = sellerCustNo;

			return GetCategories(filter, displayType);
		}

		public List<CategoryInfoT> GetCategories(string sellerCustNo, string category, GeneralCategoryDisplayType displayType)
		{
			ItemFilter filter = new ItemFilter();
			filter.SellCustNo = sellerCustNo;
			filter.Category = category;

			//카테고리정보
			if (!String.IsNullOrEmpty(category))
			{
				CategoryInfoT cateinfo = CategoryBiz.GetCategoryInfo(category);
				switch (cateinfo.Level)
				{
					case CategoryLevel.GroupCategory:
						displayType = GeneralCategoryDisplayType.LargeCategoryOnly;
						break;
					case CategoryLevel.LargeCategory:
						displayType = GeneralCategoryDisplayType.MediumCategoryOnly;
						break;
					case CategoryLevel.MediumCategory:
						displayType = GeneralCategoryDisplayType.SmallCategoryOnly;
						break;
					case CategoryLevel.SmallCategory:
						displayType = GeneralCategoryDisplayType.SmallCategoryOnly;
						break;
					default:
						displayType = GeneralCategoryDisplayType.None;
						break;
				}
			}

			return GetCategories(filter, displayType);
		}

		private List<CategoryInfoT> GetCategories(ItemFilter itemFilter, GeneralCategoryDisplayType? displayType)
		{
			#region :::: Set Groupby
			HistogramQueryCollection hqc = new HistogramQueryCollection();
			string CategoryGroupField1 = Tables.ItemTable.LCode;
			string CategoryGroupField2 = String.Empty;
			string CategoryGroupField3 = String.Empty;

			switch (displayType)
			{
				case GeneralCategoryDisplayType.LargeCategoryOnly:
					CategoryGroupField1 = Tables.ItemTable.LCode;
					break;
				case GeneralCategoryDisplayType.MediumCategoryOnly:
					CategoryGroupField1 = Tables.ItemTable.MCode;
					break;
				case GeneralCategoryDisplayType.SmallCategoryOnly:
					CategoryGroupField1 = Tables.ItemTable.SCode;
					break;
				case GeneralCategoryDisplayType.LargeMediumCategory:
					CategoryGroupField1 = Tables.ItemTable.LCode;
					CategoryGroupField2 = Tables.ItemTable.MCode;
					break;
				case GeneralCategoryDisplayType.MediumSmallCategory:
					CategoryGroupField1 = Tables.ItemTable.MCode;
					CategoryGroupField2 = Tables.ItemTable.SCode;
					break;
				case GeneralCategoryDisplayType.LargeMediumSmallCategory:
					CategoryGroupField1 = Tables.ItemTable.LCode;
					CategoryGroupField2 = Tables.ItemTable.MCode;
					CategoryGroupField3 = Tables.ItemTable.SCode;
					break;
				default:
					CategoryGroupField1 = Tables.ItemTable.LCode;
					break;
			}

			hqc.Add(new HistogramQuery(CategoryGroupField1, HistogramSortOrder.Decrc));
			if (!String.IsNullOrEmpty(CategoryGroupField2))
			{
				hqc.Add(new HistogramQuery(CategoryGroupField2, HistogramSortOrder.Decrc));
			}
			if (!String.IsNullOrEmpty(CategoryGroupField3))
			{
				hqc.Add(new HistogramQuery(CategoryGroupField3, HistogramSortOrder.Decrc));
			}
			#endregion

			#region :::: Get Result
			QueryResult result = new SearchItemBiz().GetQueryResult(itemFilter, 0, 1, hqc);
			Histogram hs1 = (Histogram)result.Histograms[CategoryGroupField1];
			Histogram hs2 = null;
			Histogram hs3 = null;
			if (!String.IsNullOrEmpty(CategoryGroupField2))
			{
				hs2 = (Histogram)result.Histograms[CategoryGroupField2];
			}
			if (!String.IsNullOrEmpty(CategoryGroupField3))
			{
				hs3 = (Histogram)result.Histograms[CategoryGroupField3];
			}
			#endregion

			#region :::: 카테고리 그룹핑 결과 반환

			List<CategoryInfoT> categoryList = new List<CategoryInfoT>();
			CategoryInfoT category;
			if (hs1 != null)
			{
				foreach (HistogramEntry entry in hs1)
				{
					category = CategoryBiz.GetCategoryInfo(entry.Value.ToString());

					if (category.Level != CategoryLevel.GroupCategory)
					{
						category.ItemCount = entry.Count;
						categoryList.Add(category);
					}
				}
			}
			if (hs2 != null)
			{
				foreach (HistogramEntry entry in hs2)
				{
					category = CategoryBiz.GetCategoryInfo(entry.Value.ToString());

					if (category.Level != CategoryLevel.GroupCategory)
					{
						category.ItemCount = entry.Count;
						categoryList.Add(category);
					}
				}
			}
			if (hs3 != null)
			{
				foreach (HistogramEntry entry in hs3)
				{
					category = CategoryBiz.GetCategoryInfo(entry.Value.ToString());

					if (category.Level != CategoryLevel.GroupCategory)
					{
						category.ItemCount = entry.Count;
						categoryList.Add(category);
					}
				}
			}
			#endregion

			return categoryList;
		}

		public List<CategoryInfoT> GetCategories(string sellerCustNo, CategoryLevel? level)
		{
			ItemFilter filter = new ItemFilter();
			filter.SellCustNo = sellerCustNo;

			return GetCategories(filter, level);
		}

		private List<CategoryInfoT> GetCategories(ItemFilter itemFilter, CategoryLevel? level)
		{
			
			#region :::: Set Groupby
			HistogramQueryCollection hqc = new HistogramQueryCollection();
			string CategoryGroupField = Tables.ItemTable.LCode;

			switch (level)
			{
				case CategoryLevel.LargeCategory:
					CategoryGroupField = Tables.ItemTable.LCode;
					break;
				case CategoryLevel.MediumCategory:
					CategoryGroupField = Tables.ItemTable.MCode;
					break;
				case CategoryLevel.SmallCategory:
					CategoryGroupField = Tables.ItemTable.SCode;
					break;
				default:
					CategoryGroupField = Tables.ItemTable.LCode;
					break;
			}

			hqc.Add(new HistogramQuery(CategoryGroupField, HistogramSortOrder.Decrc));
			#endregion

			#region :::: Get Result
			QueryResult result = new SearchItemBiz().GetQueryResult(itemFilter, 0, 1, hqc);
			Histogram hs = (Histogram)result.Histograms[CategoryGroupField];
			#endregion

			#region :::: 카테고리 그룹핑 결과 반환

			List<CategoryInfoT> categoryList = new List<CategoryInfoT>();
			CategoryInfoT category;
			if (hs != null)
			{
				foreach (HistogramEntry entry in hs)
				{
					category = CategoryBiz.GetCategoryInfo(entry.Value.ToString());

					if (category.Level != CategoryLevel.GroupCategory)
					{
						category.ItemCount = entry.Count;
						categoryList.Add(category);
					}
				}
			}
			#endregion

			return categoryList;
		}
		#endregion

		#region 판매자의 샵 전용 카테고리 정보
		public List<CategoryInfoT> GetAllShopCategoriesFromCache(string sellerCustNo)
		{
			List<CategoryInfoT> categoryList = new List<CategoryInfoT>();

			if (String.IsNullOrEmpty(sellerCustNo))
			{
				return categoryList;
			}

			// 캐쉬에서 먼저 조회
			try
			{
				categoryList = (List<CategoryInfoT>)CacheHelper.GetCacheItem("GetAllShopCategoriesFromCache", sellerCustNo);
			}
			catch { }

			if (categoryList != null && categoryList.Count > 0)
			{
				return categoryList;
			}

			ItemFilter filter = new ItemFilter();
			filter.SellCustNo = sellerCustNo;

			categoryList = GetCategories(filter, ShopCategoryDisplayType.LargeMediumCategory);

			try
			{
				CacheHelper.SetCacheItem("GetAllShopCategoriesFromCache", sellerCustNo, categoryList, 60 * 5); //5분
			}
			catch { }

			return categoryList;
		}

		public List<CategoryInfoT> GetCategories(string sellerCustNo, ShopCategoryDisplayType displayType)
		{
			ItemFilter filter = new ItemFilter();
			filter.SellCustNo = sellerCustNo;

			return GetCategories(filter, displayType);
		}

		public List<CategoryInfoT> GetCategories(string sellerCustNo, string shopcategory, ShopCategoryDisplayType displayType)
		{
			ItemFilter filter = new ItemFilter();
			filter.SellCustNo = sellerCustNo;
			filter.MiniShopCategory = shopcategory;

			return GetCategories(filter, displayType);
		}
		
		private List<CategoryInfoT> GetCategories(ItemFilter itemFilter, ShopCategoryDisplayType? displayType)
		{
			IEnumerable<ShopCategoryT> shopCategories = new ShopCategoryDac().SelectShopCategories(itemFilter.SellCustNo);

			#region :::: Set Groupby
			HistogramQueryCollection hqc = new HistogramQueryCollection();
			string CategoryGroupField1 = Tables.ItemTable.MiniShopCategoryLCode;
			string CategoryGroupField2 = String.Empty;

			switch (displayType)
			{
				case ShopCategoryDisplayType.LargeCategoryOnly:
					CategoryGroupField1 = Tables.ItemTable.MiniShopCategoryLCode;
					break;
				case ShopCategoryDisplayType.MediumCategoryOnly:
					CategoryGroupField1 = Tables.ItemTable.MiniShopCategoryMCode;
					break;
				case ShopCategoryDisplayType.LargeMediumCategory:
					CategoryGroupField1 = Tables.ItemTable.MiniShopCategoryLCode;
					CategoryGroupField2 = Tables.ItemTable.MiniShopCategoryMCode;
					break;
				default:
					CategoryGroupField1 = Tables.ItemTable.MiniShopCategoryLCode;
					break;
			}

			hqc.Add(new HistogramQuery(CategoryGroupField1, HistogramSortOrder.Decrc));
			if (!String.IsNullOrEmpty(CategoryGroupField2))
			{
				hqc.Add(new HistogramQuery(CategoryGroupField2, HistogramSortOrder.Decrc));
			}
			#endregion

			#region :::: Get Result
			QueryResult result = new SearchItemBiz().GetQueryResult(itemFilter, 0, 1, hqc);
			List<CategoryInfoT> categoryList = new List<CategoryInfoT>();

			if (result == null)
			{
				return categoryList;
			}

			Histogram hs1 = (Histogram)result.Histograms[CategoryGroupField1];
			Histogram hs2 = null;
			if (!String.IsNullOrEmpty(CategoryGroupField2))
			{
				hs2 = (Histogram)result.Histograms[CategoryGroupField2];
			}
			#endregion

			#region :::: 카테고리 그룹핑 결과 반환
			// TODO 샵 카테고리 정보 가져오는거 필요.
			if (hs1 != null)
			{
				foreach (HistogramEntry entry in hs1)
				{
					if (entry != null)
					{
						ShopCategoryT shopCategory = shopCategories.Where<ShopCategoryT>(c => c.CategoryCode == entry.Value.ToString("00000000")).FirstOrDefault<ShopCategoryT>();

						if (shopCategory != null)
						{
							CategoryInfoT category = new CategoryInfoT();
							category.Id = shopCategory.CategoryCode;
							category.Name = shopCategory.Name;
							category.Level = shopCategory.Level;
							category.ItemCount = entry.Count;
							categoryList.Add(category);
						}
					}
				}
			}
			if (hs2 != null)
			{
				foreach (HistogramEntry entry in hs2)
				{
					if (entry != null)
					{
						ShopCategoryT shopCategory = shopCategories.Where<ShopCategoryT>(c => c.CategoryCode == entry.Value.ToString("00000000")).FirstOrDefault<ShopCategoryT>();

						if (shopCategory != null)
						{
							CategoryInfoT category = new CategoryInfoT();
							category.Id = shopCategory.CategoryCode;
							category.Name = shopCategory.Name;
							category.Level = shopCategory.Level;
							category.ItemCount = entry.Count;
							categoryList.Add(category);
						}
					}
				}
			}
			#endregion

			return categoryList;
		}
		#endregion

		private bool IsValidParameters(string sellerCustNo, int pageNo, int pageSize)
		{
			if (String.IsNullOrEmpty(sellerCustNo) || sellerCustNo.Length != 9 || pageNo < 1 || pageSize < 1)
				return false;

			return true;
		}
	}
}

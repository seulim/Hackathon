using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Biz
{
	public class BizOnApiBiz
	{
		#region 비즈온 홈메인
		public BizOnHome GetHome()
		{
			BizOnHome result = new BizOnHome();

			ApiResponse<BizOnHome> response = new BizOnApiDac().GetHome();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new BizOnHome();

			return result;
		}
		#endregion
		
		#region 비즈온 Best100
		public List<BizOnItem> GetBizOnBest()
		{
			List<BizOnItem> result = new List<BizOnItem>();

			ApiResponse<List<BizOnItem>> response = new BizOnApiDac().GetBizOnBest(1, 100);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<BizOnItem>();

			return result;
		}
		#endregion

		#region 비즈온 Category
		public BizOnLPModel GetBizOnCategoryModel(BizOnCategoryT category)
		{
			BizOnLPModel result = new BizOnLPModel();
			string mCategoryCode = String.Empty;

			if (category == null)
				return result;

			BizOnCategoryT lCateogy = new BizOnApiBiz().GetBizOnLCategoryInfo(category.GdlcCd);
			result.LCategoryFirst = lCateogy;


			List<BizOnCategoryT> mCategoryList = new BizOnApiBiz().GetBizOnMCategoryList(category.GdlcCd);
			if (mCategoryList != null && mCategoryList.Count > 0)
			{
				List<BizOnCategoryT> sCategoryList = new List<BizOnCategoryT>();

				if (!String.IsNullOrEmpty(category.GdmcCd))
				{
					result.MCategoryFirst = mCategoryList.Where(mc => mc.CategoryCd == category.GdmcCd).First();
					mCategoryCode = category.GdmcCd;
				}
				else
				{
					result.MCategoryFirst = mCategoryList[0];
					mCategoryCode = mCategoryList[0].GdmcCd;
				}

				result.MCategoryList = mCategoryList;

				sCategoryList = new BizOnApiBiz_Cache().GetBizOnSCategoryList(mCategoryCode);
				if (sCategoryList == null || sCategoryList.Count == 0)
				{
					sCategoryList = new BizOnApiBiz().GetBizOnSCategoryList(mCategoryCode);
				}
				if (sCategoryList != null && sCategoryList.Count > 0)
				{
					if (!String.IsNullOrEmpty(category.GdscCd))
					{
						result.SCategoryFirst = sCategoryList.Where(sc => sc.CategoryCd == category.CategoryCd).First();
					}
					else
					{
						result.SCategoryFirst = sCategoryList[0];
					}

					result.SCategoryList = sCategoryList;
				}
			}

			return result;
		}

		public BizOnLPCategoryInfo GetBizOnLPCategoryInfo(string bizOnLCategoryCode)
		{
			BizOnLPCategoryInfo result = new BizOnLPCategoryInfo();

			BizOnCategoryT lCateogy = new BizOnApiBiz().GetBizOnLCategoryInfo(bizOnLCategoryCode);
			result.LCategory = lCateogy;

			List<BizOnCategoryT> mCategoryList = new BizOnApiBiz().GetBizOnMCategoryList(bizOnLCategoryCode);
			if (mCategoryList != null && mCategoryList.Count > 0)
			{
				result.MCategoryList = mCategoryList;

				List<BizOnCategoryT> sCategoryList = new BizOnApiBiz_Cache().GetBizOnSCategoryList(mCategoryList.First().GdmcCd);
				if (sCategoryList == null || sCategoryList.Count == 0)
				{
					sCategoryList = new BizOnApiBiz().GetBizOnSCategoryList(mCategoryList.First().GdmcCd);
				}

				if (sCategoryList != null && sCategoryList.Count > 0)
				{
					result.SCategoryList = sCategoryList;
				}
			}

			return result;
		}

		public BizOnCategoryT GetBizOnCategoryInfo(string bizOnCategoryCode)
		{
			BizOnCategoryT result = new BizOnCategoryT();
			BizOnCategoryT defaultCategoryInfo = new BizOnCategoryT();

			string categoryCode = "400000090";

			List<BizOnCategoryT> lCategoryList = new BizOnApiBiz_Cache().GetBizOnCategoryAll(BizOnCategoryType.Large);
			if (lCategoryList == null || lCategoryList.Count == 0)
			{
				lCategoryList = new BizOnApiBiz().GetBizOnCategoryAll(BizOnCategoryType.Large);
			}

			if (lCategoryList != null && lCategoryList.Count > 0)
			{
				defaultCategoryInfo = lCategoryList.Where(lc => lc.CategoryCd == categoryCode).FirstOrDefault();
			}

			if (!String.IsNullOrEmpty(bizOnCategoryCode) || bizOnCategoryCode.Length == 9)
			{
				categoryCode = bizOnCategoryCode;
			}

			if (categoryCode.StartsWith("4"))
			{
				if (lCategoryList != null && lCategoryList.Count > 0)
				{
					BizOnCategoryT lCategory = lCategoryList.Where(lc => lc.CategoryCd == categoryCode).FirstOrDefault();
					if (lCategory != null && !String.IsNullOrEmpty(lCategory.GdlcCd))
					{
						result = lCategory;
					}
				}
			}
			else if (categoryCode.StartsWith("5"))
			{
				List<BizOnCategoryT> mCategoryList = new BizOnApiBiz_Cache().GetBizOnCategoryAll(BizOnCategoryType.Middle);
				if (mCategoryList == null || mCategoryList.Count == 0)
				{
					mCategoryList = new BizOnApiBiz().GetBizOnCategoryAll(BizOnCategoryType.Middle);
				}

				if (mCategoryList != null && mCategoryList.Count > 0)
				{
					BizOnCategoryT mCategory = mCategoryList.Where(mc => mc.CategoryCd == categoryCode).FirstOrDefault();
					if (mCategory != null && !String.IsNullOrEmpty(mCategory.GdlcCd))
					{
						result = mCategory;
					}
				}

			}
			else if (categoryCode.StartsWith("6"))
			{
				List<BizOnCategoryT> sCategoryList = new BizOnApiBiz_Cache().GetBizOnCategoryAll(BizOnCategoryType.Small);
				if (sCategoryList == null || sCategoryList.Count == 0)
				{
					sCategoryList = new BizOnApiBiz().GetBizOnCategoryAll(BizOnCategoryType.Small);
				}

				if (sCategoryList != null && sCategoryList.Count > 0)
				{
					BizOnCategoryT sCategory = sCategoryList.Where(sc => sc.CategoryCd == categoryCode).FirstOrDefault();
					if (sCategory != null && !String.IsNullOrEmpty(sCategory.GdlcCd))
					{
						result = sCategory;
					}
				}
			}

			if (result == null)
			{
				result = defaultCategoryInfo;
			}

			return result;
		}

		public BizOnCategoryT GetBizOnLCategoryInfo(string bizOnLCategoryCode)
		{
			BizOnCategoryT result = new BizOnCategoryT();

			if (String.IsNullOrEmpty(bizOnLCategoryCode))
			{
				return result;
			}

			List<BizOnCategoryT> lCategoryList = new BizOnApiBiz_Cache().GetBizOnCategoryAll(BizOnCategoryType.Large);
			if (lCategoryList == null)
			{
				lCategoryList = new BizOnApiBiz().GetBizOnCategoryAll(BizOnCategoryType.Large);
			}

			if (lCategoryList != null && lCategoryList.Count > 0)
			{
				result = lCategoryList.Where(c => c.CategoryCd == bizOnLCategoryCode).FirstOrDefault();
			}

			return result;
		}

		public List<BizOnCategoryT> GetBizOnMCategoryList(string bizOnLCategoryCode)
		{
			List<BizOnCategoryT> result = new List<BizOnCategoryT>();

			if (String.IsNullOrEmpty(bizOnLCategoryCode))
			{
				return result;
			}

			List<BizOnCategoryT> mCategoryList = new BizOnApiBiz_Cache().GetBizOnCategoryAll(BizOnCategoryType.Middle);
			if (mCategoryList == null)
			{
				mCategoryList = new BizOnApiBiz().GetBizOnCategoryAll(BizOnCategoryType.Middle);
			}

			if (mCategoryList != null && mCategoryList.Count > 0)
			{
				result = mCategoryList.Where(c => c.GdlcCd == bizOnLCategoryCode).ToList();
			}

			return result;
		}

		public List<BizOnCategoryT> GetBizOnSCategoryList(string bizOnMCategoryCode)
		{
			List<BizOnCategoryT> result = new List<BizOnCategoryT>();

			if (String.IsNullOrEmpty(bizOnMCategoryCode))
			{
				return result;
			}

			List<BizOnCategoryT> sCategoryList = new BizOnApiBiz().GetBizOnCategory(BizOnCategoryType.Small, bizOnMCategoryCode);
			result = sCategoryList;

			if (result == null)
			{
				result = new List<BizOnCategoryT>();				
			}

			return result;
		}
		
		public List<BizOnCategoryT> GetBizOnCategoryAll(BizOnCategoryType type)
		{
			List<BizOnCategoryT> result = new List<BizOnCategoryT>();

			ApiResponse<List<BizOnCategoryT>> response = new BizOnApiDac().GetBizOnCategory(type);
			if (response != null)
			{
				result = response.Data;
			}

			return result;
		}

		public List<BizOnCategoryT> GetBizOnCategory(BizOnCategoryType categoryType, string parentCode)
		{
			List<BizOnCategoryT> result = new List<BizOnCategoryT>();

			ApiResponse<List<BizOnCategoryT>> response = new BizOnApiDac().GetBizOnCategory(categoryType, parentCode);
			if (response != null)
			{
				result = response.Data;
			}

			return result;
		}
		#endregion

		#region 비즈온 LP Search
		public List<CPPLPSRPItemModel> GetBizOnMileageItems(string shopCategory, string keyword, int pageNo, int pageSize)
		{
			List<CPPLPSRPItemModel> result = new List<CPPLPSRPItemModel>();

			ApiResponse<List<CPPLPSRPItemModel>> response;
			if (String.IsNullOrEmpty(keyword))
			{
				response  = new BizOnApiDac().GetBizOnMileageItems(shopCategory, pageNo, pageSize);
			}
			else
			{
				response = new BizOnApiDac().GetBizOnMileageItems(shopCategory, keyword, pageNo, pageSize);
			}

			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<CPPLPSRPItemModel>();

			if (result.Count > pageSize)
			{
				result = result.GetRange(0, pageSize);
			}

			return result;
		}
		#endregion
	}
}

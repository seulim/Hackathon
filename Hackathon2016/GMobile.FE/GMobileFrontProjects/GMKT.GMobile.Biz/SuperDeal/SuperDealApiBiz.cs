using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;


namespace GMKT.GMobile.Biz
{
	public class SuperDealApiBiz
	{
		#region 슈퍼딜 카테고리 정보
		public List<SuperDealCategoryV2> GetSuperDealCategory()
		{
			List<SuperDealCategoryV2> result = new List<SuperDealCategoryV2>();

			ApiResponse<List<SuperDealCategoryV2>> response = new SuperDealApiDac().GetSuperDealCategoryV2();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<SuperDealCategoryV2>();

			return result;
		}
		#endregion

		/// <summary>
		/// 슈퍼딜 테마 카테고리 
		/// </summary>
		/// <returns></returns>
		public List<SuperDealCategory> GetSuperDealThemeCategory()
		{
			List<SuperDealCategory> result = new List<SuperDealCategory>();

			ApiResponse<List<SuperDealCategory>> response = new SuperDealApiDac().GetSuperDealThemeCategory();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<SuperDealCategory>();

			return result;
		}

		/// <summary>
		/// 슈퍼딜 상품 가져오기(테마)
		/// </summary>
		/// <param name="displayType"></param>
		/// <param name="gdlcCd"></param>
		/// <param name="gdmcCd"></param>
		/// <returns></returns>
		public List<HomeMainItem> GetSuperDealThemeItem(string displayType, string gdlcCd, string gdmcCd)
		{
			List<HomeMainItem> result = new List<HomeMainItem>();

			ApiResponse<List<HomeMainItem>> response = new SuperDealApiDac().GetSuperDealThemeItem(displayType, gdlcCd, gdmcCd);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<HomeMainItem>();

			return result;
		}

		/// <summary>
		/// 슈퍼딜 메인아이템
		/// </summary>
		/// <returns></returns>
        public List<HomeMainItem> GetSuperDealThemeMainItem(string userInfo="", string code="")
		{
			List<HomeMainItem> result = new List<HomeMainItem>();

			ApiResponse<List<HomeMainItem>> response = new SuperDealApiDac().GetSuperDealThemeMainItem( userInfo,  code);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<HomeMainItem>();

			return result;
		}

		#region 슈퍼딜 상품 가져오기
		public List<SuperDealItem> GetSuperDealItems(string code, string fromWhere)
		{
			List<SuperDealItem> result = new List<SuperDealItem>();

			ApiResponse<List<SuperDealItem>> response = new SuperDealApiDac().GetSuperDealItemsV2(code);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<SuperDealItem>();

			return result;
		}

		#region [Pilot - gsohn] Pilot Test 코드 (테스트 완료후 삭제)
		public List<SuperDealItem> GetSuperDealItemsPilot(string userInfo, string code, string fromWhere)
		{
			List<SuperDealItem> result = new List<SuperDealItem>();

			ApiResponse<List<SuperDealItem>> response = new SuperDealApiDac().GetSuperDealItemsV2Pilot(userInfo, code);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<SuperDealItem>();

			return result;
		}
		#endregion
		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using GMKT.Framework.EnterpriseServices;
using GMKT.Framework.Data;
using GMKT.Framework.Cache;

namespace GMKT.GMobile.Data
{
	public class ECouponDac : MicroDacBase
	{
		#region 모바일 e쿠폰 배너
		/// <summary>
		/// 배너 조회
		/// </summary>
		/// <param name="max_count"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "SelectMobileECouponEvent", 1)]
		public List<ECouponEvent> SelectMobileECouponEventCache(int max_count)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ECouponEvent>(
				"item_read"
				, "dbo.up_gmkt_mobile_select_mobileecoupon_event"
				, MicroDacHelper.CreateParameter("@max_count", max_count, SqlDbType.Int)
			);
		}
		#endregion

		#region 모바일 e쿠폰 카테고리
		/// <summary>
		/// 카테고리 조회
		/// </summary>
		/// <param name="max_count"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "SelectMobileECouponCategory", 1)]
		public List<ECouponCategory> SelectMobileECouponCategoryCache(int max_count)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ECouponCategory>(
				"item_read"
				, "dbo.up_gmkt_mobile_select_mobileecoupon_category"
				, MicroDacHelper.CreateParameter("@max_count", max_count, SqlDbType.Int)
			);
		}
		#endregion

		#region 모바일 e쿠폰 브랜드
		/// <summary>
		/// 카테고리별 브랜드
		/// </summary>
		/// <param name="category_cd"></param>
		/// <param name="max_count"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "SelectMobileECouponBrandByCategory", 1)]
		public List<ECouponBrand> SelectMobileECouponBrandByCategoryCache(string category_cd, int max_count)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ECouponBrand>(
				"item_read"
				, "dbo.up_gmkt_mobile_select_mobileecoupon_brand_by_category"
				, MicroDacHelper.CreateParameter("@category_cd", category_cd, SqlDbType.VarChar, 10)
				, MicroDacHelper.CreateParameter("@max_count", max_count, SqlDbType.Int)
			);
		}
		#endregion

		#region 모바일 e쿠폰 메뉴
		/// <summary>
		/// 브랜드별 메뉴 조회
		/// </summary>
		/// <param name="brand_cd"></param>
		/// <param name="max_count"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "SelectMobileECouponMenuByBrand", 1)]
		public List<ECouponBrandMenu> SelectMobileECouponMenuByBrandCache(int brand_cd, int max_count)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ECouponBrandMenu>(
				"item_read"
				, "dbo.up_gmkt_mobile_select_mobileecoupon_brandmenu_by_brand"
				, MicroDacHelper.CreateParameter("@brand_cd", brand_cd, SqlDbType.Int)
				, MicroDacHelper.CreateParameter("@max_count", max_count, SqlDbType.Int)
			);
		}

		[GMKTCache(GMKTCacheType.LocalCache, "SelectMobileECouponMenuByBrandPaging", 1)]
		public List<ECouponBrandMenu> SelectMobileECouponMenuByBrandPagingCache(int brand_cd, int page_index, int page_size)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ECouponBrandMenu>(
				"item_read"
				, "dbo.up_gmkt_mobile_select_mobileecoupon_brandmenu_by_brand_paging"
				, MicroDacHelper.CreateParameter("@brand_cd", brand_cd, SqlDbType.Int)
				, MicroDacHelper.CreateParameter("@page_index", page_index, SqlDbType.Int)
				, MicroDacHelper.CreateParameter("@page_size", page_size, SqlDbType.Int)
			);
		}
		#endregion

		#region 모바일 e쿠폰 상품
		[GMKTCache(GMKTCacheType.LocalCache, "SelectMobileECouponItemByMenu", 1)]
		public List<ECouponItemByMenu> SelectMobileECouponItemByMenuCache(long menu_seq, int max_count)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ECouponItemByMenu>(
				"item_read"
				, "dbo.up_gmkt_mobile_select_mobileecoupon_item_by_menu"
				, MicroDacHelper.CreateParameter("@menu_seq", menu_seq, SqlDbType.BigInt)
				, MicroDacHelper.CreateParameter("@max_count", max_count, SqlDbType.Int)
			);
		}

		
		#endregion
	}
}

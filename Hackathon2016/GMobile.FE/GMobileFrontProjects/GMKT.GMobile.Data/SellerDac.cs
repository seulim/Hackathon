using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using GMKT.Framework.EnterpriseServices;
using GMKT.Framework.Cache;

namespace GMKT.GMobile.Data
{
	public class SellerDac : MicroDacBase
	{
		[GMKTCache(GMKTCacheType.LocalCache, "SellerInfoCache", 1)]
		public List<SellerT> SelectSeller(string sellerId)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<SellerT>(
				"tiger_read",
				"dbo.UP_GMKTNet_Seller_SelectSeller",
				MicroDacHelper.CreateParameter("@seller_cust_no", sellerId, SqlDbType.VarChar, 10));
		}

		/// <summary>
		/// 판매자 등급 포인트 조회
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "SellerFeedbackCache", 1)]
		public List<SellerT> SelectFeedbackRate(string sellerId)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<SellerT>(
				"webzine_read",
				"dbo.up_gmkt_front_get_seller_recommend_point",
				MicroDacHelper.CreateParameter("@seller_cust_no", sellerId, SqlDbType.VarChar, 10));
		}

		/// <summary>
		/// 판매자 마일리지 조회
		/// </summary>
		/// <param name="sellerId"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "SellerMileageCache", 1)]
		public List<SellerT> SelectMileageToSave(string sellerId)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<SellerT>(
				"tiger_read",
				"dbo.UP_GMKTNet_ShopBase_SelectMileageToSave",
				MicroDacHelper.CreateParameter("@cust_no", sellerId, SqlDbType.VarChar, 10)
			);
		}
	}
}

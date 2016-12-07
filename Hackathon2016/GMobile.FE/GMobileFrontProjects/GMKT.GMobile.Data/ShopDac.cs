using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using GMKT.Framework.EnterpriseServices;
using GMKT.Framework.Cache;

namespace GMKT.GMobile.Data
{
	public class ShopDac : MicroDacBase
	{
		/// <summary>
		/// 미니샵 판매자 정보 조회 (item > dealershopsettletlb)
		/// </summary>
		/// <param name="basicDomain"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "ShopBasicCache", 1)]
		public List<ShopT> SelectShopBasicCache(string alias)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ShopT>(
				"item_read",
				"dbo.UP_GMKTNet_Shop_SelectShopBasic",
				MicroDacHelper.CreateParameter("@alias", alias, SqlDbType.VarChar, 20)
			);
		}

		public ShopT SelectShopDomain(string sellerCustNo)
		{
			return MicroDacHelper.SelectSingleEntity<ShopT>(
				"item_read",
				"dbo.up_gmkt_front_minishop_get_seller_domain",
				MicroDacHelper.CreateParameter("@sell_cust_no", sellerCustNo, SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// Shop info 조회 (minishopdb > dealer_minishop_info)
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "ShopInfo", 1)]
		public List<ShopT> SelectShopCache(string sellerCustNo)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ShopT>(
				"minishop_read",
				"dbo.UP_GMKTNet_Shop_SelectShop",
				MicroDacHelper.CreateParameter("@cust_no", sellerCustNo, SqlDbType.VarChar, 10)
			);
		}

		[GMKTCache(GMKTCacheType.LocalCache, "ShopTitleCache", 1)]
		public List<ShopT> SelectShopTitleCache(string sellerCustNo)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ShopT>(
				"tiger_read",
				"dbo.UP_GMKTNet_Shop_SelectShopTitle",
				MicroDacHelper.CreateParameter("@cust_no", sellerCustNo, SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// 샵 상세소개 조회 (minishopdb > shop_detail_info)
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public ShopDetailT SelectShopIntroductionDetail(string sellerCustNo)
		{
			return MicroDacHelper.SelectScalar<ShopDetailT>(
				"minishop_read",
				"dbo.UP_GMKTNet_Shop_SelectShopIntroductionDetail",
				MicroDacHelper.CreateParameter("@cust_no", sellerCustNo, SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// 샵 모바일 기본 정보 조회
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <param name="shopLevel"></param>
		/// <returns></returns>
		public MobileBasicInfoT SelectMobileMiniShopInfo(string sellerCustNo, string shopLevel)
		{
			return MicroDacHelper.SelectSingleEntity<MobileBasicInfoT>(
				"minishop_read",
				"dbo.UP_GMKT_Mobile_SelectMiniShopBasicInfo",
				MicroDacHelper.CreateParameter("@cust_no", sellerCustNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@shop_level", shopLevel, SqlDbType.Char, 1)
			);
		}

		/// <summary>
		/// 샵 모바일 전시 정보 조회
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <param name="shopLevel"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "MobileDisplayInfo", 1)]
		public List<MobileDisplayInfoT> SelectMobileDisplayInfoCache(string sellerCustNo, string shopLevel, string use_yn)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<MobileDisplayInfoT>(
				"minishop_read",
				"dbo.UP_GMKT_Mobile_SelectMiniShopDisplayInfo",
				MicroDacHelper.CreateParameter("@cust_no", sellerCustNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@shop_level", shopLevel, SqlDbType.Char, 1),
				MicroDacHelper.CreateParameter("@use_yn", use_yn, SqlDbType.Char, 1)
			);
		}

		/// <summary>
		/// 샵 모바일 직접 선택하기
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <param name="shopLevel"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "MobileSellerOrder", 1)]
		public List<string> SelectMobileSellerOrderCache(long areaNo)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<string>(
				"minishop_read",
				"dbo.UP_GMKT_Mobile_SelectMiniShopSellerOrder",
				MicroDacHelper.CreateParameter("@area_no", areaNo, SqlDbType.BigInt)
			);
		}

		/// <summary>
		/// 샵 모바일 메인 이미지 조회
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <param name="shopLevel"></param>
		/// <returns></returns>
		[GMKTCache(GMKTCacheType.LocalCache, "MobileImageInfo", 1)]
		public List<MobileImageT> SelectMobileMiniShopImageCache(string sellerCustNo, string shopLevel, string status)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<MobileImageT>(
				"minishop_read",
				"dbo.UP_GMKT_Mobile_SelectMiniShopImage",
				MicroDacHelper.CreateParameter("@cust_no", sellerCustNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@shop_level", shopLevel, SqlDbType.Char, 1),
				MicroDacHelper.CreateParameter("@status", status, SqlDbType.Char, 1)
			);
		}

		/// <summary>
		/// 관심매장 확인
		/// </summary>
		/// <returns></returns>
		public FavoriteShopT SelectFavoriteShopInfo(string custNo, string sellerCustNo)
		{
			return MicroDacHelper.SelectSingleEntity<FavoriteShopT>(
				"tiger_read",
				"dbo.UP_GMKTNet_Shop_SelectFavoriteShopInfo",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@seller_cust_no", sellerCustNo, SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// 관심매장 리스트 조회
		/// </summary>
		/// <returns></returns>
		public List<FavoriteShopT> SelectFavoriteShopList(string custNo, int pageNo, int pageSize, byte order)
		{
			return MicroDacHelper.SelectMultipleEntities<FavoriteShopT>(
				"tiger_read",
				"dbo.UP_GMKTNet_FavoriteShop_SelectFavoriteShopListAll",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@page_no", pageNo, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@page_size", pageSize, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@order", order, SqlDbType.TinyInt)
			);
		}

		/// <summary>
		/// 관심매장 등록
		/// </summary>
		/// <returns></returns>
		public string InsertMyFavorites(string custNo, int groupNo, string sellerCustNo)
		{
			return MicroDacHelper.SelectScalar<string>(
				"tiger_write",
				//"dbo.up_GMKTNet_Shop_InserMyFavoriteShopInMain",
				"goodsdaq.up_my_favorite_shops_ins",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@grp_no", groupNo, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@seller_cust_no", sellerCustNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@minishop_cust_no", "", SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// 관심매장 삭제
		/// </summary>
		/// <returns>int</returns>
		public int DelFavoriteShop(string custNo, int groupNo, string sellerCustNo)
		{
			return MicroDacHelper.SelectScalar<int>(
				"tiger_write",
				"dbo.UP_GMKTNet_FavoriteShop_DelFavoriteShop",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@grp_no", groupNo, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@seller_cust_no", sellerCustNo, SqlDbType.VarChar, 10)
			);

		}

		/// <summary>
		/// 관심상품 등록조회
		/// </summary>
		/// <returns>int</returns>
		public int SelectFavoriteItems(string custNo, int groupNo, string gdNo)
		{
			return MicroDacHelper.SelectScalar<int>(
				"tiger_write",
				"dbo.UP_GMKT_Mobile_SelectFavoriteItem",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@grp_no", groupNo, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@gd_no", gdNo, SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// 관심상품상품등록
		/// </summary>
		/// <returns>int</returns>
		public int InsertFavoriteItems(string custNo, int groupNo, string gdNo)
		{
			return MicroDacHelper.SelectScalar<int>(
				"tiger_write",
				"dbo.UP_GMKTNet_FavoriteItems_InterestItemsAdd",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@grp_no", groupNo, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@gd_no", gdNo, SqlDbType.VarChar, 10)
			);
		}
	}

	public class DangolPolicyDac : MicroDacBase
	{
		/// <summary>
		/// 단골 할인 정보 
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		//[GMKTCache(cacheType: GMKTCacheType.LocalCache, cacheName: "DangolPolicyInfo", expireMinute: 60)]
		[GMKTCache(GMKTCacheType.LocalCache, "DangolPolicyInfo", 1)]
		public DangolPolicyT SelectDangolPolicyCache(string custNo, string sellerCustNo)
		{
			List<DangolPolicyT> results = MicroDacHelper.SelectMultipleEntitiesFromCache<DangolPolicyT>(
				"stardb_read",
				"dbo.UP_GMKTNet_OrderContract_SelectSellerDangolOrderPolicy",
				MicroDacHelper.CreateParameter("@buy_cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@seller_cust_no", sellerCustNo, SqlDbType.VarChar, 10)
			);

			return results != null && results.Count > 0 ? results[0] : null;
		}

		/// <summary>
		/// 단골매장 여부 확인
		/// </summary>
		/// <param name="custNo"></param>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public string SelectDangolSeller(string custNo, string sellerCustNo)
		{
			return MicroDacHelper.SelectScalar<string>(
				"tiger_read",
				"dbo.up_gmkt_front_get_dangol_seller_check",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@seller_cust_no", sellerCustNo, SqlDbType.VarChar, 10)
				);
		}

		/// <summary>
		/// 구매매장 정보 확인
		/// </summary>
		/// <param name="custNo"></param>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public string SelectPurchaseSeller(string custNo, string sellerCustNo)
		{
			return MicroDacHelper.SelectScalar<string>(
				"stardb_read",
				"dbo.up_gmkt_front_get_purchase_seller_check",
				MicroDacHelper.CreateParameter("@cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@seller_cust_no", sellerCustNo, SqlDbType.VarChar, 10)
				);
		}

		/// <summary>
		/// 미니샵 쇼핑이력 조회
		/// </summary>
		/// <param name="custNo"></param>
		/// <param name="sellerCustNo"></param>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public OrderSumT SelectShopOrderSum(string custNo, string sellerCustNo, DateTime startDate, DateTime endDate)
		{
			return MicroDacHelper.SelectSingleEntity<OrderSumT>(
                "neoevent_read",
				"nrdata_event.dbo.UP_GMKTNet_Shop_SelectShopContractTotalSum",
				MicroDacHelper.CreateParameter("@buy_cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@sell_cust_no", sellerCustNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@start_dt", startDate, SqlDbType.DateTime),
				MicroDacHelper.CreateParameter("@end_dt", endDate, SqlDbType.DateTime)
			);
		}

		/// <summary>
		/// 미니샵 장바구니상품수 조회
		/// </summary>
		/// <param name="custNo"></param>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public int SelectCartItemsCount(string custNo, string sellerCustNo)
		{
			return MicroDacHelper.SelectScalar<int>(
				"tiger_read",
				"dbo.UP_GMKTNet_Shop_SelectCartItemsCount",
				MicroDacHelper.CreateParameter("@buy_cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@sell_cust_no", sellerCustNo, SqlDbType.VarChar, 10)
			);
		}

		/// <summary>
		/// 미니샵 관심상품수 조회
		/// </summary>
		/// <param name="custNo"></param>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public int CountInterestItems(string custNo, string sellerCustNo)
		{
			return MicroDacHelper.SelectScalar<int>(
				"tiger_read",
				"dbo.UP_GMKTNet_Shop_SelectInterestItemsCount",
				MicroDacHelper.CreateParameter("@buy_cust_no", custNo, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@sell_cust_no", sellerCustNo, SqlDbType.VarChar, 10)
			);
		}
	}
}

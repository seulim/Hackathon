using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;

using System.Web;
using System.Web.Caching;

namespace GMKT.GMobile.Biz
{
	public class ShopBiz : BizBase
	{
		/// <summary>
		/// 미니샵 정보
		/// </summary>
		/// <param name="alias"></param>
		/// <returns></returns>
		public ShopT GetShop(string alias)
		{
			//todo:bc6877 캐시 적용 필요

			//string key = typeof(ShopT).Name + alias;
			//if (HttpContext.Current.Cache[key] != null)
			//    return (ShopT)HttpContext.Current.Cache[key];

			ShopT shop = null;
			if (!String.IsNullOrEmpty(alias))
			{
				List<ShopT> shopBasic = new ShopDac().SelectShopBasicCache(alias);

				if (shopBasic != null && shopBasic.Count > 0 )
				{
					List<ShopT> shopSub = new ShopDac().SelectShopTitleCache(shopBasic[0].SellerId);
					List<ShopT> shops = new ShopDac().SelectShopCache(shopBasic[0].SellerId);

					if (shops != null && shops.Count > 0)
					{
						shop = shops[0];
						shop.Alias = alias;
						shop.SellerId = shopBasic[0].SellerId;
						if (shopSub != null && shopSub.Count > 0)
						{
							shop.Title = shopSub[0].Title;
							shop.HelpdeskTelNo = shopSub[0].HelpdeskTelNo;
							shop.ShopType = shopSub[0].ShopType;
						}

					}
				}

				// Cache Insert
				//if (shop != null)
				//    HttpContext.Current.Cache.Insert(key, shop, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
			}
			return shop;
		}

		/// <summary>
		/// 샵 상세 소개 정보
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public ShopDetailT GetShopIntroductionDetail(string sellerCustNo)
		{
			return new ShopDac().SelectShopIntroductionDetail(sellerCustNo);
		}

		public string GetShopDomain(string sellerCustNo)
		{
			ShopT shopDomain = new ShopDac().SelectShopDomain(sellerCustNo);

			if (shopDomain != null)
				return shopDomain.Alias;
			else
				return "";
		}

		/// <summary>
		/// 샵 모바일 정보
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public MobileShopInfoT GetMobileShopInfo(string sellerCustNo, string shopLevel)
		{
			MobileShopInfoT mobileShop = new MobileShopInfoT();
			mobileShop.ShopInfo = new MobileBasicInfoT();
			mobileShop.DisplayInfo = new List<MobileDisplayInfoT>();
			mobileShop.SellerOrder = new Dictionary<int, List<string>>();
			mobileShop.MainImages = new List<MobileImageT>();

			mobileShop.ShopInfo = new ShopDac().SelectMobileMiniShopInfo(sellerCustNo, shopLevel);
			mobileShop.DisplayInfo = new ShopDac().SelectMobileDisplayInfoCache(sellerCustNo, shopLevel, "Y");
			for (int i = 0; i < mobileShop.DisplayInfo.Count; i++)
			{
				if (mobileShop.DisplayInfo[i].SortType == 5)
				{
					List<string> selleritems = new ShopDac().SelectMobileSellerOrderCache(mobileShop.DisplayInfo[i].AreaNo);
					mobileShop.SellerOrder.Add(i, selleritems);
				}
			}
			mobileShop.MainImages = new ShopDac().SelectMobileMiniShopImageCache(sellerCustNo, shopLevel, "1");

			return mobileShop;
		}

		/// <summary>
		/// 구매매장 확인
		/// </summary>
		/// <param name="custNo"></param>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public FavoriteShopT GetFavoriteShopInfo(string custNo, string sellerCustNo)
		{
			return new ShopDac().SelectFavoriteShopInfo(custNo, sellerCustNo);
		}

		public List<FavoriteShopT> GetFavoriteShopList(string custNo)
		{
			return new ShopDac().SelectFavoriteShopList(custNo, 1, 10, 0);
		}

		/// <summary>
		/// 관심매장 등록
		/// </summary>
		/// <returns></returns>
		public string AddMyFavorites(string custNo, int groupNo, string sellerCustNo)
		{
			return new ShopDac().InsertMyFavorites(custNo, groupNo, sellerCustNo);
		}

		/// <summary>
		/// 관심매장 해제
		/// </summary>
		/// <returns></returns>
		public int DeleteMyFavorites(string custNo, int groupNo, string sellerCustNo)
		{
			return new ShopDac().DelFavoriteShop(custNo, groupNo, sellerCustNo);
		}

		/// <summary>
		/// 관심상품 조회
		/// </summary>
		/// <returns></returns>
		public int SelectFavoriteItems(string custNo, int groupNo, string itemId)
		{
			return new ShopDac().SelectFavoriteItems(custNo, groupNo, itemId);
		}

		public int AddFavoriteItems(string custNo, int groupNo, string itemId)
		{
			return new ShopDac().InsertFavoriteItems(custNo, groupNo, itemId);
		}

        public static int ConvertToSortOrder(short esmSort)
        {
            int sort = 0;
            switch (esmSort)
            {
                case 1: sort = (int)DisplayOrder.SellPointDesc; break;
                case 2: sort = (int)DisplayOrder.RankPointDesc; break;
                case 3: sort = (int)DisplayOrder.ConPointDesc; break;
                case 4: sort = (int)DisplayOrder.DiscountPriceDesc; break;
                default: sort = (int)DisplayOrder.SellPointDesc; break;
            }

            return sort;
        }
	}

	//todo:bc6877
	public class DangolPolicyBiz : BizBase
	{
		public static DangolPolicyT GetInstance(string custNo, string sellerCustNo)
		{
		    DangolPolicyBiz biz = new DangolPolicyBiz();
		    DangolPolicyT dangol = biz.GetDangolPolicy(custNo, sellerCustNo);
		    if (dangol != null)
		    {
		        dangol.IsDangolShopValue = biz.GetDangolSeller(custNo, sellerCustNo);
		        dangol.IsPurchaseSellerValue = biz.GetPurchaseSeller(custNo, sellerCustNo);
		    }
		    else
		    {
		        dangol = new DangolPolicyT();
		        dangol.IsDangolShopValue = "N";
		        dangol.IsPurchaseSellerValue = "N";
		    }
		    return dangol;
		}

		/// <summary>
		/// 단골 할인 정보 조회
		/// </summary>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public DangolPolicyT GetDangolPolicy(string custNo, string sellerCustNo)
		{
			return new DangolPolicyDac().SelectDangolPolicyCache(custNo, sellerCustNo);
		}

		/// <summary>
		/// 관심매장 확인
		/// </summary>
		/// <param name="custNo"></param>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public string GetDangolSeller(string custNo, string sellerCustNo)
		{
			return new DangolPolicyDac().SelectDangolSeller(custNo, sellerCustNo);
		}

		/// <summary>
		/// 구매매장 확인
		/// </summary>
		/// <param name="custNo"></param>
		/// <param name="sellerCustNo"></param>
		/// <returns></returns>
		public string GetPurchaseSeller(string custNo, string sellerCustNo)
		{
			return new DangolPolicyDac().SelectPurchaseSeller(custNo, sellerCustNo);
		}

		public OrderSumT GetShopOrderSum(string custNo, string sellerCustNo)
		{
			DateTime endDate = DateTime.Now;
			DateTime startDate = endDate.AddMonths(-6);
			return new DangolPolicyDac().SelectShopOrderSum(custNo, sellerCustNo, startDate, endDate);
		}

		public int GetCartItemsCount(string custNo, string sellerCustNo)
		{
			return new DangolPolicyDac().SelectCartItemsCount(custNo, sellerCustNo);
		}

		public int GetInterestItemsCount(string custNo, string sellerCustNo)
		{
			return new DangolPolicyDac().CountInterestItems(custNo, sellerCustNo);
		}

		public OrderHistoryT GetShopOrderHistory(string custNo, string sellerCustNo)
		{
			string key = "Order" + custNo + sellerCustNo;
			if (HttpContext.Current.Cache[key] != null)
				return (OrderHistoryT)HttpContext.Current.Cache[key];

			DangolPolicyBiz biz = new DangolPolicyBiz();
			OrderHistoryT shopOrder = new OrderHistoryT();

			shopOrder.OrderSum = biz.GetShopOrderSum(custNo, sellerCustNo);
			shopOrder.CartItemsCount = biz.GetCartItemsCount(custNo, sellerCustNo);
			shopOrder.InterestCount = biz.GetInterestItemsCount(custNo, sellerCustNo);

			if (shopOrder != null)
				HttpContext.Current.Cache.Insert(key, shopOrder, null, Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));

			return shopOrder;
		}

	}
}

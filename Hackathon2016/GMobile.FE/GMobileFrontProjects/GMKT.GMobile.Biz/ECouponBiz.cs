using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;
using GMobile.Data.Voyager;
using GMKT.GMobile.Util;
using ArcheFx.EnterpriseServices;

namespace GMKT.GMobile.Biz
{
	[Transaction(TransactionOption.NotSupported)]
	public class ECouponBiz : BizBase
	{
		const string CACHE_KEY_ITEMTOP1 = "biz.GetMobileECouponItemTopOne";

		#region 모바일 e쿠폰 배너
		public List<ECouponEvent> GetMobileECouponEvent(int max_count)
		{
			return new ECouponDac().SelectMobileECouponEventCache(max_count);
		}
		#endregion

		#region 모바일 e쿠폰 카테고리
		public List<ECouponCategory> GetMobileECouponCategory(int max_count)
		{
			return new ECouponDac().SelectMobileECouponCategoryCache(max_count);
		}
		#endregion

		#region 모바일 e쿠폰 브랜드
		public List<ECouponBrand> GetMobileECouponBrandByCategory(string category_cd, int max_count)
		{
			return new ECouponDac().SelectMobileECouponBrandByCategoryCache(category_cd, max_count);
		}
		#endregion

		#region 모바일 e쿠폰 메뉴
		public List<ECouponBrandMenu> GetMobileECouponBrandMenu(int brand_cd, int max_count)
		{
			return new ECouponDac().SelectMobileECouponMenuByBrandCache(brand_cd, max_count);
		}

		public List<ECouponBrandMenu> GetMobileECouponBrandMenuPaging(int brand_cd, int page_index, int page_size)
		{
			return new ECouponDac().SelectMobileECouponMenuByBrandPagingCache(brand_cd, page_index, page_size);
		}

		public List<ECouponMenuItem> GetMobileECouponBrandMenuWithItem(int brand_cd, int max_count)
		{
			List<ECouponMenuItem> list = new List<ECouponMenuItem>();
			
			List<ECouponBrandMenu> menulist = new ECouponDac().SelectMobileECouponMenuByBrandCache(brand_cd, max_count);
			foreach(ECouponBrandMenu menu in menulist)
			{
				ECouponMenuItem menuitem = new ECouponMenuItem();

				menuitem.BrandCode = menu.BrandCode;
				menuitem.BrandName = menu.BrandName;
				menuitem.BrandImageUrl = menu.LogoImageUrl;
				menuitem.MenuSeq = menu.MenuSeq;
				menuitem.MenuName = menu.MenuName;
				menuitem.MenuImageUrl = menu.ImageUrl;
				menuitem.MenuLoadingImageUrl = "http://image.gmarket.co.kr/Gmarket_Mobile_v2/loading_200x200.gif";
				menuitem.MenuPriority = menu.Priority;
				menuitem.TotalCount = menu.TotalCount;
				menuitem.PageIndex = menu.PageIndex;
				menuitem.PageSize = menu.PageSize;

				ECouponItemTop1 itemTop1 = new ECouponBiz().GetMobileECouponItemTopOne(menu.MenuSeq, 100);
				if (itemTop1 != null)
				{
					menuitem.ItemNo = itemTop1.ItemNo;
					menuitem.DiscountPrice = itemTop1.DiscountPrice;
					menuitem.DealPrice = itemTop1.DealPrice;
					menuitem.SellPrice = itemTop1.SellPrice;
					menuitem.HasOption = itemTop1.HasOption;
					menuitem.VipUrl = itemTop1.VipUrl;
					menuitem.BasketUrl = itemTop1.BasketUrl;
					menuitem.OrderUrl = itemTop1.OrderUrl;
				}
				
				list.Add(menuitem);
			}

			return list;
		}

		public List<ECouponMenuItem> GetMobileECouponBrandMenuWithItemPaging(int brand_cd, int page_index, int page_size)
		{
			List<ECouponMenuItem> list = new List<ECouponMenuItem>();
			
			List<ECouponBrandMenu> menulist = new ECouponDac().SelectMobileECouponMenuByBrandPagingCache(brand_cd, page_index, page_size);
			foreach(ECouponBrandMenu menu in menulist)
			{
				ECouponMenuItem menuitem = new ECouponMenuItem();

				menuitem.BrandCode = menu.BrandCode;
				menuitem.BrandName = menu.BrandName;
				menuitem.BrandImageUrl = menu.LogoImageUrl;
				menuitem.MenuSeq = menu.MenuSeq;
				menuitem.MenuName = menu.MenuName;
				menuitem.MenuImageUrl = menu.ImageUrl;
				menuitem.MenuLoadingImageUrl = "http://image.gmarket.co.kr/Gmarket_Mobile_v2/loading_200x200.gif";
				menuitem.MenuPriority = menu.Priority;
				menuitem.TotalCount = menu.TotalCount;
				menuitem.PageIndex = menu.PageIndex;
				menuitem.PageSize = menu.PageSize;

				ECouponItemTop1 itemTop1 = new ECouponBiz().GetMobileECouponItemTopOne(menu.MenuSeq, 100);
				if (itemTop1 != null)
				{
					menuitem.ItemNo = itemTop1.ItemNo;
					menuitem.DiscountPrice = itemTop1.DiscountPrice;
					menuitem.DealPrice = itemTop1.DealPrice;
					menuitem.SellPrice = itemTop1.SellPrice;
					menuitem.HasOption = itemTop1.HasOption;
					menuitem.VipUrl = itemTop1.VipUrl;
					menuitem.BasketUrl = itemTop1.BasketUrl;
					menuitem.OrderUrl = itemTop1.OrderUrl;
				}
				
				list.Add(menuitem);
			}

			return list;
		}
		#endregion

		#region 모바일 e쿠폰 상품
		public List<ECouponItemByMenu> GetMobileECouponItem(long menu_seq, int max_count)
		{
			return new ECouponDac().SelectMobileECouponItemByMenuCache(menu_seq, max_count);
		}

		public ECouponItemTop1 GetMobileECouponItemTopOne(long menu_seq, int max_count)
		{
			//캐시 확인
			var itemCache = CacheHelper.GetCacheItem(CACHE_KEY_ITEMTOP1, menu_seq.ToString());
			if (itemCache != null)
			{
				return (ECouponItemTop1) itemCache;
			}

			//캐시 없을 경우
			ECouponItemTop1 itemTop1 = new ECouponItemTop1();
			List<string> targetItems = new List<string>();
			Dictionary<string, ECouponItemByMenu> dicItemList = new Dictionary<string,ECouponItemByMenu>();
			SearchItemT[] searchitems;

			List<ECouponItemByMenu> itemlist = new ECouponBiz().GetMobileECouponItem(menu_seq, 1000);

			if (itemlist == null || itemlist.Count <= 0)
				return itemTop1;

			//1. 우선노출
			var exposeItem = (from data in itemlist
							where data.AheadExposeYn == "Y"
							select data).ToList();
			if (exposeItem != null && exposeItem.Count > 0)
			{
				targetItems.Add(exposeItem[0].ItemNo);
				searchitems = new SearchBiz().GetItems(targetItems, DisplayOrder.SellPriceAsc);

				if (searchitems == null || searchitems.Length <=0 ) searchitems = new SearchItemT[1];

				itemTop1 = ConvertToECouponIemModel(exposeItem[0], searchitems[0]);

				return SetCacheItemTopOne(menu_seq.ToString(), itemTop1);
			}
			else 
			{
				//검색할 아이템 리스트 생성
				foreach(ECouponItemByMenu ecitem in itemlist)
				{
					targetItems.Add(ecitem.ItemNo);
					dicItemList.Add(ecitem.ItemNo, ecitem);
				}

				//2. 최저가 상품
				searchitems = new SearchBiz().GetItems(targetItems, DisplayOrder.SellPriceAsc);

				if (searchitems != null && searchitems.Length > 0)
				{
					decimal lowestPrice2 = searchitems[0].Discount != null ? (searchitems[0].SellPrice - searchitems[0].Discount.DiscountPrice) : searchitems[0].SellPrice;
					SearchItemT[] items2 = (from itemR in searchitems 
										 where (itemR.Discount != null ? (itemR.SellPrice - itemR.Discount.DiscountPrice) : itemR.SellPrice)  == lowestPrice2
										 select itemR).ToArray();

					if (items2 != null && items2.Length == 1)
					{
						ECouponItemByMenu ecoupon2 = dicItemList[items2[0].GdNo.ToString()];
						itemTop1 = ConvertToECouponIemModel(ecoupon2, items2[0]);
						return SetCacheItemTopOne(menu_seq.ToString(), itemTop1);
					}
				
					//3. 랭크 포인트순
					//   (최저가 동일할 경우)
					List<string> targetitems3 = (from item3 in items2
												 select item3.GdNo.ToString()).ToList();
					searchitems = new SearchBiz().GetItems(targetitems3, DisplayOrder.RankPointDesc);

					if (searchitems != null && searchitems.Length > 0)
					{
						int lowestRankPoint = searchitems[0].RankPoint2;
						SearchItemT[] items3 = (from itemR in searchitems 
											 where itemR.RankPoint2  == lowestRankPoint
											 select itemR).ToArray();

						if (items3 != null && items3.Length == 1)
						{
							ECouponItemByMenu ecoupon3 = dicItemList[items3[0].GdNo.ToString()];
							itemTop1 = ConvertToECouponIemModel(ecoupon3, items3[0]);
							return SetCacheItemTopOne(menu_seq.ToString(), itemTop1);
						}

						//4. 신상품
						//   (랭크포인트가 동일할 경우)
						List<string> targetitems4 = (from item4 in items3
												 select item4.GdNo.ToString()).ToList();
						searchitems = new SearchBiz().GetItems(targetitems4, DisplayOrder.New);

						if (searchitems != null && searchitems.Length > 0)
						{
							ECouponItemByMenu ecoupon4 = dicItemList[searchitems[0].GdNo.ToString()];
							itemTop1 = ConvertToECouponIemModel(ecoupon4, searchitems[0]);
							return SetCacheItemTopOne(menu_seq.ToString(), itemTop1);
						}
					}
				}
			}

			//default
			ECouponItemByMenu defaultEcoupon = itemlist[0];
			if (searchitems == null || searchitems.Length <=0 ) searchitems = new SearchItemT[1];
			itemTop1 = ConvertToECouponIemModel(defaultEcoupon, searchitems[0]);

			return SetCacheItemTopOne(menu_seq.ToString(), itemTop1);
		}


		protected ECouponItemTop1 SetCacheItemTopOne(string cache_key, ECouponItemTop1 itemTop1)
		{
			//todo:캐시확인
			itemTop1.CacheTime = DateTime.Now.ToString("hh:mm:ss");
			CacheHelper.SetCacheItem(CACHE_KEY_ITEMTOP1, cache_key, itemTop1, 30);
			return itemTop1;
		}
		#endregion

	#region 모바일 e쿠폰 홈
	public ApiResponse<ECouponHome> GetECouponHome()
	{
		return new ECouponApiDac().GetECouponHome();
	}
	#endregion

        #region 모바일 e쿠폰 브랜드lp
        /// <summary>
        /// 브랜드LP
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        public ApiResponse<ECouponBrandLpInfo> GetECouponBrandLp(string categoryCode)
        {
            return new ECouponApiDac().GetECouponBrandLp(categoryCode);
        }

        /// <summary>
        /// 브랜드LP 더보기
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ApiResponse<ECouponBrandLpMore> GetECouponBrandLpMore(string categoryCode, int pageIndex, int pageSize)
        {
            return new ECouponApiDac().GetECouponBrandLpMore(categoryCode, pageIndex, pageSize);
        }
        #endregion

        #region 모바일 e쿠폰 브랜드홈
        /// <summary>
        /// 브랜드홈
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <param name="brandCode"></param>
        /// <returns></returns>
        public ApiResponse<ECouponBrandHomeInfo> GetECouponBrandHomeInfo(string categoryCode, int brandCode)
        {
            return new ECouponApiDac().GetECouponBrandHomeInfo(categoryCode, brandCode);
        }

        /// <summary>
        /// 브랜드홈 더보기
        /// </summary>
        /// <param name="categoryCode"></param>
        /// <param name="brandCode"></param>
        /// <returns></returns>
        public ApiResponse<ECouponBrandMenuMore> GetECouponBrandHomeMenuMore(int brandCode, int pageIndex, int pageSize)
        {
            return new ECouponApiDac().GetECouponBrandHomeMenuMore(brandCode, pageIndex, pageSize);
        }
        #endregion

        public ECouponItemTop1 ConvertToECouponIemModel(ECouponItemByMenu ecoupon, SearchItemT search)
		{
			ECouponItemTop1 item = new ECouponItemTop1();

			item.ItemNo = ecoupon.ItemNo;
			item.CategoryCode = ecoupon.CategoryCode;
			item.BrandCode = ecoupon.BrandCode;
			item.MenuSeq =  ecoupon.MenuSeq;

			if (search != null)
			{
				item.VipUrl = Urls.MItemUrl + "?goodscode=" + item.ItemNo;
				item.BasketUrl = Urls.MItemWebURL + "/Basket/ToBasket?GoodsCode="+item.ItemNo+"&OrderType=Basket";
				item.OrderUrl = Urls.MItemWebURL + "/Basket/ToBasket?GoodsCode="+item.ItemNo+"&OrderType=Order";
				item.DealPrice =  search.DealPrice;
				if (search.Discount != null)
				{
					item.DiscountPrice = search.Discount.DiscountPrice;
					item.SellPrice = (search.SellPrice - search.Discount.DiscountPrice);
					if (search.SellPrice > 1 && search.Discount.DiscountPrice > 0)
					{
						item.DiscountRate =  Convert.ToInt32(Math.Floor(search.Discount.DiscountPrice / search.SellPrice * 100));
					}
				}
				else
				{
					item.DiscountPrice = 0;
					item.SellPrice = search.SellPrice;
					item.DiscountRate = 0;
				}
			}
			else
			{
				item.VipUrl = String.Empty;
				item.BasketUrl = String.Empty;
				item.OrderUrl = String.Empty;
				item.DealPrice = 0;
				item.DiscountPrice =  0;
				item.SellPrice = 0;
				item.DiscountRate = 0;
			}

			return item;
		}
	}
}

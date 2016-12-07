using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;
using GMKT.GMobile.Util;
using GMKT.Web.Context;

namespace GMKT.GMobile.Data.SellerShop
{
	public class SellerShopApiDac : ApiBase
	{
		public SellerShopApiDac() : base("GMApi") { }

		public ApiResponse<ShopData> GetShop(string alias)
		{
			ApiResponse<ShopData> result = ApiHelper.CallAPI<ApiResponse<ShopData>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetShop"),
				new
				{
					alias = alias					
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<SellerData> GetSeller(string sellerId)
		{
			ApiResponse<SellerData> result = ApiHelper.CallAPI<ApiResponse<SellerData>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetSeller"),
				new
				{
					sellerId = sellerId
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<FavoriteShopData> GetFavoriteShopInfo(string custNo, string sellerCustNo)
		{
			ApiResponse<FavoriteShopData> result = ApiHelper.CallAPI<ApiResponse<FavoriteShopData>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetFavoriteShopInfo"),
				new
				{
					buyCustNo = custNo,
					sellCustNo = sellerCustNo
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

        public ApiResponse<long> GetFavoriteShopCount(string sellerCustNo)
        {
            ApiResponse<long> result = ApiHelper.CallAPI<ApiResponse<long>>(
                "GET",
                ApiHelper.MakeUrl("api/SellerShop/GetFavoriteShopCount"),
                new
                {
                    sellCustNo = sellerCustNo
                }
                , ConnApiUtil.GetEXIDCookieParameter()
            );
            return result;
        }

		public ApiResponse<SimpleModel> AddFavoriteShop(string custNo, string sellerCustNo)
		{
			ApiResponse<SimpleModel> result = ApiHelper.CallAPI<ApiResponse<SimpleModel>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetAddFavoriteShop"),
				new
				{
					buyCustNo = custNo,
					sellCustNo = sellerCustNo
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<SimpleModel> DelFavoriteShop(string custNo, string sellerCustNo)
		{
			ApiResponse<SimpleModel> result = ApiHelper.CallAPI<ApiResponse<SimpleModel>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetDelFavoriteShop"),
				new
				{
					buyCustNo = custNo,
					sellCustNo = sellerCustNo
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<SellerShopMain> GetMain(string sellCustNo, string goodsCode)
		{
			ApiResponse<SellerShopMain> result = ApiHelper.CallAPI<ApiResponse<SellerShopMain>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetMain"),
				new
				{
					sellCustNo = sellCustNo,
					goodsCode = goodsCode
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<List<MenuT>> GetMenu(string sellCustNo, long shopDispNo)
		{
			ApiResponse<List<MenuT>> result = ApiHelper.CallAPI<ApiResponse<List<MenuT>>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetMenu"),
				new
				{
					sellCustNo = sellCustNo,
					shopDispNo = shopDispNo
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<SellerShopMenuContent> GetMenuContent(string sellCustNo, long shopDispNo, int menuNo)
		{
			ApiResponse<SellerShopMenuContent> result = ApiHelper.CallAPI<ApiResponse<SellerShopMenuContent>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetMenuContent"),
				new
				{
					sellCustNo = sellCustNo,
					shopDispNo = shopDispNo,
					menuNo = menuNo
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}


		public ApiResponse<SellerShopNoticeList> GetMenuSellerNoticeList(string sellCustNo, int pageSize, int pageNo)
		{
			ApiResponse<SellerShopNoticeList> result = ApiHelper.CallAPI<ApiResponse<SellerShopNoticeList>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetMenuSellerNoticeList"),
				new
				{
					sellCustNo = sellCustNo,
					pageSize = pageSize,
					pageNo = pageNo
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<ShopNewsT> GetMenuSellerNoticeDetail(string sellCustNo, long noticeSeq)
		{
			ApiResponse<ShopNewsT> result = ApiHelper.CallAPI<ApiResponse<ShopNewsT>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetMenuSellerNoticeDetail"),
				new
				{
					sellCustNo = sellCustNo,
					noticeSeq = noticeSeq
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<ShopBasicData> GetShopBasicInfo(string sellerCustNo)
		{
			ApiResponse<ShopBasicData> result = ApiHelper.CallAPI<ApiResponse<ShopBasicData>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetShopBasicInfo"),
				new
				{
					sellerCustNo = sellerCustNo
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<List<SellerShopItemDisplay>> GetMainItemDisplayInfo(long shopDispNo, string shopLevel)
		{
			ApiResponse<List<SellerShopItemDisplay>> result = ApiHelper.CallAPI<ApiResponse<List<SellerShopItemDisplay>>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetMainItemDisplayInfo"),
				new
				{
					shopDispNo = shopDispNo,
					shopLevel = shopLevel
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

        public ApiResponse<ItemDisplayPage> GetItemDisplay(string sellerCustNo, int dispType, int sortType, long areaNo, int pageNo, long shopDispNo = 0)
		{
			ApiResponse<ItemDisplayPage> result = ApiHelper.CallAPI<ApiResponse<ItemDisplayPage>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetItemDisplay"),
				new
				{
					sellCustNo = sellerCustNo,
					dispType = dispType,
					sortType = sortType,
					areaNo = areaNo,
					pageNo = pageNo,
                    shopDispNo = shopDispNo
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<YoutubeInfo> GetYoutubeInfo(string vodKey)
		{
			ApiResponse<YoutubeInfo> result = ApiHelper.CallAPI<ApiResponse<YoutubeInfo>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetYoutubeInfo"),
				new
				{
					vodKey = vodKey
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<SellerShopNavigation> GetNavigation(SellerShopNavigationRequest request)
		{
			ApiResponse<SellerShopNavigation> result = ApiHelper.CallAPI<ApiResponse<SellerShopNavigation>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetNavigation"),
				request,
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<List<SellerShopCategory>> GetShopCategories(string sellerId)
		{
			ApiResponse<List<SellerShopCategory>> result = ApiHelper.CallAPI<ApiResponse<List<SellerShopCategory>>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetShopCategories"),
				new
				{
					sellerId = sellerId
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<SellerShopLPSRP> Search(SellerShopSearchRequest request)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("api/SellerShop/Search?pageNo=" + request.PageNo + "&pageSize=" + request.PageSize +
			"&minPrice=" + request.MinPrice + "&maxPrice=" + request.MaxPrice + "&sortType=" + request.SortType + "&shopCategoryLevel=" + request.Level);
			if (!string.IsNullOrEmpty(request.MenuName))
				sb.Append("&menuName=" + request.MenuName);
			if (!string.IsNullOrEmpty(request.Keyword))
				sb.Append("&keyword=" + request.Keyword);
			if (!string.IsNullOrEmpty(request.ScKeyword))
				sb.Append("&scKeyword=" + request.ScKeyword);
			if (!string.IsNullOrEmpty(request.ExKeyword))
				sb.Append("&exKeyword=" + request.ExKeyword);
			if (!string.IsNullOrEmpty(request.LcId))
				sb.Append("&lcId=" + request.LcId);
			if (!string.IsNullOrEmpty(request.McId))
				sb.Append("&mcId=" + request.McId);
			if (!string.IsNullOrEmpty(request.ScId))
				sb.Append("&scId=" + request.ScId);
			if (!string.IsNullOrEmpty(request.SellCustNo))
				sb.Append("&sellCustNo=" + request.SellCustNo);							
			if (!string.IsNullOrEmpty(request.IsFreeDelivery))
				sb.Append("&isFreeDelivery=" + request.IsFreeDelivery);
			if (!string.IsNullOrEmpty(request.IsMileage))
				sb.Append("&isMileage=" + request.IsMileage);
			if (!string.IsNullOrEmpty(request.IsDiscount))
				sb.Append("&isDiscount=" + request.IsDiscount);
			if (!string.IsNullOrEmpty(request.IsStamp))
				sb.Append("&isStamp=" + request.IsStamp);
			if (!string.IsNullOrEmpty(request.IsSmartDelivery))
				sb.Append("&isSmartDelivery=" + request.IsSmartDelivery);			
			if (!string.IsNullOrEmpty(request.ShopCategory))
				sb.Append("&shopCategory=" + request.ShopCategory);
			if (!string.IsNullOrEmpty(request.IsShopCategory))
				sb.Append("&isShopCategory=" + request.IsShopCategory);

			ApiResponse<SellerShopLPSRP> result = ApiHelper.CallAPI<ApiResponse<SellerShopLPSRP>>(
				"GET",
				ApiHelper.MakeUrl(sb.ToString()),
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

		public ApiResponse<SellerShopPromotion> GetPromotion(long id, string sellCustNo)
		{
			ApiResponse<SellerShopPromotion> result = ApiHelper.CallAPI<ApiResponse<SellerShopPromotion>>(
				"GET",
				ApiHelper.MakeUrl("api/SellerShop/GetPromotion"),
				new
				{
					id = id
					, sellCustNo = sellCustNo
				},
				ConnApiUtil.GetEXIDCookieParameter()
			);
			return result;
		}

        public ApiResponse<CategoryBannerInfo> GetCategoryBanner(CategoryBannerInfoRequest request)
        {
            ApiResponse<CategoryBannerInfo> result = ApiHelper.CallAPI<ApiResponse<CategoryBannerInfo>>(
                "GET",
                ApiHelper.MakeUrl("api/SellerShop/GetCategoryBanner"),
                request
                , ConnApiUtil.GetEXIDCookieParameter()
            );
            return result;
        }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.SellerShop;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz.SellerShop
{
	public class SellerShopApiBiz
	{
		//샵정보 조회
		public ShopData GetShop(string alias)
		{
			ShopData result = new ShopData();

			ApiResponse<ShopData> response = new SellerShopApiDac().GetShop(alias);
			if (response != null)
			{
				result = response.Data;
			}

			//if (result == null) result = new ShopData();

			return result;
		}

		//판매자정보 조회
		public SellerData GetSeller(string sellerId)
		{
			SellerData result = new SellerData();

			ApiResponse<SellerData> response = new SellerShopApiDac().GetSeller(sellerId);
			if (response != null)
			{
				result = response.Data;
			}

			//if (result == null) result = new SellerData();

			return result;
		}

		//관심매장 조회
		public FavoriteShopData GetFavoriteShopInfo(string custNo, string sellerCustNo)
		{
			FavoriteShopData result = new FavoriteShopData();

			ApiResponse<FavoriteShopData> response = new SellerShopApiDac().GetFavoriteShopInfo(custNo, sellerCustNo);
			if (response != null)
			{
				result = response.Data;
			}

			//if (result == null) result = new FavoriteShopData();

			return result;
		}

        public long GetFavoriteShopCount(string sellerCustNo)
        {
            long result = 0;

            ApiResponse<long> response = new SellerShopApiDac().GetFavoriteShopCount(sellerCustNo);
            if (response != null)
            {
                result = response.Data;
            }

            return result;
        }


		// 관심매장 등록
		public SimpleModel AddFavoriteShop(string custNo, string sellerCustNo)
		{
			SimpleModel result = new SimpleModel();
			ApiResponse<SimpleModel> response = new SellerShopApiDac().AddFavoriteShop(custNo, sellerCustNo);
			if (response != null)
			{
				result = response.Data;
			}

			return result;
		}

		// 관심매장 해제
		public SimpleModel DelFavoriteShop(string custNo, string sellerCustNo)
		{
			SimpleModel result = new SimpleModel();
			ApiResponse<SimpleModel> response = new SellerShopApiDac().DelFavoriteShop(custNo, sellerCustNo);
			if (response != null)
			{
				result = response.Data;
			}

			return result;
		}

		//샵 메인
		public SellerShopMain GetMain(string sellCustNo, string goodsCode)
		{
			SellerShopMain result = new SellerShopMain();
			ApiResponse<SellerShopMain> response = new SellerShopApiDac().GetMain(sellCustNo, goodsCode);
			if (response != null)
			{
				result = response.Data;
			}

			//if (result == null) result = new SellerShopMain();

			return result;
		}

		//메뉴페이지
		public List<MenuT> GetMenu(string sellCustNo, long shopDispNo)
		{
			List<MenuT> result = new List<MenuT>();
			ApiResponse<List<MenuT>> response = new SellerShopApiDac().GetMenu(sellCustNo, shopDispNo);
			if (response != null)
			{
				result = response.Data;
			}

			return result;
		}

		public SellerShopMenuContent GetMenuContent(string sellCustNo, long shopDispNo, int menuNo)
		{
			SellerShopMenuContent result = new SellerShopMenuContent();
			ApiResponse<SellerShopMenuContent> response = new SellerShopApiDac().GetMenuContent(sellCustNo, shopDispNo, menuNo);
			if (response != null)
			{
				result = response.Data;
			}
			return result;
		}

		public ShopBasicData GetShopBasicInfo(string sellerCustNo)
		{
			ShopBasicData result = new ShopBasicData();

			ApiResponse<ShopBasicData> response = new SellerShopApiDac().GetShopBasicInfo(sellerCustNo);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new ShopBasicData();

			return result;
		}

		public List<SellerShopItemDisplay> GetMainItemDisplayInfo(long shopDispNo, string shopLevel)
		{
			List<SellerShopItemDisplay> result = new List<SellerShopItemDisplay>();
			
			if(shopDispNo == 0) return result;

			ApiResponse<List<SellerShopItemDisplay>> response = new SellerShopApiDac().GetMainItemDisplayInfo(shopDispNo, shopLevel);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<SellerShopItemDisplay>();

			return result;
		}

		public ItemDisplayPage GetItemDisplay(string sellerCustNo, int dispType, int sortType, long areaNo, int pageNo, long shopDispNo)
		{
			ItemDisplayPage result = new ItemDisplayPage();

            ApiResponse<ItemDisplayPage> response = new SellerShopApiDac().GetItemDisplay(sellerCustNo, dispType, sortType, areaNo, pageNo, shopDispNo);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new ItemDisplayPage();

			return result;
		}

		public YoutubeInfo GetYoutubeInfo(string vodKey)
		{
			YoutubeInfo result = new YoutubeInfo();

			ApiResponse<YoutubeInfo> response = new SellerShopApiDac().GetYoutubeInfo(vodKey);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new YoutubeInfo();

			return result;
		}
		
		//공지사항 list
		public SellerShopNoticeList GetMenuSellerNoticeList(string sellCustNo, int pageSize, int pageNo)
		{
			SellerShopNoticeList result = new SellerShopNoticeList();
			ApiResponse<SellerShopNoticeList> response = new SellerShopApiDac().GetMenuSellerNoticeList(sellCustNo, pageSize, pageNo);
			if (response != null)
			{
				result = response.Data;
			}

			return result;
		}

		//공지사항 상세
		public ShopNewsT GetMenuSellerNoticeDetail(string sellCustNo, long noticeSeq)
		{
			ShopNewsT result = new ShopNewsT();
			ApiResponse<ShopNewsT> response = new SellerShopApiDac().GetMenuSellerNoticeDetail(sellCustNo, noticeSeq);
			if (response != null)
			{
				result = response.Data;
			}

			return result;
		}

		public SellerShopNavigation GetNavigation(SellerShopNavigationRequest request)
		{
			SellerShopNavigation result = new SellerShopNavigation();			

			ApiResponse<SellerShopNavigation> response = new SellerShopApiDac().GetNavigation(request);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new SellerShopNavigation();

			return result;
		}

		public List<SellerShopCategory> GetShopCategories(string sellerId)
		{
			List<SellerShopCategory> result = new List<SellerShopCategory>();

			ApiResponse<List<SellerShopCategory>> response = new SellerShopApiDac().GetShopCategories(sellerId);
			if(response != null)
			{
				result = response.Data;
			}

			if(result == null) result = new List<SellerShopCategory>();

			return result;
		}

		public SellerShopLPSRP Search(SellerShopSearchRequest request)
		{
			SellerShopLPSRP result = new SellerShopLPSRP();

			ApiResponse<SellerShopLPSRP> response = new SellerShopApiDac().Search(request);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new SellerShopLPSRP();

			return result;
		}

        public CategoryBannerInfo GetCategoryBanner(CategoryBannerInfoRequest request)
        {
            return new SellerShopApiDac().GetCategoryBanner(request).Data;
        }

		public SellerShopPromotion GetPromotion(long id, string sellCustNo)
		{
			return new SellerShopApiDac().GetPromotion(id, sellCustNo).Data;
		}

		public CategoryLevel GetRootCategoryLevel(ShopCategoryDisplayType displayType) 
		{
			switch(displayType)
			{
				case ShopCategoryDisplayType.LargeMediumSmallCategory:
				case ShopCategoryDisplayType.LargeMediumCategory:
				case ShopCategoryDisplayType.LargeCategoryOnly:
					return CategoryLevel.LargeCategory;
				case ShopCategoryDisplayType.MediumSmallCategory:
				case ShopCategoryDisplayType.MediumCategoryOnly:
					return CategoryLevel.MediumCategory;
				case ShopCategoryDisplayType.SmallCategoryOnly:
					return CategoryLevel.SmallCategory;
				default:
					return CategoryLevel.None;
			}
		}

		public CategoryLevel GetRootCategoryLevel(GeneralCategoryDisplayType displayType) 
		{
			switch(displayType)
			{
				case GeneralCategoryDisplayType.LargeMediumSmallCategory:
				case GeneralCategoryDisplayType.LargeMediumCategory:
				case GeneralCategoryDisplayType.LargeCategoryOnly:
					return CategoryLevel.LargeCategory;
				case GeneralCategoryDisplayType.MediumSmallCategory:
				case GeneralCategoryDisplayType.MediumCategoryOnly:
					return CategoryLevel.MediumCategory;
				case GeneralCategoryDisplayType.SmallCategoryOnly:
					return CategoryLevel.SmallCategory;
				default:
					return CategoryLevel.None;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz.Search
{
	public class SearchAPIBiz
	{
		private SearchApiDac apidac = null;

		public SearchApiDac APIDac
		{
			get
			{
				if(apidac == null)
					apidac = new SearchApiDac();

				return apidac;
			}
		}

		public SRPResultModel PostSearchItem(SearchRequest input, string userInfo)
		{
			SRPResultModel result = new SRPResultModel();

			ApiResponse<SRPResultModel> response = APIDac.PostSearchItem(input, userInfo);
			if(response != null && response.Data != null)
			{
				result = response.Data;
			}

			return result;
		}

		public List<CPPLPSRPItemModel> PostGetItemInfo(SearchItemRequest input)
		{
			List<CPPLPSRPItemModel> result = new List<CPPLPSRPItemModel>();

			ApiResponse<List<CPPLPSRPItemModel>> response = APIDac.PostGetItemInfo(input);
			if(response != null && response.Data != null)
			{
				result = response.Data;
			}

			return result;
		}

		public List<CPPLPSRPItemModel> GetSmartClickItems(string menuName, string keyword, string scKeyword, string userInfo,
			int pageNo, int pageSize, string lcId, string mcId, string scId, string sellCustNo, string brandList,
			int minPrice, int maxPrice, string sortType,
			string isFeeFree, string isMileage, string isDiscount, string isStamp, string isSmartDelivery, int startRank, int maxCount, long keywordSeqNo)
		{
			List<CPPLPSRPItemModel> result = new List<CPPLPSRPItemModel>();

			ApiResponse<List<CPPLPSRPItemModel>> response =
				APIDac.GetSmartClickItems(menuName, keyword, scKeyword, userInfo, pageNo, pageSize, lcId, mcId, scId,
				sellCustNo, brandList, minPrice, maxPrice, sortType,
				isFeeFree, isMileage, isDiscount, isStamp, isSmartDelivery, startRank, maxCount, keywordSeqNo);
			if (response != null && response.Data != null)
			{
				result = response.Data;
			}

			return result;
		}

		public LPSRPBlockAdModel GetPowerClickItems(string menuName, string keyword, string userInfo, string lcId, string mcId, string scId, int startRank, long keywordSeqNo, List<string> categories = null, List<string> brands = null,
			string isBrand = "N", string isDiscount = "N", string isShippingFree = "N", string isMileage = "N", string isSmartDelivery = "N", string sellCustNo = "N", int minPrice = 0, int maxPrice = 0)
		{
			LPSRPBlockAdModel result = new LPSRPBlockAdModel();

			ApiResponse<LPSRPBlockAdModel> response = null;
			if (categories == null && brands == null)
			{
				response = APIDac.GetPowerClickItems(menuName, keyword, userInfo, lcId, mcId, scId, startRank, keywordSeqNo, isBrand, isDiscount, isShippingFree, isMileage, isSmartDelivery, sellCustNo, minPrice, maxPrice);
			}
			else
			{
				response = APIDac.GetAdvPowerClickItems(menuName, keyword, userInfo, lcId, mcId, scId, startRank, keywordSeqNo, categories, brands, isBrand, isDiscount, isShippingFree, isMileage, isSmartDelivery, sellCustNo, minPrice, maxPrice);
			}

			if (response != null && response.Data != null)
			{
				result = response.Data;
			}

			return result;
		}

		public CPPCategoryBest100Model GetCPPCategory( string lcId )
		{
			ApiResponse<CPPCategoryBest100Model> response = APIDac.GetCPPCategory( lcId );
			CPPCategoryBest100Model model = response.Data;
			if ( model == null )
			{
				model = new CPPCategoryBest100Model();
				model.Best100Items = new List<SearchItemModel>();
				model.CategoryList = new List<SRPSearchCategory>();
				model.CPPTitle = "";
			}

			return model;
		}

		public List<CategoryGroupInfo> GetCategoryGroupList()
		{
			var response = new SearchApiDac().GetCategoryGroupList();
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return new List<CategoryGroupInfo>();
			}
		}

		public List<BestSellerGroupInfo> GetBestSellerGroupList()
		{
			var response = new SearchApiDac().GetBestSellerGroupList();
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return new List<BestSellerGroupInfo>();
			}
		}

		public SmartBoxModel GetSmartBox(SearchRequest input, string userInfo)
		{
			ApiResponse<SmartBoxModel> response = new SearchApiDac().PostGetSmartBox(input, userInfo);
			return GenericUtil.AtLeastReturnNewObjct(response.Data);
		}

		public List<SmartBoxTileEntity> GetSmartBoxDetail(SearchRequest input)
		{
			ApiResponse<List<SmartBoxTileEntity>> response = new SearchApiDac().PostGetSmartBoxDetail(input);
			return GenericUtil.AtLeastReturnEmptyList(response.Data);
		}

		public List<RecommendKeywordModel> GetRecommendKeyword(string primeKeyword, bool needDiver)
		{
			ApiResponse<List<RecommendKeywordModel>> response = new SearchApiDac().GetRecommedKeyword(primeKeyword, needDiver);
			return GenericUtil.AtLeastReturnNewObjct(response.Data);
		}

		public ApiResponse<GetAddCartResponseT> GetAddCartResult(string itemNo, short orderQty, bool isInstantOrder)
		{
			ApiResponse<GetAddCartResponseT> addCartResultI = new SearchApiDac().GetAddCartResult(itemNo, orderQty, isInstantOrder, "");
			return addCartResultI;
		}
	}
}

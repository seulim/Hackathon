using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using ConnApi.Client;
using GMKT.Web.Context;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Util;


namespace GMKT.GMobile.Data
{
	public class SearchApiDac : ApiBase
	{
		public SearchApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<SRPResultModel> PostSearchItem(SearchRequest input, string userInfo)
		{
			string userInfoKey = GMobileWebContext.EncodedCookieNameOfUserInfo;

			ApiResponse<SRPResultModel> result = ApiHelper.CallAPI<ApiResponse<SRPResultModel>>(				
				"POST",
				ApiHelper.MakeUrl("api/Search/PostSearchItem"),
				input,
				new CookieParameter(userInfoKey, userInfo),
				ConnApiUtil.GetEXIDCookieParameter(),
				new HeaderParameter("Client-Ip", GMKT.Component.Member.UserUtil.IPAddressBySecure(System.Web.HttpContext.Current))
			);
			return result;
		}

		public ApiResponse<List<CPPLPSRPItemModel>> PostGetItemInfo(SearchItemRequest input)
		{
			ApiResponse<List<CPPLPSRPItemModel>> result = ApiHelper.CallAPI<ApiResponse<List<CPPLPSRPItemModel>>>(
				"POST",
				ApiHelper.MakeUrl("api/Search/PostGetItemInfo"),
				input
			);
			return result;
		}
		

		public ApiResponse<List<RecommendKeywordModel>> GetRecommedKeyword(string primeKeyword, bool needDiver)
		{
			ApiResponse<List<RecommendKeywordModel>> result = ApiHelper.CallAPI<ApiResponse<List<RecommendKeywordModel>>>(
				"GET",
				ApiHelper.MakeUrl("api/Search/GetRecommendKeyword"),
				new { primeKeyword, needDiver }
			);
			return result;
		}

		public ApiResponse<List<CPPLPSRPItemModel>> GetSmartClickItems(string menuName, string keyword, string scKeyword, string userInfo,
			int pageNo, int pageSize, string lcId, string mcId, string scId, string sellCustNo, string brandList,
			int minPrice, int maxPrice, string sortType,
			string isFeeFree, string isMileage, string isDiscount, string isStamp, string isSmartDelivery, int startRank, int maxCount, long keywordSeqNo)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("api/Search/GetSmartClickItems?pageNo=" + pageNo + "&pageSize=" + pageSize +
			"&minPrice=" + minPrice + "&maxPrice=" + maxPrice + "&keywordSeqNo=" + keywordSeqNo);
			if (!string.IsNullOrEmpty(menuName))
				sb.Append("&menuName=" + menuName);
			if (!string.IsNullOrEmpty(keyword))
				sb.Append("&keyword=" + keyword);
			if (!string.IsNullOrEmpty(scKeyword))
				sb.Append("&scKeyword=" + scKeyword);
			if (!string.IsNullOrEmpty(lcId))
				sb.Append("&lcId=" + lcId);
			if (!string.IsNullOrEmpty(mcId))
				sb.Append("&mcId=" + mcId);
			if (!string.IsNullOrEmpty(scId))
				sb.Append("&scId=" + scId);
			if (!string.IsNullOrEmpty(sellCustNo))
				sb.Append("&sellCustNo=" + sellCustNo);
			if (!string.IsNullOrEmpty(brandList))
				sb.Append("&brandList=" + HttpUtility.UrlDecode(brandList));
			if (!string.IsNullOrEmpty(sortType))
				sb.Append("&sortType=" + sortType);
			if (!string.IsNullOrEmpty(isFeeFree))
				sb.Append("&isFeeFree=" + isFeeFree);
			if (!string.IsNullOrEmpty(isMileage))
				sb.Append("&isMileage=" + isMileage);
			if (!string.IsNullOrEmpty(isDiscount))
				sb.Append("&isDiscount=" + isDiscount);
			if (!string.IsNullOrEmpty(isStamp))
				sb.Append("&isStamp=" + isStamp);
			if (!string.IsNullOrEmpty(isSmartDelivery))
				sb.Append("&isSmartDelivery=" + isSmartDelivery);

			sb.Append("&startRank=" + startRank.ToString());
			sb.Append("&maxCount=" + maxCount.ToString());

			string userInfoKey = GMobileWebContext.EncodedCookieNameOfUserInfo;
			
			ApiResponse<List<CPPLPSRPItemModel>> result = ApiHelper.CallAPI<ApiResponse<List<CPPLPSRPItemModel>>>(
				"GET",
				ApiHelper.MakeUrl(sb.ToString()),
				new CookieParameter(userInfoKey, userInfo),
				ConnApiUtil.GetEXIDCookieParameter()
			);

			return result;
		}

		public ApiResponse<LPSRPBlockAdModel> GetPowerClickItems(string menuName, string keyword, string userInfo, string lcId, string mcId, string scId, int startRank, long keywordSeqNo,
			string isBrand, string isDiscount, string isShippingFree, string isMileage, string isSmartDelivery, string sellCustNo, int minPrice, int maxPrice)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("api/Search/GetPowerClickItems?keywordSeqNo=" + keywordSeqNo + "&startRank=" + startRank);
			if (!string.IsNullOrEmpty(menuName))
				sb.Append("&menuName=" + menuName);
			if (!string.IsNullOrEmpty(keyword))
				sb.Append("&keyword=" + keyword);
			if (!string.IsNullOrEmpty(lcId))
				sb.Append("&lcId=" + lcId);
			if (!string.IsNullOrEmpty(mcId))
				sb.Append("&mcId=" + mcId);
			if (!string.IsNullOrEmpty(scId))
				sb.Append("&scId=" + scId);

			if (!string.IsNullOrEmpty(isBrand))
				sb.Append("&isBrand=" + isBrand);
			if (!string.IsNullOrEmpty(isDiscount))
				sb.Append("&isDiscount=" + isDiscount);
			if (!string.IsNullOrEmpty(isShippingFree))
				sb.Append("&isShippingFree=" + isShippingFree);
			if (!string.IsNullOrEmpty(isMileage))
				sb.Append("&isMileage=" + isMileage);
			if (!string.IsNullOrEmpty(isSmartDelivery))
				sb.Append("&isSmartDelivery=" + isSmartDelivery);
			if (!string.IsNullOrEmpty(sellCustNo))
				sb.Append("&sellCustNo=" + sellCustNo);
			sb.Append("&minPrice=" + minPrice);
			sb.Append("&maxPrice=" + maxPrice);

			string userInfoKey = GMobileWebContext.EncodedCookieNameOfUserInfo;

			ApiResponse<LPSRPBlockAdModel> result = ApiHelper.CallAPI<ApiResponse<LPSRPBlockAdModel>>(
				"GET",
				ApiHelper.MakeUrl(sb.ToString()),
				new CookieParameter(userInfoKey, userInfo),
				new HeaderParameter("Client-Ip", GMKT.Component.Member.UserUtil.IPAddressBySecure(System.Web.HttpContext.Current))
			);

			return result;
		}

		public ApiResponse<LPSRPBlockAdModel> GetAdvPowerClickItems(string menuName, string keyword, string userInfo, string lcId, string mcId, string scId, int startRank, long keywordSeqNo, List<string> categories, List<string> brands,
			string isBrand, string isDiscount, string isShippingFree, string isMileage, string isSmartDelivery, string sellCustNo, int minPrice, int maxPrice)
		{
			GetAdvPowerClickItemRequestT req = new GetAdvPowerClickItemRequestT();
			req.menuName = menuName;
			req.keyword = keyword;
			req.lcId = lcId;
			req.mcId = mcId;
			req.scId = scId;
			req.startRank = startRank;
			req.keywordSeq = keywordSeqNo;
			req.categories = categories;
			req.brands = brands;

			req.isBrand = isBrand;
			req.isDiscount = isBrand;
			req.isShippingFree = isShippingFree;
			req.isMileage = isMileage;
			req.isSmartDelivery = isSmartDelivery;
			req.sellCustNo = sellCustNo;
			req.minPrice = minPrice;
			req.maxPrice = maxPrice;

			ApiResponse<LPSRPBlockAdModel> result = ApiHelper.CallAPI<ApiResponse<LPSRPBlockAdModel>>(
				"POST",
				ApiHelper.MakeUrl("api/Search/PostAdvPowerClickItems"),
				req,
				new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, userInfo),
				new HeaderParameter("Client-Ip", GMKT.Component.Member.UserUtil.IPAddressBySecure(System.Web.HttpContext.Current))
			);

			return result;
		}

		public ApiResponse<CPPCategoryBest100Model> GetCPPCategory(string lcId)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("api/Search/GetCPPCategory" );

			ApiResponse<CPPCategoryBest100Model> result = ApiHelper.CallAPI<ApiResponse<CPPCategoryBest100Model>>(
				"GET",
				ApiHelper.MakeUrl(sb.ToString()),
				new {
					lcId = lcId
				}
			);
			return result;
		}

		public ApiResponse<List<CategoryGroupInfo>> GetCategoryGroupList()
		{
			ApiResponse<List<CategoryGroupInfo>> result = ApiHelper.CallAPI<ApiResponse<List<CategoryGroupInfo>>>(
				"GET",
				ApiHelper.MakeUrl( "api/Search/GetCategoryGroupList" )
			);

			return result;
		}

		public ApiResponse<List<BestSellerGroupInfo>> GetBestSellerGroupList()
		{
			ApiResponse<List<BestSellerGroupInfo>> result = ApiHelper.CallAPI<ApiResponse<List<BestSellerGroupInfo>>>(
				"GET",
				ApiHelper.MakeUrl( "api/Search/GetBestSellerGroupList" )
			);

			return result;
		}

		public ApiResponse<SmartBoxModel> PostGetSmartBox(SearchRequest input, string userInfo)
		{
			ApiResponse<SmartBoxModel> result = ApiHelper.CallAPI<ApiResponse<SmartBoxModel>>(
				"POST",
				ApiHelper.MakeUrl("api/Search/PostGetSmartBox"),
				input,
				new CookieParameter(GMobileWebContext.EncodedCookieNameOfUserInfo, userInfo)
			);
			return result;
		}

		public ApiResponse<List<SmartBoxTileEntity>> PostGetSmartBoxDetail(SearchRequest input)
		{
			ApiResponse<List<SmartBoxTileEntity>> result = ApiHelper.CallAPI<ApiResponse<List<SmartBoxTileEntity>>>(
				"POST",
				ApiHelper.MakeUrl("api/Search/PostGetSmartBoxDetail"),
				input
			);
			return result;
		}

		public ApiResponse<GetAddCartResponseT> GetAddCartResult(string itemNo, short orderQty, bool isInstantOrder, string branchZipCode = "")
		{
			//request형식문제 error나서 임의값 "0" 부여..
			if (branchZipCode == "") branchZipCode = "0";

			ApiResponse<GetAddCartResponseT> result = ApiHelper.CallAPI<ApiResponse<GetAddCartResponseT>>(
					"GET"
					, ApiHelper.MakeUrl("api/Search/GetAddCartResult")
					, new
					{
						itemNo = itemNo,
						orderQty = orderQty,
						isInstantOrder = isInstantOrder,
						branchZipCode = branchZipCode
					}
					, ConnApiUtil.GetUserInfoCookieParameter()
					, ConnApiUtil.GetOrderInfoCookieParameter()
					, ConnApiUtil.GetEtcInfoCookieParameter()
					, ConnApiUtil.GetPguidCookieParameter()
					, ConnApiUtil.GetCguidCookieParameter()
					, ConnApiUtil.GetSguidCookieParameter()
			);
			return result;
		}
	}
}

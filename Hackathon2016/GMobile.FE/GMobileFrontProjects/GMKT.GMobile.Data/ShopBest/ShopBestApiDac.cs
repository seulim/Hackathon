using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

using ConnApi.Client;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Util;
using GMKT.GMobile.Data.ShopBest;



namespace GMKT.GMobile.Data
{
	public class ShopBestApiDac : ApiBase
	{
		public ShopBestApiDac() : base( "GMApi" ) { }

		public ApiResponse<List<Brand>> GetBrandList( string groupCode, int brandCount )
		{
			ApiResponse<List<Brand>> result = ApiHelper.CallAPI<ApiResponse<List<Brand>>>(
				"GET",
				ApiHelper.MakeUrl( "api/ShopBest/GetBrandList" ),
				new
				{
					groupCode = groupCode
					,	brandCount = brandCount
				}
			);
			return result;
		}

		public ApiResponse<BrandShopsData> GetBrandShopList( string groupCode, int brandNo, int pageNo, int pageSize, int itemCount )
		{
			ApiResponse<BrandShopsData> result = ApiHelper.CallAPI<ApiResponse<BrandShopsData>>(
				"GET",
				ApiHelper.MakeUrl( "api/ShopBest/GetBrandShopList" ),
				new
				{
					groupCode = groupCode
					,	brandNo = brandNo
					,	pageNo = pageNo
					,	pageSize = pageSize
					,	itemCount = itemCount
				}
			);
			return result;
		}

		public ApiResponse<BestShopsData> GetCategoryGroupShopList( string groupCode, int pageNo, int pageSize, int itemCount )
		{
			ApiResponse<BestShopsData> result = ApiHelper.CallAPI<ApiResponse<BestShopsData>>(
				"GET",
				ApiHelper.MakeUrl( "api/ShopBest/GetCategoryGroupShopList" ),
				new
				{
					groupCode = groupCode
					,	pageNo = pageNo
					,	pageSize = pageSize
					,	itemCount = itemCount
				}
			);
			return result;
		}

		public ApiResponse<NewShopsData> GetRecentShopList( string groupCode, int pageNo, int pageSize, int itemCount )
		{
			ApiResponse<NewShopsData> result = ApiHelper.CallAPI<ApiResponse<NewShopsData>>(
				"GET",
				ApiHelper.MakeUrl( "api/ShopBest/GetRecentShopList" ),
				new
				{
					groupCode = groupCode
					,	pageNo = pageNo
					,	pageSize = pageSize
					,	itemCount = itemCount
				}
			);
			return result;
		}

		public ApiResponse<List<MiniShopInfo>> GetFavoriteShop( string custNo )
		{
			dynamic urlParameters = new ExpandoObject();
			if( String.IsNullOrEmpty( custNo ) )
			{
				urlParameters.custNo = custNo;
			}
			ApiResponse<List<MiniShopInfo>> result = ApiHelper.CallAPI<ApiResponse<List<MiniShopInfo>>>(
				"GET",
				ApiHelper.MakeUrl( "api/ShopBest/GetFavoriteShop" )
				, urlParameters
				, ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

		public ApiResponse<List<ShopBestItem>> GetSellerItems( string sellCustNo, int pageNo, int pageSize )
		{
			ApiResponse<List<ShopBestItem>> result = ApiHelper.CallAPI<ApiResponse<List<ShopBestItem>>>(
				"GET",
				ApiHelper.MakeUrl( "api/ShopBest/GetSellerItems" )
				, new
				{
					encodeSellCustNo = EncodeUtil.custNoEncode( sellCustNo )
					, pageNo
					, pageSize
				}
			);
			return result;
		}

		//public ApiResponse<SRPResultModel> GetShopItems( string sellCustNo, int pageNo, int pageSize )
		//{
		//  ApiResponse<SRPResultModel> result = null;
		//  result = ApiHelper.CallAPI<ApiResponse<SRPResultModel>>(
		//    "GET",
		//    ApiHelper.MakeUrl( "api/Search/GetSearchItem" )
		//    , new
		//    {
		//      sellCustNo = EncodeUtil.custNoEncode(sellCustNo)
		//      ,	pageNo = pageNo
		//      ,	pageSize = pageSize
		//      , siteType = ""
		//    }
		//  );
		//  return result;
		//}

		public ApiResponse<string> SetFavoriteShop( string sellerCustNo, string custNo )
		{
			dynamic urlParameters = new ExpandoObject();
			urlParameters.SellerCustNo = sellerCustNo;
			if( String.IsNullOrEmpty( custNo ) )
			{
				urlParameters.custNo = custNo;
			}
			ApiResponse<string> result = ApiHelper.CallAPI<ApiResponse<string>>(
				"POST",
				ApiHelper.MakeUrl( "api/ShopBest/SetFavoriteShop" )
				,	urlParameters
				, ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

		public ApiResponse<string> SetFavoriteItem( string gdNo, string custNo, int groupNo )
		{
			dynamic urlParameters = new ExpandoObject();
			urlParameters.GdNo = gdNo;
			urlParameters.GroupNo = groupNo;
			if( String.IsNullOrEmpty( custNo ) )
			{
				urlParameters.custNo = custNo;
			}
			ApiResponse<string> result = ApiHelper.CallAPI<ApiResponse<string>>(
				"POST",
				ApiHelper.MakeUrl( "api/ShopBest/SetFavoriteItem" )
				, urlParameters
				, ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}

		public ApiResponse<string> RemoveFavoriteShop( string sellerCustNo, string custNo )
		{
			dynamic urlParameters = new ExpandoObject();
			urlParameters.SellerCustNo = sellerCustNo;
			if( String.IsNullOrEmpty( custNo ) )
			{
				urlParameters.custNo = custNo;
			}
			ApiResponse<string> result = ApiHelper.CallAPI<ApiResponse<string>>(
				"POST",
				ApiHelper.MakeUrl( "api/ShopBest/RemoveFavoriteShop" )
				, urlParameters
				, ConnApiUtil.GetUserInfoCookieParameter()
			);
			return result;
		}
	}
}

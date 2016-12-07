using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.ShopBest;

namespace GMKT.GMobile.Biz
{
	public class ShopBestApiBiz
	{
		public List<Brand> GetBrandList( string groupCode, int brandCount )
		{
			var response = new ShopBestApiDac().GetBrandList( groupCode, brandCount );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return new List<Brand>();
			}
		}

		public BestShopsData GetBestShopList( string groupCode, int pageNo, int pageSize, int itemCount )
		{
			var response = new ShopBestApiDac().GetCategoryGroupShopList( groupCode, pageNo, pageSize, itemCount );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return new BestShopsData();
			}
		}

		public NewShopsData GetNewShopList( string groupCode, int pageNo, int pageSize, int itemCount )
		{
			var response = new ShopBestApiDac().GetRecentShopList( groupCode, pageNo, pageSize, itemCount );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return new NewShopsData();
			}
		}

		public BrandShopsData GetBrandShopList( string groupCode, int brandNo, int pageNo, int pageSize, int itemCount )
		{
			var response = new ShopBestApiDac().GetBrandShopList( groupCode, brandNo, pageNo, pageSize, itemCount );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return new BrandShopsData();
			}
		}

		public List<MiniShopInfo> GetAllFavoriteShop( string custNo = null )
		{
			var response = new ShopBestApiDac().GetFavoriteShop( custNo );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return new List<MiniShopInfo>();
			}
		}

		public List<ShopBestItem> GetSellerItems( string sellCustNo, int pageSize, int pageNo = 1 )
		{
			var response = new ShopBestApiDac().GetSellerItems( sellCustNo, pageNo, pageSize );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return new List<ShopBestItem>();
			}
		}

		public List<ShopAndItems> GetFavoriteShopAndItems( int pageNo, int pageSize, int itemCount, ShopList model, string custNo = null )
		{
			List<ShopAndItems> result = new List<ShopAndItems>();
			List<MiniShopInfo> shops = GetAllFavoriteShop( custNo );

			model.TotalShopCount = shops.Count;

			ShopAndItems temp;

			int start = (pageNo - 1) * pageSize;

			try
			{
				shops = shops.Skip( start ).Take( pageSize ).ToList();
			}
			catch
			{
				shops = shops.Take( pageSize ).ToList();
			}

			if( shops != null && shops.Count > 0 )
			{
				foreach( MiniShopInfo i in shops )
				{
					temp = new ShopAndItems();
					temp.IsFavorite = true;
					temp.Shop = i;
					temp.Items = GetSellerItems( i.SellCustNo, itemCount );
					result.Add(temp);
				}
			}
			return result;
		}

		public string SetFavoriteShop( string sellerCustNo, string custNo = null  )
		{
			var response = new ShopBestApiDac().SetFavoriteShop( sellerCustNo, custNo );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return "";
			}
		}

		public string SetFavoriteItem( string gdNo, int groupNo, string custNo = null )
		{
			var response = new ShopBestApiDac().SetFavoriteItem( gdNo, custNo, groupNo );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return "";
			}
		}

		public string RemoveFavoriteShop( string sellerCustNo, string custNo = null )
		{
			var response = new ShopBestApiDac().RemoveFavoriteShop( sellerCustNo, custNo );
			if( response != null && response.ResultCode == 0 && response.Data != null )
			{
				return response.Data;
			}
			else
			{
				return "";
			}
		}
	}
}

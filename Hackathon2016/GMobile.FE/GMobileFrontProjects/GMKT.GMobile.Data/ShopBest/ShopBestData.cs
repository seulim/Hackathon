using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Data.ShopBest
{
	public class Brand
	{
		public long ID { get; set; }
		public string Name { get; set; }
		public string NameEng { get; set; }
		public string BRAND_IMAGE { get; set; }
		public int Rank { get; set; }
	}

	public class BestShopsData : ShopList
	{
	}

	public class BrandShopsData : ShopList
	{
		public int BrandNo { get; set; }
	}

	public class NewShopsData : ShopList
	{
	}

	public class FavoriteShopsData : ShopList
	{
	}

	public class ShopList
	{
		public int TotalShopCount { get; set; }
		public List<ShopAndItems> Shops { get; set; }
	}

	public class ShopAndItems
	{
		public int Rank { get; set; }
		public MiniShopInfo Shop { get; set; }
		public bool IsFavorite { get; set; }
		public List<ShopBestItem> Items { get; set; }
	}

	public class ShopBestItem : CPPLPSRPItemModel
	{
		public string AltrnativeImageUrl { get; set; }
	}

	public class MiniShopInfo
	{
		public string ShopUrl { get; set; }
		public string ShopLogoPath { get; set; }
		public string ShopRepImagePath { get; set; }
		public string ShopRepListImagePath { get; set; }
		public string ShopIntroContent { get; set; }
		public string ShopName { get; set; }
		public string SellCustNo { get; set; }
		public int ShopLevel { get; set; }
		public int ShopLogoType { get; set; }
		public int ShopRepType { get; set; }
		public int MileageValue { get; set; }
		public DangolPolicy DangolCostPolicy { get; set; }
	}

	public class DangolPolicy
	{
		public int DangolCostRate { get; set; }
		public int DangolCostLimitCnt { get; set; }
		public int DangolCostLimitMoney { get; set; }
		public string DangolCostType { get; set; }
	}
}

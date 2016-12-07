using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;

using Arche.Data.Voyager;

using GMobile.Data.Tiger;
using GMobile.Service.Home;
using GMobile.Data.Voyager;
using GMobile.Service.Search;
using GMKT.GMobile.Util;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;

namespace GMobile.Service.Tests
{
	[TestFixture]
	public class GMobileShopTest
	{
		[TestCase("gtesttest123")]
		[TestCase("test1212sd")]
		[TestCase("wlakzpt123")]
		[TestCase("cristata")]
		public void GetNavigationNewsList(string alias)
		{
			ShopT shopinfo = GetShopinfo(alias);

			if (shopinfo == null)
			{
				Trace.WriteLine("판매자 정보가 없습니다");
				return;
			}

			List<ShopNewsT> list = new ShopNewsBiz().GetNavigationNewsList(shopinfo.SellerId);

			foreach (ShopNewsT news in list)
			{
				Trace.DumpBusinessEntity(news);
			}

		}

		[TestCase("gtesttest123")]
		[TestCase("test1212sd")]
		[TestCase("wlakzpt123")]
		[TestCase("cristata")]
		public void GetShopNewsList(string alias)
		{
			ShopT shopinfo = GetShopinfo(alias);

			if (shopinfo == null)
			{
				Trace.WriteLine("판매자 정보가 없습니다");
				return;
			}

			List<ShopNewsT> list = new ShopNewsBiz().GetNavigationNewsList(shopinfo.SellerId);

			if (list == null || list.Count <= 0)
			{
				Trace.WriteLine("데이터가 없습니다");
				return;
			}

			foreach (ShopNewsT news in list)
			{
				Trace.DumpBusinessEntity(news);
			}

		}

		[TestCase("gtesttest123")]
		[TestCase("test1212sd")]
		[TestCase("wlakzpt123")]
		[TestCase("cristata")]
		public void GetShop(string alias)
		{
			ShopT shopinfo = new ShopBiz().GetShop(alias);

			if (shopinfo == null)
			{
				Trace.WriteLine("데이터가 없습니다");
				return;
			}

			Trace.DumpBusinessEntity(shopinfo);
		}

		[TestCase("gtesttest123")]
		[TestCase("114653890")]
		[TestCase("119594380")]
		[TestCase("114653826")]
		public void GetSeller(string seller_no)
		{
			SellerT sellerinfo = new SellerBiz().GetSeller(seller_no);

			if (sellerinfo == null)
			{
				Trace.WriteLine("데이터가 없습니다");
				return;
			}

			Trace.DumpBusinessEntity(sellerinfo);
		}

		[TestCase("gtesttest123")]
		[TestCase("test1212sd")]
		[TestCase("wlakzpt123")]
		[TestCase("cristata")]
		public void GetShopCategories(string alias)
		{
			ShopT shopinfo = new ShopBiz().GetShop(alias);

			if (shopinfo == null)
			{
				Trace.WriteLine("판매자 정보가 없습니다");
				return;
			}

			Trace.WriteLine(String.Format("판매자 alias={0} cust_no={1}", alias, shopinfo.SellerId));

			IEnumerable<ShopCategoryT> shopcatelist = new ShopCategoryBiz().GetShopCategories(shopinfo.SellerId);

			if (shopcatelist == null)
			{
				Trace.WriteLine("데이터가 없습니다");
				return;
			}

			foreach (ShopCategoryT cate in shopcatelist)
			{
				Trace.DumpBusinessEntity(cate);
			}
		}

		[TestCase("gtesttest123", "01000000")]
		[TestCase("gtesttest123", "01010000")]
		[TestCase("gtesttest123", "02000000")]
		[TestCase("gtesttest123", "02010000")]
		public void CountShopCategoryProduct(string alias, string shopcategoryCode)
		{
			ShopT shopinfo = new ShopBiz().GetShop(alias);

			if (shopinfo == null)
			{
				Trace.WriteLine("판매자 정보가 없습니다");
				return;
			}

			Trace.WriteLine(String.Format("판매자 alias={0} cust_no={1}", alias, shopinfo.SellerId));

			int itemcount = new ShopCategoryBiz().CountShopCategoryProduct(shopcategoryCode, shopinfo.SellerId);

			Trace.WriteLine(String.Format("shopcategoryCode={0} count={1}", shopcategoryCode, itemcount));
		}

		[TestCase("gtesttest123")]
		[TestCase("test1212sd")]
		[TestCase("wlakzpt123")]
		[TestCase("cristata")]
		public void GetShopCategoryInfoList(string alias)
		{
			ShopT shopinfo = GetShopinfo(alias);

			if (shopinfo == null)
			{
			Trace.WriteLine("판매자 정보가 없습니다");
			return;
			}

			Trace.WriteLine(String.Format("판매자 alias={0} cust_no={1} CategoryDisplay={2}", alias, shopinfo.SellerId, shopinfo.CategoryDisplay.ToString()));

			Trace.WriteLine("===================G마켓 카테고리===================================");
			IEnumerable<ShopCategoryInfoT> shopcatelist = new ShopCategoryBiz().GetShopCategoryInfoList(shopinfo.SellerId, CategoryType.General);

			if (shopcatelist == null)
			{
				Trace.WriteLine("데이터가 없습니다");
				return;
			}

			foreach (ShopCategoryInfoT cateinfo in shopcatelist)
			{
				Trace.DumpBusinessEntity(cateinfo);
			}


			Trace.WriteLine("===================미니샵 카테고리===================================");
			IEnumerable<ShopCategoryInfoT> shopcatelist2 = new ShopCategoryBiz().GetShopCategoryInfoList(shopinfo.SellerId, CategoryType.Shop);

			if (shopcatelist2 == null)
			{
				Trace.WriteLine("데이터가 없습니다");
				return;
			}

			foreach (ShopCategoryInfoT cateinfo2 in shopcatelist2)
			{
				Trace.DumpBusinessEntity(cateinfo2);
			}
		}


		private ShopT GetShopinfo(string alias)
		{
			return new ShopBiz().GetShop(alias);
		}


		// 샵 카테고리로 상품만 검색
		[TestCase("114653826", "01000000")]
		[TestCase("114653826", "02000000")]
		[TestCase("100605608", "03000000")]
		[TestCase("100605608", "04000000")]
		[TestCase("100605608", "05000000")]
		public void GetShopItemsFromShopCategoryCode(string sellerCustNo, string shopcategory)
		{
			SearchItemT[] items;
			items = new ShopSearchBiz().GetSellerItem(sellerCustNo, "", shopcategory, true, 1, 20, DisplayOrder.FocusRankDesc);

			Trace.DumpBusinessEntity(items);
		}

		// 모든 샵 카테고리 정보 가져옴(캐쉬)
		[TestCase("114653826")]
		public void GetAllShopCategoriesFromVoyager(string sellerCustNo)
		{
			List<CategoryInfoT> shopCategories = new ShopSearchBiz().GetAllShopCategoriesFromCache(sellerCustNo);
			Trace.DumpBusinessEntity(shopCategories);
		}

		// 샵 카테고리 정보 가져옴
		[TestCase("114653826")]
		public void GetShopCategoriesFromVoyager(string sellerCustNo)
		{
			List<CategoryInfoT> shopCategories = new ShopSearchBiz().GetCategories(sellerCustNo, ShopCategoryDisplayType.LargeMediumCategory);
			Trace.DumpBusinessEntity(shopCategories);
		}

		// 샵 카테고리로 검색(아이템 + 카테고리)
		[TestCase("114653826", "01000000")]
		public void GetShopItemsFromShopcategory(string sellerCustNo, string shopcategory)
		{
			SearchResultT result = new ShopSearchBiz().GetSearchResult(sellerCustNo, "", shopcategory, true, 1, 20, DisplayOrder.FocusRankDesc);
			Trace.DumpBusinessEntity(result);
		}

		
	}
}

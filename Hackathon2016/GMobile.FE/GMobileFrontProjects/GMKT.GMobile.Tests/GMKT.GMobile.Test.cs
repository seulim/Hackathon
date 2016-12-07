using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using GMobile.Data.Tiger;
using GMobile.Service.Home;
using GMobile.Data.Voyager;
using GMobile.Service.Search;
using GMKT.GMobile.Util;
using GMKT.GMobile.Biz;

using Arche.Data.Voyager;
using GMKT.GMobile.Data;

namespace GMobile.Service.Tests
{
	[TestFixture]
	public class GMobileServiceTest
	{
		[TestCase]
		public void APIGetBest100GroupCategoryInfo()
		{
			List<Best100GroupCateogyDetail> result = new BestItemApiBiz().GetBest100GroupCategoryInfo();
			Trace.DumpBusinessEntity(result);
		}

		[TestCase]
		public void APIGetBest100CategoryList()
		{
			List<Best100CateogyDetail> result = new BestItemApiBiz().GetBest100CategoryList();
			Trace.DumpBusinessEntity(result);
		}

		[TestCase]
		public void APIGetBest100Items()
		{
			List<SearchItemModel> result = new BestItemApiBiz().GetBest100Items(1, 100);
			Trace.DumpBusinessEntity(result);
		}

		[TestCase("0", 1, 20)]
		public void APIGetBest100GroupItems(string groupCode, int pageNo, int pageSize)
		{
			SearchResultModel result = new BestItemApiBiz().GetBest100GroupItems(groupCode, pageNo, pageSize);
			Trace.DumpBusinessEntity(result);
		}

		[TestCase]
		public void GetItemsTest()
		{
			List<string> itemnos = new List<string>();
			itemnos.Add("173776169");
			itemnos.Add("404517624");
			itemnos.Add("390922445");
			SearchItemT[] result = new SearchBiz().GetItems(itemnos, DisplayOrder.RankPointDesc);
			Trace.DumpBusinessEntity(result);
		}

		public void GetSellerItem()
		{
			string sellerCustNo = "";
			string keyword = "";
			int pageNo = 1;
			int pageSize = 20;
			DisplayOrder sort = DisplayOrder.FocusRankDesc;

			// sellerCustNo 는 필수, pageNo, pageSize 1이상
			SearchItemT[] result = new ShopSearchBiz().GetSellerItem(sellerCustNo, keyword, pageNo, pageSize, sort);
			Trace.DumpBusinessEntity(result);
		}
		

		[TestCase("티ㅅ")]
		public void GetSuggestKeyword(string keyword)
		{
			SuggestionKeywordT suggest = new SuggestionKeywordBiz().GetSuggestionKeyword(keyword);

			Trace.DumpBusinessEntity(suggest);
		}

		[TestCase("100000003")]
		public void GetCategoryNm(string code)
		{
			string categoryNm = CategoryUtil.GetCategoryName(code);

			ArrayList categoryList = CategoryUtil.GetXMLCategoryInfo("L", code);

			Trace.WriteLine(categoryNm);

			//Trace.DumpBusinessEntity(categoryList);
		}

		[TestCase("나이키", 10)]
		public void GetSearchItem(string keyword, int rowCount)
		{
			ItemFilter filter = new ItemFilter { ItemName = keyword };

			//RankPoint순으로 정렬, 상품번호로 2차 정렬
			SortCollection sc = new SortCollection();
			sc.Add(Sort.Create("RankPoint", SortOrder.Decreasing));
			sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));

			//Grouping
			HistogramQueryCollection hqc = new HistogramQueryCollection();
			// 대, 중,소분류 필드로 그룹핑. 카운트 많은 순으로 정렬
			hqc.Add(new HistogramQuery(Tables.ItemTable.LCode, HistogramSortOrder.Decrc));

			QueryResult result = new SearchItemBiz().GetQueryResult(filter, 0, rowCount, sc, hqc);

			Trace.WriteLine(filter.GetExpression());

			SearchItemT[] items = (SearchItemT[])result.Entities;

			foreach (SearchItemT item in items)
			{
				Trace.DumpBusinessEntity(item);
			}

			Histogram hs = result.Histograms[Tables.ItemTable.LCode];

			// Grouping Result
			foreach (HistogramEntry entry in hs)
			{
				Trace.DumpBusinessEntity(entry);
			}
		}

		[TestCase("티셔츠", "반팔")]
		public void GetSearchItem(string keyword, string scKeyword)
		{
			ItemFilter filter = new ItemFilter { ItemName = keyword  + " " + scKeyword };

			//RankPoint순으로 정렬, 상품번호로 2차 정렬
			SortCollection sc = new SortCollection();
			sc.Add(Sort.Create("RankPoint", SortOrder.Decreasing));
			sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));

			//Grouping
			HistogramQueryCollection hqc = new HistogramQueryCollection();
			// 대, 중,소분류 필드로 그룹핑. 카운트 많은 순으로 정렬
			hqc.Add(new HistogramQuery(Tables.ItemTable.LCode, HistogramSortOrder.Decrc));

			QueryResult result = new SearchItemBiz().GetQueryResult(filter, 0, 50, sc, hqc);

			Trace.WriteLine(filter.GetExpression());

			SearchItemT[] items = (SearchItemT[])result.Entities;

			foreach (SearchItemT item in items)
			{
				Trace.DumpBusinessEntity(item);
			}
		}

		[TestCase]
		public void GetDate()
		{
			string date = DateTime.Now.ToString(string.Format("MM월 dd일 ddd요일"));
			Trace.WriteLine(date);

			date = DateTime.Now.ToString(string.Format("M월 dd일 (ddd)"));
			Trace.WriteLine(date);
		}

		[TestCase("test4plan", "114653826")]
		public void GetSellerItemList(string seller_id, string seller_cust_no)
		{
			ItemFilter itemFilter = new ItemFilter();
			itemFilter.ItemName = seller_id;
			itemFilter.SellCustNo = seller_cust_no;

			//판매인기순
			SortCollection sort = new SortCollection();
			//sort.Add(Sort.Create("SellPointInfo", SortOrder.Decreasing));
			sort.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
			sort.Add(Sort.Create("ItemID", SortOrder.Decreasing));

			//Grouping
			HistogramQueryCollection hqc = new HistogramQueryCollection();
			// 대, 중,소분류 필드로 그룹핑. 카운트 많은 순으로 정렬
			hqc.Add(new HistogramQuery(Tables.ItemTable.LCode, HistogramSortOrder.Decrc));

			Trace.WriteLine(itemFilter.GetExpression());

			QueryResult result = new SearchItemBiz().GetQueryResult(itemFilter, 0, 50, sort, hqc);

			SearchItemT[] items = (SearchItemT[])result.Entities;

			foreach (SearchItemT item in items)
			{
				Trace.DumpBusinessEntity(item);
			}
		}
	}
}

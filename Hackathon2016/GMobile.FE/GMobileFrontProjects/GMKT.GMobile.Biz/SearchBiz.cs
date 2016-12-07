using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Arche.Data.Voyager;
using GMobile.Data.Voyager;
using GMobile.Service.Search;
using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;

namespace GMKT.GMobile.Biz
{
	public class SearchBiz : BizBase
	{
        private const string ShopCodeSfc = "16";

		public SearchItemT[] GetItems(List<string> itemNoList, DisplayOrder sort)
		{
			if (itemNoList == null || itemNoList.Count == 0)
			{
				return new SearchItemT[0];
			}

			SearchItemT[] result = null;

			ItemFilter filter = new ItemFilter();
			filter.ItemNoList = itemNoList;

			result = new SearchItemBiz().GetItems(filter, 0, itemNoList.Count, GetSortCollection(sort));

			return result;
		}

		public SearchItemT[] GetItems(List<string> itemNoList, string sortType)
		{
			if (itemNoList == null || itemNoList.Count == 0)
			{
				return new SearchItemT[0];
			}

			SearchItemT[] result = null;

			ItemFilter filter = new ItemFilter();
			filter.ItemNoList = itemNoList;

			#region ::: Set Sort Option
			SortCollection sc = new SortCollection();
			switch (sortType.ToLower())
			{
				case "focus_rank_desc":
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "rank_point_desc":
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "sell_point_desc":
					sc.Add(Sort.Create("SellPointInfo", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "disc_price_desc":
					sc.Add(Sort.Create("MACRO(DiscountPrice)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					break;
				case "sell_price_asc":
					sc.Add(Sort.Create("MACRO(SellPriceSrch)", SortOrder.Increasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "sell_price_desc":
					sc.Add(Sort.Create("MACRO(SellPriceSrch)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "new":
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case "con_point_desc":
					sc.Add(Sort.Create("ConPoint", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				default:
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
			}
			#endregion

			result = new SearchItemBiz().GetItems(filter, 0, itemNoList.Count, sc);

			return result;
		}

		public SearchItemT[] GetSearchItem(string keyword, string sellerCustNo, string shopcategory, int pageNo, int pageSize, DisplayOrder sort, GMKT.Web.Membership.ExIdAppEnum exid = GMKT.Web.Membership.ExIdAppEnum.None)
		{
			ItemFilter filter = new ItemFilter();

			// 키워드
			if (!String.IsNullOrEmpty(keyword) && keyword.Length > 0)
			{
				filter.ItemName = keyword;
			}

			// 판매자 번호
			if (!String.IsNullOrEmpty(sellerCustNo) && sellerCustNo.Length > 0)
			{
				filter.SellCustNo = sellerCustNo;
			}

			if (!String.IsNullOrEmpty(shopcategory) && shopcategory.Length > 0)
			{
				filter.MiniShopCategory = shopcategory;
			}

            //SFC 예외처리
            if (exid != GMKT.Web.Membership.ExIdAppEnum.SFC && !filter.ShopGroupCdNotOrList.Contains(ShopCodeSfc))
            {
                filter.ShopGroupCdNotOrList.Add(ShopCodeSfc);
            }

			return GetSearchResult(filter, pageNo, pageSize, sort, CategoryLevel.None).Items;
		}

		public SearchItemT[] GetSearchItem(string keyword, string sellerCustNo, string gdlc, string gdmc, string gdsc, int pageNo, int pageSize, DisplayOrder sort, GMKT.Web.Membership.ExIdAppEnum exid = GMKT.Web.Membership.ExIdAppEnum.None)
		{
			ItemFilter filter = new ItemFilter();

			// 키워드
			if (!String.IsNullOrEmpty(keyword) && keyword.Length > 0)
			{
				filter.ItemName = keyword;
			}

			// 판매자 번호
			if (!String.IsNullOrEmpty(sellerCustNo) && sellerCustNo.Length > 0)
			{
				filter.SellCustNo = sellerCustNo;
			}

			List<string> categoryCodeList = new List<string>();

			if (!String.IsNullOrEmpty(gdsc))
			{
				categoryCodeList.Add(gdsc);
			}
			else if (!String.IsNullOrEmpty(gdmc))
			{
				categoryCodeList.Add(gdmc);
			}
			else if (!String.IsNullOrEmpty(gdlc))
			{
				categoryCodeList.Add(gdlc);
			}
			//if (!String.IsNullOrEmpty(gdlc) && String.IsNullOrEmpty(gdmc) && String.IsNullOrEmpty(gdsc))
			//{
			//    categoryCodeList.Add(gdlc);
			//}
			//else if (!String.IsNullOrEmpty(gdlc) && !String.IsNullOrEmpty(gdmc) && String.IsNullOrEmpty(gdsc))
			//{
			//    categoryCodeList.Add(gdmc);
			//}
			//else if (!String.IsNullOrEmpty(gdlc) && !String.IsNullOrEmpty(gdmc) && !String.IsNullOrEmpty(gdsc))
			//{
			//    categoryCodeList.Add(gdsc);
			//}

			// 카테고리
			if (categoryCodeList.Count > 0)
			{
				filter.CategoryOrList = categoryCodeList;
			}

            //SFC 예외처리
            if (exid != GMKT.Web.Membership.ExIdAppEnum.SFC && !filter.ShopGroupCdNotOrList.Contains(ShopCodeSfc))
            {
                filter.ShopGroupCdNotOrList.Add(ShopCodeSfc);
            }

			return GetSearchResult(filter, pageNo, pageSize, sort, CategoryLevel.None).Items;
		}

		public SearchResultT GetSearchResult(string keyword, string sellerCustNo, string gdlc, string gdmc, string gdsc, int pageNo, int pageSize, DisplayOrder sort)
		{
			ItemFilter filter = new ItemFilter();

			// 키워드
			if (!String.IsNullOrEmpty(keyword) && keyword.Length > 0)
			{
				filter.ItemName = keyword;
			}

			// 판매자 번호
			if (!String.IsNullOrEmpty(sellerCustNo) && sellerCustNo.Length > 0)
			{
				filter.SellCustNo = sellerCustNo;
			}

			CategoryLevel level = CategoryLevel.None;
			List<string> categoryCodeList = new List<string>();

			if (String.IsNullOrEmpty(gdlc) && String.IsNullOrEmpty(gdmc) && String.IsNullOrEmpty(gdsc))
			{
				level = CategoryLevel.LargeCategory;
			}
			else
			{
				if (!String.IsNullOrEmpty(gdsc))
				{
					level = CategoryLevel.SmallCategory;
					categoryCodeList.Add(gdsc);
				}
				else if (!String.IsNullOrEmpty(gdmc))
				{
					level = CategoryLevel.SmallCategory;
					categoryCodeList.Add(gdmc);
				}
				else if (!String.IsNullOrEmpty(gdlc))
				{
					level = CategoryLevel.MediumCategory;
					categoryCodeList.Add(gdlc);
				}
				//if (!String.IsNullOrEmpty(gdsc))
				//{
				//    level = CategoryLevel.SmallCategory;
				//    categoryCodeList.Add(gdsc);
				//}
				//else if (!String.IsNullOrEmpty(gdmc))
				//{
				//    level = CategoryLevel.MediumCategory;
				//    categoryCodeList.Add(gdmc);
				//}
				//else if (!String.IsNullOrEmpty(gdlc))
				//{
				//    level = CategoryLevel.LargeCategory;
				//    categoryCodeList.Add(gdlc);
				//}
				//if (!String.IsNullOrEmpty(gdlc) && String.IsNullOrEmpty(gdmc) && String.IsNullOrEmpty(gdsc))
				//{
				//    level = CategoryLevel.LargeCategory;
				//    categoryCodeList.Add(gdlc);
				//}
				//else if (!String.IsNullOrEmpty(gdlc) && !String.IsNullOrEmpty(gdmc) && String.IsNullOrEmpty(gdsc))
				//{
				//    level = CategoryLevel.MediumCategory;
				//    categoryCodeList.Add(gdmc);
				//}
				//else if (!String.IsNullOrEmpty(gdlc) && !String.IsNullOrEmpty(gdmc) && !String.IsNullOrEmpty(gdsc))
				//{
				//    level = CategoryLevel.SmallCategory;
				//    categoryCodeList.Add(gdsc);
				//}
			}

			// 카테고리
			if (categoryCodeList.Count > 0)
			{
				filter.CategoryOrList = categoryCodeList;
			}

			return GetSearchResult(filter, pageNo, pageSize, sort, level);

		}

		public SearchResultT GetSearchResult(ItemFilter filter, int pageNo, int pageSize, DisplayOrder sort, CategoryLevel level)
		{
			SearchResultT result = new SearchResultT();

			QueryResult queryResult = new SearchItemBiz().GetQueryResult(filter, (pageNo - 1) * pageSize, pageSize, GetSortCollection(sort), GetHistogramQueryCollection(level));

			Histogram hs1 = null;
			Histogram hs2 = null;

			switch (level)
			{
				case CategoryLevel.LargeCategory:
					hs1 = (Histogram)queryResult.Histograms[Tables.ItemTable.LCode];
					hs2 = (Histogram)queryResult.Histograms[Tables.ItemTable.MCode];
					break;
				case CategoryLevel.MediumCategory:
					hs1 = (Histogram)queryResult.Histograms[Tables.ItemTable.MCode];
					hs2 = (Histogram)queryResult.Histograms[Tables.ItemTable.SCode];
					break;
				case CategoryLevel.SmallCategory:
					hs1 = (Histogram)queryResult.Histograms[Tables.ItemTable.SCode];
					hs2 = null;
					break;
				default:
					hs1 = null;
					hs2 = null;
					break;
			}

			if (hs1 != null)
			{
				List<CategoryInfoT> categoryList = new List<CategoryInfoT>();
				CategoryInfoT category;

				foreach (HistogramEntry entry in hs1)
				{
					category = CategoryBiz.GetCategoryInfo(entry.Value.ToString());

					if (category.Level != CategoryLevel.GroupCategory)
					{
						category.ItemCount = entry.Count;
						categoryList.Add(category);
					}
				}
				result.CategoryInfo = categoryList;
			}
			if (hs2 != null)
			{
				List<CategoryInfoT> categoryList = new List<CategoryInfoT>();
				CategoryInfoT category;

				foreach (HistogramEntry entry in hs2)
				{
					category = CategoryBiz.GetCategoryInfo(entry.Value.ToString());

					if (category.Level != CategoryLevel.GroupCategory)
					{
						category.ItemCount = entry.Count;
						categoryList.Add(category);
					}
				}
				result.SubCategoryInfo = categoryList;
			}

			result.Items = (SearchItemT[])queryResult.Entities;
			result.TotalCount = queryResult.Size;

			return result;
		}

		private HistogramQueryCollection GetHistogramQueryCollection(CategoryLevel? level)
		{
			HistogramQueryCollection hqc = new HistogramQueryCollection();
			string categoryGroupField1 = Tables.ItemTable.LCode;
			string categoryGroupField2 = "";

			switch (level)
			{
				case CategoryLevel.LargeCategory:
					categoryGroupField1 = Tables.ItemTable.LCode;
					categoryGroupField2 = Tables.ItemTable.MCode;
					break;
				case CategoryLevel.MediumCategory:
					categoryGroupField1 = Tables.ItemTable.MCode;
					categoryGroupField2 = Tables.ItemTable.SCode;
					break;
				case CategoryLevel.SmallCategory:
					categoryGroupField1 = Tables.ItemTable.SCode;
					break;
				case CategoryLevel.None:
					categoryGroupField1 = "";
					break;
				default:
					categoryGroupField1 = Tables.ItemTable.LCode;
					break;
			}

			if (String.IsNullOrEmpty(categoryGroupField1))
			{
				hqc = null;
				return hqc;
			}

			hqc.Add(new HistogramQuery(categoryGroupField1, HistogramSortOrder.Decrc));
			if (!String.IsNullOrEmpty(categoryGroupField2))
			{
				hqc.Add(new HistogramQuery(categoryGroupField2, HistogramSortOrder.Decrc));
			}
			return hqc;
		}

		public static SortCollection GetSortCollection(DisplayOrder sort)
		{
			SortCollection sc = new SortCollection();
			switch (sort)
			{
				case DisplayOrder.FocusRankDesc:
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case DisplayOrder.RankPointDesc:
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case DisplayOrder.SellPointDesc:
					sc.Add(Sort.Create("SellPointInfo", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case DisplayOrder.DiscountPriceDesc:
					sc.Add(Sort.Create("MACRO(DiscountPrice)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					break;
				case DisplayOrder.SellPriceAsc:
					sc.Add(Sort.Create("MACRO(SellPriceSrch)", SortOrder.Increasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case DisplayOrder.SellPriceDesc:
					sc.Add(Sort.Create("MACRO(SellPriceSrch)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case DisplayOrder.New:
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				case DisplayOrder.ConPointDesc:
					sc.Add(Sort.Create("ConPoint", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
				default:
					sc.Add(Sort.Create("MACRO(IsPremium)", SortOrder.Decreasing));
					sc.Add(Sort.Create("RankPoint2", SortOrder.Decreasing));
					sc.Add(Sort.Create("ItemID", SortOrder.Decreasing));
					break;
			}
			return sc;
		}

		// 통합검색 테스트 필요 TODO
		#region 통합검색
		public SearchResultT GetSearchResult(string keyword, int rowCount, int pageNo, int minPrice, int maxPrice, string prevKeyword
											, string gdlcCd, string gdmcCd, string gdscCd, DisplayOrder sort, string isFeeFree, string isMileage, string isDiscount, string isStamp)
		{
			SearchResultT result = new SearchResultT();

			string categoryCode = string.Empty;
			string CategoryGroupField = "LCode";
			int categoryLevel = 0;

			#region ::: Set Category
			if (gdlcCd == "" && gdmcCd == "" && gdscCd == "")
			{
				CategoryGroupField = "LCode";
			}
			else
			{
				if (gdlcCd != "" && gdmcCd == "" && gdscCd == "")
				{
					categoryLevel = 1;
					string[] arrGdlcTemp = gdlcCd.Split(',');
					if (arrGdlcTemp.Length > 1)
						CategoryGroupField = "LCode";
					else
						CategoryGroupField = "MCode";

					categoryCode = gdlcCd.Replace(",", "|");
				}
				else if (gdlcCd != "" && gdmcCd != "" && gdscCd == "")		// 중분류
				{
					categoryLevel = 2;
					string[] arrGdmcTemp = gdmcCd.Split(',');
					if (arrGdmcTemp.Length > 1) // 중분류 복수선택
					{
						CategoryGroupField = "MCode";
					}
					else
					{
						CategoryGroupField = "SCode";
					}

					categoryCode = gdmcCd.Replace(",", "|");
				}
				else if (gdlcCd != "" && gdmcCd != "" && gdscCd != "")
				{
					categoryLevel = 3;
					CategoryGroupField = "SCode";
					categoryCode = gdscCd.Replace(",", "|");
				}
			}
			#endregion

			#region ::: Set Search Option
			ItemFilter filter = new ItemFilter();

			if (prevKeyword.Length > 0)
				filter.ItemName = keyword + " " + prevKeyword;
			else
				filter.ItemName = keyword;

			// 가격 필터링
			if (minPrice > 0 && maxPrice > 0)
			{
				filter.SellPrice.Min = minPrice;
				filter.SellPrice.Max = maxPrice;
			}
			// 배송비 무료
			if ("Y".Equals(isFeeFree))
				filter.DeliveryFeeCondition = DeliveryFeeCondition.FreeOfCharge;
			// 마일리지 여부
			if ("Y".Equals(isMileage))
				filter.BuyerMlRate.Min = 1;
			// 할인 여부
			if ("Y".Equals(isDiscount))
				filter.DiscountPriceYN = true;
			// 스탬프 여부
			if ("Y".Equals(isStamp))
				filter.StickerCnt.Min = 1;

			#endregion

			#region ::: 카테고리 필터 설정
			List<string> categoryList = new List<string>();
			List<string> chkCategoryList = new List<string>();
			List<string> chkParentCategoryList = new List<string>();

			// 지정어 사전 확인
			string jijungString = string.Empty;

			if (categoryCode.Length > 1)
			{
				if (categoryCode.Substring(categoryCode.Length - 1) != "")
				{
					categoryCode += "|";
				}
			}

			QADicFinder finder = new QADicFinder(VoyagerHelper.GetConnectionString("FilterFinder"));
			jijungString = finder.GetJijung(keyword.Replace(" ", ""));

			// 지정어 사전에 포함되어 있다면,
			if (jijungString.Length > 0)
			{
				string[] valArray = jijungString.Split(':');
				int catLen = valArray.Length - 10;

				string catType = valArray[0].Trim();
				string catCode = string.Empty;

				// 카테고리 설정 내용이 있어야 하고 적용여부가 N 이 아닐때
				if (catLen > 0 && !catType.ToUpper().Equals("N"))
				{
					if (categoryLevel <= 1)
					{
						// 카테고리 Navi중 가장 마지막 Depth의 카테고리만 저장한다.
						for (int i = 0; i < catLen; i++)
						{
							catCode = valArray[10 + i].Trim();
							string[] catNavi = catCode.Split('>');
							categoryList.Add(catNavi[catNavi.Length - 1]);
						}

					}

					// 적용된 카테고리 리스트를 검색조건에 추가한다. 
					// 'I' 일경우 지정어 사전에 포함된 카테고리가 검색, 그렇지 않은 경우 제외된 카테고리 검색.
					if (catType.ToUpper().Equals("I"))
					{
						if (categoryLevel > 0)
						{
							List<string> CategoryTemp = categoryCode.TrimEnd('|').Split('|').ToList();
							foreach (string s in CategoryTemp)
							{
								bool isAdd = true;
								for (int i = 0; i < categoryList.Count; i++)
								{
									if (s == categoryList[i])
									{
										isAdd = false;
										break;
									}
								}

								if (isAdd)
								{
									if (categoryLevel > 1)
									{
										categoryList.Add(s);
									}
								}
							}
						}

						filter.CategoryOrList = categoryList;
					}
					else
					{
						if (categoryLevel > 0)
						{
							filter.CategoryOrList = categoryCode.TrimEnd('|').Split('|').ToList();
						}
						filter.CategoryNotOrList = categoryList;
					}
				}
				else
				{
					if (categoryLevel > 0)
					{
						filter.CategoryOrList = categoryCode.TrimEnd('|').Split('|').ToList();
					}
				}
			}
			// 카테고리가 존재한다면, 전시 카테고리 확인
			else if (categoryLevel > 0)
			{
				QADicFinder dpFinder = new QADicFinder(VoyagerHelper.GetConnectionString("FilterFinder"));
				string dpString = dpFinder.GetDisplayCategory(categoryCode);

				// 전시카테고리 정보 있음. 처리
				if (dpString.Length > 0)
				{
					// 입력받은 카테고리와 전시카테고리를 Append
					dpString = dpString + categoryCode;
					categoryList = dpString.TrimEnd('|').Split('|').ToList();

					// pclass_cd = 100000XXXX 이면서 class_cd가 2..... 로 시작되는 전시중분류와 3... 로 시작하는 전시소분류가 같이 있을경우
					if (categoryLevel == 1)
					{
						// 해당 경우 GroupBy 결과를 조작해야 한다.
						/*
						foreach (string chkCategory in categoryList)
						{
								if (chkCategory.StartsWith("3"))
								{
										chkCategoryList.Add(chkCategory);
										// 상위 중분류를 결과에서 GroupBy에서 빼기 위해 체크한다.
										//string catInfo = CategoryUtil.GetXMLCategoryItem("S", chkCategory);

										// 해당 전시소분류의 상위분류가 조건에 포함되지 않았을 경우만 체크하여 ..
										if (!categoryList.Contains(catInfo))
										{
												chkParentCategoryList.Add(catInfo);
										}
								}
						}
						*/

						filter.CategoryOrList = categoryList;
					}
					else
					{
						filter.CategoryOrList = categoryList;
					}
				}
				else
				{
					filter.CategoryOrList = categoryCode.TrimEnd('|').Split('|').ToList();
				}
			}
			#endregion

			#region ::: Set Sort Option
			SortCollection sc = new SortCollection();
			sc = GetSortCollection(sort);

			if (keyword != null && keyword.Length > 0 && sort == DisplayOrder.RankPointDesc)
			{
				string sortExpression = string.Empty;
				#region ::: 정확도 추가 처리
				// 키워드에 대한 형태소 분석
				// 추가-- 
				string extractorString = string.Empty;
				string notString = string.Empty;
				QADicFinder apFinder = new QADicFinder(VoyagerHelper.GetConnectionString("FilterFinder"));
				extractorString = apFinder.GetExtract(keyword);

				// Not 사전 검색
				QADicFinder notFinder = new QADicFinder(VoyagerHelper.GetConnectionString("FilterFinder"));
				notString = notFinder.GetNotKeyword(extractorString.TrimEnd());

				// Not 검색
				if (notString.Length > 0)
					filter.NotKeyword = Expression.Parse(notString);

				// 정확도점수용 호출
				QADicFinder APFinder = new QADicFinder(VoyagerHelper.GetConnectionString("FilterFinder"));
				string jijungAPString = APFinder.GetAPJijung(keyword.Replace(" ", ""));
				List<string> apJijungList = new List<string>();
				List<string> apJijungMinusList = new List<string>();
				List<string> categoryAPList = new List<string>();


				if (jijungAPString.Length > 0)
				{

					string[] valArray = jijungAPString.Split(':');
					int catLen = valArray.Length - 10;

					string catType = valArray[0].Trim();
					string catCode = string.Empty;

					// 카테고리 설정 내용이 있어야 하고 적용여부가 N 이 아닐때
					if (catLen > 0 && !catType.ToUpper().Equals("N"))
					{
						// 카테고리 Navi중 가장 마지막 Depth의 카테고리만 저장한다.
						for (int i = 0; i < catLen; i++)
						{
							catCode = valArray[10 + i].Trim();
							string[] catNavi = catCode.Split('>');
							categoryAPList.Add(catNavi[catNavi.Length - 1]);
						}
						// 적용된 카테고리 리스트를 정확도 점수에 반영한다. 
						if (catType.ToUpper().Equals("I"))
							apJijungList = categoryAPList;
						else
							apJijungMinusList = categoryAPList;
					}
				}

				int catePoint = 200000;
				int buyPoint = 100000;
				int jijungPoint = 200000;
				int limitPoint = 200000;
				int addSCatePoint = 100000;
				sortExpression = SortAPExpression.GetExpression2(new ItemFilter().ReplaceSpecialChar(keyword.ToLower().Trim()), extractorString.ToLower(), apJijungList, apJijungMinusList, catePoint, buyPoint, jijungPoint, limitPoint, addSCatePoint);

				#endregion

				//queryString = keyword + ":" + prevKeyword + ":" + filter.GetExpression() +":" + sortExpression;

				//RankPoint순으로 정렬, 상품번호로 2차 정렬
				if (extractorString.Trim().Length > 0 && extractorString.Split(' ').Length <= 5)
					sc.Add(Sort.Create(sortExpression, SortOrder.Decreasing));
			}
			#endregion

			#region :::: Set Groupby
			HistogramQueryCollection hqc = new HistogramQueryCollection();
			hqc.Add(new HistogramQuery(CategoryGroupField, HistogramSortOrder.Decrc));
			#endregion

			QueryResult queryResult = new SearchItemBiz().GetQueryResult(filter, (pageNo - 1) * rowCount, rowCount, sc, hqc);
			Histogram hs = (Histogram)queryResult.Histograms[CategoryGroupField];

			#region :::: 카테고리 그룹핑 결과 반환
			List<CategoryInfoT> categories = new List<CategoryInfoT>();
			CategoryInfoT category;
			if (hs != null)
			{
				foreach (HistogramEntry entry in hs)
				{
					category = CategoryBiz.GetCategoryInfo(entry.Value.ToString());

					if (category.Level != CategoryLevel.GroupCategory)
					{
						category.ItemCount = entry.Count;
						categories.Add(category);
					}
				}
			}
			#endregion

			result.Items = (SearchItemT[])queryResult.Entities;
			result.TotalCount = queryResult.Size;
			result.CategoryInfo = categories;

			return result;

		}
		#endregion
	}
}

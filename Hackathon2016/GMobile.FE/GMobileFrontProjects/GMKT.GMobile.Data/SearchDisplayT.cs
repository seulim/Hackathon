using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMobile.Data.Voyager;

namespace GMKT.GMobile.Data
{
	/// <summary>
	/// 검색 정렬 순서
	/// </summary>
	public enum DisplayOrder
	{
		FocusRankDesc = 0, //포커스랭크순
		RankPointDesc = 1, //G마켓랭크순
		SellPointDesc = 2, //판매인기순
		DiscountPriceDesc = 3, //할인액높은순
		SellPriceAsc = 4, //가격낮은순
		SellPriceDesc = 5, //가격높은순
		New = 6, //신규상품순
		ConPointDesc = 7 // 컨텐츠점수순
	}

	// 통합 검색 보이져 사용시
	public partial class SearchResultT
	{
		public SearchItemT[] Items { get; set; }
		public List<CategoryInfoT> CategoryInfo { get; set; }
		public List<CategoryInfoT> SubCategoryInfo { get; set; }
		public int TotalCount { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
	public class Best100GroupCateogyDetail
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public string IconOnURL { get; set; }
		public string IconOffURL { get; set; }
		public string IconPopupURL { get; set; }
		public string LinkURL { get; set; }
		public string ApiURL { get; set; }
		public string CssName { get; set; }

		public List<CategoryInfo> LargeCategoryList { get; set; }
	}

	public class Best100CateogyDetail
	{
		public int Code { get; set; }
		public string Name { get; set; }
		public List<CategoryInfo> LargeCategoryList { get; set; }
	}

	public class SearchResultModel
	{
		public string ListName { get; set; }
		public string lcId { get; set; }
		public string GroupName { get; set; }
		public string SubGroupName { get; set; }
		public List<SearchItemModel> Items { get; set; }
	}

	public class GmarketBestItemResponse
	{
		public List<SearchItemModel> Data { get; set; }
	}

	public class SearchItemModel
	{
		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }

		//public string Gdlc { get; set; }
		//public string Gdmc { get; set; }
		//public string Gdsc { get; set; }

		// 판매가
		public int SellPrice { get; set; }
		// 할인 적용된 가격
		public int DispPrice { get; set; }
		// 할인 가격
		public int DiscountPrice { get; set; }

		public string ImageUrl { get; set; }

		public int DeliveryFee { get; set; }
		public string DeliveryInfo { get; set; }
		public string DeliveryCode { get; set; }
		public string DeliveryText { get; set; }
		public string DeliveryType { get; set; }
		public bool ShowDeliveryInfo { get; set; }

		public bool IsAdult { get; set; }

		public int GStampCount { get; set; }

		public string LinkURL { get; set; }

		public bool IsTpl { get; set; }
	}

	#region Best100 Main
	public class Best100Main
	{
		public List<Best100GroupCateogyDetail> GroupCategoryList { get; set; }
		public SearchResultModel SearchModel { get; set; }
	}
	#endregion
}

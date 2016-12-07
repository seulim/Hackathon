using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GMKT.GMobile.Data
{
	#region TourGroup
	public class TourGroup
	{
		public long MiddleGroupNo { get; set; }
		public string MiddleGroupName { get; set; }
		public long SmallGroupNo { get; set; }
		public string SmallGroupName { get; set; }
		public string SmallGroupTypeName { get; set; }
		public string OnImageUrl { get; set; }
		public string OffImageUrl { get; set; }
		public int Priority { get; set; }
		public string UseYN { get; set; }
		public string ApiUrl { get; set; }
	}

	public class TourMGroup : TourGroup
	{
		public TourMGroup()
		{
			this.SGroupList = new List<TourGroup>();

			this.BannerName = string.Empty;
			this.BannerImageUrl = string.Empty;
			this.LandingUrl = string.Empty;
		}
		public List<TourGroup> SGroupList { get; set; }

		public string BannerName { get; set; }
		public string BannerImageUrl { get; set; }
		public string LandingUrl { get; set; }
	}

	public class TourSortOrder
	{
		public string SortOrderName { get; set; }
		public string SortOrderType { get; set; }
		public string ApiUrl { get; set; }
	}
	#endregion

	#region TourMain
	public class TourMain
	{
		public TourMain()
		{
			this.GroupList = new List<TourMGroup>();

			this.TopItemList = new List<TourItem>();
			this.ItemList = new List<TourItem>();

			this.BannerList = new List<TourBanner>();
			this.Paging = new TourPaging();

			this.SelectedMGroupBanner = new TourMiddleGroupBanner();
		}

		public List<TourMGroup> GroupList { get; set; }

		public List<TourItem> TopItemList { get; set; }
		public List<TourItem> ItemList { get; set; }

		public List<TourBanner> BannerList { get; set; }

		public TourPaging Paging { get; set; }

		public List<TourSortOrder> SortOrderList { get; set; }

		public string SelectedMGroupName { get; set; }
		public long SelectedMGroupNo { get; set; }
		public string SelectedSGroupName { get; set; }
		public long SelectedSGroupNo { get; set; }
		public string SelectedSortName { get; set; }
		public string SelectedSortOrderType { get; set; }

		public TourMiddleGroupBanner SelectedMGroupBanner { get; set; }

	}
	#endregion

	public class TourBanner
	{	
		public long Seq { get; set; }
		public string BannerType { get; set; }
		public int Priority { get; set; }
		public string EventName { get; set; }
		public string UseYN { get; set; }
		public DateTime DispStartDate { get; set; }
		public DateTime DispEndDate { get; set; }
		public string LandingUrl { get; set; }
		public string ImageUrl { get; set; }
	}

	public class TourItem
	{
		public TourItem()
		{
			this.Type = TourItemType.Large;
		}

		public string GoodsCode { get; set; }
		public string GoodsName { get; set; }

		public long MiddleGroupNo { get; set; }
		public long SmallGroupNo { get; set; }

		public string Title { get; set; }

		public string TopExposeYN { get; set; }
		public string ImageUrl { get; set; }

		public string Price { get; set; }
		public string OriginalPrice { get; set; }
		public int AdCouponPrice { get; set; }

		public long RankPoint2 { get; set; }

		public string LinkUrl { get; set; }
		public string DiscountRate { get; set; }

		public bool IsSoldOut { get; set; }
		public string DeliveryText { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
 		public TourItemType Type { get; set; }
	}

	public class TourMiddleGroupBanner
	{
		public TourMiddleGroupBanner()
		{
			this.HasBanner = false;

			this.Name = string.Empty;
			this.ImageUrl = string.Empty;
			this.LandingUrl = string.Empty;
		}

		public bool HasBanner { get; set; }

		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public string LandingUrl { get; set; }
	}

	public class TourPaging
	{
		public TourPaging()
		{
			this.FirstUrl = String.Empty;
			this.NextUrl = String.Empty;
		}

		public int TotalCount { get; set; }
		public int CurrentPageNo { get; set; }

		public string FirstUrl { get; set; }

		public bool HasNext { get; set; }
		public string NextUrl { get; set; }
	}

	public enum TourOrderEnum
	{
		None = 0, 
		RankPointDesc = 1, // 랭크 포인트 순
		SellPriceAsc = 2, // 가격 낮은 순
		New = 3 //신규 상품순
	}

	public enum TourItemType
	{
		None = 0,
		Large = 1,
		Small = 2
	}

}

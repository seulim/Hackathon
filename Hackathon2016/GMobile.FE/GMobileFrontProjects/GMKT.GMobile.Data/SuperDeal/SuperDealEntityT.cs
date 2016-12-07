using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMKT.GMobile.Data
{
	public class SuperDealCategoryInfo
	{
		public string Type { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string IconOnURL { get; set; }
		public string IconOffURL { get; set; }
		public string IconPopupURL { get; set; }
		public string LinkURL { get; set; }
		public string CssName { get; set; }
	}

    public class SuperDealItem
    {
		public char Type { get; set; }
		public string ItemType { get; set; }
        public string GoodsCode { get; set; }

        public string ItemTitle { get; set; }

		public string ItemSubTitle { get; set; }

        public string MainTitle1 { get; set; }

        public string MainTitle2 { get; set; }

        public string ImageURL { get; set; }

        public DateTime DispStartDate { get; set; }

        public DateTime DispEndDate { get; set; }

        public string DisplayYN { get; set; }

        public int Priority { get; set; }

        public string TagCloseYN { get; set; }

        public string TagLimitAmtYN { get; set; }

        public string TagNewYN { get; set; }

        public string TagEncoreYN { get; set; }

        public int BuyCount { get; set; }

        public int RemainCount { get; set; }

        public string OriginalPrice { get; set; }

        public int DiscountRate { get; set; }

        public string Price { get; set; }

        public string DiscountPrice { get; set; }

        public string DeliveryInfo { get; set; }

        public bool IsSoldOut { get; set; }

        public bool IsAdult { get; set; }

		public bool IsHomePlus { get; set; }

        public string LinkURL { get; set; }

		public bool ShowBuyCount { get; set; }

		public bool ShowRemainCount { get; set; }

		public int RankPoint { get; set; }

		public string UnitPrice { get; set; }

		public bool IsTpl { get; set; }

		public string ShopGdlcNm { get; set; }
		public string SubUrl { get; set; }
		public string ShopGdlcCd { get; set; }
		public string ShopGdmcCd { get; set; }

		public string ShopGroupCd { get; set; }
    }

	public class SuperDealCategoryV2 : CategoryInfoV2
	{
		public SuperDealCategoryV2()
		{
			ChildCategoryList = new List<CategoryInfoV2>();
		}

		public List<CategoryInfoV2> ChildCategoryList { get; set; }
		public string CssName { get; set; }
		public string IconOnURL { get; set; }
		public string IconOffURL { get; set; }
	}

	public class CategoryInfoV2 : CategoryInfo
	{
		public string Type { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string LinkURL { get; set; }
		public string ApiURL { get; set; }
		public int Rank { get; set; }
	}
	/// <summary>
	/// 카테고리
	/// </summary>
	public class SuperDealCategory
	{
		/// <summary>
		/// 샵카테고리 명
		/// </summary>
		public string CategoryName
		{
			get;
			set;
		}

		/// <summary>
		/// 카테고리No 샵카테고리는 삽카테고리 코드
		/// </summary>
		public int CategoryNo
		{
			get;
			set;
		}
		/// <summary>
		/// 2은 테마,1는 샵대분류
		/// </summary>
		public int CategoryType
		{
			get;
			set;
		}

		/// <summary>
		/// 우선순위
		/// </summary>
		public int CategoryPriority
		{
			get;
			set;
		}

		/// <summary>
		/// 모바일용아이콘
		/// </summary>
		public string IconMobile
		{
			get;
			set;
		}

		/// <summary>
		/// PC용 아이콘
		/// </summary>
		public string IconPC
		{
			get;
			set;
		}

	

		/// <summary>
		/// 하위카테고리
		/// </summary>
		public System.Collections.Generic.List<SuperDealSubCategory> SubCategory
		{
			get;
			set;
		}

	}
	/// <summary>
	/// 하위 카테고리
	/// </summary>
	public class SuperDealSubCategory
	{
		public string CategoryName
		{
			get;
			set;
		}

		public int CategoryNo
		{
			get;
			set;
		}
		/// <summary>
		/// 2은 테마,1는 샵대분류
		/// </summary>
		public int CategoryType
		{
			get;
			set;
		}
	}
}

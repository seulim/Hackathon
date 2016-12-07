using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	#region ShopCategory - ShopCategoryT, ShopCategoryInfoT, ShopCategoryProductT

	[TableName("SHOP_CATG")]
	public class ShopCategoryT
	{
		[Column("SHOP_CATEGORY_CODE")]
		public string CategoryCode { get; set; }

		//[Column("CUST_NO")]
		//public string SellerId { get; set; }

		[Column("SHOP_CATEGORY_NAME")]
		public string Name { get; set; }

		[Column("CATEGORY_LEVEL")]
		public CategoryLevel Level { get; set; }

		[Column("PARENT_CATEGORY_CODE")]
		public string ParentCategoryCode { get; set; }

		[Column("LEAF_YN")]
		public string LeafYN { get; set; }

		[Column("CATEGORY_SORT_ORDERBY")]
		public Nullable<int> SortOrder { get; set; }
	}

	[TableName("SHOP_CATG_INFO")]
	public class ShopCategoryInfoT
	{
		[Column("CATEGORY_INFO_SEQ")]
		public Int64 SeqNo { get; set; }

		//[Column("CUST_NO")]
		//public string SellerId { get; set; }

		[Column("CATEGORY_TYPE")]
		public string CategoryType { get; set; }

		[Column("GMKT_CATEGORY_CODE")]
		public string GeneralCategoryCode { get; set; }

		[Column("SHOP_CATEGORY_CODE")]
		public string ShopCategoryCode { get; set; }

		[Column("ICON_TYPE")]
		public IconType IconType { get; set; }
	}

	[TableName("SHOP_CATG_PROD")]
	public class ShopCategoryItemT
	{
		[Column("GD_NO")]
		public string No { get; set; }

		[Column("SHOP_CATEGORY_CODE")]
		public string ShopCategoryCode { get; set; }

		//[Column("CUST_NO")]
		//public string SellerId { get; set; }

		[Column("CATEGORY_LEVEL")]
		public CategoryLevel CategoryLevel { get; set; }
	}

	#endregion

	public enum IconType
	{
		None = 0,
		New = 1,
		Hot = 2
	}

	public enum CategoryType
	{
		General,
		Shop
	}

	// 샵 카테고리 레벨
	public enum ShopCategoryLevel
	{
		None = 0,
		LargeCategory = 1,
		MediumCategory = 2,
	}

	//1-기본카테고리만사용, 2-Shop카테고리만 사용, 3-기본카테고리 우선 사용, 4-Shop카테고리 우선 사용
	public enum CategoryDisplay
	{
		None = 0,
		GeneralCategoryOnly = 1,
		ShopCategoryOnly = 2,
		GeneralCategoryFirst = 3,
		ShopCategoryFirst = 4
	}

	//1이면 대+중+소분류, 2이면 대+중분류, 3이면 중+소분류, 4이면 대분류, 5이면 중분류, 6이면 
	public enum GeneralCategoryDisplayType
	{
		None = 0,
		LargeMediumSmallCategory = 1,
		LargeMediumCategory = 2,
		MediumSmallCategory = 3,
		LargeCategoryOnly = 4,
		MediumCategoryOnly = 5,
		SmallCategoryOnly = 6,
	}

	//1이면 대+중+소분류, 2이면 대+중분류, 3이면 중+소분류, 4이면 대분류, 5이면 중분류, 6이면 
	public enum ShopCategoryDisplayType
	{
		None = 0,
		LargeMediumSmallCategory = 1,
		LargeMediumCategory = 2,
		MediumSmallCategory = 3,
		LargeCategoryOnly = 4,
		MediumCategoryOnly = 5,
		SmallCategoryOnly = 6,
	}
}

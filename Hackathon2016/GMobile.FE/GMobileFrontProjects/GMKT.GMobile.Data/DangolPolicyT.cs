using System;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public class DangolPolicyT
	{
		public bool IsFavoriteShop { get; set; }

		[Column("DANGOL_YN")]
		public string IsDangolShopValue { get; set; }
		public bool IsDangolShop
		{
			get
			{
				if (!String.IsNullOrEmpty(this.IsDangolShopValue) &&
					this.IsDangolShopValue.Equals("Y"))
					return true;
				else
					return false;
			}
		}

		[Column("PURCHASE_SELLER_YN")]
		public string IsPurchaseSellerValue { get; set; }
		public bool IsPurchaseSeller
		{
			get
			{
				if (!String.IsNullOrEmpty(this.IsPurchaseSellerValue) &&
					this.IsPurchaseSellerValue.Equals("Y"))
					return true;
				else
					return false;
			}
		}

		//[Column("SELLER_CUST_NO")]
		//public string SellerId { get; set; }

		[Column("COST_RATE")]
		public decimal DiscountRate { get; set; }

		[Column("DANGOL_COST_TYPE")]
		public string DangolDiscountType { get; set; }

		[Column("DANGOL_COST_LIMIT_CNT")]
		public int? DangolDiscountBaseCount { get; set; }

		[Column("DANGOL_COST_LIMIT_MONEY")]
		public decimal DangolDiscountBaseAmount { get; set; }

		[Column("LOWEST_BUY_MONEY")]
		public decimal MinimumOrderAmount { get; set; }

		//[Column("BUYER_CUST_NO")]
		//public string BuyerId { get; set; }

		[Column("ACML_BUY_CNT")]
		public int? TotalOrderCount { get; set; }

		//[Column("ACML_BUY_MONEY")]
		//public decimal TotalOrderAmount { get; set; }

		[Column("USE_ACML_BUY_CNT")]
		public int? RecentTotalOrderCount { get; set; }

		[Column("USE_ACML_BUY_MONEY")]
		public decimal RecentTotalOrderAmount { get; set; }

		[Column("START_DATE")]
		public DateTime? StartDate { get; set; }

		[Column("END_DATE")]
		public DateTime? EndDate { get; set; }

		//[Column("USE_ACML_ISSUE_DATE")]
		//public Nullable<DateTime> UseAcmlIssueDate { get; set; }


	}
}

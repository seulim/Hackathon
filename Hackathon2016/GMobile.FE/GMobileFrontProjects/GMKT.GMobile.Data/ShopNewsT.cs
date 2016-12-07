using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PetaPoco;
using GMKT.Framework.Data;

namespace GMKT.GMobile.Data
{
	#region ShopNews
	public class ShopNewsT
	{
		public long Index { get; set; }

		[Column("NOTICE_SEQ")]
		public long No { get; set; }
		
		[Column("NOTICE_TYPE")]
		public ShopNewsType ShopNewsType { get; set; }

		//public string NewsTypeText
		//{
		//    get
		//    {
		//        return EnumHelperEx.GetDescription(this.NewsType);
		//    }
		//}

		[Column("NOTICE_TITLE")]
		public string Title { get; set; }

		[Column("NOTICE_CONTENT")]
		public string Content { get; set; }

		[Column("EXPOSE_YN")]
		public string IsDisplayValue { get; set; }
		public bool IsDisplay
		{
			get
			{
				if (!String.IsNullOrEmpty(this.IsDisplayValue) &&
					this.IsDisplayValue.Equals("Y"))
					return true;
				else
					return false;
			}
		}

		//[Column("EXPOSE_PERIOD_TYPE")]
		//public string DisplayPeriodType { get; set; }

		[Column("EXPOSE_START_DATE")]
		public DateTime DisplayStartDate { get; set; }

		[Column("EXPOSE_END_DATE")]
		public DateTime DisplayEndDate { get; set; }

		[Column("UPPER_FIX_EXPOSE_YN")]
		public string IsDisplayAlwaysValue { get; set; }
		public bool IsDisplayAlways
		{
			get
			{
				if (!String.IsNullOrEmpty(this.IsDisplayAlwaysValue) &&
					this.IsDisplayAlwaysValue.Equals("Y"))
					return true;
				else
					return false;
			}
		}

		[Column("READ_CNT")]
		public int Hit { get; set; }

		[Column("INS_DATE")]
		public DateTime PostDate { get; set; }

		public bool IsNew
		{
			get
			{
				return DateTime.Now < this.PostDate.AddDays(30) ? true : false;
			}
		}

		public string GetShopNewTypeText(ShopNewsType shopNewsType)
		{
			switch (shopNewsType)
			{
				case ShopNewsType.None:
					return "전체";
				case ShopNewsType.Delivery:
					return "배송관련";
				case ShopNewsType.Order:
					return "주문관련";
				case ShopNewsType.Item:
					return "상품관련";
				case ShopNewsType.Promotion:
					return "이벤트관련";
				case ShopNewsType.Etc:
					return "기타";
				default:
					return "전체";
			}
		}
	}
	#endregion

	public enum ShopNewsType
	{
		None = 0,
		Delivery = 1,
		Order = 2,
		Item = 3,
		Promotion = 4,
		Etc = 5
	}

	//public struct DisplayPeriodType
	//{
	//    public const string Unlimited = "U";	// 무제한
	//    public const string Limited = "L";		// 제한
	//}
}

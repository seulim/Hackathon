using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class SellerT
	{
		public string Id
		{
			get
			{
				return this.CustomerNo;
			}
			set
			{
				this.CustomerNo = value;
			}
		}

		[Column("CUST_NO")]
		public string CustomerNo { get; set; }

		[Column("LOGIN_ID")]
		public string UserName { get; set; }	// ID

		[Column("CUST_NM")]
		public string Name { get; set; }

		[Column("CCUST_CMPNM")]
		public string CompanyName { get; set; }

		[Column("TOP_MGR")]
		public string Manager { get; set; }

		[Column("HELPDESK_TEL_NO")]
		public string PhoneNumber { get; set; }

		[Column("FAX_NO")]
		public string FaxNo { get; set; }

		[Column("E_MAIL")]
		public string EmailAddress { get; set; }

		[Column("CORP_NO")]
		public string BusinessNo { get; set; }

		public string BusinessNoText { 
			get
			{
				if (this.CustomerType != "EC" && this.CustomerType != "EG")
				{
					this.BusinessNo = "";
				}				

				if (this.CustomerType == "EG")
				{
					if (!string.IsNullOrEmpty(this.OverseasBusinessNo))
						return this.OverseasBusinessNo;
					else
						return "";
				}
				else
				{
					return this.BusinessNo;
				}
			}
		}

		[Column("OVERSEA_CORP_NO")]
		public string OverseasBusinessNo { get; set; }

		[Column("ECOMERCE_NO")]
		public string EcommerceNo { get; set; }

		[Column("SELLERADDRESS")]
		public string StreetAddress { get; set; }

		[Column("HELPDESK_SDT")]
		public string HelpdeskStartHour { get; set; }

		[Column("HELPDESK_EDT")]
		public string HelpdeskEndHour { get; set; }

		[Column("CUST_TYPE")]
		public string CustomerType { get; set; } // @todo: enum으로 변경 예정

		[Column("CUST_GR")]
		public string SellerGradeValue { get; set; }
		public string SellerGrade
		{
			get
			{
				return GMKT.GMobile.Data.SellerGrade.ToString(this.SellerGradeValue);
			}
		}

		[Column("CS_GRADE")]
		public string DeliveryGradeValue { get; set; }
		public string DeliveryGrade
		{
			get
			{
				return GMKT.GMobile.Data.DeliveryGrade.ToString(this.DeliveryGradeValue);
			}
		}

		[Column("CS_GRADE_RESP")]
		public string CommunicationGradeValue { get; set; }
		public string CommunicationGrade
		{
			get
			{
				return GMKT.GMobile.Data.CommunicationGrade.ToString(this.CommunicationGradeValue);
			}
		}

		[Column("TOTAL_RCMD_POINT_RATE")]
		public int FeedbackRateValue { get; set; }
		public int FeedbackRate
		{
			get
			{
				if (this.FeedbackRateValue < 20)
					return 1;
				else if (this.FeedbackRateValue >= 20 && this.FeedbackRateValue < 40)
					return 2;
				else if (this.FeedbackRateValue >= 40 && this.FeedbackRateValue < 60)
					return 3;
				else if (this.FeedbackRateValue >= 60 && this.FeedbackRateValue < 80)
					return 4;
				else if (this.FeedbackRateValue >= 80)
					return 5;
				else
					return 1;
			}
		}

		[Column("ML_VALUE")]
		public decimal MileageToSave { get; set; }


		[Column("WD_CNSL_TIME_CONTENT")]
		public string WeekdayTime { get; set; }

		[Column("SAT_CNSL_TIME_CONTENT")]
		public string SaturdayTime { get; set; }

		[Column("HOLIDAY_CNSL_TIME_CONTENT")]
		public string HolidayTime { get; set; }

		[Column("STAT")]
		public string Stat { get; set; }

		//public static SellerT GetInstance(string sellerId)
		//{
		//    SellerBiz biz = new SellerBiz();
		//    SellerT seller = biz.GetSeller(sellerId);
		//    return seller;
		//}
	}

	public struct SellerGrade
	{
		public const string Power = "A1";	// 파워딜러
		public const string Good = "B1";	// 우수딜러
		public const string None = "";		// N/A

		public static string ToString(string grade)
		{
			switch (grade)
			{
				case Power:
					return "파워딜러";
				case Good:
					return "우수딜러";
				default:
					return "";
			}
		}
	}

	public struct DeliveryGrade
	{
		public const string Good = "AA";	// 배송우수
		public const string None = "";		// N/A

		public static string ToString(string grade)
		{
			switch (grade)
			{
				case Good:
					return "고객만족우수";
				default:
					return "";
			}
		}
	}

	public struct CommunicationGrade
	{
		public const string Good = "AA";	// 응대우수
		public const string None = "";		// N/A

		public static string ToString(string grade)
		{
			switch (grade)
			{
				case Good:
					return "응대우수";
				default:
					return "";
			}
		}
	}
}

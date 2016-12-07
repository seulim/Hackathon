using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheFx.EnterpriseServices;
using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Util;
using GMKT.Framework.Exceptions;

namespace GMKT.GMobile.Biz
{
	public class PluszoneBiz : EventzoneBiz
	{
		#region constants
		protected const string ROULETTE_ITEM_BANNER_TYPE_1 = "PR";
		protected const string COUPON_BANNER_TYPE_1 = "PG";

		protected const int NUMBER_OF_ROULETTE_ITEMS = 12;
		protected const int NUMBER_OF_COUPONS = 6;

		// 당첨율(%)
		protected const double PROBABILITY_I_SEQ_NUM_0 = 2;		// 스탬프 1장
		protected const double PROBABILITY_I_SEQ_NUM_1 = 3;		// 스탬프 1장
		protected const double PROBABILITY_I_SEQ_NUM_2 = 0.1;	// 스탬프 2장
		protected const double PROBABILITY_I_SEQ_NUM_3 = 0.1;	// 스탬프 3장
		protected const double PROBABILITY_I_SEQ_NUM_4 = 47;		// 10마일리지
		protected const double PROBABILITY_I_SEQ_NUM_5 = 45;		// 30마일리지
		protected const double PROBABILITY_I_SEQ_NUM_6 = 2.3;	// 50마일리지
		protected const double PROBABILITY_I_SEQ_NUM_7 = 0.1;	// 쇼핑지원금 1천원
		protected const double PROBABILITY_I_SEQ_NUM_8 = 0.1;	// 1천원 쿠폰
		protected const double PROBABILITY_I_SEQ_NUM_9 = 0.1;	// 2천원 쿠폰
		protected const double PROBABILITY_I_SEQ_NUM_10 = 0.1;	// 3천원 쿠폰
		protected const double PROBABILITY_I_SEQ_NUM_11 = 0.1;	// 무료배송쿠폰

		// double의 확률 값을 int로 만들기 위해 곱해주는 수(확률이 소수 첫째 자리까지 있다면 10을 곱하고 둘째 자리까지 있다면 100을 곱한다)
		protected const int CONVERSION_FACTOR = 10;
		protected const int I_SEQ_NUM_0_MAX = 0 + (int)(PROBABILITY_I_SEQ_NUM_0 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_1_MAX = I_SEQ_NUM_0_MAX + (int)(PROBABILITY_I_SEQ_NUM_1 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_2_MAX = I_SEQ_NUM_1_MAX + (int)(PROBABILITY_I_SEQ_NUM_2 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_3_MAX = I_SEQ_NUM_2_MAX + (int)(PROBABILITY_I_SEQ_NUM_3 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_4_MAX = I_SEQ_NUM_3_MAX + (int)(PROBABILITY_I_SEQ_NUM_4 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_5_MAX = I_SEQ_NUM_4_MAX + (int)(PROBABILITY_I_SEQ_NUM_5 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_6_MAX = I_SEQ_NUM_5_MAX + (int)(PROBABILITY_I_SEQ_NUM_6 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_7_MAX = I_SEQ_NUM_6_MAX + (int)(PROBABILITY_I_SEQ_NUM_7 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_8_MAX = I_SEQ_NUM_7_MAX + (int)(PROBABILITY_I_SEQ_NUM_8 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_9_MAX = I_SEQ_NUM_8_MAX + (int)(PROBABILITY_I_SEQ_NUM_9 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_10_MAX = I_SEQ_NUM_9_MAX + (int)(PROBABILITY_I_SEQ_NUM_10 * CONVERSION_FACTOR);
		protected const int I_SEQ_NUM_11_MAX = I_SEQ_NUM_10_MAX + (int)(PROBABILITY_I_SEQ_NUM_11 * CONVERSION_FACTOR);

		// 당첨율과 룰렛 각도 매핑
		protected const int ANGLE_BETWEEN_ITEMS = 360 / NUMBER_OF_ROULETTE_ITEMS;
		protected readonly int[] ANGLE_ITEM = new int[NUMBER_OF_ROULETTE_ITEMS]
		{
			ANGLE_BETWEEN_ITEMS * 7,
			ANGLE_BETWEEN_ITEMS * 10,
			ANGLE_BETWEEN_ITEMS * 1,
			ANGLE_BETWEEN_ITEMS * 4,
			ANGLE_BETWEEN_ITEMS * 0,
			ANGLE_BETWEEN_ITEMS * 3,
			ANGLE_BETWEEN_ITEMS * 6,
			ANGLE_BETWEEN_ITEMS * 5,
			ANGLE_BETWEEN_ITEMS * 2,
			ANGLE_BETWEEN_ITEMS * 11,
			ANGLE_BETWEEN_ITEMS * 9,
			ANGLE_BETWEEN_ITEMS * 8
		};

		#endregion

		/// <summary>
		/// 이 달의 출석체크 내역을 가져옵니다.
		/// </summary>
		/// <param name="custNo">custNo</param>
		/// <returns>출석체크 List</returns>
		[Transaction(TransactionOption.NotSupported)]
        public List<PluszoneNewAttendanceT> GetPluszoneAttendanceThisMonth(string custNo)
		{
			DateTime today = DateTime.Today;

			return new EventzoneDac().SelectPluszoneAttendance
			(
				custNo, 
				new DateTime(today.Year, today.Month, 1),
				new DateTime(today.Year, today.Month, today.Day, 23, 59, 59)
			);
		}

		/// <summary>
		/// 당첨률에 따라 룰렛의 당첨 결과를 반환합니다.
		/// </summary>
		/// <returns>당첨된 룰렛 item의 번호를 반환합니다.</returns>
		[Transaction(TransactionOption.NotSupported)]
		[Obsolete("Promotion팀의 이벤트 플랫폼을 사용하기로 변경되어 더 이상 사용하지 않는 Action입니다.", true)]
		protected int GetEidRandom()
		{
			int randomNumber = new Random().Next(I_SEQ_NUM_11_MAX);

			if (randomNumber < I_SEQ_NUM_0_MAX)
				return 0;
			else if (randomNumber < I_SEQ_NUM_1_MAX)
				return 1;
			else if (randomNumber < I_SEQ_NUM_2_MAX)
				return 2;
			else if (randomNumber < I_SEQ_NUM_3_MAX)
				return 3;
			else if (randomNumber < I_SEQ_NUM_4_MAX)
				return 4;
			else if (randomNumber < I_SEQ_NUM_5_MAX)
				return 5;
			else if (randomNumber < I_SEQ_NUM_6_MAX)
				return 6;
			else if (randomNumber < I_SEQ_NUM_7_MAX)
				return 7;
			else if (randomNumber < I_SEQ_NUM_8_MAX)
				return 8;
			else if (randomNumber < I_SEQ_NUM_9_MAX)
				return 9;
			else if (randomNumber < I_SEQ_NUM_10_MAX)
				return 10;
			else if (randomNumber < I_SEQ_NUM_11_MAX)
				return 11;
			else
				throw new PluszoneException("Pluszone 룰렛 당첨률에 문제가 있습니다.");
		}

		[Transaction(TransactionOption.NotSupported)]
        public List<PluszoneNewAttendanceT> SelectPluszoneAttance(string custNo, DateTime startDate, DateTime endDate)
		{
			return new EventzoneDac().SelectPluszoneAttendance(custNo, startDate, endDate);
		}

		[Transaction(TransactionOption.NotSupported)]
		public List<EventzoneBannerT> GetCouponList()
		{
			return new EventzoneDac().GetEventzoneBannerList(COUPON_BANNER_TYPE_1, GetYearMonthFromDateTime(DateTime.Now), DateTime.Now, NUMBER_OF_COUPONS);
		}

		[Transaction(TransactionOption.NotSupported)]
		[Obsolete("Promotion팀의 이벤트 플랫폼을 사용하기로 변경되어 더 이상 사용하지 않는 Action입니다.", true)]
		public List<EventzoneBannerT> GetRouletteItemList()
		{
			return new EventzoneDac().GetEventzoneBannerList(ROULETTE_ITEM_BANNER_TYPE_1, GetYearMonthFromDateTime(DateTime.Now), DateTime.Now, NUMBER_OF_ROULETTE_ITEMS);
		}

		[Transaction(TransactionOption.NotSupported)]
		[Obsolete("Promotion팀의 이벤트 플랫폼을 사용하기로 변경되어 더 이상 사용하지 않는 Action입니다.", true)]
		public int ConvertISeqNumToAngle(int itemNumber)
		{
			return ANGLE_ITEM[itemNumber];
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheFx.EnterpriseServices;

using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Biz
{
	public class EventzoneBiz : BizBase
	{
		/// <summary>
		/// DateTime으로부터 YYYYMM 형식의 string을 추출합니다.
		/// </summary>
		/// <param name="dateTime">날짜</param>
		/// <returns>YYYYMM 형식의 string</returns>
		[Transaction(TransactionOption.NotSupported)]
		public string GetYearMonthFromDateTime(DateTime dateTime)
		{
			if (dateTime.Month < 10)
			{
				return dateTime.Year.ToString() + "0" + dateTime.Month.ToString();
			}
			else
			{
				return dateTime.Year.ToString() + dateTime.Month.ToString();
			}
		}
	}
}

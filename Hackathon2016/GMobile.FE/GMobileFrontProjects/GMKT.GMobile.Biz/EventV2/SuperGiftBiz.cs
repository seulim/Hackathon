using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.GMobile.Data;
using GMKT.GMobile.Data.EventV2;
using GMKT.Framework.EnterpriseServices;

namespace GMKT.GMobile.Biz.EventV2
{
	public class SuperGiftBiz
	{
		private string CONST_isGdLc = "N";
		private string CONST_isPlusBuy = "Y";
		private string CONST_transportStat = "D4";

		public BuyHistoryT GetMonthlyBuyHistory()
		{
			DateTime now = DateTime.Now;
			ApiResponse<BuyHistoryT> response = new SuperGiftDac().GetMonthlyBuyHistory( 
					new DateTime( now.Year, now.Month, 1 ).ToShortDateString()
					, new DateTime( now.Year, now.Month, DateTime.DaysInMonth( now.Year, now.Month ), 12, 59, 59 ).ToShortDateString()
					, CONST_isGdLc
					, CONST_isPlusBuy
					, CONST_transportStat );
			if( response != null )
			{
				return response.Data;
			}
			else
			{
				return new BuyHistoryT();
			}
		}

		public BuyHistoryT GetMobileBuyHistory()
		{
			DateTime now = DateTime.Now;
			ApiResponse<BuyHistoryT> response = new SuperGiftDac().GetMobileBuyHistory(
					new DateTime( now.Year, now.Month, 1 ).ToShortDateString()
					, new DateTime( now.Year, now.Month, DateTime.DaysInMonth( now.Year, now.Month ), 12, 59, 59 ).ToShortDateString()
					, CONST_transportStat );
			if( response != null )
			{
				return response.Data;
			}
			else
			{
				return new BuyHistoryT();
			}
		}

		public SuperGiftJsonEntityT GetSuperGiftEntity()
		{
			ApiResponse<SuperGiftJsonEntityT> response = new SuperGiftDac().GetSuperGiftEntity();
			if( response != null )
			{
				return response.Data;
			}
			else
			{
				return new SuperGiftJsonEntityT();
			}
		}
	}
}

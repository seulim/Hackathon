using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.Member;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz.Member
{
	public class SmileCashApiBiz
	{
		public SmileUserInfoResultT GetCreateSmileMember(string custNo)
		{
			SmileUserInfoResultT result = new SmileUserInfoResultT();
			ApiResponse<SmileUserInfoResultT> response = new SmileCashApiDac().GetCreateSmileMember(custNo);

			if (response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}

			if (result == null) result = new SmileUserInfoResultT();
			
			return result;
		}

		public SmilePointBalanceT PostSmilePointBalance()
		{
			SmilePointBalanceT result = new SmilePointBalanceT();
			ApiResponse<SmilePointBalanceT> response = new SmileCashApiDac().PostSmilePointBalance();

			if (response != null && response.ResultCode == 0)
			{
				result = response.Data;
			}

			if (result == null) result = new SmilePointBalanceT();

			return result;
		}
	}
}

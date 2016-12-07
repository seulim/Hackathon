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
	public class GStampBiz : EventzoneBiz
	{
		#region constants
		protected const string GIFT_LIST_GTYPE = "GB";

		protected const int AMOUNT_OF_GET_EVENTZONE_GIFT_LIST = 50;
		#endregion

		[Transaction(TransactionOption.NotSupported)]
		public List<EventzoneGiftT> GetEventzoneGiftList()
		{
			return new EventzoneDac().GetEventzoneGiftList(GIFT_LIST_GTYPE, DateTime.Now, AMOUNT_OF_GET_EVENTZONE_GIFT_LIST);
		}

		[Transaction(TransactionOption.NotSupported)]
		public GStampIssueT GetStampIssueCount(string custNo)
		{
			return new EventzoneDac().GetStampIssueCount(custNo);
		}

		[Transaction(TransactionOption.NotSupported)]
		public EventStickerT GetEventStickerMinusCount(int eid)
		{
			return new EventzoneDac().GetEventStickerMinusCount(eid);
		}

		[Transaction(TransactionOption.NotSupported)]
		public EventWinnerT GetEventRemainCount(int eid, DateTime today)
		{
			return new EventzoneDac().GetEventRemainCount(eid, today);
		}

		
	}
}

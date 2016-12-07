using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class EventzoneGiftT
	{
		[Column("ez_gid")]
		public Int32 EzGid { get; set; }

		[Column("gtitle")]
		public string Title { get; set; }

		[Column("gdesc")]
		public string Description { get; set; }

		[Column("display_st")]
		public DateTime DisplayStartTime { get; set; }

		[Column("display_et")]
		public DateTime DisplayEndTime { get; set; }

		[Column("apply_eid")]
		public Int32 ApplyEid { get; set; }

		[Column("change_eid")]
		public double ChangeEid { get; set; }

		[Column("stype")]
		public byte Type { get; set; }

		[Column("mobile_banner_url")]
		public string MobileImageUrl { get; set; }

		[Column("support_amt")]
		public Int32 SupportAmount { get; set; }

		[Column("entry_gd_no")]
		public string EntryGdNo { get; set; }

		[Column("entry_gstamp_cnt")]
		public Int32 EntryGStampCount { get; set; }

		[Column("exchange_gd_no")]
		public string exchangeGdNo { get; set; }

		[Column("exchange_gstamp_cnt")]
		public Int32 exchangeGStampCount { get; set; }

	}
}

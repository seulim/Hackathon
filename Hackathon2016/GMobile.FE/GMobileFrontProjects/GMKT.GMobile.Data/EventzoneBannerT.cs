using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
	public partial class EventzoneBannerT
	{
		[Column("seq")]
		public Int32 Seq { get; set; }

		[Column("banner_seq")]
		public Int32 BannerSeq { get; set; }

		[Column("banner_type_1")]
		public string BannerType1 { get; set; }

		[Column("banner_type_2")]
		public string BannerType2 { get; set; }

		[Column("banner_title")]
		public string BannerTitle { get; set; }

		[Column("disp_start_dt")]
		public DateTime DispStartDateTime { get; set; }

		[Column("disp_end_dt")]
		public DateTime DispEndDateTime { get; set; }

		[Column("banner_sub_type")]
		public string BannerSubType { get; set; }

		[Column("priority")]
		public Byte Priority { get; set; }

		[Column("use_yn")]
		public string UseYN { get; set; }

		[Column("banner_text")]
		public string BannerText { get; set; }

		[Column("banner_desc")]
		public string BannerDesc { get; set; }

		[Column("banner_url")]
		public string BannerUrl { get; set; }

		[Column("mobile_banner_url")]
		public string MobileBannerUrl { get; set; }

		[Column("lnk_url")]
		public string LinkUrl { get; set; }

		[Column("sid")]
		public Int32 Sid { get; set; }

		[Column("eid")]
		public Int32 Eid { get; set; }

		[Column("gd_no")]
		public string GdNo { get; set; }
	}
}

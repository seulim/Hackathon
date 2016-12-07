using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaPoco;

namespace GMKT.GMobile.Data
{
    public partial class PartnerT
    {
        [Column("SEQ")]
        public Nullable<Int64> Seq { get; set; }

        [Column("PP_SELLER_NO")]
        public string PpSellerNo { get; set; }

        [Column("PP_NM")]
        public string PpNm { get; set; }

        [Column("PP_MOBILE_IMAGE_URL")]
        public string PpMobileImageUrl { get; set; }

		public bool IsForSale { get; set; }
		public string ImageUrl { get; set; }
		public string PartnerNm { get; set; }
		public string PppOffImageUrl { get; set; }
		public string PppOnImageUrl { get; set; }
		public string SellerNo { get; set; }
		public string PRMTBannerCd { get; set; }
		public string PRMTBannerImg { get; set; }
		public string PRMTBannerUrl { get; set; }
		public string PRMTText { get; set; }
		public string PRMTPdsLogJson { get; set; }
		public int PpItemCount { get; set; }
		public bool PRMTUseIs { get; set; }
		public string PdsLogJson { get; set; }
    }
}

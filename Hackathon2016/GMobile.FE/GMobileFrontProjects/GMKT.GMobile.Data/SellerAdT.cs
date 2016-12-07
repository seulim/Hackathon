using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using System.Data;
using PetaPoco;

namespace GMKT.GMobile.Data
{
    public class SellerAdT
    {
        [Column("bid_no")]
        public string BidNo { get; set; }

        [Column("succ_bid_no")]
        public string SuccbidNo { get; set; }

        [Column("bidgdno")]
        public string GdNo { get; set; }

		[Column("keyword")]
		public string Keyword { get; set; }

		[Column("gldc_cd")]
		public string Gldc { get; set; }

		[Column("gdmc_cd")]
		public string Gdmc { get; set; }

		[Column("gdsc_cd")]
		public string Gdsc { get; set; }
    }
}

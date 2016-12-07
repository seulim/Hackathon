using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using System.Data;
using System.Collections;

namespace GMKT.GMobile.Data
{
    public class SellerAdDac : MicroDacBase
    {
        /// <summary>
        /// LP/SRP - 광고상품 가져오기
        /// </summary>
		public Hashtable SelectSellerAd(string date)
        {
			Hashtable hash = new Hashtable();

			try
			{
				// SEARCH
				hash.Add("48", MicroDacHelper.SelectMultipleEntities<SellerAdT>(
					"addb_read"
					, "dbo.up_gmkt_front_sellerAD_get_Mobile_display_data"
					, MicroDacHelper.CreateParameter("@mno", "48", SqlDbType.Int)
					, MicroDacHelper.CreateParameter("@BidCellData", "", SqlDbType.VarChar)
					//, MicroDacHelper.CreateParameter("@search_date", date, SqlDbType.VarChar )
				));

				// 중분류
				hash.Add("49", MicroDacHelper.SelectMultipleEntities<SellerAdT>(
					"addb_read"
					, "dbo.up_gmkt_front_sellerAD_get_Mobile_display_data"
					, MicroDacHelper.CreateParameter("@mno", "49", SqlDbType.Int)
					, MicroDacHelper.CreateParameter("@BidCellData", "", SqlDbType.VarChar)
					//, MicroDacHelper.CreateParameter("@search_date", date , SqlDbType.VarChar)
				));

				// 소분류
				hash.Add("50", MicroDacHelper.SelectMultipleEntities<SellerAdT>(
					"addb_read"
					, "dbo.up_gmkt_front_sellerAD_get_Mobile_display_data"
					, MicroDacHelper.CreateParameter("@mno", "50", SqlDbType.Int)
					, MicroDacHelper.CreateParameter("@BidCellData", "", SqlDbType.VarChar)
					//, MicroDacHelper.CreateParameter("@search_date", date, SqlDbType.VarChar )
				));
			}
			catch( Exception ex )
			{
				hash.Add("48", new List<SellerAdT>());
				hash.Add("49", new List<SellerAdT>());
				hash.Add("50", new List<SellerAdT>());
			}

			return hash;
        }
    }
}

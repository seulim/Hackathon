using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using ArcheFx.EnterpriseServices;
using GMKT.GMobile.Data;
using System.Collections;
using System.Web;
using GMKT.GMobile.Constant;
using System.Web.Caching;

namespace GMKT.GMobile.Biz
{
    public class SellerAdBiz : BizBase
    {
        [Transaction(TransactionOption.NotSupported)]
		public List<SellerAdT> GetSellerAdListFromDB(string mno, string mcId, string scId, string keyword)
        {
			Hashtable hash = null;

			if ( mno == null || "".Equals( mno ) )
				return new List<SellerAdT>();

			if (HttpRuntime.Cache[Const.CACHE_LPSRP_PLUS_ITEM_LIST] == null)
			{
				hash = new SellerAdDac().SelectSellerAd("");
				// 30분마다 데이터 갱신
				HttpRuntime.Cache.Insert(Const.CACHE_LPSRP_PLUS_ITEM_LIST, hash, null, DateTime.Now.AddMinutes(30),
								Cache.NoSlidingExpiration);
			}
			else
			{
				hash = (Hashtable) HttpRuntime.Cache[Const.CACHE_LPSRP_PLUS_ITEM_LIST];
			}

			if ( hash == null ) new List<SellerAdT>();

			List<SellerAdT> items = (List<SellerAdT>) hash[mno];

			IEnumerable<SellerAdT> list = items;

			if ("48".Equals(mno) && !"".Equals(keyword))
			{
				list = items.Where(c => c.Keyword.ToLower().Trim().Equals(keyword.ToLower().Trim()));

				if ( !"".Equals( mcId ) )
					list = list.Where(c => c.Gdmc == mcId);
				if (!"".Equals(scId))
					list = list.Where(c => c.Gdsc == scId);
			}
			else if ("49".Equals(mno))
				list = list.Where(c => c.Gdmc == mcId);
			else if ("50".Equals(mno))
				list = list.Where(c => c.Gdsc == scId);

			if ( list != null )
				list = list.OrderBy(c => c.SuccbidNo);

			items = new List<SellerAdT>( list );

            return items;
        }
    }
}

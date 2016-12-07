using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using GMKT.Framework.EnterpriseServices;
using GMKT.Framework.Data;
using GMKT.Framework.Cache;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class ShopNewsDac : MicroDacBase
	{
		[GMKTCache(GMKTCacheType.LocalCache, "ShopNewsNaviag", 1)]
		public List<ShopNewsT> SelectNavigationNewsListCache(string sellerId)
		{
			return MicroDacHelper.SelectMultipleEntitiesFromCache<ShopNewsT>(
				"minishop_read",
				"dbo.UP_GMKTNet_News_SelectNavigationNewsList",
				MicroDacHelper.CreateParameter("@cust_no", sellerId, SqlDbType.VarChar, 10));
		}

		public List<ShopNewsT> SelectList(string sellerId, ShopNewsType? newsType, string keyword, int pageNo, int pageSize)
		{
			return MicroDacHelper.SelectMultipleEntities<ShopNewsT>(
				"minishop_read",
				"dbo.UP_GMKTNet_News_SelectNewsList",
				MicroDacHelper.CreateParameter("@cust_no", sellerId, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@notice_type", newsType, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@keyword", keyword, SqlDbType.VarChar, 50),
				MicroDacHelper.CreateParameter("@page_no", pageNo, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@page_size", pageSize, SqlDbType.Int));
		}

		public int CountTotal(string sellerId, ShopNewsType? newsType, string keyword)
		{
			return MicroDacHelper.SelectScalar<int>(
				"minishop_read",
				"dbo.UP_GMKTNet_News_SelectNewsCount",
				MicroDacHelper.CreateParameter("@cust_no", sellerId, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@notice_type", newsType, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@keyword", keyword, SqlDbType.VarChar, 50));
		}

		public ShopNewsT SelectDetails(long newsNo, string sellerId)
		{
			return MicroDacHelper.SelectSingleEntity<ShopNewsT>(
				"minishop_read",
				"dbo.UP_GMKTNet_News_SelectNewsDetail",
				MicroDacHelper.CreateParameter("@notice_seq", newsNo, SqlDbType.BigInt),
				MicroDacHelper.CreateParameter("@cust_no", sellerId, SqlDbType.VarChar, 10));
		}

		public long SelectNextPostNo(long newsNo, string sellerId, ShopNewsType? newsType, string keyword)
		{
			return MicroDacHelper.SelectScalar<long>(
				"minishop_read",
				"dbo.UP_GMKTNet_News_SelectNewsNextNoticeSeq",
				MicroDacHelper.CreateParameter("@notice_seq", newsNo, SqlDbType.BigInt),
				MicroDacHelper.CreateParameter("@cust_no", sellerId, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@notice_type", newsType, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@keyword", keyword, SqlDbType.VarChar, 50));
		}

		public long SelectPreviousPostNo(long newsNo, string sellerId, ShopNewsType? newsType, string keyword)
		{
			return MicroDacHelper.SelectScalar<long>(
				"minishop_read",
				"dbo.UP_GMKTNet_News_SelectNewsPrevNoticeSeq",
				MicroDacHelper.CreateParameter("@notice_seq", newsNo, SqlDbType.BigInt),
				MicroDacHelper.CreateParameter("@cust_no", sellerId, SqlDbType.VarChar, 10),
				MicroDacHelper.CreateParameter("@notice_type", newsType, SqlDbType.Int),
				MicroDacHelper.CreateParameter("@keyword", keyword, SqlDbType.VarChar, 50));
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class ShopNewsBiz : BizBase
	{
		public List<ShopNewsT> GetNavigationNewsList(string sellerId)
		{
			return new ShopNewsDac().SelectNavigationNewsListCache(sellerId);
		}

		public List<ShopNewsT> GetList(string sellerId, ShopNewsType? newsType, string keyword, int pageNo, int pageSize)
		{
			return new ShopNewsDac().SelectList(sellerId, newsType, keyword, pageNo, pageSize);
		}

		public int CountTotal(string sellerId, ShopNewsType? newsType, string keyword)
		{
			return new ShopNewsDac().CountTotal(sellerId, newsType, keyword);
		}

		public ShopNewsT GetDetails(long newsNo, string sellerId)
		{
			return new ShopNewsDac().SelectDetails(newsNo, sellerId);
		}

		public long GetNextPostNo(long newsNo, string sellerId, ShopNewsType? newsType, string keyword)
		{
			return new ShopNewsDac().SelectNextPostNo(newsNo, sellerId, newsType, keyword);
		}

		public long GetPreviousPostNo(long newsNo, string sellerId, ShopNewsType? newsType, string keyword)
		{
			return new ShopNewsDac().SelectPreviousPostNo(newsNo, sellerId, newsType, keyword);
		}
	}
}

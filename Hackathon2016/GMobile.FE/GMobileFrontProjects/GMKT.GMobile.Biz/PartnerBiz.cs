using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using ArcheFx.EnterpriseServices;
using GMKT.GMobile.Data;
using Arche.Data.Voyager;
using GMobile.Data.Voyager;
using GMobile.Service.Search;
using GMKT.GMobile.Constant;
using System.Web;
using System.Web.Caching;

namespace GMKT.GMobile.Biz
{
	[Transaction(TransactionOption.NotSupported)]
    public class PartnerBiz : BizBase
    {
		public List<PartnerT> GetPartnerListFromDB(string pp_seller_no)
        {
			List<PartnerT> list = null;
			if (HttpRuntime.Cache[Const.CACHE_LPSRP_PARTNER_LIST] == null)
			{
				// 데이터를 최초로 가져올 경우 
				list = new PartnerDac().SelectPartner(pp_seller_no);
				// 10분마다 데이터 갱신
				HttpRuntime.Cache.Insert(Const.CACHE_LPSRP_PARTNER_LIST, list, null, DateTime.Now.AddMinutes(10),
								Cache.NoSlidingExpiration);
			}

			list = (List<PartnerT>)HttpRuntime.Cache[Const.CACHE_LPSRP_PARTNER_LIST];

			if (HttpRuntime.Cache[Const.CACHE_LPSRP_PARTNER_LIST] == null)
			{
				return new List<PartnerT>();
			}

			List<PartnerT> result = new List<PartnerT>(list);

			return result;
        }

		public List<PartnerT> GetPremiumPartnerList(string lcId, string mcId, string scId, string keyword)
		{
			ItemFilter filter = new ItemFilter();

			if (!"".Equals(scId))
				filter.Category = scId;
			else if (!"".Equals(mcId))
				filter.Category = mcId;

			if (!"".Equals(keyword))
				filter.ItemName = keyword;

			List<PartnerT> list = GetPartnerListFromDB("");

			foreach (PartnerT partner in list)
			{
				filter.SellCustNoOrList.Add(partner.PpSellerNo);
			}

			//RankPoint순으로 정렬
			SortCollection sc = new SortCollection();
			sc.Add(Sort.Create("RankPoint", SortOrder.Decreasing));

			GroupByQueryCollection gqc = new GroupByQueryCollection();
			GroupByQuery query = new GroupByQuery(AggregateSortOrder.Decr, 0, "SellCustNoByName");

			query.AddAggregator(new AggregateSUM("RankPoint"));
			query.AddAggregator(new AggregateCount());

			gqc.Add(query);

			// 검색결과를 가져온다.
			QueryResult result = new SearchItemBiz().GetQueryResult(filter, 0, 100, sc, null, gqc);

			List<PartnerT> tempList = new List<PartnerT>();

			if (result.GroupBy != null && result.GroupBy.Count > 0)
			{
				GroupByResult gbResult = result.GroupBy[0];
				for (int i = 0; i < gbResult.Count; i++)
				{
					GroupByResultEntry entry = gbResult[i];
					int count = (int)entry.Count;
					if (count > 20)
					{
						// 판매자의 상품이 20개 이상인 것만 대상.

						string seller_cust_no = (string)entry.FieldValues[0];

						if ( seller_cust_no == null || "".Equals( seller_cust_no ) ) continue;

						// 어드민에 있는 판매자리스트만 추린다.
						for ( int j = 0; j < list.Count; j++ )
						{
							if ( seller_cust_no.Equals( list[j].PpSellerNo ) )
								tempList.Add(list[j]);
						}
					}
				}
			}

			return tempList;
		}
    }
}

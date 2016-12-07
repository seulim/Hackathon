using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.Voyager;
using Arche.Data.Voyager;
using GMobile.Data.DisplayDB;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.Search;

namespace GMKT.GMobile.Web.Models
{
	public class SearchModel
	{
		public string Keyword  { get; set;}
		public int PageNo  { get; set;}
		public int PageSize { get; set; }
		public int StartPrice { get; set; }
		public int EndPrice { get; set; }
		public string PrevKeyword { get; set; } 
		public string LCode { get; set; }
		public string MCode { get; set; }
		public string SCode { get; set; }
		public string SortType { get; set; }
		public string IsFeeFreeYN { get; set; }
		public string IsMileageYN { get; set; }
		public string IsDiscountYN { get; set; }
		public string IsStampYN { get; set; }
		public string ListType { get; set; }
        public string SellCustNo { get; set; }
		public int CategoryLevel { get; set; }

		public SearchItemT[] Items  {get; set;}
		public Histogram CategoryGroups  {get; set;}
		public int TotalCount { get; set; }

		// 2013-04-04 이윤호
		// 모바일 구매 로그 작업
		public long KeywordSeq { get; set; }

		public MobileSrpT SrpList { get; set; }
		public string SrpYN { get; set; }
		public string SrpUrl { get; set; }
		public string SrpText { get; set; }
		public string SrpDesc { get; set; }

        public string[] ADItmes { get; set; }

        // 프리미엄 파트너 리스트
        public List<PartnerT> PartnerList { get; set; }
	}

	public class TireSmartFinderM
	{
		public string Keyword { get; set; }
		public List<SmartFinderMClass> MClassList { get; set; }
	}
}
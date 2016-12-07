using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.Diver;
using GMobile.Data.Voyager;
using Arche.Data.Voyager;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
	public class CategorySearchModel
	{
		public string Keyword { get; set; }
		public int PageNo { get; set; }
		public int PageSize { get; set; }
		public int StartPrice { get; set; }
		public int EndPrice { get; set; }
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

		public CategoryItemT[] Items { get; set; }
		public Histogram CategoryGroups { get; set; }
		public int TotalCount { get; set; }
		public List<SpecialShopping> Plans { get; set; }
		public SortedDictionary<string, HistogramEntry> sortedCategoryGroups {get; set;}

        public string[] ADItmes { get; set; }

        // 프리미엄 파트너 리스트
        public List<PartnerT> PartnerList { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMobile.Data.Voyager;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Web.Models
{
    public class ShopTestModel
    {
		public List<CategoryInfoT> Categoryinfo{ get; set; }
		public SearchItemT[] Items { get; set; }
		public SearchResultT SearchResult { get; set; }
    }
}

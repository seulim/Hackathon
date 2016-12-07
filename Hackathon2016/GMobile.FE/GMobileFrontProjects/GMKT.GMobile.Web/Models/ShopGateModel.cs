using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GMobile.Data.Voyager;

namespace GMKT.GMobile.Web.Models
{	
	public class BestItemModel
	{		
		public string Name { get; set; }
		public string Price { get; set; }
		public string PriceAppendString { get; set; }
		public string ImageUrl { get; set; }
		public string LandingUrl { get; set; }
        public string GoodsCode { get; set; }
	}
}
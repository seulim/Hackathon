using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GMKT.GMobile.Web.Models
{
	public class HomePlusCartListModel : Nova.Thrift.CartListDetailI
	{
		public string ItemLinkUrl { get; set; }
		public string ItemImageUrl { get; set; }
		public double dblPrice { get; set; }
		public string strPrice { get; set; }
		public double dblShippingCost { get; set; }
		public string strShippingCost { get; set; }
	}
}
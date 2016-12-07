using System;
using System.Collections.Generic;

namespace GMKT.GMobile.Web.Models
{
	[Serializable]
	public class GetGoodsInfosM
	{
		public bool Success { get; set; }
		public string Message { get; set; }

		public List<GoodsInfoM> GoodsInfoList { get; set; }
	}

	public class GoodsInfoM
	{
		public string GoodsNo { get; set; }
		public string GoodsName { get; set; }
		public decimal OriginalPrice { get; set; }
		public decimal? DcPrice { get; set; }
		public string ImageUrl { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GMKT.GMobile.Data
{
	public class RegInterestItemsInfo
	{
		public int SuccessCount { get; set; }
		public int FailCount { get; set; }
	}

	public enum GoodsHeaderType
	{
		None
		, Shop
		, Banner
		, Text
	}

	public class DynamicHeader
	{
		[JsonConverter(typeof(StringEnumConverter))]
		public GoodsHeaderType HeaderType { get; set; }

		public string Text { get; set; }
		public string ImageUrl { get; set; }
		public string LandingUrl { get; set; }
	}

	public class PDSLogging
	{
		public PDSLogging()
		{
		}
		public string AreaType { get; set; }
		public string AreaCode { get; set; }
		public object Parameter = new object();
	}
}

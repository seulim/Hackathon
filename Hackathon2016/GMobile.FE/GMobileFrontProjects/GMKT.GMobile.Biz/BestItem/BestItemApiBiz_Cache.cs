using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.MobileCache;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class BestItemApiBiz_Cache : CacheContextObject
	{
		#region Best100 UI개편
		[CacheDuration(DurationSeconds = 60)]
		public Best100Main GetBest100Main(string code = "")
		{
			Best100Main result = new Best100Main();
			result = new BestItemApiBiz().GetBest100Main(code);

			if (result == null)
			{
				result.GroupCategoryList = new List<Best100GroupCateogyDetail>();
				result.SearchModel = new SearchResultModel();
				result.SearchModel.Items = new List<SearchItemModel>();
			}

			return result;
		}
		#endregion
	}
}

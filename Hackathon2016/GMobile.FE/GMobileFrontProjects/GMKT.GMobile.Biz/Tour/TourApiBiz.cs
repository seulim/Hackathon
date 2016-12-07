using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class TourApiBiz
	{
		public TourMain GetTourItem(long middleGroupNo = 0, long smallGroupNo = 0, int pageNo = 1, int pageSize = 80, TourOrderEnum order = TourOrderEnum.RankPointDesc)
		{
			ApiResponse<TourMain> response = new TourApiDac().GetTourItem(middleGroupNo, smallGroupNo, pageNo, pageSize, order);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new TourMain();
			}			
		}
	}
}

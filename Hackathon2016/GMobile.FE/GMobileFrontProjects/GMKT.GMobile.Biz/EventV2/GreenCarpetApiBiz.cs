using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.EventV2;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz.EventV2
{
	public class GreenCarpetApiBiz
	{
		public GreenCarpetT GetGreenCarpetInfo()
		{
			GreenCarpetT result = new GreenCarpetT();
			ApiResponse<GreenCarpetT> response = new GreenCarpetApiDac().GetGreenCarpetInfo();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new GreenCarpetT();

			return result;
		}
	}
}

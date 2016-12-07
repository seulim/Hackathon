using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.GMobile.Data.G9Gate;

namespace GMKT.GMobile.Biz.G9Gate
{
	public class G9GateApiBiz
	{
		public G9GateBannerT GetG9GateBanner()
		{
			G9GateBannerT result = new G9GateBannerT();

			ApiResponse<G9GateBannerT> response = new G9GateApiDac().GetG9GateBanner();

			if(response != null && response.Data != null)
			{
				result = response.Data;
			}

			return result;
		}
	}
}

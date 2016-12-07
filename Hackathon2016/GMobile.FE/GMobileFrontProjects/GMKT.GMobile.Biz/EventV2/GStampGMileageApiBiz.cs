using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data.EventV2;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz.EventV2
{
	public class GStampGMileageApiBiz
	{
		public GStampDataT GetGStampInfo()
		{
			GStampDataT result = new GStampDataT();
			ApiResponse<GStampDataT> response = new GStampGMileageApiDac().GetGStampInfo();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new GStampDataT();

			return result;
		}

		public GMileageDataT GetGMileageInfo()
		{
			GMileageDataT result = new GMileageDataT();
			ApiResponse<GMileageDataT> response = new GStampGMileageApiDac().GetGMileageInfo();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new GMileageDataT();

			return result;
		}
	}
}

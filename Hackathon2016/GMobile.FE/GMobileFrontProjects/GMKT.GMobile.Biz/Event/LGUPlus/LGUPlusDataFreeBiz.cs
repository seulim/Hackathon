using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Biz.Util;

namespace GMKT.GMobile.Biz
{
	public class LGUPlusDataFreeBiz
	{
		private string[] LGUPlusAddressRange = 
		{
			"117.111.1.0-117.111.28.255",
			"211.36.130.0-211.36.159.255"
		};

		public IPChecker GetUplusIpAddressRange()
		{
			IPChecker checker = new IPChecker(LGUPlusAddressRange.ToList());
			return checker;
		}

		public bool IsUPlusIpAddress(string ip)
		{
			bool result = false;

			if (String.IsNullOrEmpty(ip))
				return result;

			try
			{
				IPChecker checker = new LGUPlusDataFreeBiz_Cache().GetUplusIpAddressRange();

				if (checker != null)
				{
					result = checker.IsInRange(ip);
				}
			}
			catch {}

			return result;
		}
	}
}

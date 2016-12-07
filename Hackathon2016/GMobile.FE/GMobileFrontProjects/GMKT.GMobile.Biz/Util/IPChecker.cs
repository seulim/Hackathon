using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools;

namespace GMKT.GMobile.Biz.Util
{
	public class IPChecker
	{
		public IPChecker(List<string> ipAddressRange)
		{
			this.AddressRange = ipAddressRange;
		}
		// Pattern 1. CIDR range: "192.168.0.0/24", "fe80::/10"
		// Pattern 2. Uni address: "127.0.0.1", ":;1"
		// Pattern 3. Begin end range: "169.258.0.0-169.258.0.255"
		// Pattern 4. Bit mask range: "192.168.0.0/255.255.255.0"
		private List<string> AddressRange { get; set; }

		public bool IsInRange(string ipAddress)
		{
			bool contains = false;

			if (!String.IsNullOrEmpty(ipAddress) && AddressRange != null && AddressRange.Count > 0)
			{
				foreach (string addRange in AddressRange)
				{
					if (!String.IsNullOrEmpty(addRange))
					{
						IPAddressRange range = IPAddressRange.Parse(addRange);
						if (range != null)
						{
							contains = range.Contains(IPAddressRange.Parse(ipAddress));
							if (contains)
							{
								break;
							}
						}
					}
				}
			}

			return contains;
		}
	}
}

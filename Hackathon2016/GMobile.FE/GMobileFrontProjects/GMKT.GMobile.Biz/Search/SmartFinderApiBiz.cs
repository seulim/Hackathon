using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz.Search
{
	public class SmartFinderApiBiz
	{
		public List<SmartFinderMClass> GetSmartFinderMClassList(string lClassSeq)
		{
			var response = new SmartFinderApiDac().GetSmartFinderMClassList(lClassSeq);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new List<SmartFinderMClass>();
			}
		}

		public List<SmartFinderSClass> GetSmartFinderSClassList(string mClassSeq)
		{
			var response = new SmartFinderApiDac().GetSmartFinderSClassList(mClassSeq);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new List<SmartFinderSClass>();
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArcheFx.EnterpriseServices;
using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Biz
{
	[Transaction(TransactionOption.NotSupported)]
	public class JaehuIdBiz : BizBase
	{
		public static readonly string DEFAULT_MOBILE_JAEHUID = "200003514";

		public static readonly string NAVER_MOBILE_JAEHUID = "200006220";

		public static readonly Dictionary<string, string> PC_MOBILE_JAEHUID_DICTIONARY = new Dictionary<string, string>()
		{
			{"200001169", NAVER_MOBILE_JAEHUID},
			{"200001170", NAVER_MOBILE_JAEHUID},
			{"200001171", NAVER_MOBILE_JAEHUID},
			{"200002617", NAVER_MOBILE_JAEHUID},
			{"200002788", NAVER_MOBILE_JAEHUID},
			{"200003206", NAVER_MOBILE_JAEHUID},
			{"200004269", NAVER_MOBILE_JAEHUID},
		};

		public List<JaehuT> GetIncJaehuStringValueFromDB()
		{
			return new JaehuIdDac().SelectJaehuList();
		} 
	}
}

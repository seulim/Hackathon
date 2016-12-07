using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using NUnit.Framework;
using GMKT.GMobile.Biz;
using GMobile.Data.Voyager;
using GMKT.GMobile.Data;
using GMKT.MobileCache;
using GMKT.Framework.EnterpriseServices;
using PetaPoco;
using System.Data;

namespace GMobile.Service.Tests
{
    [TestFixture]
    public class CacheTest
    {
        [TestCase("aaa")]
        public void GetCacheItem(string keyword)
        {
            List<string> itemnos = new List<string>();
            itemnos.Add("390922445");

			SearchItemT[] result = null;
			if (CacheHelper.IsExist("GetCacheItem", "390922445"))
			{
				result = (SearchItemT[])CacheHelper.GetCacheItem("GetCacheItem", "390922445");
				Trace.WriteLine("From Cache!!!");
				if (result != null)
				{
					Trace.DumpBusinessEntity(result);
				}
				else
				{
					Trace.WriteLine("From Cache && result is null");
				}
			}
			else
			{
				result = new SearchBiz().GetItems(itemnos, DisplayOrder.RankPointDesc);
				CacheHelper.SetCacheItem("GetCacheItem", "390922445", result, 30);
				Trace.WriteLine("No Exist Cache!!!");

				if (result != null)
				{
					Trace.DumpBusinessEntity(result);
				}
				else
				{
					Trace.WriteLine("result is null");
				}
			}
        }
    }

    [TestFixture]
    public class NewBizCacheTest
    {
		[TestCase("lcode")]
		[TestCase("mcode")]
		[TestCase("scode")]
		public void GetCacheTest(string type)
		{
			List<CategoryT> result = new NewCacheTestBiz().GetCacheCategory(type);

			Trace.DumpBusinessEntity(result);
		}
	}

	public class NewCacheTestBiz : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 30)]
		public List<CategoryT> GetCacheCategory(string type)
		{
			return new TolhmTestBiz().GetCategoryTest(type);
		}
	}

	public class TolhmTestBiz : BizBase
	{
		public List<CategoryT> GetCategoryTest(string type)
		{
			List<CategoryT> result = new List<CategoryT>();

			result = new TolhmTestDac().GetCategoryTest(type);

			return result;
		}

	}

	public class TolhmTestDac : MicroDacBase
	{
		public List<CategoryT> GetCategoryTest(string type)
		{
			return MicroDacHelper.SelectMultipleEntities<CategoryT>(
				"item_read"
				, "dbo.up_test_tolhm"
				, MicroDacHelper.CreateParameter("@type", type, SqlDbType.VarChar, 10)
			);
		}
	}

	public partial class CategoryT
	{
		[Column("CCODE")]
		public string CCode { get; set; }

		[Column("NAME")]
		public string Name { get; set; }
	}
  
}

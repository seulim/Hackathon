using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;

using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using System.Web;
using System.IO;

namespace GMobile.Service.Tests
{
	[TestFixture]
	public class GMobileECouponTests
	{
		public HttpContext httpContext { get; set; }

		[Test]
		public void GetMobileECouponEvent()
		{
			List<ECouponEvent> list = new ECouponBiz().GetMobileECouponEvent(100);

			if (list == null)
			{
				Trace.WriteLine("정보가 없습니다");
				return;
			}

			foreach (ECouponEvent banner in list)
			{
				Trace.DumpBusinessEntity(banner);
			}

		}

		[Test]
		public void GetMobileECouponCategory()
		{
			List<ECouponCategory> list = new ECouponBiz().GetMobileECouponCategory(100);

			if (list == null)
			{
				Trace.WriteLine("정보가 없습니다");
				return;
			}

			foreach (ECouponCategory data in list)
			{
				Trace.DumpBusinessEntity(data);
			}

		}

		[TestCase("100000001")]
		[TestCase("100000002")]
		[TestCase("100000003")]
		[TestCase("100000004")]
		[TestCase("100000005")]
		[TestCase("100000006")]
		public void GetMobileECouponBrandByCategory(string category_cd)
		{
			List<ECouponBrand> list = new ECouponBiz().GetMobileECouponBrandByCategory(category_cd, 100);

			if (list == null)
			{
				Trace.WriteLine("정보가 없습니다");
				return;
			}

			foreach (ECouponBrand data in list)
			{
				Trace.DumpBusinessEntity(data);
			}

		}

		[TestCase(1, 100)]
		[TestCase(2, 100)]
		[TestCase(3, 100)]
		[TestCase(4, 100)]
		[TestCase(5, 100)]
		[TestCase(6, 100)]
		[TestCase(23, 6)]
		public void GetMobileECouponBrandMenu(int brand_cd, int max_count)
		{
			List<ECouponBrandMenu> list = new ECouponBiz().GetMobileECouponBrandMenu(brand_cd, max_count);

			if (list == null)
			{
				Trace.WriteLine("정보가 없습니다");
				return;
			}

			foreach (ECouponBrandMenu data in list)
			{
				Trace.DumpBusinessEntity(data);
			}

		}

		[TestCase(23, 2, 6)]
		[TestCase(23, 3, 6)]
		public void GetMobileECouponBrandMenuPaging(int brand_cd, int page_index, int page_size)
		{
			List<ECouponBrandMenu> list = new ECouponBiz().GetMobileECouponBrandMenuPaging(brand_cd, page_index, page_size);

			if (list == null)
			{
				Trace.WriteLine("정보가 없습니다");
				return;
			}

			foreach (ECouponBrandMenu data in list)
			{
				Trace.DumpBusinessEntity(data);
			}

		}

		[TestCase(1, 100)]
		[TestCase(2, 100)]
		[TestCase(3, 100)]
		[TestCase(4, 100)]
		[TestCase(5, 100)]
		[TestCase(6, 100)]
		[TestCase(23, 6)]
		public void GetMobileECouponBrandMenuWithItem(int brand_cd, int max_count)
		{
			List<ECouponMenuItem> list = new ECouponBiz().GetMobileECouponBrandMenuWithItem(brand_cd, max_count);

			if (list == null)
			{
				Trace.WriteLine("정보가 없습니다");
				return;
			}

			foreach (ECouponMenuItem data in list)
			{
				Trace.DumpBusinessEntity(data);
			}

		}

		[TestCase(23, 2, 6)]
		[TestCase(23, 3, 6)]
		public void GetMobileECouponBrandMenuWithItemPaging(int brand_cd, int page_index, int page_size)
		{
			List<ECouponMenuItem> list = new ECouponBiz().GetMobileECouponBrandMenuWithItemPaging(brand_cd, page_index, page_size);

			if (list == null)
			{
				Trace.WriteLine("정보가 없습니다");
				return;
			}

			foreach (ECouponMenuItem data in list)
			{
				Trace.DumpBusinessEntity(data);
			}

		}


		[TestCase(15)]
		[TestCase(16)]
		[TestCase(17)]
		[TestCase(18)]
		[TestCase(19)]
		[TestCase(20)]
		public void GetMobileECouponItemTopOne(int menu_seq)
		{
			ECouponItemTop1 itemtop1 = new ECouponBiz().GetMobileECouponItemTopOne(menu_seq, 100);

			if (itemtop1 == null)
			{
				Trace.WriteLine("정보가 없습니다");
				return;
			}

			Trace.DumpBusinessEntity(itemtop1);
		}
		
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class MartApiBiz
	{
		public MartView GetMartView(string categoryCode)
		{
			MartView result = new MartView();

			ApiResponse<MartView> response = new MartApiDac().GetMartView(categoryCode);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null)
			{
				result = new MartView();

				result.Items = new List<MartItem>();				
			}

			if (result.CategoryList == null)
			{
				result.CategoryList = new List<MartCategory>();
			}

			if (result.Items == null)
			{
				result.Items = new List<MartItem>();
			}

			return result;
		}

		public MartV2View GetMartV2View(string categoryCode, string goodsCode)
		{
			MartV2View result = new MartV2View();

			ApiResponse<MartV2View> response = string.IsNullOrEmpty(goodsCode) ? new MartApiDac().GetMartV2View(categoryCode) : new MartApiDac().GetMartV2View(categoryCode, goodsCode);
			if(response != null)
			{
				result = response.Data;
			}

			if(result == null)
			{
				result = new MartV2View()
				{
					TimeDeal = new MartTimeDealT(),
					CategoryList = new List<MartV2Category>(),
					ItemGroupList = new List<MartV2ItemGroupT>()
				};
			}

			if(result.TimeDeal == null)
			{
				result.TimeDeal = new MartTimeDealT();
			}

			if(result.CategoryList == null)
			{
				result.CategoryList = new List<MartV2Category>();
			}

			if(result.ItemGroupList == null)
			{
				result.ItemGroupList = new List<MartV2ItemGroupT>();
			}

			return result;
		}

		public MartV3View GetMartV3View(string categoryCode, string goodsCode, long categorySeq)
		{
			MartV3View result = new MartV3View();

			ApiResponse<MartV3View> response = new MartApiDac().GetMartV3View(categoryCode, goodsCode, categorySeq);

			if (response != null)
				result = response.Data;

			if (result == null)
			{
				result = new MartV3View()
				{
					IsMain = true,
					TimeDeal = new MartTimeDealT(),
					CategoryList = new List<MartV3Category>(),
					ItemGroupList = new List<MartV2ItemGroupT>(),
					BrandList = new List<MartBrand>()
				};
			}

			if (result.TimeDeal == null)
				result.TimeDeal = new MartTimeDealT();

			if (result.CategoryList == null)
				result.CategoryList = new List<MartV3Category>();

			if (result.ItemGroupList == null)
				result.ItemGroupList = new List<MartV2ItemGroupT>();

			if (result.BrandList == null)
				result.BrandList = new List<MartBrand>();
			else
				//result.BrandList = result.BrandList.Select(n => GetMartBrandLandingUrl(n)).ToList();
				result.BrandList = result.BrandList.ToList();

			if (result.ContentsList == null)
				result.ContentsList = new List<MartContents>();

			return result;
		}

		public MartContentsView GetMartContentsView(long id = 0)
		{
			MartContentsView result = new MartContentsView();

			ApiResponse<MartContentsView> response = new MartApiDac().GetMartContentsView(id);

			if (response != null)
				result = response.Data;

			return result;
		}

		private MartBrand GetMartBrandLandingUrl(MartBrand martBrand)
		{
			if (martBrand.LnkType == "S")
				martBrand.LandingUrl = String.Format("{0}/SellerShop/{1}", GMKT.GMobile.Util.Urls.MobileWebUrl, martBrand.SellerId);

			return martBrand;
		}

		public MartTimeDealT GetTimeDeal()
		{
			ApiResponse<MartTimeDealT> response = new MartApiDac().GetTimeDeal();
			MartTimeDealT result = response.Data;
			
			return result == null ? new MartTimeDealT() : result;
		}

		public List<MartV2ItemGroupT> GetMartV2HomePreview(long groupNo)
		{
			var response = new MartApiDac().GetMartV2HomePreview(groupNo);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new List<MartV2ItemGroupT>();
			}
		}

		public MartV3View GetMartV3HomePreview(long groupNo)
		{
			var response = new MartApiDac().GetMartV3HomePreview(groupNo);
			if (response != null && response.ResultCode == 0 && response.Data != null)
				return response.Data;
			else
				return new MartV3View();
		}
	}
}

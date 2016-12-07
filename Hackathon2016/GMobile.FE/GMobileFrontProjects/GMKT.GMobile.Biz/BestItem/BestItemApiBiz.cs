using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.Framework.EnterpriseServices;


namespace GMKT.GMobile.Biz
{
	public class BestItemApiBiz
	{
		#region 베스트100 그룹카테고리 정보 가져오기
		public List<Best100GroupCateogyDetail> GetBest100GroupCategoryInfo()
		{
			List<Best100GroupCateogyDetail> result = new List<Best100GroupCateogyDetail>();

			ApiResponse<List<Best100GroupCateogyDetail>> response = new BestItemApiDac().GetBest100GroupCategoryInfo();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<Best100GroupCateogyDetail>();

			return result;
		}
		#endregion

		#region BEST100 카테고리 정보
		public List<Best100CateogyDetail> GetBest100CategoryList()
		{
			List<Best100CateogyDetail> result = new List<Best100CateogyDetail>();

			ApiResponse<List<Best100CateogyDetail>> response = new BestItemApiDac().GetBest100CategoryList();
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<Best100CateogyDetail>();

			return result;
		}
		#endregion

		#region BEST100 전체 상품 정보 가져오기
		public List<SearchItemModel> GetBest100Items(int pageNo, int pageSize, bool forMobile = true)
		{
			List<SearchItemModel> result = new List<SearchItemModel>();

			ApiResponse<List<SearchItemModel>> response = new BestItemApiDac().GetBest100Items(pageNo, pageSize, forMobile);
			if (response != null)
			{
				result = response.Data;
			}

			if (result == null) result = new List<SearchItemModel>();

			return result;
		}
		#endregion

		#region 베스트100 그룹카테고리 상품정보 가져오기
		public SearchResultModel GetBest100GroupItems(string groupCode, int pageNo, int pageSize)
		{
			SearchResultModel result = new SearchResultModel();

			ApiResponse<SearchResultModel> response = new BestItemApiDac().GetBest100GroupItems(groupCode, pageNo, pageSize);
			if (response != null)
			{
				result = response.Data;
				if (result == null)
				{
					result = new SearchResultModel();
					result.Items = new List<SearchItemModel>();
				}
			}

			return result;
		}
		#endregion

		#region 베스트100 카테고리 상품정보 가져오기
		public SearchResultModel GetBest100CategoryItems(string code, int pageNo, int pageSize)
		{
			SearchResultModel result = new SearchResultModel();

			ApiResponse<SearchResultModel> response = new BestItemApiDac().GetBest100CategoryItems(code, pageNo, pageSize);
			if (response != null)
			{
				result = response.Data;
				if (result == null)
				{
					result = new SearchResultModel();
					result.Items = new List<SearchItemModel>();
				}
			}

			return result;
		}

		public List<SearchItemModel> GetGmarketBest100Items(string lcId, int pageNo, int pageSize)
		{
			SearchResultModel result = new SearchResultModel();

			GmarketBestItemResponse response = new BestItemApiDac().GetGmarketBest100Items(lcId, pageNo, pageSize);
			if (response != null)
			{
				return response.Data;
			}

			return new List<SearchItemModel>();
		}
		#endregion

		#region BEST100 UI개편
		public Best100Main GetBest100Main(string code = "")
		{
			Best100Main result = new Best100Main();

			ApiResponse<Best100Main> response = new BestItemApiDac().GetBest100Main(code);
			if (response != null && response.Data != null)
			{
				result = response.Data;
			}

			if (result == null || response.Data == null)
			{
				result = new Best100Main();
				result.GroupCategoryList = new List<Best100GroupCateogyDetail>();
				result.SearchModel = new SearchResultModel();
			}

			return result;
		}
		#endregion

	}
}

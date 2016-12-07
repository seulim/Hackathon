using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data
{
	public class MartApiDac : ApiBase
	{
		public MartApiDac()
			: base("GMApi")
		{
		}

		public ApiResponse<MartView> GetMartView(string categoryCode)
		{
			ApiResponse<MartView> result = ApiHelper.CallAPI<ApiResponse<MartView>>(
				"GET",
				ApiHelper.MakeUrl("api/mart/GetMartView"),
				new
				{
					categoryCode = categoryCode
				}
			);
			return result;
		}

		public ApiResponse<MartV2View> GetMartV2View(string categoryCode, string goodsCode)
		{	
			ApiResponse<MartV2View> result = ApiHelper.CallAPI<ApiResponse<MartV2View>>(
				"GET",
				ApiHelper.MakeUrl("api/mart/GetMartV2View"),
				new 
				{
					categoryCode = categoryCode,
					goodsCode = goodsCode
				}
			);			
			return result;
		}

		public ApiResponse<MartV2View> GetMartV2View(string categoryCode)
		{	
			ApiResponse<MartV2View> result = ApiHelper.CallAPI<ApiResponse<MartV2View>>(
				"GET",
				ApiHelper.MakeUrl("api/mart/GetMartV2View"),
				new 
				{
					categoryCode = categoryCode
				}
			);			
			return result;
		}

		public ApiResponse<MartV3View> GetMartV3View(string categoryCode, string goodsCode, long categorySeq)
		{
			object apiParams;

			if (string.IsNullOrEmpty(goodsCode))
				apiParams = new { categoryCode = categoryCode, categorySeq = categorySeq };
			else
				apiParams = new { categoryCode = categoryCode, goodsCode = goodsCode, categorySeq = categorySeq };

			ApiResponse<MartV3View> result = ApiHelper.CallAPI<ApiResponse<MartV3View>>(
				"GET",
				ApiHelper.MakeUrl("api/mart/GetMartV3View"),
				apiParams
			);
			return result;
		}

		public ApiResponse<MartContentsView> GetMartContentsView(long id = 0)
		{
			ApiResponse<MartContentsView> result = ApiHelper.CallAPI<ApiResponse<MartContentsView>>(
				"GET",
				ApiHelper.MakeUrl("api/mart/GetMartContentsView"),
				new { id = id }
			);
			return result;
		}

		public ApiResponse<MartTimeDealT> GetTimeDeal()
		{
			ApiResponse<MartTimeDealT> result = ApiHelper.CallAPI<ApiResponse<MartTimeDealT>>(
				"GET",
				ApiHelper.MakeUrl("api/mart/GetTimeDeal")
			);
			return result;
		}

		public ApiResponse<List<MartV2ItemGroupT>> GetMartV2HomePreview(long groupNo)
		{
			ApiResponse<List<MartV2ItemGroupT>> result = ApiHelper.CallAPI<ApiResponse<List<MartV2ItemGroupT>>>(
				"GET",
				ApiHelper.MakeUrl("api/mart/GetMartV2HomePreview"),
				new
				{
					groupNo = groupNo
				}
			);
			return result;
		}

		public ApiResponse<MartV3View> GetMartV3HomePreview(long groupNo)
		{
			ApiResponse<MartV3View> result = ApiHelper.CallAPI<ApiResponse<MartV3View>>(
				"GET",
				ApiHelper.MakeUrl("api/mart/GetMartV3HomePreview"),
				new
				{
					groupNo = groupNo
				}
			);
			return result;
		}
	}
}

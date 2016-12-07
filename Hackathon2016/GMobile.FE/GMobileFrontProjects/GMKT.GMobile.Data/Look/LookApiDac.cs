using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data
{
	public class LookApiDac : ApiBase
	{
		public LookApiDac()	: base("GMApi")	{ }

		public ApiResponse<LookInfo> GetLookInfo()
		{
			ApiResponse<LookInfo> result = ApiHelper.CallAPI<ApiResponse<LookInfo>>(
				"GET",
				ApiHelper.MakeUrl("api/Look/GetLookInfo")
			);

			return result;
		}

		public ApiResponse<LookV2Main> GetLookV2Main()
		{
			ApiResponse<LookV2Main> result = ApiHelper.CallAPI<ApiResponse<LookV2Main>>(
				"GET",
				ApiHelper.MakeUrl("api/LookV2/GetLookV2Main")
			);

			return result;
		}

		public ApiResponse<LookV2BrandGalleryMain> GetLookV2BrandGalleryMain(LookV2PageType pageType)
		{
			ApiResponse<LookV2BrandGalleryMain> result = ApiHelper.CallAPI<ApiResponse<LookV2BrandGalleryMain>>(
				"GET",
				ApiHelper.MakeUrl("api/LookV2/GetLookV2BrandGallery"),
				new
				{
					pageType = pageType
				}
			);
			return result;
		}

		public ApiResponse<List<LookV2Category>> GetLookV2Category(LookV2CategoryType categoryType)
		{
			ApiResponse<List<LookV2Category>> result = ApiHelper.CallAPI<ApiResponse<List<LookV2Category>>>(
				"GET",
				ApiHelper.MakeUrl("api/LookV2/GetLookV2Category"),
				new
				{
					categoryType = categoryType	
				}
			);

			return result;
		}

		public ApiResponse<LookV2Best> GetLookV2BrandBest(LookV2PageType pageType, LookV2CategoryType categoryType, string lCategoryCode, int brandNo, int pageNo, int pageSize)
		{
			ApiResponse<LookV2Best> result = ApiHelper.CallAPI<ApiResponse<LookV2Best>>(
				"GET",
				ApiHelper.MakeUrl("api/LookV2/GetLookV2Best"),
				new
				{
					pageType = pageType,
					categoryType = categoryType,
					lCategoryCode = lCategoryCode,
					brandNo = brandNo,
					pageNo = pageNo,
					pageSize = pageSize
				}
			);
			return result;
		}

		public ApiResponse<LookV2BrandLp> GetBrandDetail(LookV2PageType pageType, LookV2CategoryType categoryType, long brandGallerySeq)
		{
			ApiResponse<LookV2BrandLp> result = ApiHelper.CallAPI<ApiResponse<LookV2BrandLp>>(
				"GET",
				ApiHelper.MakeUrl("api/LookV2/GetLookV2BrandLp"),
				new
				{
					pageType = pageType,
					categoryType = categoryType,
					brandGallerySeq = brandGallerySeq
				}
			);

			return result;
		}

		#region LookV2 Cast
		public ApiResponse<LookV2Cast> GetLookV2Cast(LookV2PageType pageType, int pageNo, int pageSize)
		{
			ApiResponse<LookV2Cast> result = ApiHelper.CallAPI<ApiResponse<LookV2Cast>>(
				"GET",
				ApiHelper.MakeUrl("api/LookV2/GetLookV2Cast"),
				new
				{
					pageType = pageType,
					pageNo = pageNo,
					pageSize = pageSize
				}
			);

			return result;
		}

		public ApiResponse<LookV2CastDetail> GetLookV2CastDetail(long castContentsSeq)
		{
			ApiResponse<LookV2CastDetail> result = ApiHelper.CallAPI<ApiResponse<LookV2CastDetail>>(
				"GET",
				ApiHelper.MakeUrl("api/LookV2/GetLookV2CastDetail"),
				new
				{
					castContentsSeq = castContentsSeq
				}
			);

			return result;
		}
		#endregion

		public ApiResponse<List<LookContents>> GetLookContentsList()
		{
			ApiResponse<List<LookContents>> result = ApiHelper.CallAPI<ApiResponse<List<LookContents>>>(
				"GET",
				ApiHelper.MakeUrl("api/Look/GetLookContentsList")
			);

			return result;
		}

		public ApiResponse<LookContentsDetail> GetLookContentsDetail(long contentsSeq)
		{
			ApiResponse<LookContentsDetail> result = ApiHelper.CallAPI<ApiResponse<LookContentsDetail>>(
				"GET",
				ApiHelper.MakeUrl("api/Look/GetLookContentsDetail"),
				new
				{
					seq = contentsSeq
				}
			);

			return result;
		}

		public ApiResponse<LookSectionItem> GetLookSectionItem(long groupNo, int pageNo, int pageSize, string gdLcCd)
		{
			object param;

			if (false == string.IsNullOrEmpty(gdLcCd))
			{
				param = new
				{
					groupNo = groupNo,
					pageNo = pageNo,
					pageSize = pageSize,
					gdLcCd = gdLcCd
				};
			}
			else
			{
				param = new
				{
					groupNo = groupNo,
					pageNo = pageNo,
					pageSize = pageSize
				};
			}
			
			ApiResponse<LookSectionItem> result = ApiHelper.CallAPI<ApiResponse<LookSectionItem>>(
				"GET",
				ApiHelper.MakeUrl("api/Look/GetLookSectionItem"),
				param
			);

			return result;
		}
	}
}

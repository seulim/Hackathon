using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnApi.Client;

namespace GMKT.GMobile.Data
{
	public class SpaceApiDac : ApiBase
	{
		public SpaceApiDac() : base("GMApi") { }

		public ApiResponse<SpaceInfo> GetSpaceInfo()
		{
			ApiResponse<SpaceInfo> result = ApiHelper.CallAPI<ApiResponse<SpaceInfo>>(
				"GET",
				ApiHelper.MakeUrl("api/Space/GetSpaceInfo")
			);

			return result;
		}

		public ApiResponse<List<SpaceContents>> GetSpaceContentsList()
		{
			ApiResponse<List<SpaceContents>> result = ApiHelper.CallAPI<ApiResponse<List<SpaceContents>>>(
				"GET",
				ApiHelper.MakeUrl("api/Space/GetSpaceContentsList")
			);

			return result;
		}

		public ApiResponse<SpaceContentsDetail> GetSpaceContentsDetail(long contentsSeq)
		{
			ApiResponse<SpaceContentsDetail> result = ApiHelper.CallAPI<ApiResponse<SpaceContentsDetail>>(
				"GET",
				ApiHelper.MakeUrl("api/Space/GetSpaceContentsDetail"),
				new
				{
					seq = contentsSeq
				}
			);

			return result;
		}

		public ApiResponse<SpaceSectionItem> GetSpaceSectionItem(long lgroupNo, long mgroupNo, int pageNo, int pageSize)
		{
			object param = new
			{
				lgroupNo = lgroupNo,
				mgroupNo = mgroupNo,
				pageNo = pageNo,
				pageSize = pageSize
			};

			ApiResponse<SpaceSectionItem> result = ApiHelper.CallAPI<ApiResponse<SpaceSectionItem>>(
				"GET",
				ApiHelper.MakeUrl("api/Space/GetSpaceSectionItem"),
				param
			);

			return result;
		}

		public ApiResponse<SpaceBrandSectionItem> GetSpaceBrandSectionItem(long lgroupNo, long mgroupNo)
		{
			object param = new
			{
				lgroupNo = lgroupNo,
				mgroupNo = mgroupNo
			};

			ApiResponse<SpaceBrandSectionItem> result = ApiHelper.CallAPI<ApiResponse<SpaceBrandSectionItem>>(
				"GET",
				ApiHelper.MakeUrl("api/Space/GetSpaceBrandSectionItem"),
				param
			);

			return result;
		}

		public ApiResponse<SpaceBrandGroupDetail> GetSpaceBrandDetail(long seq)
		{
			object param = new
			{
				seq = seq
			};

			ApiResponse<SpaceBrandGroupDetail> result = ApiHelper.CallAPI<ApiResponse<SpaceBrandGroupDetail>>(
				"GET",
				ApiHelper.MakeUrl("api/Space/GetSpaceBrandDetail"),
				param
			);

			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
	public class SpaceApiBiz
	{
		public SpaceInfo GetSpaceInfo()
		{
			var response = new SpaceApiDac().GetSpaceInfo();
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new SpaceInfo()
				{
					ContentsSection = new SpaceContentsSection()
					{
						ContentsList = new List<SpaceContents>()
					},
					SpaceSection = new SpaceSection()
					{
						GroupList = new List<SpaceGroup>()
					}
				};
			}
		}

		public List<SpaceContents> GetSpaceContentsList()
		{
			var response = new SpaceApiDac().GetSpaceContentsList();
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new List<SpaceContents>();
			}
		}

		public SpaceContentsDetail GetSpaceContentsDetail(long contentsSeq)
		{
			var response = new SpaceApiDac().GetSpaceContentsDetail(contentsSeq);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else if (response.ResultCode == 1301)
			{
				return new SpaceContentsDetail()
				{
					Html = "1301",
					Items = new List<SpaceContentsItem>()
				};
			}
			else
			{
				return new SpaceContentsDetail()
				{
					Items = new List<SpaceContentsItem>()
				};
			}
		}

		public SpaceSectionItem GetSpaceSectionItem(long lgroupNo, long mgroupNo, int pageNo, int pageSize)
		{
			var response = new SpaceApiDac().GetSpaceSectionItem(lgroupNo, mgroupNo, pageNo, pageSize);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new SpaceSectionItem()
				{
					ItemGroupList = new List<SpaceItemGroup>(),
					Paging = new SpacePaging()
				};
			}
		}

		public SpaceBrandSectionItem GetSpaceBrandSectionItem(long lgroupNo, long mgroupNo)
		{
			var response = new SpaceApiDac().GetSpaceBrandSectionItem(lgroupNo, mgroupNo);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new SpaceBrandSectionItem()
				{
					BrandGroupList = new List<SpaceBrandGroup>()
				};
			}
		}

		public SpaceBrandGroupDetail GetSpaceBrandDetail(long seq)
		{
			var response = new SpaceApiDac().GetSpaceBrandDetail(seq);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else if (response.ResultCode == 1306)
			{
				return new SpaceBrandGroupDetail()
				{
					Html = "1306",
					Items = new List<SpaceBrandGroupItem>()
				};
			}
			else
			{
				return new SpaceBrandGroupDetail()
				{
					Items = new List<SpaceBrandGroupItem>()
				};
			}
		}
	}
}

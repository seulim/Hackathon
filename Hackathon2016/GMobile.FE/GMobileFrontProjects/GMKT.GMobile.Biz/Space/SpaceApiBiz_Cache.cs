using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.MobileCache;

namespace GMKT.GMobile.Biz
{
	public class SpaceApiBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 60)]
		public SpaceInfo GetSpaceInfo()
		{
			return new SpaceApiBiz().GetSpaceInfo();
		}

		[CacheDuration(DurationSeconds = 60)]
		public List<SpaceContents> GetSpaceContentsList()
		{
			return new SpaceApiBiz().GetSpaceContentsList();
		}

		[CacheDuration(DurationSeconds = 60)]
		public SpaceContentsDetail GetSpaceContentsDetail(long contentsSeq)
		{
			return new SpaceApiBiz().GetSpaceContentsDetail(contentsSeq);
		}
		
		[CacheDuration(DurationSeconds = 60)]
		public SpaceSectionItem GetSpaceSectionItem(long lgroupNo, long mgroupNo, int pageNo, int pageSize)
		{
			return new SpaceApiBiz().GetSpaceSectionItem(lgroupNo, mgroupNo, pageNo, pageSize);
		}
		
		[CacheDuration(DurationSeconds = 60)]
		public SpaceBrandSectionItem GetSpaceBrandSectionItem(long lgroupNo, long mgroupNo)
		{
			return new SpaceApiBiz().GetSpaceBrandSectionItem(lgroupNo, mgroupNo);
		}
		
		[CacheDuration(DurationSeconds = 60)]
		public SpaceBrandGroupDetail GetSpaceBrandDetail(long seq)
		{
			return new SpaceApiBiz().GetSpaceBrandDetail(seq);
		}
	}
}

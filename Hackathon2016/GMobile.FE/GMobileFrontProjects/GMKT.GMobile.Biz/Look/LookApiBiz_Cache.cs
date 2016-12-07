using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.MobileCache;

namespace GMKT.GMobile.Biz
{
	public class LookApiBiz_Cache : CacheContextObject
	{
		[CacheDuration(DurationSeconds = 60)]
		public LookInfo GetLookInfo()
		{
			return new LookApiBiz().GetLookInfo();
		}

		[CacheDuration(DurationSeconds = 60)]
		public LookV2Main GetLookV2Main()
		{
			return new LookApiBiz().GetLookV2Main();
		}

		[CacheDuration(DurationSeconds = 60)]
		public LookV2BrandGalleryMain GetLookV2BrandGalleryMain(LookV2PageType pageType)
		{
			return new LookApiBiz().GetLookV2BrandGalleryMain(pageType);
		}

		[CacheDuration(DurationSeconds = 60)]
		public List<LookV2Category> GetLookV2Category(LookV2CategoryType categoryType)
		{
			return new LookApiBiz().GetLookV2Category(categoryType);
		}

		[CacheDuration(DurationSeconds = 60)]
		public LookV2Best GetLookV2BrandBest(LookV2PageType pageType, LookV2CategoryType categoryType, string lCategoryCode, int brandNo, int pageNo, int pageSize)
		{
			return new LookApiBiz().GetLookV2BrandBest(pageType, categoryType, lCategoryCode, brandNo, pageNo, pageSize);
		}

		[CacheDuration(DurationSeconds = 60)]
		public LookV2BrandLp GetBrandDetail(LookV2PageType pageType, LookV2CategoryType categoryType, long brandGallerySeq)
		{
			return new LookApiBiz().GetBrandDetail(pageType, categoryType, brandGallerySeq);
		}

		#region LookV2 Cast
		[CacheDuration(DurationSeconds = 60)]
		public LookV2Cast GetLookV2Cast(LookV2PageType pageType, int pageNo, int pageSize)
		{
			return new LookApiBiz().GetLookV2Cast(pageType, pageNo, pageSize);
		}

		[CacheDuration(DurationSeconds = 60)]
		public LookV2CastDetailModel GetLookV2CastDetailModel(long castContentsSeq)
		{
			return new LookApiBiz().GetLookV2CastDetailModel(castContentsSeq);
		}
		#endregion

		[CacheDuration(DurationSeconds = 60)]
		public List<LookContents> GetLookContentsList()
		{
			return new LookApiBiz().GetLookContentsList();
		}

		[CacheDuration(DurationSeconds = 60)]
		public LookContentsDetail GetLookContentsDetail(long contentsSeq)
		{
			return new LookApiBiz().GetLookContentsDetail(contentsSeq);
		}

		[CacheDuration(DurationSeconds = 60)]
		public LookSectionItem GetLookSectionItem(long groupNo, int pageNo, int pageSize, string gdLcCd)
		{
			return new LookApiBiz().GetLookSectionItem(groupNo, pageNo, pageSize, gdLcCd);
		}
	}
}

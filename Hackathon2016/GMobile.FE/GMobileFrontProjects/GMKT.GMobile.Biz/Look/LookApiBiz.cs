using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;
using GMKT.GMobile.Util;

namespace GMKT.GMobile.Biz
{
	public class LookApiBiz
	{
		public LookInfo GetLookInfo()
		{
			var response = new LookApiDac().GetLookInfo();
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new LookInfo()
				{
					ContentsSection = new LookContentsSection()
					{
						ContentsList = new List<LookContents>()
					},
					LookSection = new LookSection()
					{
						GroupList = new List<LookGroup>()
					}
				};
			}
		}

		public LookV2Main GetLookV2Main()
		{
			var response = new LookApiDac().GetLookV2Main();
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new LookV2Main()
				{
					Fashion = new List<LookV2Group>(),
					Beauty = new List<LookV2Group>()
				};
			}


		}

		public LookV2BrandGalleryMain GetLookV2BrandGalleryMain(LookV2PageType pageType)
		{
			var response = new LookApiDac().GetLookV2BrandGalleryMain(pageType);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new LookV2BrandGalleryMain()
				{
					GalleryItems = new List<LookV2BrandGalleryItem>(),
					Category = new List<LookV2Category>(),
					BestItems = new List<LookV2Item>()
				};
			}
		}

		public List<LookV2Category> GetLookV2Category(LookV2CategoryType categoryType)
		{
			var response = new LookApiDac().GetLookV2Category(categoryType);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new List<LookV2Category>();
				
			}
		}

		public LookV2Best GetLookV2BrandBest(LookV2PageType pageType, LookV2CategoryType categoryType, string lCategoryCode, int brandNo, int pageNo, int pageSize)
		{
			var response = new LookApiDac().GetLookV2BrandBest(pageType, categoryType, lCategoryCode, brandNo, pageNo, pageSize);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new LookV2Best()
				{
					BestItems = new List<LookV2Item>(),
					Paging = new LookV2Paging()
				};
			}
		}

		public LookV2BrandLp GetBrandDetail(LookV2PageType pageType, LookV2CategoryType categoryType, long brandGallerySeq)
		{
			var response = new LookApiDac().GetBrandDetail(pageType, categoryType, brandGallerySeq);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new LookV2BrandLp()
				{
					Items = new List<LookV2Item>(),
					ImageUrl = "",
					BrandNo = 0,
					BrandGallerySeq = 0
				};
			}
		}

		#region LookV2 Cast
		public LookV2Cast GetLookV2Cast(LookV2PageType pageType, int pageNo, int pageSize)
		{
			var response = new LookApiDac().GetLookV2Cast(pageType, pageNo, pageSize);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new LookV2Cast();
			}
		}

		public LookV2CastDetail GetLookV2CastDetail(long castContentsSeq)
		{
			var response = new LookApiDac().GetLookV2CastDetail(castContentsSeq);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new LookV2CastDetail();
			}
		}

		public LookV2CastDetailModel GetLookV2CastDetailModel(long castContentsSeq)
		{
			LookV2CastDetailModel result = new LookV2CastDetailModel();

			LookV2CastDetail castDetail = new LookApiBiz().GetLookV2CastDetail(castContentsSeq);
			if (castDetail != null)
			{
				result = this.ConvertToCastDetailModel(castDetail);
			}

			return result;
		}

		private LookV2CastDetailModel ConvertToCastDetailModel(LookV2CastDetail data)
		{
			LookV2CastDetailModel result = new LookV2CastDetailModel();

			if (data != null)
			{
				if (data.Cast != null)
				{
					LookV2CastContents cast = data.Cast;
					result.Seq = cast.Seq;
					result.PageType = cast.PageType;
					result.ContentsTitle = cast.ContentsTitle;
					result.IsMovie = cast.ContentsType == LookV2CastContentsType.Movie ? true : false;
					result.IframeHtml = cast.TextHtml;
					result.ImageUrl = cast.ImageUrl;
					result.MainText1 = cast.MainText1;
					result.MainText2 = cast.MainText2;
					result.PublisherImageUrl = cast.PublisherImageUrl;
					result.PublisherName = cast.PublisherName;
					result.TitleLandingUrl = result.PageType == LookV2PageType.Unknown ? String.Format("{0}/Look", Urls.MobileWebUrl) : String.Format("{0}/Look/Cast?pageType={1}", Urls.MobileWebUrl, result.PageType);
					if (result.IsMovie)
					{
						string inValidMovieImageUrl = Urls.MobileImagePics + "/mobile/main/not_used.jpg";
						string defaultImageUrl = String.Format("<img src=\"{0}\" alt=\"invalid contents\">", inValidMovieImageUrl);
						if (String.IsNullOrEmpty(result.IframeHtml))
						{
							result.IsValidMovie = false;
							result.IframeHtml = defaultImageUrl;
						}
						else
						{
							string iframeHtml = result.IframeHtml.Trim().ToLower();
							if (iframeHtml.StartsWith("<iframe") && iframeHtml.EndsWith("</iframe>"))
							{
								result.IsValidMovie = true;
							}
							else
							{
								result.IsValidMovie = false;
								result.IframeHtml = defaultImageUrl;
							}
						}
					}
				}
				result.RelatedItems = data.RelatedItems != null ? data.RelatedItems : new List<LookV2CastRelatedItem>();
				result.RelatedCasts = data.RelatedCasts != null ? data.RelatedCasts : new List<LookV2CastContents>();
			}

			return result;
		}
		#endregion


		public List<LookContents> GetLookContentsList()
		{
			var response = new LookApiDac().GetLookContentsList();
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new List<LookContents>();
			}
		}

		public LookContentsDetail GetLookContentsDetail(long contentsSeq)
		{
			var response = new LookApiDac().GetLookContentsDetail(contentsSeq);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else if(response.ResultCode == 1201)
			{
				return new LookContentsDetail()
				{
					Html = "1201",
					Items = new List<LookContentsItem>()
				};
			}
			else
			{
				return new LookContentsDetail()
				{
					Items = new List<LookContentsItem>()
				};
			}
		}

		public LookSectionItem GetLookSectionItem(long groupNo, int pageNo, int pageSize, string gdLcCd)
		{
			var response = new LookApiDac().GetLookSectionItem(groupNo, pageNo, pageSize, gdLcCd);
			if (response != null && response.ResultCode == 0 && response.Data != null)
			{
				return response.Data;
			}
			else
			{
				return new LookSectionItem()
				{
					ItemGroupList = new List<LookItemGroup>(),
					Paging = new LookPaging()
				};
			}
		}
	}
}

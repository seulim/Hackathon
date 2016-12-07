using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Data;
using Newtonsoft.Json;
using GMKT.GMobile.Util;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.CommonData;

namespace GMKT.GMobile.Web.Controllers
{
	public class LookController : GMobileControllerBase
	{
		private readonly string LOOK_TITLE = "패션 - G마켓 모바일";
		public ActionResult Index()
		{
			ViewBag.Title = LOOK_TITLE;
			ViewBag.IsBeautyFirst = false;
			SetHomeTabName("패션");

			PageAttr.IsMain = true;
			PageAttr.HeaderType = HeaderTypeEnum.Normal;

			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket 패션";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Look";
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket 패션";
			#endregion


			//LookV2MainEntity 초기화
			LookV2MainEntity model = new LookV2MainEntity()
									{
										Fashion = new LookV2Group(),
										Beauty =new LookV2Group()
									};
			model.Fashion.Promotion = new List<LookV2Promotion>();
			model.Beauty.Promotion = new List<LookV2Promotion>();
			
			model.Fashion.Cast = new LookV2CastMain();
			model.Fashion.Cast.Items = new List<LookV2CastContents>();
			model.Beauty.Cast = new LookV2CastMain();
			model.Beauty.Cast.Items = new List<LookV2CastContents>();
			
			model.Fashion.SohoGallery = new LookV2SohoMain();
			model.Fashion.SohoGallery.Items = new List<LookV2SohoMainItemGroup>();
			model.Beauty.SohoGallery = new LookV2SohoMain();
			model.Beauty.SohoGallery.Items = new List<LookV2SohoMainItemGroup>();
			
			model.Fashion.BrandGallery = new LookV2BrandMain();
			model.Fashion.BrandGallery.Items = new List<LookV2BrandGalleryItem>();
			model.Beauty.BrandGallery = new LookV2BrandMain();
			model.Beauty.BrandGallery.Items = new List<LookV2BrandGalleryItem>();
			
			model.Fashion.BrandBest = new LookV2BrandBestMain();
			model.Fashion.BrandBest.Items = new List<LookV2Item>();
			model.Beauty.BrandBest = new LookV2BrandBestMain();
			model.Beauty.BrandBest.Items = new List<LookV2Item>();

			
			//api call
			LookV2Main tempModel = new LookApiBiz_Cache().GetLookV2Main();
			if (tempModel == null)
			{
				tempModel = new LookApiBiz().GetLookV2Main();
			}

			//Fashion Model 유효한 값 할당
			if (tempModel != null && tempModel.Fashion != null && tempModel.Fashion.Count > 0)
			{
				foreach (LookV2Group entity in tempModel.Fashion)
				{
					if (entity.Type == LookV2MainGroupType.Promotion)
					{
						if (entity.Promotion != null && entity.Promotion.Count > 0)
						{
							model.Fashion.Promotion = entity.Promotion;
							for (int i = 0; i < model.Fashion.Promotion.Count; i++)
							{
								if (model.Fashion.Promotion[i].ExposeType == LookV2PromotionType.AType)
								{
									model.Fashion.Promotion[i].PromotionClassStr = "look_list type_a";
								}
								else if (model.Fashion.Promotion[i].ExposeType == LookV2PromotionType.BType)
								{
									model.Fashion.Promotion[i].PromotionClassStr = "look_list type_b";
								}
								else
								{
									model.Fashion.Promotion[i].PromotionClassStr = "look_list type_a";
								}

								if (model.Fashion.Promotion[i].Price == null || model.Fashion.Promotion[i].Price == String.Empty)
								{
									model.Fashion.Promotion[i].PromotionSpanClassStr = "info_box copy";
								}
								else
								{
									model.Fashion.Promotion[i].PromotionSpanClassStr = "info_box";
								}
							}
						}
					}
					else if (entity.Type == LookV2MainGroupType.Cast)
					{
						if (entity.Cast != null && entity.Cast.Items != null && entity.Cast.Items.Count > 0)
						{
							model.Fashion.Cast = entity.Cast;
						}
					}
					else if (entity.Type == LookV2MainGroupType.SohoGallery)
					{
						if (entity.SohoGallery != null && entity.SohoGallery.Items != null && entity.SohoGallery.Items.Count > 0)
						{
							model.Fashion.SohoGallery.Title = entity.SohoGallery.Title;
							model.Fashion.SohoGallery.MoreLandingUrl = entity.SohoGallery.MoreLandingUrl;
							model.Fashion.SohoGallery.DetailViewText = entity.SohoGallery.DetailViewText;
							foreach(LookV2SohoMainItemGroup miniEntity in entity.SohoGallery.Items){
								LookV2SohoMainItemGroup sohoItemEntity = new LookV2SohoMainItemGroup();
								sohoItemEntity.Type = miniEntity.Type;
								sohoItemEntity.Items = new List<LookV2SohoMainItem>();
								if(miniEntity.Items != null && miniEntity.Items.Count > 0){
									sohoItemEntity.Items = miniEntity.Items;
								}
								sohoItemEntity.SohoMainItemGroupClass = sohoItemEntity.Type == LookV2SohoMainItemGroupType.LeftTwoRightBig ? "soho_list v2" : "soho_list";

								model.Fashion.SohoGallery.Items.Add(sohoItemEntity);
							}
						}
					}
					else if (entity.Type == LookV2MainGroupType.BrandGallery)
					{
						if (entity.BrandGallery != null && entity.BrandGallery.Items != null && entity.BrandGallery.Items.Count > 0)
						{
							model.Fashion.BrandGallery = entity.BrandGallery;
						}
					}
					else if (entity.Type == LookV2MainGroupType.BrandBest)
					{
						if (entity.BrandBest != null && entity.BrandBest.Items != null && entity.BrandBest.Items.Count > 0)
						{
							model.Fashion.BrandBest = entity.BrandBest;
						}
					}
				}
			}

			//Beauty Model 유효한 값 할당
			if (tempModel != null && tempModel.Beauty != null && tempModel.Beauty.Count > 0)
			{
				foreach (LookV2Group entity in tempModel.Beauty)
				{
					if (entity.Type == LookV2MainGroupType.Promotion)
					{
						if (entity.Promotion != null && entity.Promotion.Count > 0)
						{
							//model.Beauty.Promotion = entity.Promotion;
							for (int i = 0; i < entity.Promotion.Count; i++)
							{
								model.Beauty.Promotion.Add(entity.Promotion[i]);

								if (model.Beauty.Promotion[model.Beauty.Promotion.Count-1].ExposeType == LookV2PromotionType.AType)
								{
									model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].PromotionClassStr = "look_list type_a";
								}
								else if (model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].ExposeType == LookV2PromotionType.BType)
								{
									model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].PromotionClassStr = "look_list type_b";
								}
								else
								{
									model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].PromotionClassStr = "look_list type_a";
								}

								if (model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].Price == null || model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].Price == String.Empty)
								{
									model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].PromotionSpanClassStr = "info_box copy";
								}
								else
								{
									model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].PromotionSpanClassStr = "info_box";
								}
							}
						}
					}
					else if (entity.Type == LookV2MainGroupType.Cast)
					{
						if (entity.Cast != null && entity.Cast.Items != null && entity.Cast.Items.Count > 0)
						{
							model.Beauty.Cast = entity.Cast;
						}
					}
					else if (entity.Type == LookV2MainGroupType.SohoGallery)
					{
						if (entity.SohoGallery != null && entity.SohoGallery.Items != null && entity.SohoGallery.Items.Count > 0)
						{
							model.Beauty.SohoGallery.Title = entity.SohoGallery.Title;
							model.Beauty.SohoGallery.MoreLandingUrl = entity.SohoGallery.MoreLandingUrl;
							model.Beauty.SohoGallery.DetailViewText = entity.SohoGallery.DetailViewText;
							foreach (LookV2SohoMainItemGroup miniEntity in entity.SohoGallery.Items)
							{
								LookV2SohoMainItemGroup sohoItemEntity = new LookV2SohoMainItemGroup();
								sohoItemEntity.Type = miniEntity.Type;
								sohoItemEntity.Items = new List<LookV2SohoMainItem>();
								if (miniEntity.Items != null && miniEntity.Items.Count > 0)
								{
									sohoItemEntity.Items = miniEntity.Items;
								}
								sohoItemEntity.SohoMainItemGroupClass = sohoItemEntity.Type == LookV2SohoMainItemGroupType.LeftTwoRightBig ? "soho_list v2" : "soho_list";

								model.Beauty.SohoGallery.Items.Add(sohoItemEntity);
							}
						}
					}
					else if (entity.Type == LookV2MainGroupType.BrandGallery)
					{
						if (entity.BrandGallery != null && entity.BrandGallery.Items != null && entity.BrandGallery.Items.Count > 0)
						{
							model.Beauty.BrandGallery = entity.BrandGallery;
						}
					}
					else if (entity.Type == LookV2MainGroupType.BrandBest)
					{
						if (entity.BrandBest != null && entity.BrandBest.Items != null && entity.BrandBest.Items.Count > 0)
						{
							model.Beauty.BrandBest = entity.BrandBest;
						}
					}
				}
			}

			
			
			LookV2MainModel lookV2Model = new LookV2MainModel();
			lookV2Model.Fashion = model.Fashion;
			lookV2Model.Beauty = model.Beauty;

			/*Landing Banner */
			new LandingBannerSetter(Request).Set(lookV2Model, PageAttr.IsApp);

			return View(lookV2Model);
		}

		public ActionResult Beauty()
		{
			ViewBag.Title = LOOK_TITLE;
			SetHomeTabName("패션");
			ViewBag.IsBeautyFirst = true;

			PageAttr.IsMain = true;
			PageAttr.HeaderType = HeaderTypeEnum.Normal;

			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket 패션";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Look/Beauty";
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket 패션";
			#endregion


			//LookV2MainEntity 초기화
			LookV2MainEntity model = new LookV2MainEntity()
			{
				Fashion = new LookV2Group(),
				Beauty = new LookV2Group()
			};
			model.Fashion.Promotion = new List<LookV2Promotion>();
			model.Beauty.Promotion = new List<LookV2Promotion>();

			model.Fashion.Cast = new LookV2CastMain();
			model.Fashion.Cast.Items = new List<LookV2CastContents>();
			model.Beauty.Cast = new LookV2CastMain();
			model.Beauty.Cast.Items = new List<LookV2CastContents>();

			model.Fashion.SohoGallery = new LookV2SohoMain();
			model.Fashion.SohoGallery.Items = new List<LookV2SohoMainItemGroup>();
			model.Beauty.SohoGallery = new LookV2SohoMain();
			model.Beauty.SohoGallery.Items = new List<LookV2SohoMainItemGroup>();

			model.Fashion.BrandGallery = new LookV2BrandMain();
			model.Fashion.BrandGallery.Items = new List<LookV2BrandGalleryItem>();
			model.Beauty.BrandGallery = new LookV2BrandMain();
			model.Beauty.BrandGallery.Items = new List<LookV2BrandGalleryItem>();

			model.Fashion.BrandBest = new LookV2BrandBestMain();
			model.Fashion.BrandBest.Items = new List<LookV2Item>();
			model.Beauty.BrandBest = new LookV2BrandBestMain();
			model.Beauty.BrandBest.Items = new List<LookV2Item>();


			//api call
			LookV2Main tempModel = new LookApiBiz_Cache().GetLookV2Main();
			if (tempModel == null)
			{
				tempModel = new LookApiBiz().GetLookV2Main();
			}

			//Fashion Model 유효한 값 할당
			if (tempModel != null && tempModel.Fashion != null && tempModel.Fashion.Count > 0)
			{
				foreach (LookV2Group entity in tempModel.Fashion)
				{
					if (entity.Type == LookV2MainGroupType.Promotion)
					{
						if (entity.Promotion != null && entity.Promotion.Count > 0)
						{
							model.Fashion.Promotion = entity.Promotion;
							for (int i = 0; i < model.Fashion.Promotion.Count; i++)
							{
								if (model.Fashion.Promotion[i].ExposeType == LookV2PromotionType.AType)
								{
									model.Fashion.Promotion[i].PromotionClassStr = "look_list type_a";
								}
								else if (model.Fashion.Promotion[i].ExposeType == LookV2PromotionType.BType)
								{
									model.Fashion.Promotion[i].PromotionClassStr = "look_list type_b";
								}
								else
								{
									model.Fashion.Promotion[i].PromotionClassStr = "look_list type_a";
								}

								if (model.Fashion.Promotion[i].Price == null || model.Fashion.Promotion[i].Price == String.Empty)
								{
									model.Fashion.Promotion[i].PromotionSpanClassStr = "info_box copy";
								}
								else
								{
									model.Fashion.Promotion[i].PromotionSpanClassStr = "info_box";
								}
							}
						}
					}
					else if (entity.Type == LookV2MainGroupType.Cast)
					{
						if (entity.Cast != null && entity.Cast.Items != null && entity.Cast.Items.Count > 0)
						{
							model.Fashion.Cast = entity.Cast;
						}
					}
					else if (entity.Type == LookV2MainGroupType.SohoGallery)
					{
						if (entity.SohoGallery != null && entity.SohoGallery.Items != null && entity.SohoGallery.Items.Count > 0)
						{
							model.Fashion.SohoGallery.Title = entity.SohoGallery.Title;
							model.Fashion.SohoGallery.MoreLandingUrl = entity.SohoGallery.MoreLandingUrl;
							model.Fashion.SohoGallery.DetailViewText = entity.SohoGallery.DetailViewText;
							foreach (LookV2SohoMainItemGroup miniEntity in entity.SohoGallery.Items)
							{
								LookV2SohoMainItemGroup sohoItemEntity = new LookV2SohoMainItemGroup();
								sohoItemEntity.Type = miniEntity.Type;
								sohoItemEntity.Items = new List<LookV2SohoMainItem>();
								if (miniEntity.Items != null && miniEntity.Items.Count > 0)
								{
									sohoItemEntity.Items = miniEntity.Items;
								}
								sohoItemEntity.SohoMainItemGroupClass = sohoItemEntity.Type == LookV2SohoMainItemGroupType.LeftTwoRightBig ? "soho_list v2" : "soho_list";

								model.Fashion.SohoGallery.Items.Add(sohoItemEntity);
							}
						}
					}
					else if (entity.Type == LookV2MainGroupType.BrandGallery)
					{
						if (entity.BrandGallery != null && entity.BrandGallery.Items != null && entity.BrandGallery.Items.Count > 0)
						{
							model.Fashion.BrandGallery = entity.BrandGallery;
						}
					}
					else if (entity.Type == LookV2MainGroupType.BrandBest)
					{
						if (entity.BrandBest != null && entity.BrandBest.Items != null && entity.BrandBest.Items.Count > 0)
						{
							model.Fashion.BrandBest = entity.BrandBest;
						}
					}
				}
			}

			//Beauty Model 유효한 값 할당
			if (tempModel != null && tempModel.Beauty != null && tempModel.Beauty.Count > 0)
			{
				foreach (LookV2Group entity in tempModel.Beauty)
				{
					if (entity.Type == LookV2MainGroupType.Promotion)
					{
						if (entity.Promotion != null && entity.Promotion.Count > 0)
						{
							//model.Beauty.Promotion = entity.Promotion;
							for (int i = 0; i < entity.Promotion.Count; i++)
							{
								model.Beauty.Promotion.Add(entity.Promotion[i]);

								if (model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].ExposeType == LookV2PromotionType.AType)
								{
									model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].PromotionClassStr = "look_list type_a";
								}
								else if (model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].ExposeType == LookV2PromotionType.BType)
								{
									model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].PromotionClassStr = "look_list type_b";
								}
								else
								{
									model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].PromotionClassStr = "look_list type_a";
								}

								if (model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].Price == null || model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].Price == String.Empty)
								{
									model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].PromotionSpanClassStr = "info_box copy";
								}
								else
								{
									model.Beauty.Promotion[model.Beauty.Promotion.Count - 1].PromotionSpanClassStr = "info_box";
								}
							}
						}
					}
					else if (entity.Type == LookV2MainGroupType.Cast)
					{
						if (entity.Cast != null && entity.Cast.Items != null && entity.Cast.Items.Count > 0)
						{
							model.Beauty.Cast = entity.Cast;
						}
					}
					else if (entity.Type == LookV2MainGroupType.SohoGallery)
					{
						if (entity.SohoGallery != null && entity.SohoGallery.Items != null && entity.SohoGallery.Items.Count > 0)
						{
							model.Beauty.SohoGallery.Title = entity.SohoGallery.Title;
							model.Beauty.SohoGallery.MoreLandingUrl = entity.SohoGallery.MoreLandingUrl;
							model.Beauty.SohoGallery.DetailViewText = entity.SohoGallery.DetailViewText;
							foreach (LookV2SohoMainItemGroup miniEntity in entity.SohoGallery.Items)
							{
								LookV2SohoMainItemGroup sohoItemEntity = new LookV2SohoMainItemGroup();
								sohoItemEntity.Type = miniEntity.Type;
								sohoItemEntity.Items = new List<LookV2SohoMainItem>();
								if (miniEntity.Items != null && miniEntity.Items.Count > 0)
								{
									sohoItemEntity.Items = miniEntity.Items;
								}
								sohoItemEntity.SohoMainItemGroupClass = sohoItemEntity.Type == LookV2SohoMainItemGroupType.LeftTwoRightBig ? "soho_list v2" : "soho_list";

								model.Beauty.SohoGallery.Items.Add(sohoItemEntity);
							}
						}
					}
					else if (entity.Type == LookV2MainGroupType.BrandGallery)
					{
						if (entity.BrandGallery != null && entity.BrandGallery.Items != null && entity.BrandGallery.Items.Count > 0)
						{
							model.Beauty.BrandGallery = entity.BrandGallery;
						}
					}
					else if (entity.Type == LookV2MainGroupType.BrandBest)
					{
						if (entity.BrandBest != null && entity.BrandBest.Items != null && entity.BrandBest.Items.Count > 0)
						{
							model.Beauty.BrandBest = entity.BrandBest;
						}
					}
				}
			}



			LookV2MainModel lookV2Model = new LookV2MainModel();
			lookV2Model.Fashion = model.Fashion;
			lookV2Model.Beauty = model.Beauty;

			/*Landing Banner */
			new LandingBannerSetter(Request).Set(lookV2Model, PageAttr.IsApp);

			return View("Index",lookV2Model);
		}


		[HttpGet]
		public ActionResult GetLookV2BrandGallery(LookV2PageType pageType = LookV2PageType.Fashion)
		{
			LookV2BrandGalleryMain brandGalleryModel = new LookV2BrandGalleryMain()
			{
				GalleryItems = new List<LookV2BrandGalleryItem>(),
				Category = new List<LookV2Category>(),
				BestItems = new List<LookV2Item>(),
				BestPaging = new LookV2Paging()
			};

			//NULL처리?
			LookV2BrandGalleryMain model = new LookApiBiz_Cache().GetLookV2BrandGalleryMain(pageType);
			if (model == null)
			{
				model = new LookApiBiz().GetLookV2BrandGalleryMain(pageType);
			}

			if (model.BestPaging != null)
			{
				brandGalleryModel.BestPaging = model.BestPaging;
			}

			if (model.GalleryItems != null && model.GalleryItems.Count > 0)
			{
				brandGalleryModel.GalleryItems = model.GalleryItems;
			}

			if (model.Category != null && model.Category.Count > 0)
			{
				brandGalleryModel.Category = model.Category;
			}

			if (model.BestItems != null && model.BestItems.Count > 0)
			{
				brandGalleryModel.BestItems = model.BestItems;
			}

			return Content(JsonConvert.SerializeObject(brandGalleryModel), "application/json");
		}

		public ActionResult BrandGalleryAndBest(LookV2PageType pageType = LookV2PageType.Fashion, LookV2BrandMenuType menuType = LookV2BrandMenuType.Gallery)
		{
			ViewBag.Title = LOOK_TITLE;

			if (pageType == LookV2PageType.Fashion)
			{
				ViewBag.TitleName = "패션 Brand";
				ViewBag.HeaderTitle = "패션 Brand";
			}
			else
			{
				ViewBag.TitleName = "뷰티 Brand";
				ViewBag.HeaderTitle = "뷰티 Brand";
			}

			ViewBag.pageType = pageType == LookV2PageType.Fashion ? "Fashion" : "Beauty";
			ViewBag.menuType = menuType == LookV2BrandMenuType.Gallery ? "Gallery" : "Best";

			return View();
		}

		public ActionResult SohoGallery(LookV2PageType pageType = LookV2PageType.Fashion)
		{
			ViewBag.HeaderTitle = "SOHO Gallery";
			ViewBag.Title = LOOK_TITLE;


			return View();
		}

		public ActionResult GetLookV2Category(LookV2CategoryType categoryType = LookV2CategoryType.Soho)
		{
			List<LookV2Category> categoryModel = new List<LookV2Category>();
			

			List<LookV2Category> model = new LookApiBiz_Cache().GetLookV2Category(categoryType);
			if (model == null)
			{
				model = new LookApiBiz().GetLookV2Category(categoryType);
			}

			if (model.Count > 0)
			{
				categoryModel = model;
			}
			

			return Content(JsonConvert.SerializeObject(categoryModel), "application/json");
		}

		public ActionResult GetLookV2BrandBest(LookV2PageType pageType = LookV2PageType.Fashion, LookV2CategoryType categoryType = LookV2CategoryType.Brand, string lCategoryCode = "ALL", int brandNo = 0, int pageNo = 1, int pageSize = 100)
		{
			LookV2Best bestModel = new LookV2Best()
			{
				BestItems = new List<LookV2Item>(),
				Paging = new LookV2Paging()
			};

			LookV2Best model = new LookApiBiz_Cache().GetLookV2BrandBest(pageType, categoryType, lCategoryCode, brandNo, pageNo, pageSize);
			if (model == null)
			{
				model = new LookApiBiz().GetLookV2BrandBest(pageType, categoryType, lCategoryCode, brandNo, pageNo, pageSize);
			}

			if (model.BestItems != null && model.BestItems.Count > 0)
			{
				bestModel.BestItems = model.BestItems;
			}

			if (model.Paging != null)
			{
				bestModel.Paging = model.Paging;
			}

			return Content(JsonConvert.SerializeObject(bestModel), "application/json");
		}

		public ActionResult GetLookV2BrandBestMore()
		{

			return View();
		}

		public ActionResult BrandDetail(LookV2PageType pageType = LookV2PageType.Fashion, LookV2CategoryType categoryType = LookV2CategoryType.Brand, long brandGallerySeq = 0)
		{
			ViewBag.Title = LOOK_TITLE;

			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket 패션";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Look";
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket 패션";
			#endregion

			if (pageType == LookV2PageType.Fashion)
			{
				ViewBag.TitleName = "패션 Brand";
			}
			else
			{
				ViewBag.TitleName = "뷰티 Brand";
			}

			LookV2BrandLp brandLpModel = new LookV2BrandLp()
			{
				Items = new List<LookV2Item>(),
				ImageUrl = "",
				BrandNo = 0,
				BrandGallerySeq = 0
			};

			LookV2BrandLp model = new LookApiBiz_Cache().GetBrandDetail(pageType, categoryType, brandGallerySeq);
			if (model == null)
			{
				model = new LookApiBiz().GetBrandDetail(pageType, categoryType, brandGallerySeq);
			}

			if (model != null)
			{
				brandLpModel.ImageUrl = model.ImageUrl;
				brandLpModel.BrandNo = model.BrandNo;
				brandLpModel.BrandGallerySeq = model.BrandGallerySeq;
			}
			if (model != null && model.Items != null && model.Items.Count > 0)
			{
				brandLpModel.Items = model.Items;
			}

			return View(brandLpModel);
		}

		#region LookV2 Cast
		public ActionResult Cast(LookV2PageType pageType = LookV2PageType.Fashion)
		{
			ViewBag.HeaderTitle = "패션&뷰티 Cast";
			ViewBag.Title = LOOK_TITLE;
			ViewBag.PageType = pageType;
			return View();
		}

		[HttpPost]
		public ActionResult GetLookV2Cast(LookV2PageType pageType, int pageNo, int pageSize)
		{
			LookV2Cast model = new LookApiBiz_Cache().GetLookV2Cast(pageType, pageNo, pageSize);
			if (model == null)
			{
				model = new LookApiBiz().GetLookV2Cast(pageType, pageNo, pageSize);
			}
			
			if (model == null)
			{
				model = new LookV2Cast();
			}

			return Content(JsonConvert.SerializeObject(model), "application/json");
		}

		public ActionResult CastDetail(Nullable<long> id)
		{
			ViewBag.HeaderTitle = "패션&뷰티 Cast";
			ViewBag.Title = LOOK_TITLE;

			LookV2CastDetailModel model = new LookV2CastDetailModel();

			if (id.HasValue && id.Value > 0)
			{
			    model = new LookApiBiz_Cache().GetLookV2CastDetailModel(id.Value);
			    if (model == null)
			    {
			        model = new LookApiBiz().GetLookV2CastDetailModel(id.Value);
			    }
			}
			
			if (model == null)
			{
				model = new LookV2CastDetailModel();
			}

			return View(model);
		}
		#endregion

		public ActionResult IndexV1()
		{
			ViewBag.Title = LOOK_TITLE;
			SetHomeTabName("LOOK");

			PageAttr.IsMain = true;

			#region 페이스북 공유하기
			PageAttr.FbTitle = "Gmarket LOOK";
			PageAttr.FbTagUrl = Urls.MobileWebUrl + "/Look";
			PageAttr.FbTagImage = "";
			PageAttr.FbTagDescription = "Gmarket LOOK";
			#endregion

			LookInfo model = new LookApiBiz_Cache().GetLookInfo();
			if (model == null)
			{
				model = new LookApiBiz().GetLookInfo();
			}

			LookModel lookModel = new LookModel();
			lookModel.ContentsSection = model.ContentsSection;
			lookModel.LookSection = model.LookSection;

			/* Landing Banner */
			new LandingBannerSetter(Request).Set(lookModel, PageAttr.IsApp);

			return View(lookModel);
		}

		[HttpPost]
		public ActionResult GetLookSectionItem(long groupNo, int pageNo = 1, int pageSize = 80, string gdLcCd = "")
		{
			LookSectionItem model = new LookApiBiz_Cache().GetLookSectionItem(groupNo, pageNo, pageSize, gdLcCd);
			if (model == null)
			{
				model = new LookApiBiz().GetLookSectionItem(groupNo, pageNo, pageSize, gdLcCd);
			}

			return Content(JsonConvert.SerializeObject(model), "application/json");
		}

		public ActionResult ContentsAll(Nullable<long> id)
		{
			ViewBag.Title = LOOK_TITLE;
			List<LookContents> model = new LookApiBiz_Cache().GetLookContentsList();
			if (model == null)
			{
				model = new LookApiBiz().GetLookContentsList();
			}


			bool contentExist = false;
			if (id != null)
			{
				foreach (LookContents contents in model)
				{
					if (contents.ContentsSeq == id)
					{
						ViewBag.selectedContentId = id;
						contentExist = true;
						break;
					}
				}
			}
			else
			{
				contentExist = true;
			}

			if (!contentExist)
			{
				string href = Urls.MobileWebUrl + "/Look/ContentsAll";
				return AlertMessageAndLocationChange("컨텐츠 전시기간이 종료되었습니다.", href);
			}

			return View(model);
		}

		public ActionResult ContentsDetail(long id)
		{
			ViewBag.Title = LOOK_TITLE;
			LookContentsDetail model = new LookApiBiz_Cache().GetLookContentsDetail(id);
			if (model == null)
			{
				model = new LookApiBiz().GetLookContentsDetail(id);
			}

			if (model.Html == "1201")//api 직접 호출 후에도 없으면~
			{
				string href = Urls.MobileWebUrl + "/Look/ContentsAll";
				return AlertMessageAndLocationChange("컨텐츠 전시기간이 종료되었습니다.", href);
			}

			return View(model);
		}
	}
}

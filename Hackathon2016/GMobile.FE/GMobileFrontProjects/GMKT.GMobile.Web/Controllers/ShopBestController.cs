using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Biz;
using GMKT.GMobile.Util;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Constant;
using GMKT.Web.Context;
using GMKT.GMobile.Data.ShopBest;

namespace GMKT.GMobile.Web.Controllers
{
	public class ShopBestController : GMobileControllerBase
	{
		private const int BRAND_COUNT = 20;
		private const int ITEM_COUNT = 10;
		private const int SHOP_PAGE_SIZE = 5;

		private const int EXPOSE_BESTSELLER_GROUP_HIGH_LIMIT = 9;

		private const string ITEM_IMAGE_SIZE = "SP";
		private const string ALTERNATIVE_ITEM_IMAGE_SIZE = "M";

		private const string DEFAULT_BESTSELLER_GROUP = "G01";

		private const int DEFAULT_ITEM_GROUP = 1;

		public ActionResult Index()
		{
			return RedirectToAction( "BestShop" );
		}

		public ActionResult BestShop( string id = DEFAULT_BESTSELLER_GROUP, int pageNo = 1 )
		{
			PageAttr.IsMain = true;

			BestShopListModel model = new BestShopListModel();
			ShopBestApiBiz_Cache cache = new ShopBestApiBiz_Cache();

			BestShopsData data = cache.GetBestShopList( id, pageNo, SHOP_PAGE_SIZE, ITEM_COUNT );

			FillModelShopAndItems( data, model );

			model.BestSellerGroupList = cache.GetBestSellerGroupList().TakeWhile( ( T, Q ) => Q < EXPOSE_BESTSELLER_GROUP_HIGH_LIMIT ).ToList();
			model.GroupCode = id;
			model.RenderScript = true;
			model.HasRank = true;

			return View( model );
		}

		public ActionResult NewShop( string id = DEFAULT_BESTSELLER_GROUP, int pageNo = 1 )
		{
			PageAttr.IsMain = true;

			NewShopListModel model = new NewShopListModel();
			ShopBestApiBiz_Cache cache = new ShopBestApiBiz_Cache();

			NewShopsData data = cache.GetNewShopList( id, pageNo, SHOP_PAGE_SIZE, ITEM_COUNT );

			FillModelShopAndItems( data, model );

			model.BestSellerGroupList = cache.GetBestSellerGroupList().TakeWhile( ( T, Q ) => Q < EXPOSE_BESTSELLER_GROUP_HIGH_LIMIT ).ToList();
			model.GroupCode = id;
			model.RenderScript = true;
			model.HasRank = false;

			return View( model );
		}

		public ActionResult Brand( string id = DEFAULT_BESTSELLER_GROUP )
		{
			PageAttr.IsMain = true;

			BrandListModel model = new BrandListModel();
			ShopBestApiBiz_Cache cache = new ShopBestApiBiz_Cache();

			model.GroupCode = id;
			model.BestSellerGroupList = cache.GetBestSellerGroupList().TakeWhile( ( T, Q ) => Q < EXPOSE_BESTSELLER_GROUP_HIGH_LIMIT ).ToList();

			model.BrandList = cache.GetBrandList( id, BRAND_COUNT );

			return View( model );
		}

		public ActionResult BrandShop( string id, int brandNo, string brandName = "", int pageNo = 1 )
		{
			PageAttr.IsMain = true;

			BrandShopListModel model = new BrandShopListModel();
			ShopBestApiBiz_Cache cache = new ShopBestApiBiz_Cache();

			BrandShopsData data = cache.GetBrandShopList( id, brandNo, pageNo, SHOP_PAGE_SIZE, ITEM_COUNT );

			FillModelShopAndItems( data, model );

			model.BrandName = brandName;
			model.BrandNo = brandNo;
			model.GroupCode = id;
			model.RenderScript = true;
			model.HasRank = true;

			return View( model );
		}

		public ActionResult FavoriteShop( int pageNo = 1, int pageSize = SHOP_PAGE_SIZE, int itemCount = ITEM_COUNT )
		{
			PageAttr.IsMain = true;

			ShopBestApiBiz biz = new ShopBestApiBiz();
			FavoriteShopListModel model = new FavoriteShopListModel();

			FavoriteShopsData data = new FavoriteShopsData();
			data.Shops = biz.GetFavoriteShopAndItems( pageNo, pageSize, ITEM_COUNT, data );

			FillModelShopAndItems( data, model );

			model.BestSellerGroupList = new List<BestSellerGroupInfo>{ 
				new BestSellerGroupInfo
				{
					GroupName = "관심매장 전체보기"
				}
			};

			model.RenderScript = true;
			model.HasRank = false;

			return View( model );
		}

		public JsonResult GetShopList( string id, int pageNo, int? brandNo = null, string indicator = "" )
		{
			ShopBestApiBiz_Cache cache = new ShopBestApiBiz_Cache();
			ShopListPageModel model = new ShopListPageModel();
			ShopList data = null;

			if( brandNo.HasValue )
			{
				data = (ShopList)cache.GetBrandShopList( id, brandNo.Value, pageNo, SHOP_PAGE_SIZE, ITEM_COUNT );
				model.HasRank = true;
			}
			else
			{
				if( String.IsNullOrEmpty( indicator ) )
				{
					data = (ShopList)cache.GetBestShopList( id, pageNo, SHOP_PAGE_SIZE, ITEM_COUNT );
					model.HasRank = true;
				}
				else if( indicator == "new" )
				{
					data = (ShopList)cache.GetNewShopList( id, pageNo, SHOP_PAGE_SIZE, ITEM_COUNT );
					model.HasRank = false;
				}
				else if( indicator == "favorite" )
				{
					data = new ShopList();
					data.Shops = new ShopBestApiBiz().GetFavoriteShopAndItems( pageNo, SHOP_PAGE_SIZE, ITEM_COUNT, data );
					model.HasRank = false;
				}
			}

			FillModelShopAndItems( data, model );
			model.RenderScript = false;

			return Json(
				new
				{
					hasRsMsg = "False",
					rsCd = "SUCCESS",
					data = BizUtil.RenderPartialViewToString( "~/Views/ShopBest/Shared/_ShopListTemplete.cshtml", model, this.ControllerContext )
				}, "application/json", JsonRequestBehavior.AllowGet
			);
		}

		[HttpGet]
		public JsonResult GetBrandList( string id )
		{
			ShopBestApiBiz_Cache cache = new ShopBestApiBiz_Cache();

			List<Brand> brandList = cache.GetBrandList( id, BRAND_COUNT );

			return Json( brandList, JsonRequestBehavior.AllowGet );
		}

		[HttpPost]
		public JsonResult SetFavoriteShop( string id )
		{
			string result = new ShopBestApiBiz().SetFavoriteShop( id );
			return Json( new { code = result } );
		}

		[HttpPost]
		public JsonResult SetFavoriteItem( string id )
		{
			string result = new ShopBestApiBiz().SetFavoriteItem( id, DEFAULT_ITEM_GROUP );
			return Json( new { code = result } );
		}

		[HttpPost]
		public JsonResult RemoveFavoriteShop( string id )
		{
			string result = new ShopBestApiBiz().RemoveFavoriteShop( id );
			return Json( new { code = result } );
		}

		private void FillModelShopAndItems( ShopList data, ShopListPageModel model )
		{
			ShopBestApiBiz biz = new ShopBestApiBiz();
			List<MiniShopInfo> favorite = null;
			if( PageAttr.IsLogin && !(model is FavoriteShopListModel) )
			{
				favorite = biz.GetAllFavoriteShop();
			}

			bool isAdult = BizUtil.IsAdultUser( GMobileWebContext.Current.UserProfile.CustNo );

			if( data.Shops != null && data.Shops.Count > 0 )
			{
				foreach( ShopAndItems i in data.Shops )
				{
					if( i.Items != null && i.Items.Count > 0 )
					{
						foreach( ShopBestItem j in i.Items )
						{
							j.ImageURL = BizUtil.GetGoodsImagePath( j.GoodsCode, ITEM_IMAGE_SIZE );
							j.AltrnativeImageUrl = BizUtil.GetGoodsImagePath( j.GoodsCode, ALTERNATIVE_ITEM_IMAGE_SIZE );
							SetItemImageNLinkByAdult( j, isAdult );
						}
					}
					if( favorite != null && favorite.Count > 0 )
					{
						if( favorite.Where( T => T.SellCustNo == i.Shop.SellCustNo ).Count() > 0 )
						{
							i.IsFavorite = true;
						}
						else
						{
							i.IsFavorite = false;
						}
					}
					else if( model is FavoriteShopListModel )
					{
						i.IsFavorite = true;
					}
					if( i.Items != null && i.Items.Count > 0 )
					{
						i.Shop.ShopRepImagePath = String.IsNullOrEmpty( i.Shop.ShopRepImagePath ) ? i.Items.Last().AltrnativeImageUrl : i.Shop.ShopRepImagePath;
					}
				}
			}
			model.ShopAndItems = data.Shops;
			model.TotalShopCount = data.TotalShopCount;
			model.PageSize = SHOP_PAGE_SIZE;
		}

		private void SetItemImageNLinkByAdult( ShopBestItem item, bool isAdult )
		{
			if( item.IsAdult == false ) return;

			if( PageAttr.IsAdultUse ) return;

			item.ImageURL = Urls.MobileImageUrlV2 + Const.ADULT_IMAGE_210.Replace( "images/", string.Empty );
			item.AltrnativeImageUrl = Urls.MobileImageUrlV2 + Const.ADULT_IMAGE_210.Replace( "images/", string.Empty );

			if( PageAttr.IsLogin )
			{
				item.MoreImageCnt = 1;
				if(!isAdult) item.LinkURL = "javascript:alert('죄송합니다.\n성인만 구매 가능한 상품입니다.');";
			}
			else
			{			
				item.LinkURL = Urls.MWebUrl + "/Login/login_mw.asp?rtnurl=" + HttpUtility.UrlEncode( item.LinkURL ) + "&adultUseLoinCheck=Y";
			}
		}
	}
}

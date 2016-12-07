using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMKT.Component.Member.Data.Entity;
using GMKT.Component.Member.MyInfo;
using GMKT.GMobile.Web.Classes;
using GMKT.GMobile.Data.HomePlus;
using GMKT.GMobile.Biz.HomePlus;
using GMKT.GMobile.Filter;
using GMKT.GMobile.Data.Search;
using GMKT.GMobile.Biz.Search;
using GMKT.Web.Context;
using GMKT.GMobile.Data;
using GMKT.GMobile.Biz;
using Nova.Thrift;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Util;
using GMKT.Web.Filter;
using GMKT.GMobile.CommonData;
using System.Web.Caching;

namespace GMKT.GMobile.Web.Controllers
{
	public class HomePlusController : HomePlusBaseController
	{
		public const int DEFAULT_ITEM_LIST_PAGE_SIZE = 20;
		private const int LISTING_PAGE_SIZE =50;
		public ActionResult Index()
		{
			ViewBag.HeaderTitle = "홈플러스 당일배송";
			ViewBag.hasBranchCode = false;
			HomePlusHomeT apiResult = new HomePlusHomeT();
			if(PageAttr.IsLogin && this.settedBranch != null)
			{
				apiResult = new HomePlusApiBiz().GetHome(settedBranch.BranchInfo.BranchCd);
				ViewBag.hasBranchCode = true;
			}
			else
			{
				apiResult = new HomePlusApiBiz().GetHome(0);
			}

			categoryList = GenericUtil.AtLeastReturnEmptyList(categoryList);
			ViewBag.categoryList = categoryList;
			ViewBag.itemListSize = DEFAULT_ITEM_LIST_PAGE_SIZE;
			PageAttr.HeaderType = HeaderTypeEnum.Simple;

			return View(apiResult);
		}

		public ActionResult SpecialShop(long id)
		{
			ViewBag.HeaderTitle = "홈플러스 당일배송";
			ViewBag.hasBranchCode = false;
			HomePlusSpecialShopT apiResult;
			if(PageAttr.IsLogin && this.settedBranch != null)
			{
				apiResult = new HomePlusApiBiz().GetSpecialShop(id, settedBranch.BranchInfo.BranchCd);
				ViewBag.hasBranchCode = true;
			}
			else
			{
				apiResult = new HomePlusApiBiz().GetSpecialShop(id, 0);
			}

			ViewBag.Id = id;
			ViewBag.itemListSize = LISTING_PAGE_SIZE;
			PageAttr.HeaderType = HeaderTypeEnum.Simple;

			return View(apiResult);
		}

		
		public ActionResult List(string shopCategory)
		{
			ViewBag.HeaderTitle = "홈플러스 당일배송";
			ViewBag.hasBranchCode = false;
			if(PageAttr.IsLogin && this.settedBranch != null)
			{
				ViewBag.BranchCode = settedBranch.BranchInfo.BranchCd;
				ViewBag.hasBranchCode = true;
			}
			else
			{
				ViewBag.BranchCode = 0;
			}
			categoryList = GenericUtil.AtLeastReturnEmptyList(categoryList);
			ViewBag.categoryList = categoryList;

			if(!String.IsNullOrEmpty(shopCategory))
			{
				ViewBag.MenuName = "lp";
			}
			else
			{
				ViewBag.MenuName = "srp";
			}
			ViewBag.PageSize = LISTING_PAGE_SIZE;
			PageAttr.HeaderType = HeaderTypeEnum.Simple;

			return View();
		}

		[RequireSsl(Require = true)]
		public ActionResult AddressBook(string goodsCode)
		{
			if( !PageAttr.IsLogin )
			{
				return AlertMessageAndHistorybackOrClose( "로그인 하시기 바랍니다.", "-1" );
			}

			ViewBag.GoodsCode = goodsCode;
			PageAttr.IsMain = true;
			return View();
		}

		public ActionResult DeliveryTimeTable(string goodsCode)
		{
			ViewBag.GoodsCode = goodsCode;
			PageAttr.IsMain = true;
			return View();
		}

		[HttpGet]
		public ActionResult GetHomeSectionItem(string area, int pageNo = 1, int pageSize = DEFAULT_ITEM_LIST_PAGE_SIZE)
		{
			int branchCode;
			if(PageAttr.IsLogin && this.settedBranch != null)
			{
				branchCode = settedBranch.BranchInfo.BranchCd;
			}
			else
			{
				branchCode = 0;
			}
			var result = new HomePlusApiBiz().GetHomeSectionItem(area, pageNo, pageSize, branchCode);
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult GetSpecialShopSectionItem(long pageSeq, int pageNo = 1, int pageSize = LISTING_PAGE_SIZE)
		{
			int branchCode;
			if(PageAttr.IsLogin && this.settedBranch != null)
			{
				branchCode = settedBranch.BranchInfo.BranchCd;
			}
			else
			{
				branchCode = 0;
			}
			var result = new HomePlusApiBiz().GetSpecialShopSectionItem(pageSeq, pageNo, pageSize, branchCode);
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult Search(SearchRequest input)
		{
            string topCategory = string.Empty;
            string topCategorykey = string.Empty;
            string key = string.Empty;

            List<SRPSearchCategory> subCategoryList = new List<SRPSearchCategory>();

            //하위카레고리 리스트 캐시에서 조회
            if (!string.IsNullOrEmpty(input.shopCategory))
            {
                if (input.shopCategory.Substring(0, 1).IndexOf("5") >= 0)
                {
                    key = "SubCategoryList" + input.shopCategory;
                    if (HttpRuntime.Cache[key] == null)
                    {
                        SRPResultModel apiResultModelForCategory = new HomePlusApiBiz().PostSearchItem(input);

                        HttpRuntime.Cache.Insert(key, apiResultModelForCategory.ShopCategory, null, DateTime.Now.AddMinutes(10), Cache.NoSlidingExpiration);
                        subCategoryList = (List<SRPSearchCategory>)HttpRuntime.Cache[key];
                    }
                    subCategoryList = (List<SRPSearchCategory>)HttpRuntime.Cache[key];
                }
            }

            if (!string.IsNullOrEmpty(input.requestShopCategory))
            {
                input.shopCategory = input.requestShopCategory;
            }

			if(PageAttr.IsLogin && this.settedBranch != null)
			{
				input.branchCode = settedBranch.BranchInfo.BranchCd.ToString();
			}
			else
			{
				input.branchCode = "";
			}
			SRPResultModel apiResultModel = new HomePlusApiBiz().PostSearchItem(input);

            apiResultModel.ShopCategory = subCategoryList;  //하위카레고리 리스트 캐시에서 가져온 데이터로 대입

			return Json(apiResultModel);
		}


		#region homeplus header address, branch setting (used from mitem also)
		[HttpGet]
		[GMobileHandleErrorAttribute]
		[AllowCrossDomainCall("*")]
		public JsonpResult JsonReturnZipList(string locationKeyword)
		{
			string strLocationKeyword = locationKeyword + "%";
			List<SelectAddressByZipT> SttlBankList = new MyAddrBiz().GetAddressByZipList(strLocationKeyword, null, null, null, null, 1);

			return this.Jsonp(SttlBankList);
		}

		[RequireSsl(Require = true)]
		[HttpGet]
		[GMobileHandleErrorAttribute]
		[AllowCrossDomainCall("*")]
		public JsonpResult MyAddrList()
		{
			List<MyAddrListT> addrList = new HomePlusApiBiz().GetMyAddressList(gmktUserProfile.CustNo);
			int selectedAddrNo = 0;

			//if (this.shippingAddr != null && this.shippingAddr.AddrNo > 0)
			//{
			//    selectedAddrNo = this.shippingAddr.AddrNo;
			//}
			//else
			//{
				SpecialShopPrimaryAddrT addrApiResult = new HomePlusApiBiz().GetSpecialShopPrimaryAddr(gmktUserProfile.CustNo, this.sellCustNo);
				if (addrApiResult != null)
				{
					this.shippingAddr = addrApiResult;
					selectedAddrNo = this.shippingAddr.AddrNo;
					//new GMobileCookieHelper().SetCookieValue(COOKIENAME_ADDR_INFO, COOKIENAME_ZIP_CODE, this.shippingAddr.FriendZipCode);
				}
			//}
			return this.Jsonp(new{addrList = addrList, selectedAddrNo = selectedAddrNo });
		}

		[HttpGet]
		[GMobileHandleErrorAttribute]
		[AllowCrossDomainCall("*")]
		public JsonpResult HomePlusBranchName(string zipCd)
		{
			if (zipCd == null || zipCd == "")
			{
				return this.Jsonp(new { success = false });
			}

			SpecialShopZipBranchMatchingT branch = new HomePlusApiBiz().GetSpecialShopBranchInfo(zipCd, this.sellCustNo);
			if (branch != null)
			{
				if (this.settedBranch != null)
				{
					ResultI cartChkResult = new HomePlusApiBiz().GetCartBranchDataResult(this.settedBranch.BranchInfo.BranchCd);
					if (cartChkResult != null && cartChkResult.ReturnCode == "000") 
					{
						return this.Jsonp(new { success = true, branch = branch });
					}
					else
					{
						return this.Jsonp(new { success = true, branch = branch, tryBranchChangeButFail = true });
					}
				}
				else
				{
					return this.Jsonp(new { success = true, branch = branch });
				}
			}
			else
			{
				return this.Jsonp(new { success = false });
			}

		}

		[HttpGet]
		[GMobileHandleErrorAttribute]
		[AllowCrossDomainCall("*")]
		public JsonpResult SetMyHomePlusBranch(int addrNo)
		{
			if (addrNo <= 0)
			{
				return this.Jsonp(new { success = false });
			}

			//TODO : GetCartBranchData (zipcode chk)

			ApiResponse<bool> setMyHomePlusBranchResult = new HomePlusApiBiz().GetAddPrimaryAddr(addrNo, gmktUserProfile.CustNo, gmktUserProfile.LoginID, this.sellCustNo);
			if (setMyHomePlusBranchResult != null)
			{
				if (setMyHomePlusBranchResult.Data == true)
				{
					return this.Jsonp(new { success = true });
				}
				else
				{
					return this.Jsonp(new { success = false, message = setMyHomePlusBranchResult.Message });
				}
			}
			else
			{
				return this.Jsonp(new { success = false });
			}
		}

		[RequireSsl(Require=true)]
		[HttpGet]
		[GMobileHandleErrorAttribute]
		[AllowCrossDomainCall("*")]
		public JsonpResult SetMyHomePlusBranchWithNewAddr(string addrName, string addrZipCode, string addrFront, string addrBack, string addrHpNo)
		{
			//TODO : GetCartBranchData (zipcode chk)
			
			AddPrimaryAddrWithNewRequestT request = new AddPrimaryAddrWithNewRequestT();
			request.SellCustNo = this.sellCustNo;
			request.ShippingAddress = new ShippingAddressRequestT();
			request.ShippingAddress.WorkMode = "Insert";
			request.ShippingAddress.CustNo = gmktUserProfile.CustNo;
			request.ShippingAddress.FriendLoginId = gmktUserProfile.LoginID;
			request.ShippingAddress.FriendName = gmktUserProfile.CustName;
			request.ShippingAddress.FriendAddressAlias = addrName;
			request.ShippingAddress.FriendFrontAddress = addrFront;
			request.ShippingAddress.FriendBackAddress =addrBack;
			request.ShippingAddress.FriendHandPhoneNo = addrHpNo;
			request.ShippingAddress.FriendTelNo = addrHpNo;
			request.ShippingAddress.FriendZipCode = addrZipCode;
			request.ShippingAddress.FriendNationISOCode = "KR";
			request.ShippingAddress.ChangeDate = DateTime.Now;
			request.ShippingAddress.ChangeId = gmktUserProfile.LoginID;
			request.ShippingAddress.IsDeliverySeqNo = "Y";
			request.ShippingAddress.LoginId = gmktUserProfile.LoginID;

			ApiResponse<bool> setMyHomePlusBranchResult = new HomePlusApiBiz().PostAddPrimaryAddrWithNew(request);

			if (setMyHomePlusBranchResult != null)
			{
				if (setMyHomePlusBranchResult.ResultCode == 0)
				{
					return this.Jsonp(new { success = true });
				}
				else if (setMyHomePlusBranchResult.ResultCode == 1)
				{
					return this.Jsonp(new { success = false, message = setMyHomePlusBranchResult.Message });
				}
				else
				{
					return this.Jsonp(new { success = false });
				}
			}
			else
			{
				return this.Jsonp(new { success = false });
			}
		}

		[HttpGet]
		[GMobileHandleErrorAttribute]
		[AllowCrossDomainCall("*")]
		public JsonpResult GetMyHomePlusBranchTimeTable()
		{
			if (this.shippingAddr == null || this.shippingAddr.FriendBackAddress == null || this.shippingAddr.FriendBackAddress == null)
			{
				SpecialShopPrimaryAddrT addrApiResult = new HomePlusApiBiz().GetSpecialShopPrimaryAddr(gmktUserProfile.CustNo, this.sellCustNo);
				if (addrApiResult != null)
				{
					this.shippingAddr = addrApiResult;
					//new GMobileCookieHelper().SetCookieValue(COOKIENAME_ADDR_INFO, COOKIENAME_ZIP_CODE, this.shippingAddr.FriendZipCode);
				}
			}

			if (this.shippingAddr == null || string.IsNullOrEmpty(this.shippingAddr.FriendZipCode))
			{
				return this.Jsonp(new { success = false });
			}

			if (this.settedBranch != null && this.settedBranch.BranchInfo != null && this.settedBranch.TimeSlot != null && this.settedBranch.TimeTable != null)
			{
				return this.Jsonp(new { success = true, shippingAddr = this.shippingAddr, branchNm = this.settedBranch.BranchInfo.BranchNm, timeTable = this.settedBranch.TimeTable, timeSlot = this.settedBranch.TimeSlot });
			}
			else
			{
				DateTime deliStartDt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
				DateTime deliEndDt = deliStartDt.AddDays(7);
				SpecialShopBranchInfoTimeTable branchInfoTimeTable = new HomePlusApiBiz().GetSpecialShopBranchInfoTimeTable(this.shippingAddr.FriendZipCode, this.sellCustNo, deliStartDt, deliEndDt);
				if (branchInfoTimeTable != null)
				{
					this.settedBranch = branchInfoTimeTable;
					return this.Jsonp(new { success = true, shippingAddr = this.shippingAddr, branchNm = this.settedBranch.BranchInfo.BranchNm, timeTable = this.settedBranch.TimeTable, timeSlot = this.settedBranch.TimeSlot });
				}
				else
				{
					return this.Jsonp(new { success = false });
				}
			}
		}

		[HttpGet]
		public ActionResult OrderGate(string orderParams = "", string goodsNo = "", string pbid = "")
		{
			if (gmktShoppingInfo != null && string.IsNullOrEmpty(gmktShoppingInfo.PBID))
			{
				gmktShoppingInfo.SetPBId(pbid);
			}

			string orderUrl = String.Format("{0}/ko/branch?orderIdx={1}&itemNo={2}", Urls.MEscrowUrl, orderParams, goodsNo);
			ViewBag.OrderUrl = orderUrl;

			return View();
		}
		#endregion


		/// <summary>
		/// </summary>
		/// <param name="itemNo"></param>
		/// <param name="orderQty"></param>
		/// <returns></returns>
		[HttpPost]
		public JsonResult GetAddCartResult(string itemNo, short orderQty, bool isInstantOrder)
		{
			if (this.shippingAddr != null && !string.IsNullOrEmpty(this.shippingAddr.FriendZipCode))
			{
				ApiResponse<Nova.Thrift.AddCartResultI>  result = new ECouponApiBiz().GetAddCartResult(itemNo, orderQty, isInstantOrder, this.shippingAddr.FriendZipCode);
				if (gmktShoppingInfo != null && string.IsNullOrEmpty(gmktShoppingInfo.PBID))
				{
					if (result != null && result.Data != null && !string.IsNullOrEmpty(result.Data.CartPID))
					{
						gmktShoppingInfo.SetPBId(result.Data.CartPID);
					}
				}
				return Json(result.Data.Result);
			}
			else
			{
				return Json(null);
			}
			
			

		}

		/// <summary>
		/// </summary>
		[HttpPost]
		public JsonResult GetRemoveCartResult(string orderIdx)
		{
			ApiResponse<Nova.Thrift.ResultI> result = new ApiResponse<ResultI>();
			if (gmktShoppingInfo != null && string.IsNullOrEmpty(gmktShoppingInfo.PBID))
			{
				string newPbid = string.Empty;
				CartPIDResultI pidResult = new HomePlusApiBiz().GetCartPIDResult("0", gmktUserProfile.CustNo, false);
				if (pidResult != null)
				{
					newPbid = pidResult.CartPID;
					if (!string.IsNullOrEmpty(newPbid))
					{
						gmktShoppingInfo.SetPBId(newPbid);
					}
					result = new ECouponApiBiz().GetRemoveCartResult(newPbid, orderIdx);
				}
				else
				{
					return Json(new { success = false });
				}
			}
			else
			{
				result = new ECouponApiBiz().GetRemoveCartResult(gmktShoppingInfo.PBID, orderIdx);
			}
			
			if (result != null && result.Data != null && result.Data.ReturnCode == "000")
			{
				CartListResultI cartResult = new HomePlusApiBiz().GetCartResult(this.settedBranch.BranchInfo.BranchCd);
				if (cartResult != null && cartResult.Result != null && cartResult.Result.ReturnCode == "000")
				{
					this.cartList = cartResult.CartListDetail;

					return Json(new { success = true, cartlist = ConvertToCartListModel(this.cartList) });
				}
				else
				{
					return Json(new { success = false });
				}
			}
			else
			{
				return Json(new { success = false });
			}
		}

		[HttpPost]
		public JsonResult GetCartResult()
		{
			CartListResultI cartResult = new CartListResultI();
			if (this.settedBranch != null && this.settedBranch.BranchInfo != null)
			{
				cartResult = new HomePlusApiBiz().GetCartResult(this.settedBranch.BranchInfo.BranchCd);
			}
			
			if (cartResult != null && cartResult.Result != null && cartResult.Result.ReturnCode == "000")
			{
				this.cartList = cartResult.CartListDetail;

				return Json(new { success = true, cartlist = ConvertToCartListModel(this.cartList) });
			}
			else
			{
				return Json(new { success = false });
			}
		}

		[HttpGet]
		public JsonResult GetItemMinUnitBuyCount(string itemNo)
		{
			ItemMinUnitBuyCountT itemCntResult = new HomePlusApiBiz().GetItemMinUnitBuyCount(itemNo);
			if (itemCntResult != null )
			{
				return Json(new { success = true, itemcntdata = itemCntResult }, JsonRequestBehavior.AllowGet);
			}
			else
			{
				return Json(new { success = false }, JsonRequestBehavior.AllowGet);
			}
		}

        /// <summary>
        /// 나의 지점정보 조회
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetMyBranchInfo()
        {
            SpecialShopZipBranchMatchingT brandInfoResult = new SpecialShopZipBranchMatchingT();
            if (PageAttr.IsLogin && this.settedBranch != null)
            {
                brandInfoResult = this.settedBranch.BranchInfo;
                return Json(new { success = true, branchInfo = brandInfoResult }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

		private List<HomePlusCartListModel> ConvertToCartListModel(List<CartListDetailI> cartList)
		{
			List<HomePlusCartListModel> result = new List<HomePlusCartListModel>();
			foreach(CartListDetailI item in cartList)
			{
				HomePlusCartListModel model = new HomePlusCartListModel();
				model.IsOrderFail = item.IsOrderFail;
				model.OrderIdx = item.OrderIdx;
				model.ItemInfo = item.ItemInfo;
				model.OptionInfo = item.OptionInfo;
				model.OrderQty = item.OrderQty;
				model.ItemLinkUrl = Urls.MItemWebURL + "/Item?goodscode=" + item.ItemInfo.ItemNo; 
				model.ItemImageUrl = BizUtil.GetGoodsImagePath(item.ItemInfo.ItemNo.ToString(), "S");

				if (model.IsOrderFail)
				{
					model.dblPrice = 0;
				}
				else
				{
					double optionTotalPrice = 0;
					if (item.OptionInfo != null)
					{
						foreach (var option in item.OptionInfo)
						{
							optionTotalPrice += option.OptionPrice * option.OptionQty;
						}
					}
					double branchPrice = 0;
					if (item.BranchOption != null)
					{
						branchPrice = item.BranchOption.BranchPrice;
					}

					model.dblPrice = (item.OrderQty * (item.ItemInfo.OriginSellPrice + branchPrice)) + optionTotalPrice;

					if (item.DiscountInfo != null)
					{
						model.dblPrice -= item.DiscountInfo.DiscountPrice;
					}
				}

				model.strPrice = model.dblPrice.ToString("#,##0");
				model.dblShippingCost = item.ShippingInfo.ShippingCost;
				model.strShippingCost = item.ShippingInfo.ShippingCost.ToString("#,##0");

				result.Add(model);
			}
			return result;
		}
	}
}

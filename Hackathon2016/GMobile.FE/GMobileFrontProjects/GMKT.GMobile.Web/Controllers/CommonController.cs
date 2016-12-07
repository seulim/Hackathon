using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GMobile.Data.DisplayDB;
using GMobile.Service.Home;
using GMKT.GMobile.Biz;
using GMobile.Data.Voyager;
using GMKT.GMobile.Web.Models;
using GMKT.GMobile.Util;
using System.Collections.Specialized;
using GMKT.GMobile.Data;
using GMKT.GMobile.CommonData;

namespace GMKT.GMobile.Web.Controllers
{
	public class CommonController : GMobileControllerBase
	{
		public static readonly int GOODS_CODE_LENGTH = 9;
		public static readonly string[] ALLOWED_CROSS_DOMAIN_CALL = 
		{
			Urls.MobileWebUrl, 
			Urls.MobileWebUrlSecure, 
			Urls.MDeliveryUrl, 
			Urls.MItemWebURL, 
			Urls.MMyGUrl, 
			Urls.MMyGUrlSecure,
			Urls.MClaimUrl,
			Urls.MClaimUrlSecure,
			Urls.MEscrowUrl,
			Urls.MEscrowUrlSecure,
			Urls.MAirUrl,
			Urls.MTourUrl,
			Urls.MTourWebUrl,
			Urls.MWebUrl,
			Urls.MWebUrlSecure,
			Urls.MemberSignInRootSecure
		};

		public ActionResult Index()
		{
			return null;
		}

		public ActionResult AdultGoodsViewAgree(string goodsNo)
		{
			ViewData["goodsNo"] = goodsNo;

			return View();
		}

		[OutputCache(Duration = 600)]
		[AllowCrossDomainCall("*")]
		public ActionResult HomeService()
		{
			List<MobileHomeServiceT> service = new MobileHomeServiceBiz().GetMobileHomeService();

			return Json(service, JsonRequestBehavior.AllowGet);
		}

		[OutputCache(Duration = 600)]
		[AllowCrossDomainCall("*")]
		public ActionResult NoticeList(int rowCount=5)
		{
			List<MobileNoticeT> noticeList = new MobileNoticeBiz().GetMobileNotice(1, rowCount);

			return Json(noticeList, JsonRequestBehavior.AllowGet);
		}

		public JsonpResult Header(bool isMain = false, string code = "", HeaderTypeEnum type = HeaderTypeEnum.Simple)
		{
			PageAttr.IsMain = isMain;

			ViewBag.IsDynamicHeader = false;
			ViewBag.IsCornerHeader = false;
			ViewBag.HideIcons = "";			

			if(type == HeaderTypeEnum.Normal || type == HeaderTypeEnum.Srp)
			{
				ViewBag.CartId = "cartIcon";
			}
			else
			{
				ViewBag.CartId = "simple_cartIcon";
			}

			if(!String.IsNullOrEmpty(code))
			{
				if(type == HeaderTypeEnum.Vip || type == HeaderTypeEnum.Minishop)
				{
					ViewBag.IsDynamicHeader = true;
					if(code.ToLower() == "superdeal")
					{
						ViewBag.HeaderType = GoodsHeaderType.Banner;
						ViewBag.Text = "슈퍼딜";
						ViewBag.ImageUrl = "http://pics.gmkt.kr/mobile/app/logo_sdeal@2x.png";
						ViewBag.LandingUrl = Urls.MobileWebUrl + "/SuperDeal";
					}
					else if (code.ToLower() == "department")
					{
						ViewBag.IsCornerHeader = true;
						ViewBag.HeaderType = GoodsHeaderType.Banner;
						ViewBag.Text = "백화점 Now";
						ViewBag.ImageUrl = "http://pics.gmkt.kr/mobile/app/gnb_logo_dpt@2x.png";
						ViewBag.LandingUrl = Urls.MobileWebUrl + "/DepartmentStore";
						ViewBag.HideIcons = "search";
					}
					else if (code.ToLower() == "smartdelivery")
					{
						ViewBag.IsCornerHeader = true;
						ViewBag.HeaderType = GoodsHeaderType.Banner;
						ViewBag.Text = "스마트배송";
						ViewBag.ImageUrl = "http://pics.gmkt.kr/mobile/app/gnb_logo_smd@2x.png ";
						ViewBag.LandingUrl = Urls.MobileWebUrl + "/smartDelivery";
						ViewBag.HideIcons = "search";
					}
					else
					{
						DynamicHeader title = null;
						if(type == HeaderTypeEnum.Vip)
						{
							title = new MobileCommonBiz_Cache().GetVipHeader(code);
						}
						else if(type == HeaderTypeEnum.Minishop)
						{
							title = new MobileCommonBiz().GetDynamicHeader(type, EncodeUtil.custNoDecode(code));
						}

						if(title != null)
						{
							ViewBag.HeaderType = title.HeaderType;
							ViewBag.Text = String.IsNullOrEmpty(title.Text) ? String.Empty : title.Text;
							ViewBag.ImageUrl = String.IsNullOrEmpty(title.ImageUrl) ? String.Empty : title.ImageUrl;
							ViewBag.LandingUrl = String.IsNullOrEmpty(title.LandingUrl) ? String.Empty : title.LandingUrl;
						}
					}
				}				
			}

			ViewBag.type = type;

			return this.Jsonp(this.RenderViewToString());
		}

		public JsonpResult Footer(bool isMain = false, bool isPushLanding = false, string goodsCode = "")
		{
			string pif = gmktUserProfile.PIFString;
			string sif = gmktUserProfile.SIFString;

			PageAttr.IsMain = isMain;
			PageAttr.IsPushLanding = isPushLanding;
            ViewData["goodsCode"] = goodsCode;

			CheckAppVersion checkNewVersion = new CheckAppVersion();
			if(!checkNewVersion.CheckOverVersion(PageAttr.AppVersion, checkNewVersion.GetAppVersion("5.5.1"))) 
			{
				int appNo = 1;

				if(isPushLanding)
				{
					var pushServiceAgreement = new MobileCommonBiz().GetPushServiceAgreementInfo(pif, sif, appNo);				

					if (pushServiceAgreement != null && !string.IsNullOrEmpty(pushServiceAgreement.ServiceAgreeYn))
					{
						PageAttr.IsPushLanding = pushServiceAgreement.ServiceAgreeYn.ToUpper() == "Y";
					}
				}
			}
			
			return this.Jsonp(this.RenderViewToString());
		}

		public JsonpResult DeclinePushMessage()
		{
			string loginID = gmktUserProfile.LoginID;
			string pif = gmktUserProfile.PIFString;
			string sif = gmktUserProfile.SIFString;
			int appNo = 1;

			var result = new MobileCommonBiz().SetPushServiceAgreementInfo(pif, sif, appNo, "N", loginID);
			
			if(result.sRetCode == 0)
			{
				return this.Jsonp(new { success = true });
			}
			else 
			{
				return this.Jsonp(new { success = false });
			}			
		}

		public ActionResult SetRecentItems(string goodsNo)
		{
			if (false == string.IsNullOrEmpty(goodsNo) && goodsNo.Length == GOODS_CODE_LENGTH)
			{
				return View((object)goodsNo);
			}
			else
			{
				return null;
			}
		}

		public ActionResult RecentThings()
		{
			return View();
		}

		public ActionResult CartCount()
		{
			return View();
		}

		[AllowCrossDomainCall("*")]
		[HttpGet]
		public JsonpResult GetCartCount(string pid="")
		{
			int count = new MobileCommonBiz().GetCartCount(pid);

			if(count >= 0)
			{
				return this.Jsonp(new { success = true, count = count });
			}
			else
			{
				return this.Jsonp(new { success = false, count = count });
			}
			
		}

		[AllowCrossDomainCall("*")]
		[HttpPost]
		public JsonResult GetGoodsInfos(List<string> goodsNoList)
		{
			GetGoodsInfosM model = new GetGoodsInfosM();
			
			if (goodsNoList != null && goodsNoList.Count > 0)
			{
				var isValidate = true;
				// 상품번호 validation
				foreach (var eachGoodsNo in goodsNoList)
				{
					if (eachGoodsNo.Length != GOODS_CODE_LENGTH)
					{
						isValidate = false;
						break;
					}
				}

				// 중복으로 들어가있는 상품번호가 있다면 1개만 남기고 삭제
				goodsNoList = goodsNoList.Distinct().ToList();

				if (isValidate)
				{
					SearchItemT[] result = new SearchBiz().GetItems(goodsNoList, Data.DisplayOrder.New);

					if (result.Length > 0)
					{
						model.GoodsInfoList = new List<GoodsInfoM>();

						foreach (var eachGoodsNo in goodsNoList)
						{
							SearchItemT eachSearchItem = result.FirstOrDefault(o => o.GdNo.ToString() == eachGoodsNo);

							if (eachSearchItem != null)
							{
								GoodsInfoM eachGoods = new GoodsInfoM()
								{
									GoodsNo = eachSearchItem.GdNo.ToString(),
									GoodsName = eachSearchItem.Name,
									OriginalPrice = eachSearchItem.SellPrice,
									DcPrice = (eachSearchItem.Discount != null) ? (decimal?)(eachSearchItem.SellPrice - eachSearchItem.Discount.DiscountPrice) : null,
									ImageUrl = BizUtil.GetGoodsImagePath(eachSearchItem.GdNo.ToString(), "M")
								};

								model.GoodsInfoList.Add(eachGoods);
							}
						}
						
						model.Success = true;

						return Json(model);
					}
					else
					{
						model.Success = true;
						model.Message = "조건에 맞는 결과가 없습니다.";

						return Json(model);
					}
				}
				else
				{
					model.Success = false;
					model.Message = "잘못된 상품번호가 있습니다.";

					return Json(model);
				}
			}
			else
			{
				model.Success = false;
				model.Message = "잘못된 입력입니다.";

				return Json(model);
			}
		}

		public JsonpResult RecentItemsLayer()
		{
			return this.Jsonp(this.RenderViewToString());
		}

		[HttpGet]
		public JsonResult AddFavoriteItems(string itemNos)
		{
			int retValue = 0;
			string retMsg = string.Empty;
			int itemCount = 0;

			GMKT.GMobile.Data.RegInterestItemsInfo result = new Data.RegInterestItemsInfo();

			if (false == String.IsNullOrEmpty(gmktUserProfile.CustNo))
			{
				if (!string.IsNullOrEmpty(itemNos))
				{
					string[] goodsCodeList = itemNos.Split(',');
					itemCount = goodsCodeList.Count();

					result = new MobileCommonBiz().RegInterestItems(gmktUserProfile.CustNo, itemNos);
					if (result != null /*&& result.SuccessCount == itemCount*/)
					{
						retMsg = "관심상품으로 등록되었습니다.";
					}
					else
					{
						retValue = -999;
						retMsg = "관심상품 등록중 실패가 발생하였습니다";
					}
				}
				else 
				{
					retValue = -200;
					retMsg = "상품을 선택해주세요.";
				}
			}
			else
			{
				retValue = -100;
				retMsg = "로그인이 필요합니다.";
			}

			if (ALLOWED_CROSS_DOMAIN_CALL.Contains(Request.Headers.Get("Origin")))
			{
				Response.AddHeader("Access-Control-Allow-Origin", Request.Headers.Get("Origin"));
				Response.AddHeader( "Access-Control-Allow-Credentials", "true" );
			}			

			return Json(new { result = retValue, msg = retMsg }, JsonRequestBehavior.AllowGet);
		}

	}
}
